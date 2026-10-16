using System;

namespace Mono.Net.Dns
{
	// Token: 0x020000C0 RID: 192
	internal class DnsResourceRecord
	{
		// Token: 0x060003BB RID: 955 RVA: 0x0000219B File Offset: 0x0000039B
		internal DnsResourceRecord()
		{
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000B370 File Offset: 0x00009570
		internal void CopyFrom(DnsResourceRecord rr)
		{
			this.name = rr.name;
			this.type = rr.type;
			this.klass = rr.klass;
			this.ttl = rr.ttl;
			this.rdlength = rr.rdlength;
			this.m_rdata = rr.m_rdata;
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000B3C8 File Offset: 0x000095C8
		internal static DnsResourceRecord CreateFromBuffer(DnsPacket packet, int size, ref int offset)
		{
			string text = packet.ReadName(ref offset);
			DnsType dnsType = (DnsType)packet.ReadUInt16(ref offset);
			DnsClass dnsClass = (DnsClass)packet.ReadUInt16(ref offset);
			int num = packet.ReadInt32(ref offset);
			ushort num2 = packet.ReadUInt16(ref offset);
			DnsResourceRecord dnsResourceRecord = new DnsResourceRecord();
			dnsResourceRecord.name = text;
			dnsResourceRecord.type = dnsType;
			dnsResourceRecord.klass = dnsClass;
			dnsResourceRecord.ttl = num;
			dnsResourceRecord.rdlength = num2;
			dnsResourceRecord.m_rdata = new ArraySegment<byte>(packet.Packet, offset, (int)num2);
			offset += (int)num2;
			if (dnsClass == DnsClass.Internet)
			{
				if (dnsType <= DnsType.CNAME)
				{
					if (dnsType != DnsType.A)
					{
						if (dnsType == DnsType.CNAME)
						{
							dnsResourceRecord = new DnsResourceRecordCName(dnsResourceRecord);
						}
					}
					else
					{
						dnsResourceRecord = new DnsResourceRecordA(dnsResourceRecord);
					}
				}
				else if (dnsType != DnsType.PTR)
				{
					if (dnsType == DnsType.AAAA)
					{
						dnsResourceRecord = new DnsResourceRecordAAAA(dnsResourceRecord);
					}
				}
				else
				{
					dnsResourceRecord = new DnsResourceRecordPTR(dnsResourceRecord);
				}
			}
			return dnsResourceRecord;
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060003BE RID: 958 RVA: 0x0000B495 File Offset: 0x00009695
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060003BF RID: 959 RVA: 0x0000B49D File Offset: 0x0000969D
		public DnsType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x0000B4A5 File Offset: 0x000096A5
		public DnsClass Class
		{
			get
			{
				return this.klass;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x0000B4AD File Offset: 0x000096AD
		public int Ttl
		{
			get
			{
				return this.ttl;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x0000B4B5 File Offset: 0x000096B5
		public ArraySegment<byte> Data
		{
			get
			{
				return this.m_rdata;
			}
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000B4C0 File Offset: 0x000096C0
		public override string ToString()
		{
			return string.Format("Name: {0}, Type: {1}, Class: {2}, Ttl: {3}, Data length: {4}", new object[]
			{
				this.name,
				this.type,
				this.klass,
				this.ttl,
				this.Data.Count
			});
		}

		// Token: 0x0400033B RID: 827
		private string name;

		// Token: 0x0400033C RID: 828
		private DnsType type;

		// Token: 0x0400033D RID: 829
		private DnsClass klass;

		// Token: 0x0400033E RID: 830
		private int ttl;

		// Token: 0x0400033F RID: 831
		private ushort rdlength;

		// Token: 0x04000340 RID: 832
		private ArraySegment<byte> m_rdata;
	}
}
