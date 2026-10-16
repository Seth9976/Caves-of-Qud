using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Audio;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000012 RID: 18
	[StaticAccessor("AudioClipBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/Audio/Public/ScriptBindings/Audio.bindings.h")]
	public sealed class AudioClip : AudioResource
	{
		// Token: 0x06000036 RID: 54 RVA: 0x0000249B File Offset: 0x0000069B
		private AudioClip()
		{
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000024B4 File Offset: 0x000006B4
		private unsafe static bool GetData([NotNull] AudioClip clip, Span<float> data, int samplesOffset)
		{
			if (clip == null)
			{
				ThrowHelper.ThrowArgumentNullException(clip, "clip");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioClip>(clip);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(clip, "clip");
			}
			Span<float> span = data;
			bool data_Injected;
			fixed (float* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				data_Injected = AudioClip.GetData_Injected(intPtr, ref managedSpanWrapper, samplesOffset);
			}
			return data_Injected;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000250C File Offset: 0x0000070C
		private unsafe static bool SetData([NotNull] AudioClip clip, ReadOnlySpan<float> data, int samplesOffset)
		{
			if (clip == null)
			{
				ThrowHelper.ThrowArgumentNullException(clip, "clip");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioClip>(clip);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(clip, "clip");
			}
			ReadOnlySpan<float> readOnlySpan = data;
			bool flag;
			fixed (float* pinnableReference = readOnlySpan.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, readOnlySpan.Length);
				flag = AudioClip.SetData_Injected(intPtr, ref managedSpanWrapper, samplesOffset);
			}
			return flag;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002564 File Offset: 0x00000764
		private static AudioClip Construct_Internal()
		{
			return Unmarshal.UnmarshalUnityObject<AudioClip>(AudioClip.Construct_Internal_Injected());
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000257C File Offset: 0x0000077C
		private string GetName()
		{
			string stringAndDispose;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				AudioClip.GetName_Injected(intPtr, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000025BC File Offset: 0x000007BC
		private unsafe void CreateUserSound(string name, int lengthSamples, int channels, int frequency, bool stream)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				AudioClip.CreateUserSound_Injected(intPtr, ref managedSpanWrapper, lengthSamples, channels, frequency, stream);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002628 File Offset: 0x00000828
		[NativeProperty("LengthSec")]
		public float length
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioClip.get_length_Injected(intPtr);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003D RID: 61 RVA: 0x0000264C File Offset: 0x0000084C
		[NativeProperty("SampleCount")]
		public int samples
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioClip.get_samples_Injected(intPtr);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002670 File Offset: 0x00000870
		[NativeProperty("ChannelCount")]
		public int channels
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioClip.get_channels_Injected(intPtr);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002694 File Offset: 0x00000894
		public int frequency
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioClip.get_frequency_Injected(intPtr);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000026B8 File Offset: 0x000008B8
		[Obsolete("Use AudioClip.loadState instead to get more detailed information about the loading process.")]
		public bool isReadyToPlay
		{
			[NativeName("ReadyToPlay")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioClip.get_isReadyToPlay_Injected(intPtr);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000026DC File Offset: 0x000008DC
		public AudioClipLoadType loadType
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioClip.get_loadType_Injected(intPtr);
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002700 File Offset: 0x00000900
		public bool LoadAudioData()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioClip>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return AudioClip.LoadAudioData_Injected(intPtr);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002724 File Offset: 0x00000924
		public bool UnloadAudioData()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioClip>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return AudioClip.UnloadAudioData_Injected(intPtr);
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002748 File Offset: 0x00000948
		public bool preloadAudioData
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioClip.get_preloadAudioData_Injected(intPtr);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000045 RID: 69 RVA: 0x0000276C File Offset: 0x0000096C
		public bool ambisonic
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioClip.get_ambisonic_Injected(intPtr);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002790 File Offset: 0x00000990
		public bool loadInBackground
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioClip.get_loadInBackground_Injected(intPtr);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000047 RID: 71 RVA: 0x000027B4 File Offset: 0x000009B4
		public AudioDataLoadState loadState
		{
			[NativeMethod(Name = "AudioClipBindings::GetLoadState", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioClip.get_loadState_Injected(intPtr);
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000027D8 File Offset: 0x000009D8
		public bool GetData(Span<float> data, int offsetSamples)
		{
			bool flag = this.channels <= 0;
			bool flag2;
			if (flag)
			{
				Debug.Log("AudioClip.GetData failed; AudioClip " + this.GetName() + " contains no data");
				flag2 = false;
			}
			else
			{
				flag2 = AudioClip.GetData(this, data, offsetSamples);
			}
			return flag2;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002824 File Offset: 0x00000A24
		public bool GetData(float[] data, int offsetSamples)
		{
			bool flag = this.channels <= 0;
			bool flag2;
			if (flag)
			{
				Debug.Log("AudioClip.GetData failed; AudioClip " + this.GetName() + " contains no data");
				flag2 = false;
			}
			else
			{
				flag2 = AudioClip.GetData(this, data.AsSpan<float>(), offsetSamples);
			}
			return flag2;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002874 File Offset: 0x00000A74
		public bool SetData(float[] data, int offsetSamples)
		{
			bool flag = this.channels <= 0;
			bool flag2;
			if (flag)
			{
				Debug.Log("AudioClip.SetData failed; AudioClip " + this.GetName() + " contains no data");
				flag2 = false;
			}
			else
			{
				bool flag3 = offsetSamples < 0 || offsetSamples >= this.samples;
				if (flag3)
				{
					throw new ArgumentException("AudioClip.SetData failed; invalid offsetSamples");
				}
				bool flag4 = data == null || data.Length == 0;
				if (flag4)
				{
					throw new ArgumentException("AudioClip.SetData failed; invalid data");
				}
				flag2 = AudioClip.SetData(this, data.AsSpan<float>(), offsetSamples);
			}
			return flag2;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002904 File Offset: 0x00000B04
		public bool SetData(ReadOnlySpan<float> data, int offsetSamples)
		{
			bool flag = this.channels <= 0;
			bool flag2;
			if (flag)
			{
				Debug.Log("AudioClip.SetData failed; AudioClip " + this.GetName() + " contains no data");
				flag2 = false;
			}
			else
			{
				bool flag3 = offsetSamples < 0 || offsetSamples >= this.samples;
				if (flag3)
				{
					throw new ArgumentException("AudioClip.SetData failed; invalid offsetSamples");
				}
				bool flag4 = data.Length == 0;
				if (flag4)
				{
					throw new ArgumentException("AudioClip.SetData failed; invalid data");
				}
				flag2 = AudioClip.SetData(this, data, offsetSamples);
			}
			return flag2;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000298C File Offset: 0x00000B8C
		[Obsolete("The _3D argument of AudioClip is deprecated. Use the spatialBlend property of AudioSource instead to morph between 2D and 3D playback.")]
		public static AudioClip Create(string name, int lengthSamples, int channels, int frequency, bool _3D, bool stream)
		{
			return AudioClip.Create(name, lengthSamples, channels, frequency, stream);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000029AC File Offset: 0x00000BAC
		[Obsolete("The _3D argument of AudioClip is deprecated. Use the spatialBlend property of AudioSource instead to morph between 2D and 3D playback.")]
		public static AudioClip Create(string name, int lengthSamples, int channels, int frequency, bool _3D, bool stream, AudioClip.PCMReaderCallback pcmreadercallback)
		{
			return AudioClip.Create(name, lengthSamples, channels, frequency, stream, pcmreadercallback, null);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000029CC File Offset: 0x00000BCC
		[Obsolete("The _3D argument of AudioClip is deprecated. Use the spatialBlend property of AudioSource instead to morph between 2D and 3D playback.")]
		public static AudioClip Create(string name, int lengthSamples, int channels, int frequency, bool _3D, bool stream, AudioClip.PCMReaderCallback pcmreadercallback, AudioClip.PCMSetPositionCallback pcmsetpositioncallback)
		{
			return AudioClip.Create(name, lengthSamples, channels, frequency, stream, pcmreadercallback, pcmsetpositioncallback);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000029F0 File Offset: 0x00000BF0
		public static AudioClip Create(string name, int lengthSamples, int channels, int frequency, bool stream)
		{
			return AudioClip.Create(name, lengthSamples, channels, frequency, stream, null, null);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002A14 File Offset: 0x00000C14
		public static AudioClip Create(string name, int lengthSamples, int channels, int frequency, bool stream, AudioClip.PCMReaderCallback pcmreadercallback)
		{
			return AudioClip.Create(name, lengthSamples, channels, frequency, stream, pcmreadercallback, null);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002A38 File Offset: 0x00000C38
		public static AudioClip Create(string name, int lengthSamples, int channels, int frequency, bool stream, AudioClip.PCMReaderCallback pcmreadercallback, AudioClip.PCMSetPositionCallback pcmsetpositioncallback)
		{
			bool flag = name == null;
			if (flag)
			{
				throw new NullReferenceException();
			}
			bool flag2 = lengthSamples <= 0;
			if (flag2)
			{
				throw new ArgumentException("Length of created clip must be larger than 0");
			}
			bool flag3 = channels <= 0;
			if (flag3)
			{
				throw new ArgumentException("Number of channels in created clip must be greater than 0");
			}
			bool flag4 = frequency <= 0;
			if (flag4)
			{
				throw new ArgumentException("Frequency in created clip must be greater than 0");
			}
			AudioClip audioClip = AudioClip.Construct_Internal();
			bool flag5 = pcmreadercallback != null;
			if (flag5)
			{
				audioClip.m_PCMReaderCallback += pcmreadercallback;
			}
			bool flag6 = pcmsetpositioncallback != null;
			if (flag6)
			{
				audioClip.m_PCMSetPositionCallback += pcmsetpositioncallback;
			}
			audioClip.CreateUserSound(name, lengthSamples, channels, frequency, stream);
			return audioClip;
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000052 RID: 82 RVA: 0x00002ADC File Offset: 0x00000CDC
		// (remove) Token: 0x06000053 RID: 83 RVA: 0x00002B14 File Offset: 0x00000D14
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private event AudioClip.PCMReaderCallback m_PCMReaderCallback = null;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000054 RID: 84 RVA: 0x00002B4C File Offset: 0x00000D4C
		// (remove) Token: 0x06000055 RID: 85 RVA: 0x00002B84 File Offset: 0x00000D84
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private event AudioClip.PCMSetPositionCallback m_PCMSetPositionCallback = null;

		// Token: 0x06000056 RID: 86 RVA: 0x00002BBC File Offset: 0x00000DBC
		[RequiredByNativeCode]
		private void InvokePCMReaderCallback_Internal(float[] data)
		{
			bool flag = this.m_PCMReaderCallback != null;
			if (flag)
			{
				this.m_PCMReaderCallback(data);
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002BE4 File Offset: 0x00000DE4
		[RequiredByNativeCode]
		private void InvokePCMSetPositionCallback_Internal(int position)
		{
			bool flag = this.m_PCMSetPositionCallback != null;
			if (flag)
			{
				this.m_PCMSetPositionCallback(position);
			}
		}

		// Token: 0x06000058 RID: 88
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetData_Injected(IntPtr clip, ref ManagedSpanWrapper data, int samplesOffset);

		// Token: 0x06000059 RID: 89
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetData_Injected(IntPtr clip, ref ManagedSpanWrapper data, int samplesOffset);

		// Token: 0x0600005A RID: 90
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Construct_Internal_Injected();

		// Token: 0x0600005B RID: 91
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetName_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x0600005C RID: 92
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreateUserSound_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name, int lengthSamples, int channels, int frequency, bool stream);

		// Token: 0x0600005D RID: 93
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_length_Injected(IntPtr _unity_self);

		// Token: 0x0600005E RID: 94
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_samples_Injected(IntPtr _unity_self);

		// Token: 0x0600005F RID: 95
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_channels_Injected(IntPtr _unity_self);

		// Token: 0x06000060 RID: 96
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_frequency_Injected(IntPtr _unity_self);

		// Token: 0x06000061 RID: 97
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isReadyToPlay_Injected(IntPtr _unity_self);

		// Token: 0x06000062 RID: 98
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AudioClipLoadType get_loadType_Injected(IntPtr _unity_self);

		// Token: 0x06000063 RID: 99
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool LoadAudioData_Injected(IntPtr _unity_self);

		// Token: 0x06000064 RID: 100
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool UnloadAudioData_Injected(IntPtr _unity_self);

		// Token: 0x06000065 RID: 101
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_preloadAudioData_Injected(IntPtr _unity_self);

		// Token: 0x06000066 RID: 102
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_ambisonic_Injected(IntPtr _unity_self);

		// Token: 0x06000067 RID: 103
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_loadInBackground_Injected(IntPtr _unity_self);

		// Token: 0x06000068 RID: 104
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AudioDataLoadState get_loadState_Injected(IntPtr _unity_self);

		// Token: 0x02000013 RID: 19
		// (Invoke) Token: 0x0600006A RID: 106
		public delegate void PCMReaderCallback(float[] data);

		// Token: 0x02000014 RID: 20
		// (Invoke) Token: 0x0600006E RID: 110
		public delegate void PCMSetPositionCallback(int position);
	}
}
