using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

public enum StatusRtmp
{
    StreamSucces,
    StreamDisconnect,
    StreamFaile,
    StreamAuthSuccess,
    StreamAuthError,
    StreamBitrate

}
namespace RtmpFormsClient
{
    public class CameraControlRtmp : View
    {
        public static BindableProperty UrlTextProperty =
        BindableProperty.Create(nameof(UrlText), typeof(string), typeof(string));
        public static BindableProperty BitrateProperty =
        BindableProperty.Create(nameof(Bitrate), typeof(int), typeof(int));
        public static BindableProperty WidthStreamProperty =
        BindableProperty.Create(nameof(StreamWidth), typeof(int), typeof(int));
        public static BindableProperty HeightStreamProperty =
        BindableProperty.Create(nameof(StreamHeight), typeof(int), typeof(int));
        public static BindableProperty IsAzureProperty =
            BindableProperty.Create(nameof(IsAzure), typeof(bool), typeof(bool));

        #region Properties
        public string UrlText
        {
            get => (string)GetValue(UrlTextProperty);
            set => SetValue(UrlTextProperty, value);
        }
        public int Bitrate
        {
            get => (int)GetValue(BitrateProperty);
            set => SetValue(BitrateProperty, value);
        }
        public int StreamWidth
        {
            get => (int)GetValue(WidthStreamProperty);
            set => SetValue(WidthStreamProperty, value);
        }

        public int StreamHeight
        {
            get => (int)GetValue(HeightStreamProperty);
            set => SetValue(HeightStreamProperty,value);
        }
        public bool IsAzure
        {
            get => (bool)GetValue(IsAzureProperty);
            set => SetValue(IsAzureProperty, value);
        }
        #endregion

        #region Methods
        public void StarStream()
        {
            var Handler = OnStartStream;
            Handler?.Invoke(this, EventArgs.Empty);
        }
        public void StopStream()
        {
            var Handler = OnStopStream;
            Handler?.Invoke(this, EventArgs.Empty);
        }
        public void StreamNotifier(StatusRtmp statusRtmp)
        {
           EventHandler Handler;
            switch (statusRtmp)
            {
                case StatusRtmp.StreamSucces:
                    Handler = OnConnectionSuccesStream;
                    break;
                case StatusRtmp.StreamFaile:
                    Handler = OnConectionFailStream;
                    break;
                case StatusRtmp.StreamAuthError:
                    Handler = OnAuthErrorStream;
                    break;
                case StatusRtmp.StreamAuthSuccess:
                    Handler = OnAuthSuccessStream;
                    break;
                case StatusRtmp.StreamBitrate:
                    Handler = OnNewBitrateStream;
                    break;
                case StatusRtmp.StreamDisconnect:
                    Handler = OnDisconnectStream;
                    break;
                default:
                    Handler = OnDisconnectStream;
                    break;
            }
            Handler?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region Events
        public event EventHandler OnStartStream;
        public event EventHandler OnStopStream;

        #endregion
        #region Events IConnectCheckerRtmp
        public event EventHandler OnConnectionSuccesStream;
        public event EventHandler OnDisconnectStream;
        public event EventHandler OnAuthSuccessStream;
        public event EventHandler OnAuthErrorStream;
        public event EventHandler OnNewBitrateStream;
        public event EventHandler OnConectionFailStream;
        #endregion       
    }
}
