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
using Net.Ossrs.Rtmp;
using Com.Pedro.Rtplibrary.Rtmp;
using Com.Pedro.Encoder.Input.Video;

namespace RtmpFormsClient.Droid
{
    public sealed class CameraControlRtmp : ViewGroup, ISurfaceHolderCallback
	{
        SurfaceView surfaceView;
        ISurfaceHolder holder;
        RtmpCamera2 RtmpCamera2;
		Context Context;

		public bool IsPreviewing { get; set; }


		public CameraControlRtmp(Context context, IConnectCheckerRtmp connectCheckerRtmp) : base(context)
		{
			Context = context;
			surfaceView = new SurfaceView(context);
			AddView(surfaceView);
			IsPreviewing = true;
			RtmpCamera2 = new RtmpCamera2(surfaceView, connectCheckerRtmp);
			RtmpCamera2.SetReTries(10);
			holder = surfaceView.Holder;
			holder.AddCallback(this);
		}

		protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
		{
			int width = ResolveSize(SuggestedMinimumWidth, widthMeasureSpec);
			int height = ResolveSize(SuggestedMinimumHeight, heightMeasureSpec);
			SetMeasuredDimension(width, height);
		}

		protected override void OnLayout(bool changed, int l, int t, int r, int b)
		{
			var msw = MeasureSpec.MakeMeasureSpec(r - l, MeasureSpecMode.Exactly);
			var msh = MeasureSpec.MakeMeasureSpec(b - t, MeasureSpecMode.Exactly);

			surfaceView.Measure(msw, msh);
			surfaceView.Layout(0, 0, r - l, b - t);
		}


		#region Implementar la Interface ISurfaceHolderCallback
		public void SurfaceCreated(ISurfaceHolder holder)
		{
			RtmpCamera2.StartPreview();
		}

		public void SurfaceDestroyed(ISurfaceHolder holder)
		{
			if (RtmpCamera2.IsStreaming)
			{
				RtmpCamera2.StopStream();
			}
			RtmpCamera2.StopPreview();
		}

		public void SurfaceChanged(ISurfaceHolder holder, Android.Graphics.Format format, int width, int height)
		{
			RtmpCamera2.StartPreview();
		}
		#endregion

		#region Methods Streaming

		public void StarStreaming(string Url)
		{
			// Hay audio y video?
			if (RtmpCamera2.PrepareAudio() && RtmpCamera2.PrepareVideo())
			{

				RtmpCamera2.StartStream(Url);
			}
		}

		public void StartStreaming(string Url,int width,int height,int bitrate)
		{
			// Hay audio y video?
			if (RtmpCamera2.PrepareAudio() && RtmpCamera2.PrepareVideo(width,height,30,(bitrate*1200),false, CameraHelper.GetCameraOrientation(Context)))
			{

				RtmpCamera2.StartStream(Url);
			}
		}

		public void StopStreaming()
		{
			if (RtmpCamera2.IsStreaming)
			{
				try
				{
					RtmpCamera2.StopStream();
				}
				catch (Exception)
				{

				}
			}
		}
		#endregion
	}
}