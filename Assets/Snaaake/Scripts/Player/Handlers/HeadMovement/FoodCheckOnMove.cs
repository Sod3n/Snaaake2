using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Player
{
    public class FoodCheckOnMove : RaycastCheckOnMove
    {
        public Action<GameObject> OnFoodEated = (GameObject g) => { };

        private Settings _settings;
        private GrowthControll _growthControll;
        public FoodCheckOnMove(Movement movement, Settings settings, 
            GrowthControll growthControll, Rigidbody2D rigidbody) 
            : base(movement, settings.FoodMask, rigidbody)
        {
            _settings = settings;
            _growthControll = growthControll;
        }

        protected override void Check()
        {
            base.Check();

            if (!_hit) return;

            _hit.collider.transform.position = new Vector2(UnityEngine.Random.Range(-15, 17) - 0.5f, UnityEngine.Random.Range(-8, 10) - 0.5f);

            _growthControll.Points++;

            OnFoodEated.Invoke(_hit.collider.gameObject);
        }

        [Serializable]
        public class Settings
        {
            public LayerMask FoodMask;
        }
    }
}
