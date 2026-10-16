using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Bindings;

namespace Unity.IO.LowLevel.Unsafe
{
	// Token: 0x02000089 RID: 137
	[NativeHeader("Runtime/File/AsyncReadManagerManagedApi.h")]
	public static class AsyncReadManager
	{
		// Token: 0x06000280 RID: 640 RVA: 0x0000560C File Offset: 0x0000380C
		[FreeFunction("AsyncReadManagerManaged::Read", IsThreadSafe = true)]
		[ThreadAndSerializationSafe]
		private unsafe static ReadHandle ReadInternal(string filename, void* cmds, uint cmdCount, string assetName, ulong typeID, AssetLoadingSubsystem subsystem)
		{
			ReadHandle readHandle2;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(filename, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = filename.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(assetName, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = assetName.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				ReadHandle readHandle;
				AsyncReadManager.ReadInternal_Injected(ref managedSpanWrapper, cmds, cmdCount, ref managedSpanWrapper2, typeID, subsystem, out readHandle);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
				ReadHandle readHandle;
				readHandle2 = readHandle;
			}
			return readHandle2;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x000056A0 File Offset: 0x000038A0
		public unsafe static ReadHandle Read(string filename, ReadCommand* readCmds, uint readCmdCount, string assetName = "", ulong typeID = 0UL, AssetLoadingSubsystem subsystem = AssetLoadingSubsystem.Scripts)
		{
			return AsyncReadManager.ReadInternal(filename, (void*)readCmds, readCmdCount, assetName, typeID, subsystem);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x000056C0 File Offset: 0x000038C0
		[FreeFunction("AsyncReadManagerManaged::GetFileInfo", IsThreadSafe = true)]
		[ThreadAndSerializationSafe]
		private unsafe static ReadHandle GetFileInfoInternal(string filename, void* cmd)
		{
			ReadHandle readHandle2;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(filename, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = filename.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ReadHandle readHandle;
				AsyncReadManager.GetFileInfoInternal_Injected(ref managedSpanWrapper, cmd, out readHandle);
			}
			finally
			{
				char* ptr = null;
				ReadHandle readHandle;
				readHandle2 = readHandle;
			}
			return readHandle2;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000571C File Offset: 0x0000391C
		public unsafe static ReadHandle GetFileInfo(string filename, FileInfoResult* result)
		{
			bool flag = result == null;
			if (flag)
			{
				throw new NullReferenceException("GetFileInfo must have a valid FileInfoResult to write into.");
			}
			return AsyncReadManager.GetFileInfoInternal(filename, (void*)result);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000574C File Offset: 0x0000394C
		[FreeFunction("AsyncReadManagerManaged::ReadWithHandles_NativePtr", IsThreadSafe = true)]
		[ThreadAndSerializationSafe]
		private unsafe static ReadHandle ReadWithHandlesInternal_NativePtr(in FileHandle fileHandle, void* readCmdArray, JobHandle dependency)
		{
			ReadHandle readHandle;
			AsyncReadManager.ReadWithHandlesInternal_NativePtr_Injected(in fileHandle, readCmdArray, ref dependency, out readHandle);
			return readHandle;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00005768 File Offset: 0x00003968
		[FreeFunction("AsyncReadManagerManaged::ReadWithHandles_NativeCopy", IsThreadSafe = true)]
		[ThreadAndSerializationSafe]
		private unsafe static ReadHandle ReadWithHandlesInternal_NativeCopy(in FileHandle fileHandle, void* readCmdArray)
		{
			ReadHandle readHandle;
			AsyncReadManager.ReadWithHandlesInternal_NativeCopy_Injected(in fileHandle, readCmdArray, out readHandle);
			return readHandle;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00005780 File Offset: 0x00003980
		public unsafe static ReadHandle ReadDeferred(in FileHandle fileHandle, ReadCommandArray* readCmdArray, JobHandle dependency)
		{
			bool flag = !fileHandle.IsValid();
			if (flag)
			{
				throw new InvalidOperationException("FileHandle is invalid and may not be read from.");
			}
			return AsyncReadManager.ReadWithHandlesInternal_NativePtr(in fileHandle, (void*)readCmdArray, dependency);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x000057B4 File Offset: 0x000039B4
		public static ReadHandle Read(in FileHandle fileHandle, ReadCommandArray readCmdArray)
		{
			bool flag = !fileHandle.IsValid();
			if (flag)
			{
				throw new InvalidOperationException("FileHandle is invalid and may not be read from.");
			}
			return AsyncReadManager.ReadWithHandlesInternal_NativeCopy(in fileHandle, UnsafeUtility.AddressOf<ReadCommandArray>(ref readCmdArray));
		}

		// Token: 0x06000288 RID: 648 RVA: 0x000057EC File Offset: 0x000039EC
		[FreeFunction("AsyncReadManagerManaged::ScheduleOpenRequest", IsThreadSafe = true)]
		[ThreadAndSerializationSafe]
		private unsafe static FileHandle OpenFileAsync_Internal(string fileName)
		{
			FileHandle fileHandle2;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(fileName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = fileName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				FileHandle fileHandle;
				AsyncReadManager.OpenFileAsync_Internal_Injected(ref managedSpanWrapper, out fileHandle);
			}
			finally
			{
				char* ptr = null;
				FileHandle fileHandle;
				fileHandle2 = fileHandle;
			}
			return fileHandle2;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00005848 File Offset: 0x00003A48
		public static FileHandle OpenFileAsync(string fileName)
		{
			bool flag = fileName.Length == 0;
			if (flag)
			{
				throw new InvalidOperationException("FileName is empty");
			}
			return AsyncReadManager.OpenFileAsync_Internal(fileName);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00005878 File Offset: 0x00003A78
		[ThreadAndSerializationSafe]
		[FreeFunction("AsyncReadManagerManaged::ScheduleCloseRequest", IsThreadSafe = true)]
		internal static JobHandle CloseFileAsync(in FileHandle fileHandle, JobHandle dependency)
		{
			JobHandle jobHandle;
			AsyncReadManager.CloseFileAsync_Injected(in fileHandle, ref dependency, out jobHandle);
			return jobHandle;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00005890 File Offset: 0x00003A90
		[FreeFunction("AsyncReadManagerManaged::ScheduleCloseCachedFileRequest", IsThreadSafe = true)]
		[ThreadAndSerializationSafe]
		public unsafe static JobHandle CloseCachedFileAsync(string fileName, JobHandle dependency = default(JobHandle))
		{
			JobHandle jobHandle2;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(fileName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = fileName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				JobHandle jobHandle;
				AsyncReadManager.CloseCachedFileAsync_Injected(ref managedSpanWrapper, ref dependency, out jobHandle);
			}
			finally
			{
				char* ptr = null;
				JobHandle jobHandle;
				jobHandle2 = jobHandle;
			}
			return jobHandle2;
		}

		// Token: 0x0600028C RID: 652
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void ReadInternal_Injected(ref ManagedSpanWrapper filename, void* cmds, uint cmdCount, ref ManagedSpanWrapper assetName, ulong typeID, AssetLoadingSubsystem subsystem, out ReadHandle ret);

		// Token: 0x0600028D RID: 653
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void GetFileInfoInternal_Injected(ref ManagedSpanWrapper filename, void* cmd, out ReadHandle ret);

		// Token: 0x0600028E RID: 654
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void ReadWithHandlesInternal_NativePtr_Injected(in FileHandle fileHandle, void* readCmdArray, [In] ref JobHandle dependency, out ReadHandle ret);

		// Token: 0x0600028F RID: 655
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void ReadWithHandlesInternal_NativeCopy_Injected(in FileHandle fileHandle, void* readCmdArray, out ReadHandle ret);

		// Token: 0x06000290 RID: 656
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void OpenFileAsync_Internal_Injected(ref ManagedSpanWrapper fileName, out FileHandle ret);

		// Token: 0x06000291 RID: 657
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CloseFileAsync_Injected(in FileHandle fileHandle, [In] ref JobHandle dependency, out JobHandle ret);

		// Token: 0x06000292 RID: 658
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CloseCachedFileAsync_Injected(ref ManagedSpanWrapper fileName, [In] ref JobHandle dependency, out JobHandle ret);
	}
}
