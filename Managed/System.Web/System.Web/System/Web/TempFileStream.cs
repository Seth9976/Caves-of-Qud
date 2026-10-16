using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Web
{
	// Token: 0x020000DB RID: 219
	internal class TempFileStream : FileStream
	{
		// Token: 0x06000BC8 RID: 3016 RVA: 0x0001F147 File Offset: 0x0001D347
		public TempFileStream(string name)
			: base(name, FileMode.Create, FileAccess.ReadWrite, FileShare.None, 8192)
		{
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000BC9 RID: 3017 RVA: 0x0001F158 File Offset: 0x0001D358
		public override bool CanRead
		{
			get
			{
				return this.read_mode;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000BCA RID: 3018 RVA: 0x0001F160 File Offset: 0x0001D360
		public override bool CanWrite
		{
			get
			{
				return !this.read_mode;
			}
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x0001F16B File Offset: 0x0001D36B
		public void SavePosition()
		{
			this.saved_position = this.Position;
			this.Position = 0L;
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x0001F181 File Offset: 0x0001D381
		public void RestorePosition()
		{
			this.Position = this.saved_position;
			this.saved_position = -1L;
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x0001F197 File Offset: 0x0001D397
		public void SetReadOnly()
		{
			this.read_mode = true;
			this.Position = 0L;
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x0001F1A8 File Offset: 0x0001D3A8
		public void SetWriteOnly()
		{
			this.read_mode = false;
			this.Position = 0L;
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x0001F1B9 File Offset: 0x0001D3B9
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.read_mode)
			{
				throw new InvalidOperationException("mode read");
			}
			base.Write(buffer, offset, count);
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x0001F1D7 File Offset: 0x0001D3D7
		public override int Read([In] [Out] byte[] buffer, int offset, int count)
		{
			if (!this.read_mode)
			{
				throw new InvalidOperationException("mode write");
			}
			return base.Read(buffer, offset, count);
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x0001F1F8 File Offset: 0x0001D3F8
		protected override void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				this.disposed = true;
				base.Dispose(disposing);
				try
				{
					File.Delete(this.Name);
				}
				catch
				{
				}
			}
		}

		// Token: 0x040010B9 RID: 4281
		private bool read_mode;

		// Token: 0x040010BA RID: 4282
		private bool disposed;

		// Token: 0x040010BB RID: 4283
		private long saved_position;
	}
}
