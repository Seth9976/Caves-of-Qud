using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Audio;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000017 RID: 23
	[StaticAccessor("AudioSourceBindings", StaticAccessorType.DoubleColon)]
	[RequireComponent(typeof(Transform))]
	public sealed class AudioSource : AudioBehaviour
	{
		// Token: 0x06000083 RID: 131 RVA: 0x00002D68 File Offset: 0x00000F68
		private static float GetPitch([NotNull] AudioSource source)
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
			return AudioSource.GetPitch_Injected(intPtr);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00002DA0 File Offset: 0x00000FA0
		private static void SetPitch([NotNull] AudioSource source, float pitch)
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
			AudioSource.SetPitch_Injected(intPtr, pitch);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00002DD8 File Offset: 0x00000FD8
		private static void PlayHelper([NotNull] AudioSource source, ulong delay)
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
			AudioSource.PlayHelper_Injected(intPtr, delay);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00002E10 File Offset: 0x00001010
		private void Play(double delay)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AudioSource.Play_Injected(intPtr, delay);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00002E34 File Offset: 0x00001034
		private static void PlayOneShotHelper([NotNull] AudioSource source, [NotNull] AudioClip clip, float volumeScale)
		{
			if (source == null)
			{
				ThrowHelper.ThrowArgumentNullException(source, "source");
			}
			if (clip == null)
			{
				ThrowHelper.ThrowArgumentNullException(clip, "clip");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(source);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(source, "source");
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<AudioClip>(clip);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(clip, "clip");
			}
			AudioSource.PlayOneShotHelper_Injected(intPtr, intPtr2, volumeScale);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00002E90 File Offset: 0x00001090
		private void Stop(bool stopOneShots)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AudioSource.Stop_Injected(intPtr, stopOneShots);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00002EB4 File Offset: 0x000010B4
		[NativeThrows]
		private static void SetCustomCurveHelper([NotNull] AudioSource source, AudioSourceCurveType type, AnimationCurve curve)
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
			AudioSource.SetCustomCurveHelper_Injected(intPtr, type, (curve == null) ? ((IntPtr)0) : AnimationCurve.BindingsMarshaller.ConvertToNative(curve));
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00002EFC File Offset: 0x000010FC
		private static AnimationCurve GetCustomCurveHelper([NotNull] AudioSource source, AudioSourceCurveType type)
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
			IntPtr customCurveHelper_Injected = AudioSource.GetCustomCurveHelper_Injected(intPtr, type);
			return (customCurveHelper_Injected == 0) ? null : AnimationCurve.BindingsMarshaller.ConvertToManaged(customCurveHelper_Injected);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00002F44 File Offset: 0x00001144
		private unsafe static void GetOutputDataHelper([NotNull] AudioSource source, [Out] float[] samples, int channel)
		{
			if (source == null)
			{
				ThrowHelper.ThrowArgumentNullException(source, "source");
			}
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(source);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowArgumentNullException(source, "source");
				}
				BlittableArrayWrapper blittableArrayWrapper;
				if (samples != null)
				{
					fixed (float[] array = samples)
					{
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
					}
				}
				AudioSource.GetOutputDataHelper_Injected(intPtr, out blittableArrayWrapper, channel);
			}
			finally
			{
				float[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<float>(ref array);
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00002FB8 File Offset: 0x000011B8
		[NativeThrows]
		private unsafe static void GetSpectrumDataHelper([NotNull] AudioSource source, [Out] float[] samples, int channel, FFTWindow window)
		{
			if (source == null)
			{
				ThrowHelper.ThrowArgumentNullException(source, "source");
			}
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(source);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowArgumentNullException(source, "source");
				}
				BlittableArrayWrapper blittableArrayWrapper;
				if (samples != null)
				{
					fixed (float[] array = samples)
					{
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
					}
				}
				AudioSource.GetSpectrumDataHelper_Injected(intPtr, out blittableArrayWrapper, channel, window);
			}
			finally
			{
				float[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<float>(ref array);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600008D RID: 141 RVA: 0x0000302C File Offset: 0x0000122C
		// (set) Token: 0x0600008E RID: 142 RVA: 0x00003050 File Offset: 0x00001250
		public float volume
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_volume_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioSource.set_volume_Injected(intPtr, value);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00003074 File Offset: 0x00001274
		// (set) Token: 0x06000090 RID: 144 RVA: 0x0000308C File Offset: 0x0000128C
		public float pitch
		{
			get
			{
				return AudioSource.GetPitch(this);
			}
			set
			{
				AudioSource.SetPitch(this, value);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00003098 File Offset: 0x00001298
		// (set) Token: 0x06000092 RID: 146 RVA: 0x000030BC File Offset: 0x000012BC
		[NativeProperty("SecPosition")]
		public float time
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_time_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioSource.set_time_Injected(intPtr, value);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000093 RID: 147 RVA: 0x000030E0 File Offset: 0x000012E0
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00003104 File Offset: 0x00001304
		[NativeProperty("SamplePosition")]
		public int timeSamples
		{
			[NativeMethod(IsThreadSafe = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_timeSamples_Injected(intPtr);
			}
			[NativeMethod(IsThreadSafe = true)]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioSource.set_timeSamples_Injected(intPtr, value);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00003127 File Offset: 0x00001327
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00003134 File Offset: 0x00001334
		public AudioClip clip
		{
			get
			{
				return this.resource as AudioClip;
			}
			set
			{
				this.resource = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00003140 File Offset: 0x00001340
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00003168 File Offset: 0x00001368
		public AudioResource resource
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<AudioResource>(AudioSource.get_resource_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioSource.set_resource_Injected(intPtr, Object.MarshalledUnityObject.Marshal<AudioResource>(value));
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00003190 File Offset: 0x00001390
		// (set) Token: 0x0600009A RID: 154 RVA: 0x000031B8 File Offset: 0x000013B8
		public AudioMixerGroup outputAudioMixerGroup
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<AudioMixerGroup>(AudioSource.get_outputAudioMixerGroup_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioSource.set_outputAudioMixerGroup_Injected(intPtr, Object.MarshalledUnityObject.Marshal<AudioMixerGroup>(value));
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000031E0 File Offset: 0x000013E0
		[ExcludeFromDocs]
		public void Play()
		{
			AudioSource.PlayHelper(this, 0UL);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000031EC File Offset: 0x000013EC
		public void Play([DefaultValue("0")] ulong delay)
		{
			AudioSource.PlayHelper(this, delay);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000031F7 File Offset: 0x000013F7
		public void PlayDelayed(float delay)
		{
			this.Play((delay < 0f) ? 0.0 : (-(double)delay));
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003217 File Offset: 0x00001417
		public void PlayScheduled(double time)
		{
			this.Play((time < 0.0) ? 0.0 : time);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003239 File Offset: 0x00001439
		[ExcludeFromDocs]
		public void PlayOneShot(AudioClip clip)
		{
			this.PlayOneShot(clip, 1f);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000324C File Offset: 0x0000144C
		public void PlayOneShot(AudioClip clip, [DefaultValue("1.0F")] float volumeScale)
		{
			bool flag = clip == null;
			if (flag)
			{
				Debug.LogWarning("PlayOneShot was called with a null AudioClip.");
			}
			else
			{
				AudioSource.PlayOneShotHelper(this, clip, volumeScale);
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000327C File Offset: 0x0000147C
		public void SetScheduledStartTime(double time)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AudioSource.SetScheduledStartTime_Injected(intPtr, time);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000032A0 File Offset: 0x000014A0
		public void SetScheduledEndTime(double time)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AudioSource.SetScheduledEndTime_Injected(intPtr, time);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000032C3 File Offset: 0x000014C3
		public void Stop()
		{
			this.Stop(true);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000032D0 File Offset: 0x000014D0
		public void Pause()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AudioSource.Pause_Injected(intPtr);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000032F4 File Offset: 0x000014F4
		public void UnPause()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AudioSource.UnPause_Injected(intPtr);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003318 File Offset: 0x00001518
		internal void SkipToNextElementIfHasContainer()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AudioSource.SkipToNextElementIfHasContainer_Injected(intPtr);
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x0000333C File Offset: 0x0000153C
		public bool isPlaying
		{
			[NativeName("IsPlayingScripting")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_isPlaying_Injected(intPtr);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00003360 File Offset: 0x00001560
		internal bool isContainerPlaying
		{
			[NativeName("IsContainerPlaying")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_isContainerPlaying_Injected(intPtr);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00003384 File Offset: 0x00001584
		internal ActivePlayable[] containerActivePlayables
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_containerActivePlayables_Injected(intPtr);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000AA RID: 170 RVA: 0x000033A8 File Offset: 0x000015A8
		public bool isVirtual
		{
			[NativeName("GetLastVirtualState")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_isVirtual_Injected(intPtr);
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000033CA File Offset: 0x000015CA
		[ExcludeFromDocs]
		public static void PlayClipAtPoint(AudioClip clip, Vector3 position)
		{
			AudioSource.PlayClipAtPoint(clip, position, 1f);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000033DC File Offset: 0x000015DC
		public static void PlayClipAtPoint(AudioClip clip, Vector3 position, [DefaultValue("1.0F")] float volume)
		{
			GameObject gameObject = new GameObject("One shot audio");
			gameObject.transform.position = position;
			AudioSource audioSource = (AudioSource)gameObject.AddComponent(typeof(AudioSource));
			audioSource.clip = clip;
			audioSource.spatialBlend = 1f;
			audioSource.volume = volume;
			audioSource.Play();
			Object.Destroy(gameObject, clip.length * ((Time.timeScale < 0.01f) ? 0.01f : Time.timeScale));
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00003464 File Offset: 0x00001664
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00003488 File Offset: 0x00001688
		public bool loop
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_loop_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioSource.set_loop_Injected(intPtr, value);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000AF RID: 175 RVA: 0x000034AC File Offset: 0x000016AC
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x000034D0 File Offset: 0x000016D0
		public bool ignoreListenerVolume
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_ignoreListenerVolume_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioSource.set_ignoreListenerVolume_Injected(intPtr, value);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x000034F4 File Offset: 0x000016F4
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x00003518 File Offset: 0x00001718
		public bool playOnAwake
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_playOnAwake_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioSource.set_playOnAwake_Injected(intPtr, value);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x0000353C File Offset: 0x0000173C
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00003560 File Offset: 0x00001760
		public bool ignoreListenerPause
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_ignoreListenerPause_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioSource.set_ignoreListenerPause_Injected(intPtr, value);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00003584 File Offset: 0x00001784
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x000035A8 File Offset: 0x000017A8
		public AudioVelocityUpdateMode velocityUpdateMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_velocityUpdateMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioSource.set_velocityUpdateMode_Injected(intPtr, value);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x000035CC File Offset: 0x000017CC
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x000035F0 File Offset: 0x000017F0
		[NativeProperty("StereoPan")]
		public float panStereo
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_panStereo_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioSource.set_panStereo_Injected(intPtr, value);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00003614 File Offset: 0x00001814
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00003638 File Offset: 0x00001838
		[NativeProperty("SpatialBlendMix")]
		public float spatialBlend
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_spatialBlend_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioSource.set_spatialBlend_Injected(intPtr, value);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000BB RID: 187 RVA: 0x0000365C File Offset: 0x0000185C
		// (set) Token: 0x060000BC RID: 188 RVA: 0x00003680 File Offset: 0x00001880
		public bool spatialize
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_spatialize_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioSource.set_spatialize_Injected(intPtr, value);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000BD RID: 189 RVA: 0x000036A4 File Offset: 0x000018A4
		// (set) Token: 0x060000BE RID: 190 RVA: 0x000036C8 File Offset: 0x000018C8
		public bool spatializePostEffects
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_spatializePostEffects_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioSource.set_spatializePostEffects_Injected(intPtr, value);
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000036EB File Offset: 0x000018EB
		public void SetCustomCurve(AudioSourceCurveType type, AnimationCurve curve)
		{
			AudioSource.SetCustomCurveHelper(this, type, curve);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000036F8 File Offset: 0x000018F8
		public AnimationCurve GetCustomCurve(AudioSourceCurveType type)
		{
			return AudioSource.GetCustomCurveHelper(this, type);
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00003714 File Offset: 0x00001914
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x00003738 File Offset: 0x00001938
		public float reverbZoneMix
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_reverbZoneMix_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioSource.set_reverbZoneMix_Injected(intPtr, value);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x0000375C File Offset: 0x0000195C
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x00003780 File Offset: 0x00001980
		public bool bypassEffects
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_bypassEffects_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioSource.set_bypassEffects_Injected(intPtr, value);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x000037A4 File Offset: 0x000019A4
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x000037C8 File Offset: 0x000019C8
		public bool bypassListenerEffects
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_bypassListenerEffects_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioSource.set_bypassListenerEffects_Injected(intPtr, value);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x000037EC File Offset: 0x000019EC
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00003810 File Offset: 0x00001A10
		public bool bypassReverbZones
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_bypassReverbZones_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioSource.set_bypassReverbZones_Injected(intPtr, value);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00003834 File Offset: 0x00001A34
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00003858 File Offset: 0x00001A58
		public float dopplerLevel
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_dopplerLevel_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioSource.set_dopplerLevel_Injected(intPtr, value);
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000CB RID: 203 RVA: 0x0000387C File Offset: 0x00001A7C
		// (set) Token: 0x060000CC RID: 204 RVA: 0x000038A0 File Offset: 0x00001AA0
		public float spread
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_spread_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioSource.set_spread_Injected(intPtr, value);
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000CD RID: 205 RVA: 0x000038C4 File Offset: 0x00001AC4
		// (set) Token: 0x060000CE RID: 206 RVA: 0x000038E8 File Offset: 0x00001AE8
		public int priority
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_priority_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioSource.set_priority_Injected(intPtr, value);
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000CF RID: 207 RVA: 0x0000390C File Offset: 0x00001B0C
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00003930 File Offset: 0x00001B30
		public bool mute
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_mute_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioSource.set_mute_Injected(intPtr, value);
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00003954 File Offset: 0x00001B54
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00003978 File Offset: 0x00001B78
		public float minDistance
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_minDistance_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioSource.set_minDistance_Injected(intPtr, value);
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x0000399C File Offset: 0x00001B9C
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x000039C0 File Offset: 0x00001BC0
		public float maxDistance
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_maxDistance_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioSource.set_maxDistance_Injected(intPtr, value);
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x000039E4 File Offset: 0x00001BE4
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x00003A08 File Offset: 0x00001C08
		public AudioRolloffMode rolloffMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_rolloffMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioSource.set_rolloffMode_Injected(intPtr, value);
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00003A2C File Offset: 0x00001C2C
		[Obsolete("GetOutputData returning a float[] is deprecated, use GetOutputData and pass a pre allocated array instead.")]
		public float[] GetOutputData(int numSamples, int channel)
		{
			float[] array = new float[numSamples];
			AudioSource.GetOutputDataHelper(this, array, channel);
			return array;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00003A4F File Offset: 0x00001C4F
		public void GetOutputData(float[] samples, int channel)
		{
			AudioSource.GetOutputDataHelper(this, samples, channel);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00003A5C File Offset: 0x00001C5C
		[Obsolete("GetSpectrumData returning a float[] is deprecated, use GetSpectrumData and pass a pre allocated array instead.")]
		public float[] GetSpectrumData(int numSamples, int channel, FFTWindow window)
		{
			float[] array = new float[numSamples];
			AudioSource.GetSpectrumDataHelper(this, array, channel, window);
			return array;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00003A80 File Offset: 0x00001C80
		public void GetSpectrumData(float[] samples, int channel, FFTWindow window)
		{
			AudioSource.GetSpectrumDataHelper(this, samples, channel, window);
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00003A90 File Offset: 0x00001C90
		// (set) Token: 0x060000DC RID: 220 RVA: 0x00003AB2 File Offset: 0x00001CB2
		[Obsolete("minVolume is not supported anymore. Use min-, maxDistance and rolloffMode instead.", true)]
		public float minVolume
		{
			get
			{
				Debug.LogError("minVolume is not supported anymore. Use min-, maxDistance and rolloffMode instead.");
				return 0f;
			}
			set
			{
				Debug.LogError("minVolume is not supported anymore. Use min-, maxDistance and rolloffMode instead.");
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00003AC0 File Offset: 0x00001CC0
		// (set) Token: 0x060000DE RID: 222 RVA: 0x00003AE2 File Offset: 0x00001CE2
		[Obsolete("maxVolume is not supported anymore. Use min-, maxDistance and rolloffMode instead.", true)]
		public float maxVolume
		{
			get
			{
				Debug.LogError("maxVolume is not supported anymore. Use min-, maxDistance and rolloffMode instead.");
				return 0f;
			}
			set
			{
				Debug.LogError("maxVolume is not supported anymore. Use min-, maxDistance and rolloffMode instead.");
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00003AF0 File Offset: 0x00001CF0
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x00003B12 File Offset: 0x00001D12
		[Obsolete("rolloffFactor is not supported anymore. Use min-, maxDistance and rolloffMode instead.", true)]
		public float rolloffFactor
		{
			get
			{
				Debug.LogError("rolloffFactor is not supported anymore. Use min-, maxDistance and rolloffMode instead.");
				return 0f;
			}
			set
			{
				Debug.LogError("rolloffFactor is not supported anymore. Use min-, maxDistance and rolloffMode instead.");
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00003B20 File Offset: 0x00001D20
		public bool SetSpatializerFloat(int index, float value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return AudioSource.SetSpatializerFloat_Injected(intPtr, index, value);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00003B44 File Offset: 0x00001D44
		public bool GetSpatializerFloat(int index, out float value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return AudioSource.GetSpatializerFloat_Injected(intPtr, index, out value);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00003B68 File Offset: 0x00001D68
		public bool GetAmbisonicDecoderFloat(int index, out float value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return AudioSource.GetAmbisonicDecoderFloat_Injected(intPtr, index, out value);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00003B8C File Offset: 0x00001D8C
		public bool SetAmbisonicDecoderFloat(int index, float value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return AudioSource.SetAmbisonicDecoderFloat_Injected(intPtr, index, value);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00003BB0 File Offset: 0x00001DB0
		internal float GetAudioRandomContainerRuntimeMeterValue()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return AudioSource.GetAudioRandomContainerRuntimeMeterValue_Injected(intPtr);
		}

		// Token: 0x060000E7 RID: 231
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetPitch_Injected(IntPtr source);

		// Token: 0x060000E8 RID: 232
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPitch_Injected(IntPtr source, float pitch);

		// Token: 0x060000E9 RID: 233
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PlayHelper_Injected(IntPtr source, ulong delay);

		// Token: 0x060000EA RID: 234
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Play_Injected(IntPtr _unity_self, double delay);

		// Token: 0x060000EB RID: 235
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PlayOneShotHelper_Injected(IntPtr source, IntPtr clip, float volumeScale);

		// Token: 0x060000EC RID: 236
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Stop_Injected(IntPtr _unity_self, bool stopOneShots);

		// Token: 0x060000ED RID: 237
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetCustomCurveHelper_Injected(IntPtr source, AudioSourceCurveType type, IntPtr curve);

		// Token: 0x060000EE RID: 238
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetCustomCurveHelper_Injected(IntPtr source, AudioSourceCurveType type);

		// Token: 0x060000EF RID: 239
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetOutputDataHelper_Injected(IntPtr source, out BlittableArrayWrapper samples, int channel);

		// Token: 0x060000F0 RID: 240
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSpectrumDataHelper_Injected(IntPtr source, out BlittableArrayWrapper samples, int channel, FFTWindow window);

		// Token: 0x060000F1 RID: 241
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_volume_Injected(IntPtr _unity_self);

		// Token: 0x060000F2 RID: 242
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_volume_Injected(IntPtr _unity_self, float value);

		// Token: 0x060000F3 RID: 243
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_time_Injected(IntPtr _unity_self);

		// Token: 0x060000F4 RID: 244
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_time_Injected(IntPtr _unity_self, float value);

		// Token: 0x060000F5 RID: 245
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_timeSamples_Injected(IntPtr _unity_self);

		// Token: 0x060000F6 RID: 246
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_timeSamples_Injected(IntPtr _unity_self, int value);

		// Token: 0x060000F7 RID: 247
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_resource_Injected(IntPtr _unity_self);

		// Token: 0x060000F8 RID: 248
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_resource_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x060000F9 RID: 249
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_outputAudioMixerGroup_Injected(IntPtr _unity_self);

		// Token: 0x060000FA RID: 250
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_outputAudioMixerGroup_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x060000FB RID: 251
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetScheduledStartTime_Injected(IntPtr _unity_self, double time);

		// Token: 0x060000FC RID: 252
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetScheduledEndTime_Injected(IntPtr _unity_self, double time);

		// Token: 0x060000FD RID: 253
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Pause_Injected(IntPtr _unity_self);

		// Token: 0x060000FE RID: 254
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UnPause_Injected(IntPtr _unity_self);

		// Token: 0x060000FF RID: 255
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SkipToNextElementIfHasContainer_Injected(IntPtr _unity_self);

		// Token: 0x06000100 RID: 256
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isPlaying_Injected(IntPtr _unity_self);

		// Token: 0x06000101 RID: 257
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isContainerPlaying_Injected(IntPtr _unity_self);

		// Token: 0x06000102 RID: 258
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ActivePlayable[] get_containerActivePlayables_Injected(IntPtr _unity_self);

		// Token: 0x06000103 RID: 259
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isVirtual_Injected(IntPtr _unity_self);

		// Token: 0x06000104 RID: 260
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_loop_Injected(IntPtr _unity_self);

		// Token: 0x06000105 RID: 261
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_loop_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000106 RID: 262
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_ignoreListenerVolume_Injected(IntPtr _unity_self);

		// Token: 0x06000107 RID: 263
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_ignoreListenerVolume_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000108 RID: 264
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_playOnAwake_Injected(IntPtr _unity_self);

		// Token: 0x06000109 RID: 265
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_playOnAwake_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600010A RID: 266
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_ignoreListenerPause_Injected(IntPtr _unity_self);

		// Token: 0x0600010B RID: 267
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_ignoreListenerPause_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600010C RID: 268
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AudioVelocityUpdateMode get_velocityUpdateMode_Injected(IntPtr _unity_self);

		// Token: 0x0600010D RID: 269
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_velocityUpdateMode_Injected(IntPtr _unity_self, AudioVelocityUpdateMode value);

		// Token: 0x0600010E RID: 270
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_panStereo_Injected(IntPtr _unity_self);

		// Token: 0x0600010F RID: 271
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_panStereo_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000110 RID: 272
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_spatialBlend_Injected(IntPtr _unity_self);

		// Token: 0x06000111 RID: 273
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_spatialBlend_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000112 RID: 274
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_spatialize_Injected(IntPtr _unity_self);

		// Token: 0x06000113 RID: 275
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_spatialize_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000114 RID: 276
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_spatializePostEffects_Injected(IntPtr _unity_self);

		// Token: 0x06000115 RID: 277
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_spatializePostEffects_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000116 RID: 278
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_reverbZoneMix_Injected(IntPtr _unity_self);

		// Token: 0x06000117 RID: 279
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_reverbZoneMix_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000118 RID: 280
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_bypassEffects_Injected(IntPtr _unity_self);

		// Token: 0x06000119 RID: 281
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_bypassEffects_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600011A RID: 282
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_bypassListenerEffects_Injected(IntPtr _unity_self);

		// Token: 0x0600011B RID: 283
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_bypassListenerEffects_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600011C RID: 284
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_bypassReverbZones_Injected(IntPtr _unity_self);

		// Token: 0x0600011D RID: 285
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_bypassReverbZones_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600011E RID: 286
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_dopplerLevel_Injected(IntPtr _unity_self);

		// Token: 0x0600011F RID: 287
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_dopplerLevel_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000120 RID: 288
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_spread_Injected(IntPtr _unity_self);

		// Token: 0x06000121 RID: 289
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_spread_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000122 RID: 290
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_priority_Injected(IntPtr _unity_self);

		// Token: 0x06000123 RID: 291
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_priority_Injected(IntPtr _unity_self, int value);

		// Token: 0x06000124 RID: 292
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_mute_Injected(IntPtr _unity_self);

		// Token: 0x06000125 RID: 293
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_mute_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000126 RID: 294
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_minDistance_Injected(IntPtr _unity_self);

		// Token: 0x06000127 RID: 295
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_minDistance_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000128 RID: 296
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_maxDistance_Injected(IntPtr _unity_self);

		// Token: 0x06000129 RID: 297
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_maxDistance_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600012A RID: 298
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AudioRolloffMode get_rolloffMode_Injected(IntPtr _unity_self);

		// Token: 0x0600012B RID: 299
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_rolloffMode_Injected(IntPtr _unity_self, AudioRolloffMode value);

		// Token: 0x0600012C RID: 300
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetSpatializerFloat_Injected(IntPtr _unity_self, int index, float value);

		// Token: 0x0600012D RID: 301
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetSpatializerFloat_Injected(IntPtr _unity_self, int index, out float value);

		// Token: 0x0600012E RID: 302
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetAmbisonicDecoderFloat_Injected(IntPtr _unity_self, int index, out float value);

		// Token: 0x0600012F RID: 303
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetAmbisonicDecoderFloat_Injected(IntPtr _unity_self, int index, float value);

		// Token: 0x06000130 RID: 304
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetAudioRandomContainerRuntimeMeterValue_Injected(IntPtr _unity_self);
	}
}
