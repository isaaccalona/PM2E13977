using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using static System.Net.Mime.MediaTypeNames;

namespace PM2E13977.View
{

    public interface IGpsService
    {
        bool IsGpsEnabled();
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageMap : ContentPage
    {
        String pathImagen;
        public PageMap()
        {
            InitializeComponent();
        }



        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var conectividad = Connectivity.NetworkAccess;
            var locl = CrossGeolocator.Current;
            if(conectividad == NetworkAccess.Internet)
            {
         
                if(locl != null) 
                {
                    locl.PositionChanged += Locl_PositionChanged;
                    if(!locl.IsListening)
                    {
                      await  locl.StartListeningAsync(TimeSpan.FromSeconds(10), 100);
                    }

                    var posicion = await locl.GetPositionAsync();
                    var mapcenter = new Xamarin.Forms.Maps.Position(posicion.Latitude, posicion.Longitude);
                    mapa.MoveToRegion(new MapSpan(mapcenter, 1, 1));
                }
            }
            else
            {
                var posicion = await locl.GetLastKnownLocationAsync();
                var mapcenter = new Xamarin.Forms.Maps.Position(posicion.Latitude, posicion.Longitude);
                mapa.MoveToRegion(new MapSpan(mapcenter, 1, 1));
            }
           
        }

        private void Locl_PositionChanged(object sender, PositionEventArgs e)
        {
            var mapcenter = new Xamarin.Forms.Maps.Position(e.Position.Latitude, e.Position.Longitude);
            mapa.MoveToRegion(new MapSpan(mapcenter, 1, 1));
        }

        private void atras_Clicked(object sender, EventArgs e)
        {

        }

        private async void btnCompartir_Clicked(object sender, EventArgs e)
        {

            base.OnAppearing();

            var conectividad = Connectivity.NetworkAccess;
            var locl = CrossGeolocator.Current;
            if (conectividad == NetworkAccess.Internet)
            {

                if (locl != null)
                {
                    locl.PositionChanged += Locl_PositionChanged;
                    if (!locl.IsListening)
                    {
                        await locl.StartListeningAsync(TimeSpan.FromSeconds(10), 100);
                    }

                    var posicion = await locl.GetPositionAsync();
                    var direccion = "";



                    var placemarks = await Geocoding.GetPlacemarksAsync(posicion.Latitude, posicion.Longitude);

                    var placemark = placemarks?.FirstOrDefault();

                    if (placemark != null)
                    {
                        direccion =
                       $"\nPais: {placemark.CountryName}\n";
                    }

                    try
                    {
                        await Share.RequestAsync(new ShareTextRequest
                        {
                            Title = "Compartiendo Ubicación \n",
                            Uri = "https://maps.google.com/?q=" + posicion.Latitude + "," + posicion.Longitude
                        });
                    }
                    catch { }

                }
            }
            else
            {
                var posicion = await locl.GetLastKnownLocationAsync();

            }

          
        }
    }
    }