/*************************************************************************/
/*                 			   Hear2Read      			                 */
/*                         Copyright (c) 2015                            */
/*                        All Rights Reserved.                           */
/*                                                                       */
/*  Permission is hereby granted, free of charge, to use and distribute  */
/*  this software and its documentation without restriction, including   */
/*  without limitation the rights to use, copy, modify, merge, publish,  */
/*  distribute, sublicense, and/or sell copies of this work, and to      */
/*  permit persons to whom this work is furnished to do so, subject to   */
/*  the following conditions:                                            */
/*   1. The code must retain the above copyright notice, this list of    */
/*      conditions and the following disclaimer.                         */
/*   2. Any modifications must be clearly marked as such.                */
/*   3. Original authors' names are not deleted.                         */
/*   4. The authors' names are not used to endorse or promote products   */
/*      derived from this software without specific prior written        */
/*      permission.                                                      */
/*                                                                       */
/*  COBALT SPEECH AND LANGUAGE INC AND THE CONTRIBUTORS TO THIS WORK     */
/*  DISCLAIM ALL WARRANTIES WITH REGARD TO THIS SOFTWARE, INCLUDING      */
/*  ALL IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS, IN NO EVENT   */
/*  SHALL CARNEGIE MELLON UNIVERSITY NOR THE CONTRIBUTORS BE LIABLE      */
/*  FOR ANY SPECIAL, INDIRECT OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES    */
/*  WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS, WHETHER IN   */
/*  AN ACTION OF CONTRACT, NEGLIGENCE OR OTHER TORTIOUS ACTION,          */
/*  ARISING OUT OF OR IN CONNECTION WITH THE USE OR PERFORMANCE OF       */
/*  THIS SOFTWARE.                                                       */
/*                                                                       */
/*************************************************************************/
/*             Authors:  Timothy White (tim@timwhitedesign.org)          */
/*                Date:  December 2017                                   */
/*************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;


namespace Hear2Read_TTS_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //Find supported voices from the Hear2Read server

            // Find installed voices in c:/program files(x86)/Hear2Read/Languages
            Display_Installed_Voices();
 
        }

        private void Logo_Click(object sender, RoutedEventArgs e)
        {
                       try
                       {
                           System.Diagnostics.Process.Start("http://www.hear2read.org/");
                      }
                       catch { }
        }
        private void Get_Voices(object sender, RoutedEventArgs e)
        {
                       try
                       {
                         System.Diagnostics.Process.Start("http://www.hear2read.org/TTS_for_windows.php");
                       }
                       catch { }
        }
        private void Demo_Voices(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://www.hear2read.org/demo/app.php");
            }
            catch { }
        }
        private void Refresh_List(object sender, RoutedEventArgs e)
        {
            //Remove currently displayed voices
            while (VoiceList.Children.Count > 0)
            {
                VoiceList.Children.RemoveAt(0);
            }
            //Re-populate list with voices
            Display_Installed_Voices();
        }

        private string GetDLLName(string MachineType, string CLSID)
        {
            // int DLLCount = 0;
            RegistryKey myKey;
            string DLLName;

            if (MachineType == "32bit")
            {
                myKey = Registry.ClassesRoot.OpenSubKey(@"Wow6432Node\CLSID", false);
            }
            else
            {
                myKey = Registry.ClassesRoot.OpenSubKey(@"CLSID", false);
            }
            RegistryKey myKey2 = myKey.OpenSubKey(CLSID, false);
            {
                if (myKey2 != null)
                {
                    RegistryKey myKey3 = myKey2.OpenSubKey(@"InprocServer32", false);
                    if ((DLLName = (string)myKey3.GetValue("", "")) != "")
                    {
                        return (DLLName);
                    }
                }
            }
            return (null);
        }

         void FindVoices(string ProgramType)
        {
            string voxdir;
            string languageName;
            string ErrorString;
            int LanguageCount = 0;
            string DLLName;
            string DLLExistanceString;
            RegistryKey myKey;
            if (ProgramType == "32bit")
            {
                // Add a line about the DLL here.
                myKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Microsoft\SPEECH\Voices\Tokens", false);
                ErrorString = "No 32bit Languages exist on this machine.";
            } else
            {
                myKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\Microsoft\Speech\Voices\Tokens", false);
                ErrorString = "No 64bit Languages exist on this machine.";
            }
            string[] tokensArray = myKey.GetSubKeyNames();
            // Check Each Token to see if it is a Hear2Read Voice (voxdir will be defined if this is a Hear2Read voice)
            foreach (string Token in tokensArray)
            {
                if (Token != "")
                {
                    RegistryKey myKey2 = myKey.OpenSubKey(Token, false);
                    if ((voxdir = (string)myKey2.GetValue("voxdir", "")) != "")
                    {
                        string CLSID = (string)myKey2.GetValue("CLSID", "");
                        if (File.Exists(voxdir))
                        {
                            RegistryKey myKey3 = myKey2.OpenSubKey("Attributes", false);
                            if ((languageName = (string)myKey3.GetValue("Name", "")) != "")
                            {
                                LanguageCount += 1;
                                // See if the DLL exists
                               
                                DLLName = GetDLLName(ProgramType, CLSID);
                                if (( DLLName != null) && (File.Exists(voxdir)) )
                                {
                                    // Indicate that the DLL exists
                                    DLLExistanceString = "DLL  [Yes]  ";
                                }
                                else
                                {
                                    // Indicate that the DLL does not exist.
                                    DLLExistanceString = "DLL  [No]   ";
                                }
                                 //Display the Installed Voice name in the VoiceList with Demo and Download Buttons

                                //First Create the DocPanel to contain the text and buttons
                                DockPanel myDockPanel = new DockPanel
                                {
                                    LastChildFill = true

                                };
                                //Create the textblock which contains the DLL status
                                TextBlock DLLTextBlock = new TextBlock
                                {
                                    TextWrapping = TextWrapping.Wrap,
                                    Margin = new Thickness(0, 5, 5, 5),
                                    TextAlignment = TextAlignment.Right,
                                    // Width = 100,
                                    FontSize = 25,
                                    Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF101fbb")),
                                    Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD")),
                                    Text = DLLExistanceString
                                };
                                //Create the TextBlock which contains the Voice
                                TextBlock myTextBlock = new TextBlock
                                {
                                    TextWrapping = TextWrapping.Wrap,
                                    Margin = new Thickness(5, 5, 0, 5),
                                    FontSize = 25,
                                    Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF101fbb")),
                                    Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD")),
                                    Text = languageName
                                };
                                DockPanel.SetDock(DLLTextBlock, Dock.Right);
                                DockPanel.SetDock(myTextBlock, Dock.Left);
                                myDockPanel.Children.Add(myTextBlock);
                                myDockPanel.Children.Add(DLLTextBlock);

                                VoiceList.Children.Add(myDockPanel);
                            }
                        }
                    }
                }
            }
            if (LanguageCount == 0)
            {
                TextBlock myTextBlock = new TextBlock
                {
                    TextWrapping = TextWrapping.Wrap,
                    Margin = new Thickness(5, 5, 5, 5),
                    FontSize = 25,
                    Text = ErrorString,
                    TextAlignment = TextAlignment.Center
                };
                myTextBlock.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF101fbb"));
                myTextBlock.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));

                VoiceList.Children.Add(myTextBlock);
            }
        }
 
        private void Display_Installed_Voices()
        {
            // First Look for 32 bit Voices 
            TextBlock Title = new TextBlock
            {
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(5, 5, 5, 5),
                // Width = 100,
                FontSize = 25,
                Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD")),
                Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF101fbb")),
                MinWidth = 420,
                Text = "32Bit Applications",
                TextAlignment = TextAlignment.Center
            };
            VoiceList.Children.Add(Title);
            FindVoices("32bit");
            //First look for voices
            TextBlock Title2 = new TextBlock
            {
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(5, 5, 5, 5),
                // Width = 100,
                FontSize = 25,
                Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD")),
                Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF101fbb")),
                MinWidth = 420,
                Text = "64Bit Applications",
                TextAlignment = TextAlignment.Center
            };
            VoiceList.Children.Add(Title2);
            FindVoices("64bit");
         }
    }
}
