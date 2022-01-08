using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp.GameMenu;
using DAL;
using Domain;
using Domain.Model;
using Game;
using Microsoft.EntityFrameworkCore;
using SFML.Window;
using Point = RogueSharp.Point;

namespace ConsoleApp
{
    internal static class Program
    {
        private static void Main()
        {
            List<Menu.MenuAction> menuTree = new List<Menu.MenuAction>() { Menu.getMenuMain() };
            while (true)
            {
                Console.CursorVisible = false;
                bool isContinueActive;
                try
                {
                    isContinueActive = DbQueries.SavesAmount() != 0;
                }
                catch
                {
                    isContinueActive = false;
                }

                RuleSet ruleSet = Menu.Start(menuTree, isContinueActive);
                
                ConsoleBattle game;
                switch (ruleSet.ExitCode)
                {
                    case ExitResult.Start:
                        game = new ConsoleBattle(ruleSet.BoardHeight, ruleSet.BoardWidth, ruleSet.Ships, ruleSet.AllowedPlacementType, -1, -1);
                        break;
                    case ExitResult.Continue:
                        DbQueries.TryGetGameWithIdx(0, out GameData? gameDataTemp);
                        if (gameDataTemp == null) { throw new Exception("unexpected!");}
                        game = new ConsoleBattle(gameDataTemp);
                        break;
                    case ExitResult.Exit:
                        return;
                    default:
                        throw new Exception("unexpected");
                }

                GameResult Gameloop(BaseBattleship game)
                {
                    DateTime startTime = DateTime.Now;
                    Window window = ((ConsoleBattle) game).Window;
                    while (window.IsOpen)
                    {
                        window.DispatchEvents();
                        double elapsedTime = (DateTime.Now - startTime).TotalSeconds;
                        startTime = DateTime.Now;
                        double timeCap = Math.Min(elapsedTime, 0.05);  // 20 fps
                        bool running = BaseBattleship.Update(timeCap, game);
                        if (!running)
                        {
                            window.Close();
                            break;
                        }
                        game.Draw(timeCap, game.GameData);
                    }
                    
                    var gameResult = new GameResult(UpdateLogic.IsOver(game.GameData, out string winner), game.GameData);

                    return gameResult;
                }

                GameResult gameResult = Gameloop(game);
                if (gameResult.IsOver)
                {
                    return;
                }
                
                PauseMenu.PauseResult result;
                while (true)
                {
                    Console.CursorVisible = true;
                    result = PauseMenu.Run();
                    if (result == PauseMenu.PauseResult.LoadDb)
                    {
                        DbQueries.TryGetGameWithIdx(0, out GameData? gameDataTemp);
                        if (gameDataTemp == null) { throw new Exception("unexpected!");}
                        game = new ConsoleBattle(gameDataTemp);
                    }
                    if (result == PauseMenu.PauseResult.LoadJson)
                    {
                        bool isGood = DataManager.LoadGameAction(out GameData? gameDataTemp);
                        if (isGood)
                        {
                            if (gameDataTemp == null) { throw new Exception("unexpected"); }
                            game = new ConsoleBattle(gameDataTemp);
                        }
                    }
                    if (result == PauseMenu.PauseResult.Cont)
                    {
                        game = new ConsoleBattle(gameResult.Data);
                    }
                    if (result == PauseMenu.PauseResult.LoadDb 
                        || result == PauseMenu.PauseResult.LoadJson
                        || result == PauseMenu.PauseResult.Cont)
                    {
                        Console.CursorVisible = false;
                        gameResult = Gameloop(game);
                        if (gameResult.IsOver)
                        {
                            return;
                        }
                    }
                    else { break; }
                }
                switch (result)
                {
                    case PauseMenu.PauseResult.SaveDb:
                        DbQueries.SaveAndDeleteOthers(gameResult.Data);
                        break;
                    case PauseMenu.PauseResult.SaveJson:
                        DataManager.SaveGameAction(gameResult.Data);
                        break;
                    case PauseMenu.PauseResult.MainMenu:
                        menuTree = new List<Menu.MenuAction>() { Menu.getMenuMain() };
                        continue;
                }
                if (result != PauseMenu.PauseResult.SaveDb &&
                    result != PauseMenu.PauseResult.SaveJson &&
                    result != PauseMenu.PauseResult.MainMenu)
                {
                    return;
                }
            }
        }
    }
}