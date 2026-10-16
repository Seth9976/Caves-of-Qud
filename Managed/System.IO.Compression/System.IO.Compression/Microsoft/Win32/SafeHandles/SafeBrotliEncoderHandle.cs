using System;
using System.Runtime.InteropServices;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000041 RID: 65
	internal sealed class SafeBrotliEncoderHandle : SafeHandle
	{
		// Token: 0x060001E0 RID: 480 RVA: 0x00009D69 File Offset: 0x00007F69
		public SafeBrotliEncoderHandle()
			: base(IntPtr.Zero, true)
		{
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00009D77 File Offset: 0x00007F77
		protected override bool ReleaseHandle()
		{
			Interop.Brotli.BrotliEncoderDestroyInstance(this.handle);
			return true;
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00009D85 File Offset: 0x00007F85
		public override bool IsInvalid
		{
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}
	}
}
