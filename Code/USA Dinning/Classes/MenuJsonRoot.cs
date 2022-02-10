using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusDish
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Allergen
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class SpecialDiet
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class MenuPeriod
    {
        public string PeriodId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string UtcMealPeriodStartTime { get; set; }
        public string UtcMealPeriodEndTime { get; set; }
        public string TimeString { get; set; }
    }

    public class Category
    {
        public string CategoryId { get; set; }
        public string DisplayName { get; set; }
        public object ImageUrl { get; set; }
        public object Description { get; set; }
        public int MenuRanking { get; set; }
    }

    public class DietaryInformation
    {
        public string IconUrl { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDisabled { get; set; }
        public bool IsEnabled { get; set; }
    }

    public class AvailableFilters
    {
        public bool ContainsEggs { get; set; }
        public bool ContainsFish { get; set; }
        public bool ContainsMilk { get; set; }
        public bool ContainsPeanuts { get; set; }
        public bool ContainsShellfish { get; set; }
        public bool ContainsSoy { get; set; }
        public bool ContainsTreeNuts { get; set; }
        public bool ContainsWheat { get; set; }
        public bool IsGlutenFree { get; set; }
        public bool IsHalal { get; set; }
        public bool IsKosher { get; set; }
        public bool IsLocallyGrown { get; set; }
        public bool IsOrganic { get; set; }
        public bool IsVegan { get; set; }
        public bool IsVegetarian { get; set; }
    }

    public class Product
    {
        public string ProductId { get; set; }
        public object DisplayName { get; set; }
        public string MarketingName { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string ImageUrl { get; set; }
        public double? CurrentPrice { get; set; }
        public string CurrentPriceCulture { get; set; }
        public string CurrencyCulture { get; set; }
        public List<Category> Categories { get; set; }
        public List<DietaryInformation> DietaryInformation { get; set; }
        public object BrandId { get; set; }
        public string Ingredients { get; set; }
        public string LocationId { get; set; }
        public bool IsBulkProduct { get; set; }
        public List<object> Variants { get; set; }
        public bool HasNutritionalInformation { get; set; }
        public AvailableFilters AvailableFilters { get; set; }
        public bool? ContainsEggs { get; set; }
        public bool? ContainsFish { get; set; }
        public bool? ContainsMilk { get; set; }
        public bool? ContainsPeanuts { get; set; }
        public bool? ContainsShellfish { get; set; }
        public bool? ContainsSoy { get; set; }
        public bool? ContainsTreeNuts { get; set; }
        public bool? ContainsWheat { get; set; }
        public bool? IsGlutenFree { get; set; }
        public bool? IsHalal { get; set; }
        public bool? IsKosher { get; set; }
        public bool IsLocallyGrown { get; set; }
        public bool IsOrganic { get; set; }
        public bool IsVegan { get; set; }
        public bool IsVegetarian { get; set; }
        public List<object> ProductSummaries { get; set; }
        public string PrimaRecipeType { get; set; }
        public string AllergenStatement { get; set; }
        public string CatalogId { get; set; }
        public DateTime MinPickupTime { get; set; }
        public DateTime MinDeliveryTime { get; set; }
        public object StoreId { get; set; }
        public object DeliveryLeadTime { get; set; }
        public object DeliveryLeadTimeFrame { get; set; }
        public bool IngredientsOrAllergensInconsisten { get; set; }
        public string ServingSize { get; set; }
        public string ServingUnit { get; set; }
        public string Calories { get; set; }
        public string CaloriesFromFat { get; set; }
        public string TotalFat { get; set; }
        public string TransFat { get; set; }
        public string Cholesterol { get; set; }
        public string Sodium { get; set; }
        public string TotalCarbohydrates { get; set; }
        public string DietaryFiber { get; set; }
        public string Sugars { get; set; }
        public string Protein { get; set; }
        public object VitaminA { get; set; }
        public string VitaminC { get; set; }
        public string Calcium { get; set; }
        public string Iron { get; set; }
        public string SaturatedFat { get; set; }
    }

    public class MenuProduct
    {
        public string MenuProductId { get; set; }
        public string ProductId { get; set; }
        public string PeriodId { get; set; }
        public string StationId { get; set; }
        public string StationName { get; set; }
        public int StationRank { get; set; }
        public object MenuId { get; set; }
        public string CatalogId { get; set; }
        public Product Product { get; set; }
        public DateTime AssignedDate { get; set; }
        public bool IsDeemphasized { get; set; }
        public bool HasDeliverySlotInSchedule { get; set; }
        public bool IsUserFavorite { get; set; }
    }

    public class MenuStation
    {
        public string StationId { get; set; }
        public int StationRank { get; set; }
        public string Name { get; set; }
        public object Description { get; set; }
        public object ImageUrl { get; set; }
        public object BrandId { get; set; }
        public object BrandName { get; set; }
        public bool CollapseDeemphasized { get; set; }
        public int DisplayDeemphasizedType { get; set; }
        public string PeriodId { get; set; }
        public string StationDescription { get; set; }
        public string StationImageSitecorePath { get; set; }
        public object StationImagePath { get; set; }
        public string StationImageSitecorePathExntensionless { get; set; }
    }

    public class Menu
    {
        public string? MenuId { get; set; }
        public string? Name { get; set; }
        public string? LocationId { get; set; }
        public object LocationName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<Allergen> Allergens { get; set; }
        public List<SpecialDiet> SpecialDiets { get; set; }
        public List<MenuPeriod> MenuPeriods { get; set; }
        public List<MenuProduct> MenuProducts { get; set; }
        public List<MenuStation> MenuStations { get; set; }
        public bool? ShowCategories { get; set; }
        public List<Category> Categories { get; set; }
        public bool? DisplayPriceOnTheMenu { get; set; }
        public bool? DisplayOnlyCurrentDayMenu { get; set; }
        public bool? OnlyShowCurrentDaysOnlineOrdering { get; set; }
        public bool? HideEatWellIconsFromMenu { get; set; }
        public bool? ImportPrimaIngredients { get; set; }
        public bool? DisplayFoodPreferenceFilter { get; set; }
        public int? MenuSourceSystem { get; set; }
        public bool? ShowLegend { get; set; }
        public bool? DisplayProductImageInFoodOrderingMenu { get; set; }
        public bool? DisplayProductImageInDailyMenu { get; set; }
    }

    public class Item
    {
        public string IconUrl { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDisabled { get; set; }
        public bool IsEnabled { get; set; }
    }

    public class Legend
    {
        public List<Item> Items { get; set; }
        public object LinkUrl { get; set; }
        public object LinkText { get; set; }
    }

    public class Property
    {
        public string Key { get; set; }
        public object Value { get; set; }
    }

    public class Target
    {
        public string ModelName { get; set; }
        public List<Property> Properties { get; set; }
    }

    public class TimeZoneInformation
    {
        public Target Target { get; set; }
        public string ModelName { get; set; }
        public List<object> Properties { get; set; }
    }

    public class Location
    {
        public object Contacts { get; set; }
        public object PaymentMethods { get; set; }
        public object PickupTimeLookup { get; set; }
        public string Id { get; set; }
        public string ModelName { get; set; }
        public List<Property> Properties { get; set; }
        public bool HideEatWellIconsOnMenus { get; set; }
        public object MerchantId { get; set; }
        public object Email { get; set; }
        public object Phone { get; set; }
        public object GeoCode { get; set; }
        public string Name { get; set; }
        public object MarketingName { get; set; }
        public object ShortName { get; set; }
        public object Line1 { get; set; }
        public object Line2 { get; set; }
        public object Line3 { get; set; }
        public object City { get; set; }
        public string RegionCode { get; set; }
        public string PostalCode { get; set; }
        public string CountryCode { get; set; }
        public string ChannelGroupName { get; set; }
        public string ChannelGroupId { get; set; }
        public bool HideProductsNutritional { get; set; }
        public bool HideProductsCalorie { get; set; }
        public bool HideProductsIngredients { get; set; }
        public int CreditCardProviderVersion { get; set; }
        public object CreditCardPaymentForm { get; set; }
        public object HostedPaymentRedirectUrl { get; set; }
        public object SessionToken { get; set; }
        public object CreditCardProviderUrl { get; set; }
        public object CreditCardProviderRequestErrorMessage { get; set; }
        public object ClientId { get; set; }
        public TimeZoneInformation TimeZoneInformation { get; set; }
        public List<object> HolidayDates { get; set; }
        public bool DisplayOnlyCurrentDayMenu { get; set; }
        public bool DisplayPriceOnTheMenu { get; set; }
        public bool ShowPickupDate { get; set; }
        public bool ShowPickupTime { get; set; }
        public bool ShowRecipientEmail { get; set; }
        public bool ShowRecipientPhone { get; set; }
        public bool ShowSpecialInstructions { get; set; }
        public bool IsPickupTimeForEachMealPeriod { get; set; }
        public int PickupTimeIncrement { get; set; }
        public object DisplayDailyMenu { get; set; }
        public object DisplayWeeklyMenu { get; set; }
        public object DefaultMenuView { get; set; }
        public string MenuLabel { get; set; }
        public int MenuLabelingOption { get; set; }
        public bool EnableAllergenFiltering { get; set; }
        public string UnderSpecialDietsHeading { get; set; }
        public string UnderAllergensHeading { get; set; }
        public string AllergensTitle { get; set; }
        public string SpecialDietsTitle { get; set; }
        public string BottomOfNutritionalAttributes { get; set; }
        public int PatronManagementSystemID { get; set; }
        public object LastMenuUpdatedDate { get; set; }
        public string CategoriesTitle { get; set; }
        public int LocationMarketingType { get; set; }
        public object OrderEmailList { get; set; }
        public bool SendOrderEmails { get; set; }
        public bool SmsOrderNotifyEnabled { get; set; }
        public bool RealTimeOrderNotifyEnabled { get; set; }
        public int MenuSourceSystem { get; set; }
        public bool OnlyShowCurrentDaysOnlineOrdering { get; set; }
        public bool OnlyAllowFutureDaysOnlineOrdering { get; set; }
        public bool OnlineOrdering { get; set; }
        public bool HideProductsShortDescription { get; set; }
        public object ScheduledPaymentNotificationEmailList { get; set; }
        public bool SendScheduledPaymentNotifications { get; set; }
        public object DeliveryLeadTime { get; set; }
        public object DeliveryLeadTimeFrame { get; set; }
        public bool InternationalShipping { get; set; }
        public bool DisplayProductImageInDailyMenu { get; set; }
        public bool DisplayProductImageInFoodOrderingMenu { get; set; }
        public object OutletCode { get; set; }
        public bool IsOutletCodeActive { get; set; }
        public bool DisplayProductImageInEmail { get; set; }
        public bool FulfillmentPickup { get; set; }
        public bool FulfillmentDelivery { get; set; }
    }

    public class MenuJsonRoot
    {
        public int Mode { get; set; }
        public string Date { get; set; }
        public string LocationId { get; set; }
        public object StoreIds { get; set; }
        public string SelectedPeriodId { get; set; }
        public Menu Menu { get; set; }
        public int DefaultMenuView { get; set; }
        public bool DisplayOnlyCurrentDayMenu { get; set; }
        public bool DisplayWeeklyMenu { get; set; }
        public bool DisplayDailyMenu { get; set; }
        public bool DisplayFoodOrderMenu { get; set; }
        public string SelectedPeriodName { get; set; }
        public bool DisplayPriceOnTheMenu { get; set; }
        public bool HideProductsCalorie { get; set; }
        public bool HideProductsNutritional { get; set; }
        public bool HideEatWellIconsFromMenu { get; set; }
        public string BottomOfNutritionalAttributes { get; set; }
        public string CaloriesAdvice { get; set; }
        public bool EnableAllergenFiltering { get; set; }
        public string AllergensTitle { get; set; }
        public string SpecialDietsTitle { get; set; }
        public bool MenuConfigured { get; set; }
        public object SeeMenuLink { get; set; }
        public object Target { get; set; }
        public string CategoriesTitle { get; set; }
        public bool IsCheckoutAndCartEnabled { get; set; }
        public bool HideProductsShortDescription { get; set; }
        public bool OnlyShowCurrentDaysOnlineOrdering { get; set; }
        public bool OnlyAllowFutureDaysOnlineOrdering { get; set; }
        public string MinDate { get; set; }
        public object FavoriteProductsModel { get; set; }
        public string LinkToLoginPageWithReturnUrl { get; set; }
        public object MenuFoodOrderingProducts { get; set; }
        public Legend Legend { get; set; }
        public bool IsFavorites { get; set; }
        public bool IsDaily { get; set; }
        public bool IsWeekly { get; set; }
        public bool IsFoodOrder { get; set; }
        public bool DisplayProductImages { get; set; }
        public bool ShowAddToCalculatorButton { get; set; }
        public bool UserFavoritiesEnabled { get; set; }
        public string CacheKey { get; set; }
        public bool IsCached { get; set; }
        public Location Location { get; set; }
        public bool FulfillmentDelivery { get; set; }
        public bool FulfillmentPickup { get; set; }
    }
}
