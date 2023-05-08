using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;


namespace UnweWeatherApp
{
    public partial class MainPage : CarouselPage
    {
        public MainPage()
        {

            InitializeComponent();

            for (int i = 0; i < 5; i++)
            {
                Children.Add(new ForecastPage(i));
            }

        }

    }
}
