using System;
using Xamarin.Forms;

namespace HitachiDemo
{
	public class App
	{
		public static Page GetMainPage ()
		{
            
            return new NavigationPage(new HomePage()) { };
		}
	}
}

