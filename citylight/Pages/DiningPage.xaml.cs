using System;
using System.Collections.Generic;
using citylight.Model;
using Xamarin.Forms;
using citylight.Utils;
using citylight.Pages;

namespace citylight
{
    public partial class DiningPage : ContentPage
    {
        public List<CitylightGroup> categories;
        public DiningPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (categories == null)
            {
                ActivityIndicator.IsRunning = true;
                categories = await CitylightGroup.GetGroupsAsync(Utils.Utils.DINING_ID);
                pointsListView.ItemsSource = await CitylightPoint.GetPointsAsync(Utils.Utils.DINING_ID);
                categoryPicker.ItemsSource = categories;
                ActivityIndicator.IsRunning = false;
            }
        }

        

        protected async void categoryPicker_ItemSelected(object sender, EventArgs args)
        {
            try
            {
                var selectedItem = categoryPicker.SelectedItem as CitylightGroup;
                ActivityIndicator.IsRunning = true;
                pointsListView.ItemsSource = await CitylightPoint.GetPointsAsync(int.Parse(selectedItem.properties.group_id));
                ActivityIndicator.IsRunning = false;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        async void pointsListView_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var selectedItem = e.SelectedItem as Model.CitylightPoint;
            await Navigation.PushAsync(new DetailsPage(int.Parse(selectedItem.properties.point_id)));
        }
    }
}
