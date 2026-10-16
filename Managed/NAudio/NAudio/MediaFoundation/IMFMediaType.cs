using System;
using System.Runtime.InteropServices;
using System.Text;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.MediaFoundation
{
	// Token: 0x02000053 RID: 83
	[InterfaceType(1)]
	[Guid("44AE0FA8-EA31-4109-8D2E-4CAE4997C555")]
	[ComImport]
	public interface IMFMediaType : IMFAttributes
	{
		// Token: 0x060001E2 RID: 482
		void GetItem([MarshalAs(43)] [In] Guid guidKey, [In] [Out] ref PropVariant pValue);

		// Token: 0x060001E3 RID: 483
		void GetItemType([MarshalAs(43)] [In] Guid guidKey, out int pType);

		// Token: 0x060001E4 RID: 484
		void CompareItem([MarshalAs(43)] [In] Guid guidKey, IntPtr Value, [MarshalAs(2)] out bool pbResult);

		// Token: 0x060001E5 RID: 485
		void Compare([MarshalAs(28)] IMFAttributes pTheirs, int MatchType, [MarshalAs(2)] out bool pbResult);

		// Token: 0x060001E6 RID: 486
		void GetUINT32([MarshalAs(43)] [In] Guid guidKey, out int punValue);

		// Token: 0x060001E7 RID: 487
		void GetUINT64([MarshalAs(43)] [In] Guid guidKey, out long punValue);

		// Token: 0x060001E8 RID: 488
		void GetDouble([MarshalAs(43)] [In] Guid guidKey, out double pfValue);

		// Token: 0x060001E9 RID: 489
		void GetGUID([MarshalAs(43)] [In] Guid guidKey, out Guid pguidValue);

		// Token: 0x060001EA RID: 490
		void GetStringLength([MarshalAs(43)] [In] Guid guidKey, out int pcchLength);

		// Token: 0x060001EB RID: 491
		void GetString([MarshalAs(43)] [In] Guid guidKey, [MarshalAs(21)] [Out] StringBuilder pwszValue, int cchBufSize, out int pcchLength);

		// Token: 0x060001EC RID: 492
		void GetAllocatedString([MarshalAs(43)] [In] Guid guidKey, [MarshalAs(21)] out string ppwszValue, out int pcchLength);

		// Token: 0x060001ED RID: 493
		void GetBlobSize([MarshalAs(43)] [In] Guid guidKey, out int pcbBlobSize);

		// Token: 0x060001EE RID: 494
		void GetBlob([MarshalAs(43)] [In] Guid guidKey, [MarshalAs(42)] [Out] byte[] pBuf, int cbBufSize, out int pcbBlobSize);

		// Token: 0x060001EF RID: 495
		void GetAllocatedBlob([MarshalAs(43)] [In] Guid guidKey, out IntPtr ip, out int pcbSize);

		// Token: 0x060001F0 RID: 496
		void GetUnknown([MarshalAs(43)] [In] Guid guidKey, [MarshalAs(43)] [In] Guid riid, [MarshalAs(25)] out object ppv);

		// Token: 0x060001F1 RID: 497
		void SetItem([MarshalAs(43)] [In] Guid guidKey, IntPtr Value);

		// Token: 0x060001F2 RID: 498
		void DeleteItem([MarshalAs(43)] [In] Guid guidKey);

		// Token: 0x060001F3 RID: 499
		void DeleteAllItems();

		// Token: 0x060001F4 RID: 500
		void SetUINT32([MarshalAs(43)] [In] Guid guidKey, int unValue);

		// Token: 0x060001F5 RID: 501
		void SetUINT64([MarshalAs(43)] [In] Guid guidKey, long unValue);

		// Token: 0x060001F6 RID: 502
		void SetDouble([MarshalAs(43)] [In] Guid guidKey, double fValue);

		// Token: 0x060001F7 RID: 503
		void SetGUID([MarshalAs(43)] [In] Guid guidKey, [MarshalAs(43)] [In] Guid guidValue);

		// Token: 0x060001F8 RID: 504
		void SetString([MarshalAs(43)] [In] Guid guidKey, [MarshalAs(21)] [In] string wszValue);

		// Token: 0x060001F9 RID: 505
		void SetBlob([MarshalAs(43)] [In] Guid guidKey, [MarshalAs(42, SizeParamIndex = 2)] [In] byte[] pBuf, int cbBufSize);

		// Token: 0x060001FA RID: 506
		void SetUnknown([MarshalAs(43)] Guid guidKey, [MarshalAs(25)] [In] object pUnknown);

		// Token: 0x060001FB RID: 507
		void LockStore();

		// Token: 0x060001FC RID: 508
		void UnlockStore();

		// Token: 0x060001FD RID: 509
		void GetCount(out int pcItems);

		// Token: 0x060001FE RID: 510
		void GetItemByIndex(int unIndex, out Guid pGuidKey, [In] [Out] ref PropVariant pValue);

		// Token: 0x060001FF RID: 511
		void CopyAllItems([MarshalAs(28)] [In] IMFAttributes pDest);

		// Token: 0x06000200 RID: 512
		void GetMajorType(out Guid pguidMajorType);

		// Token: 0x06000201 RID: 513
		void IsCompressedFormat([MarshalAs(2)] out bool pfCompressed);

		// Token: 0x06000202 RID: 514
		[PreserveSig]
		int IsEqual([MarshalAs(28)] [In] IMFMediaType pIMediaType, ref int pdwFlags);

		// Token: 0x06000203 RID: 515
		void GetRepresentation([MarshalAs(27)] [In] Guid guidRepresentation, ref IntPtr ppvRepresentation);

		// Token: 0x06000204 RID: 516
		void FreeRepresentation([MarshalAs(27)] [In] Guid guidRepresentation, [In] IntPtr pvRepresentation);
	}
}
