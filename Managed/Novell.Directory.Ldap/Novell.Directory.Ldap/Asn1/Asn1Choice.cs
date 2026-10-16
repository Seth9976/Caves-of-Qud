using System;
using System.IO;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x020000CF RID: 207
	public class Asn1Choice : Asn1Object
	{
		// Token: 0x17000170 RID: 368
		// (set) Token: 0x0600050F RID: 1295 RVA: 0x00016E2D File Offset: 0x0001502D
		[CLSCompliant(false)]
		protected internal virtual Asn1Object ChoiceValue
		{
			set
			{
				this.content = value;
			}
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00016E36 File Offset: 0x00015036
		public Asn1Choice(Asn1Object content)
			: base(null)
		{
			this.content = content;
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00016E46 File Offset: 0x00015046
		protected internal Asn1Choice()
			: base(null)
		{
			this.content = null;
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00016E56 File Offset: 0x00015056
		public override void encode(Asn1Encoder enc, Stream out_Renamed)
		{
			this.content.encode(enc, out_Renamed);
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00016E65 File Offset: 0x00015065
		public Asn1Object choiceValue()
		{
			return this.content;
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00016E6D File Offset: 0x0001506D
		public override Asn1Identifier getIdentifier()
		{
			return this.content.getIdentifier();
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00016E7A File Offset: 0x0001507A
		public override void setIdentifier(Asn1Identifier id)
		{
			this.content.setIdentifier(id);
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00016E88 File Offset: 0x00015088
		public override string ToString()
		{
			return this.content.ToString();
		}

		// Token: 0x0400049C RID: 1180
		private Asn1Object content;
	}
}
