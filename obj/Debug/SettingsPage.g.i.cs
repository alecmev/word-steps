﻿#pragma checksum "D:\Stuff\work\WordSteps\SettingsPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "3A72CD48FE5AF5C3B20BD2C7EE14AA69"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.239
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace WordSteps {
    
    
    public partial class SettingsPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal Microsoft.Phone.Controls.Pivot SettingsPivot;
        
        internal Microsoft.Phone.Controls.PivotItem LanguagePivotItem;
        
        internal Microsoft.Phone.Controls.ListPicker LearningLanguage;
        
        internal Microsoft.Phone.Controls.ListPicker TranslationLanguage;
        
        internal Microsoft.Phone.Controls.ListPicker InterfaceLanguage;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/WordSteps;component/SettingsPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.SettingsPivot = ((Microsoft.Phone.Controls.Pivot)(this.FindName("SettingsPivot")));
            this.LanguagePivotItem = ((Microsoft.Phone.Controls.PivotItem)(this.FindName("LanguagePivotItem")));
            this.LearningLanguage = ((Microsoft.Phone.Controls.ListPicker)(this.FindName("LearningLanguage")));
            this.TranslationLanguage = ((Microsoft.Phone.Controls.ListPicker)(this.FindName("TranslationLanguage")));
            this.InterfaceLanguage = ((Microsoft.Phone.Controls.ListPicker)(this.FindName("InterfaceLanguage")));
        }
    }
}
