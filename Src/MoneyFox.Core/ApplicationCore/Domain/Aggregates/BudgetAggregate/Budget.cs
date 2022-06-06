namespace MoneyFox.Core.ApplicationCore.Domain.Aggregates.BudgetAggregate
{

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using AutoMapper.Configuration.Annotations;
    using JetBrains.Annotations;
    using MoneyFox.Core.Common.Interfaces;

    public class Budget : EntityBase, IAggregateRoot
    {
        [UsedImplicitly]
        private Budget() { }

        public Budget(string name, decimal spendingLimit, IList<int> includedCategories)
        {
            Name = name;
            SpendingLimit = spendingLimit;
            IncludedCategories = includedCategories;
        }

        public int Id
        {
            get;

            [UsedImplicitly]
            private set;
        }

        public string Name
        {
            get;

            [UsedImplicitly]
            private set;
        } = string.Empty;

        public decimal SpendingLimit
        {
            get;

            [UsedImplicitly]
            private set;
        }

        public IList<int> IncludedCategories
        {
            get;

            [UsedImplicitly]
            private set;
        } = new List<int>();
    }

}
