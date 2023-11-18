using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Spawners
{
    public class FoodSpawner : IInitializable
    {
        private PlayerFacade _playerFacade;
        private Settings _settings;
        private IFieldSpawnHandler _foodSpawnHandler;
        public GameFieldCircle _circleAtPlayerHead;

        public FoodSpawner(
            Settings settings, IFieldSpawnHandler foodSpawnLogic,
            GameFieldCircle circleAtPlayerHead, PlayerFacade playerFacade
            )
        {
            _settings = settings;
            _foodSpawnHandler = foodSpawnLogic;
            _circleAtPlayerHead = circleAtPlayerHead;
            _playerFacade = playerFacade;
        }

        public bool TrySpawn()
        {
            _foodSpawnHandler.ResetSpawnPoints();
            
            _foodSpawnHandler.RemoveGameObjectsFromSpawn(_playerFacade.ActiveBodyParts);

            _circleAtPlayerHead.MoveTo(Vector2Int.RoundToInt(_playerFacade.Transform.position));
            _foodSpawnHandler.RemovePointsFromSpawn(_circleAtPlayerHead.Points);

            Vector2Int randomSpawnPoint;

            if (!_foodSpawnHandler.TryGetRandomSpawnPoint(out randomSpawnPoint)) return false;

            SpawnFoodAtFoodPosition(randomSpawnPoint);
            return true;
        }
        private void Respawn(GameObject gameObject)
        {
            GameObject.Destroy(gameObject);
            TrySpawn();
        }
        public void Initialize()
        {
            _playerFacade.OnFoodEated += Respawn;
        }

        private void SpawnFoodAtFoodPosition(Vector2 foodPosition)
        {
            GameObject.Instantiate(_settings.FoodPrefab, foodPosition,
                _settings.FoodPrefab.transform.rotation);
        }

        [Serializable]
        public class Settings
        {
            public GameObject FoodPrefab;
        }
    }
}
