using System;
using System.Threading.Tasks;

namespace Unity.Services.Core.Configuration
{
	// Token: 0x02000009 RID: 9
	internal class MemoryConfigurationLoader : IConfigurationLoader
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002267 File Offset: 0x00000467
		// (set) Token: 0x06000019 RID: 25 RVA: 0x0000226F File Offset: 0x0000046F
		public SerializableProjectConfiguration Config { get; set; }

		// Token: 0x0600001A RID: 26 RVA: 0x00002278 File Offset: 0x00000478
		Task<SerializableProjectConfiguration> IConfigurationLoader.GetConfigAsync()
		{
			TaskCompletionSource<SerializableProjectConfiguration> taskCompletionSource = new TaskCompletionSource<SerializableProjectConfiguration>();
			taskCompletionSource.SetResult(this.Config);
			return taskCompletionSource.Task;
		}
	}
}
