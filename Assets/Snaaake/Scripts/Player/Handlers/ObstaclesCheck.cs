using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WSWhitehouse.TagSelector;
using Zenject;

namespace Player
{
    public class ObstaclesCheck : RaycastCheckOnMove
    {
        private Quaternion _randRotation;
        private Settings _settings;
        private Model.Invincibility _invincibility;
        public ObstaclesCheck(Settings settings, Model.Movement movement, Model.Invincibility invincibility)
            : base(movement, settings.ObstaclesMask)
        {
            _settings = settings;
            _invincibility = invincibility;
        }

        protected override void Check()
        {
            base.Check();

            if (!_hit) return;

            if (_invincibility.Invincible && _hit.collider.gameObject.CompareTag(_settings.BodyPartTag)) return;

            RandomizeRandRotation();

            _movement.MovementDirection = _randRotation * _movement.MovementDirection;

            _movement.OnBumped.Invoke();

            if (_hit.collider.gameObject.CompareTag(_settings.WallTag))
            {
                Check();
            }
        }

        private void RandomizeRandRotation()
        {
            if(UnityEngine.Random.Range(0, 2) == 0)
            {
                _randRotation = Quaternion.Euler(0, 0, -90);
            }
            else
            {
                _randRotation = Quaternion.Euler(0, 0, 90);
            }
        }

        [Serializable]
        public class Settings
        {
            public LayerMask ObstaclesMask;
            [TagSelector] public string WallTag;
            [TagSelector] public string BodyPartTag;
        }
    }
}
