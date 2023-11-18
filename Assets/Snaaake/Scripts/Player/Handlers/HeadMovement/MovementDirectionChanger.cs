using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Player
{
    public class MovementDirectionChanger : ITickable
    {
        private Input _input;
        private Movement _movement;

        public MovementDirectionChanger(Input input, Movement movement)
        {
            _input = input;
            _movement = movement;
        }

        public void Tick()
        {
            if(_input.Rotation == -_movement.MovementDirection) return;
            if(_input.Rotation.sqrMagnitude != 1) return;

            _movement.MovementDirection = _input.Rotation;
        }
    }
}
