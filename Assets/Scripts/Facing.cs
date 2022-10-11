using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CrossFates
{
    [Serializable]
    public class Facing
    {

        [SerializeField] private Sprite _spriteUp;
        [SerializeField] private Sprite _spriteDown;
        [SerializeField] private Sprite _spriteLeft;
        [SerializeField] private Sprite _spriteRight;

        private enum Directions
        {
            up,
            down,
            left,
            right,
        }

        private Directions _direction;
        private Dictionary<Directions, Vector2> _directions = new Dictionary<Directions, Vector2>()
    {
        {Directions.up, new Vector2(0, 1)},
        {Directions.down, new Vector2(0, -1)},
        {Directions.left, new Vector2(-1, 0)},
        {Directions.right, new Vector2(1, 0)},

    };
        private Dictionary<Directions, Sprite> _sprites = new Dictionary<Directions, Sprite>();

        public Vector2 Direction => _directions.GetValueOrDefault(_direction);
        public Sprite Sprite => _sprites.GetValueOrDefault(_direction);

        public void Init()
        {
            _sprites.Add(Directions.up, _spriteUp);
            _sprites.Add(Directions.down, _spriteDown);
            _sprites.Add(Directions.left, _spriteLeft);
            _sprites.Add(Directions.right, _spriteRight);
        }
        public void CheckDirection(Vector2 vector)
        {
            if (Mathf.Abs(vector.x) > Mathf.Abs(vector.y))
            {
                if (vector.x < 0)
                {
                    _direction = Directions.left;
                }
                else
                {
                    _direction = Directions.right;
                }
            }
            else
            {
                if (vector.y < 0)
                {
                    _direction = Directions.down;
                }
                else
                {
                    _direction = Directions.up;
                }
            }
        }

    }
}
