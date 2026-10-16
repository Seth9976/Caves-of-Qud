using System;

namespace UnityEngine.Android
{
	// Token: 0x0200001B RID: 27
	public class AndroidAssetPackInfo
	{
		// Token: 0x06000263 RID: 611 RVA: 0x0000A2A3 File Offset: 0x000084A3
		internal AndroidAssetPackInfo(string name, AndroidAssetPackStatus status, ulong size, ulong bytesDownloaded, float transferProgress, AndroidAssetPackError error)
		{
			this.name = name;
			this.status = status;
			this.size = size;
			this.bytesDownloaded = bytesDownloaded;
			this.transferProgress = transferProgress;
			this.error = error;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000A2DA File Offset: 0x000084DA
		public string name { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000265 RID: 613 RVA: 0x0000A2E2 File Offset: 0x000084E2
		public AndroidAssetPackStatus status { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0000A2EA File Offset: 0x000084EA
		public ulong size { get; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0000A2F2 File Offset: 0x000084F2
		public ulong bytesDownloaded { get; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000268 RID: 616 RVA: 0x0000A2FA File Offset: 0x000084FA
		public float transferProgress { get; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000A302 File Offset: 0x00008502
		public AndroidAssetPackError error { get; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600026A RID: 618 RVA: 0x0000A30A File Offset: 0x0000850A
		internal bool downloadInProgress
		{
			get
			{
				return AndroidAssetPackInfo.DownloadInProgress(this.status);
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000A318 File Offset: 0x00008518
		internal static bool DownloadInProgress(AndroidAssetPackStatus status)
		{
			return status != AndroidAssetPackStatus.Canceled && status != AndroidAssetPackStatus.Completed && status != AndroidAssetPackStatus.Failed && status > AndroidAssetPackStatus.Unknown;
		}
	}
}
