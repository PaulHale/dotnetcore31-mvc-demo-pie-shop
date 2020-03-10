using System.Collections.Generic;
using PieShop.Models;

namespace PieShop.ViewModels
{

    public class PiesListViewModel
    {
        public IEnumerable<Pie> Pies { get; set; }
        public string CurrentCategory { set; get; }
    }

}