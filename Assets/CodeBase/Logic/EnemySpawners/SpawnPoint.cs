using CodeBase.Data;
using CodeBase.Enemy;
using CodeBase.Hero;
using CodeBase.Infrastructure.Factory;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Logic.EnemySpawners
{
  public class SpawnPoint : MonoBehaviour, ISavedProgress
  {
    public MonsterTypeId MonsterTypeId;
    public string Id { get; set; }

    public bool Slain;
    
    private string _id;
    private IGameFactory _factory;
    private EnemyDeath enemyDeath;

    public void Construct(IGameFactory gameFactory) => 
      _factory = gameFactory;

    public void LoadProgress(PlayerProgress progress)
    {
      if (progress.KillData.ClearedSpawners.Contains(_id))
        Slain = true;
      else
        Spawn();
    }

    private void Spawn()
    {
      GameObject monster = _factory.CreateMonster(MonsterTypeId, transform);
      enemyDeath = monster.GetComponent<EnemyDeath>();
      enemyDeath.Happened += Slay;
    }

    private void Slay()
    {
      if (enemyDeath!=null) 
        enemyDeath.Happened -= Slay;
      
      Slain = true;
    }

    public void UpdateProgress(PlayerProgress progress)
    {
      if (Slain)
        progress.KillData.ClearedSpawners.Add(_id);
    }
  }
}