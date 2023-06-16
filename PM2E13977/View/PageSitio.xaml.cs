using Plugin.Media;
using SQLite;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace PM2E13977.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageSitio : ContentPage
    {
        Plugin.Media.Abstractions.MediaFile photo = null;
        public PageSitio()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            // Verificar el estado del permiso de ubicación
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

            // Intentar obtener la ubicación para verificar la disponibilidad de la geolocalización
            try
            {
                var location = await Geolocation.GetLocationAsync();
                // Obtener la ubicación actual
               

                if (location != null)
                {
                    // Mostrar la ubicación en el Label
                    latitud.Text = location.Latitude.ToString();
                    longitud.Text = location.Longitude.ToString();
                }
            }
            catch (FeatureNotSupportedException)
            {
                // Geolocalización no está soportada en el dispositivo
                await DisplayAlert("Error", "La geolocalización no está disponible en este dispositivo.", "OK");
                return;
            }
            catch (PermissionException)
            {
                // Permiso de ubicación no otorgado
                await DisplayAlert("Error", "El permiso de ubicación no se ha otorgado.", "OK");
                System.Environment.Exit(0);
                return;
            }
            catch (Exception)
            {   
                // Otro error al intentar obtener la ubicación
                await DisplayAlert("Error", "Ha ocurrido un error al obtener la ubicación. Por favor encienda el GPS en su Dispositivo", "OK");
                System.Environment.Exit(0);
                return;
            }

            // Si no se han producido excepciones, la geolocalización está disponible
            if (status != PermissionStatus.Granted)
            {
                // Mostrar alerta si el permiso de ubicación no se ha otorgado
                await DisplayAlert("GPS Inactivo", "Por favor, otorgue el permiso de ubicación para continuar.", "OK");
            }
        }
    


        public String Getimage64()
        {
            if (photo == null)
            {
                using(MemoryStream memory = new MemoryStream())
                {
                    Stream stream = photo.GetStream();
                    stream.CopyTo(memory);
                    byte[] fotobyte = memory.ToArray();

                    String Base64 = Convert.ToBase64String(fotobyte);

                    return Base64;

                }
            }
            return null;
        }

        public byte[] GetimageBytes()
        {
            if (photo == null)
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    Stream stream = photo.GetStream();
                    stream.CopyTo(memory);
                    byte[] fotobyte = memory.ToArray();

                    return fotobyte;

                }
            }
            return null;
        }



        private async void btnagregar_Clicked(object sender, EventArgs e)
        {
            // Verificar si los campos están vacíos
            if (foto.Source ==null || string.IsNullOrEmpty(latitud.Text) || string.IsNullOrEmpty(longitud.Text) || string.IsNullOrEmpty(descrip.Text))
            {
                await DisplayAlert("Error", "Todos los campos deben ser completados", "OK");
                return;
            }
            var sit = new Models.Sitio
            {
                latitud = latitud.Text,
                longitud = longitud.Text,
                descrip = descrip.Text,
                foto = GetimageBytes()
            };
           if (await App.Instancia.AddSitio(sit) > 0)
            {
              await  DisplayAlert("Aviso", "Sitio agregado", "OK");

            }
           else
                await DisplayAlert("Aviso", "a ocurrido un error", "OK");

        }

        private async void btnfoto_Clicked(object sender, EventArgs e)
        {
            photo = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "MYAPP",
                Name = "Foto.jpg",
                SaveToAlbum = true
            });
            if (photo != null)
            {
                foto.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
            }

            // Obtener la ubicación actual
            var location = await Geolocation.GetLocationAsync();

               
            }
        

        private async void btn_lista_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.PageListSitio());
        }

        private void btn_salir_Clicked(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}

