using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Burst;
using Unity.Profiling.LowLevel;
using Unity.Profiling.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.Profiling
{
	// Token: 0x0200006F RID: 111
	[DebuggerDisplay("Count = {Count}")]
	[NativeHeader("Runtime/Profiler/ScriptBindings/ProfilerRecorder.bindings.h")]
	[DebuggerTypeProxy(typeof(ProfilerRecorderDebugView))]
	[UsedByNativeCode]
	public struct ProfilerRecorder : IDisposable
	{
		// Token: 0x060001BC RID: 444 RVA: 0x00004150 File Offset: 0x00002350
		public ProfilerRecorder(string statName, int capacity = 1, ProfilerRecorderOptions options = ProfilerRecorderOptions.Default)
		{
			this = new ProfilerRecorder(ProfilerCategory.Any, statName, capacity, options);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00004162 File Offset: 0x00002362
		public ProfilerRecorder(string categoryName, string statName, int capacity = 1, ProfilerRecorderOptions options = ProfilerRecorderOptions.Default)
		{
			this = new ProfilerRecorder(new ProfilerCategory(categoryName), statName, capacity, options);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00004178 File Offset: 0x00002378
		public ProfilerRecorder(ProfilerCategory category, string statName, int capacity = 1, ProfilerRecorderOptions options = ProfilerRecorderOptions.Default)
		{
			ProfilerRecorderHandle byName = ProfilerRecorderHandle.GetByName(category, statName);
			this = ProfilerRecorder.Create(byName, capacity, options);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000041A0 File Offset: 0x000023A0
		public unsafe ProfilerRecorder(ProfilerCategory category, char* statName, int statNameLen, int capacity = 1, ProfilerRecorderOptions options = ProfilerRecorderOptions.Default)
		{
			ProfilerRecorderHandle byName = ProfilerRecorderHandle.GetByName(category, statName, statNameLen);
			this = ProfilerRecorder.Create(byName, capacity, options);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x000041C7 File Offset: 0x000023C7
		public ProfilerRecorder(ProfilerMarker marker, int capacity = 1, ProfilerRecorderOptions options = ProfilerRecorderOptions.Default)
		{
			this = ProfilerRecorder.Create(ProfilerRecorderHandle.Get(marker), capacity, options);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000041DD File Offset: 0x000023DD
		public ProfilerRecorder(ProfilerRecorderHandle statHandle, int capacity = 1, ProfilerRecorderOptions options = ProfilerRecorderOptions.Default)
		{
			this = ProfilerRecorder.Create(statHandle, capacity, options);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x000041F0 File Offset: 0x000023F0
		public unsafe static ProfilerRecorder StartNew(ProfilerCategory category, string statName, int capacity = 1, ProfilerRecorderOptions options = ProfilerRecorderOptions.Default)
		{
			char* ptr = statName;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return new ProfilerRecorder(category, ptr, statName.Length, capacity, options | ProfilerRecorderOptions.StartImmediately);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00004224 File Offset: 0x00002424
		public static ProfilerRecorder StartNew(ProfilerMarker marker, int capacity = 1, ProfilerRecorderOptions options = ProfilerRecorderOptions.Default)
		{
			return new ProfilerRecorder(marker, capacity, options | ProfilerRecorderOptions.StartImmediately);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00004240 File Offset: 0x00002440
		internal static ProfilerRecorder StartNew()
		{
			return ProfilerRecorder.Create(default(ProfilerRecorderHandle), 0, ProfilerRecorderOptions.StartImmediately);
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00004262 File Offset: 0x00002462
		public bool Valid
		{
			get
			{
				return this.handle != 0UL && ProfilerRecorder.GetValid(this);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x0000427C File Offset: 0x0000247C
		public ProfilerMarkerDataType DataType
		{
			get
			{
				this.CheckInitializedAndThrow();
				return ProfilerRecorder.GetValueDataType(this);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x000042A0 File Offset: 0x000024A0
		public ProfilerMarkerDataUnit UnitType
		{
			get
			{
				this.CheckInitializedAndThrow();
				return ProfilerRecorder.GetValueUnitType(this);
			}
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x000042C4 File Offset: 0x000024C4
		public void Start()
		{
			this.CheckInitializedAndThrow();
			ProfilerRecorder.Control(this, ProfilerRecorder.ControlOptions.Start);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x000042DB File Offset: 0x000024DB
		public void Stop()
		{
			this.CheckInitializedAndThrow();
			ProfilerRecorder.Control(this, ProfilerRecorder.ControlOptions.Stop);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x000042F2 File Offset: 0x000024F2
		public void Reset()
		{
			this.CheckInitializedAndThrow();
			ProfilerRecorder.Control(this, ProfilerRecorder.ControlOptions.Reset);
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060001CB RID: 459 RVA: 0x0000430C File Offset: 0x0000250C
		public long CurrentValue
		{
			get
			{
				this.CheckInitializedAndThrow();
				return ProfilerRecorder.GetCurrentValue(this);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00004330 File Offset: 0x00002530
		public double CurrentValueAsDouble
		{
			get
			{
				this.CheckInitializedAndThrow();
				return ProfilerRecorder.GetCurrentValueAsDouble(this);
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00004354 File Offset: 0x00002554
		public long LastValue
		{
			get
			{
				this.CheckInitializedAndThrow();
				return ProfilerRecorder.GetLastValue(this);
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00004378 File Offset: 0x00002578
		public double LastValueAsDouble
		{
			get
			{
				this.CheckInitializedAndThrow();
				return ProfilerRecorder.GetLastValueAsDouble(this);
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060001CF RID: 463 RVA: 0x0000439C File Offset: 0x0000259C
		public int Capacity
		{
			get
			{
				this.CheckInitializedAndThrow();
				return ProfilerRecorder.GetCount(this, ProfilerRecorder.CountOptions.MaxCount);
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x000043C4 File Offset: 0x000025C4
		public int Count
		{
			get
			{
				this.CheckInitializedAndThrow();
				return ProfilerRecorder.GetCount(this, ProfilerRecorder.CountOptions.Count);
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x000043EC File Offset: 0x000025EC
		public bool IsRunning
		{
			get
			{
				this.CheckInitializedAndThrow();
				return ProfilerRecorder.GetRunning(this);
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00004410 File Offset: 0x00002610
		public bool WrappedAround
		{
			get
			{
				this.CheckInitializedAndThrow();
				return ProfilerRecorder.GetWrapped(this);
			}
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00004434 File Offset: 0x00002634
		public ProfilerRecorderSample GetSample(int index)
		{
			this.CheckInitializedAndThrow();
			return ProfilerRecorder.GetSampleInternal(this, index);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000445C File Offset: 0x0000265C
		public void CopyTo(List<ProfilerRecorderSample> outSamples, bool reset = false)
		{
			bool flag = outSamples == null;
			if (flag)
			{
				throw new ArgumentNullException("outSamples");
			}
			this.CheckInitializedAndThrow();
			ProfilerRecorder.CopyTo_List(this, outSamples, reset);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00004494 File Offset: 0x00002694
		public unsafe int CopyTo(ProfilerRecorderSample* dest, int destSize, bool reset = false)
		{
			this.CheckInitializedWithParamsAndThrow(dest);
			return ProfilerRecorder.CopyTo_Pointer(this, dest, destSize, reset);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x000044BC File Offset: 0x000026BC
		public unsafe ProfilerRecorderSample[] ToArray()
		{
			this.CheckInitializedAndThrow();
			int count = this.Count;
			ProfilerRecorderSample[] array = new ProfilerRecorderSample[count];
			ProfilerRecorderSample[] array2;
			ProfilerRecorderSample* ptr;
			if ((array2 = array) == null || array2.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array2[0];
			}
			ProfilerRecorder.CopyTo_Pointer(this, ptr, count, false);
			array2 = null;
			return array;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00004511 File Offset: 0x00002711
		internal void FilterToCurrentThread()
		{
			this.CheckInitializedAndThrow();
			ProfilerRecorder.Control(this, ProfilerRecorder.ControlOptions.SetFilterToCurrentThread);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00004528 File Offset: 0x00002728
		internal void CollectFromAllThreads()
		{
			this.CheckInitializedAndThrow();
			ProfilerRecorder.Control(this, ProfilerRecorder.ControlOptions.SetToCollectFromAllThreads);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00004540 File Offset: 0x00002740
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		private static ProfilerRecorder Create(ProfilerRecorderHandle statHandle, int maxSampleCount, ProfilerRecorderOptions options)
		{
			ProfilerRecorder profilerRecorder;
			ProfilerRecorder.Create_Injected(ref statHandle, maxSampleCount, options, out profilerRecorder);
			return profilerRecorder;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000455C File Offset: 0x0000275C
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		private static void Control(ProfilerRecorder handle, ProfilerRecorder.ControlOptions options)
		{
			ProfilerRecorder.Control_Injected(ref handle, options);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00004574 File Offset: 0x00002774
		[NativeMethod(IsThreadSafe = true)]
		private static ProfilerMarkerDataUnit GetValueUnitType(ProfilerRecorder handle)
		{
			return ProfilerRecorder.GetValueUnitType_Injected(ref handle);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00004588 File Offset: 0x00002788
		[NativeMethod(IsThreadSafe = true)]
		private static ProfilerMarkerDataType GetValueDataType(ProfilerRecorder handle)
		{
			return ProfilerRecorder.GetValueDataType_Injected(ref handle);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000459C File Offset: 0x0000279C
		[NativeMethod(IsThreadSafe = true)]
		private static long GetCurrentValue(ProfilerRecorder handle)
		{
			return ProfilerRecorder.GetCurrentValue_Injected(ref handle);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000045B0 File Offset: 0x000027B0
		[NativeMethod(IsThreadSafe = true)]
		private static double GetCurrentValueAsDouble(ProfilerRecorder handle)
		{
			return ProfilerRecorder.GetCurrentValueAsDouble_Injected(ref handle);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x000045C4 File Offset: 0x000027C4
		[NativeMethod(IsThreadSafe = true)]
		private static long GetLastValue(ProfilerRecorder handle)
		{
			return ProfilerRecorder.GetLastValue_Injected(ref handle);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x000045D8 File Offset: 0x000027D8
		[NativeMethod(IsThreadSafe = true)]
		private static double GetLastValueAsDouble(ProfilerRecorder handle)
		{
			return ProfilerRecorder.GetLastValueAsDouble_Injected(ref handle);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x000045EC File Offset: 0x000027EC
		[NativeMethod(IsThreadSafe = true)]
		private static int GetCount(ProfilerRecorder handle, ProfilerRecorder.CountOptions countOptions)
		{
			return ProfilerRecorder.GetCount_Injected(ref handle, countOptions);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00004604 File Offset: 0x00002804
		[NativeMethod(IsThreadSafe = true)]
		private static bool GetValid(ProfilerRecorder handle)
		{
			return ProfilerRecorder.GetValid_Injected(ref handle);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00004618 File Offset: 0x00002818
		[NativeMethod(IsThreadSafe = true)]
		private static bool GetWrapped(ProfilerRecorder handle)
		{
			return ProfilerRecorder.GetWrapped_Injected(ref handle);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000462C File Offset: 0x0000282C
		[NativeMethod(IsThreadSafe = true)]
		private static bool GetRunning(ProfilerRecorder handle)
		{
			return ProfilerRecorder.GetRunning_Injected(ref handle);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00004640 File Offset: 0x00002840
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		private static ProfilerRecorderSample GetSampleInternal(ProfilerRecorder handle, int index)
		{
			ProfilerRecorderSample profilerRecorderSample;
			ProfilerRecorder.GetSampleInternal_Injected(ref handle, index, out profilerRecorderSample);
			return profilerRecorderSample;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00004658 File Offset: 0x00002858
		[NativeMethod(IsThreadSafe = true)]
		private unsafe static void CopyTo_List(ProfilerRecorder handle, List<ProfilerRecorderSample> outSamples, bool reset)
		{
			try
			{
				BlittableListWrapper blittableListWrapper;
				if (outSamples != null)
				{
					fixed (ProfilerRecorderSample[] array = NoAllocHelpers.ExtractArrayFromList<ProfilerRecorderSample>(outSamples))
					{
						BlittableArrayWrapper blittableArrayWrapper;
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
						blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, outSamples.Count);
					}
				}
				ProfilerRecorder.CopyTo_List_Injected(ref handle, ref blittableListWrapper, reset);
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<ProfilerRecorderSample>(outSamples);
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x000046BC File Offset: 0x000028BC
		[NativeMethod(IsThreadSafe = true)]
		private unsafe static int CopyTo_Pointer(ProfilerRecorder handle, ProfilerRecorderSample* outSamples, int outSamplesSize, bool reset)
		{
			return ProfilerRecorder.CopyTo_Pointer_Injected(ref handle, outSamples, outSamplesSize, reset);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x000046D4 File Offset: 0x000028D4
		public void Dispose()
		{
			bool flag = this.handle == 0UL;
			if (!flag)
			{
				ProfilerRecorder.Control(this, ProfilerRecorder.ControlOptions.Release);
				this.handle = 0UL;
			}
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00004708 File Offset: 0x00002908
		[BurstDiscard]
		private unsafe void CheckInitializedWithParamsAndThrow(ProfilerRecorderSample* dest)
		{
			bool flag = this.handle == 0UL;
			if (flag)
			{
				throw new InvalidOperationException("ProfilerRecorder object is not initialized or has been disposed.");
			}
			bool flag2 = dest == null;
			if (flag2)
			{
				throw new ArgumentNullException("dest");
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00004744 File Offset: 0x00002944
		[BurstDiscard]
		private void CheckInitializedAndThrow()
		{
			bool flag = this.handle == 0UL;
			if (flag)
			{
				throw new InvalidOperationException("ProfilerRecorder object is not initialized or has been disposed.");
			}
		}

		// Token: 0x060001EB RID: 491
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Create_Injected([In] ref ProfilerRecorderHandle statHandle, int maxSampleCount, ProfilerRecorderOptions options, out ProfilerRecorder ret);

		// Token: 0x060001EC RID: 492
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Control_Injected([In] ref ProfilerRecorder handle, ProfilerRecorder.ControlOptions options);

		// Token: 0x060001ED RID: 493
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ProfilerMarkerDataUnit GetValueUnitType_Injected([In] ref ProfilerRecorder handle);

		// Token: 0x060001EE RID: 494
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ProfilerMarkerDataType GetValueDataType_Injected([In] ref ProfilerRecorder handle);

		// Token: 0x060001EF RID: 495
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long GetCurrentValue_Injected([In] ref ProfilerRecorder handle);

		// Token: 0x060001F0 RID: 496
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern double GetCurrentValueAsDouble_Injected([In] ref ProfilerRecorder handle);

		// Token: 0x060001F1 RID: 497
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long GetLastValue_Injected([In] ref ProfilerRecorder handle);

		// Token: 0x060001F2 RID: 498
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern double GetLastValueAsDouble_Injected([In] ref ProfilerRecorder handle);

		// Token: 0x060001F3 RID: 499
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetCount_Injected([In] ref ProfilerRecorder handle, ProfilerRecorder.CountOptions countOptions);

		// Token: 0x060001F4 RID: 500
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetValid_Injected([In] ref ProfilerRecorder handle);

		// Token: 0x060001F5 RID: 501
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetWrapped_Injected([In] ref ProfilerRecorder handle);

		// Token: 0x060001F6 RID: 502
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetRunning_Injected([In] ref ProfilerRecorder handle);

		// Token: 0x060001F7 RID: 503
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSampleInternal_Injected([In] ref ProfilerRecorder handle, int index, out ProfilerRecorderSample ret);

		// Token: 0x060001F8 RID: 504
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyTo_List_Injected([In] ref ProfilerRecorder handle, ref BlittableListWrapper outSamples, bool reset);

		// Token: 0x060001F9 RID: 505
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern int CopyTo_Pointer_Injected([In] ref ProfilerRecorder handle, ProfilerRecorderSample* outSamples, int outSamplesSize, bool reset);

		// Token: 0x04000191 RID: 401
		internal ulong handle;

		// Token: 0x04000192 RID: 402
		internal const ProfilerRecorderOptions SharedRecorder = (ProfilerRecorderOptions)128;

		// Token: 0x02000070 RID: 112
		internal enum ControlOptions
		{
			// Token: 0x04000194 RID: 404
			Start,
			// Token: 0x04000195 RID: 405
			Stop,
			// Token: 0x04000196 RID: 406
			Reset,
			// Token: 0x04000197 RID: 407
			Release = 4,
			// Token: 0x04000198 RID: 408
			SetFilterToCurrentThread,
			// Token: 0x04000199 RID: 409
			SetToCollectFromAllThreads
		}

		// Token: 0x02000071 RID: 113
		internal enum CountOptions
		{
			// Token: 0x0400019B RID: 411
			Count,
			// Token: 0x0400019C RID: 412
			MaxCount
		}
	}
}
