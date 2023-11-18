using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameRunner : MonoBehaviour
{
    public List<GameObjectContext> Contexts = new List<GameObjectContext>();
    private void Awake()
    {
        foreach(GameObjectContext installer in Contexts)
        {
            installer.Run();
        }
    }
}
