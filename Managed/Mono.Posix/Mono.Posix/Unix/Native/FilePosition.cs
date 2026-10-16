using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Mono.Unix.Native
{
	// Token: 0x02000029 RID: 41
	public sealed class FilePosition : MarshalByRefObject, IDisposable, IEquatable<FilePosition>
	{
		// Token: 0x06000363 RID: 867 RVA: 0x00009540 File Offset: 0x00007740
		public FilePosition()
		{
			IntPtr intPtr = Stdlib.CreateFilePosition();
			if (intPtr == IntPtr.Zero)
			{
				throw new OutOfMemoryException("Unable to malloc fpos_t!");
			}
			this.pos = new HandleRef(this, intPtr);
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000364 RID: 868 RVA: 0x0000957E File Offset: 0x0000777E
		internal HandleRef Handle
		{
			get
			{
				return this.pos;
			}
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00009586 File Offset: 0x00007786
		public void Dispose()
		{
			this.Cleanup();
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00009594 File Offset: 0x00007794
		private void Cleanup()
		{
			if (this.pos.Handle != IntPtr.Zero)
			{
				Stdlib.free(this.pos.Handle);
				this.pos = new HandleRef(this, IntPtr.Zero);
			}
		}

		// Token: 0x06000367 RID: 871 RVA: 0x000095CE File Offset: 0x000077CE
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"(",
				base.ToString(),
				" ",
				this.GetDump(),
				")"
			});
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00009608 File Offset: 0x00007808
		private string GetDump()
		{
			if (FilePosition.FilePositionDumpSize <= 0)
			{
				return "internal error";
			}
			StringBuilder stringBuilder = new StringBuilder(FilePosition.FilePositionDumpSize + 1);
			if (Stdlib.DumpFilePosition(stringBuilder, this.Handle, FilePosition.FilePositionDumpSize + 1) <= 0)
			{
				return "internal error dumping fpos_t";
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00009654 File Offset: 0x00007854
		public override bool Equals(object obj)
		{
			FilePosition filePosition = obj as FilePosition;
			return obj != null && !(filePosition == null) && this.ToString().Equals(obj.ToString());
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00009687 File Offset: 0x00007887
		public bool Equals(FilePosition value)
		{
			return this == value || this.ToString().Equals(value.ToString());
		}

		// Token: 0x0600036B RID: 875 RVA: 0x000096A0 File Offset: 0x000078A0
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x0600036C RID: 876 RVA: 0x000096B0 File Offset: 0x000078B0
		~FilePosition()
		{
			this.Cleanup();
		}

		// Token: 0x0600036D RID: 877 RVA: 0x000096DC File Offset: 0x000078DC
		public static bool operator ==(FilePosition lhs, FilePosition rhs)
		{
			return object.Equals(lhs, rhs);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x000096E5 File Offset: 0x000078E5
		public static bool operator !=(FilePosition lhs, FilePosition rhs)
		{
			return !object.Equals(lhs, rhs);
		}

		// Token: 0x04000138 RID: 312
		private static readonly int FilePositionDumpSize = Stdlib.DumpFilePosition(null, new HandleRef(null, IntPtr.Zero), 0);

		// Token: 0x04000139 RID: 313
		private HandleRef pos;
	}
}
