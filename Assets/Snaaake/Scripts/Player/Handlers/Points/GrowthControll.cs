using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class GrowthControll
    {
        
        public int Points 
        {
            get
            {
                return _points;
            }
            set
            {
                _points = value;

                if (Size > _points) return;

                _bodyPartsContainer.ActivateUnit();
                _points = 0;
            }
        }
        private int Size
        {
            get => _bodyPartsContainer.ActiveUnits.Count;
        }
        private int _points;
        private BodyPartsContainer _bodyPartsContainer;

        public GrowthControll(BodyPartsContainer bodyPartsContainer)
        {
            _bodyPartsContainer = bodyPartsContainer;
        }
    }
}
