using FuneralServiceWH_MAUI.Data;
using FuneralServiceWH_MAUI.Models;
using FuneralServiceWH_MAUI.Utilities;
using System.Text;

namespace FuneralServiceWH_MAUI;

public partial class BuildingView : ContentPage
{
	private App thisApp;
	private List<Building> buildings;
    private Building EditBuilding,newBuilding;
	private List<Casket> caskets;
    
	public BuildingView()
	{
		InitializeComponent();
        thisApp = Application.Current as App;
        thisApp.needBuildingRefresh = true;
        buildings = new List<Building> { new Building { Id = 0, Name = "All Buildings", Address = "None",
            City = "None", Region = "None", PostalCode="A0A 0A0", RowVersion = null } };
        caskets = new List<Casket>();
        //_Listbuilding = new List<Building>();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        //Disable Add Button and clear the ListView until data arrives
        //btnAdd.IsEnabled = false;
        newBuilding = (Building)this.BindingContext;
        casketList.ItemsSource = null;

        if (thisApp.needBuildingRefresh)
        {
            //    await ShowBuildings();
            //    //Remember, this will also trigger ShowArtworks()
            //}
            //else
            //{
            await ShowCaskets();
        }
        //Enable the Add Button
        //btnAdd.IsEnabled = true;
    }

    //private async Task ShowBuildings()
    //{
    //    //Get the artTypes
    //    BuildingRepository atr = new BuildingRepository();
    //    try
    //    {
    //        thisApp.Allbuildings = await atr.GetBuildings();
    //        foreach (Building p in thisApp.Allbuildings.OrderBy(d => d.Name))
    //        {
    //            buildings.Add(p);
    //        }
    //        ddlBuildings.ItemsSource = buildings;
    //        thisApp.needBuildingRefresh = false;
    //        ddlBuildings.SelectedIndex = 0;
    //    }
    //    catch (ApiException apiEx)
    //    {
    //        var sb = new StringBuilder();
    //        sb.AppendLine("Errors:");
    //        foreach (var error in apiEx.Errors)
    //        {
    //            sb.AppendLine("-" + error);
    //        }
    //        await DisplayAlert("Problem Getting List of Buildings:", sb.ToString(), "Ok");
    //    }
    //    catch (Exception ex)
    //    {
    //        if (ex.InnerException != null)
    //        {
    //            if (ex.GetBaseException().Message.Contains("connection with the server"))
    //            {

    //                await DisplayAlert("Error", "No connection with the server. Check that the Web Service is running and available and then click the Refresh button.", "Ok");
    //            }
    //            else
    //            {
    //                await DisplayAlert("Error", "If the problem persists, please call your system administrator.", "Ok");
    //            }
    //        }
    //        else
    //        {
    //            if (ex.Message.Contains("NameResolutionFailure"))
    //            {
    //                await DisplayAlert("Internet Access Error ", "Cannot resolve the Uri: " + Jeeves.DBUri.ToString(), "Ok");
    //            }
    //            else
    //            {
    //                await DisplayAlert("General Error ", ex.Message, "Ok");
    //            }
    //        }
    //    }
    //}
    //private async void ddlArtTypes_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    await ShowCaskets();
    //}

    private async Task ShowCaskets()
    {
        Loading.IsRunning = true;
        //int? BuildingID = ((Building)ddlBuildings.SelectedItem)?.Id;
        CasketRepository r = new CasketRepository();
        try
        {
            
                caskets = await r.GetCasketsByBuilding(newBuilding.Id);
            
            casketList.ItemsSource = caskets;
            casketList.SelectedItem = null;

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
            await DisplayAlert("Error Getting Caskets:", sb.ToString(), "Ok");
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

    private async void CasketSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            var casket = (Casket)e.SelectedItem;
            var casketDetailPage = new casketDetailsPage();
            casketDetailPage.BindingContext = casket;
            await Navigation.PushAsync(casketDetailPage);
        }
    }

    private async void ddlBuildings_SelectedIndexChanged(object sender, EventArgs e)
    {
        await ShowCaskets();
    }

    private async void btnAdd_Clicked(object sender, EventArgs e)
    {
        Casket newcasket = new Casket();
        newcasket.BuildingId = 0; //Adding so show the prompt to select an art type
        newcasket.ModelID = 0;
        newcasket.ColorId = 0;
        newcasket.ManufacturerId = 0;
        newcasket.CreatedDate = DateTime.Today;
        var casketDetailPage = new CasketEditView();
        casketDetailPage.BindingContext = newcasket;
        
        await Navigation.PushAsync(casketDetailPage);
    }

    private async void btnEdit_Clicked(object sender, EventArgs e)
    {
        EditBuilding = (Building)this.BindingContext;
        
        var buildingDetailPage = new BuildingDetailsView();
        buildingDetailPage.BindingContext = EditBuilding;
        await Navigation.PushAsync(buildingDetailPage);
    }
}