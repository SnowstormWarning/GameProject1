using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using SharpDX.Direct3D9;
using GameProject1.Tools;

namespace GameProject1
{
    public static class Ball
    {
        public static StaticSprite Sprite { get; } = new StaticSprite("Ball") {Position = new Vector2(24,24)};
        public static Vector2 Center => PosTool.VectorSubtraction(Sprite.Position,new Vector2(-8,-8));
        public static float Radius = 16;
        public static Vector2 Velocity = Vector2.Zero;
        public static BoundingCircle Bounds = new BoundingCircle(Center, 16);

        public static void LoadContent(ContentManager content)
        {
            Sprite.LoadContent(content);
        }

        public static void Update(GameTime gameTime)
        {
            Vector2 p = Center + Velocity;
            if (p.X-8 < 0)
            {
                p.X = 0;
               Velocity *= new Vector2(-1, 1);
            }
            else if(p.X+8 > Game1.Width)
            {
                p.X = Game1.Width;
                Velocity *= new Vector2(-1, 1);
            }
            if (p.Y - 8 < 0)
            {
                p.Y = 0;
                Velocity *= new Vector2(1, -1);
            }
            else if (p.Y + 8 > Game1.Height)
            {
                p.Y = Game1.Height;
                Velocity *= new Vector2(1, -1);
            }
            Sprite.Position += Velocity;
            Bounds.Center = Center;

        }

        public static void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Sprite.Draw(gameTime,spriteBatch);
        }
    }
}
