using System;
using System.Text;

namespace System.Net
{
	// Token: 0x020006A1 RID: 1697
	internal struct IPv6AddressFormatter
	{
		// Token: 0x0600364A RID: 13898 RVA: 0x000BE2A4 File Offset: 0x000BC4A4
		public IPv6AddressFormatter(ushort[] addr, long scopeId)
		{
			this.address = addr;
			this.scopeId = scopeId;
		}

		// Token: 0x0600364B RID: 13899 RVA: 0x000BE2B4 File Offset: 0x000BC4B4
		private static ushort SwapUShort(ushort number)
		{
			return (ushort)(((number >> 8) & 255) + (((int)number << 8) & 65280));
		}

		// Token: 0x0600364C RID: 13900 RVA: 0x000BE2CA File Offset: 0x000BC4CA
		private uint AsIPv4Int()
		{
			return (uint)(((int)IPv6AddressFormatter.SwapUShort(this.address[7]) << 16) + (int)IPv6AddressFormatter.SwapUShort(this.address[6]));
		}

		// Token: 0x0600364D RID: 13901 RVA: 0x000BE2EC File Offset: 0x000BC4EC
		private bool IsIPv4Compatible()
		{
			for (int i = 0; i < 6; i++)
			{
				if (this.address[i] != 0)
				{
					return false;
				}
			}
			return this.address[6] != 0 && this.AsIPv4Int() > 1U;
		}

		// Token: 0x0600364E RID: 13902 RVA: 0x000BE328 File Offset: 0x000BC528
		private bool IsIPv4Mapped()
		{
			for (int i = 0; i < 5; i++)
			{
				if (this.address[i] != 0)
				{
					return false;
				}
			}
			return this.address[6] != 0 && this.address[5] == ushort.MaxValue;
		}

		// Token: 0x0600364F RID: 13903 RVA: 0x000BE368 File Offset: 0x000BC568
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.IsIPv4Compatible() || this.IsIPv4Mapped())
			{
				stringBuilder.Append("::");
				if (this.IsIPv4Mapped())
				{
					stringBuilder.Append("ffff:");
				}
				stringBuilder.Append(new IPAddress((long)((ulong)this.AsIPv4Int())).ToString());
				return stringBuilder.ToString();
			}
			int num = -1;
			int num2 = 0;
			int num3 = 0;
			for (int i = 0; i < 8; i++)
			{
				if (this.address[i] != 0)
				{
					if (num3 > num2 && num3 > 1)
					{
						num2 = num3;
						num = i - num3;
					}
					num3 = 0;
				}
				else
				{
					num3++;
				}
			}
			if (num3 > num2 && num3 > 1)
			{
				num2 = num3;
				num = 8 - num3;
			}
			if (num == 0)
			{
				stringBuilder.Append(":");
			}
			for (int j = 0; j < 8; j++)
			{
				if (j == num)
				{
					stringBuilder.Append(":");
					j += num2 - 1;
				}
				else
				{
					stringBuilder.AppendFormat("{0:x}", this.address[j]);
					if (j < 7)
					{
						stringBuilder.Append(':');
					}
				}
			}
			if (this.scopeId != 0L)
			{
				stringBuilder.Append('%').Append(this.scopeId);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001FB9 RID: 8121
		private ushort[] address;

		// Token: 0x04001FBA RID: 8122
		private long scopeId;
	}
}
