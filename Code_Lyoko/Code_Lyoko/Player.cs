using System;
using Microsoft.Xna.Framework;

namespace Code_Lyoko
{
    public class Player
    {
        public float Life = 0;
        public Vector2 Position;
        private float _speed = 2;
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
            Position = position * 32;
        }

        public void Move(int x, int y, Map map)
        {
            int finalx = Convert.ToInt32(Position.X + x * _speed);
            int finaly = Convert.ToInt32(Position.Y + y * _speed);
            if (!map.IsColliding(finalx, y))
                Position.X = finalx;
            if (!map.IsColliding(x, finaly))
                Position.Y = finaly; 
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