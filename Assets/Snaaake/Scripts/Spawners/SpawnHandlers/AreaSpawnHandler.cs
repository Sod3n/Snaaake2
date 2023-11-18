using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Spawners
{
    public interface IAreaSpawnHandler : IFieldSpawnHandler
    {
        public Vector2Int MaxAreaSize { get; }
        public bool TryGetRandomSpawnArea(bool[,] needPoints, out List<Vector2Int> randomSpawnArea);
    }

    public class AreaSpawnHandler : FieldSpawnHandler, IAreaSpawnHandler, IInitializable
    {
        public Vector2Int MaxAreaSize => _settings.AreaSize;

        private Dictionary<List<int?>, List<Vector2Int>> _areaSpawnPoints
            = new Dictionary<List<int?>, List<Vector2Int>>();
        private List<int?> _areaSpawnPointsKeys = new List<int?>();
        private List<Vector2Int> _areaSpawnPointsValues = new List<Vector2Int>();
        private ListPool<int?> _keysPool;
        private ListPool<Vector2Int> _valuesPool;

        private Settings _settings;

        //we can create ListPool internaly but if we do this it will create hidden dependency.
        public AreaSpawnHandler(Settings settings, ListPool<Vector2Int> areaSpawnPointsValuesPool, 
            ListPool<int?> keysPool) : base(settings)
        {
            _settings = settings;
            _valuesPool = areaSpawnPointsValuesPool;
            _keysPool = keysPool;
        }

        public bool TryGetRandomSpawnArea(bool[,] needPoints, out List<Vector2Int> randomSpawnArea)
        {
            RefillAreaSpawnPoints(needPoints);
#if DebugAreaSpawn
            foreach (var pair in _areaSpawnPoints)
            {
                for(int i = 0; i < pair.Value.Count; i++)
                {
                    Color color = Color.white;
                    float size = 0;
                    switch(i)
                    {
                        case 0:
                            color = Color.blue;
                            size = 0.1f;
                            break;
                        case 1:
                            color = Color.green;
                            size = 0.2f;
                            break;
                        case 2:
                            color = Color.red;
                            size = 0.3f;
                            break;
                        case 3:
                            color = Color.black;
                            size = 0.4f;
                            break;
                    }
                    DebugUtils.DrawRect(pair.Value[i], size, color, 1);
                }
            }
#endif
            randomSpawnArea = null;
            if (_areaSpawnPoints.Count == 0) return false;

            randomSpawnArea = _areaSpawnPoints.ElementAt(UnityEngine.Random.Range(0, _areaSpawnPoints.Count)).Value;
            
            return true;
        }

        public new void Initialize()
        {
            base.Initialize();
            int size = _fieldSize.x * _fieldSize.y;
            _areaSpawnPoints.EnsureCapacity(size);
            _valuesPool.Resize(size);
            _keysPool.Resize(size);
            RefillAreaSpawnPoints(new bool[_settings.AreaSize.y, _settings.AreaSize.x]);
        }

        private void RefillAreaSpawnPoints(bool[,] needPoints)
        {
            DespawnAreaSpawnPoints();
            _areaSpawnPoints.Clear();

            for (int i = 0; i < _spawnPoints.Count; i++)
            {
                if (!TryRefillAreaSpawnPointLists(needPoints, _spawnPoints.ElementAt(i).Value)) continue;

                _areaSpawnPoints.Add(_areaSpawnPointsKeys, _areaSpawnPointsValues);
            }
        }

        private bool TryRefillAreaSpawnPointLists(bool[,] needPoints, Vector2Int offset)
        {
            _areaSpawnPointsKeys = _keysPool.Spawn();
            _areaSpawnPointsValues = _valuesPool.Spawn();

            Vector2Int findPoint;

            int rows = needPoints.GetUpperBound(0) + 1;
            int columns = needPoints.Length / rows;

            for (int v = 0; v < rows; v++)
            {
                for (int q = 0; q < columns; q++)
                {
                    if (!needPoints[v, q]) continue;

                    findPoint = offset;
                    findPoint.x += q;
                    findPoint.y += v;


                    var point = _spawnPoints.FirstOrDefault(v => v.Value == findPoint);

                    if (point.Key is null)
                        return false;

                    _areaSpawnPointsValues.Add(point.Value);
                    _areaSpawnPointsKeys.Add(point.Key);
                }
            }

            return true;
        }

        private void DespawnAreaSpawnPoints()
        {
            foreach (var pair in _areaSpawnPoints)
            {
                _keysPool.Despawn(pair.Key);
                _valuesPool.Despawn(pair.Value);
            }
        }

        [Serializable]
        public new class Settings : FieldSpawnHandler.Settings
        {
            public Vector2Int AreaSize;
        }
    }
}
