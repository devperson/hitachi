using HitachiDemo.Pages;
using HitachiDemo.ViewModels;
using System;
using Xamarin.Forms;

namespace HitachiDemo
{
	public class App
	{
        public static ViewModelLocator Locator = new ViewModelLocator();
		public static Page GetMainPage ()
		{
            
            return new NavigationPage(new HomePage()) { };
		}
	}
}

