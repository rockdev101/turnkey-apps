using RightPath.Views;
using Xamarin.Forms;
using RightPath.Data;

namespace RightPath
{
    public class App : Application
    {
        public static UserManager userManager { get; private set; }

        public App()
        {
            // The root page of your application
            object loggedInObject;
            var isLoggedIn = Application.Current.Properties.TryGetValue("isLoggedIn", out loggedInObject);
            userManager = new UserManager(new RestService());
            if (!isLoggedIn)
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                MainPage = new NavigationPage(new MainPage("Start")) { BarBackgroundColor = Color.FromHex("#FFC107"), BarTextColor = Color.White }; //Turnkey
            }

			//MainPage = new NavigationPage(new MainPage("Start")) { BarBackgroundColor = Color.FromHex("#AFCB40"), BarTextColor = Color.Black }; //Right path
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
