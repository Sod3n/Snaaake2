using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace Player
{
    public class BodyPartsContainer : IInitializable
    {
        public List<GameObject> ActiveUnits
        {
            get { return _settings.ActiveUnits; }
        }

        private ObjectPool<GameObject> _pool;
        private Settings _settings;

        public BodyPartsContainer(Settings settings)
        {
            _settings = settings;
        }

        public void ActivateUnit()
        {
            var g = _pool.Get();
            _settings.ActiveUnits.Add(g);
        }
        public void DisableUnit()
        {
            if(_settings.ActiveUnits.Count < 1) return;

            _pool.Release(ActiveUnits.Last());
            ActiveUnits.Remove(ActiveUnits.Last());
        }

        public void Initialize()
        {
            _pool = new ObjectPool<GameObject>(CreatePooledUnit, 
                OnTakeFromPool, OnReturnedToPool, null, 
                true, _settings.Capacity, _settings.Capacity);

            ActiveUnits.Capacity = _settings.Capacity;
        }

        private GameObject CreatePooledUnit()
        {
            if (ActiveUnits.Count > 0)
                return GameObject.Instantiate(_settings.UnitPrefab,
                    ActiveUnits.First().transform.position,
                    ActiveUnits.First().transform.rotation);
            else
                return GameObject.Instantiate(_settings.UnitPrefab);
        }
        private void OnTakeFromPool(GameObject g)
        {
            g.SetActive(true);

            if (ActiveUnits.Count == 0) return;
            
            g.transform.position = ActiveUnits.First().transform.position;
        }
        private void OnReturnedToPool(GameObject g)
        {
            g.SetActive(false);
        }

        [Serializable]
        public class Settings
        {
            public List<GameObject> ActiveUnits;
            public GameObject UnitPrefab;
            public int Capacity;
        }
    }
}
