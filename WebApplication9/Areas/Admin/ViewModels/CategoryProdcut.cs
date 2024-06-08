

using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Models;

namespace WebApplication9.ViewModels;

public class CategoryProdcut
{
    public Category category { get; set; }
    public Product product { get; set; }
    public List<SelectListItem> ListOfCategories { get; set; }
}
