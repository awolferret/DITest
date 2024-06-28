using System;
using Data;
using GameInfrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.UI;

namespace GameInfrastructure.UI
{
    public abstract class WindowBase : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        
        protected IPersistentProgressService ProgressService;
        protected PlayerProgress Progress => ProgressService.PlayerProgress;

        public void Construct(IPersistentProgressService progressService) => 
            ProgressService = progressService;

        private void Awake() =>
            OnAwake();

        private void Start()
        {
            Initialize();
            SubscribeUpdates();
        }

        protected virtual void OnAwake() => 
            _closeButton.onClick.AddListener(() => Destroy(gameObject));

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(() => Destroy(gameObject));
            CleanUp();
        }

        protected virtual void Initialize()
        {
        }

        protected virtual void SubscribeUpdates()
        {
        }

        protected virtual void CleanUp()
        {
            
        }
    }
}