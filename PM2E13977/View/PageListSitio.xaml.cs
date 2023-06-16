using PM2E13977.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM2E13977.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageListSitio : ContentPage
    {
        private Sitio selectedSitio;
        public PageListSitio()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            list.ItemsSource = await App.Instancia.GetAll();
        }


        private async void atras_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.PageSitio());
        }

        private async void lista_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.PageListSitio());
          
        }

        private void list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedSitio = (Sitio)e.CurrentSelection.FirstOrDefault(); 
        }



        private async void Mapa_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.PageMap());
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            // DeleteSitio
            if (selectedSitio != null)
            {
                int rowsAffected = await App.Instancia.DeleteSitio(selectedSitio);
                if (rowsAffected > 0)
                {
                    await DisplayAlert("Eliminado", "El sitio ha sido eliminado correctamente.", "OK");
                    list.ItemsSource = await App.Instancia.GetAll();
                }
                else
                {
                    await DisplayAlert("Error", "No se pudo eliminar el sitio.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "Ningún sitio seleccionado.", "OK");
            }
        }

        private void btnVerM_Clicked(object sender, EventArgs e)
        {

        }

        private void atras_Clicked_1(object sender, EventArgs e)
        {

        }
    }
}