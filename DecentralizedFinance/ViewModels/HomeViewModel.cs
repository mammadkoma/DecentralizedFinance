using DecentralizedFinance.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DecentralizedFinance.ViewModels
{
    public class HomeViewModel
    {
        public DefiProjectViewModel LastDeFiProject { get; set; }
        public List<TokenViewModel> Tokens { get; set; }
    }
}
