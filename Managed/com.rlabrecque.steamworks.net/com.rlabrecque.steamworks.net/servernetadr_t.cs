using System;

namespace Steamworks
{
	// Token: 0x02000197 RID: 407
	[Serializable]
	public struct servernetadr_t
	{
		// Token: 0x06000960 RID: 2400 RVA: 0x0000EAEF File Offset: 0x0000CCEF
		public void Init(uint ip, ushort usQueryPort, ushort usConnectionPort)
		{
			this.m_unIP = ip;
			this.m_usQueryPort = usQueryPort;
			this.m_usConnectionPort = usConnectionPort;
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x0000EB06 File Offset: 0x0000CD06
		public ushort GetQueryPort()
		{
			return this.m_usQueryPort;
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x0000EB0E File Offset: 0x0000CD0E
		public void SetQueryPort(ushort usPort)
		{
			this.m_usQueryPort = usPort;
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x0000EB17 File Offset: 0x0000CD17
		public ushort GetConnectionPort()
		{
			return this.m_usConnectionPort;
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x0000EB1F File Offset: 0x0000CD1F
		public void SetConnectionPort(ushort usPort)
		{
			this.m_usConnectionPort = usPort;
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x0000EB28 File Offset: 0x0000CD28
		public uint GetIP()
		{
			return this.m_unIP;
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x0000EB30 File Offset: 0x0000CD30
		public void SetIP(uint unIP)
		{
			this.m_unIP = unIP;
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x0000EB39 File Offset: 0x0000CD39
		public string GetConnectionAddressString()
		{
			return servernetadr_t.ToString(this.m_unIP, this.m_usConnectionPort);
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x0000EB4C File Offset: 0x0000CD4C
		public string GetQueryAddressString()
		{
			return servernetadr_t.ToString(this.m_unIP, this.m_usQueryPort);
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x0000EB60 File Offset: 0x0000CD60
		public static string ToString(uint unIP, ushort usPort)
		{
			return string.Format("{0}.{1}.{2}.{3}:{4}", new object[]
			{
				(ulong)(unIP >> 24) & 255UL,
				(ulong)(unIP >> 16) & 255UL,
				(ulong)(unIP >> 8) & 255UL,
				(ulong)unIP & 255UL,
				usPort
			});
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0000EBD2 File Offset: 0x0000CDD2
		public static bool operator <(servernetadr_t x, servernetadr_t y)
		{
			return x.m_unIP < y.m_unIP || (x.m_unIP == y.m_unIP && x.m_usQueryPort < y.m_usQueryPort);
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x0000EC02 File Offset: 0x0000CE02
		public static bool operator >(servernetadr_t x, servernetadr_t y)
		{
			return x.m_unIP > y.m_unIP || (x.m_unIP == y.m_unIP && x.m_usQueryPort > y.m_usQueryPort);
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x0000EC32 File Offset: 0x0000CE32
		public override bool Equals(object other)
		{
			return other is servernetadr_t && this == (servernetadr_t)other;
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x0000EC4F File Offset: 0x0000CE4F
		public override int GetHashCode()
		{
			return this.m_unIP.GetHashCode() + this.m_usQueryPort.GetHashCode() + this.m_usConnectionPort.GetHashCode();
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x0000EC74 File Offset: 0x0000CE74
		public static bool operator ==(servernetadr_t x, servernetadr_t y)
		{
			return x.m_unIP == y.m_unIP && x.m_usQueryPort == y.m_usQueryPort && x.m_usConnectionPort == y.m_usConnectionPort;
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x0000ECA2 File Offset: 0x0000CEA2
		public static bool operator !=(servernetadr_t x, servernetadr_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x0000ECAE File Offset: 0x0000CEAE
		public bool Equals(servernetadr_t other)
		{
			return this.m_unIP == other.m_unIP && this.m_usQueryPort == other.m_usQueryPort && this.m_usConnectionPort == other.m_usConnectionPort;
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x0000ECDC File Offset: 0x0000CEDC
		public int CompareTo(servernetadr_t other)
		{
			return this.m_unIP.CompareTo(other.m_unIP) + this.m_usQueryPort.CompareTo(other.m_usQueryPort) + this.m_usConnectionPort.CompareTo(other.m_usConnectionPort);
		}

		// Token: 0x04000AAF RID: 2735
		private ushort m_usConnectionPort;

		// Token: 0x04000AB0 RID: 2736
		private ushort m_usQueryPort;

		// Token: 0x04000AB1 RID: 2737
		private uint m_unIP;
	}
}
