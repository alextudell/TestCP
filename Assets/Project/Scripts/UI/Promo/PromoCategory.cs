using System.Collections.Generic;
using Grace.DependencyInjection;
using RedPanda.Project.Data;
using RedPanda.Project.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace RedPanda.Project.UI
{
    public class PromoCategory : MonoBehaviour
    {
        [SerializeField] private Text _title;
        [SerializeField] private Transform _cardsTransform;
        [SerializeField] private PromoCard _commonPromoCardPrefab;
        [SerializeField] private PromoCard _rarePromoCardPrefab;
        [SerializeField] private PromoCard _epicPromoCardPrefab;

        private List<PromoCard> _promoCards = new();

        public void LoadPromoDataToUI(IExportLocatorScope container, List<IPromoModel> data)
        {
            _title.text = data[0].Type.ToString();

            foreach (var promoModel in data)
            {
                PromoCard prefabToUse;
                switch (promoModel.Rarity)
                {
                    case PromoRarity.Common:
                        prefabToUse = _commonPromoCardPrefab;
                        break;
                    case PromoRarity.Rare:
                        prefabToUse = _rarePromoCardPrefab;
                        break;
                    case PromoRarity.Epic:
                        prefabToUse = _epicPromoCardPrefab;
                        break;
                    default:
                        throw new InvalidOperationException("Unknown PromoRarity");
                }

                var card = Instantiate(prefabToUse, _cardsTransform);
                card.PrepareCardDisplay(container, promoModel);
                _promoCards.Add(card);
            }
        }
    }
}
