using System;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine.Bindings;

namespace Unity.IO.LowLevel.Unsafe
{
	// Token: 0x02000087 RID: 135
	public readonly struct FileHandle
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000527C File Offset: 0x0000347C
		public FileStatus Status
		{
			get
			{
				bool flag = !FileHandle.IsFileHandleValid(in this);
				if (flag)
				{
					throw new InvalidOperationException("FileHandle.Status cannot be called on a closed FileHandle");
				}
				return FileHandle.GetFileStatus_Internal(in this);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600025E RID: 606 RVA: 0x000052AC File Offset: 0x000034AC
		public JobHandle JobHandle
		{
			get
			{
				bool flag = !FileHandle.IsFileHandleValid(in this);
				if (flag)
				{
					throw new InvalidOperationException("FileHandle.JobHandle cannot be called on a closed FileHandle");
				}
				return FileHandle.GetJobHandle_Internal(in this);
			}
		}

		// Token: 0x0600025F RID: 607 RVA: 0x000052DC File Offset: 0x000034DC
		public bool IsValid()
		{
			return FileHandle.IsFileHandleValid(in this);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x000052F4 File Offset: 0x000034F4
		public JobHandle Close(JobHandle dependency = default(JobHandle))
		{
			bool flag = !FileHandle.IsFileHandleValid(in this);
			if (flag)
			{
				throw new InvalidOperationException("FileHandle.Close cannot be called twice on the same FileHandle");
			}
			return AsyncReadManager.CloseFileAsync(in this, dependency);
		}

		// Token: 0x06000261 RID: 609
		[FreeFunction("AsyncReadManagerManaged::IsFileHandleValid")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsFileHandleValid(in FileHandle handle);

		// Token: 0x06000262 RID: 610
		[FreeFunction("AsyncReadManagerManaged::GetFileStatusFromManagedHandle")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern FileStatus GetFileStatus_Internal(in FileHandle handle);

		// Token: 0x06000263 RID: 611 RVA: 0x00005328 File Offset: 0x00003528
		[FreeFunction("AsyncReadManagerManaged::GetJobFenceFromManagedHandle")]
		private static JobHandle GetJobHandle_Internal(in FileHandle handle)
		{
			JobHandle jobHandle;
			FileHandle.GetJobHandle_Internal_Injected(in handle, out jobHandle);
			return jobHandle;
		}

		// Token: 0x06000264 RID: 612
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetJobHandle_Internal_Injected(in FileHandle handle, out JobHandle ret);

		// Token: 0x04000212 RID: 530
		[NativeDisableUnsafePtrRestriction]
		internal readonly IntPtr fileCommandPtr;

		// Token: 0x04000213 RID: 531
		internal readonly int version;
	}
}
