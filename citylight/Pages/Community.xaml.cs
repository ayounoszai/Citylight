using System;
using System.Collections.Generic;
using citylight.Model;
using Xamarin.Forms;

namespace citylight
{
    public partial class CommunityPage : ContentPage
    {
        public CommunityPage()
        {
            InitializeComponent();

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            categoryPicker.ItemsSource = await CitylightGroup.GetGroupsAsync(Utils.Utils.COMMUNITY_ID);
            pointsListView.ItemsSource = await CitylightPoint.GetPointsAsync(Utils.Utils.COMMUNITY_ID);
        }

        protected async void categoryPicker_ItemSelected(object sender, EventArgs args)
        {
            try
            {
                var selectedItem = categoryPicker.SelectedItem as CitylightGroup;
                pointsListView.ItemsSource = await CitylightPoint.GetPointsAsync(int.Parse(selectedItem.properties.group_id));
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
    }
}
