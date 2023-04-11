using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject1
{
    public enum PieceTypes
    {
        ArrowField
    }
    public abstract class BoardPiece
    {
        private StaticSprite _sprite;
        private Rectangle _rectangle;
        private Vector2 _position;

        public abstract bool IsCollision();

    }
}
