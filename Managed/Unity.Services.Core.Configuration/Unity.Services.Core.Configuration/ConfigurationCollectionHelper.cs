using System;
using System.Collections.Generic;
using System.Globalization;
using Unity.Services.Core.Internal;

namespace Unity.Services.Core.Configuration
{
	// Token: 0x02000004 RID: 4
	internal static class ConfigurationCollectionHelper
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000020D0 File Offset: 0x000002D0
		public static void FillWith(this IDictionary<string, ConfigurationEntry> self, SerializableProjectConfiguration config)
		{
			for (int i = 0; i < config.Keys.Length; i++)
			{
				string text = config.Keys[i];
				ConfigurationEntry configurationEntry = config.Values[i];
				self.SetOrCreateEntry(text, configurationEntry);
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000210C File Offset: 0x0000030C
		public static void FillWith(this IDictionary<string, ConfigurationEntry> self, InitializationOptions options)
		{
			foreach (KeyValuePair<string, object> keyValuePair in options.Values)
			{
				string text = Convert.ToString(keyValuePair.Value, CultureInfo.InvariantCulture);
				self.SetOrCreateEntry(keyValuePair.Key, text);
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002178 File Offset: 0x00000378
		private static void SetOrCreateEntry(this IDictionary<string, ConfigurationEntry> self, string key, ConfigurationEntry entry)
		{
			ConfigurationEntry configurationEntry;
			if (self.TryGetValue(key, out configurationEntry))
			{
				if (!configurationEntry.TrySetValue(entry))
				{
					CoreLogger.LogWarning("You are attempting to initialize Operate Solution SDK with an option \"" + key + "\" which is readonly at runtime and can be modified only through Project Settings. The value provided as initialization option will be ignored. Please update InitializationOptions in order to remove this warning.");
					return;
				}
			}
			else
			{
				self[key] = entry;
			}
		}
	}
}
