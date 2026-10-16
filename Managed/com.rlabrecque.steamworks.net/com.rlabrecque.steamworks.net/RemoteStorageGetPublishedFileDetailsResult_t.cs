using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000A4 RID: 164
	[CallbackIdentity(1318)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageGetPublishedFileDetailsResult_t
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x00004577 File Offset: 0x00002777
		// (set) Token: 0x06000480 RID: 1152 RVA: 0x00004584 File Offset: 0x00002784
		public string m_rgchTitle
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchTitle_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchTitle_, 129);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x00004597 File Offset: 0x00002797
		// (set) Token: 0x06000482 RID: 1154 RVA: 0x000045A4 File Offset: 0x000027A4
		public string m_rgchDescription
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchDescription_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchDescription_, 8000);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x000045B7 File Offset: 0x000027B7
		// (set) Token: 0x06000484 RID: 1156 RVA: 0x000045C4 File Offset: 0x000027C4
		public string m_rgchTags
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchTags_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchTags_, 1025);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x000045D7 File Offset: 0x000027D7
		// (set) Token: 0x06000486 RID: 1158 RVA: 0x000045E4 File Offset: 0x000027E4
		public string m_pchFileName
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_pchFileName_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_pchFileName_, 260);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x000045F7 File Offset: 0x000027F7
		// (set) Token: 0x06000488 RID: 1160 RVA: 0x00004604 File Offset: 0x00002804
		public string m_rgchURL
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchURL_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchURL_, 256);
			}
		}

		// Token: 0x0400024C RID: 588
		public const int k_iCallback = 1318;

		// Token: 0x0400024D RID: 589
		public EResult m_eResult;

		// Token: 0x0400024E RID: 590
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x0400024F RID: 591
		public AppId_t m_nCreatorAppID;

		// Token: 0x04000250 RID: 592
		public AppId_t m_nConsumerAppID;

		// Token: 0x04000251 RID: 593
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 129)]
		private byte[] m_rgchTitle_;

		// Token: 0x04000252 RID: 594
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8000)]
		private byte[] m_rgchDescription_;

		// Token: 0x04000253 RID: 595
		public UGCHandle_t m_hFile;

		// Token: 0x04000254 RID: 596
		public UGCHandle_t m_hPreviewFile;

		// Token: 0x04000255 RID: 597
		public ulong m_ulSteamIDOwner;

		// Token: 0x04000256 RID: 598
		public uint m_rtimeCreated;

		// Token: 0x04000257 RID: 599
		public uint m_rtimeUpdated;

		// Token: 0x04000258 RID: 600
		public ERemoteStoragePublishedFileVisibility m_eVisibility;

		// Token: 0x04000259 RID: 601
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bBanned;

		// Token: 0x0400025A RID: 602
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1025)]
		private byte[] m_rgchTags_;

		// Token: 0x0400025B RID: 603
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bTagsTruncated;

		// Token: 0x0400025C RID: 604
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 260)]
		private byte[] m_pchFileName_;

		// Token: 0x0400025D RID: 605
		public int m_nFileSize;

		// Token: 0x0400025E RID: 606
		public int m_nPreviewFileSize;

		// Token: 0x0400025F RID: 607
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		private byte[] m_rgchURL_;

		// Token: 0x04000260 RID: 608
		public EWorkshopFileType m_eFileType;

		// Token: 0x04000261 RID: 609
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bAcceptedForUse;
	}
}
