using UnityEngine;

namespace EdCon.MiniGameTemplate.HUD
{
    public class HUDCustomizationService : MonoBehaviour
    {
        [SerializeField] private GameObject _highlighter;
        [SerializeField] private HUDPropertiesPanel _propertiesPanel;

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
            
            _propertiesPanel.AddOnScaleChangedListener(OnScaleValueChanged);
            
            _propertiesPanel.AddOnOpacityChangedListener(OnOpacityValueChanged);
        }

        private void OnScaleValueChanged(float value)
        {
            if (_selectedHUDElement != null)
            {
                float height = Mathf.Lerp(_selectedHUDElement.MinHeight, _selectedHUDElement.MaxHeight, value);
                float width = Mathf.Lerp(_selectedHUDElement.MinWidth, _selectedHUDElement.MaxWidth, value);
                    
                _selectedHUDElement.SetScale(new Vector2(width, height));
                _propertiesPanel.SetScaleSliderValue(value);
            }
        }

        private void OnOpacityValueChanged(float value)
        {
            if (_selectedHUDElement != null)
            {
                _selectedHUDElement.CurrentOpacity = Mathf.Lerp(_selectedHUDElement.MinOpacity, _selectedHUDElement.MaxOpacity, value);
                _propertiesPanel.SetOpacitySliderValue(value);
            }
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
            var scaleSliderValue = (currentElementWidth - _selectedHUDElement.MinWidth) / (_selectedHUDElement.MaxWidth - _selectedHUDElement.MinWidth);
            _propertiesPanel.SetScaleSliderValue(scaleSliderValue);
            
            var opacitySliderValue = (_selectedHUDElement.CurrentOpacity - _selectedHUDElement.MinOpacity) / (_selectedHUDElement.MaxOpacity - _selectedHUDElement.MinOpacity);
            _propertiesPanel.SetOpacitySliderValue(opacitySliderValue);
        }
    }
}