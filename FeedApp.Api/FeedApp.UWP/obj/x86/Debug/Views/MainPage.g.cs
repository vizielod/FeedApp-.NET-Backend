﻿#pragma checksum "E:\Egyetem\III. ev\II. felev\Szoftverfejlesztés .NET platformra\FeedApp.Api\FeedApp.UWP\Views\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D0117CABEEE437BA7CCB0E554623ED62"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FeedApp.UWP
{
    partial class MainPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.16.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1: // Views\MainPage.xaml line 11
                {
                    this.FeedAppTextBlock = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 2: // Views\MainPage.xaml line 12
                {
                    this.LogoImage = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 3: // Views\MainPage.xaml line 14
                {
                    this.AppDescriptionTextBlock = (global::Windows.UI.Xaml.Controls.RichTextBlock)(target);
                }
                break;
            case 4: // Views\MainPage.xaml line 23
                {
                    this.WelcomeTextBlock = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 5: // Views\MainPage.xaml line 24
                {
                    this.ForgotPasswordHyperLinkButton = (global::Windows.UI.Xaml.Controls.HyperlinkButton)(target);
                }
                break;
            case 6: // Views\MainPage.xaml line 25
                {
                    this.SignInButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                }
                break;
            case 7: // Views\MainPage.xaml line 26
                {
                    this.SignUpTextBlock = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 8: // Views\MainPage.xaml line 27
                {
                    this.SignUpHyperLinkButton = (global::Windows.UI.Xaml.Controls.HyperlinkButton)(target);
                    ((global::Windows.UI.Xaml.Controls.HyperlinkButton)this.SignUpHyperLinkButton).Click += this.SignUpHyperLinkButton_Click;
                }
                break;
            case 9: // Views\MainPage.xaml line 28
                {
                    this.RememberMeCheckBox = (global::Windows.UI.Xaml.Controls.CheckBox)(target);
                }
                break;
            case 10: // Views\MainPage.xaml line 29
                {
                    this.EmailTextBlock = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 11: // Views\MainPage.xaml line 30
                {
                    this.PasswordBox = (global::Windows.UI.Xaml.Controls.PasswordBox)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.16.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

