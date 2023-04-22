using FuneralServiceWH_MAUI.Data;
using FuneralServiceWH_MAUI.Models;
using FuneralServiceWH_MAUI.Utilities;
using Microsoft.Maui.Controls;
using System.Text;
using Color = FuneralServiceWH_MAUI.Models.Color;

namespace FuneralServiceWH_MAUI;

public partial class CasketEditView : ContentPage
{
	private App thisApp;
	private Casket casket;
	private List<Building> buildings;
	private List<Model> models;
	private List<Color> colors;
	private List<Manufacturer> manufacturer;
	public CasketEditView()
	{
		InitializeComponent();
		thisApp = Application.Current as App;
		buildings = new List<Building>();
		models = new List<Model>();
		colors = new List<Color>();
		manufacturer = new List<Manufacturer>();

	}

    protected override  void OnAppearing()
    {
        base.OnAppearing();
		casket = (Casket)this.BindingContext;
        if (casket.Id == 0)
        {
            this.Title = "Add New Casket";
            Building noBuilding = new Building { Id = 0, Name = " Select a Building" };
            Model noModel = new Model { Id = 0, Name = " Select a Model" };
            Color noColor = new Color { Id = 0, Name = " Select a Color" };
            Manufacturer noManufacturer = new Manufacturer { Id = 0, Name = " Select a Manufacturer" };
            buildings.Add(noBuilding);
            models.Add(noModel);
            colors.Add(noColor);
            manufacturer.Add(noManufacturer);
            btnDelete.IsEnabled = false;
        }
        else
        {
            //this.Title = "Edit " + casket.Model.Name + " Details";
            btnDelete.IsEnabled = true;
        };


        FillBuilding();
        //FillModel();
        //FillColor();
        //FillManufacturer();
    }

    private void FillBuilding()
    {

        foreach (Building d in thisApp.Allbuildings.OrderBy(d => d.Name))
        {
            buildings.Add(d);
        }
        //Fill the Drop Down Items
        ddlBuilding.ItemsSource = buildings;
        //Set value to current or if inserting, set it to the prompt
        if (casket.BuildingId == 0)
        {
            ddlBuilding.SelectedIndex = 0;

        }
        else if (casket.BuildingId > 0)
        {
            ddlBuilding.SelectedItem = thisApp.Allbuildings.FirstOrDefault(d => d.Id == casket.BuildingId);

        }
    }

    //private void FillModel()
    //{

    //    foreach (Model d in thisApp.AllModels.OrderBy(d => d.Name))
    //    {
    //        models.Add(d);
    //    }
    //    //Fill the Drop Down Items
    //    ddlModel.ItemsSource = models;
    //    //Set value to current or if inserting, set it to the prompt
    //    if (casket.ModelID == 0)
    //    {
    //        ddlModel.SelectedIndex = 0;
    //    }
    //    else if (casket.ModelID >= 0)
    //    {
    //        ddlModel.SelectedItem = thisApp.AllModels.FirstOrDefault(d => d.Id == casket.ModelID);
    //    }
    //}

    //private void FillColor()
    //{

    //    foreach (Color d in thisApp.AllColors.OrderBy(d => d.Name))
    //    {
    //        colors.Add(d);
    //    }
    //    //Fill the Drop Down Items
    //    ddlColor.ItemsSource = colors;
    //    //Set value to current or if inserting, set it to the prompt
    //    if (casket.ColorId == 0)
    //    {
    //        ddlColor.SelectedIndex = 0;
    //    }
    //    else if (casket.ColorId >= 0)
    //    {
    //        ddlColor.SelectedItem = thisApp.AllColors.FirstOrDefault(d => d.Id == casket.ColorId);
    //    }
    //}

    //private void FillManufacturer()
    //{

    //    foreach (Manufacturer d in thisApp.AllManufacturers.OrderBy(d => d.Name))
    //    {
    //        manufacturer.Add(d);
    //    }
    //    //Fill the Drop Down Items
    //    ddlManufacturer.ItemsSource = manufacturer;
    //    //Set value to current or if inserting, set it to the prompt
    //    if (casket.ManufacturerId == 0)
    //    {
    //        ddlManufacturer.SelectedIndex = 0;
    //    }
    //    else if (casket.ManufacturerId >= 0)
    //    {
    //        ddlManufacturer.SelectedItem = thisApp.AllManufacturers.FirstOrDefault(d => d.Id == casket.ManufacturerId);
    //    }
    //}


    private async void SaveClicked(object sender, EventArgs e)
    {
        try
        {
            casket.BuildingId = (((Building)ddlBuilding.SelectedItem)?.Id).GetValueOrDefault();
            casket.ManufacturerId = (((Manufacturer)ddlManufacturer.SelectedItem)?.Id).GetValueOrDefault();
            casket.ColorId = (((Color)ddlColor.SelectedItem)?.Id).GetValueOrDefault();
            casket.ModelID = (((Model)ddlModel.SelectedItem)?.Id).GetValueOrDefault();

            CasketRepository b = new CasketRepository();

                if (casket.Id == 0)
                {
                    await b.AddCasket(casket);
                }
                else
                {
                    await b.UpdateCasket(casket);
            }
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
            await DisplayAlert("Problem Saving the Casket:", sb.ToString(), "Ok");
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


}