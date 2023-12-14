using Grace.DependencyInjection;
using RedPanda.Project.Interfaces;
using RedPanda.Project.Services.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.U2D;

namespace RedPanda.Project.UI
{
    public class PromoCard : MonoBehaviour
    {
        [SerializeField] private Text _title;
        [SerializeField] private Text _price;
        [SerializeField] private Image _icon;
        [SerializeField] private SpriteAtlas _spriteAtlas;
        [SerializeField] private Button _purchaseButton;
        [SerializeField] private Vector3 _onPressScale;
        [SerializeField] private float _animationDuration;

        private IUserService _userService;
        private IPromoModel _data;

        private void Start()
        {
            _purchaseButton.onClick.AddListener(OnPurchaseClick);
        }

        public void PrepareCardDisplay(IExportLocatorScope container, IPromoModel data)
        {
            _userService = container.Locate<IUserService>();
            _data = data;
            _title.text = data.Title;
            _price.text = data.Cost.ToString();
            
            string spriteName = $"sprite_{data.Type}_{data.Rarity}";
            _icon.sprite = _spriteAtlas.GetSprite(spriteName);
        }

        private void OnPurchaseClick()
        {
            Vector3 originalScale = transform.localScale;
            transform.DOScale(_onPressScale, _animationDuration)
                .OnComplete(() => transform.DOScale(originalScale, _animationDuration));
            
            if (_userService.HasCurrency(_data.Cost))
            {
                _userService.ReduceCurrency(_data.Cost);
                Debug.Log($"Успешная покупка {_data.Title}");
            }
            else
            {
                Debug.LogError("Недостаточно средств");
            }
        }
    }
}
