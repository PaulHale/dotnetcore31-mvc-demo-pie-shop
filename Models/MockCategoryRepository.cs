
using System.Collections.Generic;

namespace PieShop.Models {

    public class MockCategoryRepository : ICategoryRepository
    {
        public IEnumerable<Category> AllCategories => 
        new List<Category> {
            new Category{CategoryId=1, CategoryName="Fruit pies", Description="These are fruit pies"},
            new Category{CategoryId=2, CategoryName="Cheese cakes", Description="These are cheese cakes"},
            new Category{CategoryId=3, CategoryName="Seasonal pies", Description="These are seasonal pies"}
        };

    }

}