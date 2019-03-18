using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CAN
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CameraPage : ContentPage
    {
        public CameraPage()
        {
            InitializeComponent();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
                return;
            }
            try
            {
                var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                  //  Directory = "Sample",
                    Name = "test.jpg",
                    SaveToAlbum = true
                });

                if (file == null)
                    return;
                childimg.Source = ImageSource.FromStream(() => file.GetStream());
               // await DisplayAlert("File Location", (saveToGallery.IsToggled ? file.AlbumPath : file.Path), "OK");

                //image.Source = ImageSource.FromStream(() =>
                //{
                //    var stream = file.GetStream();
                //    file.Dispose();
                //    return stream;
                //});
            }
            catch (Exception ex)
            {
                // Xamarin.Insights.Report(ex);
                // await DisplayAlert("Uh oh", "Something went wrong, but don't worry we captured it in Xamarin Insights! Thanks.", "OK");
            }
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                return;
            }
            try
            {
               // Stream stream = null;
                var file = await CrossMedia.Current.PickPhotoAsync();


                if (file == null)
                    return;
                childimg.Source = ImageSource.FromStream(() => file.GetStream());
               // stream = file.GetStream();
                //file.Dispose();

               // image.Source = ImageSource.FromStream(() => stream);

            }
            catch //(Exception ex)
            {
                // Xamarin.Insights.Report(ex);
                // await DisplayAlert("Uh oh", "Something went wrong, but don't worry we captured it in Xamarin Insights! Thanks.", "OK");
            }
        }
    }
}