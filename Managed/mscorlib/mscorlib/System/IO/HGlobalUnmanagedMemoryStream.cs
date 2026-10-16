using System;
using System.Runtime.InteropServices;

namespace System.IO
{
	// Token: 0x02000B62 RID: 2914
	internal class HGlobalUnmanagedMemoryStream : UnmanagedMemoryStream
	{
		// Token: 0x060069CC RID: 27084 RVA: 0x001697EF File Offset: 0x001679EF
		public unsafe HGlobalUnmanagedMemoryStream(byte* pointer, long length, IntPtr ptr)
			: base(pointer, length, length, FileAccess.ReadWrite)
		{
			this.ptr = ptr;
		}

		// Token: 0x060069CD RID: 27085 RVA: 0x00169802 File Offset: 0x00167A02
		protected override void Dispose(bool disposing)
		{
			if (this._isOpen)
			{
				Marshal.FreeHGlobal(this.ptr);
			}
			base.Dispose(disposing);
		}

		// Token: 0x04003D4D RID: 15693
		private IntPtr ptr;
	}
}
