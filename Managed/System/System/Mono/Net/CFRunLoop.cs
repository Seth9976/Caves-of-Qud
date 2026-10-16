using System;
using System.Runtime.InteropServices;

namespace Mono.Net
{
	// Token: 0x02000081 RID: 129
	internal class CFRunLoop : CFObject
	{
		// Token: 0x060001E0 RID: 480
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern void CFRunLoopAddSource(IntPtr rl, IntPtr source, IntPtr mode);

		// Token: 0x060001E1 RID: 481
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern void CFRunLoopRemoveSource(IntPtr rl, IntPtr source, IntPtr mode);

		// Token: 0x060001E2 RID: 482
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern int CFRunLoopRunInMode(IntPtr mode, double seconds, bool returnAfterSourceHandled);

		// Token: 0x060001E3 RID: 483
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFRunLoopGetCurrent();

		// Token: 0x060001E4 RID: 484
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern void CFRunLoopStop(IntPtr rl);

		// Token: 0x060001E5 RID: 485 RVA: 0x00002474 File Offset: 0x00000674
		public CFRunLoop(IntPtr handle, bool own)
			: base(handle, own)
		{
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00005630 File Offset: 0x00003830
		public static CFRunLoop CurrentRunLoop
		{
			get
			{
				return new CFRunLoop(CFRunLoop.CFRunLoopGetCurrent(), false);
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000563D File Offset: 0x0000383D
		public void AddSource(IntPtr source, CFString mode)
		{
			CFRunLoop.CFRunLoopAddSource(base.Handle, source, mode.Handle);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00005651 File Offset: 0x00003851
		public void RemoveSource(IntPtr source, CFString mode)
		{
			CFRunLoop.CFRunLoopRemoveSource(base.Handle, source, mode.Handle);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00005665 File Offset: 0x00003865
		public int RunInMode(CFString mode, double seconds, bool returnAfterSourceHandled)
		{
			return CFRunLoop.CFRunLoopRunInMode(mode.Handle, seconds, returnAfterSourceHandled);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00005674 File Offset: 0x00003874
		public void Stop()
		{
			CFRunLoop.CFRunLoopStop(base.Handle);
		}
	}
}
