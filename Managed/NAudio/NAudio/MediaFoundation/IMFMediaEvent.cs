using System;
using System.Runtime.InteropServices;
using System.Text;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.MediaFoundation
{
	// Token: 0x0200004A RID: 74
	[InterfaceType(1)]
	[Guid("DF598932-F10C-4E39-BBA2-C308F101DAA3")]
	[ComImport]
	public interface IMFMediaEvent : IMFAttributes
	{
		// Token: 0x06000171 RID: 369
		void GetItem([MarshalAs(43)] [In] Guid guidKey, [In] [Out] ref PropVariant pValue);

		// Token: 0x06000172 RID: 370
		void GetItemType([MarshalAs(43)] [In] Guid guidKey, out int pType);

		// Token: 0x06000173 RID: 371
		void CompareItem([MarshalAs(43)] [In] Guid guidKey, IntPtr Value, [MarshalAs(2)] out bool pbResult);

		// Token: 0x06000174 RID: 372
		void Compare([MarshalAs(28)] IMFAttributes pTheirs, int MatchType, [MarshalAs(2)] out bool pbResult);

		// Token: 0x06000175 RID: 373
		void GetUINT32([MarshalAs(43)] [In] Guid guidKey, out int punValue);

		// Token: 0x06000176 RID: 374
		void GetUINT64([MarshalAs(43)] [In] Guid guidKey, out long punValue);

		// Token: 0x06000177 RID: 375
		void GetDouble([MarshalAs(43)] [In] Guid guidKey, out double pfValue);

		// Token: 0x06000178 RID: 376
		void GetGUID([MarshalAs(43)] [In] Guid guidKey, out Guid pguidValue);

		// Token: 0x06000179 RID: 377
		void GetStringLength([MarshalAs(43)] [In] Guid guidKey, out int pcchLength);

		// Token: 0x0600017A RID: 378
		void GetString([MarshalAs(43)] [In] Guid guidKey, [MarshalAs(21)] [Out] StringBuilder pwszValue, int cchBufSize, out int pcchLength);

		// Token: 0x0600017B RID: 379
		void GetAllocatedString([MarshalAs(43)] [In] Guid guidKey, [MarshalAs(21)] out string ppwszValue, out int pcchLength);

		// Token: 0x0600017C RID: 380
		void GetBlobSize([MarshalAs(43)] [In] Guid guidKey, out int pcbBlobSize);

		// Token: 0x0600017D RID: 381
		void GetBlob([MarshalAs(43)] [In] Guid guidKey, [MarshalAs(42)] [Out] byte[] pBuf, int cbBufSize, out int pcbBlobSize);

		// Token: 0x0600017E RID: 382
		void GetAllocatedBlob([MarshalAs(43)] [In] Guid guidKey, out IntPtr ip, out int pcbSize);

		// Token: 0x0600017F RID: 383
		void GetUnknown([MarshalAs(43)] [In] Guid guidKey, [MarshalAs(43)] [In] Guid riid, [MarshalAs(25)] out object ppv);

		// Token: 0x06000180 RID: 384
		void SetItem([MarshalAs(43)] [In] Guid guidKey, IntPtr Value);

		// Token: 0x06000181 RID: 385
		void DeleteItem([MarshalAs(43)] [In] Guid guidKey);

		// Token: 0x06000182 RID: 386
		void DeleteAllItems();

		// Token: 0x06000183 RID: 387
		void SetUINT32([MarshalAs(43)] [In] Guid guidKey, int unValue);

		// Token: 0x06000184 RID: 388
		void SetUINT64([MarshalAs(43)] [In] Guid guidKey, long unValue);

		// Token: 0x06000185 RID: 389
		void SetDouble([MarshalAs(43)] [In] Guid guidKey, double fValue);

		// Token: 0x06000186 RID: 390
		void SetGUID([MarshalAs(43)] [In] Guid guidKey, [MarshalAs(43)] [In] Guid guidValue);

		// Token: 0x06000187 RID: 391
		void SetString([MarshalAs(43)] [In] Guid guidKey, [MarshalAs(21)] [In] string wszValue);

		// Token: 0x06000188 RID: 392
		void SetBlob([MarshalAs(43)] [In] Guid guidKey, [MarshalAs(42, SizeParamIndex = 2)] [In] byte[] pBuf, int cbBufSize);

		// Token: 0x06000189 RID: 393
		void SetUnknown([MarshalAs(43)] Guid guidKey, [MarshalAs(25)] [In] object pUnknown);

		// Token: 0x0600018A RID: 394
		void LockStore();

		// Token: 0x0600018B RID: 395
		void UnlockStore();

		// Token: 0x0600018C RID: 396
		void GetCount(out int pcItems);

		// Token: 0x0600018D RID: 397
		void GetItemByIndex(int unIndex, out Guid pGuidKey, [In] [Out] ref PropVariant pValue);

		// Token: 0x0600018E RID: 398
		void CopyAllItems([MarshalAs(28)] [In] IMFAttributes pDest);

		// Token: 0x0600018F RID: 399
		void GetType(out MediaEventType pmet);

		// Token: 0x06000190 RID: 400
		void GetExtendedType(out Guid pguidExtendedType);

		// Token: 0x06000191 RID: 401
		void GetStatus([MarshalAs(45)] out int phrStatus);

		// Token: 0x06000192 RID: 402
		void GetValue(out PropVariant pvValue);
	}
}
