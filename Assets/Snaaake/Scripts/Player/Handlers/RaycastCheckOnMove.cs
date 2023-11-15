using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using static Player.Model;

namespace Player
{
    public abstract class RaycastCheckOnMove : IInitializable
    {
        protected readonly Model.Movement _movement;
        protected RaycastHit2D _hit;

        private readonly LayerMask _layerMask;

        protected RaycastCheckOnMove(Movement movement, LayerMask layerMask)
        {
            _movement = movement;
            _layerMask = layerMask;
        }

        protected virtual void Check()
        {
            _hit = Physics2D.Raycast(
                _movement.Rigidbody.position, _movement.MovementDirection, 1, _layerMask);

        }

        public void Initialize()
        {
            _movement.OnHeadMove += Check;
        }
    }
}
