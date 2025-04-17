using Services;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EdCon.MiniGameTemplate.HUD
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class CustomizableHUDElement : MonoBehaviour, ICustomizable
    {
        [SerializeField] private ElementType _elementType;
        [SerializeField] private DefaultConfiguration _defaultConfiguration;

        [Header("Size Parameters")]
        [SerializeField] private float _minWidth;
        [SerializeField] private float _maxWidth;
        [SerializeField] private float _minHeight;
        [SerializeField] private float _maxHeight;

        [Header("Opacity Parameters")]
        [SerializeField] private float _minOpacity;
        [SerializeField] private float _maxOpacity;

        private HUDCustomizationService _hudCustomizationService;
        private RectTransform _rectTransform;
        private CanvasGroup _canvasGroup;
        private bool _isDragging;
        
        public ElementType ElementType => _elementType;
            
        public float MinWidth { get => _minWidth; set => _minWidth = value; }
        public float MaxWidth { get => _maxWidth; set => _maxWidth = value; }
        public float MinHeight { get => _minHeight; set => _minHeight = value; }
        public float MaxHeight { get => _maxHeight; set => _maxHeight = value; }
        public float MinOpacity { get => _minOpacity; set => _minOpacity = value; }

        public float MaxOpacity { get => _maxOpacity; set => _maxOpacity = value; }

        public float CurrentOpacity { get => _canvasGroup.alpha; set => _canvasGroup.alpha = value; }

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!_isDragging) return;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _rectTransform.parent as RectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out var localPoint
            );

            _rectTransform.localPosition = localPoint;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isDragging = true;
            HUDCustomizationService.Instance.ActivateCustomizationForElement(this);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isDragging = false;
        }

        public void SetScale(Vector2 scale)
        {
            _rectTransform.sizeDelta = scale;
        }

        public HUDElementData GetData()
        {
            return new HUDElementData
            {
                ElementType = _elementType,
                Position = _rectTransform.localPosition,
                Opacity = CurrentOpacity,
                Scale = _rectTransform.sizeDelta
            };
        }

        public void ApplySaveData(HUDElementData data)
        {
            _rectTransform.localPosition = data.Position;
            _rectTransform.sizeDelta = data.Scale;
            CurrentOpacity = data.Opacity;
        }

        public virtual void SetDefaultValues()
        {
            _rectTransform.anchoredPosition = _defaultConfiguration.DefaultPosition;
            _rectTransform.sizeDelta = _defaultConfiguration.DefaultScale;
            CurrentOpacity = _defaultConfiguration.DefaultOpacity;
        }
    }
}