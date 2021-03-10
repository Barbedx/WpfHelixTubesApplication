using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace WpfAppDatagridGroupingHeader
{
    public abstract class ViewModelBase
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName, object oldValue, object newValue)
        {
            RaisePropertyChanged(propertyName);
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetValue<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (object.Equals(field, value))
            {
                return false;
            }

            T val = field;
            field = value;
            OnPropertyChanged(propertyName, val, value);
            return true;
        }

        [Conditional("DEBUG")]
        private void VerifyProperty(string propertyName)
        {
            GetType().GetTypeInfo().GetDeclaredProperty(propertyName);
        }
    }
}