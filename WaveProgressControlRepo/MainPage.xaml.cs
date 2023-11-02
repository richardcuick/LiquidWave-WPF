using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace KunSheng.Drilling
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            ThreadStart();

        }

        private async void ThreadStart()
        {
            Thread P_th = new Thread(//建立线程
                () =>//使用lambda表达式
                {
                    while (true)
                    {
                        this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                            () => {
                                this.Line1.StrokeDashOffset += 2;

                                this.Line2.StrokeDashOffset += 2;
                            });



                        Thread.Sleep(50);
                    }
                }
            );

            P_th.IsBackground = true;
            P_th.Start();
        }
    }
}
