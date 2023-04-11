using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameProject1.Tools;
using Microsoft.Xna.Framework;

namespace GameProject1
{
    public enum Directions
    {
        Up, Down, Left, Right
    }
    public class ArrowField: BoardPiece
    {
        private Directions _direction;
        public AniSprite Sprite;
        public static float Width = 60;
        public static float Height = 60;
        public BoundingRectangle Bounding;

        public ArrowField(Vector2 position, Directions direction)
        {
            if (direction == Directions.Down)
            {
                Sprite = new AniSprite("ArrowField", Vector2.Zero, 1f, Directions.Down, 1, 2, 60, 60, Color.White);
            }
            else
            {
                Sprite = new AniSprite("ArrowField", Vector2.Zero, 1f, Directions.Up, 1, 2, 60, 60, Color.Blue);

            }
            _direction = direction;
            Sprite.Position = position;
            Sprite.Direction = direction;
            Bounding = new BoundingRectangle(position, Width, Height);
        }

        public override bool IsCollision()
        {
            return PosTool.Collides(Ball.Bounds, this.Bounding);
        }

        public Vector2 GetDirectionVector()
        {
            switch (_direction)
            {
                case Directions.Up:
                    return new Vector2(0, -1);
                    break;
                case Directions.Down:
                    return new Vector2(0, 1);
                    break;
                case Directions.Left:
                    return new Vector2(-1, 0);
                    break;
                case Directions.Right:
                    return new Vector2(1, 0);
                    break;
                default:
                    return Vector2.Zero;
                    break;
            }
        }
    }
}
