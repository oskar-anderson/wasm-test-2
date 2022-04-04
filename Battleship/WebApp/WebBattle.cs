﻿using System;
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
        public WebBattle(GameData gameData) : base(gameData)
        {
            // Initialize();
        }

        public WebBattle(int boardHeight, int boardWidth, string ships, int allowAdjacentPlacement, int startingPlayerType, int secondPlayerType)
            : base(boardHeight, boardWidth, ships, allowAdjacentPlacement, startingPlayerType, secondPlayerType)
        {
            // Initialize();
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
    }
}