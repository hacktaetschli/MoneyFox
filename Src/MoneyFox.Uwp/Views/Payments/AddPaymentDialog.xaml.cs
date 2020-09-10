﻿using MoneyFox.Domain;
using MoneyFox.Application.Common;
using MoneyFox.Uwp.ViewModels;
using Windows.UI.Xaml.Controls;

namespace MoneyFox.Uwp.Views.Payments
{
    public sealed partial class AddPaymentDialog : ContentDialog
    {
        private AddPaymentViewModel ViewModel => (AddPaymentViewModel) DataContext;

        public AddPaymentDialog(PaymentType paymentType)
        {
            InitializeComponent();

            ViewModel.PaymentType = paymentType;
            ViewModel.InitializeCommand.ExecuteAsync().FireAndForgetSafeAsync();
        }
    }
}
