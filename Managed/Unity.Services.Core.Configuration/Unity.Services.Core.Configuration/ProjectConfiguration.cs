using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Unity.Services.Core.Configuration.Internal;
using Unity.Services.Core.Internal;
using Unity.Services.Core.Internal.Serialization;

namespace Unity.Services.Core.Configuration
{
	// Token: 0x0200000A RID: 10
	internal class ProjectConfiguration : IProjectConfiguration, IServiceComponent
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002298 File Offset: 0x00000498
		internal IJsonSerializer Serializer { get; }

		// Token: 0x0600001D RID: 29 RVA: 0x000022A0 File Offset: 0x000004A0
		public ProjectConfiguration(IReadOnlyDictionary<string, ConfigurationEntry> configValues, IJsonSerializer serializer)
		{
			this.m_ConfigValues = configValues;
			this.Serializer = serializer;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000022B8 File Offset: 0x000004B8
		public bool GetBool(string key, bool defaultValue = false)
		{
			bool flag;
			if (!bool.TryParse(this.GetString(key, null), out flag))
			{
				return defaultValue;
			}
			return flag;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000022DC File Offset: 0x000004DC
		public int GetInt(string key, int defaultValue = 0)
		{
			int num;
			if (!int.TryParse(this.GetString(key, null), out num))
			{
				return defaultValue;
			}
			return num;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002300 File Offset: 0x00000500
		public float GetFloat(string key, float defaultValue = 0f)
		{
			float num;
			if (!float.TryParse(this.GetString(key, null), NumberStyles.Float, CultureInfo.InvariantCulture, out num))
			{
				return defaultValue;
			}
			return num;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000232C File Offset: 0x0000052C
		public string GetString(string key, string defaultValue = null)
		{
			ConfigurationEntry configurationEntry;
			if (!this.m_ConfigValues.TryGetValue(key, out configurationEntry))
			{
				return defaultValue;
			}
			return configurationEntry.Value;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002354 File Offset: 0x00000554
		public string ToJson()
		{
			if (this.m_JsonCache == null)
			{
				Dictionary<string, string> dictionary = this.m_ConfigValues.ToDictionary<KeyValuePair<string, ConfigurationEntry>, string, string>((KeyValuePair<string, ConfigurationEntry> pair) => pair.Key, (KeyValuePair<string, ConfigurationEntry> pair) => pair.Value.Value);
				this.m_JsonCache = this.Serializer.SerializeObject<Dictionary<string, string>>(dictionary);
			}
			return this.m_JsonCache;
		}

		// Token: 0x04000006 RID: 6
		private string m_JsonCache;

		// Token: 0x04000007 RID: 7
		private readonly IReadOnlyDictionary<string, ConfigurationEntry> m_ConfigValues;
	}
}
