using System;
using Microsoft.Xna.Framework;

namespace Code_Lyoko
{
    public class Player
    {
        public float Life = 0;
        public Vector2 Position;
        private float _speed = 0.1f;
        private float _jumpPower = 1;
        public UInt32 Money = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="life"></param>
        /// <param name="position">The position is multiplied by 32</param>
        public Player(float life, Vector2 position)
        {
            Life = life;
            Position = position;
        }

        public void Move(int x, int y, Map map)
        {
            float finalx = Position.X + x * _speed;
            float finaly = Position.Y + y * _speed;
            if (!map.IsColliding(finalx, finaly))
            {
                Position.X = finalx;
                Position.Y = finaly;
            }
        }

        public void AddMoney(uint amount)
        {
            Money += amount;
        }

        public void Pay(uint amount)
        {
            Money -= amount;
        }

        public void Setposition(float x, float y)
        {
            Position.X = x;
            Position.Y = y;
        }

        public void IncreaseSpeed()
        {
            _speed *= 1.25f;
        }

        public void DecreaseSpeed()
        {
            _speed /= 1.25f;
        }
    }
}