using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.Profiling.LowLevel.Unsafe
{
	// Token: 0x0200007A RID: 122
	[UsedByNativeCode]
	[NativeHeader("Runtime/Profiler/ScriptBindings/ProfilerUnsafeUtility.bindings.h")]
	[IgnoredByDeepProfiler]
	public static class ProfilerUnsafeUtility
	{
		// Token: 0x0600021C RID: 540 RVA: 0x00004A14 File Offset: 0x00002C14
		[ThreadSafe]
		internal unsafe static ushort CreateCategory(string name, ProfilerCategoryColor colorIndex)
		{
			ushort num;
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
				num = ProfilerUnsafeUtility.CreateCategory_Injected(ref managedSpanWrapper, colorIndex);
			}
			finally
			{
				char* ptr = null;
			}
			return num;
		}

		// Token: 0x0600021D RID: 541
		[RequiredMember]
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern ushort CreateCategory__Unmanaged(byte* name, int nameLen, ProfilerCategoryColor colorIndex);

		// Token: 0x0600021E RID: 542 RVA: 0x00004A6C File Offset: 0x00002C6C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static ushort CreateCategory(char* name, int nameLen, ProfilerCategoryColor colorIndex)
		{
			return ProfilerUnsafeUtility.CreateCategory_Unsafe(name, nameLen, colorIndex);
		}

		// Token: 0x0600021F RID: 543
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern ushort CreateCategory_Unsafe(char* name, int nameLen, ProfilerCategoryColor colorIndex);

		// Token: 0x06000220 RID: 544 RVA: 0x00004A88 File Offset: 0x00002C88
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static ushort GetCategoryByName(char* name, int nameLen)
		{
			return ProfilerUnsafeUtility.GetCategoryByName_Unsafe(name, nameLen);
		}

		// Token: 0x06000221 RID: 545
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern ushort GetCategoryByName_Unsafe(char* name, int nameLen);

		// Token: 0x06000222 RID: 546 RVA: 0x00004AA4 File Offset: 0x00002CA4
		[ThreadSafe]
		public static ProfilerCategoryDescription GetCategoryDescription(ushort categoryId)
		{
			ProfilerCategoryDescription profilerCategoryDescription;
			ProfilerUnsafeUtility.GetCategoryDescription_Injected(categoryId, out profilerCategoryDescription);
			return profilerCategoryDescription;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00004ABC File Offset: 0x00002CBC
		[ThreadSafe]
		internal static Color32 GetCategoryColor(ProfilerCategoryColor colorIndex)
		{
			Color32 color;
			ProfilerUnsafeUtility.GetCategoryColor_Injected(colorIndex, out color);
			return color;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00004AD4 File Offset: 0x00002CD4
		[ThreadSafe]
		public unsafe static IntPtr CreateMarker(string name, ushort categoryId, MarkerFlags flags, int metadataCount)
		{
			IntPtr intPtr;
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
				intPtr = ProfilerUnsafeUtility.CreateMarker_Injected(ref managedSpanWrapper, categoryId, flags, metadataCount);
			}
			finally
			{
				char* ptr = null;
			}
			return intPtr;
		}

		// Token: 0x06000225 RID: 549
		[ThreadSafe]
		[RequiredMember]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern IntPtr CreateMarker__Unmanaged(byte* name, int nameLen, ushort categoryId, MarkerFlags flags, int metadataCount);

		// Token: 0x06000226 RID: 550 RVA: 0x00004B30 File Offset: 0x00002D30
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static IntPtr CreateMarker(char* name, int nameLen, ushort categoryId, MarkerFlags flags, int metadataCount)
		{
			return ProfilerUnsafeUtility.CreateMarker_Unsafe(name, nameLen, categoryId, flags, metadataCount);
		}

		// Token: 0x06000227 RID: 551
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern IntPtr CreateMarker_Unsafe(char* name, int nameLen, ushort categoryId, MarkerFlags flags, int metadataCount);

		// Token: 0x06000228 RID: 552 RVA: 0x00004B50 File Offset: 0x00002D50
		[ThreadSafe]
		internal unsafe static IntPtr GetMarker(string name)
		{
			IntPtr marker_Injected;
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
				marker_Injected = ProfilerUnsafeUtility.GetMarker_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return marker_Injected;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00004BA8 File Offset: 0x00002DA8
		[ThreadSafe]
		public unsafe static void SetMarkerMetadata(IntPtr markerPtr, int index, string name, byte type, byte unit)
		{
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
				ProfilerUnsafeUtility.SetMarkerMetadata_Injected(markerPtr, index, ref managedSpanWrapper, type, unit);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x0600022A RID: 554
		[RequiredMember]
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void SetMarkerMetadata__Unmanaged(IntPtr markerPtr, int index, byte* name, int nameLen, byte type, byte unit);

		// Token: 0x0600022B RID: 555 RVA: 0x00004C04 File Offset: 0x00002E04
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void SetMarkerMetadata(IntPtr markerPtr, int index, char* name, int nameLen, byte type, byte unit)
		{
			ProfilerUnsafeUtility.SetMarkerMetadata_Unsafe(markerPtr, index, name, nameLen, type, unit);
		}

		// Token: 0x0600022C RID: 556
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void SetMarkerMetadata_Unsafe(IntPtr markerPtr, int index, char* name, int nameLen, byte type, byte unit);

		// Token: 0x0600022D RID: 557
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void BeginSample(IntPtr markerPtr);

		// Token: 0x0600022E RID: 558
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void BeginSampleWithMetadata(IntPtr markerPtr, int metadataCount, void* metadata);

		// Token: 0x0600022F RID: 559
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void EndSample(IntPtr markerPtr);

		// Token: 0x06000230 RID: 560
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void SingleSampleWithMetadata(IntPtr markerPtr, int metadataCount, void* metadata);

		// Token: 0x06000231 RID: 561 RVA: 0x00004C18 File Offset: 0x00002E18
		[ThreadSafe]
		public unsafe static void* CreateCounterValue(out IntPtr counterPtr, string name, ushort categoryId, MarkerFlags flags, byte dataType, byte dataUnit, int dataSize, ProfilerCounterOptions counterOptions)
		{
			void* ptr2;
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
				ptr2 = ProfilerUnsafeUtility.CreateCounterValue_Injected(out counterPtr, ref managedSpanWrapper, categoryId, flags, dataType, dataUnit, dataSize, counterOptions);
			}
			finally
			{
				char* ptr = null;
			}
			return ptr2;
		}

		// Token: 0x06000232 RID: 562
		[RequiredMember]
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void* CreateCounterValue__Unmanaged(out IntPtr counterPtr, byte* name, int nameLen, ushort categoryId, MarkerFlags flags, byte dataType, byte dataUnit, int dataSize, ProfilerCounterOptions counterOptions);

		// Token: 0x06000233 RID: 563 RVA: 0x00004C7C File Offset: 0x00002E7C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void* CreateCounterValue(out IntPtr counterPtr, char* name, int nameLen, ushort categoryId, MarkerFlags flags, byte dataType, byte dataUnit, int dataSize, ProfilerCounterOptions counterOptions)
		{
			return ProfilerUnsafeUtility.CreateCounterValue_Unsafe(out counterPtr, name, nameLen, categoryId, flags, dataType, dataUnit, dataSize, counterOptions);
		}

		// Token: 0x06000234 RID: 564
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void* CreateCounterValue_Unsafe(out IntPtr counterPtr, char* name, int nameLen, ushort categoryId, MarkerFlags flags, byte dataType, byte dataUnit, int dataSize, ProfilerCounterOptions counterOptions);

		// Token: 0x06000235 RID: 565
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void FlushCounterValue(void* counterValuePtr);

		// Token: 0x06000236 RID: 566 RVA: 0x00004CA4 File Offset: 0x00002EA4
		internal unsafe static string Utf8ToString(byte* chars, int charsLen)
		{
			bool flag = chars == null;
			string text;
			if (flag)
			{
				text = null;
			}
			else
			{
				byte[] array = new byte[charsLen];
				Marshal.Copy((IntPtr)((void*)chars), array, 0, charsLen);
				text = Encoding.UTF8.GetString(array, 0, charsLen);
			}
			return text;
		}

		// Token: 0x06000237 RID: 567
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint CreateFlow(ushort categoryId);

		// Token: 0x06000238 RID: 568
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void FlowEvent(uint flowId, ProfilerFlowEventType flowEventType);

		// Token: 0x06000239 RID: 569 RVA: 0x00004CE8 File Offset: 0x00002EE8
		[ThreadSafe]
		internal static void Internal_BeginWithObject(IntPtr markerPtr, Object contextUnityObject)
		{
			ProfilerUnsafeUtility.Internal_BeginWithObject_Injected(markerPtr, Object.MarshalledUnityObject.Marshal<Object>(contextUnityObject));
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00004D04 File Offset: 0x00002F04
		[NativeConditional("ENABLE_PROFILER")]
		internal static string Internal_GetName(IntPtr markerPtr)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				ProfilerUnsafeUtility.Internal_GetName_Injected(markerPtr, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600023B RID: 571
		public static extern long Timestamp
		{
			[ThreadSafe]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600023C RID: 572 RVA: 0x00004D34 File Offset: 0x00002F34
		public static ProfilerUnsafeUtility.TimestampConversionRatio TimestampToNanosecondsConversionRatio
		{
			[ThreadSafe]
			get
			{
				ProfilerUnsafeUtility.TimestampConversionRatio timestampConversionRatio;
				ProfilerUnsafeUtility.get_TimestampToNanosecondsConversionRatio_Injected(out timestampConversionRatio);
				return timestampConversionRatio;
			}
		}

		// Token: 0x0600023D RID: 573
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ushort CreateCategory_Injected(ref ManagedSpanWrapper name, ProfilerCategoryColor colorIndex);

		// Token: 0x0600023E RID: 574
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetCategoryDescription_Injected(ushort categoryId, out ProfilerCategoryDescription ret);

		// Token: 0x0600023F RID: 575
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetCategoryColor_Injected(ProfilerCategoryColor colorIndex, out Color32 ret);

		// Token: 0x06000240 RID: 576
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr CreateMarker_Injected(ref ManagedSpanWrapper name, ushort categoryId, MarkerFlags flags, int metadataCount);

		// Token: 0x06000241 RID: 577
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetMarker_Injected(ref ManagedSpanWrapper name);

		// Token: 0x06000242 RID: 578
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMarkerMetadata_Injected(IntPtr markerPtr, int index, ref ManagedSpanWrapper name, byte type, byte unit);

		// Token: 0x06000243 RID: 579
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void* CreateCounterValue_Injected(out IntPtr counterPtr, ref ManagedSpanWrapper name, ushort categoryId, MarkerFlags flags, byte dataType, byte dataUnit, int dataSize, ProfilerCounterOptions counterOptions);

		// Token: 0x06000244 RID: 580
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_BeginWithObject_Injected(IntPtr markerPtr, IntPtr contextUnityObject);

		// Token: 0x06000245 RID: 581
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GetName_Injected(IntPtr markerPtr, out ManagedSpanWrapper ret);

		// Token: 0x06000246 RID: 582
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_TimestampToNanosecondsConversionRatio_Injected(out ProfilerUnsafeUtility.TimestampConversionRatio ret);

		// Token: 0x040001CB RID: 459
		public const ushort CategoryRender = 0;

		// Token: 0x040001CC RID: 460
		public const ushort CategoryScripts = 1;

		// Token: 0x040001CD RID: 461
		public const ushort CategoryGUI = 4;

		// Token: 0x040001CE RID: 462
		public const ushort CategoryPhysics = 5;

		// Token: 0x040001CF RID: 463
		public const ushort CategoryAnimation = 6;

		// Token: 0x040001D0 RID: 464
		public const ushort CategoryAi = 7;

		// Token: 0x040001D1 RID: 465
		public const ushort CategoryAudio = 8;

		// Token: 0x040001D2 RID: 466
		public const ushort CategoryVideo = 11;

		// Token: 0x040001D3 RID: 467
		public const ushort CategoryParticles = 12;

		// Token: 0x040001D4 RID: 468
		public const ushort CategoryLighting = 13;

		// Token: 0x040001D5 RID: 469
		[Obsolete("CategoryLightning has been renamed. Use CategoryLighting instead (UnityUpgradable) -> CategoryLighting", false)]
		public const ushort CategoryLightning = 13;

		// Token: 0x040001D6 RID: 470
		public const ushort CategoryNetwork = 14;

		// Token: 0x040001D7 RID: 471
		public const ushort CategoryLoading = 15;

		// Token: 0x040001D8 RID: 472
		public const ushort CategoryOther = 16;

		// Token: 0x040001D9 RID: 473
		public const ushort CategoryVr = 22;

		// Token: 0x040001DA RID: 474
		public const ushort CategoryAllocation = 23;

		// Token: 0x040001DB RID: 475
		public const ushort CategoryInternal = 24;

		// Token: 0x040001DC RID: 476
		public const ushort CategoryFileIO = 25;

		// Token: 0x040001DD RID: 477
		public const ushort CategoryInput = 30;

		// Token: 0x040001DE RID: 478
		public const ushort CategoryVirtualTexturing = 31;

		// Token: 0x040001DF RID: 479
		internal const ushort CategoryGPU = 32;

		// Token: 0x040001E0 RID: 480
		public const ushort CategoryPhysics2D = 33;

		// Token: 0x040001E1 RID: 481
		internal const ushort CategoryAny = 65535;

		// Token: 0x0200007B RID: 123
		public struct TimestampConversionRatio
		{
			// Token: 0x040001E2 RID: 482
			public long Numerator;

			// Token: 0x040001E3 RID: 483
			public long Denominator;
		}
	}
}
