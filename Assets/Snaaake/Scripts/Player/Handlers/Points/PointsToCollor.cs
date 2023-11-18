using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PointsToCollor : ITickable
    {
        private BodyPartsContainer _bodyPartsContainer;
        private GrowthControll _growthControll;
        private Settings _settings;

        public PointsToCollor(BodyPartsContainer bodyPartsContainer, GrowthControll growthControll, Settings settings)
        {
            _bodyPartsContainer = bodyPartsContainer;
            _growthControll = growthControll;
            _settings = settings;
        }
        int _pointsIterator = 0;
        public void Tick()
        {
            _pointsIterator = 0;
            foreach (var gameObject in _bodyPartsContainer.ActiveUnits)
            {
                if(_pointsIterator < _growthControll.Points)
                {
                    gameObject.GetComponent<SpriteRenderer>().color = _settings.FullColor;
                }
                else
                {
                    gameObject.GetComponent<SpriteRenderer>().color = _settings.BaseColor;
                }
                _pointsIterator++;
            }
        }

        [Serializable]
        public class Settings
        {
            public Color BaseColor;
            public Color FullColor;
        }
    }
}
