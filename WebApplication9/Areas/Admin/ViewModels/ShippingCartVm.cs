using Models.Models;

namespace WebApplication9.Areas.Admin.ViewModels
{
    public class ShippingCartVm
    {
        public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
        public OrderHeader OrderHeader { get; set; }

    }
}
