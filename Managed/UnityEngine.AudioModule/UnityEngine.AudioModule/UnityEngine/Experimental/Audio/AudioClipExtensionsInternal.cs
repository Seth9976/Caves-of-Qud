using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Experimental.Audio
{
	// Token: 0x02000026 RID: 38
	[NativeHeader("AudioScriptingClasses.h")]
	[NativeHeader("Modules/Audio/Public/AudioClip.h")]
	[NativeHeader("Modules/Audio/Public/ScriptBindings/AudioClipExtensions.bindings.h")]
	internal static class AudioClipExtensionsInternal
	{
		// Token: 0x06000264 RID: 612 RVA: 0x00005310 File Offset: 0x00003510
		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		public static uint Internal_CreateAudioClipSampleProvider([NotNull] this AudioClip audioClip, ulong start, long end, bool loop, bool allowDrop, bool loopPointIsStart = false)
		{
			if (audioClip == null)
			{
				ThrowHelper.ThrowArgumentNullException(audioClip, "audioClip");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioClip>(audioClip);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(audioClip, "audioClip");
			}
			return AudioClipExtensionsInternal.Internal_CreateAudioClipSampleProvider_Injected(intPtr, start, end, loop, allowDrop, loopPointIsStart);
		}

		// Token: 0x06000265 RID: 613
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint Internal_CreateAudioClipSampleProvider_Injected(IntPtr audioClip, ulong start, long end, bool loop, bool allowDrop, bool loopPointIsStart);
	}
}
