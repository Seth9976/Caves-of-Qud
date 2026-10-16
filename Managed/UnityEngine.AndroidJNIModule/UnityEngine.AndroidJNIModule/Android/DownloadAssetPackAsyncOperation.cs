using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.Android
{
	// Token: 0x0200001E RID: 30
	public class DownloadAssetPackAsyncOperation : CustomYieldInstruction
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000A390 File Offset: 0x00008590
		public override bool keepWaiting
		{
			get
			{
				Dictionary<string, AndroidAssetPackInfo> assetPackInfos = this.m_AssetPackInfos;
				bool flag3;
				lock (assetPackInfos)
				{
					foreach (AndroidAssetPackInfo androidAssetPackInfo in this.m_AssetPackInfos.Values)
					{
						bool flag2 = androidAssetPackInfo == null;
						if (flag2)
						{
							return true;
						}
						bool downloadInProgress = androidAssetPackInfo.downloadInProgress;
						if (downloadInProgress)
						{
							return true;
						}
					}
					flag3 = false;
				}
				return flag3;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000A438 File Offset: 0x00008638
		public bool isDone
		{
			get
			{
				return !this.keepWaiting;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000A444 File Offset: 0x00008644
		public float progress
		{
			get
			{
				Dictionary<string, AndroidAssetPackInfo> assetPackInfos = this.m_AssetPackInfos;
				float num4;
				lock (assetPackInfos)
				{
					float num = 0f;
					float num2 = 0f;
					foreach (AndroidAssetPackInfo androidAssetPackInfo in this.m_AssetPackInfos.Values)
					{
						bool flag2 = androidAssetPackInfo == null;
						if (!flag2)
						{
							bool flag3 = !androidAssetPackInfo.downloadInProgress;
							if (flag3)
							{
								num += 1f;
								num2 += 1f;
							}
							else
							{
								double num3 = androidAssetPackInfo.bytesDownloaded / androidAssetPackInfo.size;
								num += (float)num3;
								num2 += androidAssetPackInfo.transferProgress;
							}
						}
					}
					num4 = Mathf.Clamp((num * 0.8f + num2 * 0.2f) / (float)this.m_AssetPackInfos.Count, 0f, 1f);
				}
				return num4;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000A55C File Offset: 0x0000875C
		public string[] downloadedAssetPacks
		{
			get
			{
				Dictionary<string, AndroidAssetPackInfo> assetPackInfos = this.m_AssetPackInfos;
				string[] array;
				lock (assetPackInfos)
				{
					List<string> list = new List<string>();
					foreach (AndroidAssetPackInfo androidAssetPackInfo in this.m_AssetPackInfos.Values)
					{
						bool flag2 = androidAssetPackInfo == null;
						if (!flag2)
						{
							bool flag3 = androidAssetPackInfo.status == AndroidAssetPackStatus.Completed;
							if (flag3)
							{
								list.Add(androidAssetPackInfo.name);
							}
						}
					}
					array = list.ToArray();
				}
				return array;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000A61C File Offset: 0x0000881C
		public string[] downloadFailedAssetPacks
		{
			get
			{
				Dictionary<string, AndroidAssetPackInfo> assetPackInfos = this.m_AssetPackInfos;
				string[] array;
				lock (assetPackInfos)
				{
					List<string> list = new List<string>();
					foreach (KeyValuePair<string, AndroidAssetPackInfo> keyValuePair in this.m_AssetPackInfos)
					{
						AndroidAssetPackInfo value = keyValuePair.Value;
						bool flag2 = value == null;
						if (flag2)
						{
							list.Add(keyValuePair.Key);
						}
						else
						{
							bool flag3 = value.status == AndroidAssetPackStatus.Canceled || value.status == AndroidAssetPackStatus.Failed || value.status == AndroidAssetPackStatus.Unknown;
							if (flag3)
							{
								list.Add(value.name);
							}
						}
					}
					array = list.ToArray();
				}
				return array;
			}
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000A708 File Offset: 0x00008908
		internal DownloadAssetPackAsyncOperation(string[] assetPackNames)
		{
			this.m_AssetPackInfos = assetPackNames.ToDictionary<string, string, AndroidAssetPackInfo>((string name) => name, (string name) => null);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000A768 File Offset: 0x00008968
		internal void OnUpdate(AndroidAssetPackInfo info)
		{
			Dictionary<string, AndroidAssetPackInfo> assetPackInfos = this.m_AssetPackInfos;
			lock (assetPackInfos)
			{
				this.m_AssetPackInfos[info.name] = info;
			}
		}

		// Token: 0x0400006C RID: 108
		private Dictionary<string, AndroidAssetPackInfo> m_AssetPackInfos;
	}
}
