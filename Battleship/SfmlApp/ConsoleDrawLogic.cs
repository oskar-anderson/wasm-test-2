using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using Domain;
using Domain.Model;
using Domain.Tile;
using Game;
using SFML;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using Color = SFML.Graphics.Color;
using Font = SFML.Graphics.Font;
using Image = SFML.Graphics.Image;
using Point = RogueSharp.Point;

namespace SfmlApp
{
   public static class ConsoleDrawLogic
   {
      private static readonly Point BoardOffset = new Point(0, 5);
      private static readonly Font Font = new Font(AppDomain.CurrentDomain.BaseDirectory + "/media/Font/PressStart2P.ttf");

      /// <summary>
      /// This is called when the game should draw itself.
      /// </summary>
      /// <param name="deltaTime">Provides a snapshot of timing values.</param>
      /// <param name="gameData">Game data</param>
      public static void Draw(double deltaTime, GameData gameData, RenderWindow window)
      {
         window.Clear();
         BaseDraw.Get_UI(gameData);
         BaseDraw.Draw(deltaTime, gameData);
         window.SetTitle("BattleShip");
         

         TileData.CharInfo[,] map = new TileData.CharInfo[40, 40];
         BaseDraw.GetDrawArea(gameData, ref map);
         for (int y = 0; y < map.GetHeight(); y++)
         {
            for (int x = 0; x < map.GetWidth(); x++)
            {
               TileData.CharInfo tile = map.Get(new RogueSharp.Point(x, y));
               var text = new Text(
                  tile.GetGlyphString(), 
                  Font)
               {
                  FillColor = new Color(
                     tile.Color.RgbR, 
                     tile.Color.RgbG, 
                     tile.Color.RgbB
                     ), 
                  Position = new Vector2f(
                     (x + BoardOffset.X) * ConsoleBattle.FontW, 
                     (y + BoardOffset.Y) * ConsoleBattle.FontH), 
                  CharacterSize = 8
               };
               window.Draw(text);
            }
         }
         // End draw transformed elements

          
          
          
         // Draw interface elements that do not scale or transform
         float fMouseScreenX, fMouseScreenY;
         UpdateLogic.ScreenToWorld(
            gameData.Input.Mouse.X, gameData.Input.Mouse.Y, gameData.ActivePlayer, 
            out fMouseScreenX, out fMouseScreenY);
         fMouseScreenX = (float) Math.Floor(fMouseScreenX);
         fMouseScreenY = (float) Math.Floor(fMouseScreenY);
         
         window.Draw(new RectangleShape(new Vector2f(ConsoleBattle.ScreenWidth - 20 * ConsoleBattle.FontW, BoardOffset.Y * ConsoleBattle.FontH)) { Position = new Vector2f(0, 0), FillColor = Color.Yellow});
         window.Draw(new Text(gameData.ActivePlayer.UI_Message, Font) { Position = new Vector2f(10 * ConsoleBattle.FontW, 4 * ConsoleBattle.FontH), FillColor = Color.Black, CharacterSize = 8});
         if (gameData.State == GameState.GameOver)
         {
            UpdateLogic.IsOver(gameData, out string winner);
            string gameWonMsg = $"Game over, {winner} won!";
            window.Draw(new Text(gameWonMsg, Font)
            {
               Position = new Vector2f((ConsoleBattle.ScreenWidth - 20 - gameWonMsg.Length) / 2, ConsoleBattle.ScreenHeight / 2), 
               FillColor = Color.Black,
               CharacterSize = 8
            });
         }
         window.Draw(new Text($"[ESC] MENU", Font)
         {
            Position = new Vector2f(2 * ConsoleBattle.FontW, 0), 
            FillColor = Color.Black,
            CharacterSize = 8
         });

         window.Draw(new RectangleShape(new Vector2f(20 * ConsoleBattle.FontW, ConsoleBattle.ScreenHeight))
         {
            Position = new Vector2f(ConsoleBattle.ScreenWidth - 20 * ConsoleBattle.FontW, 0), 
            FillColor = Color.Red
         });
         window.Draw(new Text($"offsetX:{Math.Round(gameData.ActivePlayer.fCameraPixelPosX, 4)}", Font)
         {
            Position = new Vector2f(ConsoleBattle.ScreenWidth - 18 * ConsoleBattle.FontW, 2 * ConsoleBattle.FontH), 
            FillColor = Color.Black,
            CharacterSize = 8
         });
         window.Draw(new Text($"offsetY:{Math.Round(gameData.ActivePlayer.fCameraPixelPosY, 4)}", Font)
         {
            Position = new Vector2f(ConsoleBattle.ScreenWidth - 18 * ConsoleBattle.FontW, 3 * ConsoleBattle.FontH), 
            FillColor = Color.Black, 
            CharacterSize = 8
         });
         window.Draw(new Text(
            $"Zoom:{Math.Round(gameData.ActivePlayer.fCameraScaleX, 3)}:" +
            $"{Math.Round(gameData.ActivePlayer.fCameraScaleY, 3)}", 
            Font)
         {
            Position = new Vector2f(ConsoleBattle.ScreenWidth - 18 * ConsoleBattle.FontW, 6 * ConsoleBattle.FontH), 
            FillColor = Color.Black, 
            CharacterSize = 8
         });
         window.Draw(new Text($"W Mouse: {fMouseScreenX}:{fMouseScreenY}", Font)
         {
            Position = new Vector2f(ConsoleBattle.ScreenWidth - 18 * ConsoleBattle.FontW, 7 * ConsoleBattle.FontH), 
            FillColor = Color.Black, 
            CharacterSize = 8
         });
         window.Draw(new Text($"S Mouse: {gameData.Input.Mouse.X}:{gameData.Input.Mouse.Y}", Font)
         {
            Position = new Vector2f(ConsoleBattle.ScreenWidth - 18 * ConsoleBattle.FontW, 8 * ConsoleBattle.FontH), 
            FillColor = Color.Black, 
            CharacterSize = 8
         });
         window.Draw(new Text($"P Tile Pos: {gameData.ActivePlayer.Sprite.Pos.X}:{gameData.ActivePlayer.Sprite.Pos.Y}", Font)
         {
            Position = new Vector2f(ConsoleBattle.ScreenWidth - 18 * ConsoleBattle.FontW, 9 * ConsoleBattle.FontH), 
            FillColor = Color.Black, 
            CharacterSize = 8
         });
         
         window.Draw(new Text($"Avg FPS: {Math.Round(gameData.FrameCount / gameData.ElapsedTime, 3)}", Font)
         {
            Position = new Vector2f(ConsoleBattle.ScreenWidth - 18 * ConsoleBattle.FontW, 10 * ConsoleBattle.FontH), 
            FillColor = Color.Black, 
            CharacterSize = 8
         });
         
         window.Draw(new Text($"Player: {gameData.ActivePlayer.Name}", Font)
         {
            Position = new Vector2f(ConsoleBattle.ScreenWidth - 18 * ConsoleBattle.FontW, 17 * ConsoleBattle.FontH), 
            FillColor = Color.Black, 
            CharacterSize = 8
         });
         window.Draw(new Text($"Phase: {gameData.State}", Font)
         {
            Position = new Vector2f(ConsoleBattle.ScreenWidth - 18 * ConsoleBattle.FontW, 18 * ConsoleBattle.FontH), 
            FillColor = Color.Black, 
            CharacterSize = 8
         });

         window.Draw(new Text($"Frame: {gameData.FrameCount}", Font)
         {
            Position = new Vector2f(ConsoleBattle.ScreenWidth - 18 * ConsoleBattle.FontW, 20 * ConsoleBattle.FontH), 
            FillColor = Color.Black, 
            CharacterSize = 8
         });
         
         for (int i = 0; i < gameData.ActivePlayer.UI_DialogOptions.Count; i++)
         {
            var dialogOption = gameData.ActivePlayer.UI_DialogOptions[i];
            window.Draw(new Text($"[{dialogOption.key}] {dialogOption.text}", Font)
            {
               Position = new Vector2f(ConsoleBattle.ScreenWidth - 18 * ConsoleBattle.FontW, (22 + i) * ConsoleBattle.FontH), 
               FillColor = Color.Black, 
               CharacterSize = 8
            });
         }
         
         window.Display();
      }
      
      
      public static string GetDrawAreaAsPicture(GameData gameData)
      {
         var fontSize = new Vector2i(8, 8);
         var window = new RenderWindow(new VideoMode(480, 360), "Battleships");
         window.SetFramerateLimit(0);
         TileData.CharInfo[,] map = new TileData.CharInfo[40, 40];

         var mapPool = new Text[map.GetHeight(), map.GetWidth()];
         for (int y = 0; y < map.GetHeight(); y++)
         {
            for (int x = 0; x < map.GetWidth(); x++)
            {
               mapPool[y,x] = new Text(" ", Font)
               {
                  CharacterSize = 8
               };
            } 
         }
         
         
         BaseDraw.GetDrawArea(gameData, ref map);
         for (int y = 0; y < map.GetHeight(); y++)
         {
            for (int x = 0; x < map.GetWidth(); x++)
            {
               TileData.CharInfo tile = map.Get(new RogueSharp.Point(x, y));
               Point screenCoord = new Point(
                  x * fontSize.X,
                  y * fontSize.Y);
               // var text = new Text(tile.GetGlyphString(), Font) { FillColor = new Color(tile.Color.RgbR, tile.Color.RgbG, tile.Color.RgbB), Position = new Vector2f(screenCoord.X, screenCoord.Y), CharacterSize = 8};
               var text = mapPool[y, x];
               text.DisplayedString = tile.GetGlyphString();
               text.FillColor = new Color(tile.Color.RgbR, tile.Color.RgbG, tile.Color.RgbB);
               text.Position = new Vector2f(screenCoord.X, screenCoord.Y);
               /*
               */
               window.Draw(text);
            }
         }
         
         
         var texture = new Texture(window.Size.X, window.Size.Y);
         texture.Update(window);
         var pixels = texture.CopyToImage().Pixels;
         int windowSizeX = (int) window.Size.X;
         int windowSizeY = (int) window.Size.Y;
         window.Close();
         
         var base64String = RgbaArrToBase64PngString(pixels, windowSizeX, windowSizeY);
         return base64String;
      }

      public static string RgbaArrToBase64PngString(byte[] pixels, int width ,int height)
      {
         var image = new SixLabors.ImageSharp.Image<Rgba32>(width, height);
         for (int y = 0; y < height; y++)
         {
            for (int x = 0; x < width; x++)
            {
               image[x, y] = new Rgba32(
                  pixels[(y * width + x) * 4 + 0],
                  pixels[(y * width + x) * 4 + 1],
                  pixels[(y * width + x) * 4 + 2],
                  pixels[(y * width + x) * 4 + 3]);
            }
         }
         
         using MemoryStream ms = new MemoryStream();
         image.Save(ms, new PngEncoder());
         byte[] imageBytes = ms.ToArray();
         var base64String = Convert.ToBase64String(imageBytes);
         return base64String;
      }

      public static List<byte> GetBoardAsImage(GameData gameData)
      {
         var window = new RenderWindow(new VideoMode(480, 360), "Battleships");
         window.SetVisible(false);
         TileData.CharInfo[,] map = new TileData.CharInfo[40, 40];
         BaseDraw.GetDrawArea(gameData, ref map);
         for (int y = 0; y < map.GetHeight(); y++)
         {
            for (int x = 0; x < map.GetWidth(); x++)
            {
               TileData.CharInfo tile = map.Get(new RogueSharp.Point(x, y));
               var text = new Text(
                  tile.GetGlyphString(), 
                  Font)
               {
                  FillColor = new Color(
                     tile.Color.RgbR, 
                     tile.Color.RgbG, 
                     tile.Color.RgbB
                  ), 
                  Position = new Vector2f(
                     (x + BoardOffset.X) * ConsoleBattle.FontW, 
                     (y + BoardOffset.Y) * ConsoleBattle.FontH), 
                  CharacterSize = 8
               };
               window.Draw(text);
            }
         }

         var texture = new Texture(window.Size.X, window.Size.Y);
         texture.Update(window);
         window.Close();
         var pixels = texture.CopyToImage().Pixels.ToList();
         return pixels;
      }
   }
}