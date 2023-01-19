using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RecipesFinder
{

    public partial class RecipeList : ContentPage
    {
        
        NetworkingManager nm = new NetworkingManager();
        RecipeCollection r;
        //FavouriteList FavouriteList = new FavouriteList();
        List<Recipe> favRecipes = new List<Recipe>();
        public RecipeList(RecipeCollection rec)
        {
            InitializeComponent();
            r = rec;
            myList.ItemsSource = r.R;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            {
                myList.ItemsSource = null;
                myList.ItemsSource = r.R;
            }
        }

        private async void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        [Obsolete]
        private async void Detail_Clicked(object sender, EventArgs e)
        {
            var menu = sender as MenuItem;

            var client = new HttpClient();
            string apiUrl = "https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/recipes/" + (menu.CommandParameter as Recipe).id.ToString() + "/information";
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(apiUrl),
                Headers =
                    {
                        { "X-RapidAPI-Key", "705ffbfdcdmshf55cc87916caa72p172908jsnb5a7a616a364" },
                        { "X-RapidAPI-Host", "spoonacular-recipe-food-nutrition-v1.p.rapidapi.com" },
                    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                int startIndex = body.IndexOf("sourceUrl") + "sourceUrl".Length + 3;
                int endIndex = body.IndexOf("\"", startIndex);

                string url = body.Substring(startIndex, endIndex - startIndex);
                //await DisplayAlert("Url", url, "ok");

                Device.OpenUri(new Uri(url));

            }
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            var menu = sender as MenuItem;
            Recipe favRecipe = menu.CommandParameter as Recipe;
            
            var favList =  await App.Database.GetFavouriteRecipes();
            foreach(var item in favList)
            {
                if(item.id == favRecipe.id)
                {
                    DisplayAlert("Failed", "This Recipe already exists in Favourites", "OK");
                    return;
                }
               
            }
            DisplayAlert("Save Button", favRecipe.title + " saved to favourites.", "OK");
            await App.Database.SaveFavourite(favRecipe);

        }

         

        [Obsolete]
        private void myList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            
        }

        //private async void Favourite_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushModalAsync(new FavouritePage());
        //}

        //private async Task<string> getRecipeDetail(int id)
        //{

        //    await DisplayAlert("00", "1", "ok");
        //    var client = new HttpClient();
        //    await DisplayAlert("0", "1", "ok");
        //    string apiUrl = "https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/recipes/" + id.ToString() + "/information";
        //    await DisplayAlert("1", "1", "ok");
        //    var request = new HttpRequestMessage
        //    {
        //        Method = HttpMethod.Get,
        //        RequestUri = new Uri(apiUrl),
        //        Headers =
        //            {
        //                { "X-RapidAPI-Key", "705ffbfdcdmshf55cc87916caa72p172908jsnb5a7a616a364" },
        //                { "X-RapidAPI-Host", "spoonacular-recipe-food-nutrition-v1.p.rapidapi.com" },
        //            },
        //    };
        //    using (var response = await client.SendAsync(request))
        //    {
        //        response.EnsureSuccessStatusCode();
        //        var body = await response.Content.ReadAsStringAsync();
        //        int startIndex = body.IndexOf("sourceUrl") + "sourceUrl".Length + 3;
        //        int endIndex = body.IndexOf("\"", startIndex);
        //        await DisplayAlert("Url", body.Substring(startIndex, endIndex - startIndex), "ok");

        //        return body.Substring(startIndex, endIndex - startIndex);
        //    }
        //}
    }
}