using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Player
{
    public class BodyPartsMovement : IInitializable
    {
        private Movement _movement;
        private BodyPartsContainer _bodyPartsContainer;
        private Rigidbody2D _rigidbody;

        public BodyPartsMovement(Movement model, BodyPartsContainer bodyPartsContainer, Rigidbody2D rigidbody)
        {
            _movement = model;
            _bodyPartsContainer = bodyPartsContainer;
            _rigidbody = rigidbody;
        }

        public void Initialize()
        {
            _movement.OnHeadMove += Move;
        }

        private void Move()
        {
            if(_bodyPartsContainer.ActiveUnits.Count == 0) return;

            for(int i = _bodyPartsContainer.ActiveUnits.Count-1; i > 0; i--)
            {
                _bodyPartsContainer.ActiveUnits[i].transform.position =
                    _bodyPartsContainer.ActiveUnits[i - 1].transform.position;
            }
            _bodyPartsContainer.ActiveUnits[0].transform.position = _rigidbody.position;
        }
    }
}
