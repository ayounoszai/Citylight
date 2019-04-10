using System;
using System.Collections.Generic;
using citylight.Model;
using citylight.Utils;
using System.Linq;

using Xamarin.Forms;

namespace citylight.Pages
{
    public partial class DetailsPage : ContentPage
    {
        int PointId;
        public DetailsPage(int pointId)
        {
            InitializeComponent();
            PointId = pointId;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            ActivityIndicator.IsRunning = true;
            var p = await CitylightPointDetail.GetPointDetailAsync(PointId);
            if (p == null)
            {
                await DisplayAlert("Error accessing data", "We're having an issue retrieving the data; check back again shortly", "OK");
                ActivityIndicator.IsRunning = false;
                return;
            }


            pointNameLabel.Text = p.properties.point_name;
            address1Label.Text = p.properties.address1;
            if (string.IsNullOrWhiteSpace(p.properties.address2))
            {
                address2Label.IsVisible = false;
            }
            else
            {
                address2Label.Text = p.properties.address2;
            }
            CityStateZipLabel.Text = $"{p.properties.city_state} {p.properties.zip}";
            DescrWebView.Source = new HtmlWebViewSource
            {
                Html = p.properties.pdesc
            };

            if(p.citylight_images.Count > 0){
                defaultImage.Source = ImageSource.FromUri(new Uri(p.citylight_images.First().properties.image_filename));

                     //p.citylight_images.First();
            }

            ActivityIndicator.IsRunning = false;
        }
    }
}
