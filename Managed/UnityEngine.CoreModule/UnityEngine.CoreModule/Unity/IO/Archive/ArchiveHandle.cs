using System;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.IO.Archive
{
	// Token: 0x02000094 RID: 148
	[NativeHeader("Runtime/VirtualFileSystem/ArchiveFileSystem/ArchiveFileHandle.h")]
	[RequiredByNativeCode]
	public struct ArchiveHandle
	{
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x00005E9C File Offset: 0x0000409C
		public ArchiveStatus Status
		{
			get
			{
				this.ThrowIfInvalid();
				return ArchiveFileInterface.Archive_GetStatus(this);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x00005EC0 File Offset: 0x000040C0
		public JobHandle JobHandle
		{
			get
			{
				this.ThrowIfInvalid();
				return ArchiveFileInterface.Archive_GetJobHandle(this);
			}
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00005EE4 File Offset: 0x000040E4
		public JobHandle Unmount()
		{
			this.ThrowIfInvalid();
			return ArchiveFileInterface.Archive_UnmountAsync(this);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00005F08 File Offset: 0x00004108
		private void ThrowIfInvalid()
		{
			bool flag = !ArchiveFileInterface.Archive_IsValid(this);
			if (flag)
			{
				throw new InvalidOperationException("The archive has already been unmounted.");
			}
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00005F34 File Offset: 0x00004134
		public string GetMountPath()
		{
			this.ThrowIfInvalid();
			return ArchiveFileInterface.Archive_GetMountPath(this);
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060002FA RID: 762 RVA: 0x00005F58 File Offset: 0x00004158
		public CompressionType Compression
		{
			get
			{
				this.ThrowIfInvalid();
				return ArchiveFileInterface.Archive_GetCompression(this);
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060002FB RID: 763 RVA: 0x00005F7C File Offset: 0x0000417C
		public bool IsStreamed
		{
			get
			{
				this.ThrowIfInvalid();
				return ArchiveFileInterface.Archive_IsStreamed(this);
			}
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00005FA0 File Offset: 0x000041A0
		public ArchiveFileInfo[] GetFileInfo()
		{
			this.ThrowIfInvalid();
			return ArchiveFileInterface.Archive_GetFileInfo(this);
		}

		// Token: 0x04000253 RID: 595
		internal ulong Handle;
	}
}
