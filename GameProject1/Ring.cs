using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.ComponentModel.DataAnnotations;
using GameProject1.Tools;
using SharpDX.Direct3D11;
using Microsoft.Xna.Framework.Content;
using System.Reflection.Metadata;

namespace GameProject1
{
    public static class Ring
    {
        public static float MaxRadius = 10.0f;
        public static Microsoft.Xna.Framework.Vector2 Center => Ball.Center;
        public static float Radius => Math.Min((PosTool.VectorSubtraction(new Microsoft.Xna.Framework.Vector2(Game1.CurrentMouse.X,Game1.CurrentMouse.Y),Ball.Center).Length()),16*MaxRadius);

        private static StaticSprite _ballSprite = new StaticSprite("Ring");

        public static void LoadContent(ContentManager content)
        {
            _ballSprite.LoadContent(content);
        }

        public static void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            float distanceFromBallCenter = PosTool.VectorSubtraction(Game1.MousePosVector, Ball.Center).Length();
            
            _ballSprite.Scale = Math.Max(Math.Min(distanceFromBallCenter / 16,MaxRadius),1f);
            _ballSprite.Position = Ball.Center;
            _ballSprite.DrawOffset = new Vector2(-8f,-8f) * (_ballSprite.Scale);
            spriteBatch.DrawString(Game1.Font, ""+(_ballSprite.Position.X)+", "+ (_ballSprite.Position.Y), Vector2.One, Color.White);
            _ballSprite.Draw(gameTime, spriteBatch,Color.White);
        }
    
    }
}
