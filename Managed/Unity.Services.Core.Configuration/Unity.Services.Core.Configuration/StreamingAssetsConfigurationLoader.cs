using System;
using System.Threading.Tasks;
using Unity.Services.Core.Internal.Serialization;

namespace Unity.Services.Core.Configuration
{
	// Token: 0x0200000C RID: 12
	internal class StreamingAssetsConfigurationLoader : IConfigurationLoader
	{
		// Token: 0x06000025 RID: 37 RVA: 0x00002484 File Offset: 0x00000684
		public StreamingAssetsConfigurationLoader(IJsonSerializer serializer)
		{
			this.m_Serializer = serializer;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002494 File Offset: 0x00000694
		public async Task<SerializableProjectConfiguration> GetConfigAsync()
		{
			string text = await StreamingAssetsUtils.GetFileTextFromStreamingAssetsAsync("UnityServicesProjectConfiguration.json");
			return this.m_Serializer.DeserializeObject<SerializableProjectConfiguration>(text);
		}

		// Token: 0x0400000B RID: 11
		private readonly IJsonSerializer m_Serializer;
	}
}
