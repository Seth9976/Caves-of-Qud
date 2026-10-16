using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Microsoft.DiaSymReader
{
	// Token: 0x020000F7 RID: 247
	[Guid("0000000c-0000-0000-C000-000000000046")]
	[InterfaceType(1)]
	[ComImport]
	internal interface IUnsafeComStream
	{
		// Token: 0x06000877 RID: 2167
		unsafe void Read(byte* pv, int cb, int* pcbRead);

		// Token: 0x06000878 RID: 2168
		unsafe void Write(byte* pv, int cb, int* pcbWritten);

		// Token: 0x06000879 RID: 2169
		unsafe void Seek(long dlibMove, int dwOrigin, long* plibNewPosition);

		// Token: 0x0600087A RID: 2170
		void SetSize(long libNewSize);

		// Token: 0x0600087B RID: 2171
		unsafe void CopyTo(IStream pstm, long cb, int* pcbRead, int* pcbWritten);

		// Token: 0x0600087C RID: 2172
		void Commit(int grfCommitFlags);

		// Token: 0x0600087D RID: 2173
		void Revert();

		// Token: 0x0600087E RID: 2174
		void LockRegion(long libOffset, long cb, int dwLockType);

		// Token: 0x0600087F RID: 2175
		void UnlockRegion(long libOffset, long cb, int dwLockType);

		// Token: 0x06000880 RID: 2176
		void Stat(out STATSTG pstatstg, int grfStatFlag);

		// Token: 0x06000881 RID: 2177
		void Clone(out IStream ppstm);
	}
}
