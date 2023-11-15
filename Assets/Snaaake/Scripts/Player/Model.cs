using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [Serializable]
    public class Model
    {
        [Serializable]
        public class Movement
        {
            public Rigidbody2D Rigidbody;

            [Header("Seconds delay between move call")]
            public float MoveFrequency;
            [Header("Position change in units per move call")]
            public float MoveStep;

            [Space(10)]
            public Vector2 MovementDirection;
            public Action OnHeadMove = () => { };
            public Action OnBumped = () => { }; 
        }
        [Serializable]
        public class Body
        {
            public List<Transform> BodyParts;
            public int BodyPartsCapasity;
        }
        [Serializable]
        public class Invincibility
        {
            public bool Invincible = false;
            public float TimeOfInvincibility = 1;
        }

    }
}
