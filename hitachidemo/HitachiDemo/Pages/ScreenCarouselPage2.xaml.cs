using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HitachiDemo.Pages
{
    public partial class ScreensCarouselPage2
    {        
        public ScreensCarouselPage2(int indx)
        {            
            InitializeComponent();
            BindingContext = App.Locator.ScreensViewModel;
            carouselView.SetSelectedIndex(indx);
            NavigationPage.SetHasNavigationBar(this, false);            
        }        

        private void Back_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PopAsync();
        }
    }
}
