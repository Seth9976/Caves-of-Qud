using System;

namespace System.Net
{
	// Token: 0x020006A4 RID: 1700
	internal sealed class ListenerPrefix
	{
		// Token: 0x0600365E RID: 13918 RVA: 0x000BE8F3 File Offset: 0x000BCAF3
		public ListenerPrefix(string prefix)
		{
			this.original = prefix;
			this.Parse(prefix);
		}

		// Token: 0x0600365F RID: 13919 RVA: 0x000BE909 File Offset: 0x000BCB09
		public override string ToString()
		{
			return this.original;
		}

		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x06003660 RID: 13920 RVA: 0x000BE911 File Offset: 0x000BCB11
		// (set) Token: 0x06003661 RID: 13921 RVA: 0x000BE919 File Offset: 0x000BCB19
		public IPAddress[] Addresses
		{
			get
			{
				return this.addresses;
			}
			set
			{
				this.addresses = value;
			}
		}

		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x06003662 RID: 13922 RVA: 0x000BE922 File Offset: 0x000BCB22
		public bool Secure
		{
			get
			{
				return this.secure;
			}
		}

		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x06003663 RID: 13923 RVA: 0x000BE92A File Offset: 0x000BCB2A
		public string Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x06003664 RID: 13924 RVA: 0x000BE932 File Offset: 0x000BCB32
		public int Port
		{
			get
			{
				return (int)this.port;
			}
		}

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x06003665 RID: 13925 RVA: 0x000BE93A File Offset: 0x000BCB3A
		public string Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x06003666 RID: 13926 RVA: 0x000BE944 File Offset: 0x000BCB44
		public override bool Equals(object o)
		{
			ListenerPrefix listenerPrefix = o as ListenerPrefix;
			return listenerPrefix != null && this.original == listenerPrefix.original;
		}

		// Token: 0x06003667 RID: 13927 RVA: 0x000BE96E File Offset: 0x000BCB6E
		public override int GetHashCode()
		{
			return this.original.GetHashCode();
		}

		// Token: 0x06003668 RID: 13928 RVA: 0x000BE97C File Offset: 0x000BCB7C
		private void Parse(string uri)
		{
			ushort num = 80;
			if (uri.StartsWith("https://"))
			{
				num = 443;
				this.secure = true;
			}
			int length = uri.Length;
			int num2 = uri.IndexOf(':') + 3;
			if (num2 >= length)
			{
				throw new ArgumentException("No host specified.");
			}
			int num3 = uri.IndexOf(':', num2, length - num2);
			if (uri[num2] == '[')
			{
				num3 = uri.IndexOf("]:") + 1;
			}
			if (num2 == num3)
			{
				throw new ArgumentException("No host specified.");
			}
			int num4 = uri.IndexOf('/', num2, length - num2);
			if (num4 == -1)
			{
				throw new ArgumentException("No path specified.");
			}
			if (num3 > 0)
			{
				this.host = uri.Substring(num2, num3 - num2).Trim(new char[] { '[', ']' });
				this.port = ushort.Parse(uri.Substring(num3 + 1, num4 - num3 - 1));
			}
			else
			{
				this.host = uri.Substring(num2, num4 - num2).Trim(new char[] { '[', ']' });
				this.port = num;
			}
			this.path = uri.Substring(num4);
			if (this.path.Length != 1)
			{
				this.path = this.path.Substring(0, this.path.Length - 1);
			}
		}

		// Token: 0x06003669 RID: 13929 RVA: 0x000BEAC8 File Offset: 0x000BCCC8
		public static void CheckUri(string uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uriPrefix");
			}
			if (!uri.StartsWith("http://") && !uri.StartsWith("https://"))
			{
				throw new ArgumentException("Only 'http' and 'https' schemes are supported.");
			}
			int length = uri.Length;
			int num = uri.IndexOf(':') + 3;
			if (num >= length)
			{
				throw new ArgumentException("No host specified.");
			}
			int num2 = uri.IndexOf(':', num, length - num);
			if (uri[num] == '[')
			{
				num2 = uri.IndexOf("]:") + 1;
			}
			if (num == num2)
			{
				throw new ArgumentException("No host specified.");
			}
			int num3 = uri.IndexOf('/', num, length - num);
			if (num3 == -1)
			{
				throw new ArgumentException("No path specified.");
			}
			if (num2 > 0)
			{
				try
				{
					int num4 = int.Parse(uri.Substring(num2 + 1, num3 - num2 - 1));
					if (num4 <= 0 || num4 >= 65536)
					{
						throw new Exception();
					}
				}
				catch
				{
					throw new ArgumentException("Invalid port.");
				}
			}
			if (uri[uri.Length - 1] != '/')
			{
				throw new ArgumentException("The prefix must end with '/'");
			}
		}

		// Token: 0x04001FC7 RID: 8135
		private string original;

		// Token: 0x04001FC8 RID: 8136
		private string host;

		// Token: 0x04001FC9 RID: 8137
		private ushort port;

		// Token: 0x04001FCA RID: 8138
		private string path;

		// Token: 0x04001FCB RID: 8139
		private bool secure;

		// Token: 0x04001FCC RID: 8140
		private IPAddress[] addresses;

		// Token: 0x04001FCD RID: 8141
		public HttpListener Listener;
	}
}
