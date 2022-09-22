using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Data
{
    [Serializable]
    public class LootData
    {
        public int Collected;
        public List<(Vector3, int)> DroppedLoot = new List<(Vector3, int)>();
        public Action Changed;

        public void Collect(Loot loot)
        {
            Collected += loot.Value;
            Changed?.Invoke();
        }
    }
}