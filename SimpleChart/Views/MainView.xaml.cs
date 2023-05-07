using Binance.Net.Clients;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using ScottPlot;
using ScottPlot.Ticks;
using System;

using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static ScottPlot.Generate;
using Color = System.Drawing.Color;
//using DateTime = ScottPlot.Generate.DateTime;

namespace SimpleChart.Views
{
    /// <summary>
    /// Логика взаимодействия для MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        private double _second10 = 0.00011574074596865103;
        private double _second60 = 0.0006944444394321181;
        private double time;
        private double price;
        private double lower;
        private double upper;
        private bool IsMouseEnter = false;
        private (double x, double y) prevCoordinates;
        private double X = 0;
        private double Y = 0;
        private bool isRun = false;
        bool check = false;
        private double change = 0.0006944444394321181;
        private double changeRate = 1440;

        
        string[] strings = { "1000PEPEUSDT", "BTCUSDT", "ETHUSDT" };
 
        public MainView()
        {
            InitializeComponent();
            LoadChart();
            RunAsync();
            SubscribeToAggregatedTradeUpdatesAsync(strings[0]);
        }

        private void LoadChart()
        {
            for (int i = 0; i < size; i++)
            {
                xsBuyer[i] = 0;
                ysBuyer[i] = 0;
                xsMaker[i] = 0;
                ysMaker[i] = 0;
            }
            //WpfPlot1.AxesChanged += WpfPlot1_AxesChanged;
            WpfPlot1.Configuration.ScrollWheelZoom = false;
            WpfPlot1.Configuration.Zoom = false;
            WpfPlot1.Configuration.MiddleClickDragZoom = false;
            WpfPlot1.Configuration.ScrollWheelZoom = false;
            WpfPlot1.Configuration.AltLeftClickDragZoom = false;
            WpfPlot1.Configuration.RightClickDragZoom = false;
            WpfPlot1.Plot.XAxis.TickLabelFormat("ss", true);
            WpfPlot1.Plot.XAxis.TickDensity(2.5);

            Color red = Color.Red;
            WpfPlot1.Plot.XAxis.TickMarkColor(red);
            WpfPlot1.Plot.XAxis.ManualTickSpacing(100);

            WpfPlot1.Plot.XAxis.Line(color: red);

            WpfPlot1.Plot.YAxis2.Grid(true);
            WpfPlot1.Plot.YAxis.Grid(false);
            WpfPlot1.Plot.AddScatter(xsBuyer, ysBuyer, lineWidth: 0, color: Color.Green);
            WpfPlot1.Plot.AddScatter(xsMaker, ysMaker, lineWidth: 0, color: Color.Red);

            WpfPlot1.Plot.XAxis.TickLabelStyle(Color.Red);
            WpfPlot1.Plot.XAxis.TickMarkDirection(false);
            WpfPlot1.Plot.XAxis.AutomaticTickPositions();
            WpfPlot1.Plot.XAxis.AutomaticTickPositions();


            // Axis Y2
            WpfPlot1.Plot.YAxis2.Ticks(true);

            WpfPlot1.Configuration.RightClickDragZoomFromMouseDown = false;
            WpfPlot1.MouseLeftButtonDown += WpfPlot1_MouseLeftButtonDown;
            WpfPlot1.MouseLeftButtonUp += WpfPlot1_MouseLeftButtonUp;
            WpfPlot1.MouseRightButtonDown += WpfPlot1_MouseRightButtonDown;
            WpfPlot1.MouseRightButtonUp += WpfPlot1_MouseRightButtonUp;
            WpfPlot1.MouseWheel += WpfPlot1_MouseWheel;
            WpfPlot1.MouseMove += WpfPlot1_MouseMove;
        }
        public void AddPoint(double time, double price, Color color)
        {
            WpfPlot1.Dispatcher.Invoke(new Action(() => { 
                WpfPlot1.Plot.AddPoint(x: time, y: price, color: color, size: 4);
            }));
        } 
        const int size = 5000;
        double[] xsBuyer = new double[size];
        double[] ysBuyer = new double[size];
        double[] xsMaker = new double[size];
        double[] ysMaker = new double[size];
        public async void SubscribeToAggregatedTradeUpdatesAsync(String symbol)
        {
            try
            {
                
                Color color = new();
                BinanceSocketClient binanceSocketClient1 = new();

                var res3 = await binanceSocketClient1.UsdFuturesStreams.SubscribeToAggregatedTradeUpdatesAsync(symbol, message => {

                    double x = message.Data.TradeTime.ToOADate();
                    double y = (double)message.Data.Price;

                    if (message.Data.Price > 1000) zoomRate = 100000;
                    else if (message.Data.Price > 0.1m) zoomRate = 10000;
                    else zoomRate = 1000;
                    time = x;
                    price = y;


                    if (!message.Data.BuyerIsMaker)
                    {
                        AddPoint(x, y, xsBuyer, ysBuyer);
                    }
                    else {
                        AddPoint(x, y, xsMaker, ysMaker);
                    }

                    //WpfPlot1.Dispatcher.Invoke(new Action(() =>
                    //{
                    //    AddPoint(x, Decimal.ToDouble(message.Data.Price), color);
                    //    lower = (double)message.Data.Price - ((double)message.Data.Price / 300);
                    //    upper = (double)message.Data.Price + ((double)message.Data.Price / 300);
                    //    if(!isRun) isRun = true;
                    //}));
                    lower = (double)message.Data.Price - ((double)message.Data.Price / 300);
                    upper = (double)message.Data.Price + ((double)message.Data.Price / 300);
                    if (!isRun) isRun = true;
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void AddPoint(double x, double y, double[] xs, double[] ys)
        {
            for (int i = 0; i < size; i++)
            {
                if (ys[size - 1] == 0)
                {
                    xs[i] = x;
                    ys[i] = y;
                }
                else if(i != (size - 1))
                {
                    xs[i] = xs[i + 1];
                    ys[i] = ys[i + 1];
                }
                else
                {
                    xs[i] = x;
                    ys[i] = y;
                }
            }
        }
        double previousY = 0;
        private double zoom = 50;
        private int zoomRate = 10000; 
        double yMax = 0;
        private void WpfPlot1_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.RightButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                double y = e.GetPosition(this).Y;


                if (previousY != 0)
                {
                    if (y > previousY && y != previousY)
                    {
                        if(zoom > 2) zoom -= 1;
                    }
                    else
                    {
                        if(zoom < 100) zoom += 1;
                    }
                    double max = (center + (center * zoom / zoomRate));
                    double min = (center - (center * zoom / zoomRate));
                    WpfPlot1.Plot.SetAxisLimitsY(yMin: min, yMax: max, 0);
                    yMax = (max - min) / min * 100;
                    WpfPlot1.Plot.SetAxisLimitsY(yMin: 0 - yMax / 2, yMax: yMax / 2, 1);
                }
                previousY = y;
            }
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                if(yMax != 0) WpfPlot1.Plot.SetAxisLimitsY(yMin: 0 - yMax / 2, yMax: yMax / 2, 1);
            }
        }

     
        private void WpfPlot1_MouseRightButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            previousY = 0;
            WpfPlot1.Refresh();
        }
        double upperY;
        double lowerY;
        double center;
        private void WpfPlot1_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // previousY = 0;
            double height = WpfPlot1.ActualHeight;
            upperY = WpfPlot1.Plot.GetCoordinateY(0);
            lowerY = WpfPlot1.Plot.GetCoordinateY(((float)height - 20));
            center = (lowerY + upperY) / 2;

        }
        private void WpfPlot1_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            // ++
            if (_second10 < 0.02)
            {
                if (e.Delta < 0)
                {
                    _second60 += _second60 / 10;
                    _second10 += _second10 / 5;
                }
                change = _second60;
            }
            // --
            if (_second10 > 0.000071)
            {
                if(e.Delta > 0)
                {
                    _second60 -= _second60 / 10;
                    _second10 -= _second10 / 5;
                }
                change = _second60;
            }

            if(_second10 < 0.005) WpfPlot1.Plot.XAxis.TickLabelFormat("ss", true);
            else WpfPlot1.Plot.XAxis.TickLabelFormat("mm", true);

        }

        private void WpfPlot1_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            double height = this.ActualHeight;
            double width = this.ActualWidth;

            double x = e.GetPosition(this).X;
            double y = e.GetPosition(this).Y;

            double xResult = x / width;
            double yResult = y / height;

            X += ((xResult - prevCoordinates.x) * (change * changeRate) / 50000000 * time);
            Y += ((yResult - prevCoordinates.y) / 50 * price);
            IsMouseEnter = false;
        }
        private void WpfPlot1_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            IsMouseEnter = true;

            double height = this.ActualHeight;
            double width = this.ActualWidth;

            double x = e.GetPosition(this).X;
            double y = e.GetPosition(this).Y;

            double xResult = x / width;
            double yResult = y / height;
            prevCoordinates.x = xResult;
            prevCoordinates.y = yResult;
        }

        private async Task RunAsync()
        {
            await Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(1);
                    if (isRun)
                    {
                        WpfPlot1.Dispatcher.Invoke(new Action(() =>
                        {
                            if (!IsMouseEnter)
                            {
                                System.DateTime date = System.DateTime.UtcNow;
                                AutoAxis(date.ToOADate(), lower, upper);
                                WpfPlot1.Refresh();
                            }
                        }));
                    }
                }
            });
        }
        private void AutoAxis(double dateTime, double lowerDistance, double upperDistance)
        {
            if (!IsMouseEnter)
            {
                if (!check)
                {
                    check = true;
                    WpfPlot1.Plot.SetAxisLimitsY(yMin: (lowerDistance - (lowerDistance / 300) + (Y)), yMax: (upperDistance + (upperDistance / 300) + (Y)));
                }
                WpfPlot1.Plot.SetAxisLimitsX(xMin: (dateTime - _second60 - (X)), xMax: (dateTime + _second10 - (X)));
            }
        }

        public async void GetKlinesAsync()
        {
            await Task.Run(async () =>
            {
                try
                {
                    BinanceClient binanceClient = new BinanceClient();
                    var res2 = await binanceClient.UsdFuturesApi.ExchangeData.GetKlinesAsync("BTCUSDT", KlineInterval.OneMinute);
                    List<IBinanceKline> binanceKlines = res2.Data.ToList();
                    List<OHLC> prices = new();
                    int i = 0;
                    foreach (var item in binanceKlines)
                    {
                        if (i < (binanceKlines.Count - 1))
                        {
                            OHLC price = new(
                            open: (double)item.OpenPrice,
                            high: (double)item.HighPrice,
                            low: (double)item.LowPrice,
                            close: (double)item.ClosePrice,
                            timeStart: item.OpenTime,
                            timeSpan: TimeSpan.FromMinutes(1));
                            prices.Add(price);
                        }
                        i++;
                    }

                    OHLC[] oHLC = prices.ToArray();
                    Dispatcher.Invoke(new Action(() =>
                    {
                        WpfPlot1.Plot.AddCandlesticks(oHLC);
                        WpfPlot1.Refresh();
                    }));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
        }
    }
}
