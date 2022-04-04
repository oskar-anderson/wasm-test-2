using System;
using System.Collections.Generic;
using Domain;
using Domain.Model;
using Domain.Tile;
using RogueSharp;
using Point = RogueSharp.Point;

namespace Game
{
    public class BaseBattleship
    {
       public readonly GameData GameData;

       public BaseBattleship(GameData gameData)
       {
          GameData = gameData;
       }

       public BaseBattleship(int boardHeight, int boardWidth, string ships, int allowAdjacentPlacement, int startingPlayerType, int secondPlayerType)
       {
          List<Point> shipList;
          string errorMsg = "";
          if (! Utils.ShipStringParse(ships, out shipList, ref errorMsg))
          {
             throw new Exception($"Unexpected! Failed to parse: {ships}! This should have been checked before! {errorMsg}");
          }
          if (ships == null) throw new ArgumentNullException(nameof(ships));
          if (ships.Length == 0) throw new Exception("No ships provided!");
          
          const int playerVerticalSeparator = 10;
          string[,] boardMap = TileFunctions.GetRndSeaTiles(boardWidth, boardHeight * 2 + playerVerticalSeparator);
          for (int y = boardHeight; y < boardHeight + playerVerticalSeparator; y++)
          {
             for (int x = 0; x < boardMap.GetWidth(); x++)
             {
                Point point = new Point(x, y);
                boardMap.Set(point, TextureValue.VoidTile);
             }
          }

          List<Sprite> sprites = new List<Sprite>();
          Player activePlayer = new Player(
             new Rectangle(0, 0, boardWidth, boardHeight),
             new Point(4,4),
             startingPlayerType,
             "Player A",
             Point.Zero,
             sprites);
          Player inactivePlayer = new Player(
             new Rectangle(0, boardHeight + playerVerticalSeparator, boardWidth, boardHeight),
             new Point(4,boardHeight + playerVerticalSeparator + 4),
             secondPlayerType,
             "Player B",
             new Point(0,(boardHeight + playerVerticalSeparator) * TileData.Height),
             sprites);
          
          GameData = new GameData(allowAdjacentPlacement, boardMap, shipList, activePlayer, inactivePlayer, sprites);
       }
    }
}