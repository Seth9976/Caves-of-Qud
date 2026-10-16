using System;
using Unity.Services.Core.Device.Internal;
using Unity.Services.Core.Internal;
using UnityEngine;

namespace Unity.Services.Core.Device
{
	// Token: 0x02000003 RID: 3
	internal class InstallationId : IInstallationId, IServiceComponent
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public InstallationId()
		{
			this.UnityAdsIdentifierProvider = new UnityAdsIdentifier();
			this.UnityAnalyticsIdentifierProvider = new UnityAnalyticsIdentifier();
			this.UnityEngineIdentifierProvider = new UnityEngineIdentifier();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020E7 File Offset: 0x000002E7
		public string GetOrCreateIdentifier()
		{
			if (string.IsNullOrEmpty(this.Identifier))
			{
				this.CreateIdentifier();
			}
			return this.Identifier;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002104 File Offset: 0x00000304
		public void CreateIdentifier()
		{
			this.Identifier = InstallationId.ReadIdentifierFromFile();
			if (!string.IsNullOrEmpty(this.Identifier))
			{
				return;
			}
			string userId = this.UnityEngineIdentifierProvider.UserId;
			if (!string.IsNullOrEmpty(userId))
			{
				this.Identifier = userId;
				InstallationId.WriteIdentifierToFile(this.Identifier);
				return;
			}
			string userId2 = this.UnityAnalyticsIdentifierProvider.UserId;
			string userId3 = this.UnityAdsIdentifierProvider.UserId;
			if (!string.IsNullOrEmpty(userId2))
			{
				this.Identifier = userId2;
			}
			else if (!string.IsNullOrEmpty(userId3))
			{
				this.Identifier = userId3;
			}
			else
			{
				this.Identifier = InstallationId.GenerateGuid();
			}
			InstallationId.WriteIdentifierToFile(this.Identifier);
			if (string.IsNullOrEmpty(userId2))
			{
				this.UnityAnalyticsIdentifierProvider.UserId = this.Identifier;
			}
			if (string.IsNullOrEmpty(userId3))
			{
				this.UnityAdsIdentifierProvider.UserId = this.Identifier;
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000021D3 File Offset: 0x000003D3
		private static string ReadIdentifierFromFile()
		{
			return PlayerPrefs.GetString("UnityInstallationId");
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021DF File Offset: 0x000003DF
		private static void WriteIdentifierToFile(string identifier)
		{
			PlayerPrefs.SetString("UnityInstallationId", identifier);
			PlayerPrefs.Save();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021F4 File Offset: 0x000003F4
		private static string GenerateGuid()
		{
			return Guid.NewGuid().ToString();
		}

		// Token: 0x04000001 RID: 1
		private const string k_UnityInstallationIdKey = "UnityInstallationId";

		// Token: 0x04000002 RID: 2
		internal string Identifier;

		// Token: 0x04000003 RID: 3
		internal IUserIdentifierProvider UnityAdsIdentifierProvider;

		// Token: 0x04000004 RID: 4
		internal IUserIdentifierProvider UnityAnalyticsIdentifierProvider;

		// Token: 0x04000005 RID: 5
		internal IUserIdentifierProvider UnityEngineIdentifierProvider;
	}
}
