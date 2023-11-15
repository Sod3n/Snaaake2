using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Player
{
    public class BodyPartsMovement : IInitializable
    {
        private Model.Movement _movement;
        private Model.Body _body;

        public BodyPartsMovement(Model.Movement model, Model.Body body)
        {
            _movement = model;
            _body = body;
        }

        public void Initialize()
        {
            _movement.OnHeadMove += MoveBodyParts;
        }

        private void MoveBodyParts()
        {
            if(_body.BodyParts.Count == 0) return;

            for(int i = _body.BodyParts.Count-1; i > 0; i--)
            {
                _body.BodyParts[i].position = _body.BodyParts[i - 1].position;
            }
            _body.BodyParts[0].position = _movement.Rigidbody.position;
        }
    }
}
