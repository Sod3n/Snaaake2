using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class ControllsInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Controlls>().AsSingle();
    }
}
