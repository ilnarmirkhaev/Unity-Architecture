using CodeBase.StaticData;

namespace CodeBase.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void LoadStaticData();
        MonsterStaticData ForMonster(MonsterTypeId typeId);
        LevelStaticData ForLevel(string sceneKey);
    }
}