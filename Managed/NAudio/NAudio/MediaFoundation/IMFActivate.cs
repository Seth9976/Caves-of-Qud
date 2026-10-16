using System;
using System.Runtime.InteropServices;
using System.Text;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.MediaFoundation
{
	// Token: 0x02000048 RID: 72
	[InterfaceType(1)]
	[Guid("7FEE9E9A-4A89-47a6-899C-B6A53A70FB67")]
	[ComImport]
	public interface IMFActivate : IMFAttributes
	{
		// Token: 0x0600014A RID: 330
		void GetItem([MarshalAs(43)] [In] Guid guidKey, [In] [Out] ref PropVariant pValue);

		// Token: 0x0600014B RID: 331
		void GetItemType([MarshalAs(43)] [In] Guid guidKey, out int pType);

		// Token: 0x0600014C RID: 332
		void CompareItem([MarshalAs(43)] [In] Guid guidKey, IntPtr Value, [MarshalAs(2)] out bool pbResult);

		// Token: 0x0600014D RID: 333
		void Compare([MarshalAs(28)] IMFAttributes pTheirs, int MatchType, [MarshalAs(2)] out bool pbResult);

		// Token: 0x0600014E RID: 334
		void GetUINT32([MarshalAs(43)] [In] Guid guidKey, out int punValue);

		// Token: 0x0600014F RID: 335
		void GetUINT64([MarshalAs(43)] [In] Guid guidKey, out long punValue);

		// Token: 0x06000150 RID: 336
		void GetDouble([MarshalAs(43)] [In] Guid guidKey, out double pfValue);

		// Token: 0x06000151 RID: 337
		void GetGUID([MarshalAs(43)] [In] Guid guidKey, out Guid pguidValue);

		// Token: 0x06000152 RID: 338
		void GetStringLength([MarshalAs(43)] [In] Guid guidKey, out int pcchLength);

		// Token: 0x06000153 RID: 339
		void GetString([MarshalAs(43)] [In] Guid guidKey, [MarshalAs(21)] [Out] StringBuilder pwszValue, int cchBufSize, out int pcchLength);

		// Token: 0x06000154 RID: 340
		void GetAllocatedString([MarshalAs(43)] [In] Guid guidKey, [MarshalAs(21)] out string ppwszValue, out int pcchLength);

		// Token: 0x06000155 RID: 341
		void GetBlobSize([MarshalAs(43)] [In] Guid guidKey, out int pcbBlobSize);

		// Token: 0x06000156 RID: 342
		void GetBlob([MarshalAs(43)] [In] Guid guidKey, [MarshalAs(42)] [Out] byte[] pBuf, int cbBufSize, out int pcbBlobSize);

		// Token: 0x06000157 RID: 343
		void GetAllocatedBlob([MarshalAs(43)] [In] Guid guidKey, out IntPtr ip, out int pcbSize);

		// Token: 0x06000158 RID: 344
		void GetUnknown([MarshalAs(43)] [In] Guid guidKey, [MarshalAs(43)] [In] Guid riid, [MarshalAs(25)] out object ppv);

		// Token: 0x06000159 RID: 345
		void SetItem([MarshalAs(43)] [In] Guid guidKey, IntPtr Value);

		// Token: 0x0600015A RID: 346
		void DeleteItem([MarshalAs(43)] [In] Guid guidKey);

		// Token: 0x0600015B RID: 347
		void DeleteAllItems();

		// Token: 0x0600015C RID: 348
		void SetUINT32([MarshalAs(43)] [In] Guid guidKey, int unValue);

		// Token: 0x0600015D RID: 349
		void SetUINT64([MarshalAs(43)] [In] Guid guidKey, long unValue);

		// Token: 0x0600015E RID: 350
		void SetDouble([MarshalAs(43)] [In] Guid guidKey, double fValue);

		// Token: 0x0600015F RID: 351
		void SetGUID([MarshalAs(43)] [In] Guid guidKey, [MarshalAs(43)] [In] Guid guidValue);

		// Token: 0x06000160 RID: 352
		void SetString([MarshalAs(43)] [In] Guid guidKey, [MarshalAs(21)] [In] string wszValue);

		// Token: 0x06000161 RID: 353
		void SetBlob([MarshalAs(43)] [In] Guid guidKey, [MarshalAs(42, SizeParamIndex = 2)] [In] byte[] pBuf, int cbBufSize);

		// Token: 0x06000162 RID: 354
		void SetUnknown([MarshalAs(43)] Guid guidKey, [MarshalAs(25)] [In] object pUnknown);

		// Token: 0x06000163 RID: 355
		void LockStore();

		// Token: 0x06000164 RID: 356
		void UnlockStore();

		// Token: 0x06000165 RID: 357
		void GetCount(out int pcItems);

		// Token: 0x06000166 RID: 358
		void GetItemByIndex(int unIndex, out Guid pGuidKey, [In] [Out] ref PropVariant pValue);

		// Token: 0x06000167 RID: 359
		void CopyAllItems([MarshalAs(28)] [In] IMFAttributes pDest);

		// Token: 0x06000168 RID: 360
		void ActivateObject([MarshalAs(43)] [In] Guid riid, [MarshalAs(28)] out object ppv);

		// Token: 0x06000169 RID: 361
		void ShutdownObject();

		// Token: 0x0600016A RID: 362
		void DetachObject();
	}
}
