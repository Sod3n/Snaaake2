using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Player
{
    public class InvincibleControll : IInitializable
    {
        private Model.Movement _movement;
        private Model.Invincibility _invincibility;
        public InvincibleControll(Model.Movement movement, Model.Invincibility invincibility)
        {
            _movement = movement;
            _invincibility = invincibility;
        }

        public void Initialize()
        {
            _movement.OnBumped += MakeInvincible;
        }

        private void MakeInvincible()
        {
            _invincibility.Invincible = true;
            _ = ResetInvincible();
        }
        private async UniTask ResetInvincible()
        {
            await UniTask.WaitForSeconds(_invincibility.TimeOfInvincibility);
            _invincibility.Invincible = false; 
        }
    }
}
