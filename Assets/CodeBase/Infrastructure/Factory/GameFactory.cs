using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Enemy;
using CodeBase.Hero;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic;
using CodeBase.Logic.EnemySpawners;
using CodeBase.StaticData;
using CodeBase.UI.Elements;
using CodeBase.UI.Services.Windows;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Infrastructure.Factory
{
  public class GameFactory : IGameFactory
  {
    public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
    public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
    private GameObject HeroGameObject { get; set; }

    private readonly IAssetProvider _assets;
    private readonly IStaticDataService _staticData;
    private IRandomService _randomService;
    private IPersistentProgressService _progressService;
    private IWindowService _windowService;

    public GameFactory(IAssetProvider assets,
      IStaticDataService staticData,
      IRandomService randomService,
      IPersistentProgressService progressService,
      IWindowService windowService)
    {
      _assets = assets;
      _staticData = staticData;
      _randomService = randomService;
      _progressService = progressService;
      _windowService = windowService;
    }

    public async Task WarmUp()
    {
      await _assets.Load<GameObject>(AssetAddress.Loot);
      await _assets.Load<GameObject>(AssetAddress.Spawner);
    }
    public GameObject CreateHero(Vector3 at)
    {
      HeroGameObject = InstantiateRegistered(AssetAddress.HeroPath, at);
      return HeroGameObject;
    }

    public GameObject CreateHud()
    {
      var hud = InstantiateRegistered(AssetAddress.HudPath);
      hud.GetComponentInChildren<LootCounter>().Construct(_progressService.Progress.WorldData);

            foreach(var openWindowButton in hud.GetComponentsInChildren<OpenWindowButton>())
            {
                openWindowButton.Construct(_windowService);
            }
      
      return hud;
    }

    public async Task<GameObject> CreateMonster(MonsterTypeId typeId, Transform parent)
    {
      MonsterStaticData monsterData = _staticData.ForMonster(typeId);

      var prefab = await _assets.Load<GameObject>(monsterData.PrefabReference);
      
      GameObject monster = Object.Instantiate(prefab, parent.position, Quaternion.identity, parent);
      
      var health = monster.GetComponent<IHealth>();
      health.Current = monsterData.Hp;
      health.Max = monsterData.Hp;

      monster.GetComponent<ActorUI>().Construct(health);
      monster.GetComponent<AgentMoveToHero>().Construct(HeroGameObject.transform);
      monster.GetComponent<NavMeshAgent>().speed = monsterData.MoveSpeed;

      var lootSpawner = monster.GetComponentInChildren<LootSpawner>();
      lootSpawner.Construct(this, _randomService);
      lootSpawner.SetLoot(monsterData.MinLoot, monsterData.MaxLoot);

      Attack attack = monster.GetComponent<Attack>();
      attack.Construct(HeroGameObject.transform);
      attack.Damage = monsterData.Damage;
      attack.Cleavage = monsterData.Cleavage;
      attack.EffectiveDistance = monsterData.EffectiveDistance;
      
      monster.GetComponent<RotateToHero>()?.Construct(HeroGameObject.transform);
      
      return monster;
    }

    public async Task<LootPiece> CreateLoot()
    {
      var prefab = await _assets.Load<GameObject>(AssetAddress.Loot);
      var lootPiece = InstantiateRegistered(prefab).GetComponent<LootPiece>();
      lootPiece.Construct(_progressService.Progress.WorldData);
      
      return lootPiece;
    }

    public async Task CreateSpawner(Vector3 at, string spawnerId, MonsterTypeId monsterTypeId)
    {
      var prefab = await _assets.Load<GameObject>(AssetAddress.Spawner);
      
      var spawner = InstantiateRegistered(prefab, at).GetComponent<SpawnPoint>();
      spawner.Construct(this);
      spawner.Id = spawnerId;
      spawner.MonsterTypeId = monsterTypeId;
    }

    public void CleanUp()
    {
      ProgressReaders.Clear();
      ProgressWriters.Clear();
      _assets.CleanUp();
    }

    private void Register(ISavedProgressReader progressReader)
    {
      if (progressReader is ISavedProgress progressWriter)
      {
        ProgressWriters.Add(progressWriter);
      }
      
      ProgressReaders.Add(progressReader);
    }

    private GameObject InstantiateRegistered(GameObject prefab, Vector3 position)
    {
      GameObject heroGameObject = Object.Instantiate(prefab, position, Quaternion.identity);
      RegisterProgressWatchers(heroGameObject);
      return heroGameObject;
    }
    private GameObject InstantiateRegistered(string prefabPath, Vector3 position)
    {
      GameObject heroGameObject = _assets.Instantiate(prefabPath, position);
      RegisterProgressWatchers(heroGameObject);
      return heroGameObject;
    }

    private GameObject InstantiateRegistered(string prefabPath)
    {
      GameObject heroGameObject = _assets.Instantiate(prefabPath);
      RegisterProgressWatchers(heroGameObject);
      return heroGameObject;
    }
    private GameObject InstantiateRegistered(GameObject prefab)
    {
      GameObject heroGameObject = Object.Instantiate(prefab);
      RegisterProgressWatchers(heroGameObject);
      return heroGameObject;
    }

    private void RegisterProgressWatchers(GameObject gameObject)
    {
      foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
      {
        Register(progressReader);
      }
    }
  }
}