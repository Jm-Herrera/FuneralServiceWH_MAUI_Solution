using FuneralServiceWH_MAUI.Data;
using FuneralServiceWH_MAUI.Models;
using FuneralServiceWH_MAUI.Utilities;
using System.Security.AccessControl;
using System.Text;

namespace FuneralServiceWH_MAUI;

public partial class casketDetailsPage : ContentPage
{
	private App thisApp;
	private Casket casket;
	private List<Building> building;
	public casketDetailsPage()
	{
		InitializeComponent();
		thisApp = Application.Current as App;
		building = new List<Building>();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        casket = (Casket)this.BindingContext;
        if (casket.Id == 0)//Adding New
        {
            this.Title = "Add New Casket";
            //Since we do not have an ArtType yet, we want to get one ready
            Building noBuilding = new Building { Id = 0, Name = " Select a Building", Address = "None", City = "None", Region = "None", PostalCode = "A0A 0A0", RowVersion = null };
            building.Add(noBuilding);
            btnDelete.IsEnabled = false;
        }
        else
        {
            this.Title = "Casket Details";
            btnDelete.IsEnabled = true;
        }

        //FillBuilding();
    }
    //private void FillBuilding()
    //{

    //    foreach (Building d in thisApp.Allbuildings.OrderBy(d => d.Name))
    //    {
    //        building.Add(d);
    //    }
    //    //Fill the Drop Down Items
    //    ddlArtTypes.ItemsSource = building;
    //    //Set value to current or if inserting, set it to the prompt
    //    if (casket.BuildingId == 0)
    //    {
    //        ddlArtTypes.SelectedIndex = 0;
    //    }
    //    else if (casket.BuildingId >= 0)
    //    {
    //        ddlArtTypes.SelectedItem = thisApp.Allbuildings.FirstOrDefault(d => d.Id == casket.BuildingId);
    //    }
    //}

    //private async void SaveClicked(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        //If nothing is selected then we still want 0 for the foreign key
    //        //casket.BuildingId = (((Building)ddlArtTypes.SelectedItem)?.Id).GetValueOrDefault();

    //        CasketRepository r = new CasketRepository();
    //        if (casket.Id == 0)//Inserting a new record
    //        {
    //            await r.AddCasket(casket);
    //        }
    //        else
    //        {
    //            await r.UpdateCasket(casket);
    //        }
    //        await Navigation.PopAsync();

    //    }
    //    catch (ApiException apiEx)
    //    {
    //        var sb = new StringBuilder();
    //        sb.AppendLine("Errors:");
    //        foreach (var error in apiEx.Errors)
    //        {
    //            sb.AppendLine("-" + error);
    //        }
    //        await DisplayAlert("Problem Saving the Casket:", sb.ToString(), "Ok");
    //    }
    //    catch (Exception ex)
    //    {
    //        if (ex.GetBaseException().Message.Contains("connection with the server"))
    //        {
    //            await DisplayAlert("Error", "No connection with the server.", "Ok");
    //        }
    //        else
    //        {
    //            await DisplayAlert("Error", "Could not complete operation.", "Ok");
    //        }
    //    }
    //}

    private async void DeleteClicked(object sender, EventArgs e)
    {
        var answer = await DisplayAlert("Confirm Delete", "Are you certain that you want to delete " + casket.Model.Name + "?", "Yes", "No");
        if (answer == true)
        {
            try
            {
                CasketRepository er = new CasketRepository();
                await er.DeleteCasket(casket);
                await Navigation.PopAsync();
            }
            catch (ApiException apiEx)
            {
                var sb = new StringBuilder();
                sb.AppendLine("Errors:");
                foreach (var error in apiEx.Errors)
                {
                    sb.AppendLine("-" + error);
                }
                await DisplayAlert("Problem Deleting the Casket:", sb.ToString(), "Ok");
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

    private async void EditClicked(object sender, EventArgs e)
    {
        var casketEditView = new CasketEditView();
        casketEditView.BindingContext = casket;
        await Navigation.PushAsync(casketEditView);
    }
}