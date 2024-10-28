using System.Collections.ObjectModel;
using System.Xml.Linq;
using Counter.Models;

namespace Counter.Views;

public partial class AllCountersPage : ContentPage
{
    private const string FilePath = "counters.xml";
    public ObservableCollection<CounterModel> Counters { get; set; }

    public AllCountersPage()
    {
        InitializeComponent();
        Counters = new ObservableCollection<CounterModel>();
        BindingContext = this;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadCounters();
    }

    private async void AddClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("addCounter");
    }

    private void LoadCounters()
    {
        var filePath = Path.Combine(FileSystem.AppDataDirectory, FilePath);

        if (!File.Exists(filePath))
        {
            return;
        }

        var xmlDoc = XDocument.Load(filePath);
        var counters = xmlDoc.Root.Elements("Counter")
            .Select(x => new CounterModel
            {
                Id = (int)x.Attribute("Id"),
                Name = (string)x.Element("Name"),
                Value = (int)x.Element("Value"),
                DefaultValue = (int)x.Element("DefaultValue"),
                Color = (string)x.Element("Color")
            });

        Counters.Clear();
        foreach (var counter in counters)
        {
            Counters.Add(counter);
        }
    }

    private void OnIncrementClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.BindingContext is CounterModel counter)
        {
            counter.Value++;
            SaveCounters();
        }
    }

    private void OnDecrementClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.BindingContext is CounterModel counter)
        {
            if (counter.Value > 0)
            {
                counter.Value--;
                SaveCounters();
            }
        }
    }

    private void SaveCounters()
    {
        var filePath = Path.Combine(FileSystem.AppDataDirectory, FilePath);
        var xDoc = new XDocument(new XElement("Counters"));

        foreach (var counter in Counters)
        {
            xDoc.Root.Add(new XElement("Counter",
                new XAttribute("Id", counter.Id),
                new XElement("Name", counter.Name),
                new XElement("Value", counter.Value),
                new XElement("DefaultValue", counter.DefaultValue),
                new XElement("Color", counter.Color)
            ));
        }

        xDoc.Save(filePath);
    }
}
