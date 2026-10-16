using System;

namespace UnityEngine.SubsystemsImplementation.Extensions
{
	// Token: 0x0200001A RID: 26
	public static class SubsystemDescriptorExtensions
	{
		// Token: 0x06000095 RID: 149 RVA: 0x00002FD8 File Offset: 0x000011D8
		public static SubsystemProxy<TSubsystem, TProvider> CreateProxy<TSubsystem, TProvider>(this SubsystemDescriptorWithProvider<TSubsystem, TProvider> descriptor) where TSubsystem : SubsystemWithProvider, new() where TProvider : SubsystemProvider<TSubsystem>
		{
			TProvider tprovider = descriptor.CreateProvider();
			return (tprovider != null) ? new SubsystemProxy<TSubsystem, TProvider>(tprovider) : null;
		}
	}
}
