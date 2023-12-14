using System.Collections.Generic;
using RedPanda.Project.Interfaces;

namespace RedPanda.Project
{
    public class PromoModelComparer : IComparer<IPromoModel>
    {
        public int Compare(IPromoModel x, IPromoModel y)
        {
            int typeComparison = x.Type.CompareTo(y.Type);
            if (typeComparison != 0)
            {
                return typeComparison;
            }
            
            return y.Rarity.CompareTo(x.Rarity);
        }
    }
}
