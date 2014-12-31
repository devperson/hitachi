using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HitachiDemo.Pages
{
    public class ScreensCarouselPage : CarouselPage
    {
        public ScreensCarouselPage()
        {
            this.GeneratePages();

            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void GeneratePages()
        {
            for (int i = 0; i < App.Locator.ScreensViewModel.Screens.Count; i++)
            {
                this.Children.Add(this.CreatePage(i));                
            }
        }

        private ContentPage CreatePage(int i)
        {            
            ContentPage page = new ContentPage();
            page.BindingContext = App.Locator.ScreensViewModel.Screens[i];
            Grid layout = new Grid();
            var bgImage = new Image() { Aspect = Aspect.AspectFill, VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };
            bgImage.SetBinding(Image.SourceProperty, new Binding("ImageName"));
            layout.Children.Add(bgImage, 0, 0);

            var lbl = new Label() { VerticalOptions = LayoutOptions.Center, TextColor = Color.Black };
            lbl.SetBinding(Label.TextProperty, new Binding("Text"));
            layout.Children.Add(lbl, 0, 0);

            page.Content = layout;

            return page;
        }
    }
}
