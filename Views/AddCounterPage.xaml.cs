using Counter.Models;
using System.Xml.Linq;

namespace Counter.Views;

public partial class AddCounterPage : ContentPage
{
	private const string filePath = "counters.xml";

	public AddCounterPage()
	{
		InitializeComponent();
	}

    private async void OnAddButtonClicked(object sender, EventArgs e)
    {
		string name = NameEntry.Text;

		if (string.IsNullOrEmpty(name))
		{
			await DisplayAlert("B³¹d", "Podaj nazwê licznika", "Ok");
			return;
		}
  
		string stringValue = DefaultValueEntry.Text;

    		if(!int.TryParse(stringValue, out int value))
      		{
      			await DisplayAlert("B³¹d", "Błąd ustawiania wartości początkowej", "Ok");
			return;
		}

		string color = ColorPicker.SelectedItem?.ToString();

		if (string.IsNullOrEmpty(color))
		{
            await DisplayAlert("B³¹d", "Wybierz kolor", "Ok");
            return;
        }

		switch (color)
		{
			case "Czerwony":
				color = "red";
				break;
			case "¯ó³ty":
				color = "yellow";
				break;
			case "Niebieski":
				color = "blue";
				break;
			case "Zielony":
				color = "green";
				break;
			default:
				color = "black";
				break;
		}

		var counter = new CounterModel
		{
			Id = Guid.NewGuid().GetHashCode(),
			Name = name,
			Value = value,
			DefaultValue = value,
			Color = color
		};

		SaveCounter(counter);
		await DisplayAlert("Sukces", "Licznik zosta³ dodany.", "OK");
        await Navigation.PopAsync();
    }

	private void SaveCounter(CounterModel counter)
	{
		string fullPath = Path.Combine(FileSystem.AppDataDirectory, filePath);

		XDocument XDoc;

        if (File.Exists(fullPath))
        {
            XDoc = XDocument.Load(fullPath);
        }
        else
        {
            XDoc = new XDocument(new XElement("Counters"));
        }

		XDoc.Root.Add(new XElement("Counter",
			new XAttribute("Id", counter.Id),
			new XElement("Name", counter.Name),
			new XElement("Value", counter.Value),
			new XElement("DefaultValue", counter.DefaultValue),
			new XElement("Color", counter.Color)
		));

		XDoc.Save(fullPath);
    }
}
