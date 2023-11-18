using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Player
{
    public class Movement : IInitializable
    {
        public Vector2 MovementDirection 
        { 
            get => _settings.MovementDirection;
            set => _settings.MovementDirection = value;
        }
        public event Action OnHeadMove = () => { };
        public Action OnBumped = () => { };

        private Settings _settings;
        private Rigidbody2D _rigidbody;

        public Movement(Settings settings, Rigidbody2D rigidbody)
        {
            _settings = settings;
            _rigidbody = rigidbody;
        }

        public void Initialize()
        {
            StartMove();
        }
        private async UniTask MoveTask()
        {
            await UniTask.WaitForSeconds(_settings.MoveFrequency);

            OnHeadMove.Invoke();

            _rigidbody.MovePosition(
                _rigidbody.position + MovementDirection * _settings.MoveStep);

            StartMove();
        }
        private void StartMove()
        {
            _ = MoveTask();
        }

        [Serializable]
        public class Settings
        {
            [Header("Seconds delay between move call")]
            public float MoveFrequency;
            [Header("Position change in units per move call")]
            public float MoveStep;

            [Header("Set start direction")]
            public Vector2 MovementDirection;
        }
    }
}
