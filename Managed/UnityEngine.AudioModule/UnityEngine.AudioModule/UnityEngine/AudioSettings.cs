using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200000F RID: 15
	[NativeHeader("Modules/Audio/Public/ScriptBindings/Audio.bindings.h")]
	[StaticAccessor("GetAudioManager()", StaticAccessorType.Dot)]
	public sealed class AudioSettings
	{
		// Token: 0x06000008 RID: 8
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AudioSpeakerMode GetSpeakerMode();

		// Token: 0x06000009 RID: 9 RVA: 0x00002088 File Offset: 0x00000288
		[NativeMethod(Name = "AudioSettings::SetConfiguration", IsFreeFunction = true)]
		[NativeThrows]
		private static bool SetConfiguration(AudioConfiguration config)
		{
			return AudioSettings.SetConfiguration_Injected(ref config);
		}

		// Token: 0x0600000A RID: 10
		[NativeMethod(Name = "AudioSettings::GetSampleRate", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetSampleRate();

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000B RID: 11
		public static extern AudioSpeakerMode driverCapabilities
		{
			[NativeName("GetSpeakerModeCaps")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000209C File Offset: 0x0000029C
		// (set) Token: 0x0600000D RID: 13 RVA: 0x000020B4 File Offset: 0x000002B4
		public static AudioSpeakerMode speakerMode
		{
			get
			{
				return AudioSettings.GetSpeakerMode();
			}
			set
			{
				Debug.LogWarning("Setting AudioSettings.speakerMode is deprecated and has been replaced by audio project settings and the AudioSettings.GetConfiguration/AudioSettings.Reset API.");
				AudioConfiguration configuration = AudioSettings.GetConfiguration();
				configuration.speakerMode = value;
				bool flag = !AudioSettings.SetConfiguration(configuration);
				if (flag)
				{
					Debug.LogWarning("Setting AudioSettings.speakerMode failed");
				}
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600000E RID: 14
		internal static extern int profilerCaptureFlags
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600000F RID: 15
		public static extern double dspTime
		{
			[NativeMethod(Name = "GetDSPTime", IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000020F4 File Offset: 0x000002F4
		// (set) Token: 0x06000011 RID: 17 RVA: 0x0000210C File Offset: 0x0000030C
		public static int outputSampleRate
		{
			get
			{
				return AudioSettings.GetSampleRate();
			}
			set
			{
				Debug.LogWarning("Setting AudioSettings.outputSampleRate is deprecated and has been replaced by audio project settings and the AudioSettings.GetConfiguration/AudioSettings.Reset API.");
				AudioConfiguration configuration = AudioSettings.GetConfiguration();
				configuration.sampleRate = value;
				bool flag = !AudioSettings.SetConfiguration(configuration);
				if (flag)
				{
					Debug.LogWarning("Setting AudioSettings.outputSampleRate failed");
				}
			}
		}

		// Token: 0x06000012 RID: 18
		[NativeMethod(Name = "AudioSettings::GetDSPBufferSize", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void GetDSPBufferSize(out int bufferLength, out int numBuffers);

		// Token: 0x06000013 RID: 19 RVA: 0x0000214C File Offset: 0x0000034C
		[Obsolete("AudioSettings.SetDSPBufferSize is deprecated and has been replaced by audio project settings and the AudioSettings.GetConfiguration/AudioSettings.Reset API.")]
		public static void SetDSPBufferSize(int bufferLength, int numBuffers)
		{
			Debug.LogWarning("AudioSettings.SetDSPBufferSize is deprecated and has been replaced by audio project settings and the AudioSettings.GetConfiguration/AudioSettings.Reset API.");
			AudioConfiguration configuration = AudioSettings.GetConfiguration();
			configuration.dspBufferSize = bufferLength;
			bool flag = !AudioSettings.SetConfiguration(configuration);
			if (flag)
			{
				Debug.LogWarning("SetDSPBufferSize failed");
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000218C File Offset: 0x0000038C
		[NativeName("GetCurrentSpatializerDefinitionName")]
		public static string GetSpatializerPluginName()
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				AudioSettings.GetSpatializerPluginName_Injected(out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000021BC File Offset: 0x000003BC
		public static AudioConfiguration GetConfiguration()
		{
			AudioConfiguration audioConfiguration;
			AudioSettings.GetConfiguration_Injected(out audioConfiguration);
			return audioConfiguration;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000021D4 File Offset: 0x000003D4
		public static bool Reset(AudioConfiguration config)
		{
			return AudioSettings.SetConfiguration(config);
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000017 RID: 23 RVA: 0x000021EC File Offset: 0x000003EC
		// (remove) Token: 0x06000018 RID: 24 RVA: 0x00002220 File Offset: 0x00000420
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event AudioSettings.AudioConfigurationChangeHandler OnAudioConfigurationChanged;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000019 RID: 25 RVA: 0x00002254 File Offset: 0x00000454
		// (remove) Token: 0x0600001A RID: 26 RVA: 0x00002288 File Offset: 0x00000488
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal static event Action OnAudioSystemShuttingDown;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600001B RID: 27 RVA: 0x000022BC File Offset: 0x000004BC
		// (remove) Token: 0x0600001C RID: 28 RVA: 0x000022F0 File Offset: 0x000004F0
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal static event Action OnAudioSystemStartedUp;

		// Token: 0x0600001D RID: 29 RVA: 0x00002324 File Offset: 0x00000524
		[RequiredByNativeCode]
		internal static void InvokeOnAudioConfigurationChanged(bool deviceWasChanged)
		{
			bool flag = AudioSettings.OnAudioConfigurationChanged != null;
			if (flag)
			{
				AudioSettings.OnAudioConfigurationChanged(deviceWasChanged);
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000234A File Offset: 0x0000054A
		[RequiredByNativeCode]
		internal static void InvokeOnAudioSystemShuttingDown()
		{
			Action onAudioSystemShuttingDown = AudioSettings.OnAudioSystemShuttingDown;
			if (onAudioSystemShuttingDown != null)
			{
				onAudioSystemShuttingDown();
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000235D File Offset: 0x0000055D
		[RequiredByNativeCode]
		internal static void InvokeOnAudioSystemStartedUp()
		{
			Action onAudioSystemStartedUp = AudioSettings.OnAudioSystemStartedUp;
			if (onAudioSystemStartedUp != null)
			{
				onAudioSystemStartedUp();
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000020 RID: 32
		// (set) Token: 0x06000021 RID: 33
		internal static extern bool unityAudioDisabled
		{
			[NativeName("IsAudioDisabled")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeName("DisableAudio")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002370 File Offset: 0x00000570
		[NativeMethod(Name = "AudioSettings::GetCurrentAmbisonicDefinitionName", IsFreeFunction = true)]
		internal static string GetAmbisonicDecoderPluginName()
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				AudioSettings.GetAmbisonicDecoderPluginName_Injected(out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000023A0 File Offset: 0x000005A0
		// (set) Token: 0x06000024 RID: 36 RVA: 0x000023B3 File Offset: 0x000005B3
		public static AudioSpatialExperience audioSpatialExperience
		{
			get
			{
				return AudioSpatialExperience.Bypassed;
			}
			set
			{
				Debug.LogWarning("AudioSettings.audioSpatialExperience is not implemented on this platform.");
			}
		}

		// Token: 0x06000026 RID: 38
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetConfiguration_Injected([In] ref AudioConfiguration config);

		// Token: 0x06000027 RID: 39
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSpatializerPluginName_Injected(out ManagedSpanWrapper ret);

		// Token: 0x06000028 RID: 40
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetConfiguration_Injected(out AudioConfiguration ret);

		// Token: 0x06000029 RID: 41
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetAmbisonicDecoderPluginName_Injected(out ManagedSpanWrapper ret);

		// Token: 0x02000010 RID: 16
		// (Invoke) Token: 0x0600002B RID: 43
		public delegate void AudioConfigurationChangeHandler(bool deviceWasChanged);

		// Token: 0x02000011 RID: 17
		public static class Mobile
		{
			// Token: 0x1700000F RID: 15
			// (get) Token: 0x0600002E RID: 46 RVA: 0x000023CC File Offset: 0x000005CC
			public static bool muteState
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000010 RID: 16
			// (get) Token: 0x0600002F RID: 47 RVA: 0x000023E0 File Offset: 0x000005E0
			// (set) Token: 0x06000030 RID: 48 RVA: 0x000023F3 File Offset: 0x000005F3
			public static bool stopAudioOutputOnMute
			{
				get
				{
					return false;
				}
				set
				{
					Debug.LogWarning("Setting AudioSettings.Mobile.stopAudioOutputOnMute is possible on iOS and Android only");
				}
			}

			// Token: 0x17000011 RID: 17
			// (get) Token: 0x06000031 RID: 49 RVA: 0x00002404 File Offset: 0x00000604
			public static bool audioOutputStarted
			{
				get
				{
					return true;
				}
			}

			// Token: 0x14000004 RID: 4
			// (add) Token: 0x06000032 RID: 50 RVA: 0x00002418 File Offset: 0x00000618
			// (remove) Token: 0x06000033 RID: 51 RVA: 0x0000244C File Offset: 0x0000064C
			[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
			public static event Action<bool> OnMuteStateChanged;

			// Token: 0x06000034 RID: 52 RVA: 0x0000247F File Offset: 0x0000067F
			public static void StartAudioOutput()
			{
				Debug.LogWarning("AudioSettings.Mobile.StartAudioOutput is implemented for iOS and Android only");
			}

			// Token: 0x06000035 RID: 53 RVA: 0x0000248D File Offset: 0x0000068D
			public static void StopAudioOutput()
			{
				Debug.LogWarning("AudioSettings.Mobile.StopAudioOutput is implemented for iOS and Android only");
			}
		}
	}
}
