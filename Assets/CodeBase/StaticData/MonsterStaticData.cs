using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "MonsterData", menuName = "Static Data/Monster")]
    public class MonsterStaticData : ScriptableObject
    {
        public MonsterTypeId MonsterTypeId;

        [Range(1, 100)] public int Hp = 50;
        [Range(1f, 30f)] public float Damage = 10f;

        public int MaxLoot;
        public int MinLoot;

        [Range(0, 10)] public float MoveSpeed = 3;
        [Range(0.5f, 1f)] public float EffectiveDistance = 0.5f;
        [Range(0.5f, 1f)] public float Cleavage = 0.5f;

        public GameObject Prefab;
    }
}