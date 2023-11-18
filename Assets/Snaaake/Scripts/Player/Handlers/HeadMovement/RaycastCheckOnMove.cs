using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Player
{
    public abstract class RaycastCheckOnMove : IInitializable
    {
        protected readonly Movement _movement;
        protected RaycastHit2D _hit;

        private readonly LayerMask _layerMask;
        private Rigidbody2D _rigidbody;

        protected RaycastCheckOnMove(Movement movement, LayerMask layerMask, Rigidbody2D rigidbody)
        {
            _movement = movement;
            _layerMask = layerMask;
            _rigidbody = rigidbody;
        }

        protected virtual void Check()
        {
            _hit = Physics2D.Raycast(
                _rigidbody.position + _movement.MovementDirection * 0.6f, 
                _movement.MovementDirection, 0.4f, _layerMask);

        }

        public void Initialize()
        {
            _movement.OnHeadMove += Check;
        }
    }
}
