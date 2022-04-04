using System;
using System.Text;
using ConsoleGameEngineCore;
using Domain.Model;
using Game;
using IrrKlang;

namespace PureConsoleApp
{
    public class ConsoleBattle : BaseBattleship
    {
        public static ConsoleEngine ConsoleEngine = null!;
       

        public const int ScreenWidth = 60;
        public const int ScreenHeight = 45;
        private const int FontW = 8;
        private const int FontH = 8;

       

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
            ConsoleEngine = new ConsoleEngine(ScreenWidth, ScreenHeight, FontW, FontH);
            Console.OutputEncoding = Encoding.Unicode;
            const SoundEngineOptionFlag options = 
                SoundEngineOptionFlag.Use3DBuffers | 
                SoundEngineOptionFlag.MultiThreaded | 
                // SoundEngineOptionFlag.PrintDebugInfoIntoDebugger |
                // SoundEngineOptionFlag.PrintDebugInfoToStdOut | 
                SoundEngineOptionFlag.LoadPlugins;
            SoundEngine = new ISoundEngine(SoundOutputDriver.AutoDetect, options);
        }
    }
}