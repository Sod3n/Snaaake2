using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerInstaller : MonoInstaller
    {
        public Settings PlayerSettings;

        public Movement.Settings MovementSettings;
        public BodyPartsContainer.Settings BodyPartsContainerSettings;
        public ObstaclesCheckOnMove.Settings CollisionSettings;
        public FoodCheckOnMove.Settings FoodCheckSettings;
        public Invincible.Settings InvincibleSettings;
        public PointsToCollor.Settings PointsToCollorSettings;
        public ReloadSceneOnCount.Settings ReloadSceneOnCountSettings;
        public override void InstallBindings()
        {
            Container.BindInstances(BodyPartsContainerSettings, MovementSettings, 
                CollisionSettings, FoodCheckSettings, InvincibleSettings,
                PointsToCollorSettings, ReloadSceneOnCountSettings
                );

            Container.BindInstances(PlayerSettings.Rigidbody);

            Container.BindInterfacesAndSelfTo<Input>().AsSingle();

            Container.BindInterfacesAndSelfTo<Movement>().AsSingle();
            Container.BindInterfacesAndSelfTo<Invincible>().AsSingle();
            Container.BindInterfacesAndSelfTo<BodyPartsContainer>().AsSingle();
            Container.BindInterfacesAndSelfTo<GrowthControll>().AsSingle();

            Container.BindInterfacesTo<PointsToCollor>().AsSingle();
            Container.BindInterfacesTo<BodyPartsMovement>().AsSingle();
            Container.BindInterfacesTo<MovementDirectionChanger>().AsSingle();
            Container.BindInterfacesTo<ObstaclesCheckOnMove>().AsSingle();

            Container.BindInterfacesAndSelfTo<FoodCheckOnMove>().AsSingle();

            Container.BindInterfacesTo<ShortenOnBumped>().AsSingle();
            Container.BindInterfacesTo<ReloadSceneOnCount>().AsSingle();
        }

        [Serializable]
        public class Settings
        {
            public Rigidbody2D Rigidbody;
        }
    }
}
