using System;
using System.Runtime.InteropServices;
using System.Text;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.MediaFoundation
{
	// Token: 0x02000047 RID: 71
	[InterfaceType(1)]
	[Guid("2CD2D921-C447-44A7-A13C-4ADABFC247E3")]
	[ComImport]
	public interface IMFAttributes
	{
		// Token: 0x0600012C RID: 300
		void GetItem([MarshalAs(43)] [In] Guid guidKey, [In] [Out] ref PropVariant pValue);

		// Token: 0x0600012D RID: 301
		void GetItemType([MarshalAs(43)] [In] Guid guidKey, out int pType);

		// Token: 0x0600012E RID: 302
		void CompareItem([MarshalAs(43)] [In] Guid guidKey, IntPtr Value, [MarshalAs(2)] out bool pbResult);

		// Token: 0x0600012F RID: 303
		void Compare([MarshalAs(28)] IMFAttributes pTheirs, int MatchType, [MarshalAs(2)] out bool pbResult);

		// Token: 0x06000130 RID: 304
		void GetUINT32([MarshalAs(43)] [In] Guid guidKey, out int punValue);

		// Token: 0x06000131 RID: 305
		void GetUINT64([MarshalAs(43)] [In] Guid guidKey, out long punValue);

		// Token: 0x06000132 RID: 306
		void GetDouble([MarshalAs(43)] [In] Guid guidKey, out double pfValue);

		// Token: 0x06000133 RID: 307
		void GetGUID([MarshalAs(43)] [In] Guid guidKey, out Guid pguidValue);

		// Token: 0x06000134 RID: 308
		void GetStringLength([MarshalAs(43)] [In] Guid guidKey, out int pcchLength);

		// Token: 0x06000135 RID: 309
		void GetString([MarshalAs(43)] [In] Guid guidKey, [MarshalAs(21)] [Out] StringBuilder pwszValue, int cchBufSize, out int pcchLength);

		// Token: 0x06000136 RID: 310
		void GetAllocatedString([MarshalAs(43)] [In] Guid guidKey, [MarshalAs(21)] out string ppwszValue, out int pcchLength);

		// Token: 0x06000137 RID: 311
		void GetBlobSize([MarshalAs(43)] [In] Guid guidKey, out int pcbBlobSize);

		// Token: 0x06000138 RID: 312
		void GetBlob([MarshalAs(43)] [In] Guid guidKey, [MarshalAs(42)] [Out] byte[] pBuf, int cbBufSize, out int pcbBlobSize);

		// Token: 0x06000139 RID: 313
		void GetAllocatedBlob([MarshalAs(43)] [In] Guid guidKey, out IntPtr ip, out int pcbSize);

		// Token: 0x0600013A RID: 314
		void GetUnknown([MarshalAs(43)] [In] Guid guidKey, [MarshalAs(43)] [In] Guid riid, [MarshalAs(25)] out object ppv);

		// Token: 0x0600013B RID: 315
		void SetItem([MarshalAs(43)] [In] Guid guidKey, IntPtr Value);

		// Token: 0x0600013C RID: 316
		void DeleteItem([MarshalAs(43)] [In] Guid guidKey);

		// Token: 0x0600013D RID: 317
		void DeleteAllItems();

		// Token: 0x0600013E RID: 318
		void SetUINT32([MarshalAs(43)] [In] Guid guidKey, int unValue);

		// Token: 0x0600013F RID: 319
		void SetUINT64([MarshalAs(43)] [In] Guid guidKey, long unValue);

		// Token: 0x06000140 RID: 320
		void SetDouble([MarshalAs(43)] [In] Guid guidKey, double fValue);

		// Token: 0x06000141 RID: 321
		void SetGUID([MarshalAs(43)] [In] Guid guidKey, [MarshalAs(43)] [In] Guid guidValue);

		// Token: 0x06000142 RID: 322
		void SetString([MarshalAs(43)] [In] Guid guidKey, [MarshalAs(21)] [In] string wszValue);

		// Token: 0x06000143 RID: 323
		void SetBlob([MarshalAs(43)] [In] Guid guidKey, [MarshalAs(42, SizeParamIndex = 2)] [In] byte[] pBuf, int cbBufSize);

		// Token: 0x06000144 RID: 324
		void SetUnknown([MarshalAs(43)] Guid guidKey, [MarshalAs(25)] [In] object pUnknown);

		// Token: 0x06000145 RID: 325
		void LockStore();

		// Token: 0x06000146 RID: 326
		void UnlockStore();

		// Token: 0x06000147 RID: 327
		void GetCount(out int pcItems);

		// Token: 0x06000148 RID: 328
		void GetItemByIndex(int unIndex, out Guid pGuidKey, [In] [Out] ref PropVariant pValue);

		// Token: 0x06000149 RID: 329
		void CopyAllItems([MarshalAs(28)] [In] IMFAttributes pDest);
	}
}
