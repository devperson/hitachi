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
        private bool showHome1 = true;
        ContentView middleContent = new ContentView();        

        public HomePage()
        {
            
            this.Initialize();
        }

        private void Initialize()
        {
            Grid mainLayout = new Grid();
            mainLayout.RowSpacing = 0;
            mainLayout.BackgroundColor = Color.White;
            mainLayout.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            mainLayout.RowDefinitions.Add(new RowDefinition());
            mainLayout.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });

            mainLayout.Children.Add(this.GetTopContent(), 0, 0);

            middleContent.Padding = new Thickness(5);
            middleContent.Content = this.GetMiddleContent();
            mainLayout.Children.Add(middleContent, 0, 1);

            var footer = new ContentView();
            footer.Padding = new Thickness(5, 0, 5, 5);
            footer.Content = this.GetFooterContent();
            mainLayout.Children.Add(footer, 0, 2);

            this.Content = mainLayout;
            
            NavigationPage.SetHasNavigationBar(this, false);
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

            topLayout.Children.Add(new Button() { Image = Device.OnPlatform("menu.png", "menu.png", "Images/menu.png"), WidthRequest = 35 }, 1, 0);
            topLayout.Children.Add(new Image() { Source = ImageSource.FromFile(Device.OnPlatform("logo.png", "logo.png", "Images/logo.png")), WidthRequest = 15 }, 3, 0);
            var logoLabel = new Button { Text = "Hitachi Consulting", TextColor = Color.White, FontSize = 12, HorizontalOptions = LayoutOptions.Center };
            logoLabel.Clicked += logoLabel_Clicked;
            topLayout.Children.Add(logoLabel, 4, 0);
            return topLayout;
        }

        private View GetMiddleContent()
        {
            if (showHome1)
                return new Image { Source = ImageSource.FromFile(Device.OnPlatform("bg.png", "bg.png", "Images/bg.png")), Aspect = Aspect.Fill, VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };
            else
            {
                Grid contentLayout = new Grid();
                contentLayout.RowSpacing = 0;
                contentLayout.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                contentLayout.RowDefinitions.Add(new RowDefinition());
                contentLayout.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                contentLayout.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                contentLayout.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });

                contentLayout.Children.Add(new ContentView()
                {
                    Padding = new Thickness(4, 2, 0, 4),
                    Content = new Label()
                    {
                        Text = "Welcome back, Nathan.",
                        TextColor = Color.Gray,
                        FontSize = 15
                    }
                }, 0, 0);
                contentLayout.Children.Add(new Image
                {
                    Source = ImageSource.FromFile(Device.OnPlatform("bg2.png", "bg2.png", "Images/bg2.png")),
                    Aspect = Aspect.AspectFill,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand
                }, 0, 1, 1, 5);

                contentLayout.Children.Add(new ContentView()
                {
                    Padding = new Thickness(7, 0, 5, 0),
                    Content = new Label()
                    {
                        Text = "Susan Anderson",
                        TextColor = Color.Black,
                        FontSize = 30
                    }
                }, 0, 2);
                contentLayout.Children.Add(new ContentView()
                {
                    Padding = new Thickness(7, 0, 5, 0),
                    Content = new Label()
                    {
                        Text = "is your VIP Liaison for the duration of your stay.",
                        TextColor = Color.Gray,
                        FontSize = 13
                    }
                }, 0, 3);                
                Grid servicesLayout = new Grid();
                
                servicesLayout.Padding = new Thickness(5);
                //servicesLayout.Padding = new Thickness(7, 0, 0, 0);
                servicesLayout.Children.AddHorizontal(new Button() { Text = "Call Sussan directly", TextColor = Color.Black, FontSize = 10});
                servicesLayout.Children.AddHorizontal(new Button() { Text = "Chat with Sussan", TextColor = Color.Black, FontSize = 10});
                servicesLayout.Children.AddHorizontal(new Button() { Text = "Schedule Activities", TextColor = Color.Black, FontSize = 10});
                contentLayout.Children.Add(servicesLayout, 0, 4);

                return contentLayout;
            }
        }

        private Grid GetFooterContent()
        {
            Grid footerLayout = new Grid();
            footerLayout.RowSpacing = 7;
            footerLayout.ColumnSpacing = 7;
            footerLayout.ColumnDefinitions.Add(new ColumnDefinition());
            footerLayout.ColumnDefinitions.Add(new ColumnDefinition());
            footerLayout.ColumnDefinitions.Add(new ColumnDefinition());
            footerLayout.RowDefinitions.Add(new RowDefinition());
            footerLayout.RowDefinitions.Add(new RowDefinition());
            footerLayout.HeightRequest = 200;
            footerLayout.Children.Add(this.CreateFooterButton("View your account"), 0, 1, 0, 1);
            footerLayout.Children.Add(this.CreateFooterButton("VIP Members Club"), 1, 2, 0, 1);
            footerLayout.Children.Add(this.CreateFooterButton("Make reservations"), 2, 3, 0, 1);
            footerLayout.Children.Add(this.CreateFooterButton("Your Favorites"), 0, 1, 1, 2);
            footerLayout.Children.Add(this.CreateFooterButton("Messages"), 1, 2, 1, 2);
            footerLayout.Children.Add(this.CreateFooterButton("Get a gift card"), 2, 3, 1, 2);
            return footerLayout;
        }


        private Grid GetPopupContent()
        {
            Grid layout = new Grid();
            layout.RowDefinitions.Add(new RowDefinition());
            layout.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            layout.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            layout.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });

            layout.Children.Add(new Image
            {
                Source = ImageSource.FromFile(Device.OnPlatform("popup.png", "popup.png", "Images/popup.png")),
                Aspect = Aspect.AspectFill,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            }, 0, 1, 0, 4);
            layout.Children.Add(new ContentView()
            {
                Padding = new Thickness(10, 5, 5, 5),
                Content = new Label()
                {
                    Text = "Welcome back to the Hitachi Casino, Nathan.",
                    TextColor = Color.White,
                    FontSize = 25
                }
            }, 0, 1);
            layout.Children.Add(new ContentView()
            {
                Padding = new Thickness(10,5,5,10),
                Content = new Label()
                {
                    Text = "Please proceed to the VIP services desk, located to the left as you enter the lobby.",
                    TextColor = Color.White,
                    FontSize = 14
                }
            }, 0, 2);
            layout.Children.Add(new ContentView()
            {
                Padding = new Thickness(10, 5, 5, 10),
                HorizontalOptions = LayoutOptions.Start,
                Content = new Button()
                {
                    Text = "Find more VIP Benefits",
                    FontSize = 13,
                    TextColor = Color.White                    
                }
            }, 0, 3);       

            return layout;
        }

        private void logoLabel_Clicked(object sender, EventArgs e)
        {
            showHome1 = !showHome1;
            middleContent.Content = this.GetMiddleContent();
        }

        private View CreateFooterButton(string text)
        {            
            return new Button { Text = text, FontSize=12, TextColor = Color.White, BackgroundColor = Color.Red };            
        }
    }
}
