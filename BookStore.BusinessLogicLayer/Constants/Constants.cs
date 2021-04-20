using System.ComponentModel;

namespace BookStore.BusinessLogicLayer.Constants
{
    public partial class Constants
    {
        public enum ErrorMessage
        {
            [Description("")]
            None = 0,

            [Description("Account was not found.")]
            AccountNotFound,

            [Description("Invalid token.")]
            InvalidToken,

            [Description("Invalid credentials.")]
            InvalidCredentials,

            [Description("Account is blocked.")]
            AccountBlocked,

            [Description("Email not confirmed.")]
            EmailNotConfirmed,

            [Description("Author was not found.")]
            AuthorNotFound,

            [Description("Printing Edition was not found.")]
            PrintingEditionNotFound,

            [Description("Invalid data.")]
            InvalidData,

            [Description("User was not found.")]
            UserNotFound,

            [Description("User cannot be blocked.")]
            UserCannotBeBlocked,

            [Description("Order was not found.")]
            OrderNotFound,

            [Description("OrderItem was not found.")]
            OrderItemNotFound,

            [Description("Payment was not found.")]
            PaymentNotFound,
        }
    }
}
