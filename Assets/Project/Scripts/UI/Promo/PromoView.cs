using System.Collections.Generic;
using RedPanda.Project.Data;
using RedPanda.Project.Interfaces;
using RedPanda.Project.Services.Interfaces;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace RedPanda.Project.UI
{
    public class PromoView : View
    {
        [SerializeField] private Text _currencyAmount;
        [SerializeField] private Transform _sectionsTransform;
        [SerializeField] private PromoCategory _promoCategoryChests;
        [SerializeField] private PromoCategory _promoCategoryCrystals;
        [SerializeField] private PromoCategory _promoCategorySpecials;
        
        private IPromoService _promoService;
        private IUserService _userService;
        private List<PromoCategory> _categoryView = new();
        
        private void Start()
        {
            _promoService = Container.Locate<IPromoService>();
            _userService = Container.Locate<IUserService>();
            _userService.CurrencyAmountChanged += CurrencyAmountChanged;
            PreparePromoContent();
        }

        private void PreparePromoContent()
        {
            UpdateCurrencyAmount();
            var promos = new List<IPromoModel>(_promoService.GetPromos());
            promos.Sort(new PromoModelComparer());
            var chestsPromos = new List<IPromoModel>();
            var crystalsPromos = new List<IPromoModel>();
            var SpecialsPromos = new List<IPromoModel>();

            foreach (var promo in promos)
            {
                switch (promo.Type)
                {
                    case PromoType.Chest:
                        chestsPromos.Add(promo);
                        break;
                    case PromoType.Crystal:
                        crystalsPromos.Add(promo);
                        break;
                    case PromoType.Special:
                        SpecialsPromos.Add(promo);
                        break;
                }
            }
            
            CreateCategoryWithCards(chestsPromos);
            CreateCategoryWithCards(crystalsPromos);
            CreateCategoryWithCards(SpecialsPromos);
        }

        private void CreateCategoryWithCards(List<IPromoModel> data)
        {
            if (data.Count > 0)
            {
                PromoCategory prefabToUse;
                switch (data[0].Type)
                {
                    case PromoType.Chest:
                        prefabToUse = _promoCategoryChests;
                        break;
                    case PromoType.Crystal:
                        prefabToUse = _promoCategoryCrystals;
                        break;
                    case PromoType.Special:
                        prefabToUse = _promoCategorySpecials;
                        break;
                    default:
                        throw new InvalidOperationException("Unknown PromoType");
                }

                var section = Instantiate(prefabToUse, _sectionsTransform);
                section.LoadPromoDataToUI(Container, data);
                _categoryView.Add(section);
            }
        }
        
        private void CurrencyAmountChanged(int gems)
        {
            _currencyAmount.text = gems.ToString();
        }

        private void UpdateCurrencyAmount()
        {
            _currencyAmount.text = ($"{_userService.Currency}");
        }

        private void OnDestroy()
        {
            _userService.CurrencyAmountChanged -= CurrencyAmountChanged;
        }
    }
}