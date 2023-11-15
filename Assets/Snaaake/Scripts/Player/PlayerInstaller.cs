using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerInstaller : MonoInstaller
    {
        public Model.Movement MovementModel;
        public Model.Body BodyModel;
        public ObstaclesCheck.Settings CollisionSettings;
        public FoodCheck.Settings FoodCheckSettings;
        public override void InstallBindings()
        {
            BodyModel.BodyParts.Capacity = BodyModel.BodyPartsCapasity;

            Container.BindInstances(BodyModel, MovementModel, CollisionSettings, FoodCheckSettings);

            Container.BindInterfacesAndSelfTo<Input>().AsSingle();

            Container.BindInterfacesTo<HeadMovement>().AsSingle();
            Container.BindInterfacesTo<MovementDirectionChanger>().AsSingle();
            Container.BindInterfacesTo<BodyPartsMovement>().AsSingle();
            Container.BindInterfacesTo<ObstaclesCheck>().AsSingle();
            Container.BindInterfacesTo<FoodCheck>().AsSingle();
            Container.BindInterfacesTo<InvincibleControll>().AsSingle();
        }
    }
}
