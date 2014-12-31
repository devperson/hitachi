using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HitachiDemo.ViewModels
{
    public class ViewModelLocator
    {
        HomeViewModel _homeViewModel;
        public HomeViewModel HomeViewModel
        {
            get
            {
                if (_homeViewModel == null)
                    _homeViewModel = new HomeViewModel();
                return _homeViewModel;
            }
        }

        ScreensViewModel _screensViewModel;
        public ScreensViewModel ScreensViewModel
        {
            get
            {
                if (_screensViewModel == null)
                    _screensViewModel = new ScreensViewModel();
                return _screensViewModel;
            }
        }
    }
}
