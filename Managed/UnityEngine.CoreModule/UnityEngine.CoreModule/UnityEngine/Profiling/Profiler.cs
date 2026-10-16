using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Profiling;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Profiling
{
	// Token: 0x020002FE RID: 766
	[UsedByNativeCode]
	[NativeHeader("Runtime/ScriptingBackend/ScriptingApi.h")]
	[NativeHeader("Runtime/Allocator/MemoryManager.h")]
	[NativeHeader("Runtime/Profiler/MemoryProfiler.h")]
	[NativeHeader("Runtime/Profiler/Profiler.h")]
	[NativeHeader("Runtime/Utilities/MemoryUtilities.h")]
	[NativeHeader("Runtime/Profiler/ScriptBindings/Profiler.bindings.h")]
	[MovedFrom("UnityEngine")]
	public sealed class Profiler
	{
		// Token: 0x060025F2 RID: 9714 RVA: 0x00030B93 File Offset: 0x0002ED93
		private Profiler()
		{
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x060025F3 RID: 9715
		public static extern bool supported
		{
			[NativeMethod(Name = "profiler_is_available", IsFreeFunction = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x060025F4 RID: 9716 RVA: 0x0004940C File Offset: 0x0004760C
		// (set) Token: 0x060025F5 RID: 9717 RVA: 0x0004943C File Offset: 0x0004763C
		[StaticAccessor("ProfilerBindings", StaticAccessorType.DoubleColon)]
		public unsafe static string logFile
		{
			get
			{
				string stringAndDispose;
				try
				{
					ManagedSpanWrapper managedSpanWrapper;
					Profiler.get_logFile_Injected(out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
			set
			{
				try
				{
					ManagedSpanWrapper managedSpanWrapper;
					if (!StringMarshaller.TryMarshalEmptyOrNullString(value, ref managedSpanWrapper))
					{
						ReadOnlySpan<char> readOnlySpan = value.AsSpan();
						fixed (char* ptr = readOnlySpan.GetPinnableReference())
						{
							managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
						}
					}
					Profiler.set_logFile_Injected(ref managedSpanWrapper);
				}
				finally
				{
					char* ptr = null;
				}
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x060025F6 RID: 9718
		// (set) Token: 0x060025F7 RID: 9719
		public static extern bool enableBinaryLog
		{
			[NativeMethod(Name = "ProfilerBindings::IsBinaryLogEnabled", IsFreeFunction = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeMethod(Name = "ProfilerBindings::SetBinaryLogEnabled", IsFreeFunction = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x060025F8 RID: 9720
		// (set) Token: 0x060025F9 RID: 9721
		public static extern int maxUsedMemory
		{
			[NativeMethod(Name = "ProfilerBindings::GetMaxUsedMemory", IsFreeFunction = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeMethod(Name = "ProfilerBindings::SetMaxUsedMemory", IsFreeFunction = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x060025FA RID: 9722
		// (set) Token: 0x060025FB RID: 9723
		public static extern bool enabled
		{
			[NativeConditional("ENABLE_PROFILER")]
			[NativeMethod(Name = "profiler_is_enabled", IsFreeFunction = true, IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeMethod(Name = "ProfilerBindings::SetProfilerEnabled", IsFreeFunction = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x060025FC RID: 9724
		// (set) Token: 0x060025FD RID: 9725
		public static extern bool enableAllocationCallstacks
		{
			[NativeMethod(Name = "ProfilerBindings::IsAllocationCallstackCaptureEnabled", IsFreeFunction = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeMethod(Name = "ProfilerBindings::SetAllocationCallstackCaptureEnabled", IsFreeFunction = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x060025FE RID: 9726
		[FreeFunction("ProfilerBindings::profiler_set_area_enabled")]
		[Conditional("ENABLE_PROFILER")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetAreaEnabled(ProfilerArea area, bool enabled);

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x060025FF RID: 9727 RVA: 0x00049490 File Offset: 0x00047690
		public static int areaCount
		{
			get
			{
				return Enum.GetNames(typeof(ProfilerArea)).Length;
			}
		}

		// Token: 0x06002600 RID: 9728
		[NativeConditional("ENABLE_PROFILER")]
		[FreeFunction("ProfilerBindings::profiler_is_area_enabled")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetAreaEnabled(ProfilerArea area);

		// Token: 0x06002601 RID: 9729 RVA: 0x000494B4 File Offset: 0x000476B4
		[Conditional("UNITY_EDITOR")]
		public static void AddFramesFromFile(string file)
		{
			bool flag = string.IsNullOrEmpty(file);
			if (flag)
			{
				Debug.LogError("AddFramesFromFile: Invalid or empty path");
			}
			else
			{
				Profiler.AddFramesFromFile_Internal(file, true);
			}
		}

		// Token: 0x06002602 RID: 9730 RVA: 0x000494E4 File Offset: 0x000476E4
		[NativeMethod(Name = "LoadFromFile")]
		[NativeConditional("ENABLE_PROFILER && UNITY_EDITOR")]
		[StaticAccessor("profiling::GetProfilerSessionPtr()", StaticAccessorType.Arrow)]
		[NativeHeader("Modules/ProfilerEditor/Public/ProfilerSession.h")]
		private unsafe static void AddFramesFromFile_Internal(string file, bool keepExistingFrames)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(file, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = file.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Profiler.AddFramesFromFile_Internal_Injected(ref managedSpanWrapper, keepExistingFrames);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06002603 RID: 9731 RVA: 0x0004953C File Offset: 0x0004773C
		[Conditional("ENABLE_PROFILER")]
		public static void BeginThreadProfiling(string threadGroupName, string threadName)
		{
			bool flag = string.IsNullOrEmpty(threadGroupName);
			if (flag)
			{
				throw new ArgumentException("Argument should be a valid string", "threadGroupName");
			}
			bool flag2 = string.IsNullOrEmpty(threadName);
			if (flag2)
			{
				throw new ArgumentException("Argument should be a valid string", "threadName");
			}
			Profiler.BeginThreadProfilingInternal(threadGroupName, threadName);
		}

		// Token: 0x06002604 RID: 9732 RVA: 0x00049588 File Offset: 0x00047788
		[NativeMethod(Name = "ProfilerBindings::BeginThreadProfiling", IsFreeFunction = true, IsThreadSafe = true)]
		[NativeConditional("ENABLE_PROFILER")]
		private unsafe static void BeginThreadProfilingInternal(string threadGroupName, string threadName)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(threadGroupName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = threadGroupName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(threadName, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = threadName.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				Profiler.BeginThreadProfilingInternal_Injected(ref managedSpanWrapper, ref managedSpanWrapper2);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
			}
		}

		// Token: 0x06002605 RID: 9733 RVA: 0x00002925 File Offset: 0x00000B25
		[NativeConditional("ENABLE_PROFILER")]
		public static void EndThreadProfiling()
		{
		}

		// Token: 0x06002606 RID: 9734 RVA: 0x00049610 File Offset: 0x00047810
		[Conditional("ENABLE_PROFILER")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void BeginSample(string name)
		{
			Profiler.ValidateArguments(name);
			Profiler.BeginSampleImpl(name, null);
		}

		// Token: 0x06002607 RID: 9735 RVA: 0x00049622 File Offset: 0x00047822
		[Conditional("ENABLE_PROFILER")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void BeginSample(string name, Object targetObject)
		{
			Profiler.ValidateArguments(name);
			Profiler.BeginSampleImpl(name, targetObject);
		}

		// Token: 0x06002608 RID: 9736 RVA: 0x00049634 File Offset: 0x00047834
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void ValidateArguments(string name)
		{
			bool flag = string.IsNullOrEmpty(name);
			if (flag)
			{
				throw new ArgumentException("Argument should be a valid string.", "name");
			}
		}

		// Token: 0x06002609 RID: 9737 RVA: 0x00049660 File Offset: 0x00047860
		[NativeMethod(Name = "ProfilerBindings::BeginSample", IsFreeFunction = true, IsThreadSafe = true)]
		private unsafe static void BeginSampleImpl(string name, Object targetObject)
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
				Profiler.BeginSampleImpl_Injected(ref managedSpanWrapper, Object.MarshalledUnityObject.Marshal<Object>(targetObject));
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x0600260A RID: 9738
		[NativeMethod(Name = "ProfilerBindings::EndSample", IsFreeFunction = true, IsThreadSafe = true)]
		[Conditional("ENABLE_PROFILER")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void EndSample();

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x0600260B RID: 9739 RVA: 0x000496BC File Offset: 0x000478BC
		// (set) Token: 0x0600260C RID: 9740 RVA: 0x00002925 File Offset: 0x00000B25
		[Obsolete("maxNumberOfSamplesPerFrame has been depricated. Use maxUsedMemory instead")]
		public static int maxNumberOfSamplesPerFrame
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x0600260D RID: 9741 RVA: 0x000496D0 File Offset: 0x000478D0
		[Obsolete("usedHeapSize has been deprecated since it is limited to 4GB. Please use usedHeapSizeLong instead.")]
		public static uint usedHeapSize
		{
			get
			{
				return (uint)Profiler.usedHeapSizeLong;
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x0600260E RID: 9742
		public static extern long usedHeapSizeLong
		{
			[NativeMethod(Name = "GetUsedHeapSize", IsFreeFunction = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x0600260F RID: 9743 RVA: 0x000496E8 File Offset: 0x000478E8
		[Obsolete("GetRuntimeMemorySize has been deprecated since it is limited to 2GB. Please use GetRuntimeMemorySizeLong() instead.")]
		public static int GetRuntimeMemorySize(Object o)
		{
			return (int)Profiler.GetRuntimeMemorySizeLong(o);
		}

		// Token: 0x06002610 RID: 9744 RVA: 0x00049704 File Offset: 0x00047904
		[NativeMethod(Name = "ProfilerBindings::GetRuntimeMemorySizeLong", IsFreeFunction = true)]
		public static long GetRuntimeMemorySizeLong([NotNull] Object o)
		{
			if (o == null)
			{
				ThrowHelper.ThrowArgumentNullException(o, "o");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Object>(o);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(o, "o");
			}
			return Profiler.GetRuntimeMemorySizeLong_Injected(intPtr);
		}

		// Token: 0x06002611 RID: 9745 RVA: 0x0004973C File Offset: 0x0004793C
		[Obsolete("GetMonoHeapSize has been deprecated since it is limited to 4GB. Please use GetMonoHeapSizeLong() instead.")]
		public static uint GetMonoHeapSize()
		{
			return (uint)Profiler.GetMonoHeapSizeLong();
		}

		// Token: 0x06002612 RID: 9746
		[NativeMethod(Name = "scripting_gc_get_heap_size", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long GetMonoHeapSizeLong();

		// Token: 0x06002613 RID: 9747 RVA: 0x00049754 File Offset: 0x00047954
		[Obsolete("GetMonoUsedSize has been deprecated since it is limited to 4GB. Please use GetMonoUsedSizeLong() instead.")]
		public static uint GetMonoUsedSize()
		{
			return (uint)Profiler.GetMonoUsedSizeLong();
		}

		// Token: 0x06002614 RID: 9748
		[NativeMethod(Name = "scripting_gc_get_used_size", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long GetMonoUsedSizeLong();

		// Token: 0x06002615 RID: 9749
		[StaticAccessor("GetMemoryManager()", StaticAccessorType.Dot)]
		[NativeConditional("ENABLE_MEMORY_MANAGER")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool SetTempAllocatorRequestedSize(uint size);

		// Token: 0x06002616 RID: 9750
		[StaticAccessor("GetMemoryManager()", StaticAccessorType.Dot)]
		[NativeConditional("ENABLE_MEMORY_MANAGER")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint GetTempAllocatorSize();

		// Token: 0x06002617 RID: 9751 RVA: 0x0004976C File Offset: 0x0004796C
		[Obsolete("GetTotalAllocatedMemory has been deprecated since it is limited to 4GB. Please use GetTotalAllocatedMemoryLong() instead.")]
		public static uint GetTotalAllocatedMemory()
		{
			return (uint)Profiler.GetTotalAllocatedMemoryLong();
		}

		// Token: 0x06002618 RID: 9752
		[StaticAccessor("GetMemoryManager()", StaticAccessorType.Dot)]
		[NativeMethod(Name = "GetTotalAllocatedMemory")]
		[NativeConditional("ENABLE_MEMORY_MANAGER")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long GetTotalAllocatedMemoryLong();

		// Token: 0x06002619 RID: 9753 RVA: 0x00049784 File Offset: 0x00047984
		[Obsolete("GetTotalUnusedReservedMemory has been deprecated since it is limited to 4GB. Please use GetTotalUnusedReservedMemoryLong() instead.")]
		public static uint GetTotalUnusedReservedMemory()
		{
			return (uint)Profiler.GetTotalUnusedReservedMemoryLong();
		}

		// Token: 0x0600261A RID: 9754
		[StaticAccessor("GetMemoryManager()", StaticAccessorType.Dot)]
		[NativeMethod(Name = "GetTotalUnusedReservedMemory")]
		[NativeConditional("ENABLE_MEMORY_MANAGER")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long GetTotalUnusedReservedMemoryLong();

		// Token: 0x0600261B RID: 9755 RVA: 0x0004979C File Offset: 0x0004799C
		[Obsolete("GetTotalReservedMemory has been deprecated since it is limited to 4GB. Please use GetTotalReservedMemoryLong() instead.")]
		public static uint GetTotalReservedMemory()
		{
			return (uint)Profiler.GetTotalReservedMemoryLong();
		}

		// Token: 0x0600261C RID: 9756
		[StaticAccessor("GetMemoryManager()", StaticAccessorType.Dot)]
		[NativeMethod(Name = "GetTotalReservedMemory")]
		[NativeConditional("ENABLE_MEMORY_MANAGER")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long GetTotalReservedMemoryLong();

		// Token: 0x0600261D RID: 9757 RVA: 0x000497B4 File Offset: 0x000479B4
		[NativeConditional("ENABLE_MEMORY_MANAGER")]
		public static long GetTotalFragmentationInfo(NativeArray<int> stats)
		{
			return Profiler.InternalGetTotalFragmentationInfo((IntPtr)stats.GetUnsafePtr<int>(), stats.Length);
		}

		// Token: 0x0600261E RID: 9758
		[StaticAccessor("GetMemoryManager()", StaticAccessorType.Dot)]
		[NativeConditional("ENABLE_MEMORY_MANAGER")]
		[NativeMethod(Name = "GetTotalFragmentationInfo")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long InternalGetTotalFragmentationInfo(IntPtr pStats, int count);

		// Token: 0x0600261F RID: 9759
		[StaticAccessor("MemoryProfiler", StaticAccessorType.DoubleColon)]
		[NativeMethod(Name = "GetRegisteredGFXDriverMemory", IsThreadSafe = true)]
		[NativeConditional("ENABLE_PROFILER")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long GetAllocatedMemoryForGraphicsDriver();

		// Token: 0x06002620 RID: 9760 RVA: 0x000497E0 File Offset: 0x000479E0
		[Conditional("ENABLE_PROFILER")]
		public unsafe static void EmitFrameMetaData(Guid id, int tag, Array data)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			Type elementType = data.GetType().GetElementType();
			bool flag2 = !UnsafeUtility.IsBlittable(elementType);
			if (flag2)
			{
				throw new ArgumentException(string.Format("{0} type must be blittable", elementType));
			}
			Profiler.Internal_EmitGlobalMetaData_Array((void*)(&id), 16, tag, data, data.Length, UnsafeUtility.SizeOf(elementType), true);
		}

		// Token: 0x06002621 RID: 9761 RVA: 0x00049848 File Offset: 0x00047A48
		[Conditional("ENABLE_PROFILER")]
		public unsafe static void EmitFrameMetaData<T>(Guid id, int tag, List<T> data) where T : struct
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			Type typeFromHandle = typeof(T);
			bool flag2 = !UnsafeUtility.IsBlittable(typeof(T));
			if (flag2)
			{
				throw new ArgumentException(string.Format("{0} type must be blittable", typeFromHandle));
			}
			Profiler.Internal_EmitGlobalMetaData_Array((void*)(&id), 16, tag, NoAllocHelpers.ExtractArrayFromList<T>(data), data.Count, UnsafeUtility.SizeOf(typeFromHandle), true);
		}

		// Token: 0x06002622 RID: 9762 RVA: 0x000498BA File Offset: 0x00047ABA
		[Conditional("ENABLE_PROFILER")]
		public unsafe static void EmitFrameMetaData<T>(Guid id, int tag, NativeArray<T> data) where T : struct
		{
			Profiler.Internal_EmitGlobalMetaData_Native((void*)(&id), 16, tag, (IntPtr)data.GetUnsafeReadOnlyPtr<T>(), data.Length, UnsafeUtility.SizeOf<T>(), true);
		}

		// Token: 0x06002623 RID: 9763 RVA: 0x000498E4 File Offset: 0x00047AE4
		[Conditional("ENABLE_PROFILER")]
		public unsafe static void EmitSessionMetaData(Guid id, int tag, Array data)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			Type elementType = data.GetType().GetElementType();
			bool flag2 = !UnsafeUtility.IsBlittable(elementType);
			if (flag2)
			{
				throw new ArgumentException(string.Format("{0} type must be blittable", elementType));
			}
			Profiler.Internal_EmitGlobalMetaData_Array((void*)(&id), 16, tag, data, data.Length, UnsafeUtility.SizeOf(elementType), false);
		}

		// Token: 0x06002624 RID: 9764 RVA: 0x0004994C File Offset: 0x00047B4C
		[Conditional("ENABLE_PROFILER")]
		public unsafe static void EmitSessionMetaData<T>(Guid id, int tag, List<T> data) where T : struct
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			Type typeFromHandle = typeof(T);
			bool flag2 = !UnsafeUtility.IsBlittable(typeof(T));
			if (flag2)
			{
				throw new ArgumentException(string.Format("{0} type must be blittable", typeFromHandle));
			}
			Profiler.Internal_EmitGlobalMetaData_Array((void*)(&id), 16, tag, NoAllocHelpers.ExtractArrayFromList<T>(data), data.Count, UnsafeUtility.SizeOf(typeFromHandle), false);
		}

		// Token: 0x06002625 RID: 9765 RVA: 0x000499BE File Offset: 0x00047BBE
		[Conditional("ENABLE_PROFILER")]
		public unsafe static void EmitSessionMetaData<T>(Guid id, int tag, NativeArray<T> data) where T : struct
		{
			Profiler.Internal_EmitGlobalMetaData_Native((void*)(&id), 16, tag, (IntPtr)data.GetUnsafeReadOnlyPtr<T>(), data.Length, UnsafeUtility.SizeOf<T>(), false);
		}

		// Token: 0x06002626 RID: 9766
		[NativeConditional("ENABLE_PROFILER")]
		[NativeMethod(Name = "ProfilerBindings::Internal_EmitGlobalMetaData_Array", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Internal_EmitGlobalMetaData_Array(void* id, int idLen, int tag, Array data, int count, int elementSize, bool frameData);

		// Token: 0x06002627 RID: 9767
		[NativeConditional("ENABLE_PROFILER")]
		[NativeMethod(Name = "ProfilerBindings::Internal_EmitGlobalMetaData_Native", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Internal_EmitGlobalMetaData_Native(void* id, int idLen, int tag, IntPtr data, int count, int elementSize, bool frameData);

		// Token: 0x06002628 RID: 9768 RVA: 0x000499E8 File Offset: 0x00047BE8
		[Conditional("ENABLE_PROFILER")]
		public static void SetCategoryEnabled(ProfilerCategory category, bool enabled)
		{
			bool flag = category == ProfilerCategory.Any;
			if (flag)
			{
				throw new ArgumentException("Argument should be a valid category", "category");
			}
			Profiler.Internal_SetCategoryEnabled(category, enabled);
		}

		// Token: 0x06002629 RID: 9769 RVA: 0x00049A2C File Offset: 0x00047C2C
		public static bool IsCategoryEnabled(ProfilerCategory category)
		{
			bool flag = category == ProfilerCategory.Any;
			if (flag)
			{
				throw new ArgumentException("Argument should be a valid category", "category");
			}
			return Profiler.Internal_IsCategoryEnabled(category);
		}

		// Token: 0x0600262A RID: 9770
		[NativeConditional("ENABLE_PROFILER")]
		[StaticAccessor("profiling::GetProfilerManagerPtr()", StaticAccessorType.Arrow)]
		[NativeMethod(Name = "GetCategoriesCount")]
		[NativeHeader("Runtime/Profiler/ProfilerManager.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint GetCategoriesCount();

		// Token: 0x0600262B RID: 9771 RVA: 0x00049A70 File Offset: 0x00047C70
		[Conditional("ENABLE_PROFILER")]
		public static void GetAllCategories(ProfilerCategory[] categories)
		{
			int num = 0;
			while ((long)num < Math.Min((long)((ulong)Profiler.GetCategoriesCount()), (long)categories.Length))
			{
				categories[num] = new ProfilerCategory((ushort)num);
				num++;
			}
		}

		// Token: 0x0600262C RID: 9772 RVA: 0x00049AAC File Offset: 0x00047CAC
		[Conditional("ENABLE_PROFILER")]
		public static void GetAllCategories(NativeArray<ProfilerCategory> categories)
		{
			int num = 0;
			while ((long)num < Math.Min((long)((ulong)Profiler.GetCategoriesCount()), (long)categories.Length))
			{
				categories[num] = new ProfilerCategory((ushort)num);
				num++;
			}
		}

		// Token: 0x0600262D RID: 9773
		[NativeConditional("ENABLE_PROFILER")]
		[NativeMethod(Name = "profiler_set_category_enable", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetCategoryEnabled(ushort categoryId, bool enabled);

		// Token: 0x0600262E RID: 9774
		[NativeMethod(Name = "profiler_is_category_enabled", IsFreeFunction = true, IsThreadSafe = true)]
		[NativeConditional("ENABLE_PROFILER")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_IsCategoryEnabled(ushort categoryId);

		// Token: 0x0600262F RID: 9775
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_logFile_Injected(out ManagedSpanWrapper ret);

		// Token: 0x06002630 RID: 9776
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_logFile_Injected(ref ManagedSpanWrapper value);

		// Token: 0x06002631 RID: 9777
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddFramesFromFile_Internal_Injected(ref ManagedSpanWrapper file, bool keepExistingFrames);

		// Token: 0x06002632 RID: 9778
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BeginThreadProfilingInternal_Injected(ref ManagedSpanWrapper threadGroupName, ref ManagedSpanWrapper threadName);

		// Token: 0x06002633 RID: 9779
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BeginSampleImpl_Injected(ref ManagedSpanWrapper name, IntPtr targetObject);

		// Token: 0x06002634 RID: 9780
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long GetRuntimeMemorySizeLong_Injected(IntPtr o);

		// Token: 0x04000AB7 RID: 2743
		internal const uint invalidProfilerArea = 4294967295U;
	}
}
