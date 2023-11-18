using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using static Zenject.CheatSheet;

namespace Spawners
{
    public class WallPool : MonoMemoryPool<Vector2, Transform>
    {
        protected override void Reinitialize(Vector2 p1, Transform item)
        {
            base.Reinitialize(p1, item);

            item.position = p1;
        }
    }
}
