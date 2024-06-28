using UnityEngine;
using UnityEngine.Serialization;

namespace GameInfrastructure
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private Bootstrapper _bootstrapperPrefab;

        private void Awake()
        {
            Bootstrapper bootstrapper = FindObjectOfType<Bootstrapper>();

            if (!bootstrapper)
                Instantiate(_bootstrapperPrefab);

            Destroy(gameObject);
        }
    }
}