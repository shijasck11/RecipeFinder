using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json;

namespace RecipesFinder
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchRecipesPage : ContentPage
    {
        private int ingredientCount = 1;
        NetworkingManager nm = new NetworkingManager();
        

        public SearchRecipesPage()
        {
            InitializeComponent();
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void More_Clicked(object sender, EventArgs e)
        {
            if (ingredientCount < 3)
            {
                ingredientCount++;
                switch (ingredientCount)
                {
                    case 2:
                        ingredient2.IsVisible = true;
                        break;
                    case 3:
                        ingredient3.IsVisible = true;
                        break;
                }
                
            }
            else
            {
                DisplayAlert("Error", "Maximun 3 ingredients", "ok");
            }

        }

        private async void Submit_Clicked(object sender, EventArgs e)
        {
            string ing = "";

            if (ingredient1.Text != null)
            {
                ing += ingredient1.Text.Trim();
            }
            if (ingredient2.Text != null)
            {
                if (ing.Length != 0) { ing += "," + ingredient2.Text.Trim(); }
                else { ing += ingredient2.Text.Trim(); }
            }
            if (ingredient3.Text != null)
            {
                if (ing.Length != 0) { ing += "," + ingredient3.Text.Trim(); }
                else { ing += ingredient3.Text.Trim(); }

            }

            if (ing.Length != 0)
            {
                RecipeCollection recCollection = new RecipeCollection();

                Recipe[] rec = await nm.getRecipeList(ing);

                for(int i = 0; i < rec.Length; i++)
                {
                    Recipe r = new Recipe
                    {
                        id = rec[i].id,
                        title = rec[i].title,
                        image = rec[i].image,
                        likes = rec[i].likes
                    };
                    recCollection.addNewRecipe(r);
                }
                
                await Navigation.PushModalAsync(new RecipeList(recCollection));
            }
            else
            {
                await DisplayAlert("Error", "Missing Information", "OK");
            }
        }



        //private async void PickImage_Clicked(object sender, EventArgs e)
        //{
        //    var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
        //    {
        //        Title = "Please pick a photo"
        //    });

        //    if (result != null)
        //    {
        //        var stream = await result.OpenReadAsync();
        //        resultImage.Source = ImageSource.FromStream(() => stream);
        //    }
        //}

        //private async void TakeImage_Clicked(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        var result = await MediaPicker.CapturePhotoAsync();

        //        if (result != null)
        //        {
        //            var stream = await result.OpenReadAsync();
        //            resultImage.Source = ImageSource.FromStream(() => stream);
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        await DisplayAlert("Error!","Error from the camera!" + ex,"ok");

        //    }


        //}

        //private void Upload_Clicked(object sender, EventArgs e)
        //{
        //    if (resultImage.Source != null)
        //    {
        //        title.Text = "Image Uploaded";
        //        title.TextColor = Color.Red;
        //        ImageRecognition();
        //    }
        //    else
        //    {
        //        DisplayAlert("Error!", "Please pick/take a food image", "ok");
        //    }

        //}

        //public void ImageRecognition()
        //{
        //    var credentials = new Amazon.CognitoIdentity.CognitoAWSCredentials(
        //        "us-east-2:452914fc-5187-4f22-930d-f8b203d2f2c7",   // Identity pool ID
        //        Amazon.RegionEndpoint.USEast2);



        //    Amazon.Rekognition.Model.Image image = new Amazon.Rekognition.Model.Image();
        //    DisplayAlert("HI!", resultImage.Source.ToString(), "ok");
        //    try
        //    {
        //        using (FileStream fs = new FileStream(resultImage.Source.ToString(), FileMode.Open, FileAccess.Read))
        //        {
        //            byte[] data = null;
        //            data = new byte[fs.Length];
        //            fs.Read(data, 0, (int)fs.Length);
        //            image.Bytes = new MemoryStream(data);
        //            DisplayAlert("GOOD!", "GOOD to load file " + resultImage.Source.ToString(), "ok");
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        DisplayAlert("Fail!", "Failed to load file " + resultImage.Source.ToString(), "ok");
        //        return;
        //    }

        //    AmazonRekognitionClient rekognitionClient = new AmazonRekognitionClient();

        //    DetectLabelsRequest request = new DetectLabelsRequest()
        //    {
        //        Image = image,
        //        MaxLabels = 10,
        //        MinConfidence = 77F
        //    };

        //    try
        //    {
        //        Task<DetectLabelsResponse> response = rekognitionClient.DetectLabelsAsync(request);
        //        DisplayAlert("Detected!", "Detected labels for " + resultImage.Source.ToString(), "ok");
        //        //foreach (Label label in detectLabelsResponse.Labels)
        //        //    Console.WriteLine("{0}: {1}", label.Name, label.Confidence);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }
        //}
    }
}