using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [Serializable]
    public class MovementData
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
}
