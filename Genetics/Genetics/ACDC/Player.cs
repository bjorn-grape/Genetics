using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;


namespace Genetics
{
    public class Player
    {
        #region Attributes

        public enum Direction
        {
            left,
            right,
            none
        }

        public Direction lastDir = Direction.none;
        
        public Vector2 Position
        {
            get => _position;
            private set => _position = value;
        }

        private Vector2 _position;


        private float _speed = 0.1f;
        private float _jumpPower = 1f;
        private float _jumpDuration = 1f;
        private bool _canJump = true;

        /// <summary>
        /// Physical force applied to player
        /// </summary>
        Vector2 _force = new Vector2(0, 0.5f);

        /// <summary>
        /// Score per Map
        /// </summary>
        int _score = 0;

        /// <summary>
        /// Score obtained at the end, _score is added at the completion of each map.
        /// </summary>
        int _finalScore = 0;

        private Matrix _brain1;
        private Matrix _brain2;
        private Matrix _brain3;

        private Matrix _cache_brain;

        #endregion

        #region Constructor

        public Player(bool init = true)
        {
            _position = new Vector2(0);
            if (init)
            {
                _brain1 = new Matrix(49, 16, true);
                _brain2 = new Matrix(16, 16, true);
                _brain3 = new Matrix(16, 4, true);
            }
            
        }

        public Player(List<Matrix> listMatrix)
        {
            _position = new Vector2(0, 0);
            _brain1 = listMatrix[0];
            _brain2 = listMatrix[1];
            _brain3 = listMatrix[2];
        }

        #endregion

        #region Getter/Setter

        public List<Matrix> Getbrains()
        {
            return new List<Matrix> {_brain1, _brain2, _brain3};
        }

        public void ResetScore()
        {
            _score = 0;
            _finalScore = 0;
        }

        public int GetScore()
        {
            return _score + _finalScore;
        }

        public void SetScore(int score)
        {
            _score = score;
        }

        public void Setposition(float x, float y)
        {
            _position.X = x;
            _position.Y = y;
        }

        /// <summary>
        /// Put player to the beginning of the map
        /// </summary>
        /// <param name="map"></param>
        public void SetStart(Map map)
        {
            _position = map.PosInit;
        }

        #endregion

        #region BrainToAct

        public Matrix UseBrain(Matrix mat)
        {
            return mat *_brain1 * _brain2 * _brain3;
        }

        public void ReceiveOrder(bool left, bool right, bool up, bool reset)
        {
            Map mappy = RessourceLoad.GetCurrentMap();
            ApplyForce(mappy);
            InteractEnv(mappy);
            lastDir = Direction.none;
            if (right)
            {
                Move(1, 0, mappy);
                lastDir = Direction.right;
            }


            if (left)
            {
                Move(-1, 0, mappy);
                lastDir = Direction.left;
            }

            if (left && right)
                lastDir = Direction.none;
                
            if (reset)
                SetStart(mappy);

            if (up)
            {
                Jump();
            }
        }

        public void PlayAFrame()
        {
            Matrix act = UseBrain(RessourceLoad.GetCurrentMap().GetMapAround(Position.X, Position.Y));
            ReceiveOrder(act.Tab[0, 0] > 0.5f, act.Tab[0, 1] > 0.5f, act.Tab[0, 2] > 0.5f, act.Tab[0, 3] > 0.5f);
        }

        #endregion

        #region ChangePlayer

        public void Replace(Player p1, bool replace_with_mutation = true)
        {
            _position = p1._position;
            _score = p1._score;
            _finalScore = p1._finalScore;
            if (!replace_with_mutation)
            {
                _brain1 = new Matrix(49, 16, true);
                _brain2 = new Matrix(16, 16, true);
                _brain3 = new Matrix(16, 4, true);
            }
            else
            {
                _brain1.MakeCopyFrom(p1._brain1);
                _brain2.MakeCopyFrom(p1._brain2);
                _brain3.MakeCopyFrom(p1._brain3);
                _brain1.ApplyMutation();
                _brain2.ApplyMutation();
                _brain3.ApplyMutation();
            }
        }

        #endregion

        #region Evironment

        /// <summary>
        /// Permits to change current map to the next one, also update final score
        /// </summary>
        /// <param name="map"></param>
        public void InteractEnv(Map map)
        {
            if (map.IsEndMap(Position.X, Position.Y))
            {
                _finalScore += _score;
                _score = 0;
            }
            else
            {
                _score = Convert.ToInt32(Position.X * 100);
            }
        }


        /// <summary>
        /// smooth the movement of player when they are near colliders
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="map"></param>
        void CreateMove(float x, float y, Map map)
        {
            int sizesplit = 10;
            float brake = 1;
            if (!IsOnGround(map))
                brake = 0.75f;
            for (float i = 0; i < sizesplit; i++)
            {
                float finalx = _position.X + x / sizesplit * brake;
                float finaly = _position.Y + y / sizesplit;
                if (!map.IsColliding(_position.X, finaly))
                    _position.Y = finaly;

                if (!map.IsColliding(finalx, _position.Y))
                    _position.X = finalx;
            }
        }

        /// <summary>
        /// Move the player and create inertia
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="map"></param>
        public void Move(int x, int y, Map map)
        {
            CreateMove(x * _speed, y * _speed, map);

            if (x > 0 && _force.X < 0.31f)
                _force.X += 0.05f;
            if (x < 0 && _force.X > -0.31f)
                _force.X -= 0.05f;
        }

        /// <summary>
        /// Change forces applied to make them back to normal
        /// </summary>
        /// <param name="val"></param>
        /// <param name="middle"></param>
        /// <param name="step"></param>
        /// <returns></returns>
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
            _force.Y = AdjustForce(_force.Y, .5f, 0.02f); // gravity
        }

        /// <summary>
        /// Apply physical force to player, once per turn
        /// </summary>
        /// <param name="map"></param>
        public void ApplyForce(Map map)
        {
            UpdateForce();
            CreateMove(_force.X, _force.Y, map);
            if (IsOnGround(map))
                _jumpDuration = 1f;
            else
                _jumpDuration -= 0.3f;
        }

        public void Jump()
        {
            if (_jumpDuration > 0)
                _force.Y = -0.4f * _jumpPower;
        }

        private bool IsOnGround(Map map)
        {
            bool grd = map.IsGroundForPlayer(_position.X, _position.Y);
            if (!grd)
                _speed = 0.05f;
            else
                _speed = 0.1f;

            return grd;
        }

        #endregion

        #region Comparisons

        public static bool operator >(Player a, Player b)
        {
            return a.GetScore() > b.GetScore();
        }

        public static bool operator <(Player a, Player b)
        {
            return a.GetScore() < b.GetScore();
        }

        #endregion
    }
}