using System;
using System.Linq;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Windows.WebCam
{
	// Token: 0x02000338 RID: 824
	[UsedByNativeCode]
	[NativeHeader("PlatformDependent/Win/Webcam/CameraParameters.h")]
	[MovedFrom("UnityEngine.XR.WSA.WebCam")]
	public struct CameraParameters
	{
		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x060027A7 RID: 10151 RVA: 0x0004C048 File Offset: 0x0004A248
		// (set) Token: 0x060027A8 RID: 10152 RVA: 0x0004C060 File Offset: 0x0004A260
		public float hologramOpacity
		{
			get
			{
				return this.m_HologramOpacity;
			}
			set
			{
				this.m_HologramOpacity = value;
			}
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x060027A9 RID: 10153 RVA: 0x0004C06C File Offset: 0x0004A26C
		// (set) Token: 0x060027AA RID: 10154 RVA: 0x0004C084 File Offset: 0x0004A284
		public float frameRate
		{
			get
			{
				return this.m_FrameRate;
			}
			set
			{
				this.m_FrameRate = value;
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x060027AB RID: 10155 RVA: 0x0004C090 File Offset: 0x0004A290
		// (set) Token: 0x060027AC RID: 10156 RVA: 0x0004C0A8 File Offset: 0x0004A2A8
		public int cameraResolutionWidth
		{
			get
			{
				return this.m_CameraResolutionWidth;
			}
			set
			{
				this.m_CameraResolutionWidth = value;
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x060027AD RID: 10157 RVA: 0x0004C0B4 File Offset: 0x0004A2B4
		// (set) Token: 0x060027AE RID: 10158 RVA: 0x0004C0CC File Offset: 0x0004A2CC
		public int cameraResolutionHeight
		{
			get
			{
				return this.m_CameraResolutionHeight;
			}
			set
			{
				this.m_CameraResolutionHeight = value;
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x060027AF RID: 10159 RVA: 0x0004C0D8 File Offset: 0x0004A2D8
		// (set) Token: 0x060027B0 RID: 10160 RVA: 0x0004C0F0 File Offset: 0x0004A2F0
		public CapturePixelFormat pixelFormat
		{
			get
			{
				return this.m_PixelFormat;
			}
			set
			{
				this.m_PixelFormat = value;
			}
		}

		// Token: 0x060027B1 RID: 10161 RVA: 0x0004C0FC File Offset: 0x0004A2FC
		public CameraParameters(WebCamMode webCamMode)
		{
			this.m_HologramOpacity = 1f;
			this.m_PixelFormat = CapturePixelFormat.BGRA32;
			this.m_FrameRate = 0f;
			this.m_CameraResolutionWidth = 0;
			this.m_CameraResolutionHeight = 0;
			bool flag = webCamMode == WebCamMode.PhotoMode;
			if (flag)
			{
				Resolution resolution = PhotoCapture.SupportedResolutions.OrderByDescending<Resolution, int>((Resolution res) => res.width * res.height).First<Resolution>();
				this.m_CameraResolutionWidth = resolution.width;
				this.m_CameraResolutionHeight = resolution.height;
			}
			else
			{
				bool flag2 = webCamMode == WebCamMode.VideoMode;
				if (flag2)
				{
					Resolution resolution2 = VideoCapture.SupportedResolutions.OrderByDescending<Resolution, int>((Resolution res) => res.width * res.height).First<Resolution>();
					float num = (from fps in VideoCapture.GetSupportedFrameRatesForResolution(resolution2)
						orderby fps descending
						select fps).First<float>();
					this.m_CameraResolutionWidth = resolution2.width;
					this.m_CameraResolutionHeight = resolution2.height;
					this.m_FrameRate = num;
				}
			}
		}

		// Token: 0x04000B1D RID: 2845
		private float m_HologramOpacity;

		// Token: 0x04000B1E RID: 2846
		private float m_FrameRate;

		// Token: 0x04000B1F RID: 2847
		private int m_CameraResolutionWidth;

		// Token: 0x04000B20 RID: 2848
		private int m_CameraResolutionHeight;

		// Token: 0x04000B21 RID: 2849
		private CapturePixelFormat m_PixelFormat;
	}
}
