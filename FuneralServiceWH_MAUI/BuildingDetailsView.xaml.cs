using FuneralServiceWH_MAUI.Data;
using FuneralServiceWH_MAUI.Models;
using FuneralServiceWH_MAUI.Utilities;
using System.Text;

namespace FuneralServiceWH_MAUI;

public partial class BuildingDetailsView : ContentPage
{
	private App thisApp;
	private Building building;
	public BuildingDetailsView()
	{
		InitializeComponent();
		thisApp = Application.Current as App;
        thisApp.needBuildingRefresh = true;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		building = (Building)this.BindingContext;

	}

	private async void SaveClicked(object sender, EventArgs e)
	{
		try
		{
			BuildingRepository b = new BuildingRepository();

			//if(building.Id == 0 )
			//{
			//	await b.AddBuilding(building);
			//}
			//else
			//{
				await b.UpdateBuilding(building);
			//}
			await Navigation.PushAsync(new MainPage());
		}
        catch (ApiException apiEx)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Errors:");
            foreach (var error in apiEx.Errors)
            {
                sb.AppendLine("-" + error);
            }
            await DisplayAlert("Problem Saving the Building:", sb.ToString(), "Ok");
        }
        catch (Exception ex)
        {
            if (ex.GetBaseException().Message.Contains("connection with the server"))
            {
                await DisplayAlert("Error", "No connection with the server.", "Ok");
            }
            else
            {
                await DisplayAlert("Error", "Could not complete operation.", "Ok");
            }
        }

    }
    private async void DeleteClicked(object sender, EventArgs e)
    {
        var answer = await DisplayAlert("Confirm Delete", "Are you certain that you want to delete " + building.Name + "?", "Yes", "No");
        if (answer == true)
        {
            try
            {
                BuildingRepository er = new BuildingRepository();
                await er.DeleteBuilding(building);
                await Navigation.PushAsync( new MainPage());
            }
            catch (ApiException apiEx)
            {
                var sb = new StringBuilder();
                sb.AppendLine("Errors:");
                foreach (var error in apiEx.Errors)
                {
                    sb.AppendLine("-" + error);
                }
                await DisplayAlert("Problem Deleting the Building:", sb.ToString(), "Ok");
            }
            catch (Exception ex)
            {
                if (ex.GetBaseException().Message.Contains("connection with the server"))
                {
                    await DisplayAlert("Error", "No connection with the server.", "Ok");
                }
                else
                {
                    await DisplayAlert("Error", "Could not complete operation.", "Ok");
                }
            }
        }
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }

}