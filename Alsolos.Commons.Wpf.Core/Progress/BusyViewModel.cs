using Alsolos.Commons.Wpf.Core.Mvvm;

namespace Alsolos.Commons.Wpf.Core.Progress
{
    public abstract class BusyViewModel : ViewModel
    {
        protected BusyViewModel()
        {
            BusyHelper = new BusyHelper();
        }

        public BusyHelper BusyHelper { get; }
    }
}