using System;
using System.Runtime.InteropServices;
using System.Text;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.MediaFoundation
{
	// Token: 0x02000054 RID: 84
	[Guid("c40a00f2-b93a-4d80-ae8c-5a1c634f58e4")]
	[InterfaceType(1)]
	[ComImport]
	public interface IMFSample : IMFAttributes
	{
		// Token: 0x06000205 RID: 517
		void GetItem([MarshalAs(43)] [In] Guid guidKey, [In] [Out] ref PropVariant pValue);

		// Token: 0x06000206 RID: 518
		void GetItemType([MarshalAs(43)] [In] Guid guidKey, out int pType);

		// Token: 0x06000207 RID: 519
		void CompareItem([MarshalAs(43)] [In] Guid guidKey, IntPtr Value, [MarshalAs(2)] out bool pbResult);

		// Token: 0x06000208 RID: 520
		void Compare([MarshalAs(28)] IMFAttributes pTheirs, int MatchType, [MarshalAs(2)] out bool pbResult);

		// Token: 0x06000209 RID: 521
		void GetUINT32([MarshalAs(43)] [In] Guid guidKey, out int punValue);

		// Token: 0x0600020A RID: 522
		void GetUINT64([MarshalAs(43)] [In] Guid guidKey, out long punValue);

		// Token: 0x0600020B RID: 523
		void GetDouble([MarshalAs(43)] [In] Guid guidKey, out double pfValue);

		// Token: 0x0600020C RID: 524
		void GetGUID([MarshalAs(43)] [In] Guid guidKey, out Guid pguidValue);

		// Token: 0x0600020D RID: 525
		void GetStringLength([MarshalAs(43)] [In] Guid guidKey, out int pcchLength);

		// Token: 0x0600020E RID: 526
		void GetString([MarshalAs(43)] [In] Guid guidKey, [MarshalAs(21)] [Out] StringBuilder pwszValue, int cchBufSize, out int pcchLength);

		// Token: 0x0600020F RID: 527
		void GetAllocatedString([MarshalAs(43)] [In] Guid guidKey, [MarshalAs(21)] out string ppwszValue, out int pcchLength);

		// Token: 0x06000210 RID: 528
		void GetBlobSize([MarshalAs(43)] [In] Guid guidKey, out int pcbBlobSize);

		// Token: 0x06000211 RID: 529
		void GetBlob([MarshalAs(43)] [In] Guid guidKey, [MarshalAs(42)] [Out] byte[] pBuf, int cbBufSize, out int pcbBlobSize);

		// Token: 0x06000212 RID: 530
		void GetAllocatedBlob([MarshalAs(43)] [In] Guid guidKey, out IntPtr ip, out int pcbSize);

		// Token: 0x06000213 RID: 531
		void GetUnknown([MarshalAs(43)] [In] Guid guidKey, [MarshalAs(43)] [In] Guid riid, [MarshalAs(25)] out object ppv);

		// Token: 0x06000214 RID: 532
		void SetItem([MarshalAs(43)] [In] Guid guidKey, IntPtr Value);

		// Token: 0x06000215 RID: 533
		void DeleteItem([MarshalAs(43)] [In] Guid guidKey);

		// Token: 0x06000216 RID: 534
		void DeleteAllItems();

		// Token: 0x06000217 RID: 535
		void SetUINT32([MarshalAs(43)] [In] Guid guidKey, int unValue);

		// Token: 0x06000218 RID: 536
		void SetUINT64([MarshalAs(43)] [In] Guid guidKey, long unValue);

		// Token: 0x06000219 RID: 537
		void SetDouble([MarshalAs(43)] [In] Guid guidKey, double fValue);

		// Token: 0x0600021A RID: 538
		void SetGUID([MarshalAs(43)] [In] Guid guidKey, [MarshalAs(43)] [In] Guid guidValue);

		// Token: 0x0600021B RID: 539
		void SetString([MarshalAs(43)] [In] Guid guidKey, [MarshalAs(21)] [In] string wszValue);

		// Token: 0x0600021C RID: 540
		void SetBlob([MarshalAs(43)] [In] Guid guidKey, [MarshalAs(42, SizeParamIndex = 2)] [In] byte[] pBuf, int cbBufSize);

		// Token: 0x0600021D RID: 541
		void SetUnknown([MarshalAs(43)] Guid guidKey, [MarshalAs(25)] [In] object pUnknown);

		// Token: 0x0600021E RID: 542
		void LockStore();

		// Token: 0x0600021F RID: 543
		void UnlockStore();

		// Token: 0x06000220 RID: 544
		void GetCount(out int pcItems);

		// Token: 0x06000221 RID: 545
		void GetItemByIndex(int unIndex, out Guid pGuidKey, [In] [Out] ref PropVariant pValue);

		// Token: 0x06000222 RID: 546
		void CopyAllItems([MarshalAs(28)] [In] IMFAttributes pDest);

		// Token: 0x06000223 RID: 547
		void GetSampleFlags(out int pdwSampleFlags);

		// Token: 0x06000224 RID: 548
		void SetSampleFlags(int dwSampleFlags);

		// Token: 0x06000225 RID: 549
		void GetSampleTime(out long phnsSampletime);

		// Token: 0x06000226 RID: 550
		void SetSampleTime(long hnsSampleTime);

		// Token: 0x06000227 RID: 551
		void GetSampleDuration(out long phnsSampleDuration);

		// Token: 0x06000228 RID: 552
		void SetSampleDuration(long hnsSampleDuration);

		// Token: 0x06000229 RID: 553
		void GetBufferCount(out int pdwBufferCount);

		// Token: 0x0600022A RID: 554
		void GetBufferByIndex(int dwIndex, out IMFMediaBuffer ppBuffer);

		// Token: 0x0600022B RID: 555
		void ConvertToContiguousBuffer(out IMFMediaBuffer ppBuffer);

		// Token: 0x0600022C RID: 556
		void AddBuffer(IMFMediaBuffer pBuffer);

		// Token: 0x0600022D RID: 557
		void RemoveBufferByIndex(int dwIndex);

		// Token: 0x0600022E RID: 558
		void RemoveAllBuffers();

		// Token: 0x0600022F RID: 559
		void GetTotalLength(out int pcbTotalLength);

		// Token: 0x06000230 RID: 560
		void CopyToBuffer(IMFMediaBuffer pBuffer);
	}
}
