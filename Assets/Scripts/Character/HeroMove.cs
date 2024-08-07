using Data;
using GameInfrastructure;
using GameInfrastructure.Services;
using GameInfrastructure.Services.Input;
using GameInfrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Character
{
    public class HeroMove : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private CharacterController _controller;
        [SerializeField] private float _movementSpeed;

        private IInputService _inputService;
        private Camera _camera;

        private void Awake() => _inputService = AllServices.Container.Single<IInputService>();

        private void Start() => _camera = Camera.main;

        private void Update()
        {
            Vector3 movementVector = Vector3.zero;

            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector = _camera.transform.TransformDirection(_inputService.Axis);
                movementVector.y = 0;
                movementVector.Normalize();

                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;
            _controller.Move(movementVector * (_movementSpeed * Time.deltaTime));
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (CurrnetLevel() == progress.WorldData.PositionOnLevel.Level)
            {
                Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;
                if (savedPosition != null)
                    Warp(warpTo: savedPosition);
            }
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.WorldData.PositionOnLevel =
                new PositionOnLevel(CurrnetLevel(), transform.position.AsVectorData());
        }

        private string CurrnetLevel() => SceneManager.GetActiveScene().name;

        private void Warp(Vector3Data warpTo)
        {
            _controller.enabled = false;
            transform.position = warpTo.AsUnityVector().AddY(_controller.height);
            _controller.enabled = true;
        }
    }
}