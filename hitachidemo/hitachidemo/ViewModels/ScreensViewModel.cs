using HitachiDemo.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HitachiDemo.ViewModels
{
    public class ScreensViewModel : ObservableObject
    {

        ObservableCollection<ScreenItem> _screens;
        public ObservableCollection<ScreenItem> Screens
        {
            get
            {
                return _screens;
            }
            set
            {
                _screens = value;
                this.RaisePropertyChanged(p => p.Screens);
            }
        }

        

        public ScreensViewModel()
        {
            this.GenereateScreens();
        }

        private void GenereateScreens()
        {
            this.Screens = new ObservableCollection<ScreenItem>();

            this.Screens.Add(new ScreenItem { Text = "Your account page", ImageName = "sample.jpg" });
            this.Screens.Add(new ScreenItem { Text = "VIP Members Club", ImageName = "sample.jpg" });
            this.Screens.Add(new ScreenItem { Text = "Make reservations", ImageName = "sample.jpg" });
            this.Screens.Add(new ScreenItem { Text = "Your Favorites", ImageName = "sample.jpg" });
            this.Screens.Add(new ScreenItem { Text = "Messages", ImageName = "sample.jpg" });
            this.Screens.Add(new ScreenItem { Text = "Get a gift card", ImageName = "sample.jpg" });

        }
    }
}
