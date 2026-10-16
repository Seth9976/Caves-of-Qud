using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Experimental.Audio
{
	// Token: 0x0200002B RID: 43
	[NativeHeader("Modules/Audio/Public/ScriptBindings/AudioSampleProviderExtensions.bindings.h")]
	[StaticAccessor("AudioSampleProviderExtensionsBindings", StaticAccessorType.DoubleColon)]
	internal static class AudioSampleProviderExtensionsInternal
	{
		// Token: 0x060002AD RID: 685 RVA: 0x00005864 File Offset: 0x00003A64
		public static float GetSpeed(this AudioSampleProvider provider)
		{
			return AudioSampleProviderExtensionsInternal.InternalGetAudioSampleProviderSpeed(provider.id);
		}

		// Token: 0x060002AE RID: 686
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float InternalGetAudioSampleProviderSpeed(uint providerId);
	}
}
