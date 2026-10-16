using System;
using System.Net.Sockets;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000757 RID: 1879
	internal class Win32UnicastIPAddressInformation : UnicastIPAddressInformation
	{
		// Token: 0x06003B46 RID: 15174 RVA: 0x000CC024 File Offset: 0x000CA224
		public Win32UnicastIPAddressInformation(Win32_IP_ADAPTER_UNICAST_ADDRESS info)
		{
			this.info = info;
			IPAddress ipaddress = info.Address.GetIPAddress();
			if (ipaddress.AddressFamily == AddressFamily.InterNetwork)
			{
				this.ipv4Mask = Win32UnicastIPAddressInformation.PrefixLengthToSubnetMask(info.OnLinkPrefixLength, ipaddress.AddressFamily);
			}
		}

		// Token: 0x17000D58 RID: 3416
		// (get) Token: 0x06003B47 RID: 15175 RVA: 0x000CC06B File Offset: 0x000CA26B
		public override IPAddress Address
		{
			get
			{
				return this.info.Address.GetIPAddress();
			}
		}

		// Token: 0x17000D59 RID: 3417
		// (get) Token: 0x06003B48 RID: 15176 RVA: 0x000CC07D File Offset: 0x000CA27D
		public override bool IsDnsEligible
		{
			get
			{
				return this.info.LengthFlags.IsDnsEligible;
			}
		}

		// Token: 0x17000D5A RID: 3418
		// (get) Token: 0x06003B49 RID: 15177 RVA: 0x000CC08F File Offset: 0x000CA28F
		public override bool IsTransient
		{
			get
			{
				return this.info.LengthFlags.IsTransient;
			}
		}

		// Token: 0x17000D5B RID: 3419
		// (get) Token: 0x06003B4A RID: 15178 RVA: 0x000CC0A1 File Offset: 0x000CA2A1
		public override long AddressPreferredLifetime
		{
			get
			{
				return (long)((ulong)this.info.PreferredLifetime);
			}
		}

		// Token: 0x17000D5C RID: 3420
		// (get) Token: 0x06003B4B RID: 15179 RVA: 0x000CC0AF File Offset: 0x000CA2AF
		public override long AddressValidLifetime
		{
			get
			{
				return (long)((ulong)this.info.ValidLifetime);
			}
		}

		// Token: 0x17000D5D RID: 3421
		// (get) Token: 0x06003B4C RID: 15180 RVA: 0x000CC0BD File Offset: 0x000CA2BD
		public override long DhcpLeaseLifetime
		{
			get
			{
				return (long)((ulong)this.info.LeaseLifetime);
			}
		}

		// Token: 0x17000D5E RID: 3422
		// (get) Token: 0x06003B4D RID: 15181 RVA: 0x000CC0CB File Offset: 0x000CA2CB
		public override DuplicateAddressDetectionState DuplicateAddressDetectionState
		{
			get
			{
				return this.info.DadState;
			}
		}

		// Token: 0x17000D5F RID: 3423
		// (get) Token: 0x06003B4E RID: 15182 RVA: 0x000CC0D8 File Offset: 0x000CA2D8
		public override IPAddress IPv4Mask
		{
			get
			{
				if (this.Address.AddressFamily != AddressFamily.InterNetwork)
				{
					return IPAddress.Any;
				}
				return this.ipv4Mask;
			}
		}

		// Token: 0x17000D60 RID: 3424
		// (get) Token: 0x06003B4F RID: 15183 RVA: 0x000CC0F4 File Offset: 0x000CA2F4
		public override PrefixOrigin PrefixOrigin
		{
			get
			{
				return this.info.PrefixOrigin;
			}
		}

		// Token: 0x17000D61 RID: 3425
		// (get) Token: 0x06003B50 RID: 15184 RVA: 0x000CC101 File Offset: 0x000CA301
		public override SuffixOrigin SuffixOrigin
		{
			get
			{
				return this.info.SuffixOrigin;
			}
		}

		// Token: 0x06003B51 RID: 15185 RVA: 0x000CC110 File Offset: 0x000CA310
		private static IPAddress PrefixLengthToSubnetMask(byte prefixLength, AddressFamily family)
		{
			byte[] array;
			if (family == AddressFamily.InterNetwork)
			{
				array = new byte[4];
			}
			else
			{
				array = new byte[16];
			}
			for (int i = 0; i < (int)prefixLength; i++)
			{
				byte[] array2 = array;
				int num = i / 8;
				array2[num] |= (byte)(128 >> i % 8);
			}
			return new IPAddress(array);
		}

		// Token: 0x04002383 RID: 9091
		private Win32_IP_ADAPTER_UNICAST_ADDRESS info;

		// Token: 0x04002384 RID: 9092
		private IPAddress ipv4Mask;
	}
}
