using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Player
{
    public class Invincible : IInitializable
    {
        public bool Value { get; private set; }

        private Movement _movement;
        private Settings _settings;
        public Invincible(Movement movement, Settings settings)
        {
            _movement = movement;
            _settings = settings;
        }

        public void Initialize()
        {
            _movement.OnBumped += MakeInvincible;
        }

        private void MakeInvincible()
        {
            Value = true;
            _ = ResetInvincible();
        }
        private async UniTask ResetInvincible()
        {
            await UniTask.WaitForSeconds(_settings.TimeOfInvincibility);
            Value = false; 
        }

        [Serializable]
        public class Settings
        {
            public float TimeOfInvincibility = 1;
        }
    }
}
