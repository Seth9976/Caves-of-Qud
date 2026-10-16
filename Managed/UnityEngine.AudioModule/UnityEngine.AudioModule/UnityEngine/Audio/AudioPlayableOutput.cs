using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Audio
{
	// Token: 0x02000037 RID: 55
	[NativeHeader("Modules/Audio/Public/ScriptBindings/AudioPlayableOutput.bindings.h")]
	[NativeHeader("Modules/Audio/Public/Director/AudioPlayableOutput.h")]
	[NativeHeader("Modules/Audio/Public/AudioSource.h")]
	[StaticAccessor("AudioPlayableOutputBindings", StaticAccessorType.DoubleColon)]
	[RequiredByNativeCode]
	public struct AudioPlayableOutput : IPlayableOutput
	{
		// Token: 0x06000314 RID: 788 RVA: 0x00006438 File Offset: 0x00004638
		public static AudioPlayableOutput Create(PlayableGraph graph, string name, AudioSource target)
		{
			PlayableOutputHandle playableOutputHandle;
			bool flag = !AudioPlayableGraphExtensions.InternalCreateAudioOutput(ref graph, name, out playableOutputHandle);
			AudioPlayableOutput audioPlayableOutput;
			if (flag)
			{
				audioPlayableOutput = AudioPlayableOutput.Null;
			}
			else
			{
				AudioPlayableOutput audioPlayableOutput2 = new AudioPlayableOutput(playableOutputHandle);
				audioPlayableOutput2.SetTarget(target);
				audioPlayableOutput = audioPlayableOutput2;
			}
			return audioPlayableOutput;
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00006478 File Offset: 0x00004678
		internal AudioPlayableOutput(PlayableOutputHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOutputOfType<AudioPlayableOutput>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AudioPlayableOutput.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000316 RID: 790 RVA: 0x000064B4 File Offset: 0x000046B4
		public static AudioPlayableOutput Null
		{
			get
			{
				return new AudioPlayableOutput(PlayableOutputHandle.Null);
			}
		}

		// Token: 0x06000317 RID: 791 RVA: 0x000064D0 File Offset: 0x000046D0
		public PlayableOutputHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x000064E8 File Offset: 0x000046E8
		public static implicit operator PlayableOutput(AudioPlayableOutput output)
		{
			return new PlayableOutput(output.GetHandle());
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00006508 File Offset: 0x00004708
		public static explicit operator AudioPlayableOutput(PlayableOutput output)
		{
			return new AudioPlayableOutput(output.GetHandle());
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00006528 File Offset: 0x00004728
		public AudioSource GetTarget()
		{
			return AudioPlayableOutput.InternalGetTarget(ref this.m_Handle);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00006545 File Offset: 0x00004745
		public void SetTarget(AudioSource value)
		{
			AudioPlayableOutput.InternalSetTarget(ref this.m_Handle, value);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00006558 File Offset: 0x00004758
		public bool GetEvaluateOnSeek()
		{
			return AudioPlayableOutput.InternalGetEvaluateOnSeek(ref this.m_Handle);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00006575 File Offset: 0x00004775
		public void SetEvaluateOnSeek(bool value)
		{
			AudioPlayableOutput.InternalSetEvaluateOnSeek(ref this.m_Handle, value);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00006588 File Offset: 0x00004788
		[NativeThrows]
		private static AudioSource InternalGetTarget(ref PlayableOutputHandle output)
		{
			return Unmarshal.UnmarshalUnityObject<AudioSource>(AudioPlayableOutput.InternalGetTarget_Injected(ref output));
		}

		// Token: 0x0600031F RID: 799 RVA: 0x000065A0 File Offset: 0x000047A0
		[NativeThrows]
		private static void InternalSetTarget(ref PlayableOutputHandle output, AudioSource target)
		{
			AudioPlayableOutput.InternalSetTarget_Injected(ref output, Object.MarshalledUnityObject.Marshal<AudioSource>(target));
		}

		// Token: 0x06000320 RID: 800
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool InternalGetEvaluateOnSeek(ref PlayableOutputHandle output);

		// Token: 0x06000321 RID: 801
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetEvaluateOnSeek(ref PlayableOutputHandle output, bool value);

		// Token: 0x06000322 RID: 802
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr InternalGetTarget_Injected(ref PlayableOutputHandle output);

		// Token: 0x06000323 RID: 803
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetTarget_Injected(ref PlayableOutputHandle output, IntPtr target);

		// Token: 0x04000080 RID: 128
		private PlayableOutputHandle m_Handle;
	}
}
