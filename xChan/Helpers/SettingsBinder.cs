using System.Windows.Data;

namespace xChan.Helpers
{
    public class SettingsBinder : Binding
    {
        public SettingsBinder()
        {
            Initialise();
        }

        public SettingsBinder(string path) : base(path)
        {
            Initialise();
        }

        private void Initialise()
        {
            Source = Properties.Settings.Default;
            Mode = BindingMode.TwoWay;
            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
        }
    }
}
