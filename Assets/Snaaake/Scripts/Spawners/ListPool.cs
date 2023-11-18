using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using static Zenject.CheatSheet;

namespace Spawners
{
    public class ListPool<T> : MemoryPool<List<T>>
    {
        protected override void Reinitialize(List<T> list)
        {
            list.Clear();
        }
    }

}
