using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace lib_utilities.Utilities
{
    [Serializable]
    public abstract class Bindable : INotifyPropertyChanged
    {
        public virtual event PropertyChangedEventHandler? PropertyChanged;

        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] String? propertyName = null)
        {
            try
            {
                if (object.Equals(storage, value))
                    return false;

                storage = value;
                OnPropertyChanged(propertyName!);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            try
            {
                var eventHandler = this.PropertyChanged;
                if (eventHandler != null)
                    eventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
            catch (Exception)
            {

            }
        }
    }
}