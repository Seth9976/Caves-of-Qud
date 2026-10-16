using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Mono.Unix
{
	// Token: 0x02000014 RID: 20
	[Serializable]
	public class UnixEndPoint : EndPoint
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x00004653 File Offset: 0x00002853
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

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x0000468D File Offset: 0x0000288D
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x00004695 File Offset: 0x00002895
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

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x0000469E File Offset: 0x0000289E
		public override AddressFamily AddressFamily
		{
			get
			{
				return AddressFamily.Unix;
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000046A4 File Offset: 0x000028A4
		public override EndPoint Create(SocketAddress socketAddress)
		{
			int num = socketAddress.Size - 2;
			byte[] array = new byte[num];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = socketAddress[i + 2];
				if (array[i] == 0)
				{
					num = i;
					break;
				}
			}
			if (num == 0)
			{
				return new UnixEndPoint("dummy")
				{
					filename = ""
				};
			}
			return new UnixEndPoint(Encoding.Default.GetString(array, 0, num));
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004710 File Offset: 0x00002910
		public override SocketAddress Serialize()
		{
			byte[] bytes = Encoding.Default.GetBytes(this.filename);
			SocketAddress socketAddress = new SocketAddress(this.AddressFamily, 2 + bytes.Length + 1);
			for (int i = 0; i < bytes.Length; i++)
			{
				socketAddress[2 + i] = bytes[i];
			}
			socketAddress[2 + bytes.Length] = 0;
			return socketAddress;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004768 File Offset: 0x00002968
		public override string ToString()
		{
			return this.filename;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004770 File Offset: 0x00002970
		public override int GetHashCode()
		{
			return this.filename.GetHashCode();
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004780 File Offset: 0x00002980
		public override bool Equals(object o)
		{
			UnixEndPoint unixEndPoint = o as UnixEndPoint;
			return unixEndPoint != null && unixEndPoint.filename == this.filename;
		}

		// Token: 0x0400006E RID: 110
		private string filename;
	}
}
