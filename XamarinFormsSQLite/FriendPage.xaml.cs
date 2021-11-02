using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinFormsSQLite
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FriendPage : ContentPage
    {
        public FriendPage()
        {
            InitializeComponent();
        }

        private async void SaveFriend(object sender, EventArgs e)
        {
            var friend = (Friend)BindingContext;
            if (!string.IsNullOrEmpty(friend.Name))
            {
                await App.Database.SaveItemAsync(friend);
            }
            await Navigation.PopAsync();
        }
        private async void DeleteFriend(object sender, EventArgs e)
        {
            var friend = (Friend)BindingContext;
            await App.Database.DeleteItemAsync(friend);
            await Navigation.PopAsync();
        }
        private async void Cancel(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}