using Xamarin.Forms;

namespace QuickStart
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }       
    }
}
