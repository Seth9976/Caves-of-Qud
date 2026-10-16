using System;
using UnityEngine;

namespace PygmyMonkey.AdvancedBuilder
{
	// Token: 0x02000318 RID: 792
	[Serializable]
	public class AppParameters : ScriptableObject
	{
		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06001D15 RID: 7445 RVA: 0x00090180 File Offset: 0x0008E380
		public static AppParameters Get
		{
			get
			{
				if (AppParameters.mInstance == null)
				{
					AppParameters.mInstance = (AppParameters)Resources.Load("AppParameters", typeof(AppParameters));
					if (AppParameters.mInstance == null)
					{
						throw new Exception("We could not find the ScriptableObject AppParameters.asset inside the folder 'PygmyMonkey/AdvancedBuilder/Resources/'");
					}
				}
				return AppParameters.mInstance;
			}
		}

		// Token: 0x06001D16 RID: 7446 RVA: 0x000901D8 File Offset: 0x0008E3D8
		public void updateParameters(string releaseType, string platformType, string distributionPlatform, string platformArchitecture, string textureCompression, string productName, string bundleIdentifier, string bundleVersion, int buildNumber)
		{
			this.m_releaseType = releaseType;
			this.m_platformType = platformType;
			this.m_distributionPlatform = distributionPlatform;
			this.m_platformArchitecture = platformArchitecture;
			this.m_textureCompression = textureCompression;
			this.m_productName = productName;
			this.m_bundleIdentifier = bundleIdentifier;
			this.m_bundleVersion = bundleVersion;
			this.m_buildNumber = buildNumber;
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06001D17 RID: 7447 RVA: 0x0009022A File Offset: 0x0008E42A
		public string releaseType
		{
			get
			{
				return this.m_releaseType;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06001D18 RID: 7448 RVA: 0x00090232 File Offset: 0x0008E432
		public string platformType
		{
			get
			{
				return this.m_platformType;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06001D19 RID: 7449 RVA: 0x0009023A File Offset: 0x0008E43A
		public string distributionPlatform
		{
			get
			{
				return this.m_distributionPlatform;
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06001D1A RID: 7450 RVA: 0x00090242 File Offset: 0x0008E442
		public string platformArchitecture
		{
			get
			{
				return this.m_platformArchitecture;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06001D1B RID: 7451 RVA: 0x0009024A File Offset: 0x0008E44A
		public string textureCompression
		{
			get
			{
				return this.m_textureCompression;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06001D1C RID: 7452 RVA: 0x00090252 File Offset: 0x0008E452
		public string productName
		{
			get
			{
				return this.m_productName;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06001D1D RID: 7453 RVA: 0x0009025A File Offset: 0x0008E45A
		public string bundleIdentifier
		{
			get
			{
				return this.m_bundleIdentifier;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06001D1E RID: 7454 RVA: 0x00090262 File Offset: 0x0008E462
		public string bundleVersion
		{
			get
			{
				return this.m_bundleVersion;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06001D1F RID: 7455 RVA: 0x0009026A File Offset: 0x0008E46A
		public int buildNumber
		{
			get
			{
				return this.m_buildNumber;
			}
		}

		// Token: 0x04000F65 RID: 3941
		private static AppParameters mInstance;

		// Token: 0x04000F66 RID: 3942
		[SerializeField]
		private string m_releaseType = "";

		// Token: 0x04000F67 RID: 3943
		[SerializeField]
		private string m_platformType = "";

		// Token: 0x04000F68 RID: 3944
		[SerializeField]
		private string m_distributionPlatform = "";

		// Token: 0x04000F69 RID: 3945
		[SerializeField]
		private string m_platformArchitecture = "";

		// Token: 0x04000F6A RID: 3946
		[SerializeField]
		private string m_textureCompression = "";

		// Token: 0x04000F6B RID: 3947
		[SerializeField]
		private string m_productName = "";

		// Token: 0x04000F6C RID: 3948
		[SerializeField]
		private string m_bundleIdentifier = "";

		// Token: 0x04000F6D RID: 3949
		[SerializeField]
		private string m_bundleVersion = "";

		// Token: 0x04000F6E RID: 3950
		[SerializeField]
		private int m_buildNumber;
	}
}
