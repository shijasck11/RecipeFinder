using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RecipesFinder
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavouritePage : ContentPage
    {
        private static Database database;
        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Recipe.db3"));
                }
                return database;
            }
        }

        public FavouritePage()
        {
            InitializeComponent();
        }

        [Obsolete]
        private void myFavourites_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        
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

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            myFavourites.ItemsSource = await App.Database.GetFavouriteRecipes();
        }

        private async void Remove_Clicked(object sender, EventArgs e)
        {
            var menu = sender as MenuItem;
            var deleteRecipe = menu.CommandParameter as Recipe;

            var result = await DisplayAlert("Remove", $"Remove {deleteRecipe.title} from favourites?", "Yes", "No");
            if (result)
            {
                await App.Database.RemoveFavourite(deleteRecipe);
                myFavourites.ItemsSource = await App.Database.GetFavouriteRecipes();
            }
        }

       
    }
}