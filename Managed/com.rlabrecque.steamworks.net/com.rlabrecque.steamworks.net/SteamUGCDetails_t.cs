using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000168 RID: 360
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamUGCDetails_t
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x000046D7 File Offset: 0x000028D7
		// (set) Token: 0x06000496 RID: 1174 RVA: 0x000046E4 File Offset: 0x000028E4
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

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x000046F7 File Offset: 0x000028F7
		// (set) Token: 0x06000498 RID: 1176 RVA: 0x00004704 File Offset: 0x00002904
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

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x00004717 File Offset: 0x00002917
		// (set) Token: 0x0600049A RID: 1178 RVA: 0x00004724 File Offset: 0x00002924
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

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x00004737 File Offset: 0x00002937
		// (set) Token: 0x0600049C RID: 1180 RVA: 0x00004744 File Offset: 0x00002944
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

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x00004757 File Offset: 0x00002957
		// (set) Token: 0x0600049E RID: 1182 RVA: 0x00004764 File Offset: 0x00002964
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

		// Token: 0x04000A55 RID: 2645
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x04000A56 RID: 2646
		public EResult m_eResult;

		// Token: 0x04000A57 RID: 2647
		public EWorkshopFileType m_eFileType;

		// Token: 0x04000A58 RID: 2648
		public AppId_t m_nCreatorAppID;

		// Token: 0x04000A59 RID: 2649
		public AppId_t m_nConsumerAppID;

		// Token: 0x04000A5A RID: 2650
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 129)]
		private byte[] m_rgchTitle_;

		// Token: 0x04000A5B RID: 2651
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8000)]
		private byte[] m_rgchDescription_;

		// Token: 0x04000A5C RID: 2652
		public ulong m_ulSteamIDOwner;

		// Token: 0x04000A5D RID: 2653
		public uint m_rtimeCreated;

		// Token: 0x04000A5E RID: 2654
		public uint m_rtimeUpdated;

		// Token: 0x04000A5F RID: 2655
		public uint m_rtimeAddedToUserList;

		// Token: 0x04000A60 RID: 2656
		public ERemoteStoragePublishedFileVisibility m_eVisibility;

		// Token: 0x04000A61 RID: 2657
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bBanned;

		// Token: 0x04000A62 RID: 2658
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bAcceptedForUse;

		// Token: 0x04000A63 RID: 2659
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bTagsTruncated;

		// Token: 0x04000A64 RID: 2660
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1025)]
		private byte[] m_rgchTags_;

		// Token: 0x04000A65 RID: 2661
		public UGCHandle_t m_hFile;

		// Token: 0x04000A66 RID: 2662
		public UGCHandle_t m_hPreviewFile;

		// Token: 0x04000A67 RID: 2663
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 260)]
		private byte[] m_pchFileName_;

		// Token: 0x04000A68 RID: 2664
		public int m_nFileSize;

		// Token: 0x04000A69 RID: 2665
		public int m_nPreviewFileSize;

		// Token: 0x04000A6A RID: 2666
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		private byte[] m_rgchURL_;

		// Token: 0x04000A6B RID: 2667
		public uint m_unVotesUp;

		// Token: 0x04000A6C RID: 2668
		public uint m_unVotesDown;

		// Token: 0x04000A6D RID: 2669
		public float m_flScore;

		// Token: 0x04000A6E RID: 2670
		public uint m_unNumChildren;

		// Token: 0x04000A6F RID: 2671
		public ulong m_ulTotalFilesSize;
	}
}
