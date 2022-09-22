using CodeBase.Data;
using CodeBase.Infrastructure.Factory;
using CodeBase.Services.Randomizer;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class LootSpawner : MonoBehaviour
    {
        public EnemyDeath EnemyDeath;
        private IGameFactory _factory;
        private int _minLoot;
        private int _maxLoot;
        private IRandomService _random;

        public void Construct(IGameFactory factory, IRandomService random)
        {
            _factory = factory;
            _random = random;
        }

        private void Start()
        {
            EnemyDeath.Happened += SpawnLoot;
        }

        private void SpawnLoot()
        {
            var lootPiece = _factory.CreateLoot();
            lootPiece.transform.position = transform.position;

            lootPiece.Initialize(GenerateLoot());
        }

        private Loot GenerateLoot() =>
            new Loot()
            {
                Value = _random.Next(_minLoot, _maxLoot)
            };

        public void SetLoot(int min, int max)
        {
            _minLoot = min;
            _maxLoot = max;
        }
    }
}