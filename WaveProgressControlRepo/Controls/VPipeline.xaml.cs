using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
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

//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace KunSheng.Drilling.Controls
{
    public sealed partial class VPipeline : UserControl
    {
        private Line line1 = null;
        private Line line2 = null;

        public VPipeline()
        {
            this.InitializeComponent();

            line1 = new Line();
            line1.StrokeThickness = 2;
            line1.StrokeDashArray= new DoubleCollection() { 2, 3 };
            line1.StrokeDashCap= PenLineCap.Flat;
            line1.Stroke= new SolidColorBrush(Windows.UI.Colors.White);
            MainCanvas.Children.Add(line1);
            Canvas.SetZIndex(line1, 2);

            line2 = new Line();
            line2.StrokeThickness = 2;
            line2.StrokeDashArray = new DoubleCollection() { 2, 3 };
            line2.StrokeDashCap = PenLineCap.Flat;
            line2.Stroke = new SolidColorBrush(Windows.UI.Colors.White);
            MainCanvas.Children.Add(line2);
            Canvas.SetZIndex(line2, 2);

            this.line2.StrokeDashOffset = 2;

            ThreadStart();
        }

        public enum PipeDirection
        {
            TopToBottom,
            BottomToTop,
        }

        public PipeDirection Direction { get; set; }= PipeDirection.TopToBottom;

        private async void ThreadStart()
        {
            int offset = this.Direction == PipeDirection.TopToBottom ? -2 : 2;

            Thread P_th = new Thread(//建立线程
                () =>//使用lambda表达式
                {
                    while (true)
                    {
                        this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                            () => {
                                this.line1.StrokeDashOffset += offset;
                                this.line2.StrokeDashOffset += offset;

                                this.line1.Y1 = 0;
                                this.line1.X1 = this.ActualWidth / 3;
                                this.line1.Y2 = this.ActualHeight;
                                this.line1.X2 = this.ActualWidth/3;

                                this.line2.Y1 = 0;
                                this.line2.X1 = this.ActualWidth / 3 * 2;
                                this.line2.Y2 = this.ActualHeight;
                                this.line2.X2 = this.ActualWidth / 3 * 2;

                                this.MainPipe.Width = this.ActualWidth;
                                this.MainPipe.Height = this.ActualHeight;
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
