using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000057 RID: 87
	public class RfcAttributeDescription : RfcLdapString
	{
		// Token: 0x06000330 RID: 816 RVA: 0x000102FD File Offset: 0x0000E4FD
		public RfcAttributeDescription(string s)
			: base(s)
		{
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00010306 File Offset: 0x0000E506
		[CLSCompliant(false)]
		public RfcAttributeDescription(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(dec, in_Renamed, len)
		{
		}
	}
}
