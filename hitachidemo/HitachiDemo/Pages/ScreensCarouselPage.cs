using HitachiDemo.Models;
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
        int _selectedPageIndex = 0;
        public ScreensCarouselPage(int selectedIndex)
        {
            _selectedPageIndex = selectedIndex;
            this.GeneratePages();

            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void GeneratePages()
        {
            for (int i = 0; i < App.Locator.ScreensViewModel.Screens.Count; i++)
            {
                var model = App.Locator.ScreensViewModel.Screens[i];
                this.Children.Add(this.CreatePage(model));                
            }
            this.CurrentPage = this.Children[_selectedPageIndex];
        }

        private ContentPage CreatePage(ScreenItem model)
        {            
            ContentPage page = new ContentPage();
            page.BindingContext = model;
            Grid layout = new Grid();
            layout.RowSpacing = 0;
            layout.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            layout.RowDefinitions.Add(new RowDefinition());
            layout.Children.Add(this.GetTopContent(), 0, 0);

            var bgImage = new Image() { Aspect = Aspect.AspectFill, VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };
            bgImage.SetBinding(Image.SourceProperty, new Binding("ImageName"));
            layout.Children.Add(bgImage, 0, 1);

            var lbl = new Label() { VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center, TextColor = Color.White, FontSize = 20 };
            lbl.SetBinding(Label.TextProperty, new Binding("Text"));
            layout.Children.Add(lbl, 0, 1);

            page.Content = layout;

            return page;
        }

        private Grid GetTopContent()
        {
            Grid topLayout = new Grid();
            topLayout.BackgroundColor = Color.Black;
            topLayout.HeightRequest = 50;
            topLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3) });
            topLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            topLayout.ColumnDefinitions.Add(new ColumnDefinition());
            topLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            topLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            topLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3) });

            var backBtn = new Button() { Image = Device.OnPlatform("backArrow.jpg", "backArrow.jpg", "Images/backArrow.jpg") };
            backBtn.Clicked += (s, e) =>
            {
                this.Navigation.PopAsync();
            };
            topLayout.Children.Add(backBtn, 1, 0);
            topLayout.Children.Add(new Image() { Source = ImageSource.FromFile(Device.OnPlatform("logo.png", "logo.png", "Images/logo.png")), WidthRequest = 15 }, 3, 0);
            var logoLabel = new Button { Text = "Hitachi Consulting", TextColor = Color.White, FontSize = 12, HorizontalOptions = LayoutOptions.Center };            
            topLayout.Children.Add(logoLabel, 4, 0);
            return topLayout;
        }
    }
}
