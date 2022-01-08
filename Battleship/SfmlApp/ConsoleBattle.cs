using System;
using System.Text;
using Domain.Model;
using Game;
using IrrKlang;
using SFML.Graphics;
using SFML.Window;

namespace SfmlApp
{
    public class ConsoleBattle : BaseBattleship
    {
       public RenderWindow Window = null!;
       

       public const int ScreenWidth = 480;
       public const int ScreenHeight = 360;
       public const int FontW = 8;
       public const int FontH = 8;

       

       public ConsoleBattle(GameData gameData) : base(gameData)
       {
          Initialize();
       }

       public ConsoleBattle(int boardHeight, int boardWidth, string ships, int allowAdjacentPlacement, int startingPlayerType, int secondPlayerType)
          : base(boardHeight, boardWidth, ships, allowAdjacentPlacement, startingPlayerType, secondPlayerType)
       {
          Initialize();
       }

       private void Initialize()
       {
          Window = new RenderWindow(new VideoMode(480, 360), "Battleships");
          Window.Closed += (sender, e) =>
          {
             Window.Close();
          };
          Window.SetFramerateLimit(0);
          Console.OutputEncoding = Encoding.Unicode;
          const SoundEngineOptionFlag options = 
             SoundEngineOptionFlag.Use3DBuffers | 
             SoundEngineOptionFlag.MultiThreaded | 
             // SoundEngineOptionFlag.PrintDebugInfoIntoDebugger |
             // SoundEngineOptionFlag.PrintDebugInfoToStdOut | 
             SoundEngineOptionFlag.LoadPlugins;
          SoundEngine = new ISoundEngine(SoundOutputDriver.AutoDetect, options);
          Input = new ConsoleInput(Window);
       }

       /// <param name="deltaTime">Provides a snapshot of timing values.</param>
       /// <param name="gameData">Game data</param>
       public override void Draw(double deltaTime, GameData gameData)
       {
          ConsoleDrawLogic.Draw(deltaTime, gameData, Window);
       }
    }
}