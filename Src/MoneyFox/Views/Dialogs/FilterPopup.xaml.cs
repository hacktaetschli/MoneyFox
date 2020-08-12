﻿using MoneyFox.ViewModels.Dialogs;
using Rg.Plugins.Popup.Extensions;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;

namespace MoneyFox.Presentation.Dialogs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilterPopup
    {
        private SelectFilterDialogViewModel ViewModel => BindingContext as SelectFilterDialogViewModel;

        public FilterPopup()
        {
            InitializeComponent();
        }

        public async Task ShowAsync()
        {
            await App.Current.MainPage.Navigation.PushPopupAsync(this);
        }

        public async Task DismissAsync()
        {
            await App.Current.MainPage.Navigation.PopPopupAsync();
        }

        private void Button_OnClicked(object sender, System.EventArgs e)
        {
            ViewModel.FilterSelectedCommand.Execute(null);
        }
    }
}
