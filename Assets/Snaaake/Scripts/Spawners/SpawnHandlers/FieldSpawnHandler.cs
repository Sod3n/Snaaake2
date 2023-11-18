using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Spawners
{
    public interface IFieldSpawnHandler
    {
        public void RemovePointFromSpawn(Vector2Int point);
        public void RemovePointsFromSpawn(List<Vector2Int> points);
        public void RemoveGameObjectsFromSpawn(List<GameObject> gameObjects);
        public bool TryGetRandomSpawnPoint(out Vector2Int randomSpawnPoint);  
        public void ResetSpawnPoints();
    }
    public class FieldSpawnHandler : IFieldSpawnHandler, IInitializable 
    {
        //use nullable int cause if not default value will be equal zero point that also maybe present in dict
        protected Dictionary<int?, Vector2Int> _spawnPoints = new Dictionary<int?, Vector2Int>();
        protected Dictionary<int?, Vector2Int> _fullSpawnPoints = new Dictionary<int?, Vector2Int>();  

        protected Vector2Int _fieldSize;
        private Settings _settings;
        public FieldSpawnHandler(Settings settings)
        {
            _settings = settings;
        }

        public bool TryGetRandomSpawnPoint(out Vector2Int randomSpawnPoint)
        {
            int random = UnityEngine.Random.Range(0, _spawnPoints.Count);

#if DebugFieldSpawn
            foreach (var point in _spawnPoints)
                DebugUtils.DrawRect(point.Value, 0.1f, Color.cyan, 1);
#endif
            randomSpawnPoint = Vector2Int.zero;

            if (_spawnPoints.Count == 0) return false;

            randomSpawnPoint = _spawnPoints.ElementAt(random).Value;

            return true;
        }

        public void Initialize()
        {
            SetFieldSize(_settings.FieldSize);
        }

        public void RemovePointFromSpawn(Vector2Int point)
        {
            int? i = _spawnPoints.FirstOrDefault(x => x.Value == point).Key;
            if (i != null)
                _spawnPoints.Remove(i);
        }

        public void RemovePointsFromSpawn(List<Vector2Int> points)
        {
            foreach (var point in points)
            {
                RemovePointFromSpawn(point);
            }
        }

        public void ResetSpawnPoints()
        {
            _spawnPoints.Clear();
            _spawnPoints = _fullSpawnPoints.ToDictionary(
                entry => entry.Key,
                entry => entry.Value);
        }

        private void SetFieldSize(Vector2Int size)
        {
            _fieldSize = size;

            _spawnPoints.Clear();
            _fullSpawnPoints.Clear();
            _spawnPoints.EnsureCapacity(_fieldSize.x * _fieldSize.y);
            _fullSpawnPoints.EnsureCapacity(_fieldSize.x * _fieldSize.y);

            FillSpawnPoints();
        }
        private void FillSpawnPoints()
        {
            for (int i = 0; i < _fieldSize.x; i++)
            {
                for (int q = 0; q < _fieldSize.y; q++)
                {
                    _spawnPoints.Add(
                        i * _fieldSize.x + q,
                        new Vector2Int(i, q));
                    _fullSpawnPoints.Add(
                        i * _fieldSize.x + q,
                        new Vector2Int(i, q));
                }
            }
        }

        public void RemoveGameObjectsFromSpawn(List<GameObject> gameObjects)
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                Vector2 bodyPartPos = gameObjects[i].transform.position;
                RemovePointFromSpawn(Vector2Int.RoundToInt(bodyPartPos));
            }
        }

        [Serializable]
        public class Settings
        {
            public Vector2Int FieldSize;
        }
    }
}
