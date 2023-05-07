using Binance.Net.Clients;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace SimpleChart.Models
{
    internal class Symbol 
    {

        public Symbol(string symbolName)
        {
            SymbolName = symbolName;
            //SubscribeToMarkPriceUpdatesAsync();
        }

        public string SymbolName { get; set; }
        public decimal SymbolPrice { get; set; }

        public override string ToString()
        {
            return SymbolName+ " " + SymbolPrice;
        }

        private async void SubscribeToMarkPriceUpdatesAsync()
        {

            await Task.Run(async () =>
            {
                BinanceSocketClient socketClient = new BinanceSocketClient();
                try
                {
                   
                    var res1 = await socketClient.UsdFuturesStreams.SubscribeToMarkPriceUpdatesAsync(SymbolName, updateInterval: 1000, message =>
                    {
                        SymbolPrice = message.Data.MarkPrice;

                    });
                    if (!res1.Success)
                    {
                        MessageBox.Show(res1.Error.Message);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
        }
    }
}
