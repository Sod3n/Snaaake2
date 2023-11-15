using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Player
{
    public class FoodCheck : RaycastCheckOnMove
    {
        private Settings _settings;
        private Model.Body _body;
        public FoodCheck(Model.Movement movement, Settings settings, Model.Body body) : base(movement, settings.FoodMask)
        {
            _settings = settings;
            _body = body;
        }

        protected override void Check()
        {
            base.Check();

            if (!_hit) return;

            _hit.collider.transform.position = new Vector2(UnityEngine.Random.Range(-15, 17) - 0.5f, UnityEngine.Random.Range(-8, 10) - 0.5f);

            var g = GameObject.Instantiate(_settings.BodyPart, _settings.BodyPart.transform.position, _settings.BodyPart.transform.rotation);
            _body.BodyParts.Add(g.transform);
        }

        [Serializable]
        public class Settings
        {
            public LayerMask FoodMask;
            public GameObject BodyPart;
        }
    }
}
