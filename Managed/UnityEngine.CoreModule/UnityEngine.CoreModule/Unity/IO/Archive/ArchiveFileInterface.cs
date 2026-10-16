using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Content;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.IO.Archive
{
	// Token: 0x02000095 RID: 149
	[NativeHeader("Runtime/VirtualFileSystem/ArchiveFileSystem/ArchiveFileHandle.h")]
	[RequiredByNativeCode]
	[StaticAccessor("GetManagedArchiveSystem()", StaticAccessorType.Dot)]
	public static class ArchiveFileInterface
	{
		// Token: 0x060002FD RID: 765 RVA: 0x00005FC4 File Offset: 0x000041C4
		public unsafe static ArchiveHandle MountAsync(ContentNamespace namespaceId, string filePath, string prefix)
		{
			ArchiveHandle archiveHandle2;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(filePath, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = filePath.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(prefix, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = prefix.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				ArchiveHandle archiveHandle;
				ArchiveFileInterface.MountAsync_Injected(ref namespaceId, ref managedSpanWrapper, ref managedSpanWrapper2, out archiveHandle);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
				ArchiveHandle archiveHandle;
				archiveHandle2 = archiveHandle;
			}
			return archiveHandle2;
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00006054 File Offset: 0x00004254
		public static ArchiveHandle[] GetMountedArchives(ContentNamespace namespaceId)
		{
			ArchiveHandle[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				ArchiveFileInterface.GetMountedArchives_Injected(ref namespaceId, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				ArchiveHandle[] array;
				blittableArrayWrapper.Unmarshal<ArchiveHandle>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000608C File Offset: 0x0000428C
		internal static ArchiveStatus Archive_GetStatus(ArchiveHandle handle)
		{
			return ArchiveFileInterface.Archive_GetStatus_Injected(ref handle);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x000060A0 File Offset: 0x000042A0
		internal static JobHandle Archive_GetJobHandle(ArchiveHandle handle)
		{
			JobHandle jobHandle;
			ArchiveFileInterface.Archive_GetJobHandle_Injected(ref handle, out jobHandle);
			return jobHandle;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x000060B8 File Offset: 0x000042B8
		internal static bool Archive_IsValid(ArchiveHandle handle)
		{
			return ArchiveFileInterface.Archive_IsValid_Injected(ref handle);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x000060CC File Offset: 0x000042CC
		internal static JobHandle Archive_UnmountAsync(ArchiveHandle handle)
		{
			JobHandle jobHandle;
			ArchiveFileInterface.Archive_UnmountAsync_Injected(ref handle, out jobHandle);
			return jobHandle;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x000060E4 File Offset: 0x000042E4
		internal static string Archive_GetMountPath(ArchiveHandle handle)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				ArchiveFileInterface.Archive_GetMountPath_Injected(ref handle, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00006118 File Offset: 0x00004318
		internal static CompressionType Archive_GetCompression(ArchiveHandle handle)
		{
			return ArchiveFileInterface.Archive_GetCompression_Injected(ref handle);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000612C File Offset: 0x0000432C
		internal static bool Archive_IsStreamed(ArchiveHandle handle)
		{
			return ArchiveFileInterface.Archive_IsStreamed_Injected(ref handle);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00006140 File Offset: 0x00004340
		internal static ArchiveFileInfo[] Archive_GetFileInfo(ArchiveHandle handle)
		{
			return ArchiveFileInterface.Archive_GetFileInfo_Injected(ref handle);
		}

		// Token: 0x06000307 RID: 775
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MountAsync_Injected([In] ref ContentNamespace namespaceId, ref ManagedSpanWrapper filePath, ref ManagedSpanWrapper prefix, out ArchiveHandle ret);

		// Token: 0x06000308 RID: 776
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetMountedArchives_Injected([In] ref ContentNamespace namespaceId, out BlittableArrayWrapper ret);

		// Token: 0x06000309 RID: 777
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ArchiveStatus Archive_GetStatus_Injected([In] ref ArchiveHandle handle);

		// Token: 0x0600030A RID: 778
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Archive_GetJobHandle_Injected([In] ref ArchiveHandle handle, out JobHandle ret);

		// Token: 0x0600030B RID: 779
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Archive_IsValid_Injected([In] ref ArchiveHandle handle);

		// Token: 0x0600030C RID: 780
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Archive_UnmountAsync_Injected([In] ref ArchiveHandle handle, out JobHandle ret);

		// Token: 0x0600030D RID: 781
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Archive_GetMountPath_Injected([In] ref ArchiveHandle handle, out ManagedSpanWrapper ret);

		// Token: 0x0600030E RID: 782
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern CompressionType Archive_GetCompression_Injected([In] ref ArchiveHandle handle);

		// Token: 0x0600030F RID: 783
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Archive_IsStreamed_Injected([In] ref ArchiveHandle handle);

		// Token: 0x06000310 RID: 784
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ArchiveFileInfo[] Archive_GetFileInfo_Injected([In] ref ArchiveHandle handle);
	}
}
