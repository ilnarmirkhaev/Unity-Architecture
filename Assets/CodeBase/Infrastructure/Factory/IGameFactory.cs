using System.Collections.Generic;
using CodeBase.Enemy;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }

        GameObject CreateHero(Vector3 at);
        GameObject CreateHUD();
        GameObject CreateMonster(MonsterTypeId typeId, Transform parent);
        LootPiece CreateLoot();
        void CreateSpawner(Vector3 at, string spawnerId, MonsterTypeId monsterTypeId);
        
        void CleanUp();
    }
}