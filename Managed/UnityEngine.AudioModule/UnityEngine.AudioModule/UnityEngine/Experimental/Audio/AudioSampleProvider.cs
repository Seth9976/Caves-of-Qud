using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Audio
{
	// Token: 0x02000027 RID: 39
	[StaticAccessor("AudioSampleProviderBindings", StaticAccessorType.DoubleColon)]
	[NativeType(Header = "Modules/Audio/Public/ScriptBindings/AudioSampleProvider.bindings.h")]
	public class AudioSampleProvider : IDisposable
	{
		// Token: 0x06000266 RID: 614 RVA: 0x00005350 File Offset: 0x00003550
		[VisibleToOtherModules]
		internal static AudioSampleProvider Lookup(uint providerId, Object ownerObj, ushort trackIndex)
		{
			AudioSampleProvider audioSampleProvider = AudioSampleProvider.InternalGetScriptingPtr(providerId);
			bool flag = audioSampleProvider != null || !AudioSampleProvider.InternalIsValid(providerId);
			AudioSampleProvider audioSampleProvider2;
			if (flag)
			{
				audioSampleProvider2 = audioSampleProvider;
			}
			else
			{
				audioSampleProvider2 = new AudioSampleProvider(providerId, ownerObj, trackIndex);
			}
			return audioSampleProvider2;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00005388 File Offset: 0x00003588
		internal static AudioSampleProvider Create(ushort channelCount, uint sampleRate)
		{
			uint num = AudioSampleProvider.InternalCreateSampleProvider(channelCount, sampleRate);
			bool flag = !AudioSampleProvider.InternalIsValid(num);
			AudioSampleProvider audioSampleProvider;
			if (flag)
			{
				audioSampleProvider = null;
			}
			else
			{
				audioSampleProvider = new AudioSampleProvider(num, null, 0);
			}
			return audioSampleProvider;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x000053BC File Offset: 0x000035BC
		private AudioSampleProvider(uint providerId, Object ownerObj, ushort trackIdx)
		{
			this.owner = ownerObj;
			this.id = providerId;
			this.trackIndex = trackIdx;
			this.m_ConsumeSampleFramesNativeFunction = (AudioSampleProvider.ConsumeSampleFramesNativeFunction)Marshal.GetDelegateForFunctionPointer(AudioSampleProvider.InternalGetConsumeSampleFramesNativeFunctionPtr(), typeof(AudioSampleProvider.ConsumeSampleFramesNativeFunction));
			ushort num = 0;
			uint num2 = 0U;
			AudioSampleProvider.InternalGetFormatInfo(providerId, out num, out num2);
			this.channelCount = num;
			this.sampleRate = num2;
			AudioSampleProvider.InternalSetScriptingPtr(providerId, this);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00005430 File Offset: 0x00003630
		~AudioSampleProvider()
		{
			this.owner = null;
			this.Dispose();
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00005468 File Offset: 0x00003668
		public void Dispose()
		{
			bool flag = this.id > 0U;
			if (flag)
			{
				AudioSampleProvider.InternalSetScriptingPtr(this.id, null);
				bool flag2 = this.owner == null;
				if (flag2)
				{
					AudioSampleProvider.InternalRemove(this.id);
				}
				this.id = 0U;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600026B RID: 619 RVA: 0x000054BD File Offset: 0x000036BD
		// (set) Token: 0x0600026C RID: 620 RVA: 0x000054C5 File Offset: 0x000036C5
		public uint id { get; private set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600026D RID: 621 RVA: 0x000054CE File Offset: 0x000036CE
		// (set) Token: 0x0600026E RID: 622 RVA: 0x000054D6 File Offset: 0x000036D6
		public ushort trackIndex { get; private set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600026F RID: 623 RVA: 0x000054DF File Offset: 0x000036DF
		// (set) Token: 0x06000270 RID: 624 RVA: 0x000054E7 File Offset: 0x000036E7
		public Object owner { get; private set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000271 RID: 625 RVA: 0x000054F0 File Offset: 0x000036F0
		public bool valid
		{
			get
			{
				return AudioSampleProvider.InternalIsValid(this.id);
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000550D File Offset: 0x0000370D
		// (set) Token: 0x06000273 RID: 627 RVA: 0x00005515 File Offset: 0x00003715
		public ushort channelCount { get; private set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000551E File Offset: 0x0000371E
		// (set) Token: 0x06000275 RID: 629 RVA: 0x00005526 File Offset: 0x00003726
		public uint sampleRate { get; private set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000276 RID: 630 RVA: 0x00005530 File Offset: 0x00003730
		public uint maxSampleFrameCount
		{
			get
			{
				return AudioSampleProvider.InternalGetMaxSampleFrameCount(this.id);
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000277 RID: 631 RVA: 0x00005550 File Offset: 0x00003750
		public uint availableSampleFrameCount
		{
			get
			{
				return AudioSampleProvider.InternalGetAvailableSampleFrameCount(this.id);
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000278 RID: 632 RVA: 0x00005570 File Offset: 0x00003770
		public uint freeSampleFrameCount
		{
			get
			{
				return AudioSampleProvider.InternalGetFreeSampleFrameCount(this.id);
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000279 RID: 633 RVA: 0x00005590 File Offset: 0x00003790
		// (set) Token: 0x0600027A RID: 634 RVA: 0x000055AD File Offset: 0x000037AD
		public uint freeSampleFrameCountLowThreshold
		{
			get
			{
				return AudioSampleProvider.InternalGetFreeSampleFrameCountLowThreshold(this.id);
			}
			set
			{
				AudioSampleProvider.InternalSetFreeSampleFrameCountLowThreshold(this.id, value);
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600027B RID: 635 RVA: 0x000055C0 File Offset: 0x000037C0
		// (set) Token: 0x0600027C RID: 636 RVA: 0x000055DD File Offset: 0x000037DD
		public bool enableSampleFramesAvailableEvents
		{
			get
			{
				return AudioSampleProvider.InternalGetEnableSampleFramesAvailableEvents(this.id);
			}
			set
			{
				AudioSampleProvider.InternalSetEnableSampleFramesAvailableEvents(this.id, value);
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600027D RID: 637 RVA: 0x000055F0 File Offset: 0x000037F0
		// (set) Token: 0x0600027E RID: 638 RVA: 0x0000560D File Offset: 0x0000380D
		public bool enableSilencePadding
		{
			get
			{
				return AudioSampleProvider.InternalGetEnableSilencePadding(this.id);
			}
			set
			{
				AudioSampleProvider.InternalSetEnableSilencePadding(this.id, value);
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00005620 File Offset: 0x00003820
		public uint ConsumeSampleFrames(NativeArray<float> sampleFrames)
		{
			bool flag = this.channelCount == 0;
			uint num;
			if (flag)
			{
				num = 0U;
			}
			else
			{
				num = this.m_ConsumeSampleFramesNativeFunction(this.id, (IntPtr)sampleFrames.GetUnsafePtr<float>(), (uint)(sampleFrames.Length / (int)this.channelCount));
			}
			return num;
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000280 RID: 640 RVA: 0x00005670 File Offset: 0x00003870
		public static AudioSampleProvider.ConsumeSampleFramesNativeFunction consumeSampleFramesNativeFunction
		{
			get
			{
				return (AudioSampleProvider.ConsumeSampleFramesNativeFunction)Marshal.GetDelegateForFunctionPointer(AudioSampleProvider.InternalGetConsumeSampleFramesNativeFunctionPtr(), typeof(AudioSampleProvider.ConsumeSampleFramesNativeFunction));
			}
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000569C File Offset: 0x0000389C
		internal uint QueueSampleFrames(NativeArray<float> sampleFrames)
		{
			bool flag = this.channelCount == 0;
			uint num;
			if (flag)
			{
				num = 0U;
			}
			else
			{
				num = AudioSampleProvider.InternalQueueSampleFrames(this.id, (IntPtr)sampleFrames.GetUnsafeReadOnlyPtr<float>(), (uint)(sampleFrames.Length / (int)this.channelCount));
			}
			return num;
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000282 RID: 642 RVA: 0x000056E4 File Offset: 0x000038E4
		// (remove) Token: 0x06000283 RID: 643 RVA: 0x0000571C File Offset: 0x0000391C
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event AudioSampleProvider.SampleFramesHandler sampleFramesAvailable;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000284 RID: 644 RVA: 0x00005754 File Offset: 0x00003954
		// (remove) Token: 0x06000285 RID: 645 RVA: 0x0000578C File Offset: 0x0000398C
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event AudioSampleProvider.SampleFramesHandler sampleFramesOverflow;

		// Token: 0x06000286 RID: 646 RVA: 0x000057C1 File Offset: 0x000039C1
		public void SetSampleFramesAvailableNativeHandler(AudioSampleProvider.SampleFramesEventNativeFunction handler, IntPtr userData)
		{
			AudioSampleProvider.InternalSetSampleFramesAvailableNativeHandler(this.id, Marshal.GetFunctionPointerForDelegate<AudioSampleProvider.SampleFramesEventNativeFunction>(handler), userData);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x000057D7 File Offset: 0x000039D7
		public void ClearSampleFramesAvailableNativeHandler()
		{
			AudioSampleProvider.InternalClearSampleFramesAvailableNativeHandler(this.id);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x000057E6 File Offset: 0x000039E6
		public void SetSampleFramesOverflowNativeHandler(AudioSampleProvider.SampleFramesEventNativeFunction handler, IntPtr userData)
		{
			AudioSampleProvider.InternalSetSampleFramesOverflowNativeHandler(this.id, Marshal.GetFunctionPointerForDelegate<AudioSampleProvider.SampleFramesEventNativeFunction>(handler), userData);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x000057FC File Offset: 0x000039FC
		public void ClearSampleFramesOverflowNativeHandler()
		{
			AudioSampleProvider.InternalClearSampleFramesOverflowNativeHandler(this.id);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000580C File Offset: 0x00003A0C
		[RequiredByNativeCode]
		private void InvokeSampleFramesAvailable(int sampleFrameCount)
		{
			bool flag = this.sampleFramesAvailable != null;
			if (flag)
			{
				this.sampleFramesAvailable(this, (uint)sampleFrameCount);
			}
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00005838 File Offset: 0x00003A38
		[RequiredByNativeCode]
		private void InvokeSampleFramesOverflow(int droppedSampleFrameCount)
		{
			bool flag = this.sampleFramesOverflow != null;
			if (flag)
			{
				this.sampleFramesOverflow(this, (uint)droppedSampleFrameCount);
			}
		}

		// Token: 0x0600028C RID: 652
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint InternalCreateSampleProvider(ushort channelCount, uint sampleRate);

		// Token: 0x0600028D RID: 653
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalRemove(uint providerId);

		// Token: 0x0600028E RID: 654
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalGetFormatInfo(uint providerId, out ushort chCount, out uint sRate);

		// Token: 0x0600028F RID: 655
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AudioSampleProvider InternalGetScriptingPtr(uint providerId);

		// Token: 0x06000290 RID: 656
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetScriptingPtr(uint providerId, AudioSampleProvider provider);

		// Token: 0x06000291 RID: 657
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool InternalIsValid(uint providerId);

		// Token: 0x06000292 RID: 658
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint InternalGetMaxSampleFrameCount(uint providerId);

		// Token: 0x06000293 RID: 659
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint InternalGetAvailableSampleFrameCount(uint providerId);

		// Token: 0x06000294 RID: 660
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint InternalGetFreeSampleFrameCount(uint providerId);

		// Token: 0x06000295 RID: 661
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint InternalGetFreeSampleFrameCountLowThreshold(uint providerId);

		// Token: 0x06000296 RID: 662
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetFreeSampleFrameCountLowThreshold(uint providerId, uint sampleFrameCount);

		// Token: 0x06000297 RID: 663
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool InternalGetEnableSampleFramesAvailableEvents(uint providerId);

		// Token: 0x06000298 RID: 664
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetEnableSampleFramesAvailableEvents(uint providerId, bool enable);

		// Token: 0x06000299 RID: 665
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetSampleFramesAvailableNativeHandler(uint providerId, IntPtr handler, IntPtr userData);

		// Token: 0x0600029A RID: 666
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalClearSampleFramesAvailableNativeHandler(uint providerId);

		// Token: 0x0600029B RID: 667
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetSampleFramesOverflowNativeHandler(uint providerId, IntPtr handler, IntPtr userData);

		// Token: 0x0600029C RID: 668
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalClearSampleFramesOverflowNativeHandler(uint providerId);

		// Token: 0x0600029D RID: 669
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool InternalGetEnableSilencePadding(uint id);

		// Token: 0x0600029E RID: 670
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetEnableSilencePadding(uint id, bool enabled);

		// Token: 0x0600029F RID: 671
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr InternalGetConsumeSampleFramesNativeFunctionPtr();

		// Token: 0x060002A0 RID: 672
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint InternalQueueSampleFrames(uint id, IntPtr interleavedSampleFrames, uint sampleFrameCount);

		// Token: 0x04000073 RID: 115
		private AudioSampleProvider.ConsumeSampleFramesNativeFunction m_ConsumeSampleFramesNativeFunction;

		// Token: 0x02000028 RID: 40
		// (Invoke) Token: 0x060002A2 RID: 674
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate uint ConsumeSampleFramesNativeFunction(uint providerId, IntPtr interleavedSampleFrames, uint sampleFrameCount);

		// Token: 0x02000029 RID: 41
		// (Invoke) Token: 0x060002A6 RID: 678
		public delegate void SampleFramesHandler(AudioSampleProvider provider, uint sampleFrameCount);

		// Token: 0x0200002A RID: 42
		// (Invoke) Token: 0x060002AA RID: 682
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void SampleFramesEventNativeFunction(IntPtr userData, uint providerId, uint sampleFrameCount);
	}
}
