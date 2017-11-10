using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;


namespace QuickStart
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private string _inputText;
        private ICommand _onTranslateCommand;
        private ICommand _onCallDialNumberCommand;
        private bool _callButtonEnabled;
        private string _callButtonText;
        private string _translatedNumber;

        public MainPageViewModel()
        {
            _onTranslateCommand = new CommandHandler(() => OnTranslate(), true);
            _onCallDialNumberCommand = new CommandHandler(async () => await OnCallPhoneNumber(), true);
            CallButtonText = "Call";
            InputText = "1-855-XAMARIN";
        }


        public ICommand OnTranslateCommand { get { return _onTranslateCommand; } }

        public ICommand OnCallCommand { get { return _onCallDialNumberCommand; } }

        public string InputText
        {
            get { return _inputText; }
            set
            {
                _inputText = value;
                NotifyPropertyChanged(nameof(InputText));
            }
        }

        public bool CallButtonEnabled
        {
            get { return _callButtonEnabled; }
            set
            {
                _callButtonEnabled = value;
                NotifyPropertyChanged(nameof(CallButtonEnabled));
            }
        }

        public string CallButtonText
        {
            get { return _callButtonText; }
            set
            {
                _callButtonText = value;
                NotifyPropertyChanged(nameof(CallButtonText));
            }
        }
        

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnTranslate()
        {
            _translatedNumber = Core.PhonewordTranslator.ToNumber(InputText);
            if (!string.IsNullOrWhiteSpace(_translatedNumber))
            {
                CallButtonEnabled = true;
                CallButtonText = "Call " + _translatedNumber;
            }
            else
            {
                CallButtonEnabled = false;
                CallButtonText = "Call";
            }
        }

        private async Task OnCallPhoneNumber()
        {
            if (await Application.Current.MainPage.DisplayAlert(
                                "Dial a Number",
                                "Would you like to call " + _translatedNumber + "?",
                                "Yes",
                                "No"))
            {
                var dialer = DependencyService.Get<IDialer>();
                if (dialer != null)
                    dialer.Dial(_translatedNumber);
            }
        }
    }
}
