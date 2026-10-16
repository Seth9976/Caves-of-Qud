using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Unity.Services.Core.Configuration
{
	// Token: 0x0200000B RID: 11
	[Serializable]
	internal struct SerializableProjectConfiguration
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000023CC File Offset: 0x000005CC
		public static SerializableProjectConfiguration Empty
		{
			get
			{
				return new SerializableProjectConfiguration
				{
					Keys = Array.Empty<string>(),
					Values = Array.Empty<ConfigurationEntry>()
				};
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000023FC File Offset: 0x000005FC
		public SerializableProjectConfiguration(IDictionary<string, ConfigurationEntry> configValues)
		{
			this.Keys = new string[configValues.Count];
			this.Values = new ConfigurationEntry[configValues.Count];
			int num = 0;
			foreach (KeyValuePair<string, ConfigurationEntry> keyValuePair in configValues)
			{
				this.Keys[num] = keyValuePair.Key;
				this.Values[num] = keyValuePair.Value;
				num++;
			}
		}

		// Token: 0x04000009 RID: 9
		[JsonRequired]
		[SerializeField]
		internal string[] Keys;

		// Token: 0x0400000A RID: 10
		[JsonRequired]
		[SerializeField]
		internal ConfigurationEntry[] Values;
	}
}
