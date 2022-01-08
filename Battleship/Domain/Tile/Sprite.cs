using System.Collections.Generic;
using System.Linq;
using System.Text;
using RogueSharp;
using static Domain.Tile.TileData;

namespace Domain.Tile
{
    public class Sprite
    {
        public bool HasCollision { get; set; }
        public string Texture { get; set; } = null!;
        public string Type { get; set; } = null!;
        public Point Pos { get; set; }
        
        public Sprite()
        {
            // Serialization requirement
        }
        
        
        private static class SpriteTextureValue
        {
            public const string SelectedTileRed =       "pVae";
            public const string SelectedTileGreen =     "ZLzo";
        }
        
        public sealed class PlayerSprite : Sprite
        {
            public PlayerSprite()
            {
                // Serialization requirement
            }
            
            public PlayerSprite(Point pos)
            {
                HasCollision = false;
                Texture = SelectedTileGreen.Value;
                Type = nameof(PlayerSprite);
                Pos = pos;
            }

            public void SetSpriteToSelectedTileRed() { Texture = SelectedTileRed.Value; }
            public void SetSpriteToSelectedTileGreen() { Texture = SelectedTileGreen.Value; }
            
            public static readonly TileData.TileProperty SelectedTileRed = new TileData.TileProperty(SpriteTextureValue.SelectedTileRed, new StringBuilder()
                    .Append("~~~~")
                    .Append("~@@~")
                    .Append("~@@~")
                    .Append("~~~~"),
                new TileColor[]
                {
                    TileData.C.DC, TileData.C.DC, TileData.C.DC, TileData.C.DC,
                    TileData.C.DC, TileData.C.DR, TileData.C.DR, TileData.C.DC,
                    TileData.C.DC, TileData.C.DR, TileData.C.DR, TileData.C.DC,
                    TileData.C.DC, TileData.C.DC, TileData.C.DC, TileData.C.DC
                }, false
                );
            
            public static readonly TileData.TileProperty SelectedTileGreen = new TileData.TileProperty(SpriteTextureValue.SelectedTileGreen, new StringBuilder()
                    .Append("~~~~")
                    .Append("~@@~")
                    .Append("~@@~")
                    .Append("~~~~"),
                new TileColor[]
                {
                    TileData.C.DC, TileData.C.DC, TileData.C.DC, TileData.C.DC,
                    TileData.C.DC, TileData.C._G, TileData.C._G, TileData.C.DC,
                    TileData.C.DC, TileData.C._G, TileData.C._G, TileData.C.DC,
                    TileData.C.DC, TileData.C.DC, TileData.C.DC, TileData.C.DC
                }, false
                );
        }
        
    }
}