﻿#pragma checksum "E:\Egyetem\III. ev\II. felev\Szoftverfejlesztés .NET platformra\OnlineTutorial\Win10WebAPIClient\Win10WebAPIClient\Delete.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "18294FAFA5BCC98167C93F00406409D4"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Win10WebAPIClient
{
    partial class Delete : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    this.panel = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 2:
                {
                    this.message = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 3:
                {
                    global::Windows.UI.Xaml.Controls.Button element3 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 13 "..\..\..\Delete.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)element3).Click += this.Button_Click;
                    #line default
                }
                break;
            case 4:
                {
                    global::Windows.UI.Xaml.Controls.Button element4 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 14 "..\..\..\Delete.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)element4).Click += this.Button_Click;
                    #line default
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

