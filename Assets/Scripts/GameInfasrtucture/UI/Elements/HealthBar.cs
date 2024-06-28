using UnityEngine;
using UnityEngine.UI;

namespace GameInfrastructure.UI.Elements
{
    public class HealthBar : MonoBehaviour
    {
        public Image _image;

        public void SetValue(float current, float max) =>
            _image.fillAmount = current / max;
    }
}