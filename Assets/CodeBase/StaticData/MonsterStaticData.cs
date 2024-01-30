using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(menuName = "StaticData/Monster", fileName = "MonsterData")]
    public class MonsterStaticData : ScriptableObject
    {
        public MonsterTypeId MonsterTypeId;
        
        [Range(1, 100)]
        public int Hp;
        [Range(1f, 30f)]
        public float Damage;
        [Range(1f, 15f)]
        public float MoveSpeed;

        [Range(0.5f, 1f)]
        public float Cleavage = 0.5f;

        [Range(0.5f, 1f)]
        public float EffectiveDistance = 0.5f;

        public GameObject Prefab;
    }
}