using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000002 RID: 2
	[NativeHeader("Modules/ScreenCapture/Public/CaptureScreenshot.h")]
	public static class ScreenCapture
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static void CaptureScreenshot(string filename)
		{
			ScreenCapture.CaptureScreenshot(filename, 1, ScreenCapture.StereoScreenCaptureMode.LeftEye);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000205C File Offset: 0x0000025C
		public static void CaptureScreenshot(string filename, int superSize)
		{
			ScreenCapture.CaptureScreenshot(filename, superSize, ScreenCapture.StereoScreenCaptureMode.LeftEye);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002068 File Offset: 0x00000268
		public static void CaptureScreenshot(string filename, ScreenCapture.StereoScreenCaptureMode stereoCaptureMode)
		{
			ScreenCapture.CaptureScreenshot(filename, 1, stereoCaptureMode);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002074 File Offset: 0x00000274
		public static Texture2D CaptureScreenshotAsTexture()
		{
			return ScreenCapture.CaptureScreenshotAsTexture(1, ScreenCapture.StereoScreenCaptureMode.LeftEye);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002090 File Offset: 0x00000290
		public static Texture2D CaptureScreenshotAsTexture(int superSize)
		{
			return ScreenCapture.CaptureScreenshotAsTexture(superSize, ScreenCapture.StereoScreenCaptureMode.LeftEye);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020AC File Offset: 0x000002AC
		public static Texture2D CaptureScreenshotAsTexture(ScreenCapture.StereoScreenCaptureMode stereoCaptureMode)
		{
			return ScreenCapture.CaptureScreenshotAsTexture(1, stereoCaptureMode);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020C8 File Offset: 0x000002C8
		public static void CaptureScreenshotIntoRenderTexture(RenderTexture renderTexture)
		{
			ScreenCapture.CaptureScreenshotIntoRenderTexture_Injected(Object.MarshalledUnityObject.Marshal<RenderTexture>(renderTexture));
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020E0 File Offset: 0x000002E0
		private unsafe static void CaptureScreenshot(string filename, [DefaultValue("1")] int superSize, [DefaultValue("1")] ScreenCapture.StereoScreenCaptureMode CaptureMode)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(filename, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = filename.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ScreenCapture.CaptureScreenshot_Injected(ref managedSpanWrapper, superSize, CaptureMode);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002138 File Offset: 0x00000338
		private static Texture2D CaptureScreenshotAsTexture(int superSize, ScreenCapture.StereoScreenCaptureMode stereoScreenCaptureMode)
		{
			return Unmarshal.UnmarshalUnityObject<Texture2D>(ScreenCapture.CaptureScreenshotAsTexture_Injected(superSize, stereoScreenCaptureMode));
		}

		// Token: 0x0600000A RID: 10
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CaptureScreenshotIntoRenderTexture_Injected(IntPtr renderTexture);

		// Token: 0x0600000B RID: 11
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CaptureScreenshot_Injected(ref ManagedSpanWrapper filename, [DefaultValue("1")] int superSize, [DefaultValue("1")] ScreenCapture.StereoScreenCaptureMode CaptureMode);

		// Token: 0x0600000C RID: 12
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr CaptureScreenshotAsTexture_Injected(int superSize, ScreenCapture.StereoScreenCaptureMode stereoScreenCaptureMode);

		// Token: 0x02000003 RID: 3
		public enum StereoScreenCaptureMode
		{
			// Token: 0x04000002 RID: 2
			LeftEye = 1,
			// Token: 0x04000003 RID: 3
			RightEye,
			// Token: 0x04000004 RID: 4
			BothEyes,
			// Token: 0x04000005 RID: 5
			MotionVectors
		}
	}
}
