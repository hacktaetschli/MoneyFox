namespace MoneyFox.Views.Payments
{

    using Core.Resources;
    using ViewModels.Payments;

    public partial class EditPaymentPage
    {
        private readonly int paymentId;

        public EditPaymentPage(int paymentId)
        {
            InitializeComponent();
            BindingContext = App.GetViewModel<EditPaymentViewModel>();
            this.paymentId = paymentId;
            var cancelItem = new ToolbarItem
            {
                Command = new Command(async () => await Navigation.PopModalAsync()),
                Text = Strings.CancelLabel,
                Priority = -1,
                Order = ToolbarItemOrder.Primary
            };

            var saveItem = new ToolbarItem
            {
                Command = new Command(() => ViewModel.SaveCommand.Execute(null)),
                Text = Strings.SaveLabel,
                Priority = 1,
                Order = ToolbarItemOrder.Primary
            };

            ToolbarItems.Add(cancelItem);
            ToolbarItems.Add(saveItem);
        }

        private EditPaymentViewModel ViewModel => (EditPaymentViewModel)BindingContext;

        protected override async void OnAppearing()
        {
            await ViewModel.InitializeAsync(paymentId);
        }
    }

}
