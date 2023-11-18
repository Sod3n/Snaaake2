using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerFacade : MonoBehaviour
    {
        public Transform Transform
        {
            get => _rigidbody.transform;
        }
        public List<GameObject> ActiveBodyParts
        {
            get => _bodyPartsContainer.ActiveUnits;
        }
        public Action<GameObject> OnFoodEated
        {
            get => _foodCheckOnMove.OnFoodEated;
            set => _foodCheckOnMove.OnFoodEated = value;
        }

        private Rigidbody2D _rigidbody;
        private BodyPartsContainer _bodyPartsContainer;
        private FoodCheckOnMove _foodCheckOnMove;

        [Inject]
        public void Inject(Rigidbody2D rigidbody, BodyPartsContainer bodyPartsContainer, 
            FoodCheckOnMove foodCheckOnMove)
        {
            _rigidbody = rigidbody;
            _bodyPartsContainer = bodyPartsContainer;
            _foodCheckOnMove = foodCheckOnMove;
        }
    }
}
