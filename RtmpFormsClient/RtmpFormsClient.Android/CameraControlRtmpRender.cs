using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Net.Ossrs.Rtmp;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(RtmpFormsClient.CameraControlRtmp), typeof(RtmpFormsClient.Droid.CameraControlRtmpRender))]
namespace RtmpFormsClient.Droid
{
    public class CameraControlRtmpRender: ViewRenderer<RtmpFormsClient.CameraControlRtmp, RtmpFormsClient.Droid.CameraControlRtmp>, IConnectCheckerRtmp
    {
        CameraControlRtmp CameraSurfaceView;
        RtmpFormsClient.CameraControlRtmp CameraControl;

        public CameraControlRtmpRender(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<RtmpFormsClient.CameraControlRtmp> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {

            }
            if (e.NewElement != null)
            {
                CameraControl = (RtmpFormsClient.CameraControlRtmp)e.NewElement;
                CameraControl.OnStartStream += OnStartStream;
                CameraControl.OnStopStream += OnStopStream;
                if (Control == null)
                {
                    CameraSurfaceView = new CameraControlRtmp(Context,this);
                    SetNativeControl(CameraSurfaceView);
                }

            }
        }

        #region Events
        void OnStartStream(object sender, EventArgs e)
        {
            var stream = sender as RtmpFormsClient.CameraControlRtmp;
            string Url = stream.Url;
            if(stream.IsAzure)
            {
                Url = $"{stream.Url}/Default";
            }
            if (stream.StreamHeight != 0 && stream.StreamWidth != 0 && stream.Bitrate != 0)
            {
                stream.Bitrate = (stream.Bitrate >= 1200) ? stream.Bitrate : 1200;
                CameraSurfaceView.StarStreaming(Url, stream.StreamWidth, stream.StreamHeight, stream.Bitrate);
            }
            else
            {
                CameraSurfaceView.StarStreaming(Url);
            }
        }
        void OnStopStream(object sender, EventArgs e)
        {
            CameraSurfaceView.StopStreaming();
        }
        #endregion

        #region Notification IConnectCheckerRtmp
        public void OnConnectionSuccessRtmp()
        {
            CameraControl.StreamNotifier(StatusRtmp.StreamSucces);
        }

        public void OnAuthErrorRtmp()
        {
            CameraControl.StreamNotifier(StatusRtmp.StreamAuthError);
        }

        public void OnAuthSuccessRtmp()
        {
            CameraControl.StreamNotifier(StatusRtmp.StreamAuthSuccess);
        }

        public void OnConnectionFailedRtmp(string message)
        {
            CameraControl.StreamNotifier(StatusRtmp.StreamFaile);
        }

        public void OnDisconnectRtmp()
        {
            CameraControl.StreamNotifier(StatusRtmp.StreamDisconnect);
        }

        public void OnNewBitrateRtmp(long birrate)
        {
            CameraControl.StreamNotifier(StatusRtmp.StreamBitrate);
        }
        #endregion

    }
}