using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "MonsterData", menuName = "staticData/Monster")]
    public class MonsterStaticData : ScriptableObject
    {
        public MonsterTypeId MonsterTypeId;

        public int MaxValue;
        public int MinValue;
        
        [Range(1,100)]
        public int Health;
        
        [Range(1,30)]
        public float Damage;
        
        [Range(0,5)]
        public float AttackCoolDown;
        
        [Range(0.5f,1)]
        public float Range;
        
        [Range(0.5f,1)]
        public float Cleavage;
        
        [Range(0f,10)]
        public float MoveSpeed;
        [Space]
        public GameObject Prefab;
    }
}