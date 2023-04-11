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
    public class Goal
    {
        public AniSprite Sprite { get; set; }
        public Vector2 Center => PosTool.VectorSubtraction(Sprite.Position, new Vector2(-16, -16));
        public float Radius = 32;
        public BoundingCircle Bounds;

        public Goal(Vector2 position)
        {
           Sprite = new AniSprite("Goal",position,1f,Directions.Down,4,4,32,32,Color.White);
            Bounds = new BoundingCircle(Center, 32);
        }

        public bool IsCollision()
        {
           return Bounds.CollidesWith(Ball.Bounds);
        }

        public void LoadContent(ContentManager content)
        {
            Sprite.LoadContent(content);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Sprite.Draw(gameTime, spriteBatch);
        }
    }
}
