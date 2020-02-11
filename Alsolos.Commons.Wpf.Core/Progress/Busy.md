# Busy Indicator

## Using BusyContentControl with BusyViewModel

**XAML**

Merge BusyStyles.xaml in your ResourceDictionary

    <ResourceDictionary Source="/Alsolos.Commons.Wpf.Core;component/Progress/BusyStyles.xaml" />

Nest your control within BusyContentControl

    <progress:BusyContentControl>
        <StackPanel>
            ...
        </StackPanel>
    </progress:BusyContentControl>

**ViewModel**

Derive from `BusyViewModel`

    public class MyViewModel : BusyViewModel

Exceute code within Busy block

    using (BusyHelper.Enter("My progress message..."))
    {
        // Do something
    }
