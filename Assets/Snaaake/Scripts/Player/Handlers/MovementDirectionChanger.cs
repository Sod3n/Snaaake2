using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Player
{
    public class MovementDirectionChanger : ITickable
    {
        private Input _input;
        private Model.Movement _movement;

        public MovementDirectionChanger(Input input, Model.Movement model)
        {
            _input = input;
            _movement = model;
        }

        public void Tick()
        {
            if(_input.Rotation == -_movement.MovementDirection) return;
            if(_input.Rotation.sqrMagnitude != 1) return;

            _movement.MovementDirection = _input.Rotation;
        }
    }
}
