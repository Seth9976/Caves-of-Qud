using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200001F RID: 31
	[StaticAccessor("GetAudioManager()", StaticAccessorType.Dot)]
	public sealed class Microphone
	{
		// Token: 0x06000200 RID: 512 RVA: 0x00004A2C File Offset: 0x00002C2C
		[NativeMethod(IsThreadSafe = true)]
		private unsafe static int GetMicrophoneDeviceIDFromName(string name)
		{
			int microphoneDeviceIDFromName_Injected;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				microphoneDeviceIDFromName_Injected = Microphone.GetMicrophoneDeviceIDFromName_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return microphoneDeviceIDFromName_Injected;
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00004A84 File Offset: 0x00002C84
		private static AudioClip StartRecord(int deviceID, bool loop, float lengthSec, int frequency)
		{
			return Unmarshal.UnmarshalUnityObject<AudioClip>(Microphone.StartRecord_Injected(deviceID, loop, lengthSec, frequency));
		}

		// Token: 0x06000202 RID: 514
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EndRecord(int deviceID);

		// Token: 0x06000203 RID: 515
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsRecording(int deviceID);

		// Token: 0x06000204 RID: 516
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetRecordPosition(int deviceID);

		// Token: 0x06000205 RID: 517
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetDeviceCaps(int deviceID, out int minFreq, out int maxFreq);

		// Token: 0x06000206 RID: 518 RVA: 0x00004AA0 File Offset: 0x00002CA0
		public static AudioClip Start(string deviceName, bool loop, int lengthSec, int frequency)
		{
			int microphoneDeviceIDFromName = Microphone.GetMicrophoneDeviceIDFromName(deviceName);
			bool flag = microphoneDeviceIDFromName == -1;
			if (flag)
			{
				throw new ArgumentException("Couldn't acquire device ID for device name " + deviceName);
			}
			bool flag2 = lengthSec <= 0;
			if (flag2)
			{
				throw new ArgumentException("Length of recording must be greater than zero seconds (was: " + lengthSec.ToString() + " seconds)");
			}
			bool flag3 = lengthSec > 3600;
			if (flag3)
			{
				throw new ArgumentException("Length of recording must be less than one hour (was: " + lengthSec.ToString() + " seconds)");
			}
			bool flag4 = frequency <= 0;
			if (flag4)
			{
				throw new ArgumentException("Frequency of recording must be greater than zero (was: " + frequency.ToString() + " Hz)");
			}
			return Microphone.StartRecord(microphoneDeviceIDFromName, loop, (float)lengthSec, frequency);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00004B58 File Offset: 0x00002D58
		public static void End(string deviceName)
		{
			int microphoneDeviceIDFromName = Microphone.GetMicrophoneDeviceIDFromName(deviceName);
			bool flag = microphoneDeviceIDFromName == -1;
			if (!flag)
			{
				Microphone.EndRecord(microphoneDeviceIDFromName);
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000208 RID: 520
		public static extern string[] devices
		{
			[NativeName("GetRecordDevices")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000209 RID: 521
		internal static extern bool isAnyDeviceRecording
		{
			[NativeName("IsAnyRecordDeviceActive")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00004B80 File Offset: 0x00002D80
		public static bool IsRecording(string deviceName)
		{
			int microphoneDeviceIDFromName = Microphone.GetMicrophoneDeviceIDFromName(deviceName);
			bool flag = microphoneDeviceIDFromName == -1;
			return !flag && Microphone.IsRecording(microphoneDeviceIDFromName);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00004BAC File Offset: 0x00002DAC
		public static int GetPosition(string deviceName)
		{
			int microphoneDeviceIDFromName = Microphone.GetMicrophoneDeviceIDFromName(deviceName);
			bool flag = microphoneDeviceIDFromName == -1;
			int num;
			if (flag)
			{
				num = 0;
			}
			else
			{
				num = Microphone.GetRecordPosition(microphoneDeviceIDFromName);
			}
			return num;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00004BD8 File Offset: 0x00002DD8
		public static void GetDeviceCaps(string deviceName, out int minFreq, out int maxFreq)
		{
			minFreq = 0;
			maxFreq = 0;
			int microphoneDeviceIDFromName = Microphone.GetMicrophoneDeviceIDFromName(deviceName);
			bool flag = microphoneDeviceIDFromName == -1;
			if (!flag)
			{
				Microphone.GetDeviceCaps(microphoneDeviceIDFromName, out minFreq, out maxFreq);
			}
		}

		// Token: 0x0600020E RID: 526
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMicrophoneDeviceIDFromName_Injected(ref ManagedSpanWrapper name);

		// Token: 0x0600020F RID: 527
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr StartRecord_Injected(int deviceID, bool loop, float lengthSec, int frequency);
	}
}
