﻿using System.Collections.Generic;
using CodeBase.Enemy;
using CodeBase.Hero;
using CodeBase.Infrastructure.Services;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
  public interface IGameFactory : IService
  {
    List<ISavedProgressReader> ProgressReaders { get; }
    List<ISavedProgress> ProgressWriters { get; }
    
    GameObject CreateHero(GameObject at);
    GameObject CreateHud();
    GameObject CreateMonster(MonsterTypeId typeId, Transform parent);
    void CreateSpawner(Vector3 at, string spawnerId, MonsterTypeId monsterTypeId);
    LootPiece CreateLoot();
    void Cleanup();
  }
}
