using System;
using Unity.Services.Core.Internal.Serialization;

namespace Unity.Services.Core.Configuration
{
	// Token: 0x02000006 RID: 6
	internal static class ConfigurationUtils
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002218 File Offset: 0x00000418
		// (set) Token: 0x06000011 RID: 17 RVA: 0x0000221F File Offset: 0x0000041F
		public static IConfigurationLoader ConfigurationLoader { get; internal set; } = new StreamingAssetsConfigurationLoader(new NewtonsoftSerializer(null));

		// Token: 0x04000003 RID: 3
		public const string ConfigFileName = "UnityServicesProjectConfiguration.json";
	}
}
