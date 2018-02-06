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
        

        public Player(float life, Vector2 position)
        {
            Life = life;
            Position = position;
        }

        public void Move(float x, float y)
        {
            Position.X += x * _speed;
            Position.Y += y * _speed;
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