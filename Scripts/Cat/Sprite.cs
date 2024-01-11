using System.IO;
using System.Collections.Generic;
using static SDL2.SDL;
using static Clangen.Utility;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using System.Linq;
using System;
using Newtonsoft.Json.Serialization;
using System.Xml.Linq;

namespace Clangen.Cats
{
    public class SpriteLoader
    {
        private Dictionary<string, Spritesheet> Spritesheets;
        public SortedDictionary<string, SheetData> Manifest;

        public SpriteLoader()
        {
            Spritesheets = new Dictionary<string, Spritesheet>();
        }

        internal void Load()
        {
            var Defaults = new SortedDictionary<string, SheetData>();
            Manifest = JsonConvert.DeserializeObject<SortedDictionary<string, SheetData>>(File.ReadAllText("Common\\Sprites\\Manifest.json"));

            string Path;
            
            foreach ((string ID, SheetData Data) in Manifest.Select(i => (File: i.Key, Data: i.Value)))
            {
                if (ID.StartsWith("@"))
                {
                    Defaults[ID] = Data;
                }
                else if (File.Exists(Path = $"Common\\Sprites\\{ID}"))
                {
                    Spritesheets[Data.ID ?? ID.Remove(ID.LastIndexOf('.'))] = new Spritesheet(Path);
                }
            }
        }

        private void MakeGroup(string Spritesheet, string Name, int Row, int Column, int RowCapacity = 3, int ColumnCapacity = 7)
        {
            Spritesheets[Spritesheet].MakeGroup(Name, Row, Column, RowCapacity, ColumnCapacity);
        }

        public Spritesheet this[string Identifier]
        {
            get => Spritesheets[Identifier];
        }
    }

    public class SheetData
    {
        public string ID { get; set; }
        public string Base { get; set; }
        public string BaseCollection { get; set; }
        public int SpriteSize { get; set; }
        public string[] Map { get; set; }
    }

    public class CollectionData
    {
        public string ID { get; set; }
        public (int Row, int Column) Layout { get; set; }
        public string[] Map { get; set; }
    }


    public class Spritesheet
    {
        private Dictionary<string, Image> Sprites;
        public short SpriteSize;
        public readonly Image TextureAtlas;

        public Spritesheet(string Path, short Size = 50) 
        {
            Sprites = new Dictionary<string, Image>();
            SpriteSize = Size;
            TextureAtlas = new Image(Path);
        }

        public void MakeGroup(string Name, int Row, int Column, int RowCapacity = 3, int ColumnCapacity = 7)
        {
            int OffsetX = Row * RowCapacity * SpriteSize;
            int OffsetY = Column * ColumnCapacity * SpriteSize;

            Image Sprite;
            SDL_Rect ClippingRect = new SDL_Rect() { x = 0, y = 0, w = SpriteSize, h = SpriteSize};

            for (int i = 0; i < RowCapacity * ColumnCapacity; i++)
            {
                ClippingRect.x = OffsetX + i / ColumnCapacity * SpriteSize;
                ClippingRect.y = OffsetY + i % ColumnCapacity * SpriteSize;

                Sprite = new Image(ClippingRect.w, ClippingRect.h);
                Context.RenderOnto(TextureAtlas, Sprite, ClippingRect, null);

                Sprites[$"{Name}{i}"] = Sprite;
            }
        }

        public Image this[string Identifier]
        {
            get => Sprites[Identifier];
        }
    }
}
