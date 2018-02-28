using System;
using System.ComponentModel;
using Microsoft.Xna.Framework;

namespace Code_Lyoko
{
    public class Player
    {
        private float Life = 0;
        public Vector2 Position;
        private float _speed = 0.1f;
        private float _jumpPower = 1;
        private float jumpDuration = 1f;
        private UInt32 Money = 0;
        Vector2 Force = new Vector2(0, 0.5f);

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

        void createMove(float x, float y, Map map)
        {
            int sizesplit = 10;
            for (float i = 0; i < sizesplit; i++)
            {
                float finalx = Position.X + x / sizesplit;
                float finaly = Position.Y + y / sizesplit;
                if (!map.IsColliding(Position.X, finaly))
                    Position.Y = finaly;

                if (!map.IsColliding(finalx, Position.Y))
                    Position.X = finalx;
            }
        }

        public void SetStart(Map map)
        {
            Position = map.posInit;
        }
        
        public void Move(int x, int y, Map map)
        {
            createMove(x * _speed, y * _speed , map);

            if (x > 0 && Force.X < 0.31f)
                Force.X += 0.05f;
            if (x < 0 && Force.X > -0.31f)
                Force.X -= 0.05f;
        }

        private float AdjustForce(float val, float middle,float step)
        {
            if (val < middle + step && val > middle - step)
                return middle;
            if (val > middle)
                return val -= step;
            return val += step;
        }

        private void UpdateForce()
        {
            Force.X = AdjustForce(Force.X, 0, 0.05f);
            Force.Y = AdjustForce(Force.Y, .5f, 0.04f); // gravity
        }

        public void ApplyForce(Map map)
        {
            UpdateForce();
            createMove(Force.X, Force.Y, map);
            Console.WriteLine("Vector X " + Force.X + "Y :" + Force.Y);
            if (IsOnGround(map))
                jumpDuration = 1f;
            else
                jumpDuration -= 0.5f;
        }

        public void Jump()
        {
            if (jumpDuration > 0)
            {
                Console.WriteLine("Jump");
                Force.Y = -0.5f;
            }
            
        }

        public bool IsOnGround(Map map)
        {
            
            return map.IsGroundForPlayer(Position.X, Position.Y);
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