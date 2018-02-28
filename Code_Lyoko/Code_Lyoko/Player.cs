using System;
using System.ComponentModel;
using Microsoft.Xna.Framework;

namespace Code_Lyoko
{
    public class Player
    {
        private float _life = 0;

        public Vector2 Position
        {
            get { return _position; }
            private set { _position = value; }
        }

        private float _speed = 0.1f;
        private float _jumpPower = 1;
        private float _jumpDuration = 1f;
        private UInt32 _money = 0;
        Vector2 _force = new Vector2(0, 0.5f);
        private Vector2 _position;

        public void InteractEnv(Map map)
        {
            if (map.IsEndMap(Position.X, Position.Y))
            {
                if (!RessourceLoad.SetNextMap())
                {
                    Console.WriteLine("End!");
                }
                Position = RessourceLoad.GetCurrentMap().PosInit;
            }
        }

        public Player(float life, Vector2 position)
        {
            _life = life;
            _position = position;
        }

        void createMove(float x, float y, Map map)
        {
            int sizesplit = 10;
            for (float i = 0; i < sizesplit; i++)
            {
                float finalx = _position.X + x / sizesplit;
                float finaly = _position.Y + y / sizesplit;
                if (!map.IsColliding(_position.X, finaly))
                    _position.Y = finaly;

                if (!map.IsColliding(finalx, _position.Y))
                    _position.X = finalx;
            }
        }

        public void SetStart(Map map)
        {
            _position = map.PosInit;
        }

        public void Move(int x, int y, Map map)
        {
            createMove(x * _speed, y * _speed, map);

            if (x > 0 && _force.X < 0.31f)
                _force.X += 0.05f;
            if (x < 0 && _force.X > -0.31f)
                _force.X -= 0.05f;
        }

        private float AdjustForce(float val, float middle, float step)
        {
            if (val < middle + step && val > middle - step)
                return middle;
            if (val > middle)
                return val -= step;
            return val += step;
        }

        private void UpdateForce()
        {
            _force.X = AdjustForce(_force.X, 0, 0.05f);
            _force.Y = AdjustForce(_force.Y, .5f, 0.04f); // gravity
        }

        public void ApplyForce(Map map)
        {
            UpdateForce();
            createMove(_force.X, _force.Y, map);
            if (IsOnGround(map))
                _jumpDuration = 1f;
            else
                _jumpDuration -= 0.5f;
        }

        public void Jump()
        {
            if (_jumpDuration > 0)
                _force.Y = -0.5f * _jumpPower;
        }

        private bool IsOnGround(Map map)
        {
            return map.IsGroundForPlayer(_position.X, _position.Y);
        }

        public void AddMoney(uint amount)
        {
            _money += amount;
        }

        public void Pay(uint amount)
        {
            _money -= amount;
        }

        public void Setposition(float x, float y)
        {
            _position.X = x;
            _position.Y = y;
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