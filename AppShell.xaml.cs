using Counter.Views;

namespace Counter
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("addCounter", typeof(AddCounterPage));
        }
    }
}
