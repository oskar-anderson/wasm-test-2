using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Domain;
using Domain.Model;
using Domain.Tile;
using Game.Pack;
using RogueSharp;
using Point = RogueSharp.Point;

namespace Game
{
    [SuppressMessage("ReSharper", "InlineOutVariableDeclaration")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "IdentifierTypo")]
    public class UpdateLogic
    {
       /// <param name="deltaTime">Time since last update in seconds.</param>
       /// <param name="basegame">Base game with game data and input handling</param>
       public virtual bool Update(double deltaTime, BaseBattleship basegame)
       {
          if (basegame.GameData.State == GameState.GameOver)
          {
             if (!basegame.GameData.Input.Keyboard.KeyboardState
                    .Any(x => x.Identifier.Key == Input.KeyboardInput.KeyboardIdentifierList.KeyZ.Key && 
                              x.Values.Contains(Input.BtnState.Pressed)) 
                 && !basegame.GameData.Input.Keyboard.KeyboardState
                    .Any(x => x.Identifier.Key == Input.KeyboardInput.KeyboardIdentifierList.Escape.Key && 
                              x.Values.Contains(Input.BtnState.Pressed)) 
                 ) return true;
             return false;
          }

          if (basegame.GameData.Input.Keyboard.KeyboardState
             .Any(x => x.Identifier.Key == Input.KeyboardInput.KeyboardIdentifierList.Escape.Key && 
                       x.Values.Contains(Input.BtnState.Pressed))
             )
          {
             return false;
          }

          basegame.GameData.ActivePlayer.fKeyboardMoveTimeout = (float) Math.Max(-1f, basegame.GameData.ActivePlayer.fKeyboardMoveTimeout - deltaTime);
          HandlePlayerMovement(out bool posChanged, basegame.GameData.ActivePlayer, basegame.GameData.Board2D, basegame.GameData.Input);
          basegame.GameData.ActivePlayer.fKeyboardMoveTimeout = posChanged ? 0.1f : basegame.GameData.ActivePlayer.fKeyboardMoveTimeout;
          
          ResolvePhase(basegame.GameData);
          

          HandleZooming(basegame.GameData.ActivePlayer, basegame.GameData.Input);
          HandleKeyboardPanning(deltaTime, basegame.GameData.ActivePlayer, basegame.GameData.Input);
          HandleMousePanning(basegame.GameData.ActivePlayer, basegame.GameData.Input);
          HandleMouseSelection(basegame.GameData.ActivePlayer, basegame.GameData.Input);
          
          return true;
       }

       private static void ResolvePhase(GameData gameData)
       {
          switch (gameData.State)
          {
             case GameState.Placement:
                if (gameData.Board2D.Get(gameData.ActivePlayer.Sprite.Pos) == TextureValue.IntactShip)
                   gameData.ActivePlayer.Sprite.SetSpriteToSelectedTileRed();
                else
                   gameData.ActivePlayer.Sprite.SetSpriteToSelectedTileGreen();
                var dialogAction = Input.KeyboardInput.KeyboardIdentifierList.KeyZ;
                var dialogRot = Input.KeyboardInput.KeyboardIdentifierList.KeyX;
                var dialogRandomize = Input.KeyboardInput.KeyboardIdentifierList.Digit1;
                var dialogClear = Input.KeyboardInput.KeyboardIdentifierList.Digit2;
                var dialogStart = Input.KeyboardInput.KeyboardIdentifierList.Digit3;

                ShipPlacementStatus shipPlacementStatus = GetShipPlacementStatus(gameData);
                Dictionary<Input.KeyboardInput.KeyboardIdentifier, Player.DialogItem> activeKeys = new Dictionary<Input.KeyboardInput.KeyboardIdentifier, Player.DialogItem>() 
                {
                   { dialogAction, new Player.DialogItem(shipPlacementStatus.isPlaceable, "Z", "Place") },
                   { dialogRot, new Player.DialogItem(shipPlacementStatus.hitboxRect != null,"X", "Rotate") },
                   { dialogRandomize, new Player.DialogItem(true,"1", "Randomize") },
                   { dialogClear, new Player.DialogItem(true,"2", "Clear") },
                   { dialogStart, new Player.DialogItem(shipPlacementStatus.isStartable,"3", "Start") }
                };

                if (activeKeys[dialogStart].isActive && gameData.Input.Keyboard.KeyboardState
                       .Any(x => x.Identifier.Key == dialogStart.Key && 
                                 x.Values.Contains(Input.BtnState.Pressed) && 
                                 ! x.Values.Contains(Input.BtnState.Echo))
                    )
                {
                   (gameData.ActivePlayer, gameData.InactivePlayer) = (gameData.InactivePlayer, gameData.ActivePlayer);
                   shipPlacementStatus = GetShipPlacementStatus(gameData);
                   if (gameData.ActivePlayer.ShipBeingPlacedIdx == -1)
                   {
                      gameData.State = GameState.Shooting;
                      Point activePlayerNewPos = new Point(
                         gameData.ActivePlayer.Sprite.Pos.X, 
                         gameData.InactivePlayer.BoardBounds.Top - gameData.ActivePlayer.BoardBounds.Top + gameData.ActivePlayer.Sprite.Pos.Y);
                      Point inactivePlayerNewPos = new Point(
                         gameData.InactivePlayer.Sprite.Pos.X, 
                         gameData.ActivePlayer.BoardBounds.Top - gameData.InactivePlayer.BoardBounds.Top + gameData.InactivePlayer.Sprite.Pos.Y);

                      (gameData.ActivePlayer.BoardBounds, gameData.InactivePlayer.BoardBounds) = 
                         (gameData.InactivePlayer.BoardBounds, gameData.ActivePlayer.BoardBounds);
                      gameData.ActivePlayer.Sprite.Pos = activePlayerNewPos;
                      gameData.InactivePlayer.Sprite.Pos = inactivePlayerNewPos;
                      gameData.ActivePlayer.Sprite.SetSpriteToSelectedTileGreen();
                      gameData.InactivePlayer.Sprite.SetSpriteToSelectedTileGreen();
                      BaseDraw.CenterCamera(gameData.ActivePlayer);
                      BaseDraw.CenterCamera(gameData.InactivePlayer);

                      return;
                   }
                }
                          
                if (activeKeys[dialogRandomize].isActive && gameData.Input.Keyboard.KeyboardState
                       .Any(x => x.Identifier.Key == dialogRandomize.Key && 
                                 x.Values.Contains(Input.BtnState.Pressed) && 
                                 ! x.Values.Contains(Input.BtnState.Echo)))
                {
                   string[,] refreshedBoard = TileFunctions.GetRndSeaTiles(
                      gameData.ActivePlayer.BoardBounds.Width, 
                      gameData.ActivePlayer.BoardBounds.Height);
                   gameData.ActivePlayer.BoardBounds.SetBoardByBounds(gameData.Board2D, refreshedBoard); 
                   gameData.ActivePlayer.Ships.Clear();
                   gameData.ActivePlayer.ShipBeingPlacedIdx = -1;
                   List<Rectangle> newBoard = ShipPlacement.PlaceShips(
                      gameData.ShipSizes,
                      gameData.ActivePlayer.BoardBounds,
                      gameData.AllowedPlacementType);
                   gameData.ActivePlayer.Ships = newBoard;
                   foreach (var ship in newBoard)
                   {
                      List<Point> points = ship.ToPoints();
                      foreach (var p in points)
                      {
                         gameData.Board2D.Set(p, TextureValue.IntactShip);
                      }
                   }
                   shipPlacementStatus = GetShipPlacementStatus(gameData);
                }

                if (activeKeys[dialogClear].isActive && gameData.Input.Keyboard.KeyboardState
                       .Any(x => x.Identifier.Key == dialogClear.Key && 
                                 x.Values.Contains(Input.BtnState.Pressed) && 
                                 ! x.Values.Contains(Input.BtnState.Echo))
                    )
                {
                   gameData.ActivePlayer.Ships.Clear();
                   gameData.ActivePlayer.ShipBeingPlacedIdx = 0;
                   string[,] refreshedBoard = TileFunctions.GetRndSeaTiles(
                      gameData.ActivePlayer.BoardBounds.Width, 
                      gameData.ActivePlayer.BoardBounds.Height);
                   gameData.ActivePlayer.BoardBounds.SetBoardByBounds(gameData.Board2D, refreshedBoard); 
                   shipPlacementStatus = GetShipPlacementStatus(gameData);
                }

                if (shipPlacementStatus.isPlaceable && activeKeys[dialogAction].isActive && 
                    gameData.Input.Keyboard.KeyboardState
                       .Any(x => x.Identifier.Key == dialogAction.Key && 
                                 x.Values.Contains(Input.BtnState.Pressed) && 
                                 ! x.Values.Contains(Input.BtnState.Echo))
                    )
                {
                   if (shipPlacementStatus.modelPoints == null || shipPlacementStatus.hitboxRect == null) { throw new Exception("Unexpected!");}
                   PlaceShip(shipPlacementStatus.modelPoints, (Rectangle) shipPlacementStatus.hitboxRect, 
                      gameData.Board2D, gameData.ActivePlayer, 
                      gameData.ShipSizes.Count, gameData.ActivePlayer.Sprite);
                }

                if (activeKeys[dialogRot].isActive && gameData.Input.Keyboard.KeyboardState
                       .Any(x => x.Identifier.Key == dialogRot.Key && 
                                 x.Values.Contains(Input.BtnState.Pressed) && 
                                 ! x.Values.Contains(Input.BtnState.Echo))
                    )
                {
                   gameData.ActivePlayer.IsHorizontalPlacement = !gameData.ActivePlayer.IsHorizontalPlacement;
                }
                break;
             
             
             case GameState.Shooting:
                string selectedOppTileValue = gameData.Board2D.Get(gameData.ActivePlayer.Sprite.Pos);
                if (gameData.Input.Keyboard.KeyboardState
                       .Any(x => x.Identifier.Key == Input.KeyboardInput.KeyboardIdentifierList.KeyZ.Key && 
                                 x.Values.Contains(Input.BtnState.Pressed) && 
                                 ! x.Values.Contains(Input.BtnState.Echo)) && 
                    !(TextureValue.HitShip == selectedOppTileValue || 
                      TextureValue.HitWater == selectedOppTileValue)
                    )
                {
                   if (TileData.SeaTiles.Contains(selectedOppTileValue))
                   { 
                      gameData.ActivePlayer.ShootingHistory.Add(
                         new ShootingHistoryItem(
                            gameData.ActivePlayer.Sprite.Pos,
                            selectedOppTileValue,
                            TextureValue.HitWater, 
                            null)
                      );
                      gameData.Board2D.Set(gameData.ActivePlayer.Sprite.Pos, TextureValue.HitWater);
                      (gameData.ActivePlayer, gameData.InactivePlayer) = (gameData.InactivePlayer, gameData.ActivePlayer);
                      
                      return;
                   }
                   if (TextureValue.IntactShip == selectedOppTileValue)
                   {
                      Rectangle rect = gameData.InactivePlayer.Ships.FirstOrDefault(x => x.Contains(gameData.ActivePlayer.Sprite.Pos));
                      List<Point> rectAsPoints = rect.ToPoints();
                      bool isShipDestroyed = rectAsPoints
                         .Where(p => p != gameData.ActivePlayer.Sprite.Pos)
                         .All(p => gameData.Board2D.Get(p) == TextureValue.HitShip);

                      if (isShipDestroyed)
                      {
                         List<Point> hitboxRectAsPoints = rect.ToHitboxPoints(gameData.AllowedPlacementType);
                         List<ShootingHistoryItem> changes = GetAreaOfSunkenShipRevealChanges(hitboxRectAsPoints, gameData.Board2D, gameData.ActivePlayer.BoardBounds);
                         foreach (var change in changes)
                         {
                            gameData.Board2D.Set(change.Point, change.CurrValue);
                         }
                         gameData.ActivePlayer.ShootingHistory.Add(new ShootingHistoryItem(
                            gameData.ActivePlayer.Sprite.Pos,
                            selectedOppTileValue,
                            TextureValue.HitShip, 
                            changes));

                         if (IsOver(gameData, out string winner))
                         {
                            gameData.State = GameState.GameOver;
                         }
                      }
                      else
                      {
                         gameData.Board2D.Set(gameData.ActivePlayer.Sprite.Pos, TextureValue.HitShip);
                         gameData.ActivePlayer.ShootingHistory.Add(
                            new ShootingHistoryItem(
                               gameData.ActivePlayer.Sprite.Pos,
                               selectedOppTileValue,
                               TextureValue.HitShip, 
                               null)
                         );
                      }
                   }
                   else {
                      throw new Exception("Unexpected!");
                   }
                }

                if (gameData.Input.Keyboard.KeyboardState
                      .Any(x => x.Identifier.Key == Input.KeyboardInput.KeyboardIdentifierList.KeyR.Key && 
                                x.Values.Contains(Input.BtnState.Pressed) && 
                                ! x.Values.Contains(Input.BtnState.Echo))
                    && gameData.ActivePlayer.ShootingHistory.Count != 0)
                {
                   ShootingHistoryItem historyItem = gameData.ActivePlayer.ShootingHistory.Last();
                   gameData.ActivePlayer.ShootingHistory.Remove(historyItem);
                   if (historyItem.AllChangedPoints != null)
                   {
                      foreach (var changedPoint in historyItem.AllChangedPoints) 
                      { 
                         gameData.Board2D.Set(changedPoint.Point, changedPoint.PrevValue); 
                      }
                   }
                   else
                   {
                      gameData.Board2D.Set(historyItem.Point, historyItem.PrevValue);
                   }
                }
                break;
             default:
                throw new Exception("Unexpected!");
          }
       }


       public static ShipPlacementStatus GetShipPlacementStatus(GameData gameData)
       {
          if (gameData.ActivePlayer.ShipBeingPlacedIdx != -1 && gameData.State == GameState.Placement)
          {
             Point shipBeingPlaced = gameData.ShipSizes[gameData.ActivePlayer.ShipBeingPlacedIdx];
             Rectangle hitboxRect = gameData.ActivePlayer.IsHorizontalPlacement ? 
                new Rectangle(gameData.ActivePlayer.Sprite.Pos.X, gameData.ActivePlayer.Sprite.Pos.Y, shipBeingPlaced.Y, shipBeingPlaced.X) :
                new Rectangle(gameData.ActivePlayer.Sprite.Pos.X, gameData.ActivePlayer.Sprite.Pos.Y, shipBeingPlaced.X, shipBeingPlaced.Y);
             if (CanPlaceShip(out List<Point> modelPoints, hitboxRect, gameData))
             {
                return new ShipPlacementStatus(false, true, hitboxRect, modelPoints);
             }
             return new ShipPlacementStatus(false, false, hitboxRect, null);
          }
          return new ShipPlacementStatus(true, false, null, null);
       }

       public struct ShipPlacementStatus
       {
          public readonly bool isStartable;
          public readonly bool isPlaceable;
          public readonly Rectangle? hitboxRect;
          public readonly List<Point>? modelPoints;

          public ShipPlacementStatus(bool isStartable, bool isPlaceable, Rectangle? hitboxRect, List<Point>? modelPoints)
          {
             this.isStartable = isStartable;
             this.isPlaceable = isPlaceable;
             this.hitboxRect = hitboxRect;
             this.modelPoints = modelPoints;
          }
       }

       public static bool IsOver(GameData gameData, out string winner)
       {
          switch (gameData.State)
          {
             case GameState.Placement:
                winner = "";
                return false;
             case GameState.Shooting:
                bool isOver = true;
                foreach (var ship in gameData.InactivePlayer.Ships)
                {
                   if (ship.ToPoints().Any(point => gameData.Board2D.Get(point) != TextureValue.HitShip))
                   {
                      isOver = false;
                      break;
                   }
                }
                winner = isOver ? gameData.ActivePlayer.Name : "";
                return isOver;
             case GameState.GameOver:
                winner = gameData.ActivePlayer.Name;
                return true;
             default:
                throw new Exception("unexpected!");
          }
       }

       private static List<ShootingHistoryItem> GetAreaOfSunkenShipRevealChanges(List<Point> hitboxRectAsPoints, string[,] board, Rectangle boardBounds)
       {
          List<ShootingHistoryItem> shootingHistoryItems = new List<ShootingHistoryItem>();
          foreach (var rectPoint in hitboxRectAsPoints)
          {
             if (!boardBounds.Contains(rectPoint))
             {
                continue;
             }
             string selectedOppTileValue = board.Get(rectPoint);
             if (TileData.SeaTiles.Contains(selectedOppTileValue))
             {
                if (selectedOppTileValue == TextureValue.HitWater)
                {
                   continue;
                }
                shootingHistoryItems.Add(
                   new ShootingHistoryItem(
                      rectPoint, 
                      selectedOppTileValue, 
                      TextureValue.HitWater, 
                      null));
             } 
             else if (TextureValue.IntactShip == selectedOppTileValue)
             {
                shootingHistoryItems.Add(
                   new ShootingHistoryItem(
                      rectPoint, 
                      TextureValue.IntactShip, 
                      TextureValue.HitShip,
                      null));
             }
          }

          return shootingHistoryItems;
       }
       
       public static void WorldToScreen(
          float fWorldX, float fWorldY, 
          Player player, 
          out int nScreenX, out int nScreenY)
       {
          WorldToScreen(fWorldX, fWorldY,
             player.fCameraScaleX, player.fCameraScaleY,
             player.fCameraPixelPosX, player.fCameraPixelPosY,
             out nScreenX, out nScreenY);
       }

       public static void WorldToScreen(
          float fWorldX, float fWorldY, 
          float fScaleX, float fScaleY, 
          float fOffsetX, float fOffsetY, 
          out int nScreenX, out int nScreenY)
       {
          nScreenX = (int) Math.Floor((fWorldX - fOffsetX) * fScaleX);
          nScreenY = (int) Math.Floor((fWorldY - fOffsetY) * fScaleY);
       }
       
       public static void ScreenToWorld(
          int nScreenX, int nScreenY, 
          Player player, 
          out float fWorldX, out float fWorldY)
       {
          ScreenToWorld(nScreenX, nScreenY,
             player.fCameraScaleX, player.fCameraScaleY,
             player.fCameraPixelPosX, player.fCameraPixelPosY,
             out fWorldX, out fWorldY);
       }
          
       public static void ScreenToWorld(
          int nScreenX, int nScreenY, 
          float fScaleX, float fScaleY, 
          float fOffsetX, float fOffsetY, 
          out float fWorldX, out float fWorldY)
       {
          fWorldX = (nScreenX / fScaleX) + fOffsetX;
          fWorldY = (nScreenY / fScaleY) + fOffsetY;
       }


       private static void HandlePlayerMovement(out bool posChanged, Player player, string[,] board, Input input)
       {
          Point playerPosBefore = new Point(player.Sprite.Pos.X, player.Sprite.Pos.Y);
          Rectangle bounds = new Rectangle(0,0, board.GetWidth(), board.GetHeight());
          if (input.Keyboard.KeyboardState
                 .Any(x => x.Identifier.Key == Input.KeyboardInput.KeyboardIdentifierList.KeyA.Key && 
                           x.Values.Contains(Input.BtnState.Pressed) 
                           || 
                           x.Identifier.Key == Input.KeyboardInput.KeyboardIdentifierList.ArrowLeft.Key && 
                           x.Values.Contains(Input.BtnState.Pressed)
                           )
          )
          {
             if (player.Sprite.Pos.X > bounds.Left && player.fKeyboardMoveTimeout < 0)
             {
                Point playerPos = new Point(player.Sprite.Pos.X - 1, player.Sprite.Pos.Y);
                if (! TileData.Tiles[board.Get(playerPos)].HasCollision)
                {
                   player.Sprite.Pos = playerPos;
                   player.fCameraPixelPosX -= TileData.Width;
                }
             }
          }

          if (input.Keyboard.KeyboardState
                .Any(x => x.Identifier.Key == Input.KeyboardInput.KeyboardIdentifierList.KeyS.Key && 
                          x.Values.Contains(Input.BtnState.Pressed) 
                          || 
                          x.Identifier.Key == Input.KeyboardInput.KeyboardIdentifierList.ArrowDown.Key && 
                          x.Values.Contains(Input.BtnState.Pressed)
                )
             )
          {
             if (player.Sprite.Pos.Y < bounds.Bottom - 1 && player.fKeyboardMoveTimeout < 0)
             {
                Point playerPos = new Point(player.Sprite.Pos.X, player.Sprite.Pos.Y + 1);
                if (! TileData.Tiles[board.Get(playerPos)].HasCollision)
                {
                   player.Sprite.Pos = playerPos;
                   player.fCameraPixelPosY += TileData.Height;
                }
             }
          }

          if (input.Keyboard.KeyboardState
                .Any(x => x.Identifier.Key == Input.KeyboardInput.KeyboardIdentifierList.KeyD.Key && 
                          x.Values.Contains(Input.BtnState.Pressed) 
                          || 
                          x.Identifier.Key == Input.KeyboardInput.KeyboardIdentifierList.ArrowRight.Key && 
                          x.Values.Contains(Input.BtnState.Pressed)
                )
             )
          {
             if (player.Sprite.Pos.X < bounds.Right - 1 && player.fKeyboardMoveTimeout < 0)
             {
                Point playerPos = new Point(player.Sprite.Pos.X + 1, player.Sprite.Pos.Y);
                if (! TileData.Tiles[board.Get(playerPos)].HasCollision)
                {
                   player.Sprite.Pos = playerPos;
                   player.fCameraPixelPosX += TileData.Width;
                }
             }
          }

          if (input.Keyboard.KeyboardState
                .Any(x => x.Identifier.Key == Input.KeyboardInput.KeyboardIdentifierList.KeyW.Key && 
                          x.Values.Contains(Input.BtnState.Pressed) 
                          || 
                          x.Identifier.Key == Input.KeyboardInput.KeyboardIdentifierList.ArrowUp.Key && 
                          x.Values.Contains(Input.BtnState.Pressed)
                )
             )
          {
             if (player.Sprite.Pos.Y > bounds.Top && player.fKeyboardMoveTimeout < 0)
             {
                Point playerPos = new Point(player.Sprite.Pos.X, player.Sprite.Pos.Y - 1);
                if (! TileData.Tiles[board.Get(playerPos)].HasCollision)
                {
                   player.Sprite.Pos = playerPos;
                   player.fCameraPixelPosY -= TileData.Height;
                }
             }
          }

          posChanged = playerPosBefore != player.Sprite.Pos;
       }

       private static bool CanPlaceShip(out List<Point> modelPoints, Rectangle rect, GameData gameData)
       {
          CanPlaceShipData canPlaceShipData = CanPlaceShipCommon(rect, gameData);
          modelPoints = new List<Point>();
          for (int i = 0; i < canPlaceShipData.hitboxOutOfBounds.Count; i++)
          {
             if (canPlaceShipData.hitboxOutOfBounds[i]) { continue; }
             if (canPlaceShipData.isPointInModelBox[i])
             {
                modelPoints.Add(canPlaceShipData.Points[i]);
             }
          }
          return canPlaceShipData.shipPointIsPlaceable.All(x => x);
       }
       
       public static bool CanPlaceShipHover(Rectangle rect, GameData gameData, out List<Player.HoverElement> hoverElements)
       {
          hoverElements = new List<Player.HoverElement>();
          CanPlaceShipData canPlaceShipData = CanPlaceShipCommon(rect, gameData);
          for (var i = 0; i < canPlaceShipData.hitboxOutOfBounds.Count; i++)
          {
             if (canPlaceShipData.hitboxOutOfBounds[i]) { continue; }
             string tile;
             if (canPlaceShipData.shipPointIsPlaceable[i])
             {
                tile = canPlaceShipData.isPointInModelBox[i] ? TextureValue.IntactShip : TextureValue.ImpossibleShipHitbox;
             }
             else
             {
                tile = TextureValue.ImpossibleShip;
             }
             hoverElements.Add(new Player.HoverElement(canPlaceShipData.Points[i], tile));
          }

          bool canBePlacedEntirely = canPlaceShipData.shipPointIsPlaceable.All(x => x);
          return canBePlacedEntirely;
       }
       
       
       private static CanPlaceShipData CanPlaceShipCommon(Rectangle rect, GameData gameData)
       {
          CanPlaceShipData canPlaceShipData = new CanPlaceShipData();
          List<Point> hitboxRectAsPoints = rect.ToHitboxPoints(gameData.AllowedPlacementType);
          foreach (var boundBoxPoint in hitboxRectAsPoints)
          {
             bool isPointInModelBox = rect.Contains(boundBoxPoint);
             bool isPointOutOfBounds = !gameData.ActivePlayer.BoardBounds.Contains(boundBoxPoint);
             bool doesHitBoxIntersect = gameData.ActivePlayer.Ships.Any(ship => ship.Contains(boundBoxPoint));
             bool isPlaceable = !(isPointInModelBox && isPointOutOfBounds || doesHitBoxIntersect);
             bool hitboxOutOfBounds = isPointOutOfBounds && !isPointInModelBox;
             canPlaceShipData.Add(hitboxOutOfBounds, isPlaceable, isPointInModelBox, boundBoxPoint);
          }
          return canPlaceShipData;
       }
       
       private class CanPlaceShipData
       {
          public readonly List<bool> hitboxOutOfBounds;
          public readonly List<bool> shipPointIsPlaceable;
          public readonly List<bool> isPointInModelBox;
          public readonly List<Point> Points;
          
          public CanPlaceShipData()
          {
             this.hitboxOutOfBounds = new List<bool>();
             this.shipPointIsPlaceable = new List<bool>();
             this.isPointInModelBox = new List<bool>();
             this.Points = new List<Point>();
          }

          public void Add(bool _hitboxOutOfBounds, bool _shipPointIsPlaceable, bool _isPointInModelBox, Point _point)
          {
             this.hitboxOutOfBounds.Add(_hitboxOutOfBounds);
             this.shipPointIsPlaceable.Add(_shipPointIsPlaceable);
             this.isPointInModelBox.Add(_isPointInModelBox);
             this.Points.Add(_point);
          }
       }



       private static void PlaceShip(List<Point> shipTiles, Rectangle rect, string[,] board, Player player, int maxShipCount, Sprite.PlayerSprite playerSprite)
       {
          foreach (var shipTile in shipTiles)
          {
             board.Set(shipTile, TextureValue.IntactShip);
          }
          playerSprite.SetSpriteToSelectedTileRed();
          player.Ships.Add(rect);
          player.ShipBeingPlacedIdx = player.ShipBeingPlacedIdx != maxShipCount - 1 ? 
             player.ShipBeingPlacedIdx + 1 : -1;
          
       }

       private static void HandleKeyboardPanning(double deltaTime, Player player, Input input)
       {
          if (input.Keyboard.KeyboardState
                .Any(x => x.Identifier.Key == Input.KeyboardInput.KeyboardIdentifierList.KeyJ.Key && 
                          x.Values.Contains(Input.BtnState.Pressed) 
                )
             )
          {
             player.fCameraPixelPosX -= (float) (50 * deltaTime);
          }

          if (input.Keyboard.KeyboardState
             .Any(x => x.Identifier.Key == Input.KeyboardInput.KeyboardIdentifierList.KeyK.Key && 
                       x.Values.Contains(Input.BtnState.Pressed) 
             )
          )
          {
             player.fCameraPixelPosY += (float) (50 * deltaTime);
          }

          if (input.Keyboard.KeyboardState
             .Any(x => x.Identifier.Key == Input.KeyboardInput.KeyboardIdentifierList.KeyL.Key && 
                       x.Values.Contains(Input.BtnState.Pressed) 
             )
          )
          {
             player.fCameraPixelPosX += (float) (50 * deltaTime);
          }

          if (input.Keyboard.KeyboardState
             .Any(x => x.Identifier.Key == Input.KeyboardInput.KeyboardIdentifierList.KeyI.Key && 
                       x.Values.Contains(Input.BtnState.Pressed) 
             )
          )
          {
             player.fCameraPixelPosY -= (float) (50 * deltaTime);
          }
       }

       private static void HandleMousePanning(Player player, Input input)
       {
          var mousePos = new Point(input.Mouse.X, input.Mouse.Y);
          if (!input.Mouse.LeftButton.Contains(Input.BtnState.Pressed))
          {
             player.pMouseStartPixelPan = mousePos;
             return;
          }
          
          player.fCameraPixelPosX -= (input.Mouse.X - player.pMouseStartPixelPan.X) / player.fCameraScaleX;
          player.fCameraPixelPosY -= (input.Mouse.Y - player.pMouseStartPixelPan.Y) / player.fCameraScaleY;

          player.pMouseStartPixelPan = mousePos;
       }

       private static void HandleZooming(Player player, Input input)
       {
          bool zoomNegative = input.Keyboard.KeyboardState
             .Any(x => x.Identifier.Key == Input.KeyboardInput.KeyboardIdentifierList.Period.Key && 
                       x.Values.Contains(Input.BtnState.Pressed));
          bool zoomPositive = input.Keyboard.KeyboardState
             .Any(x => x.Identifier.Key == Input.KeyboardInput.KeyboardIdentifierList.Comma.Key && 
                       x.Values.Contains(Input.BtnState.Pressed));
          if (! zoomNegative && ! zoomPositive)
          {
             return;
          }
          var mousePos = new Point(input.Mouse.X, input.Mouse.Y);
          float fMouseWorldX_BeforeZoom, fMouseWorldY_BeforeZoom;
          ScreenToWorld(
             mousePos.X,
             mousePos.Y,
             player,
             out fMouseWorldX_BeforeZoom,
             out fMouseWorldY_BeforeZoom);

          if (zoomPositive)
          {
             player.fCameraScaleX *= 1.001f;
             player.fCameraScaleY *= 1.001f;
          }

          if (zoomNegative)
          {
             player.fCameraScaleX *= 0.999f;
             player.fCameraScaleY *= 0.999f;
          }

          float fMouseWorldX_AfterZoom, fMouseWorldY_AfterZoom;
          ScreenToWorld(
             mousePos.X,
             mousePos.Y,
             player,
             out fMouseWorldX_AfterZoom,
             out fMouseWorldY_AfterZoom);

          player.fCameraPixelPosX += fMouseWorldX_BeforeZoom - fMouseWorldX_AfterZoom;
          player.fCameraPixelPosY += fMouseWorldY_BeforeZoom - fMouseWorldY_AfterZoom;
       }

       private static void HandleMouseSelection(Player player, Input input)
       {
          if (!input.Mouse.LeftButton.Contains(Input.BtnState.Pressed)) return;
          var mousePos = new Point(input.Mouse.X, input.Mouse.Y);
          float fMouseWorldX, fMouseWorldY;
          ScreenToWorld(
             mousePos.X,
             mousePos.Y,
             player,
             out fMouseWorldX,
             out fMouseWorldY);
          player.fMouseSelectedTileX = (float) Math.Floor(fMouseWorldX);
          player.fMouseSelectedTileY = (float) Math.Floor(fMouseWorldY);
       }
    }
}