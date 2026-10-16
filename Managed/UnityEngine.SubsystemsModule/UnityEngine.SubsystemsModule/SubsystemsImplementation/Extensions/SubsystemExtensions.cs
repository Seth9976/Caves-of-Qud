using System;

namespace UnityEngine.SubsystemsImplementation.Extensions
{
	// Token: 0x0200001B RID: 27
	public static class SubsystemExtensions
	{
		// Token: 0x06000096 RID: 150 RVA: 0x00003004 File Offset: 0x00001204
		public static TProvider GetProvider<TSubsystem, TDescriptor, TProvider>(this SubsystemWithProvider<TSubsystem, TDescriptor, TProvider> subsystem) where TSubsystem : SubsystemWithProvider, new() where TDescriptor : SubsystemDescriptorWithProvider<TSubsystem, TProvider> where TProvider : SubsystemProvider<TSubsystem>
		{
			return subsystem.provider;
		}
	}
}
