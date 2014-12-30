using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HitachiDemo
{
    public class HomePage : ContentPage
    {
        public HomePage()
        {
            this.Initialize();
        }

        private void Initialize()
        {
            Grid layout = new Grid();
            layout.BackgroundColor = Color.White;
            layout.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            layout.RowDefinitions.Add(new RowDefinition());
            layout.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });            

            Grid topBar = new Grid();
            topBar.BackgroundColor = Color.Black;
            topBar.HeightRequest = 60;
            topBar.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            topBar.ColumnDefinitions.Add(new ColumnDefinition());
            topBar.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            topBar.Children.Add(new Button() { Text = "Menu", TextColor = Color.White }, 0, 0);
            topBar.Children.Add(new Image() { Source = ImageSource.FromFile(Device.OnPlatform("logo.png", "logo.png", "Images/logo.png")) }, 2, 0);
            layout.Children.Add(topBar, 0, 0);

            ContentView middleContent = new ContentView();
            middleContent.Padding = new Thickness(5);
            middleContent.Content = new Image { Source = ImageSource.FromFile(Device.OnPlatform("bg.png", "bg.png", "Images/bg.png")) };
            layout.Children.Add(middleContent, 0, 1);

            Grid footerButtons = new Grid();            
            footerButtons.ColumnDefinitions.Add(new ColumnDefinition());
            footerButtons.ColumnDefinitions.Add(new ColumnDefinition());
            footerButtons.ColumnDefinitions.Add(new ColumnDefinition());
            footerButtons.RowDefinitions.Add(new RowDefinition());
            footerButtons.RowDefinitions.Add(new RowDefinition());
            footerButtons.HeightRequest = 200;
            footerButtons.Children.Add(this.CreateFooterButton("View your account"), 0, 1, 0, 1);
            footerButtons.Children.Add(this.CreateFooterButton("VIP Members Club"), 1, 2, 0, 1);
            footerButtons.Children.Add(this.CreateFooterButton("Make reservations"), 2, 3, 0, 1);
            footerButtons.Children.Add(this.CreateFooterButton("Your Favorites"), 0, 1, 1, 2);
            footerButtons.Children.Add(this.CreateFooterButton("Messages"), 1, 2, 1, 2);
            footerButtons.Children.Add(this.CreateFooterButton("Get a gift card"), 2, 3, 1, 2);
            layout.Children.Add(footerButtons, 0, 2);


            this.Content = layout;
        }

        private View CreateFooterButton(string text)
        {
            ContentView v = new ContentView();
            v.Padding = new Thickness(5);
            v.Content = new Button { Text = text, TextColor = Color.White, BackgroundColor = Color.Red };
            return v;
        }
    }
}
