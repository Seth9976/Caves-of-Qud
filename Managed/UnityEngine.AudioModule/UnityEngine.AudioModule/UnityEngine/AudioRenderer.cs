using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Audio;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000020 RID: 32
	[NativeType(Header = "Modules/Audio/Public/ScriptBindings/AudioRenderer.bindings.h")]
	public class AudioRenderer
	{
		// Token: 0x06000210 RID: 528 RVA: 0x00004C08 File Offset: 0x00002E08
		public static bool Start()
		{
			return AudioRenderer.Internal_AudioRenderer_Start();
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00004C20 File Offset: 0x00002E20
		public static bool Stop()
		{
			return AudioRenderer.Internal_AudioRenderer_Stop();
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00004C38 File Offset: 0x00002E38
		public static int GetSampleCountForCaptureFrame()
		{
			return AudioRenderer.Internal_AudioRenderer_GetSampleCountForCaptureFrame();
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00004C50 File Offset: 0x00002E50
		internal static bool AddMixerGroupSink(AudioMixerGroup mixerGroup, NativeArray<float> buffer, bool excludeFromMix)
		{
			return AudioRenderer.Internal_AudioRenderer_AddMixerGroupSink(mixerGroup, buffer.GetUnsafePtr<float>(), buffer.Length, excludeFromMix);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00004C78 File Offset: 0x00002E78
		public static bool Render(NativeArray<float> buffer)
		{
			return AudioRenderer.Internal_AudioRenderer_Render(buffer.GetUnsafePtr<float>(), buffer.Length);
		}

		// Token: 0x06000215 RID: 533
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool Internal_AudioRenderer_Start();

		// Token: 0x06000216 RID: 534
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool Internal_AudioRenderer_Stop();

		// Token: 0x06000217 RID: 535
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int Internal_AudioRenderer_GetSampleCountForCaptureFrame();

		// Token: 0x06000218 RID: 536 RVA: 0x00004C9C File Offset: 0x00002E9C
		internal unsafe static bool Internal_AudioRenderer_AddMixerGroupSink(AudioMixerGroup mixerGroup, void* ptr, int length, bool excludeFromMix)
		{
			return AudioRenderer.Internal_AudioRenderer_AddMixerGroupSink_Injected(Object.MarshalledUnityObject.Marshal<AudioMixerGroup>(mixerGroup), ptr, length, excludeFromMix);
		}

		// Token: 0x06000219 RID: 537
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern bool Internal_AudioRenderer_Render(void* ptr, int length);

		// Token: 0x0600021B RID: 539
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern bool Internal_AudioRenderer_AddMixerGroupSink_Injected(IntPtr mixerGroup, void* ptr, int length, bool excludeFromMix);
	}
}
