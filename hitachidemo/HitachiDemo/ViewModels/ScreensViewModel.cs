using HitachiDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HitachiDemo.ViewModels
{
    public class ScreensViewModel : ObservableObject
    {
        public ScreenItem SelectedScreen { get; set; }
        public List<ScreenItem> Screens { get; set; }

        public ScreensViewModel()
        {
            this.Screens = new List<ScreenItem>();
            this.GenereateScreens();
        }

        private void GenereateScreens()
        {
            for (int i = 0; i < 6; i++)
            {
                this.Screens.Add(new ScreenItem { Text = string.Format("Screen {0}", (i + 1)), ImageName = "bg.png" });
            }
        }
    }
}
