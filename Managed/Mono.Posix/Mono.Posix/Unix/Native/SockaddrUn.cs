using System;
using System.Runtime.InteropServices;

namespace Mono.Unix.Native
{
	// Token: 0x0200006E RID: 110
	[CLSCompliant(false)]
	public sealed class SockaddrUn : Sockaddr, IEquatable<SockaddrUn>
	{
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x0000BA79 File Offset: 0x00009C79
		// (set) Token: 0x0600047A RID: 1146 RVA: 0x0000BA81 File Offset: 0x00009C81
		public UnixAddressFamily sun_family
		{
			get
			{
				return base.sa_family;
			}
			set
			{
				base.sa_family = value;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x0000BA8A File Offset: 0x00009C8A
		// (set) Token: 0x0600047C RID: 1148 RVA: 0x0000BA92 File Offset: 0x00009C92
		public byte[] sun_path { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x0000BA9B File Offset: 0x00009C9B
		// (set) Token: 0x0600047E RID: 1150 RVA: 0x0000BAA3 File Offset: 0x00009CA3
		public long sun_path_len { get; set; }

		// Token: 0x0600047F RID: 1151 RVA: 0x0000BAAC File Offset: 0x00009CAC
		internal override byte[] DynamicData()
		{
			return this.sun_path;
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0000BAB4 File Offset: 0x00009CB4
		internal override long GetDynamicLength()
		{
			return this.sun_path_len;
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0000BABC File Offset: 0x00009CBC
		internal override void SetDynamicLength(long value)
		{
			this.sun_path_len = value;
		}

		// Token: 0x06000482 RID: 1154
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_SockaddrUn_get_sizeof_sun_path", SetLastError = true)]
		private static extern int get_sizeof_sun_path();

		// Token: 0x06000483 RID: 1155 RVA: 0x0000BAC5 File Offset: 0x00009CC5
		public SockaddrUn()
			: base((SockaddrType)32770, UnixAddressFamily.AF_UNIX)
		{
			this.sun_path = new byte[SockaddrUn.sizeof_sun_path];
			this.sun_path_len = 0L;
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0000BAEB File Offset: 0x00009CEB
		public SockaddrUn(int size)
			: base((SockaddrType)32770, UnixAddressFamily.AF_UNIX)
		{
			this.sun_path = new byte[size];
			this.sun_path_len = 0L;
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0000BB10 File Offset: 0x00009D10
		public SockaddrUn(string path, bool linuxAbstractNamespace = false)
			: base((SockaddrType)32770, UnixAddressFamily.AF_UNIX)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			byte[] bytes = UnixEncoding.Instance.GetBytes(path);
			if (linuxAbstractNamespace)
			{
				this.sun_path = new byte[1 + bytes.Length];
				Array.Copy(bytes, 0, this.sun_path, 1, bytes.Length);
			}
			else
			{
				this.sun_path = bytes;
			}
			this.sun_path_len = (long)this.sun_path.Length;
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x0000BB7F File Offset: 0x00009D7F
		public bool IsLinuxAbstractNamespace
		{
			get
			{
				return this.sun_path_len > 0L && this.sun_path[0] == 0;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x0000BB98 File Offset: 0x00009D98
		public string Path
		{
			get
			{
				int num = (this.IsLinuxAbstractNamespace ? 1 : 0);
				int num2 = 0;
				while ((long)(num + num2) < this.sun_path_len && this.sun_path[num + num2] != 0)
				{
					num2++;
				}
				return UnixEncoding.Instance.GetString(this.sun_path, num, num2);
			}
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0000BBE4 File Offset: 0x00009DE4
		public override string ToString()
		{
			return string.Format("{{sa_family={0}, sun_path=\"{1}{2}\"}}", base.sa_family, this.IsLinuxAbstractNamespace ? "\\0" : "", this.Path);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0000BC18 File Offset: 0x00009E18
		public new static SockaddrUn FromSockaddrStorage(SockaddrStorage storage)
		{
			SockaddrUn sockaddrUn = new SockaddrUn((int)storage.data_len);
			storage.CopyTo(sockaddrUn);
			return sockaddrUn;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0000BC3C File Offset: 0x00009E3C
		public override int GetHashCode()
		{
			return this.sun_family.GetHashCode() ^ this.IsLinuxAbstractNamespace.GetHashCode() ^ this.Path.GetHashCode();
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0000BC78 File Offset: 0x00009E78
		public override bool Equals(object obj)
		{
			return obj is SockaddrUn && this.Equals((SockaddrUn)obj);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0000BC90 File Offset: 0x00009E90
		public bool Equals(SockaddrUn value)
		{
			return value != null && (this.sun_family == value.sun_family && this.IsLinuxAbstractNamespace == value.IsLinuxAbstractNamespace) && this.Path == value.Path;
		}

		// Token: 0x04000499 RID: 1177
		private static readonly int sizeof_sun_path = SockaddrUn.get_sizeof_sun_path();
	}
}
