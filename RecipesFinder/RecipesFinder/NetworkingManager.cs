using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RecipesFinder
{
    public class NetworkingManager
    {
        HttpClient client = new HttpClient();
        //HttpClient client2 = new HttpClient();

        public async Task<Recipe[]> getRecipeList(string ingredient)
        {
            string apiUrl = "https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/recipes/findByIngredients?ingredients=" + ingredient + "&number=5&ignorePantry=true&ranking=1";
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(apiUrl),
                Headers =
                    {
                        { "X-RapidAPI-Host", "spoonacular-recipe-food-nutrition-v1.p.rapidapi.com" },
                        { "X-RapidAPI-Key", "705ffbfdcdmshf55cc87916caa72p172908jsnb5a7a616a364" },
                    },
            };

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Recipe[] rec = JsonConvert.DeserializeObject<Recipe[]>(body);
                return rec;
            }
        }

        //public async Task<string> getRecipeDetail(int id)
        //{
        //    string apiUrl = "https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/recipes/" + id + "/information";
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
        //    using (var response = await client2.SendAsync(request))
        //    {
        //        response.EnsureSuccessStatusCode();
        //        var body = await response.Content.ReadAsStringAsync();
        //        int startIndex = body.IndexOf("sourceUrl") + "sourceUrl".Length + 3;
        //        int endIndex = body.IndexOf("\"", startIndex);
        //        //var rec = JsonConvert.DeserializeObject<Dictionary<string, string>>(body);
        //        //rec.Count.ToString();
        //        //await DisplayAlert("Detail", "rec.Count.ToString()" + rec.Count.ToString(), "OK");

        //        return body.Substring(startIndex, endIndex - startIndex);

        //    }
        //}
    }
}
