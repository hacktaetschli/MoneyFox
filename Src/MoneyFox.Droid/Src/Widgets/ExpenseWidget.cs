﻿using System.Threading.Tasks;
using MoneyFox.Shared.Interfaces.Shotcuts;

namespace MoneyFox.Droid.Widgets
{
    public class ExpenseWidget : ISpendingShortcut
    {
        public Task CreateShortCut()
        {
            throw new System.NotImplementedException();
        }
        public Task RemoveShortcut()
        {
            throw new System.NotImplementedException();
        }
        public bool IsShortcutExisting
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }
    }
}

