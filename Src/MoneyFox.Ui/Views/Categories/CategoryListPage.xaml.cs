﻿namespace MoneyFox.Ui.Views.Categories;

public partial class CategoryListPage : ContentPage
{
    public CategoryListPage()
    {
        InitializeComponent();
        BindingContext = App.GetViewModel<CategoryListViewModel>();
    }

    private CategoryListViewModel ViewModel => (CategoryListViewModel)BindingContext;

    protected override async void OnAppearing()
    {
        await ViewModel.InitializeAsync();
    }
}
