using System;
using System.Collections;
using System.Collections.Generic;
using Domain;
using Domain.Model;
using Game;
using IrrKlang;
using RogueSharp;

namespace WebApp
{
    public class WebBattle : BaseBattleship
    {
        public WebBattle(GameData gameData, BaseInput webInput) : base(gameData)
        {
            // Initialize();
            Input = webInput;
        }

        public WebBattle(int boardHeight, int boardWidth, string ships, int allowAdjacentPlacement, int startingPlayerType, int secondPlayerType, BaseInput webInput)
            : base(boardHeight, boardWidth, ships, allowAdjacentPlacement, startingPlayerType, secondPlayerType)
        {
            // Initialize();
            Input = webInput;
        }

        private void Initialize()
        {
            const SoundEngineOptionFlag options = 
                SoundEngineOptionFlag.Use3DBuffers | 
                SoundEngineOptionFlag.MultiThreaded | 
                // SoundEngineOptionFlag.PrintDebugInfoIntoDebugger |
                // SoundEngineOptionFlag.PrintDebugInfoToStdOut | 
                SoundEngineOptionFlag.LoadPlugins;
            SoundEngine = new ISoundEngine(SoundOutputDriver.AutoDetect, options);
        }

        public override void Draw(double deltaTime, GameData data)
        {
            // this cannot be used
            throw new System.NotImplementedException();
        }
    }
}