using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Spawners
{
    public class WallSpawner : IInitializable
    {
        private WallPool _pool;
        private IAreaSpawnHandler _wallSpawnHandler;
        private GameFieldCircle _circleAtPlayerHead;
        private PlayerFacade _playerFacade;

        private List<GameObject> _spawnedWalls = new List<GameObject>();

        public WallSpawner(WallPool pool, IAreaSpawnHandler areaSpawnLogic,
            GameFieldCircle circleAtPlayerHead, PlayerFacade playerFacade)
        {
            _pool = pool;
            _wallSpawnHandler = areaSpawnLogic;
            _circleAtPlayerHead = circleAtPlayerHead;
            _playerFacade = playerFacade;
        }

        public bool TrySpawn()
        {
            _wallSpawnHandler.ResetSpawnPoints();

            _circleAtPlayerHead.MoveTo(Vector2Int.RoundToInt(_playerFacade.Transform.position));
            _wallSpawnHandler.RemovePointsFromSpawn(_circleAtPlayerHead.Points);

            _wallSpawnHandler.RemovePointFromSpawn(Vector2Int.RoundToInt(_playerFacade.Transform.position));
            _wallSpawnHandler.RemoveGameObjectsFromSpawn(_playerFacade.ActiveBodyParts);
            _wallSpawnHandler.RemoveGameObjectsFromSpawn(_spawnedWalls);

            List<Vector2Int> points;

            if (!_wallSpawnHandler.TryGetRandomSpawnArea(GetRandomPointsMask(), out points)) return false;

            GenerateWallsAtPoints(points);
            return true;
        }

        public void Initialize()
        {
            _pointsMask = new bool[_wallSpawnHandler.MaxAreaSize.y, _wallSpawnHandler.MaxAreaSize.x];
            while (TrySpawn()) ;
        }

        private bool[,] _pointsMask;
        private bool[,] GetRandomPointsMask()
        {
            int rows = _pointsMask.GetUpperBound(0) + 1;
            int columns = _pointsMask.Length / rows;

            for (int i = 0; i < rows; i++)
            {
                for(int q = 0; q < columns; q++)
                {
                    _pointsMask[i, q] = Convert.ToBoolean(UnityEngine.Random.Range(0, 2));
                }
            }
            
            return _pointsMask;
        }
        private void GenerateWallsAtPoints(List<Vector2Int> points)
        {
            foreach (var point in points)
            {
                _spawnedWalls.Add(_pool.Spawn(point).gameObject);
            }
        }

    }
}
