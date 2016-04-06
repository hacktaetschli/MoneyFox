﻿using Android.Runtime;
using MoneyFox.Shared.ViewModels;

namespace MoneyFox.Droid.Fragments
{
	[Register("moneymanager.droid.fragments.SettingsSecurityFragment")]
    public class SettingsSecurityFragment : BaseFragment<SettingsSecurityViewModel>
    {
        protected override int FragmentId => Resource.Layout.fragment_settings_security;
    }
}

