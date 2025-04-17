using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace EdCon.MiniGameTemplate.HUD
{
    public class HUDCustomizationService : MonoBehaviour
    {
        [SerializeField] private GameObject _highlighter;
        [SerializeField] private Slider _scaleSlider;
        [SerializeField] private Slider _opacitySlider;
        
        private CustomizableHUDElement _selectedHUDElement;
            
        public static HUDCustomizationService Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
        }
        
        private void Start()
        {
            _selectedHUDElement = null;
            var allHudElements = FindObjectsOfType(typeof(CustomizableHUDElement));
            var customizableElements = allHudElements.OfType<ICustomizable>().Cast<CustomizableHUDElement>().ToArray();
            Debug.Log(customizableElements.Length);
            
            _scaleSlider.onValueChanged.AddListener(value =>
            {
                if (_selectedHUDElement != null)
                {
                    float height = Mathf.Lerp(_selectedHUDElement.MinHeight, _selectedHUDElement.MaxHeight, value);
                    float width = Mathf.Lerp(_selectedHUDElement.MinWidth, _selectedHUDElement.MaxWidth, value);
                    _selectedHUDElement.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
                }
            });
            
            _opacitySlider.onValueChanged.AddListener(value =>
            {
                if (_selectedHUDElement != null)
                {
                    _selectedHUDElement.CurrentOpacity = Mathf.Lerp(_selectedHUDElement.MinOpacity, _selectedHUDElement.MaxOpacity, value);
                }
            });
        }

        public void ActivateCustomizationForElement(CustomizableHUDElement hudElement)
        {
            var highlighterTransform = _highlighter.GetComponent<RectTransform>();
            
            highlighterTransform.SetParent(hudElement.transform);
            highlighterTransform.anchorMax = Vector2.one;
            highlighterTransform.anchorMin = Vector2.zero;
            
            highlighterTransform.offsetMax = Vector2.zero;
            highlighterTransform.offsetMin = Vector2.zero;
            
            _highlighter.SetActive(true);
            _selectedHUDElement = hudElement;
            
            var currentElementWidth = _selectedHUDElement.GetComponent<RectTransform>().sizeDelta.x;
            
            _scaleSlider.value = (currentElementWidth - _selectedHUDElement.MinWidth) / (_selectedHUDElement.MaxWidth - _selectedHUDElement.MinWidth);
            _opacitySlider.value = (_selectedHUDElement.CurrentOpacity - _selectedHUDElement.MinOpacity) / (_selectedHUDElement.MaxOpacity - _selectedHUDElement.MinOpacity);
        }

        public void ChangeElementOpacity(float opacity)
        {
            
        }

        public void ChangeElementSize(float size)
        {
            
        }
    }
}