using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Media;
using Xamarin.Forms.Xaml;

namespace PM2E13977
{
    public partial class MainPage : ContentPage
    {
        Plugin.Media.Abstractions.MediaFile photo = null;
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }

        private void btn_agregar_Clicked(object sender, EventArgs e)
        {

        }

        private void btn_lista_Clicked(object sender, EventArgs e)
        {

        }

        private void btn_salir_Clicked(object sender, EventArgs e)
        {

        }

        private async void btnfoto_Clicked(object sender, EventArgs e)
        {
            photo = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "MYAPP",
                Name = "Foto.jpg",
                SaveToAlbum = true
            });
            if(photo != null )
            {
                foto.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
            }
        }
    }
}
