using System;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x020000D7 RID: 215
	public abstract class Asn1Numeric : Asn1Object
	{
		// Token: 0x06000549 RID: 1353 RVA: 0x000172E3 File Offset: 0x000154E3
		internal Asn1Numeric(Asn1Identifier id, int value_Renamed)
			: base(id)
		{
			this.content = (long)value_Renamed;
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x000172F4 File Offset: 0x000154F4
		internal Asn1Numeric(Asn1Identifier id, long value_Renamed)
			: base(id)
		{
			this.content = value_Renamed;
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00017304 File Offset: 0x00015504
		public int intValue()
		{
			return (int)this.content;
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0001730D File Offset: 0x0001550D
		public long longValue()
		{
			return this.content;
		}

		// Token: 0x040004AD RID: 1197
		private long content;
	}
}
