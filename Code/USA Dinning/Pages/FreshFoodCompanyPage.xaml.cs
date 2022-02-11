using Microsoft.Toolkit.Uwp.UI.Animations.Expressions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using USA_Dinning.Classes;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using System.Numerics;
using EF = Microsoft.Toolkit.Uwp.UI.Animations.Expressions.ExpressionFunctions;
using Microsoft.Toolkit.Uwp.UI;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace USA_Dinning.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FreshFoodCompanyPage : Page
    {
        // Page Vars
        ObservableCollection<string> DatesList { get; set; }
        ObservableCollection<CampusDish.MenuPeriod> MealTypeList { get; set; }
        ObservableCollection<GroupInfoList> ProductList { get; set; }
        bool CanLoadMenu { get; set; }
        bool NeedsStartup { get; set; }

        public FreshFoodCompanyPage()
        {
            this.InitializeComponent();
            DatesList = new ObservableCollection<string>();
            MealTypeList = new ObservableCollection<CampusDish.MenuPeriod>();
            ProductList = new ObservableCollection<GroupInfoList>();
            ProductsCVS.Source = ProductList;
            CanLoadMenu = true;
            NeedsStartup = true;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("forwardAnimation", ItemCard);
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            Location loc = e.Parameter as Location;
            NameTitle.Text = loc.Name;
            CardBackgroundImage.Source = new BitmapImage(new Uri(loc.BackgroundImage));
            BannerBackground.ImageSource = new BitmapImage(new Uri(loc.BackgroundImage));
            CardImage.ImageSource = new BitmapImage(new Uri(loc.BackgroundImage));
            IsOpenText.Text = loc.IsOpen;
            AddressText.Text = loc.Address;
            DateGlanceText.Text = loc.DateGlance;
            DescriptionTextBox.Text = "The Fresh Food Co. is our all-you-care-to-eat dining hall. Serving a rotating menu at ten stations, the Fresh Food Co.’s variety satisfies the needs of everyone at South Alabama.";
            DescriptionTextBox.Text += "\n\nThe Fresh Food Company is the next generation in Campus Dining. All - you - care - to - eat resident dining with restaurant atmosphere, which has something for everyone's taste. Breakfast offers made-to-order omelets, a selection of eggs, breakfast meats and breads, fresh fruit and cereals. Lunch and Dinner selections include pizza and pasta, burgers and fries, fruit and salad fixings, deli creations, international dishes, vegetarian and vegan options, soups, favorites such as barbecue, meat loaf and fried chicken, freshly baked breads, and a variety of desserts.";
            


                    ConnectedAnimation animation =
                ConnectedAnimationService.GetForCurrentView().GetAnimation("forwardAnimation");
            if (animation != null)
            {
                animation.TryStart(ItemCard);
            }

            Thread t = new Thread(LoadMenu);
            t.Start(new MenuLoadArgs() { date = "02/10/2022", period = ""});
        }

        public async void LoadMenu(object a)
        {
            CanLoadMenu = false;
            MenuLoadArgs args = a as MenuLoadArgs;
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                ProductsCVS.Source = new ObservableCollection<GroupInfoList>();
            });

            if (NeedsStartup)
            {
                var times = await CampusDish.CampusDishHandler.GetValidDates("7503");
                foreach (var time in times.AvailableDates)
                {
                    string str = $"{time.Month}/{time.Day}/{time.Year}";

                    await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                    {
                        DatesList.Add(str);
                    });
                }
                string select = $"{times.Now.Month}/{times.Now.Day}/{times.Now.Year}";
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    DateSelection.SelectedItem = select;
                    DateSelectionProgress.Visibility = Visibility.Collapsed;
                });
            }

            var menu = await CampusDish.CampusDishHandler.GetDailyMenu("7503", args.date, "", args.period);
            if (menu != null)
            {
                if (NeedsStartup)
                {
                    foreach (var type in menu.Menu.MenuPeriods)
                    {
                        DateTime StartTemp = DateTime.Parse(type.UtcMealPeriodStartTime);
                        DateTime EndTemp = DateTime.Parse(type.UtcMealPeriodEndTime);

                        if (StartTemp.Hour > 12)
                        {
                            type.TimeString = $"{StartTemp.ToString("hh:mm")}PM";
                        }
                        else
                        {
                            type.TimeString = $"{StartTemp.ToString("hh:mm")}AM";
                        }

                        if (EndTemp.Hour > 12)
                        {
                            type.TimeString += $" - {EndTemp.ToString("hh:mm")}PM";
                        }
                        else
                        {
                            type.TimeString += $" - {EndTemp.ToString("hh:mm")}AM";
                        }

                        await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                        {
                            MealTypeList.Add(type);
                        });
                    }
                    await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                    {
                        MealType.SelectedIndex = 0;
                        MealTypeProgress.Visibility = Visibility.Collapsed;
                    });
                }

                foreach(var item in menu.Menu.MenuProducts)
                {
                    item.StationName = menu.Menu.MenuStations.FirstOrDefault(o => o.StationId == item.StationId).Name;
                }

                var query = from item in menu.Menu.MenuProducts
                            group item by item.StationName into g
                            orderby g.Key
                            select new GroupInfoList(g) { Key = g.Key };

                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    ProductsCVS.Source = new ObservableCollection<GroupInfoList>(query);
                    MenuLoadProgress.Visibility = Visibility.Collapsed;
                    DateSelection.IsEnabled = true;
                    MealType.IsEnabled = true;
                });

                CanLoadMenu = true;
                NeedsStartup = false;
            }
        }

        // Move into own file later
        public class GroupInfoList : List<object>
        {
            public GroupInfoList(IEnumerable<object> items) : base(items)
            {
            }
            public object Key { get; set; }
        }



        CompositionPropertySet _props;
        CompositionPropertySet _scrollerPropertySet;
        Compositor _compositor;
        public async void SetupShyHeader()
        {
            // Retrieve the ScrollViewer that the GridView is using internally
            ScrollViewer scrollViewer = null;
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Low, () =>
            {
                Border border = VisualTreeHelper.GetChild(MenuItemsList, 0) as Border;
                scrollViewer = VisualTreeHelper.GetChild(border, 0) as ScrollViewer;
            });


            // Update the ZIndex of the header container so that the header is above the items when scrolling
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Low, () =>
            {
                var headerPresenter = (UIElement)VisualTreeHelper.GetParent((UIElement)MenuItemsList.Header);
                var headerContainer = (UIElement)VisualTreeHelper.GetParent(headerPresenter);
                Canvas.SetZIndex((UIElement)headerContainer, 1);
            });

            // Get the PropertySet that contains the scroll values from the ScrollViewer
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Low, () =>
            {
                _scrollerPropertySet = ElementCompositionPreview.GetScrollViewerManipulationPropertySet(scrollViewer);
            });
            _compositor = _scrollerPropertySet.Compositor;

            // Create a PropertySet that has values to be referenced in the ExpressionAnimations below
            _props = _compositor.CreatePropertySet();
            _props.InsertScalar("progress", 0);
            _props.InsertScalar("clampSize", 150);
            _props.InsertScalar("scaleFactor", 0.7f);

            // Get references to our property sets for use with ExpressionNodes
            var scrollingProperties = _scrollerPropertySet.GetSpecializedReference<ManipulationPropertySetReferenceNode>();
            var props = _props.GetReference();
            var progressNode = props.GetScalarProperty("progress");
            var clampSizeNode = props.GetScalarProperty("clampSize");
            var scaleFactorNode = props.GetScalarProperty("scaleFactor");

            // Create and start an ExpressionAnimation to track scroll progress over the desired distance

            ExpressionNode progressAnimation = EF.Clamp(-scrollingProperties.Translation.Y / clampSizeNode, 0, 1);
            _props.StartAnimation("progress", progressAnimation);

            // Get the backing visual for the header so that its properties can be animated
            Visual headerVisual = null;
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Low, () =>
            {
                headerVisual = ElementCompositionPreview.GetElementVisual(HeaderTopContainer);
            });

            // Create and start an ExpressionAnimation to clamp the header's offset to keep it onscreen
            ExpressionNode headerTranslationAnimation = EF.Conditional(progressNode < 1, 0, -scrollingProperties.Translation.Y - clampSizeNode);
            headerVisual.StartAnimation("Offset.Y", headerTranslationAnimation);

            // Create and start an ExpressionAnimation to scale the header during overpan
            ExpressionNode headerScaleAnimation = EF.Lerp(1, 1.25f, EF.Clamp(scrollingProperties.Translation.Y / 50, 0, 1));
            headerVisual.StartAnimation("Scale.X", headerScaleAnimation);
            headerVisual.StartAnimation("Scale.Y", headerScaleAnimation);

            //Set the header's CenterPoint to ensure the overpan scale looks as desired
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Low, () =>
            {
                headerVisual.CenterPoint = new Vector3((float)(HeaderBanner.ActualWidth / 2), (float)HeaderBanner.ActualHeight, 0);
            });



            // Get the backing visual for the profile picture visual so that its properties can be animated
            Visual profileVisual = null;
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Low, () =>
            {
                profileVisual = ElementCompositionPreview.GetElementVisual(BorderSubContainer);
            });

            // Create and start an ExpressionAnimation to scale the profile image with scroll position
            ExpressionNode scaleAnimation = EF.Lerp(1, scaleFactorNode, progressNode);
            profileVisual.StartAnimation("Scale.X", scaleAnimation);
            profileVisual.StartAnimation("Scale.Y", scaleAnimation);

            ExpressionNode contentOffsetAnimation = progressNode * 125;
            profileVisual.StartAnimation("Offset.Y", contentOffsetAnimation);

            Visual buttonsVisual = null;
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Low, () =>
            {
                buttonsVisual = ElementCompositionPreview.GetElementVisual(SortingContainer);
            });
            ExpressionNode buttonsOffsetAnimation = progressNode * -180;
            buttonsVisual.StartAnimation("Offset.Y", buttonsOffsetAnimation);

            // Get backing visuals for the text blocks so that their properties can be animated
            Visual blurbVisual = null;
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Low, () =>
            {
                blurbVisual = ElementCompositionPreview.GetElementVisual(DescriptionTextBox);
            });
            //Visual subtitleVisual = ElementCompositionPreview.GetElementVisual(SubtitleBlock);
            //Visual moreVisual = ElementCompositionPreview.GetElementVisual(MoreText);

            // Create an ExpressionAnimation that moves between 1 and 0 with scroll progress, to be used for text block opacity
            ExpressionNode textOpacityAnimation = EF.Clamp(1 - (progressNode * 2), 0, 1);

            // Start opacity and scale animations on the text block visuals
            blurbVisual.StartAnimation("Opacity", textOpacityAnimation);
            blurbVisual.StartAnimation("Scale.X", scaleAnimation);
            blurbVisual.StartAnimation("Scale.Y", scaleAnimation);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Thread t = new Thread(SetupShyHeader);
            t.Start();
        }

        private void DateSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!CanLoadMenu) { return; }
            DateSelection.IsEnabled = false;
            MealType.IsEnabled = false;
            MenuLoadProgress.Visibility = Visibility.Visible;
            Thread t = new Thread(LoadMenu);
            string d = (string)DateSelection.SelectedItem;
            string m = ((CampusDish.MenuPeriod)MealType.SelectedItem).PeriodId;
            var args = new MenuLoadArgs() { date = d, period = m };
            t.Start(args);
        }

        private void MealType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!CanLoadMenu) { return; }
            DateSelection.IsEnabled = false;
            MealType.IsEnabled = false;
            MenuLoadProgress.Visibility = Visibility.Visible;
            Thread t = new Thread(LoadMenu);
            string d = (string)DateSelection.SelectedItem;
            string m = ((CampusDish.MenuPeriod)MealType.SelectedItem).PeriodId;
            var args = new MenuLoadArgs() { date = d, period = m };
            t.Start(args);
        }
    }
}
