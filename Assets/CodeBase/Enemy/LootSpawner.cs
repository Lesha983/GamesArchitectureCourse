using System;
using CodeBase.Infrastructure.Factory;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class LootSpawner : MonoBehaviour
    {
        public EnemyDeath EnemyDeath;
        private IGameFactory _factory;
        
        public void Construct(IGameFactory factory)
        {
            _factory = factory;
        }
        
        private void OnEnable()
        {
            EnemyDeath.Happened += SpawnLoot;
        }

        private void OnDisable()
        {
            EnemyDeath.Happened -= SpawnLoot;
        }

        private void SpawnLoot()
        {
            // _factory
        }
    }
}