using DG.Tweening;
using UnityEngine;

namespace EdCon.MiniGameTemplate.HUD.Popups
{
    public class FloatingPopup : MonoBehaviour
    {
        [Header("Animation Parameters")]
        [SerializeField] private float _moveDistance = 200f;
        [SerializeField] private float _moveDuration = 0.5f;
        [SerializeField] private float _pauseDuration = 2f;
        [SerializeField] private Ease _moveEase;
        
        private Sequence _animationSequence;
        private RectTransform _rectTransform;
        private CanvasGroup _canvasGroup;
        private Vector3 _initialPosition;
        
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _initialPosition = Vector3.zero;
        }

        private void OnDestroy()
        {
            _animationSequence?.Kill();
        }
        
        public void PlayAnimation()
        {
            _animationSequence?.Kill();
            
            _rectTransform.anchoredPosition = _initialPosition;
            _canvasGroup.alpha = 1f;

            _animationSequence = DOTween.Sequence();

            var targetPosition = _initialPosition + new Vector3(0, -_moveDistance, 0);
            _animationSequence.Append(_rectTransform.DOAnchorPos(targetPosition, _moveDuration).SetEase(_moveEase));

            _animationSequence.AppendInterval(_pauseDuration);

            _animationSequence.Append(_rectTransform.DOAnchorPos(_initialPosition, _moveDuration).SetEase(_moveEase));
        }
        
    }
}