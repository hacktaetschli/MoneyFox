﻿namespace MoneyFox.Core.Tests.Features;

using Core.Features.CategoryDeletion;
using Domain.Tests.TestFramework;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

public class DeleteCategoryByIdCommandTests : InMemoryTestBase
{
    private readonly DeleteCategoryById.Handler handler;

    public DeleteCategoryByIdCommandTests()
    {
        handler = new(Context);
    }

    [Fact]
    public async Task DeleteCategoryWithPassedId()
    {
        // Arrange
        var testCategory = new TestData.DefaultCategory();
        Context.RegisterCategory(testCategory: testCategory);

        // Act
        await handler.Handle(command: new(testCategory.Id), cancellationToken: default);

        // Assert
        (await Context.Categories.SingleOrDefaultAsync(x => x.Id == testCategory.Id)).Should().BeNull();
    }

    [Fact]
    public async Task DoesNothingWhenCategoryNotFound()
    {
        // Arrange
        var testCategory = new TestData.DefaultCategory();
        Context.RegisterCategory(testCategory: testCategory);

        // Act
        await handler.Handle(command: new(99), cancellationToken: default);

        // Assert
        (await Context.Categories.SingleOrDefaultAsync(x => x.Id == testCategory.Id)).Should().NotBeNull();
    }

    [Fact]
    public async Task RemoveCategoryFromPaymentOnDelete()
    {
        // Arrange
        var expense = new TestData.DefaultExpense();
        var dbExpense = Context.RegisterPayment(testCategory: expense);
        var income = new TestData.DefaultIncome();
        var dbIncome = Context.RegisterPayment(testCategory: income);

        // Act
        await handler.Handle(command: new(dbExpense.Category!.Id), cancellationToken: default);

        // Assert
        var unassignedPayment = await Context.Payments.SingleAsync(x => x.Id == dbExpense.Id);
        unassignedPayment.Category.Should().BeNull();
        var unmodifiedPayment = await Context.Payments.SingleAsync(x => x.Id == dbIncome.Id);
        unmodifiedPayment.Category.Should().NotBeNull();
    }
}
