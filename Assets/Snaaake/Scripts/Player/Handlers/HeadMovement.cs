using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Player
{
    public class HeadMovement : IInitializable
    {
        private Model.Movement _model;
        public HeadMovement(Model.Movement model)
        {
            _model = model;
        }

        public void Initialize()
        {
            StartMove();
        }
        private async UniTask MoveTask()
        {
            await UniTask.WaitForSeconds(_model.MoveFrequency);

            _model.OnHeadMove.Invoke();

            _model.Rigidbody.MovePosition(
                _model.Rigidbody.position + _model.MovementDirection * _model.MoveStep);

            StartMove();
        }
        private void StartMove()
        {
            _ = MoveTask();
        }
    }
}
