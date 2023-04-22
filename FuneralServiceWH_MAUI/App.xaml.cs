using FuneralServiceWH_MAUI.Models;
using Color = FuneralServiceWH_MAUI.Models.Color;

namespace FuneralServiceWH_MAUI;

public partial class App : Application
{
	public bool needBuildingRefresh;
	public List<Building> Allbuildings;
	public List<Model> AllModels;
	public List<Color> AllColors;
	public List<Manufacturer> AllManufacturers;
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}
