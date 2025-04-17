using System;
using UnityEngine;
using UnityEngine.UI;

namespace EdCon.MiniGameTemplate.HUD
{
    public class HUDPropertiesPanel : MonoBehaviour
    {
        [SerializeField] private Text _scalePercent;
        [SerializeField] private Text _opacityPercent;
        [SerializeField] private Slider _scaleSlider;
        [SerializeField] private Slider _opacitySlider;

        public void AddOnScaleChangedListener(Action<float> listener)
        {
            _scaleSlider.onValueChanged.AddListener(listener.Invoke);
        }
        
        public void AddOnOpacityChangedListener(Action<float> listener)
        {
            _opacitySlider.onValueChanged.AddListener(listener.Invoke);
        }

        public void SetScaleSliderValue(float value)
        {
            _scalePercent.text = value.ToString("0%");
            _scaleSlider.value = value;
        }

        public void SetOpacitySliderValue(float value)
        {
            _opacityPercent.text = value.ToString("0%");
            _opacitySlider.value = value;
        }
    }
}