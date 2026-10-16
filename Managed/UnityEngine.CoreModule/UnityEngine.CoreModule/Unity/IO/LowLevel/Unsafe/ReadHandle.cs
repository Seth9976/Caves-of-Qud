using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Bindings;

namespace Unity.IO.LowLevel.Unsafe
{
	// Token: 0x02000088 RID: 136
	public struct ReadHandle : IDisposable
	{
		// Token: 0x06000265 RID: 613 RVA: 0x00005340 File Offset: 0x00003540
		public bool IsValid()
		{
			return ReadHandle.IsReadHandleValid(this);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00005360 File Offset: 0x00003560
		public void Dispose()
		{
			bool flag = !ReadHandle.IsReadHandleValid(this);
			if (flag)
			{
				throw new InvalidOperationException("ReadHandle.Dispose cannot be called twice on the same ReadHandle");
			}
			bool flag2 = this.Status == ReadStatus.InProgress;
			if (flag2)
			{
				throw new InvalidOperationException("ReadHandle.Dispose cannot be called until the read operation completes");
			}
			ReadHandle.ReleaseReadHandle(this);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x000053B0 File Offset: 0x000035B0
		public void Cancel()
		{
			bool flag = !ReadHandle.IsReadHandleValid(this);
			if (flag)
			{
				throw new InvalidOperationException("ReadHandle.Cancel cannot be called on a disposed ReadHandle");
			}
			ReadHandle.CancelInternal(this);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x000053E8 File Offset: 0x000035E8
		[FreeFunction("AsyncReadManagerManaged::CancelReadRequest")]
		private static void CancelInternal(ReadHandle handle)
		{
			ReadHandle.CancelInternal_Injected(ref handle);
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000269 RID: 617 RVA: 0x000053FC File Offset: 0x000035FC
		public JobHandle JobHandle
		{
			get
			{
				bool flag = !ReadHandle.IsReadHandleValid(this);
				if (flag)
				{
					throw new InvalidOperationException("ReadHandle.JobHandle cannot be called after the ReadHandle has been disposed");
				}
				return ReadHandle.GetJobHandle(this);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600026A RID: 618 RVA: 0x00005438 File Offset: 0x00003638
		public ReadStatus Status
		{
			get
			{
				bool flag = !ReadHandle.IsReadHandleValid(this);
				if (flag)
				{
					throw new InvalidOperationException("Cannot use a ReadHandle that has been disposed");
				}
				return ReadHandle.GetReadStatus(this);
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600026B RID: 619 RVA: 0x00005474 File Offset: 0x00003674
		public long ReadCount
		{
			get
			{
				bool flag = !ReadHandle.IsReadHandleValid(this);
				if (flag)
				{
					throw new InvalidOperationException("Cannot use a ReadHandle that has been disposed");
				}
				return ReadHandle.GetReadCount(this);
			}
		}

		// Token: 0x0600026C RID: 620 RVA: 0x000054B0 File Offset: 0x000036B0
		public long GetBytesRead()
		{
			bool flag = !ReadHandle.IsReadHandleValid(this);
			if (flag)
			{
				throw new InvalidOperationException("ReadHandle.GetBytesRead cannot be called after the ReadHandle has been disposed");
			}
			return ReadHandle.GetBytesRead(this);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x000054EC File Offset: 0x000036EC
		public long GetBytesRead(uint readCommandIndex)
		{
			bool flag = !ReadHandle.IsReadHandleValid(this);
			if (flag)
			{
				throw new InvalidOperationException("ReadHandle.GetBytesRead cannot be called after the ReadHandle has been disposed");
			}
			return ReadHandle.GetBytesReadForCommand(this, readCommandIndex);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00005528 File Offset: 0x00003728
		public unsafe ulong* GetBytesReadArray()
		{
			bool flag = !ReadHandle.IsReadHandleValid(this);
			if (flag)
			{
				throw new InvalidOperationException("ReadHandle.GetBytesReadArray cannot be called after the ReadHandle has been disposed");
			}
			return ReadHandle.GetBytesReadArray(this);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00005564 File Offset: 0x00003764
		[ThreadAndSerializationSafe]
		[FreeFunction("AsyncReadManagerManaged::GetReadStatus", IsThreadSafe = true)]
		private static ReadStatus GetReadStatus(ReadHandle handle)
		{
			return ReadHandle.GetReadStatus_Injected(ref handle);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00005578 File Offset: 0x00003778
		[FreeFunction("AsyncReadManagerManaged::GetReadCount", IsThreadSafe = true)]
		[ThreadAndSerializationSafe]
		private static long GetReadCount(ReadHandle handle)
		{
			return ReadHandle.GetReadCount_Injected(ref handle);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000558C File Offset: 0x0000378C
		[ThreadAndSerializationSafe]
		[FreeFunction("AsyncReadManagerManaged::GetBytesRead", IsThreadSafe = true)]
		private static long GetBytesRead(ReadHandle handle)
		{
			return ReadHandle.GetBytesRead_Injected(ref handle);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x000055A0 File Offset: 0x000037A0
		[FreeFunction("AsyncReadManagerManaged::GetBytesReadForCommand", IsThreadSafe = true)]
		[ThreadAndSerializationSafe]
		private static long GetBytesReadForCommand(ReadHandle handle, uint readCommandIndex)
		{
			return ReadHandle.GetBytesReadForCommand_Injected(ref handle, readCommandIndex);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x000055B8 File Offset: 0x000037B8
		[ThreadAndSerializationSafe]
		[FreeFunction("AsyncReadManagerManaged::GetBytesReadArray", IsThreadSafe = true)]
		private unsafe static ulong* GetBytesReadArray(ReadHandle handle)
		{
			return ReadHandle.GetBytesReadArray_Injected(ref handle);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x000055CC File Offset: 0x000037CC
		[ThreadAndSerializationSafe]
		[FreeFunction("AsyncReadManagerManaged::ReleaseReadHandle", IsThreadSafe = true)]
		private static void ReleaseReadHandle(ReadHandle handle)
		{
			ReadHandle.ReleaseReadHandle_Injected(ref handle);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x000055E0 File Offset: 0x000037E0
		[FreeFunction("AsyncReadManagerManaged::IsReadHandleValid", IsThreadSafe = true)]
		[ThreadAndSerializationSafe]
		private static bool IsReadHandleValid(ReadHandle handle)
		{
			return ReadHandle.IsReadHandleValid_Injected(ref handle);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x000055F4 File Offset: 0x000037F4
		[FreeFunction("AsyncReadManagerManaged::GetJobHandle", IsThreadSafe = true)]
		[ThreadAndSerializationSafe]
		private static JobHandle GetJobHandle(ReadHandle handle)
		{
			JobHandle jobHandle;
			ReadHandle.GetJobHandle_Injected(ref handle, out jobHandle);
			return jobHandle;
		}

		// Token: 0x06000277 RID: 631
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CancelInternal_Injected([In] ref ReadHandle handle);

		// Token: 0x06000278 RID: 632
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ReadStatus GetReadStatus_Injected([In] ref ReadHandle handle);

		// Token: 0x06000279 RID: 633
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long GetReadCount_Injected([In] ref ReadHandle handle);

		// Token: 0x0600027A RID: 634
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long GetBytesRead_Injected([In] ref ReadHandle handle);

		// Token: 0x0600027B RID: 635
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long GetBytesReadForCommand_Injected([In] ref ReadHandle handle, uint readCommandIndex);

		// Token: 0x0600027C RID: 636
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern ulong* GetBytesReadArray_Injected([In] ref ReadHandle handle);

		// Token: 0x0600027D RID: 637
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReleaseReadHandle_Injected([In] ref ReadHandle handle);

		// Token: 0x0600027E RID: 638
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsReadHandleValid_Injected([In] ref ReadHandle handle);

		// Token: 0x0600027F RID: 639
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetJobHandle_Injected([In] ref ReadHandle handle, out JobHandle ret);

		// Token: 0x04000214 RID: 532
		[NativeDisableUnsafePtrRestriction]
		internal IntPtr ptr;

		// Token: 0x04000215 RID: 533
		internal int version;
	}
}
