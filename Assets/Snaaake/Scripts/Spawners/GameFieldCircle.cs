using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;
using Zenject;

namespace Spawners
{
    public class GameFieldCircle : IInitializable
    {
        private Settings _settings;
        private Vector2Int _position;
        private List<Vector2Int> _points = new List<Vector2Int>();
        private Vector2Int _offsetFromLastPos = new Vector2Int();

        public List<Vector2Int> Points
        {
            get
            {
                return _points;
            }
        }

        public GameFieldCircle(Settings settings)
        {
            _settings = settings;
        }
        public void MoveTo(Vector2Int position)
        {
            _offsetFromLastPos = position - _position;
            _position = position;

            for (int i = 0; i < _points.Count; i++)
            {
                _points[i] = new Vector2Int(_points[i].x + _offsetFromLastPos.x, _points[i].y + _offsetFromLastPos.y);
            }
#if DebugGameFieldCircle
            foreach (var pair in _points)
                DebugUtils.DrawRect(pair, 0.5f, Color.black, 1);
#endif
        }

        public void Initialize()
        {
            FillPoints();
        }

        private void FillPoints()
        {
            //Simple one - just check all points in square with side equals radius * 2 + 1
            int squareSide = _settings.Radius * 2 + 1;
            Vector2Int point = new Vector2Int();
            _points.Clear();

            _position = new Vector2Int(_settings.Radius + 1, _settings.Radius + 1);

            for (int i = 0; i <= squareSide; i++)
            {
                for(int q = 0; q <= squareSide; q++)
                {
                    point.x = i;
                    point.y = q;

                    if ((point - _position).magnitude > _settings.Radius) continue;

                    _points.Add(point);
                }
            }

        }

        [Serializable]
        public class Settings
        {
            public int Radius;
        }
    }
}
