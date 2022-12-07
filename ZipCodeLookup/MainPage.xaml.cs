using System.Globalization;
using CsvHelper;
using ZipCodeLookup.Model;

namespace ZipCodeLookup;

public partial class MainPage : ContentPage
{
    private List<CityZip> _lookupTable;


	public MainPage()
	{
		InitializeComponent();
       
    }

    private async Task LoadZipCodes()
    {
        // Load Zip Code Database from resource:
        using var stream = await FileSystem.OpenAppPackageFileAsync("zip_codes.csv");

        using var reader = new StreamReader(stream);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        _lookupTable = csv.GetRecords<CityZip>().ToList();
    }



    private void OnLookupClicked(object sender, EventArgs e)
    {
        CityZip cityFound;
        if(int.TryParse(ZipEntry.Text,out var zipCode))
        {
            cityFound = _lookupTable.FirstOrDefault(city => city.ZipCode == zipCode);
            CityResult.Text = cityFound?.PrimaryCity;
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadZipCodes();
    }
}

