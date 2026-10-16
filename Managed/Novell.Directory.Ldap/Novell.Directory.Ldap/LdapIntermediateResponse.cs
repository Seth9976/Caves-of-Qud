using System;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000022 RID: 34
	public class LdapIntermediateResponse : LdapResponse
	{
		// Token: 0x0600017D RID: 381 RVA: 0x00007B2D File Offset: 0x00005D2D
		public static void register(string oid, Type extendedResponseClass)
		{
			LdapIntermediateResponse.registeredResponses.registerResponseExtension(oid, extendedResponseClass);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00007B3B File Offset: 0x00005D3B
		public static RespExtensionSet getRegisteredResponses()
		{
			return LdapIntermediateResponse.registeredResponses;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00007B42 File Offset: 0x00005D42
		public LdapIntermediateResponse(RfcLdapMessage message)
			: base(message)
		{
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00007B4C File Offset: 0x00005D4C
		public string getID()
		{
			RfcLdapOID responseName = ((RfcIntermediateResponse)this.message.Response).getResponseName();
			if (responseName == null)
			{
				return null;
			}
			return responseName.stringValue();
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00007B7C File Offset: 0x00005D7C
		[CLSCompliant(false)]
		public sbyte[] getValue()
		{
			Asn1OctetString response = ((RfcIntermediateResponse)this.message.Response).getResponse();
			if (response == null)
			{
				return null;
			}
			return response.byteValue();
		}

		// Token: 0x040000FE RID: 254
		private static RespExtensionSet registeredResponses = new RespExtensionSet();
	}
}
