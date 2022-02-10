using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace USA_Dinning.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FreshFoodCompanyPage : Page
    {

        public FreshFoodCompanyPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("forwardAnimation", ItemCard);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Location loc = e.Parameter as Location;
            NameTitle.Text = loc.Name;
            CardBackgroundImage.Source = new BitmapImage(new Uri(loc.BackgroundImage));
            BannerBackground.ImageSource = new BitmapImage(new Uri(loc.BackgroundImage));
            CardImage.ImageSource = new BitmapImage(new Uri(loc.BackgroundImage));
            IsOpenText.Text = loc.IsOpen;
            AddressText.Text = loc.Address;
            DateGlanceText.Text = loc.DateGlance;


            ConnectedAnimation animation =
                ConnectedAnimationService.GetForCurrentView().GetAnimation("forwardAnimation");
            if (animation != null)
            {
                animation.TryStart(ItemCard);
            }
        }
    }
}
