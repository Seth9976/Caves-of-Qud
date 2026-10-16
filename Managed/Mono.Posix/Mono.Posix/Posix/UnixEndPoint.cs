using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Mono.Posix
{
	// Token: 0x020000A0 RID: 160
	[Obsolete("Use Mono.Unix.UnixEndPoint")]
	[Serializable]
	public class UnixEndPoint : EndPoint
	{
		// Token: 0x06000766 RID: 1894 RVA: 0x000104E6 File Offset: 0x0000E6E6
		public UnixEndPoint(string filename)
		{
			if (filename == null)
			{
				throw new ArgumentNullException("filename");
			}
			if (filename == "")
			{
				throw new ArgumentException("Cannot be empty.", "filename");
			}
			this.filename = filename;
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000767 RID: 1895 RVA: 0x00010520 File Offset: 0x0000E720
		// (set) Token: 0x06000768 RID: 1896 RVA: 0x00010528 File Offset: 0x0000E728
		public string Filename
		{
			get
			{
				return this.filename;
			}
			set
			{
				this.filename = value;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000769 RID: 1897 RVA: 0x00010531 File Offset: 0x0000E731
		public override AddressFamily AddressFamily
		{
			get
			{
				return AddressFamily.Unix;
			}
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x00010534 File Offset: 0x0000E734
		public override EndPoint Create(SocketAddress socketAddress)
		{
			byte[] array = new byte[socketAddress.Size - 2];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = socketAddress[i + 2];
			}
			return new UnixEndPoint(Encoding.Default.GetString(array));
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0001057C File Offset: 0x0000E77C
		public override SocketAddress Serialize()
		{
			byte[] bytes = Encoding.Default.GetBytes(this.filename);
			SocketAddress socketAddress = new SocketAddress(this.AddressFamily, bytes.Length + 2);
			for (int i = 0; i < bytes.Length; i++)
			{
				socketAddress[i + 2] = bytes[i];
			}
			return socketAddress;
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x000105C6 File Offset: 0x0000E7C6
		public override string ToString()
		{
			return this.filename;
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x000105CE File Offset: 0x0000E7CE
		public override int GetHashCode()
		{
			return this.filename.GetHashCode();
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x000105DC File Offset: 0x0000E7DC
		public override bool Equals(object o)
		{
			UnixEndPoint unixEndPoint = o as UnixEndPoint;
			return unixEndPoint != null && unixEndPoint.filename == this.filename;
		}

		// Token: 0x04000560 RID: 1376
		private string filename;
	}
}
