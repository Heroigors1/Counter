using System.ComponentModel;

namespace Counter.Models
{
    public class CounterModel : INotifyPropertyChanged
    {
        private int _value;

        public int Id { get; set; }
        public string Name { get; set; }

        public int Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged(nameof(Value));
                }
            }
        }

        public int DefaultValue { get; set; }
        public string Color { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
