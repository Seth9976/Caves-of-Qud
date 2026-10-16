using System;
using System.Runtime.InteropServices;

namespace Mono.Net
{
	// Token: 0x02000080 RID: 128
	internal class CFUrl : CFObject
	{
		// Token: 0x060001DD RID: 477 RVA: 0x00002474 File Offset: 0x00000674
		public CFUrl(IntPtr handle, bool own)
			: base(handle, own)
		{
		}

		// Token: 0x060001DE RID: 478
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFURLCreateWithString(IntPtr allocator, IntPtr str, IntPtr baseURL);

		// Token: 0x060001DF RID: 479 RVA: 0x000055E0 File Offset: 0x000037E0
		public static CFUrl Create(string absolute)
		{
			if (string.IsNullOrEmpty(absolute))
			{
				return null;
			}
			CFString cfstring = CFString.Create(absolute);
			IntPtr intPtr = CFUrl.CFURLCreateWithString(IntPtr.Zero, cfstring.Handle, IntPtr.Zero);
			cfstring.Dispose();
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			return new CFUrl(intPtr, true);
		}
	}
}
