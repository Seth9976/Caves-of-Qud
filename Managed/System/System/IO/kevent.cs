using System;
using System.Runtime.InteropServices;

namespace System.IO
{
	// Token: 0x02000520 RID: 1312
	internal struct kevent : IDisposable
	{
		// Token: 0x06002A5C RID: 10844 RVA: 0x0009190C File Offset: 0x0008FB0C
		public void Dispose()
		{
			if (this.udata != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.udata);
			}
		}

		// Token: 0x040016E1 RID: 5857
		public UIntPtr ident;

		// Token: 0x040016E2 RID: 5858
		public EventFilter filter;

		// Token: 0x040016E3 RID: 5859
		public EventFlags flags;

		// Token: 0x040016E4 RID: 5860
		public FilterFlags fflags;

		// Token: 0x040016E5 RID: 5861
		public IntPtr data;

		// Token: 0x040016E6 RID: 5862
		public IntPtr udata;
	}
}
