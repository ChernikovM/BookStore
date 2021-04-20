using BookStore.DataAccessLayer.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.BusinessLogicLayer.Models.RequestModels.Order
{
    public class PaymentSessionRequestModel
    {
        public List<Item> Items { get; set; }

        public string SuccessUrl { get; set; }

        public string CancelUrl { get; set; }

        [EnumDataType(typeof(Enums.CurrencyType))]
        public int Currency { get; set; }

    }
}
