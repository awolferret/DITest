using GameInfasrtucture.UI.Services.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace GameInfasrtucture.UI.Elements
{
    public class OpenWindowButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private WindowId WindowId;
        
        private IWindowService _windowService;

        public void Construct(IWindowService windowService) => 
            _windowService = windowService;

        private void Awake() => 
            _button.onClick.AddListener(Open);

        private void OnDisable() => 
            _button.onClick.RemoveListener(Open);

        private void Open() => 
            _windowService.Open(WindowId);
    }
}