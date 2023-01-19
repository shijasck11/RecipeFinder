using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RecipesFinder
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async private void Search_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new SearchRecipesPage());
        }

        async private void Favourite_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FavouritePage());
        }
    }
}
