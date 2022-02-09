using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using USA_Dinning.Classes;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using System.Threading;
using System.Globalization;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace USA_Dinning.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Home : Page
    {
        // Page Vars
        ObservableCollection<Location> AllLocations { get;set; }
        ObservableCollection<Location> locations { get;set; }
        bool EnableSorting { get;set; }

        public Home()
        {
            this.InitializeComponent();
            EnableSorting = false;
            AllLocations = new ObservableCollection<Location>();
            locations = new ObservableCollection<Location>();
            Thread t = new Thread(GetLocations);
            t.Start();
        }

        public async void GetLocations()
        {
            var assembly = this.GetType().GetTypeInfo().Assembly;
            var resource = assembly.GetManifestResourceStream("USA_Dinning.Assets.Locations.json");

            byte[] buffer = new byte[resource.Length];
            resource.Read(buffer, 0, (int)resource.Length);

            string response = System.Text.Encoding.Default.GetString(buffer);

            LocationsResponse json = JsonConvert.DeserializeObject<LocationsResponse>(response);

            foreach(var loc in json.locations)
            {
                string day = DateTime.Now.DayOfWeek.ToString(); 
                CultureInfo provider = new CultureInfo("en-US");
                DateTime timeOpen = new DateTime();
                DateTime timeClose = new DateTime();
                switch (DateTime.Now.DayOfWeek.ToString())
                {
                    case "Monday":
                        if (loc.Mon.Open == "Closed")
                        {
                            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                            {
                                AllLocations.Add(loc);
                                locations.Add(loc);
                            });
                            break;
                        }
                        timeOpen = DateTime.Parse(loc.Mon.Open);
                        timeClose = DateTime.Parse(loc.Mon.Close);
                        break;
                    case "Tuesday":
                        if (loc.Tue.Open == "Closed")
                        {
                            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                            {
                                AllLocations.Add(loc);
                                locations.Add(loc);
                            });
                            break;
                        }
                        timeOpen = DateTime.Parse(loc.Tue.Open);
                        timeClose = DateTime.Parse(loc.Tue.Close);
                        break;
                    case "Wednesday":
                        if (loc.Wed.Open == "Closed")
                        {
                            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                            {
                                AllLocations.Add(loc);
                                locations.Add(loc);
                            });
                            break;
                        }
                        timeOpen = DateTime.Parse(loc.Wed.Open);
                        timeClose = DateTime.Parse(loc.Wed.Close);
                        break;
                    case "Thursday":
                        if (loc.Thu.Open == "Closed")
                        {
                            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                            {
                                AllLocations.Add(loc);
                                locations.Add(loc);
                            });
                            break;
                        }
                        timeOpen = DateTime.Parse(loc.Thu.Open);
                        timeClose = DateTime.Parse(loc.Thu.Close);
                        break;
                    case "Friday":
                        if(loc.Fri.Open == "Closed")
                        {
                            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                            {
                                AllLocations.Add(loc);
                                locations.Add(loc);
                            });
                            break;
                        }
                        timeOpen = DateTime.Parse(loc.Fri.Open);
                        timeClose = DateTime.Parse(loc.Fri.Close);
                        break;
                    case "Saturday":
                        if (loc.Sat.Open == "Closed")
                        {
                            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                            {
                                AllLocations.Add(loc);
                                locations.Add(loc);
                            });
                            break;
                        }
                        timeOpen = DateTime.Parse(loc.Sat.Open);
                        timeClose = DateTime.Parse(loc.Sat.Close);
                        break;
                    case "Sunday":
                        if (loc.Sun.Open == "Closed")
                        {
                            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                            {
                                AllLocations.Add(loc);
                                locations.Add(loc);
                            });
                            break;
                        }
                        timeOpen = DateTime.Parse(loc.Sun.Open);
                        timeClose = DateTime.Parse(loc.Sun.Close);
                        break;
                }

                if(DateTime.Now.Hour > timeOpen.Hour && DateTime.Now.Minute > timeClose.Minute)
                {
                    loc.IsOpen = "Open";
                }

                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    AllLocations.Add(loc);
                    locations.Add(loc);
                });
            }
            EnableSorting = true;
        }

        private void LocationsSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(EnableSorting == false)
            {
                return;
            }
            EnableSorting = false;
            switch (LocationsSort.SelectedIndex)
            {
                case 0:
                    locations.Clear();
                    foreach(var loc in AllLocations)
                    {
                        locations.Add(loc);
                    }
                    break;
                case 1:
                    locations.Clear();
                    foreach (var loc in AllLocations)
                    {
                        if (loc.IsOpen == "Open")
                        {
                            locations.Add(loc);
                        }
                    }
                    break;
                case 2:
                    locations.Clear();
                    foreach (var loc in AllLocations)
                    {
                        if (loc.Group == "Coffee")
                        {
                            locations.Add(loc);
                        }
                    }
                    break;
                case 3:
                    locations.Clear();
                    foreach (var loc in AllLocations)
                    {
                        if (loc.Group == "SCFC")
                        {
                            locations.Add(loc);
                        }
                    }
                    break;
                case 4:
                    locations.Clear();
                    foreach (var loc in AllLocations)
                    {
                        if (loc.Group == "POD")
                        {
                            locations.Add(loc);
                        }
                    }
                    break;
                case 5:
                    locations.Clear();
                    break;
            }
            EnableSorting = true;
        }
    }
}
