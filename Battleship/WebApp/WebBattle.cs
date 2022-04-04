using System;
using System.Collections;
using System.Collections.Generic;
using Domain;
using Domain.Model;
using Game;
using RogueSharp;

namespace WebApp
{
    public class WebBattle : BaseBattleship
    {
        public WebBattle(GameData gameData) : base(gameData)
        {
            Initialize();
        }

        public WebBattle(int boardHeight, int boardWidth, string ships, int allowAdjacentPlacement, int startingPlayerType, int secondPlayerType)
            : base(boardHeight, boardWidth, ships, allowAdjacentPlacement, startingPlayerType, secondPlayerType)
        {
            Initialize();
        }

        private void Initialize()
        {

        }
    }
}