using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EdCon.MiniGameTemplate.HUD
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class CustomizableHUDElement : MonoBehaviour, ICustomizable
    {
        private bool _isDragging;
        
        [SerializeField] private CanvasGroup _canvasGroup;
        
        [Header("Size Parameters")]
        [SerializeField] private float _minWidth;
        [SerializeField] private float _maxWidth;
        [SerializeField] private float _minHeight;
        [SerializeField] private float _maxHeight;

        [Header("Opacity Parameter")]
        [SerializeField] private float _minOpacity;
        [SerializeField] private float _maxOpacity;
        
        public float MinWidth { get => _minWidth; set => _minWidth = value; }
        public float MaxWidth { get => _maxWidth; set => _maxWidth = value; }
        public float MinHeight { get => _minHeight; set => _minHeight = value; }
        public float MaxHeight { get => _maxHeight; set => _maxHeight = value; }
        public float MinOpacity { get => _minOpacity; set => _minOpacity = value; }
        public float MaxOpacity { get => _maxOpacity; set => _maxOpacity = value; }
        
        public float CurrentOpacity { get => _canvasGroup.alpha; set => _canvasGroup.alpha = value; }

        public virtual void Select()
        {
            HUDCustomizationService.Instance.ActivateCustomizationForElement(this);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!_isDragging) return;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                GetComponent<RectTransform>().parent as RectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out var localPoint
            );

            GetComponent<RectTransform>().localPosition = localPoint;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isDragging = true;
            Select();
            
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isDragging = false;
        }
    }
}