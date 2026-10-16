using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Audio
{
	// Token: 0x0200002F RID: 47
	[RequiredByNativeCode]
	[StaticAccessor("AudioClipPlayableBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/Audio/Public/Director/AudioClipPlayable.h")]
	[NativeHeader("Modules/Audio/Public/ScriptBindings/AudioClipPlayable.bindings.h")]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	public struct AudioClipPlayable : IPlayable, IEquatable<AudioClipPlayable>
	{
		// Token: 0x060002B8 RID: 696 RVA: 0x00005920 File Offset: 0x00003B20
		public static AudioClipPlayable Create(PlayableGraph graph, AudioClip clip, bool looping)
		{
			PlayableHandle playableHandle = AudioClipPlayable.CreateHandle(graph, clip, looping);
			AudioClipPlayable audioClipPlayable = new AudioClipPlayable(playableHandle);
			bool flag = clip != null;
			if (flag)
			{
				audioClipPlayable.SetDuration((double)clip.length);
			}
			return audioClipPlayable;
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00005960 File Offset: 0x00003B60
		private static PlayableHandle CreateHandle(PlayableGraph graph, AudioClip clip, bool looping)
		{
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !AudioClipPlayable.InternalCreateAudioClipPlayable(ref graph, clip, looping, ref @null);
			PlayableHandle playableHandle;
			if (flag)
			{
				playableHandle = PlayableHandle.Null;
			}
			else
			{
				playableHandle = @null;
			}
			return playableHandle;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00005994 File Offset: 0x00003B94
		internal AudioClipPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<AudioClipPlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AudioClipPlayable.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x060002BB RID: 699 RVA: 0x000059D0 File Offset: 0x00003BD0
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x000059E8 File Offset: 0x00003BE8
		public static implicit operator Playable(AudioClipPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00005A08 File Offset: 0x00003C08
		public static explicit operator AudioClipPlayable(Playable playable)
		{
			return new AudioClipPlayable(playable.GetHandle());
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00005A28 File Offset: 0x00003C28
		public bool Equals(AudioClipPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00005A4C File Offset: 0x00003C4C
		public AudioClip GetClip()
		{
			return AudioClipPlayable.GetClipInternal(ref this.m_Handle);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00005A69 File Offset: 0x00003C69
		public void SetClip(AudioClip value)
		{
			AudioClipPlayable.SetClipInternal(ref this.m_Handle, value);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00005A7C File Offset: 0x00003C7C
		public bool GetLooped()
		{
			return AudioClipPlayable.GetLoopedInternal(ref this.m_Handle);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00005A99 File Offset: 0x00003C99
		public void SetLooped(bool value)
		{
			AudioClipPlayable.SetLoopedInternal(ref this.m_Handle, value);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00005AAC File Offset: 0x00003CAC
		internal float GetVolume()
		{
			return AudioClipPlayable.GetVolumeInternal(ref this.m_Handle);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00005ACC File Offset: 0x00003CCC
		internal void SetVolume(float value)
		{
			bool flag = value < 0f || value > 1f;
			if (flag)
			{
				throw new ArgumentException("Trying to set AudioClipPlayable volume outside of range (0.0 - 1.0): " + value.ToString());
			}
			AudioClipPlayable.SetVolumeInternal(ref this.m_Handle, value);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00005B18 File Offset: 0x00003D18
		internal float GetClipPositionSec()
		{
			return AudioClipPlayable.GetClipPositionSecInternal(ref this.m_Handle);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00005B38 File Offset: 0x00003D38
		internal float GetStereoPan()
		{
			return AudioClipPlayable.GetStereoPanInternal(ref this.m_Handle);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00005B58 File Offset: 0x00003D58
		internal void SetStereoPan(float value)
		{
			bool flag = value < -1f || value > 1f;
			if (flag)
			{
				throw new ArgumentException("Trying to set AudioClipPlayable stereo pan outside of range (-1.0 - 1.0): " + value.ToString());
			}
			AudioClipPlayable.SetStereoPanInternal(ref this.m_Handle, value);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00005BA4 File Offset: 0x00003DA4
		internal float GetSpatialBlend()
		{
			return AudioClipPlayable.GetSpatialBlendInternal(ref this.m_Handle);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00005BC4 File Offset: 0x00003DC4
		internal void SetSpatialBlend(float value)
		{
			bool flag = value < 0f || value > 1f;
			if (flag)
			{
				throw new ArgumentException("Trying to set AudioClipPlayable spatial blend outside of range (0.0 - 1.0): " + value.ToString());
			}
			AudioClipPlayable.SetSpatialBlendInternal(ref this.m_Handle, value);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00005C10 File Offset: 0x00003E10
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("IsPlaying() has been deprecated. Use IsChannelPlaying() instead (UnityUpgradable) -> IsChannelPlaying()", true)]
		public bool IsPlaying()
		{
			return this.IsChannelPlaying();
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00005C28 File Offset: 0x00003E28
		public bool IsChannelPlaying()
		{
			return AudioClipPlayable.GetIsChannelPlayingInternal(ref this.m_Handle);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00005C48 File Offset: 0x00003E48
		public double GetStartDelay()
		{
			return AudioClipPlayable.GetStartDelayInternal(ref this.m_Handle);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00005C65 File Offset: 0x00003E65
		internal void SetStartDelay(double value)
		{
			AudioClipPlayable.SetStartDelayInternal(ref this.m_Handle, value);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00005C78 File Offset: 0x00003E78
		public double GetPauseDelay()
		{
			return AudioClipPlayable.GetPauseDelayInternal(ref this.m_Handle);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00005C98 File Offset: 0x00003E98
		internal void GetPauseDelay(double value)
		{
			double pauseDelayInternal = AudioClipPlayable.GetPauseDelayInternal(ref this.m_Handle);
			bool flag = this.m_Handle.GetPlayState() == PlayState.Playing && (value < 0.05 || (pauseDelayInternal != 0.0 && pauseDelayInternal < 0.05));
			if (flag)
			{
				throw new ArgumentException("AudioClipPlayable.pauseDelay: Setting new delay when existing delay is too small or 0.0 (" + pauseDelayInternal.ToString() + "), audio system will not be able to change in time");
			}
			AudioClipPlayable.SetPauseDelayInternal(ref this.m_Handle, value);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00005D1A File Offset: 0x00003F1A
		public void Seek(double startTime, double startDelay)
		{
			this.Seek(startTime, startDelay, 0.0);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00005D30 File Offset: 0x00003F30
		public void Seek(double startTime, double startDelay, [DefaultValue("0")] double duration)
		{
			AudioClipPlayable.SetStartDelayInternal(ref this.m_Handle, startDelay);
			bool flag = duration > 0.0;
			if (flag)
			{
				double num = startDelay + duration;
				bool flag2 = num >= this.m_Handle.GetDuration();
				if (flag2)
				{
					this.m_Handle.SetDone(true);
				}
				this.m_Handle.SetDuration(duration + startTime);
				AudioClipPlayable.SetPauseDelayInternal(ref this.m_Handle, startDelay + duration);
			}
			else
			{
				this.m_Handle.SetDone(true);
				this.m_Handle.SetDuration(double.MaxValue);
				AudioClipPlayable.SetPauseDelayInternal(ref this.m_Handle, 0.0);
			}
			this.m_Handle.SetTime(startTime);
			this.m_Handle.Play();
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00005DF8 File Offset: 0x00003FF8
		[NativeThrows]
		private static AudioClip GetClipInternal(ref PlayableHandle hdl)
		{
			return Unmarshal.UnmarshalUnityObject<AudioClip>(AudioClipPlayable.GetClipInternal_Injected(ref hdl));
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00005E10 File Offset: 0x00004010
		[NativeThrows]
		private static void SetClipInternal(ref PlayableHandle hdl, AudioClip clip)
		{
			AudioClipPlayable.SetClipInternal_Injected(ref hdl, Object.MarshalledUnityObject.Marshal<AudioClip>(clip));
		}

		// Token: 0x060002D4 RID: 724
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetLoopedInternal(ref PlayableHandle hdl);

		// Token: 0x060002D5 RID: 725
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLoopedInternal(ref PlayableHandle hdl, bool looped);

		// Token: 0x060002D6 RID: 726
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetVolumeInternal(ref PlayableHandle hdl);

		// Token: 0x060002D7 RID: 727
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetVolumeInternal(ref PlayableHandle hdl, float volume);

		// Token: 0x060002D8 RID: 728
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetClipPositionSecInternal(ref PlayableHandle hdl);

		// Token: 0x060002D9 RID: 729
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetStereoPanInternal(ref PlayableHandle hdl);

		// Token: 0x060002DA RID: 730
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetStereoPanInternal(ref PlayableHandle hdl, float stereoPan);

		// Token: 0x060002DB RID: 731
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetSpatialBlendInternal(ref PlayableHandle hdl);

		// Token: 0x060002DC RID: 732
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSpatialBlendInternal(ref PlayableHandle hdl, float spatialBlend);

		// Token: 0x060002DD RID: 733
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetIsChannelPlayingInternal(ref PlayableHandle hdl);

		// Token: 0x060002DE RID: 734
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern double GetStartDelayInternal(ref PlayableHandle hdl);

		// Token: 0x060002DF RID: 735
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetStartDelayInternal(ref PlayableHandle hdl, double delay);

		// Token: 0x060002E0 RID: 736
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern double GetPauseDelayInternal(ref PlayableHandle hdl);

		// Token: 0x060002E1 RID: 737
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPauseDelayInternal(ref PlayableHandle hdl, double delay);

		// Token: 0x060002E2 RID: 738 RVA: 0x00005E2C File Offset: 0x0000402C
		[NativeThrows]
		private static bool InternalCreateAudioClipPlayable(ref PlayableGraph graph, AudioClip clip, bool looping, ref PlayableHandle handle)
		{
			return AudioClipPlayable.InternalCreateAudioClipPlayable_Injected(ref graph, Object.MarshalledUnityObject.Marshal<AudioClip>(clip), looping, ref handle);
		}

		// Token: 0x060002E3 RID: 739
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ValidateType(ref PlayableHandle hdl);

		// Token: 0x060002E4 RID: 740
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetClipInternal_Injected(ref PlayableHandle hdl);

		// Token: 0x060002E5 RID: 741
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetClipInternal_Injected(ref PlayableHandle hdl, IntPtr clip);

		// Token: 0x060002E6 RID: 742
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool InternalCreateAudioClipPlayable_Injected(ref PlayableGraph graph, IntPtr clip, bool looping, ref PlayableHandle handle);

		// Token: 0x0400007B RID: 123
		private PlayableHandle m_Handle;
	}
}
