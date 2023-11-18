using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Player
{
    public class ReloadSceneOnCount : ITickable
    {
        private BodyPartsContainer _container;
        private Settings _settings;

        public ReloadSceneOnCount(BodyPartsContainer container, Settings settings)
        {
            _container = container;
            _settings = settings;
        }

        public void Tick()
        {
            if (_container.ActiveUnits.Count > _settings.CountToReloadScene) return;

            SceneManager.LoadScene(0);
        }

        [Serializable]
        public class Settings
        {
            public int CountToReloadScene;
        }
    }
}
