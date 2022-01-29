// See https://aka.ms/new-console-template for more information

using System;
using Domain.Model;
using Game;
using SFML.Window;

namespace Sdl2App
{
    class Program
    {
        public static void Main(string[] args)
        {
            var game = new ConsoleBattle(10, 10, "1x5N1; 1x4N2; 1x3N3; 1x2N4", 0, -1, -1);
            
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
        }
    }
}
