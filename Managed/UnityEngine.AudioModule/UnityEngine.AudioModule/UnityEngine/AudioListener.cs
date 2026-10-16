using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000016 RID: 22
	[StaticAccessor("AudioListenerBindings", StaticAccessorType.DoubleColon)]
	[RequireComponent(typeof(Transform))]
	public sealed class AudioListener : AudioBehaviour
	{
		// Token: 0x06000072 RID: 114 RVA: 0x00002C18 File Offset: 0x00000E18
		[NativeThrows]
		private unsafe static void GetOutputDataHelper([Out] float[] samples, int channel)
		{
			try
			{
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
				AudioListener.GetOutputDataHelper_Injected(out blittableArrayWrapper, channel);
			}
			finally
			{
				float[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<float>(ref array);
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002C68 File Offset: 0x00000E68
		[NativeThrows]
		private unsafe static void GetSpectrumDataHelper([Out] float[] samples, int channel, FFTWindow window)
		{
			try
			{
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
				AudioListener.GetSpectrumDataHelper_Injected(out blittableArrayWrapper, channel, window);
			}
			finally
			{
				float[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<float>(ref array);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000074 RID: 116
		// (set) Token: 0x06000075 RID: 117
		public static extern float volume
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000076 RID: 118
		// (set) Token: 0x06000077 RID: 119
		[NativeProperty("ListenerPause")]
		public static extern bool pause
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00002CB8 File Offset: 0x00000EB8
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00002CDC File Offset: 0x00000EDC
		public AudioVelocityUpdateMode velocityUpdateMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioListener>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioListener.get_velocityUpdateMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioListener>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioListener.set_velocityUpdateMode_Injected(intPtr, value);
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002D00 File Offset: 0x00000F00
		[Obsolete("GetOutputData returning a float[] is deprecated, use GetOutputData and pass a pre allocated array instead.")]
		public static float[] GetOutputData(int numSamples, int channel)
		{
			float[] array = new float[numSamples];
			AudioListener.GetOutputDataHelper(array, channel);
			return array;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002D22 File Offset: 0x00000F22
		public static void GetOutputData(float[] samples, int channel)
		{
			AudioListener.GetOutputDataHelper(samples, channel);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002D30 File Offset: 0x00000F30
		[Obsolete("GetSpectrumData returning a float[] is deprecated, use GetSpectrumData and pass a pre allocated array instead.")]
		public static float[] GetSpectrumData(int numSamples, int channel, FFTWindow window)
		{
			float[] array = new float[numSamples];
			AudioListener.GetSpectrumDataHelper(array, channel, window);
			return array;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002D53 File Offset: 0x00000F53
		public static void GetSpectrumData(float[] samples, int channel, FFTWindow window)
		{
			AudioListener.GetSpectrumDataHelper(samples, channel, window);
		}

		// Token: 0x0600007F RID: 127
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetOutputDataHelper_Injected(out BlittableArrayWrapper samples, int channel);

		// Token: 0x06000080 RID: 128
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSpectrumDataHelper_Injected(out BlittableArrayWrapper samples, int channel, FFTWindow window);

		// Token: 0x06000081 RID: 129
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AudioVelocityUpdateMode get_velocityUpdateMode_Injected(IntPtr _unity_self);

		// Token: 0x06000082 RID: 130
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_velocityUpdateMode_Injected(IntPtr _unity_self, AudioVelocityUpdateMode value);
	}
}
