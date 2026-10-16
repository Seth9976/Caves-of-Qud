using System;
using System.Threading.Tasks;

namespace Unity.Services.Core.Configuration
{
	// Token: 0x02000008 RID: 8
	internal interface IConfigurationLoader
	{
		// Token: 0x06000017 RID: 23
		Task<SerializableProjectConfiguration> GetConfigAsync();
	}
}
