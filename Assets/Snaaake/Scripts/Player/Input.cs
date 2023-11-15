using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Player
{
    public class Input : ITickable, IInitializable
    {
        public Vector2 Rotation { get; internal set; }

        private Controlls _controlls;

        public Input(Controlls controlls)
        {
            _controlls = controlls;
        }

        public void Initialize()
        {
            _controlls.Enable();
        }

        public void Tick()
        {
            Rotation = _controlls.GameMap.Rotation.ReadValue<Vector2>();
        }
    }
}
