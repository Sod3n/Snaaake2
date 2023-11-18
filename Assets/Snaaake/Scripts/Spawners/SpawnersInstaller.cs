using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Spawners
{
    public class SpawnersInstaller : MonoInstaller
    {
        //public PlayerFacade PlayerFacade;
        public GameObject WallPrefab;

        public FoodSpawner.Settings FoodSpawnerSettings;
        public FieldSpawnHandler.Settings FoodSpawnHandlerSettings;
        public AreaSpawnHandler.Settings AreaSpawnHandlerSettings;

        [Header("Circle at player head pos where food wouldn`t generate")]
        public GameFieldCircle.Settings FoodCircleSettings;

        [Header("Circle at player head pos where walls wouldn`t generate")]
        public GameFieldCircle.Settings WallCircleSettings;

        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<GameFieldCircle>()
                .AsCached()
                .WithArguments(FoodCircleSettings)
                .When(context => context.ObjectType == typeof(FoodSpawner) || context.MemberType.IsInterface);

            Container
                .BindInterfacesAndSelfTo<GameFieldCircle>()
                .AsCached()
                .WithArguments(WallCircleSettings)
                .When(context => context.ObjectType == typeof(WallSpawner) || context.MemberType.IsInterface);

            Container.BindInstances(FoodSpawnerSettings, FoodSpawnHandlerSettings, AreaSpawnHandlerSettings);

            Container.BindMemoryPool<Transform, WallPool>()
                .WithInitialSize(250)
                .FromComponentInNewPrefab(WallPrefab)
                .UnderTransformGroup("Walls");

            Container.BindMemoryPool<List<Vector2Int>, ListPool<Vector2Int>>();
            Container.BindMemoryPool<List<int?>, ListPool<int?>>();

            Container.Bind(typeof(IAreaSpawnHandler), typeof(IInitializable)).To<AreaSpawnHandler>().AsSingle();

            Container.Bind(typeof(IFieldSpawnHandler), typeof(IInitializable)).To<FieldSpawnHandler>().AsSingle();

            Container.BindInterfacesAndSelfTo<FoodSpawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<WallSpawner>().AsSingle();
        }
    }
}
