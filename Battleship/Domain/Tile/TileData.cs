using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Domain.Tile
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class TileData
    {
        public static readonly string[] SeaTiles = {TextureValue.WaterV1, TextureValue.WaterV2};

        
        public static class C
        {
            public static TileColor DD = TileColor.Black;
            public static TileColor DB = TileColor.DarkBlue;
            public static TileColor DG = TileColor.DarkGreen;
            public static TileColor DC = TileColor.DarkCyan;
            public static TileColor DR = TileColor.DarkRed;
            public static TileColor DM = TileColor.DarkMagenta;
            public static TileColor DY = TileColor.DarkYellow;
            public static TileColor _S = TileColor.Gray;
            public static TileColor DS = TileColor.DarkGray;
            public static TileColor _B = TileColor.Blue;
            public static TileColor _G = TileColor.Green;
            public static TileColor _C = TileColor.Cyan;
            public static TileColor _R = TileColor.Red;
            public static TileColor _M = TileColor.Magenta;
            public static TileColor _Y = TileColor.Yellow;
            public static TileColor __ = TileColor.White;
        }

        public class TileColor
        {
            public static TileColor Black = new TileColor(0, 12, 12, 12);
            public static TileColor DarkBlue = new TileColor(1, 0, 55, 218);
            public static TileColor DarkGreen = new TileColor(2, 19, 161, 14);
            public static TileColor DarkCyan = new TileColor(3, 58, 150, 221);
            public static TileColor DarkRed = new TileColor(4, 197,15, 31);
            public static TileColor DarkMagenta = new TileColor(5, 136, 23, 152);
            public static TileColor DarkYellow = new TileColor(6, 193, 156, 0);
            public static TileColor Gray = new TileColor(7, 204, 204, 204);
            public static TileColor DarkGray = new TileColor(8,118, 118, 118);
            public static TileColor Blue = new TileColor(9, 59, 120, 255);
            public static TileColor Green = new TileColor(10, 22, 192, 12);
            public static TileColor Cyan = new TileColor(11, 97, 214, 214);
            public static TileColor Red = new TileColor(12, 231, 72, 86);
            public static TileColor Magenta = new TileColor(13, 180, 0, 158);
            public static TileColor Yellow = new TileColor(14, 249, 241, 165);
            public static TileColor White = new TileColor(15, 242, 242, 242);

            public int IntVal { get; set; }
            public byte RgbR { get; set; }
            public byte RgbG { get; set; }
            public byte RgbB { get; set; }
            
            public TileColor(int intVal, byte r, byte g, byte b)
            {
                IntVal = intVal;
                RgbR = r;
                RgbG = g;
                RgbB = b;
            }
        }

        public const int Width = 4;
        public const int Height = 4;


        public static readonly TileProperty VoidTile = new TileProperty(TextureValue.VoidTile, new StringBuilder()
                .Append("    ")
                .Append("    ")
                .Append("    ")
                .Append("    "),
            new TileColor[]
            {
                C.__, C.__, C.__, C.__,
                C.__, C.__, C.__, C.__,
                C.__, C.__, C.__, C.__,
                C.__, C.__, C.__, C.__
            }, true
            );

        public static readonly TileProperty EmptyTileV1 = new TileProperty(TextureValue.WaterV1, new StringBuilder()
                .Append("~~~~")
                .Append("~##~")
                .Append("~##~")
                .Append("~~~~"),
            new TileColor[]
            {
                C.DC, C.DC, C.DC, C.DC,
                C.DC, C.DM, C.DM, C.DC,
                C.DC, C.DM, C.DM, C.DC,
                C.DC, C.DC, C.DC, C.DC
            }, false
            );

        public static readonly TileProperty EmptyTileV2 = new TileProperty(TextureValue.WaterV2, new StringBuilder()
                .Append("^^^^")
                .Append("^##^")
                .Append("^##^")
                .Append("^^^^"),
            new TileColor[]
            {
                C.DC, C.DC, C.DC, C.DC,
                C.DC, C.DM, C.DM, C.DC,
                C.DC, C.DM, C.DM, C.DC,
                C.DC, C.DC, C.DC, C.DC
            }, false
            );

        public static readonly TileProperty ImpossibleShip = new TileProperty(TextureValue.ImpossibleShip, new StringBuilder()
                .Append("~~~~")
                .Append("~XX~")
                .Append("~XX~")
                .Append("~~~~"),
            new TileColor[]
            {
                C.DC, C.DC, C.DC, C.DC,
                C.DC, C.DR, C.DR, C.DC,
                C.DC, C.DR, C.DR, C.DC,
                C.DC, C.DC, C.DC, C.DC
            }, false
            );

        public static readonly TileProperty ImpossibleShipHitbox = new TileProperty(TextureValue.ImpossibleShipHitbox, new StringBuilder()
                .Append("~~~~")
                .Append("~XX~")
                .Append("~XX~")
                .Append("~~~~"),
            new TileColor[]
            {
                C.DC, C.DC, C.DC, C.DC,
                C.DC, C._Y, C._Y, C.DC,
                C.DC, C._Y, C._Y, C.DC,
                C.DC, C.DC, C.DC, C.DC
            }, false
            );

        public static readonly TileProperty HitWater = new TileProperty(TextureValue.HitWater, new StringBuilder()
                .Append("~~~~")
                .Append("~^^~")
                .Append("~^^~")
                .Append("~~~~"),
            new TileColor[]
            {
                C.DC, C.DC, C.DC, C.DC,
                C.DC, C._Y, C._Y, C.DC,
                C.DC, C._Y, C._Y, C.DC,
                C.DC, C.DC, C.DC, C.DC
            }, false
            );

        public static readonly TileProperty HitShip = new TileProperty(TextureValue.HitShip, new StringBuilder()
                .Append("*~~*")
                .Append("~**~")
                .Append("~**~")
                .Append("*~~*"),
            new TileColor[]
            {
                C.DC, C.DC, C.DC, C.DC,
                C.DC, C.DR, C.DR, C.DC,
                C.DC, C.DR, C.DR, C.DC,
                C.DC, C.DC, C.DC, C.DC
            }, false
        );
            
        public static readonly TileProperty IntactShip = new TileProperty(TextureValue.IntactShip, new StringBuilder()
                .Append("~~~~")
                .Append("~XX~")
                .Append("~XX~")
                .Append("~~~~"),
            new TileColor[]
            {
                C.DC, C.DC, C.DC, C.DC,
                C.DC, C._G, C._G, C.DC,
                C.DC, C._G, C._G, C.DC,
                C.DC, C.DC, C.DC, C.DC
            }, false
        );

        public static readonly Dictionary<string, TileProperty> Tiles = new Dictionary<string, TileProperty>()
        {
            { VoidTile.Value, VoidTile },
            { EmptyTileV1.Value, EmptyTileV1 },
            { EmptyTileV2.Value, EmptyTileV2 },
            { ImpossibleShip.Value, ImpossibleShip },
            { ImpossibleShipHitbox.Value, ImpossibleShipHitbox },
            { HitWater.Value, HitWater },
            { HitShip.Value, HitShip },
            { IntactShip.Value, IntactShip },
            { Sprite.PlayerSprite.SelectedTileGreen.Value, Sprite.PlayerSprite.SelectedTileGreen },
            { Sprite.PlayerSprite.SelectedTileRed.Value, Sprite.PlayerSprite.SelectedTileRed },
        };


        public class TileProperty
        {
            public readonly string Value;
            public readonly CharInfo[] CharInfoArray;
            public readonly bool HasCollision;

            public TileProperty(string value, StringBuilder sbTileSymbols, TileColor[] fgColors, bool hasCollision)
            {
                this.HasCollision = hasCollision;
                this.Value = value;
                this.CharInfoArray = new CharInfo[Height * Width];
                for (int i = 0; i < Height * Width; i++)
                {
                    CharInfoArray[i] = new CharInfo(sbTileSymbols[i], fgColors[i]);
                }
                if (CharInfoArray.Any(x => x == null)) { throw new Exception("Tile content is messed up!");}
                if (CharInfoArray.Length != Width * Height) { throw new Exception("Tile size is messed up!");}
            }
        }
        
        

        public class CharInfo
        {
            public char Glyph { get; set; }
            public TileColor Color { get; set; } = null!;
            
            public CharInfo()
            {
                // needed for deserialization
            }

            public CharInfo(char glyph, TileColor color)
            {
                this.Glyph = glyph;
                this.Color = color;
            }

            public char GetGlyphChar() => Glyph;
            public string GetGlyphString() => Glyph.ToString();
            public TileColor GetColor() => Color;

            public override string ToString() => GetGlyphString();
        }
    }
}