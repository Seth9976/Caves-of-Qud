using System;
using System.Runtime.InteropServices;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000042 RID: 66
	internal sealed class SafeBrotliDecoderHandle : SafeHandle
	{
		// Token: 0x060001E3 RID: 483 RVA: 0x00009D69 File Offset: 0x00007F69
		public SafeBrotliDecoderHandle()
			: base(IntPtr.Zero, true)
		{
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00009D97 File Offset: 0x00007F97
		protected override bool ReleaseHandle()
		{
			Interop.Brotli.BrotliDecoderDestroyInstance(this.handle);
			return true;
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00009D85 File Offset: 0x00007F85
		public override bool IsInvalid
		{
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}
	}
}
