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
        BindableProperty.Create(nameof(WidthStream), typeof(int), typeof(int));
        public static BindableProperty HeightStreamProperty =
        BindableProperty.Create(nameof(HeightStream), typeof(int), typeof(int));

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
        public int WidthStream
        {
            get => (int)GetValue(WidthStreamProperty);
            set => SetValue(WidthStreamProperty, value);
        }

        public int HeightStream
        {
            get => (int)GetValue(HeightStreamProperty);
            set => SetValue(HeightStreamProperty,value);
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
        public void ConnectionFailedRtmp(string message)
        {
            var Handler = OnConectionFailStream;
            Handler?.Invoke(this, EventArgs.Empty);
        }
        public void NewBitrateRtmp(long bitrate)
        {
            var Handler = OnNewBitrateStream;
            Handler?.Invoke(this, EventArgs.Empty);
        }
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
