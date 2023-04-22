using FuneralServiceWH_MAUI.Data;
using FuneralServiceWH_MAUI.Models;
using FuneralServiceWH_MAUI.Utilities;
using System.Text;

namespace FuneralServiceWH_MAUI;

public partial class MainPage : ContentPage
{
	private App thisApp;
	private List<Building> buildings, ListBuildings;
	private Casket casket;
    private Building newBuilding;
	public MainPage()
	{

		InitializeComponent();
		thisApp = Application.Current as App;
		thisApp.needBuildingRefresh = true;
        buildings = new List<Building> { new Building { Id = 0, Name = "All Buildings", Address = "None",
            City = "None", Region = "None", PostalCode="A0A 0A0", RowVersion = null } };
		casket = new Casket();
        ListBuildings = new List<Building>();
        newBuilding = new Building();
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        //Disable Add Button and clear the ListView until data arrives
        //btnAdd.IsEnabled = false;
        artworkList.ItemsSource = null;

        if (thisApp.needBuildingRefresh)
        {
            await ShowBuildings();
            //Remember, this will also trigger ShowArtworks()
        }
        else
        {
            await ShowCaskets();
        }
        //Enable the Add Button
        //btnAdd.IsEnabled = true;
    }
    private async Task ShowBuildings()
    {
        //Get the artTypes
        BuildingRepository atr = new BuildingRepository();
        try
        {
            thisApp.Allbuildings = await atr.GetBuildings();
            foreach (Building p in thisApp.Allbuildings.OrderBy(d => d.Name))
            {
                buildings.Add(p);
            }
            ddlArtTypes.ItemsSource = buildings;
            thisApp.needBuildingRefresh = false;
            ddlArtTypes.SelectedIndex = 0;
        }
        catch (ApiException apiEx)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Errors:");
            foreach (var error in apiEx.Errors)
            {
                sb.AppendLine("-" + error);
            }
            await DisplayAlert("Problem Getting List of Buildings:", sb.ToString(), "Ok");
        }
        catch (Exception ex)
        {
            if (ex.InnerException != null)
            {
                if (ex.GetBaseException().Message.Contains("connection with the server"))
                {

                    await DisplayAlert("Error", "No connection with the server. Check that the Web Service is running and available and then click the Refresh button.", "Ok");
                }
                else
                {
                    await DisplayAlert("Error", "If the problem persists, please call your system administrator.", "Ok");
                }
            }
            else
            {
                if (ex.Message.Contains("NameResolutionFailure"))
                {
                    await DisplayAlert("Internet Access Error ", "Cannot resolve the Uri: " + Jeeves.DBUri.ToString(), "Ok");
                }
                else
                {
                    await DisplayAlert("General Error ", ex.Message, "Ok");
                }
            }
        }
    }

    private async void ddlArtTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        await ShowCaskets();
    }

    private async Task ShowCaskets()
    {
        Loading.IsRunning = true;
        int? BuildingID = ((Building)ddlArtTypes.SelectedItem)?.Id;
        BuildingRepository r = new BuildingRepository();
        try
        {
            if (BuildingID.GetValueOrDefault() > 0)
            {
                ListBuildings = await r.GetBuilding(BuildingID.GetValueOrDefault());
            }
            else
            {
                ListBuildings = await r.GetBuildings();
            }
            artworkList.ItemsSource = ListBuildings;
            artworkList.SelectedItem = null;

        }
        catch (ApiException apiEx)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Errors:");
            foreach (var error in apiEx.Errors)
            {
                sb.AppendLine("-" + error);
            }
            //artworkList.IsVisible = false;
            await DisplayAlert("Error Getting Buildings:", sb.ToString(), "Ok");
        }
        catch (Exception ex)
        {
            if (ex.InnerException != null)
            {
                if (ex.GetBaseException().Message.Contains("connection with the server"))
                {

                    await DisplayAlert("Error", "No connection with the server. Check that the Web Service is running and available and then click the Refresh button.", "Ok");
                }
                else
                {
                    await DisplayAlert("Error", "If the problem persists, please call your system administrator.", "Ok");
                }
            }
            else
            {
                await DisplayAlert("General Error", "If the problem persists, please call your system administrator.", "Ok");
            }
        }
        finally
        {
            Loading.IsRunning = false;
        }
    }

    private async void btnAddBuilding_Clicked(object sender, EventArgs e)
    {
        //Building newBuilding = new Building();
        newBuilding.Caskets = null;
        var buildingDetailView = new BuildingAddView();
        buildingDetailView.BindingContext = newBuilding;
        await Navigation.PushAsync(buildingDetailView);
    }

    //private async void btnAdd_Clicked(object sender, EventArgs e)
    //{
    //    Building newBuilding = new Building();
    //    newBuilding.Id = 0; //Adding so show the prompt to select an art type
    //    var buildingDetailPage = new BuildingView();
    //    buildingDetailPage.BindingContext = newBuilding;
    //    await Navigation.PushAsync(buildingDetailPage);
    //}

    private async void BuildingSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            var building2 = (Building)e.SelectedItem;
            var buildingDetailPage = new BuildingView();
            buildingDetailPage.BindingContext = building2;
            await Navigation.PushAsync(buildingDetailPage);
        }
    }

}

