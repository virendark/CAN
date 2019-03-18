using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Toasts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CAN
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TestPage : ContentPage
	{
        private MediaFile _mediaFile;
        private string URL { get; set; }
        public TestPage ()
		{
			InitializeComponent ();
           
        }

        private async void BtnSelectPic_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Error", "This is not support on your device.", "OK");
                return;
            }
            else
            {
                var mediaOption = new PickMediaOptions()
                {
                    PhotoSize = PhotoSize.Medium
                };
                _mediaFile = await CrossMedia.Current.PickPhotoAsync();
                if (_mediaFile == null) return;
                imageView.Source = ImageSource.FromStream(() => _mediaFile.GetStream());
                UploadedUrl.Text = "Image URL:";
            }
        }

        private async void BtnTakePic_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsTakePhotoSupported || !CrossMedia.Current.IsCameraAvailable)
            {
                //await CurrentPage.DisplayAlert("Option not available", "This option is not supported / available for this device", "OK");
                return;
            }
            MediaFile _media = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                Directory = "temp",
                SaveToAlbum = true,
                CompressionQuality = 75,
                CustomPhotoSize = 50,
                PhotoSize = PhotoSize.MaxWidthHeight,
                MaxWidthHeight = 2000,
                DefaultCamera = CameraDevice.Front
            });
            if (_media == null)
                return;

            

        }

        private async void BtnUpload_Clicked(object sender, EventArgs e)
        {
            if (_mediaFile == null)
            {
                await DisplayAlert("Error", "There was an error when trying to get your image.", "OK");
                return;
            }
            else
            {
              //  UploadImage(_mediaFile.GetStream());
            }
        }
        //private async void UploadImage(Stream stream)
        //{
         
        //    var account = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=ahsanblobaccount;AccountKey=fOvpvzb8jFL0pNfDWvz9n76DzLWSlZu4aw6ZLXMbDId15YYfox15UoKvWMmTCJ6vcNoyk5w+A==;EndpointSuffix=core.windows.net");
        //    var client = account.CreateCloudBlobClient();
        //    var container = client.GetContainerReference("images");
        //    await container.CreateIfNotExistsAsync();
        //    var name = Guid.NewGuid().ToString();
        //    var blockBlob = container.GetBlockBlobReference($"{name}.png");
        //    await blockBlob.UploadFromStreamAsync(stream);
        //    URL = blockBlob.Uri.OriginalString;
        //    UploadedUrl.Text = URL;
        //    NotBusy();
        //    await DisplayAlert("Uploaded", "Image uploaded to Blob Storage Successfully!", "OK");
        //}
    }
}