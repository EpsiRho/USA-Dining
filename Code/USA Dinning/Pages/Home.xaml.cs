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
using Windows.UI.Xaml.Media.Animation;

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
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
            EnableSorting = false;
            AllLocations = new ObservableCollection<Location>();
            locations = new ObservableCollection<Location>();
            Thread t = new Thread(GetLocations);
            t.Start();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Location loc = AnimationHelper.CurrentLocation;
            var gridViewItem = LocationsGrid.ContainerFromItem(loc) as GridViewItem;

            ConnectedAnimation animation =
                ConnectedAnimationService.GetForCurrentView().GetAnimation("forwardAnimation");
            if (animation != null)
            {
                animation.TryStart(gridViewItem);
            }
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
                            loc.DateGlance = $"Closed";
                            break;
                        }
                        timeOpen = DateTime.Parse(loc.Mon.Open);
                        timeClose = DateTime.Parse(loc.Mon.Close);
                        loc.DateGlance = $"{loc.Mon.Open} - {loc.Mon.Close}";
                        break;
                    case "Tuesday":
                        if (loc.Tue.Open == "Closed")
                        {
                            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                            {
                                AllLocations.Add(loc);
                                locations.Add(loc);
                            });
                            loc.DateGlance = $"Closed";
                            break;
                        }
                        timeOpen = DateTime.Parse(loc.Tue.Open);
                        timeClose = DateTime.Parse(loc.Tue.Close);
                        loc.DateGlance = $"{loc.Tue.Open} - {loc.Tue.Close}";
                        break;
                    case "Wednesday":
                        if (loc.Wed.Open == "Closed")
                        {
                            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                            {
                                AllLocations.Add(loc);
                                locations.Add(loc);
                            });
                            loc.DateGlance = $"Closed";
                            break;
                        }
                        timeOpen = DateTime.Parse(loc.Wed.Open);
                        timeClose = DateTime.Parse(loc.Wed.Close);
                        loc.DateGlance = $"{loc.Wed.Open} - {loc.Wed.Close}";
                        break;
                    case "Thursday":
                        if (loc.Thu.Open == "Closed")
                        {
                            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                            {
                                AllLocations.Add(loc);
                                locations.Add(loc);
                            });
                            loc.DateGlance = $"Closed";
                            break;
                        }
                        timeOpen = DateTime.Parse(loc.Thu.Open);
                        timeClose = DateTime.Parse(loc.Thu.Close);
                        loc.DateGlance = $"{loc.Thu.Open} - {loc.Thu.Close}";
                        break;
                    case "Friday":
                        if(loc.Fri.Open == "Closed")
                        {
                            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                            {
                                AllLocations.Add(loc);
                                locations.Add(loc);
                            });
                            loc.DateGlance = $"Closed";
                            break;
                        }
                        timeOpen = DateTime.Parse(loc.Fri.Open);
                        timeClose = DateTime.Parse(loc.Fri.Close);
                        loc.DateGlance = $"{loc.Fri.Open} - {loc.Fri.Close}";
                        break;
                    case "Saturday":
                        if (loc.Sat.Open == "Closed")
                        {
                            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                            {
                                AllLocations.Add(loc);
                                locations.Add(loc);
                            });
                            loc.DateGlance = $"Closed";
                            break;
                        }
                        timeOpen = DateTime.Parse(loc.Sat.Open);
                        timeClose = DateTime.Parse(loc.Sat.Close);
                        loc.DateGlance = $"{loc.Sat.Open} - {loc.Sat.Close}";
                        break;
                    case "Sunday":
                        if (loc.Sun.Open == "Closed")
                        {
                            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                            {
                                AllLocations.Add(loc);
                                locations.Add(loc);
                            });
                            loc.DateGlance = $"Closed";
                            break;
                        }
                        timeOpen = DateTime.Parse(loc.Sun.Open);
                        timeClose = DateTime.Parse(loc.Sun.Close);
                        loc.DateGlance = $"{loc.Sun.Open} - {loc.Sun.Close}";
                        break;
                }

                if(loc.DateGlance.Contains("12:00AM"))
                {
                    timeClose = timeClose.AddDays(1);
                }
               

                if(DateTime.Now > timeOpen && DateTime.Now < timeClose)
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

        private void LocationsGrid_ItemClick(object sender, ItemClickEventArgs e)
        {
            Location loc = (Location)e.ClickedItem;
            AnimationHelper.CurrentLocation = loc;
            var gridViewItem = LocationsGrid.ContainerFromItem(e.ClickedItem) as GridViewItem;

            ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("forwardAnimation", gridViewItem);

            if (loc.Name == "Fresh Food Company")
            {
                this.Frame.Navigate(typeof(FreshFoodCompanyPage), loc, new SuppressNavigationTransitionInfo());
            }
            else
            {
                this.Frame.Navigate(typeof(JsonMenuPage), loc, new SuppressNavigationTransitionInfo());
            }
        }
    }
}
