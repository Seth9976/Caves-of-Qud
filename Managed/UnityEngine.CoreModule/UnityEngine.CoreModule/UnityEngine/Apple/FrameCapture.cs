using System;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Apple
{
	// Token: 0x02000471 RID: 1137
	[NativeHeader("Runtime/Export/Apple/FrameCaptureMetalScriptBindings.h")]
	[NativeConditional("PLATFORM_APPLE")]
	public class FrameCapture
	{
		// Token: 0x06002CE1 RID: 11489 RVA: 0x00030B93 File Offset: 0x0002ED93
		private FrameCapture()
		{
		}

		// Token: 0x06002CE2 RID: 11490
		[FreeFunction("FrameCaptureMetalScripting::IsDestinationSupported")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsDestinationSupportedImpl(FrameCaptureDestination dest);

		// Token: 0x06002CE3 RID: 11491 RVA: 0x00054E30 File Offset: 0x00053030
		[FreeFunction("FrameCaptureMetalScripting::BeginCapture")]
		private unsafe static void BeginCaptureImpl(FrameCaptureDestination dest, string path)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(path, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = path.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				FrameCapture.BeginCaptureImpl_Injected(dest, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06002CE4 RID: 11492
		[FreeFunction("FrameCaptureMetalScripting::EndCapture")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EndCaptureImpl();

		// Token: 0x06002CE5 RID: 11493 RVA: 0x00054E88 File Offset: 0x00053088
		[FreeFunction("FrameCaptureMetalScripting::CaptureNextFrame")]
		private unsafe static void CaptureNextFrameImpl(FrameCaptureDestination dest, string path)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(path, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = path.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				FrameCapture.CaptureNextFrameImpl_Injected(dest, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06002CE6 RID: 11494 RVA: 0x00054EE0 File Offset: 0x000530E0
		public static bool IsDestinationSupported(FrameCaptureDestination dest)
		{
			bool flag = dest != FrameCaptureDestination.DevTools && dest != FrameCaptureDestination.GPUTraceDocument;
			if (flag)
			{
				throw new ArgumentException("dest", "Argument dest has bad value (not one of FrameCaptureDestination enum values)");
			}
			return FrameCapture.IsDestinationSupportedImpl(dest);
		}

		// Token: 0x06002CE7 RID: 11495 RVA: 0x00054F1C File Offset: 0x0005311C
		public static void BeginCaptureToXcode()
		{
			bool flag = !FrameCapture.IsDestinationSupported(FrameCaptureDestination.DevTools);
			if (flag)
			{
				throw new InvalidOperationException("Frame Capture with DevTools is not supported.");
			}
			FrameCapture.BeginCaptureImpl(FrameCaptureDestination.DevTools, null);
		}

		// Token: 0x06002CE8 RID: 11496 RVA: 0x00054F4C File Offset: 0x0005314C
		public static void BeginCaptureToFile(string path)
		{
			bool flag = !FrameCapture.IsDestinationSupported(FrameCaptureDestination.GPUTraceDocument);
			if (flag)
			{
				throw new InvalidOperationException("Frame Capture to file is not supported.");
			}
			bool flag2 = string.IsNullOrEmpty(path);
			if (flag2)
			{
				throw new ArgumentException("path", "Path must be supplied when capture destination is GPUTraceDocument.");
			}
			bool flag3 = Path.GetExtension(path) != ".gputrace";
			if (flag3)
			{
				throw new ArgumentException("path", "Destination file should have .gputrace extension.");
			}
			FrameCapture.BeginCaptureImpl(FrameCaptureDestination.GPUTraceDocument, new Uri(path).AbsoluteUri);
		}

		// Token: 0x06002CE9 RID: 11497 RVA: 0x00054FC2 File Offset: 0x000531C2
		public static void EndCapture()
		{
			FrameCapture.EndCaptureImpl();
		}

		// Token: 0x06002CEA RID: 11498 RVA: 0x00054FCC File Offset: 0x000531CC
		public static void CaptureNextFrameToXcode()
		{
			bool flag = !FrameCapture.IsDestinationSupported(FrameCaptureDestination.DevTools);
			if (flag)
			{
				throw new InvalidOperationException("Frame Capture with DevTools is not supported.");
			}
			FrameCapture.CaptureNextFrameImpl(FrameCaptureDestination.DevTools, null);
		}

		// Token: 0x06002CEB RID: 11499 RVA: 0x00054FFC File Offset: 0x000531FC
		public static void CaptureNextFrameToFile(string path)
		{
			bool flag = !FrameCapture.IsDestinationSupported(FrameCaptureDestination.GPUTraceDocument);
			if (flag)
			{
				throw new InvalidOperationException("Frame Capture to file is not supported.");
			}
			bool flag2 = string.IsNullOrEmpty(path);
			if (flag2)
			{
				throw new ArgumentException("path", "Path must be supplied when capture destination is GPUTraceDocument.");
			}
			bool flag3 = Path.GetExtension(path) != ".gputrace";
			if (flag3)
			{
				throw new ArgumentException("path", "Destination file should have .gputrace extension.");
			}
			FrameCapture.CaptureNextFrameImpl(FrameCaptureDestination.GPUTraceDocument, new Uri(path).AbsoluteUri);
		}

		// Token: 0x06002CEC RID: 11500
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BeginCaptureImpl_Injected(FrameCaptureDestination dest, ref ManagedSpanWrapper path);

		// Token: 0x06002CED RID: 11501
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CaptureNextFrameImpl_Injected(FrameCaptureDestination dest, ref ManagedSpanWrapper path);
	}
}
