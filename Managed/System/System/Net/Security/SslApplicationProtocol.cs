using System;
using System.Text;

namespace System.Net.Security
{
	// Token: 0x02000855 RID: 2133
	public readonly struct SslApplicationProtocol : IEquatable<SslApplicationProtocol>
	{
		// Token: 0x060043AA RID: 17322 RVA: 0x000EC0A8 File Offset: 0x000EA2A8
		internal SslApplicationProtocol(byte[] protocol, bool copy)
		{
			if (protocol == null)
			{
				throw new ArgumentNullException("protocol");
			}
			if (protocol.Length == 0 || protocol.Length > 255)
			{
				throw new ArgumentException("The application protocol value is invalid.", "protocol");
			}
			if (copy)
			{
				byte[] array = new byte[protocol.Length];
				Array.Copy(protocol, 0, array, 0, protocol.Length);
				this._readOnlyProtocol = new ReadOnlyMemory<byte>(array);
				return;
			}
			this._readOnlyProtocol = new ReadOnlyMemory<byte>(protocol);
		}

		// Token: 0x060043AB RID: 17323 RVA: 0x000EC112 File Offset: 0x000EA312
		public SslApplicationProtocol(byte[] protocol)
		{
			this = new SslApplicationProtocol(protocol, true);
		}

		// Token: 0x060043AC RID: 17324 RVA: 0x000EC11C File Offset: 0x000EA31C
		public SslApplicationProtocol(string protocol)
		{
			this = new SslApplicationProtocol(SslApplicationProtocol.s_utf8.GetBytes(protocol), false);
		}

		// Token: 0x17000F28 RID: 3880
		// (get) Token: 0x060043AD RID: 17325 RVA: 0x000EC130 File Offset: 0x000EA330
		public ReadOnlyMemory<byte> Protocol
		{
			get
			{
				return this._readOnlyProtocol;
			}
		}

		// Token: 0x060043AE RID: 17326 RVA: 0x000EC138 File Offset: 0x000EA338
		public bool Equals(SslApplicationProtocol other)
		{
			return this._readOnlyProtocol.Length == other._readOnlyProtocol.Length && ((this._readOnlyProtocol.IsEmpty && other._readOnlyProtocol.IsEmpty) || this._readOnlyProtocol.Span.SequenceEqual<byte>(other._readOnlyProtocol.Span));
		}

		// Token: 0x060043AF RID: 17327 RVA: 0x000EC19C File Offset: 0x000EA39C
		public override bool Equals(object obj)
		{
			if (obj is SslApplicationProtocol)
			{
				SslApplicationProtocol sslApplicationProtocol = (SslApplicationProtocol)obj;
				return this.Equals(sslApplicationProtocol);
			}
			return false;
		}

		// Token: 0x060043B0 RID: 17328 RVA: 0x000EC1C4 File Offset: 0x000EA3C4
		public unsafe override int GetHashCode()
		{
			if (this._readOnlyProtocol.Length == 0)
			{
				return 0;
			}
			int num = 0;
			ReadOnlySpan<byte> span = this._readOnlyProtocol.Span;
			for (int i = 0; i < this._readOnlyProtocol.Length; i++)
			{
				num = ((num << 5) + num) ^ (int)(*span[i]);
			}
			return num;
		}

		// Token: 0x060043B1 RID: 17329 RVA: 0x000EC218 File Offset: 0x000EA418
		public unsafe override string ToString()
		{
			string text;
			try
			{
				if (this._readOnlyProtocol.Length == 0)
				{
					text = null;
				}
				else
				{
					text = SslApplicationProtocol.s_utf8.GetString(this._readOnlyProtocol.Span);
				}
			}
			catch
			{
				int num = this._readOnlyProtocol.Length * 5;
				char[] array = new char[num];
				int num2 = 0;
				ReadOnlySpan<byte> span = this._readOnlyProtocol.Span;
				for (int i = 0; i < num; i += 5)
				{
					byte b = *span[num2++];
					array[i] = '0';
					array[i + 1] = 'x';
					int num3;
					array[i + 2] = SslApplicationProtocol.GetHexValue(Math.DivRem((int)b, 16, out num3));
					array[i + 3] = SslApplicationProtocol.GetHexValue(num3);
					array[i + 4] = ' ';
				}
				text = new string(array, 0, num - 1);
			}
			return text;
		}

		// Token: 0x060043B2 RID: 17330 RVA: 0x000EC2F0 File Offset: 0x000EA4F0
		private static char GetHexValue(int i)
		{
			if (i < 10)
			{
				return (char)(i + 48);
			}
			return (char)(i - 10 + 97);
		}

		// Token: 0x060043B3 RID: 17331 RVA: 0x000EC305 File Offset: 0x000EA505
		public static bool operator ==(SslApplicationProtocol left, SslApplicationProtocol right)
		{
			return left.Equals(right);
		}

		// Token: 0x060043B4 RID: 17332 RVA: 0x000EC30F File Offset: 0x000EA50F
		public static bool operator !=(SslApplicationProtocol left, SslApplicationProtocol right)
		{
			return !(left == right);
		}

		// Token: 0x0400290C RID: 10508
		private readonly ReadOnlyMemory<byte> _readOnlyProtocol;

		// Token: 0x0400290D RID: 10509
		private static readonly Encoding s_utf8 = Encoding.GetEncoding(Encoding.UTF8.CodePage, EncoderFallback.ExceptionFallback, DecoderFallback.ExceptionFallback);

		// Token: 0x0400290E RID: 10510
		public static readonly SslApplicationProtocol Http2 = new SslApplicationProtocol(new byte[] { 104, 50 }, false);

		// Token: 0x0400290F RID: 10511
		public static readonly SslApplicationProtocol Http11 = new SslApplicationProtocol(new byte[] { 104, 116, 116, 112, 47, 49, 46, 49 }, false);
	}
}
