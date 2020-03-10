using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RtmpFormsClient
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Button.Clicked += (sender, e) =>
            {
                Dan.UrlText = URL.Text;
                Dan.StarStream();
            };
            ButtonStop.Clicked += (sender, e) =>
            {
                Dan.StopStream();
            };
            Dan.OnConnectionSuccesStream += OnStartStream;
        }

        public void OnStartStream(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() => { URL.Text = "Success Stream"; });
        }
    }
}
