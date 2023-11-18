using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Player
{
    public class ShortenOnBumped : IInitializable
    {
        private Movement _movement;
        private BodyPartsContainer _bodyPartsContainer;
        private GrowthControll _growthControll;

        public ShortenOnBumped(Movement movement, BodyPartsContainer bodyPartsContainer, GrowthControll growthControll)
        {
            _movement = movement;
            _bodyPartsContainer = bodyPartsContainer;
            _growthControll = growthControll;
        }

        public void Initialize()
        {
            _movement.OnBumped += Shorten;
        }
        private void Shorten()
        {
            _bodyPartsContainer.DisableUnit();
            _growthControll.Points = 0;
        }
    }
}
