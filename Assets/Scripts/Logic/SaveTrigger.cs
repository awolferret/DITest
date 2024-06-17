using Character;
using GameInfasrtucture.Services;
using GameInfasrtucture.Services.PersistentProgress.SaveLoad;
using UnityEngine;

namespace Logic
{
    public class SaveTrigger : MonoBehaviour
    {
        [SerializeField] private BoxCollider _collider;

        private ISaveLoadService _saveLoadService;


        private void Awake() => _saveLoadService = AllServices.Container.Single<ISaveLoadService>();

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<CharacterMove>())
            {
                _saveLoadService.SaveProgress();
                gameObject.SetActive(false);
                Debug.Log("Saved");
            }
        }

        private void OnDrawGizmos()
        {
            if (!_collider)
                return;

            Gizmos.color = new Color32(30, 200, 30, 130);
            Gizmos.DrawCube(transform.position + _collider.center, _collider.size);
        }
    }
}