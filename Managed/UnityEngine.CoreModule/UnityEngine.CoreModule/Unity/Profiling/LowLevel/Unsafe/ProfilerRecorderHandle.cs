using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.Profiling.LowLevel.Unsafe
{
	// Token: 0x02000077 RID: 119
	[UsedByNativeCode]
	[StructLayout(LayoutKind.Explicit, Size = 8)]
	public readonly struct ProfilerRecorderHandle
	{
		// Token: 0x0600020B RID: 523 RVA: 0x00004810 File Offset: 0x00002A10
		internal ProfilerRecorderHandle(ulong handle)
		{
			this.handle = handle;
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600020C RID: 524 RVA: 0x0000481A File Offset: 0x00002A1A
		public bool Valid
		{
			get
			{
				return this.handle != 0UL && this.handle != ulong.MaxValue;
			}
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00004834 File Offset: 0x00002A34
		internal static ProfilerRecorderHandle Get(ProfilerMarker marker)
		{
			return new ProfilerRecorderHandle((ulong)marker.Handle.ToInt64());
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000485C File Offset: 0x00002A5C
		internal static ProfilerRecorderHandle Get(ProfilerCategory category, string statName)
		{
			bool flag = string.IsNullOrEmpty(statName);
			if (flag)
			{
				throw new ArgumentException("String must be not null or empty", "statName");
			}
			return ProfilerRecorderHandle.GetByName(category, statName);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00004890 File Offset: 0x00002A90
		public static ProfilerRecorderDescription GetDescription(ProfilerRecorderHandle handle)
		{
			bool flag = !handle.Valid;
			if (flag)
			{
				throw new ArgumentException("ProfilerRecorderHandle is not initialized or is not available", "handle");
			}
			return ProfilerRecorderHandle.GetDescriptionInternal(handle);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x000048C8 File Offset: 0x00002AC8
		[NativeMethod(IsThreadSafe = true)]
		public unsafe static void GetAvailable([NotNull] List<ProfilerRecorderHandle> outRecorderHandleList)
		{
			if (outRecorderHandleList == null)
			{
				ThrowHelper.ThrowArgumentNullException(outRecorderHandleList, "outRecorderHandleList");
			}
			try
			{
				fixed (ProfilerRecorderHandle[] array = NoAllocHelpers.ExtractArrayFromList<ProfilerRecorderHandle>(outRecorderHandleList))
				{
					BlittableArrayWrapper blittableArrayWrapper;
					if (array.Length != 0)
					{
						blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
					}
					BlittableListWrapper blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, outRecorderHandleList.Count);
					ProfilerRecorderHandle.GetAvailable_Injected(ref blittableListWrapper);
				}
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<ProfilerRecorderHandle>(outRecorderHandleList);
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00004934 File Offset: 0x00002B34
		[NativeMethod(IsThreadSafe = true)]
		internal unsafe static ProfilerRecorderHandle GetByName(ProfilerCategory category, string name)
		{
			ProfilerRecorderHandle profilerRecorderHandle2;
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
				ProfilerRecorderHandle profilerRecorderHandle;
				ProfilerRecorderHandle.GetByName_Injected(ref category, ref managedSpanWrapper, out profilerRecorderHandle);
			}
			finally
			{
				char* ptr = null;
				ProfilerRecorderHandle profilerRecorderHandle;
				profilerRecorderHandle2 = profilerRecorderHandle;
			}
			return profilerRecorderHandle2;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00004994 File Offset: 0x00002B94
		[RequiredMember]
		[NativeMethod(IsThreadSafe = true)]
		internal unsafe static ProfilerRecorderHandle GetByName__Unmanaged(ProfilerCategory category, byte* name, int nameLen)
		{
			ProfilerRecorderHandle profilerRecorderHandle;
			ProfilerRecorderHandle.GetByName__Unmanaged_Injected(ref category, name, nameLen, out profilerRecorderHandle);
			return profilerRecorderHandle;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x000049B0 File Offset: 0x00002BB0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal unsafe static ProfilerRecorderHandle GetByName(ProfilerCategory category, char* name, int nameLen)
		{
			return ProfilerRecorderHandle.GetByName_Unsafe(category, name, nameLen);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x000049CC File Offset: 0x00002BCC
		[NativeMethod(IsThreadSafe = true)]
		private unsafe static ProfilerRecorderHandle GetByName_Unsafe(ProfilerCategory category, char* name, int nameLen)
		{
			ProfilerRecorderHandle profilerRecorderHandle;
			ProfilerRecorderHandle.GetByName_Unsafe_Injected(ref category, name, nameLen, out profilerRecorderHandle);
			return profilerRecorderHandle;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x000049E8 File Offset: 0x00002BE8
		[NativeMethod(IsThreadSafe = true)]
		private static ProfilerRecorderDescription GetDescriptionInternal(ProfilerRecorderHandle handle)
		{
			ProfilerRecorderDescription profilerRecorderDescription;
			ProfilerRecorderHandle.GetDescriptionInternal_Injected(ref handle, out profilerRecorderDescription);
			return profilerRecorderDescription;
		}

		// Token: 0x06000216 RID: 534
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetAvailable_Injected(ref BlittableListWrapper outRecorderHandleList);

		// Token: 0x06000217 RID: 535
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetByName_Injected([In] ref ProfilerCategory category, ref ManagedSpanWrapper name, out ProfilerRecorderHandle ret);

		// Token: 0x06000218 RID: 536
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void GetByName__Unmanaged_Injected([In] ref ProfilerCategory category, byte* name, int nameLen, out ProfilerRecorderHandle ret);

		// Token: 0x06000219 RID: 537
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void GetByName_Unsafe_Injected([In] ref ProfilerCategory category, char* name, int nameLen, out ProfilerRecorderHandle ret);

		// Token: 0x0600021A RID: 538
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetDescriptionInternal_Injected([In] ref ProfilerRecorderHandle handle, out ProfilerRecorderDescription ret);

		// Token: 0x040001BE RID: 446
		private const ulong k_InvalidHandle = 18446744073709551615UL;

		// Token: 0x040001BF RID: 447
		[FieldOffset(0)]
		internal readonly ulong handle;
	}
}
