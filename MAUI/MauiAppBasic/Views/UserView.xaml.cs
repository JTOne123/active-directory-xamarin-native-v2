using MAUI.MSALClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using System.Net.Http.Headers;
using System.Reflection;

namespace MauiAppBasic.Views;

public partial class UserView : ContentPage
{
    //private MSALClientHelper MSALClientHelper;
    //private MSGraphHelper MSGraphHelper;
    //private const string embeddedConfigFile = "MauiAppBasic.appsettings.json";

    public UserView()
    {
        InitializeComponent();

        //var cachedUserAccount = PublicClientWrapper.Instance.MSALClientHelper.InitializePublicClientAppAsync();

        // Sign-in the user 
        //var token = PublicClientWrapper.Instance.AcquireTokenSilentAsync();

        //var assembly = Assembly.GetExecutingAssembly();
        //using var stream = assembly.GetManifestResourceStream("MauiAppBasic.appsettings.json");
        //IConfiguration AppConfiguration = new ConfigurationBuilder()
        //    .AddJsonStream(stream)
        //    .Build();

        //AzureADConfig azureADConfig = AppConfiguration.GetSection("AzureAD").Get<AzureADConfig>();
        //this.MSALClientHelper = new MSALClientHelper(azureADConfig);

        //MSGraphApiConfig graphApiConfig = AppConfiguration.GetSection("MSGraphApi").Get<MSGraphApiConfig>();
        //this.MSGraphHelper = new MSGraphHelper(graphApiConfig, this.MSALClientHelper);

        //// Initializes the Public Client app and loads any already signed in user from the token cache
        //var cachedUserAccount = Task.Run(async () => await PublicClientWrapper.Instance.MSALClientHelper.FetchSignedInUserFromCache()).Result;

        _ = GetUserInformationAsync();
    }

    private async Task GetUserInformationAsync()
    {
        try
        {
            var user = await PublicClientWrapper.Instance.MSGraphHelper.GetMeAsync();
            UserImage.Source = ImageSource.FromStream(async _ => await PublicClientWrapper.Instance.MSGraphHelper.GetMyPhotoAsync());

            //// call Web API to get the data
            //AuthenticationResult result = await PublicClientWrapper.Instance.AcquireTokenSilentAsync();

            //HttpClient client = new HttpClient();
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);

            //GraphServiceClient graphServiceClient = new GraphServiceClient(client);

            //var user = await graphServiceClient.Me.GetAsync();

            //UserImage.Source = ImageSource.FromStream(async _ => await graphServiceClient.Me.Photo.Content.GetAsync());
            DisplayName.Text = user.DisplayName;
            Email.Text = user.Mail;
        }
        catch (MsalUiRequiredException)
        {
            await PublicClientWrapper.Instance.SignOutAsync();
            //await PublicClientWrapper.Instance.SignOutAsync().ContinueWith((t) =>
            //{
            //    return Task.CompletedTask;
            //});

            await Shell.Current.GoToAsync("mainview");
        }
    }

    protected override bool OnBackButtonPressed() { return true; }

    private async void SignOutButton_Clicked(object sender, EventArgs e)
    {
        await PublicClientWrapper.Instance.SignOutAsync();
        //await PublicClientWrapper.Instance.SignOutAsync().ContinueWith((t) =>
        //{
        //    return Task.CompletedTask;
        //});

        await Shell.Current.GoToAsync("mainview");
    }
}