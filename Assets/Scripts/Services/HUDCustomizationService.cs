using System.Collections.Generic;
using System.Linq;
using EdCon.MiniGameTemplate.HUD;
using EdCon.MiniGameTemplate.HUD.Popups;
using Services.SaveLoad;
using UnityEngine;

namespace Services
{
    public class HUDCustomizationService : MonoBehaviour
    {
        private const string JSON_KEY = "HUDElementsData";
        
        [SerializeField] private GameObject _highlighter;
        [SerializeField] private HUDPropertiesPanel _propertiesPanel;
        [SerializeField] private FloatingPopup _notificationPopup;

        private CustomizableHUDElement _selectedHUDElement;
        private List<CustomizableHUDElement> _allCustomizableElements;
        private SaveLoadService _saveLoadService;

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
            _saveLoadService = SaveLoadService.GetInstance();
            _propertiesPanel.gameObject.SetActive(false);
            
            _selectedHUDElement = null;
            
            _allCustomizableElements = FindObjectsOfType(typeof(CustomizableHUDElement)).Cast<CustomizableHUDElement>().ToList();
            
            _propertiesPanel.AddOnScaleChangedListener(OnScaleValueChanged);
            
            _propertiesPanel.AddOnOpacityChangedListener(OnOpacityValueChanged);
            
            LoadElementsData();
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
            _propertiesPanel.gameObject.SetActive(true);
        }

        public void SaveElementsChanges()
        {
            _propertiesPanel.gameObject.SetActive(false);
            _highlighter.SetActive(false);
            
            var dataCollection = new HUDElementDataList
            {
                Elements = _allCustomizableElements.Select(element => element.GetData()).ToArray()
            };
            
            _saveLoadService.Save(JSON_KEY, dataCollection);
            _notificationPopup.PlayAnimation();
        }

        private void LoadElementsData()
        {
            if (!_saveLoadService.HasSave(JSON_KEY))
            {
                ResetHUD();
                Debug.Log("No save file found");
                return;   
            }
            
            var dataCollection = _saveLoadService.Load<HUDElementDataList>(JSON_KEY);

            foreach (var data in dataCollection.Elements)
            {
                var element = _allCustomizableElements.FirstOrDefault(element => element.ElementType == data.ElementType);

                if (element != null)
                {
                    element.ApplySaveData(data);
                }
                else
                {
                    Debug.Log($"Element {data.ElementType} not found");
                }
            }
        }

        public void ResetHUD()
        {
            _propertiesPanel.gameObject.SetActive(false);
            _highlighter.SetActive(false);
            
            foreach (var element in _allCustomizableElements)
            {
                element.SetDefaultValues();
            }
        }

        [ContextMenu("Delete Elements Data")]
        public void DeleteElementsData()
        {
            SaveLoadService.GetInstance().Delete(JSON_KEY);
        }
    }
}