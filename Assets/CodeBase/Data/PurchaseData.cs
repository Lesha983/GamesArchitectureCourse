using System;
using System.Collections.Generic;

namespace CodeBase.Data
{
    [Serializable]
    public class PurchaseData
    {
        public List<BoughtIAP> BoughtIAPs = new();

        public event Action Changed;

        public void AddPurchase(string productId)
        {
            var boughtIAP = ProductBy(productId);

            if (boughtIAP != null)
                boughtIAP.Count++;
            else
                BoughtIAPs.Add(new BoughtIAP{Id = productId, Count = 1});
            
            Changed?.Invoke();
        }

        private BoughtIAP ProductBy(string productId) => 
            BoughtIAPs.Find(x => x.Id == productId);
    }
}