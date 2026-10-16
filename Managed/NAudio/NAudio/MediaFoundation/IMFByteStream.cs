using System;
using System.Runtime.InteropServices;

namespace NAudio.MediaFoundation
{
	// Token: 0x02000051 RID: 81
	[InterfaceType(1)]
	[Guid("ad4c1b00-4bf7-422f-9175-756693d9130d")]
	[ComImport]
	public interface IMFByteStream
	{
		// Token: 0x060001CE RID: 462
		void GetCapabilities(ref int pdwCapabiities);

		// Token: 0x060001CF RID: 463
		void GetLength(ref long pqwLength);

		// Token: 0x060001D0 RID: 464
		void SetLength(long qwLength);

		// Token: 0x060001D1 RID: 465
		void GetCurrentPosition(ref long pqwPosition);

		// Token: 0x060001D2 RID: 466
		void SetCurrentPosition(long qwPosition);

		// Token: 0x060001D3 RID: 467
		void IsEndOfStream([MarshalAs(2)] ref bool pfEndOfStream);

		// Token: 0x060001D4 RID: 468
		void Read(IntPtr pb, int cb, ref int pcbRead);

		// Token: 0x060001D5 RID: 469
		void BeginRead(IntPtr pb, int cb, IntPtr pCallback, IntPtr punkState);

		// Token: 0x060001D6 RID: 470
		void EndRead(IntPtr pResult, ref int pcbRead);

		// Token: 0x060001D7 RID: 471
		void Write(IntPtr pb, int cb, ref int pcbWritten);

		// Token: 0x060001D8 RID: 472
		void BeginWrite(IntPtr pb, int cb, IntPtr pCallback, IntPtr punkState);

		// Token: 0x060001D9 RID: 473
		void EndWrite(IntPtr pResult, ref int pcbWritten);

		// Token: 0x060001DA RID: 474
		void Seek(int SeekOrigin, long llSeekOffset, int dwSeekFlags, ref long pqwCurrentPosition);

		// Token: 0x060001DB RID: 475
		void Flush();

		// Token: 0x060001DC RID: 476
		void Close();
	}
}
