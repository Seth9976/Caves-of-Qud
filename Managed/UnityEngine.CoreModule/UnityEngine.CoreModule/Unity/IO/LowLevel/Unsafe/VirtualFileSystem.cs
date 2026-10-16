using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace Unity.IO.LowLevel.Unsafe
{
	// Token: 0x02000091 RID: 145
	[StaticAccessor("GetFileSystem()", StaticAccessorType.Dot)]
	[NativeHeader("Runtime/VirtualFileSystem/VirtualFileSystem.h")]
	public static class VirtualFileSystem
	{
		// Token: 0x060002F1 RID: 753 RVA: 0x00005DD8 File Offset: 0x00003FD8
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static bool GetLocalFileSystemName(string vfsFileName, out string localFileName, out ulong localFileOffset, out ulong localFileSize)
		{
			bool localFileSystemName_Injected;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(vfsFileName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = vfsFileName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				localFileSystemName_Injected = VirtualFileSystem.GetLocalFileSystemName_Injected(ref managedSpanWrapper, out managedSpanWrapper2, out localFileOffset, out localFileSize);
			}
			finally
			{
				char* ptr = null;
				ManagedSpanWrapper managedSpanWrapper2;
				localFileName = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper2);
			}
			return localFileSystemName_Injected;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00005E3C File Offset: 0x0000403C
		internal unsafe static string ToLogicalPath(string physicalPath)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(physicalPath, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = physicalPath.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				VirtualFileSystem.ToLogicalPath_Injected(ref managedSpanWrapper, out managedSpanWrapper2);
			}
			finally
			{
				char* ptr = null;
				ManagedSpanWrapper managedSpanWrapper2;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper2);
			}
			return stringAndDispose;
		}

		// Token: 0x060002F3 RID: 755
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetLocalFileSystemName_Injected(ref ManagedSpanWrapper vfsFileName, out ManagedSpanWrapper localFileName, out ulong localFileOffset, out ulong localFileSize);

		// Token: 0x060002F4 RID: 756
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ToLogicalPath_Injected(ref ManagedSpanWrapper physicalPath, out ManagedSpanWrapper ret);
	}
}
