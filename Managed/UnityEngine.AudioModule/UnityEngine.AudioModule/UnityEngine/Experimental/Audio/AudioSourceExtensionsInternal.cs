using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Experimental.Audio
{
	// Token: 0x0200002C RID: 44
	[NativeHeader("AudioScriptingClasses.h")]
	[NativeHeader("Modules/Audio/Public/AudioSource.h")]
	[NativeHeader("Modules/Audio/Public/ScriptBindings/AudioSourceExtensions.bindings.h")]
	internal static class AudioSourceExtensionsInternal
	{
		// Token: 0x060002AF RID: 687 RVA: 0x00005881 File Offset: 0x00003A81
		public static void RegisterSampleProvider(this AudioSource source, AudioSampleProvider provider)
		{
			AudioSourceExtensionsInternal.Internal_RegisterSampleProviderWithAudioSource(source, provider.id);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00005891 File Offset: 0x00003A91
		public static void UnregisterSampleProvider(this AudioSource source, AudioSampleProvider provider)
		{
			AudioSourceExtensionsInternal.Internal_UnregisterSampleProviderFromAudioSource(source, provider.id);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x000058A4 File Offset: 0x00003AA4
		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		private static void Internal_RegisterSampleProviderWithAudioSource([NotNull] AudioSource source, uint providerId)
		{
			if (source == null)
			{
				ThrowHelper.ThrowArgumentNullException(source, "source");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(source);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(source, "source");
			}
			AudioSourceExtensionsInternal.Internal_RegisterSampleProviderWithAudioSource_Injected(intPtr, providerId);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x000058DC File Offset: 0x00003ADC
		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		private static void Internal_UnregisterSampleProviderFromAudioSource([NotNull] AudioSource source, uint providerId)
		{
			if (source == null)
			{
				ThrowHelper.ThrowArgumentNullException(source, "source");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(source);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(source, "source");
			}
			AudioSourceExtensionsInternal.Internal_UnregisterSampleProviderFromAudioSource_Injected(intPtr, providerId);
		}

		// Token: 0x060002B3 RID: 691
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_RegisterSampleProviderWithAudioSource_Injected(IntPtr source, uint providerId);

		// Token: 0x060002B4 RID: 692
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_UnregisterSampleProviderFromAudioSource_Injected(IntPtr source, uint providerId);
	}
}
