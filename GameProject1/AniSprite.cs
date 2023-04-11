using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameProject1;
using GameProject1.Tools;

namespace GameProject1
{
    public class AniSprite
    {
        /// <summary>
        /// Sprite's texture
        /// </summary>
        private Texture2D _texture;
        /// <summary>
        /// Position of Sprite
        /// </summary>
        public Vector2 Position;
        /// <summary>
        /// Scale of the Sprite's Rendering
        /// </summary>
        public float Scale = 1;
        /// <summary>
        /// The string for loading the static sprite's texture.
        /// </summary>
        public string TextureName;

        public double Timer = 0;

        public Directions Direction;

        public int Width;
        public int Height;

        private int frameWidth;
        private int frameHeight;

        public Color Color = Color.White;

        private int w, h;

        public AniSprite(string Texture, Vector2 position, float scale, Directions direction, int frameWidth, int frameHeight, int width, int height, Color color)
        {
            TextureName = Texture;
            Position = position;
            Scale = scale;
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
            this.Direction = direction;
            if(direction == Directions.Up)
            {
                h++;
                
            }
            Color = color;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Loads the Sprite's sprite texture
        /// </summary>
        /// <param name="content">The content manager</param>
        public void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>(TextureName);

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Timer += gameTime.ElapsedGameTime.TotalSeconds;
            if(Timer >= 1.0)
            {
                Timer = 0;
                if(w < frameWidth-1)
                {
                    w++;
                }
                else if(h < frameHeight-1)
                {
                    w = 0;
                    h++;
                }
                else
                {
                    w = 0;
                    h = 0;
                }
            }
            SpriteEffects effect = SpriteEffects.None;
            if (Direction == Directions.Up) effect = SpriteEffects.FlipVertically;
            float rotate = 0f;
            if (Direction == Directions.Left) rotate = 0.75f;
            else if (Direction == Directions.Right) rotate = 0.25f;
            spriteBatch.Draw(_texture, Position, new Rectangle(Width*w,Height*h,Width,Height), this.Color, rotate, Vector2.Zero, Scale, effect, 0);
        }
    }
}
