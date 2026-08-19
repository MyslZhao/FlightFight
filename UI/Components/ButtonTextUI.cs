using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

namespace FlightFight.UI.Components
{
    /**
 * 按钮文本动画
 */
    internal class ButtonTextUI: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        #region 私有与序列字段

        [Tooltip("缩放比")]
        [SerializeField] private float _hoverScaleRatio = 0.9f;

        [Tooltip("动画时间")]
        [SerializeField] private float _animDuration = 0.2f;

        [Tooltip("横杠占位")]
        [SerializeField] private TMP_Text _LeftDashText;
        [SerializeField] private TMP_Text _RightDashText;

        private TMP_Text _TmpText;

        private float _originalFontSzie;

        #endregion

        #region 生命周期

        void Start()
        {
            _TmpText = GetComponentInChildren<TMP_Text>();
            if (_TmpText)
            {
                _originalFontSzie = _TmpText.fontSize;
            }

            // Another wtf
            if (_LeftDashText)
                _LeftDashText.DOFade(0, 0);
            if (_RightDashText)
                _RightDashText.DOFade(0, 0);

            Button _btn = GetComponent<Button>();
            if (_btn)
            {
                _btn.transition = Selectable.Transition.None; // Question?
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!_TmpText)
                return;

            DOTween.To(() => _TmpText.fontSize, x => _TmpText.fontSize = x,
                _originalFontSzie * _hoverScaleRatio, _animDuration)
                .SetEase(Ease.OutQuad); // Quad?

            if (_LeftDashText)
                _LeftDashText.DOFade(1, _animDuration).SetEase(Ease.OutQuad);
            if (_RightDashText)
                _RightDashText.DOFade(1, _animDuration).SetEase(Ease.OutQuad);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!_TmpText)
                return;

            DOTween.To(() => _TmpText.fontSize, x => _TmpText.fontSize = x,
                _originalFontSzie, _animDuration)
                .SetEase(Ease.OutQuad);

            if (_LeftDashText)
                _LeftDashText.DOFade(0, _animDuration).SetEase(Ease.OutQuad);
            if (_RightDashText)
                _RightDashText.DOFade(0, _animDuration).SetEase(Ease.OutQuad);
        }

        #endregion
    }
}
