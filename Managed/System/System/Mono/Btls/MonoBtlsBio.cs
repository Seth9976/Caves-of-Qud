using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Mono.Btls
{
	// Token: 0x020000D0 RID: 208
	internal class MonoBtlsBio : MonoBtlsObject
	{
		// Token: 0x0600040E RID: 1038 RVA: 0x0000CA92 File Offset: 0x0000AC92
		internal MonoBtlsBio(MonoBtlsBio.BoringBioHandle handle)
			: base(handle)
		{
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x0000CA9B File Offset: 0x0000AC9B
		protected internal new MonoBtlsBio.BoringBioHandle Handle
		{
			get
			{
				return (MonoBtlsBio.BoringBioHandle)base.Handle;
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000CAA8 File Offset: 0x0000ACA8
		public static MonoBtlsBio CreateMonoStream(Stream stream)
		{
			return MonoBtlsBioMono.CreateStream(stream, false);
		}

		// Token: 0x06000411 RID: 1041
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_bio_read(IntPtr bio, IntPtr data, int len);

		// Token: 0x06000412 RID: 1042
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_bio_write(IntPtr bio, IntPtr data, int len);

		// Token: 0x06000413 RID: 1043
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_bio_flush(IntPtr bio);

		// Token: 0x06000414 RID: 1044
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_bio_indent(IntPtr bio, uint indent, uint max_indent);

		// Token: 0x06000415 RID: 1045
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_bio_hexdump(IntPtr bio, IntPtr data, int len, uint indent);

		// Token: 0x06000416 RID: 1046
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_bio_print_errors(IntPtr bio);

		// Token: 0x06000417 RID: 1047
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_bio_free(IntPtr handle);

		// Token: 0x06000418 RID: 1048 RVA: 0x0000CAB4 File Offset: 0x0000ACB4
		public int Read(byte[] buffer, int offset, int size)
		{
			base.CheckThrow();
			IntPtr intPtr = Marshal.AllocHGlobal(size);
			if (intPtr == IntPtr.Zero)
			{
				throw new OutOfMemoryException();
			}
			bool flag = false;
			int num2;
			try
			{
				this.Handle.DangerousAddRef(ref flag);
				int num = MonoBtlsBio.mono_btls_bio_read(this.Handle.DangerousGetHandle(), intPtr, size);
				if (num > 0)
				{
					Marshal.Copy(intPtr, buffer, offset, num);
				}
				num2 = num;
			}
			finally
			{
				if (flag)
				{
					this.Handle.DangerousRelease();
				}
				Marshal.FreeHGlobal(intPtr);
			}
			return num2;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000CB3C File Offset: 0x0000AD3C
		public int Write(byte[] buffer, int offset, int size)
		{
			base.CheckThrow();
			IntPtr intPtr = Marshal.AllocHGlobal(size);
			if (intPtr == IntPtr.Zero)
			{
				throw new OutOfMemoryException();
			}
			bool flag = false;
			int num;
			try
			{
				this.Handle.DangerousAddRef(ref flag);
				Marshal.Copy(buffer, offset, intPtr, size);
				num = MonoBtlsBio.mono_btls_bio_write(this.Handle.DangerousGetHandle(), intPtr, size);
			}
			finally
			{
				if (flag)
				{
					this.Handle.DangerousRelease();
				}
				Marshal.FreeHGlobal(intPtr);
			}
			return num;
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000CBBC File Offset: 0x0000ADBC
		public int Flush()
		{
			base.CheckThrow();
			bool flag = false;
			int num;
			try
			{
				this.Handle.DangerousAddRef(ref flag);
				num = MonoBtlsBio.mono_btls_bio_flush(this.Handle.DangerousGetHandle());
			}
			finally
			{
				if (flag)
				{
					this.Handle.DangerousRelease();
				}
			}
			return num;
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0000CC14 File Offset: 0x0000AE14
		public int Indent(uint indent, uint max_indent)
		{
			base.CheckThrow();
			bool flag = false;
			int num;
			try
			{
				this.Handle.DangerousAddRef(ref flag);
				num = MonoBtlsBio.mono_btls_bio_indent(this.Handle.DangerousGetHandle(), indent, max_indent);
			}
			finally
			{
				if (flag)
				{
					this.Handle.DangerousRelease();
				}
			}
			return num;
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000CC6C File Offset: 0x0000AE6C
		public int HexDump(byte[] buffer, uint indent)
		{
			base.CheckThrow();
			IntPtr intPtr = Marshal.AllocHGlobal(buffer.Length);
			if (intPtr == IntPtr.Zero)
			{
				throw new OutOfMemoryException();
			}
			bool flag = false;
			int num;
			try
			{
				this.Handle.DangerousAddRef(ref flag);
				Marshal.Copy(buffer, 0, intPtr, buffer.Length);
				num = MonoBtlsBio.mono_btls_bio_hexdump(this.Handle.DangerousGetHandle(), intPtr, buffer.Length, indent);
			}
			finally
			{
				if (flag)
				{
					this.Handle.DangerousRelease();
				}
				Marshal.FreeHGlobal(intPtr);
			}
			return num;
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000CCF4 File Offset: 0x0000AEF4
		public void PrintErrors()
		{
			base.CheckThrow();
			bool flag = false;
			try
			{
				this.Handle.DangerousAddRef(ref flag);
				MonoBtlsBio.mono_btls_bio_print_errors(this.Handle.DangerousGetHandle());
			}
			finally
			{
				if (flag)
				{
					this.Handle.DangerousRelease();
				}
			}
		}

		// Token: 0x020000D1 RID: 209
		protected internal class BoringBioHandle : MonoBtlsObject.MonoBtlsHandle
		{
			// Token: 0x0600041E RID: 1054 RVA: 0x0000CD48 File Offset: 0x0000AF48
			public BoringBioHandle(IntPtr handle)
				: base(handle, true)
			{
			}

			// Token: 0x0600041F RID: 1055 RVA: 0x0000CD52 File Offset: 0x0000AF52
			protected override bool ReleaseHandle()
			{
				if (this.handle != IntPtr.Zero)
				{
					MonoBtlsBio.mono_btls_bio_free(this.handle);
					this.handle = IntPtr.Zero;
				}
				return true;
			}
		}
	}
}
