using System;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000021 RID: 33
	public class LdapExtendedResponse : LdapResponse
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00007AA4 File Offset: 0x00005CA4
		public virtual string ID
		{
			get
			{
				RfcLdapOID responseName = ((RfcExtendedResponse)this.message.Response).ResponseName;
				if (responseName == null)
				{
					return null;
				}
				return responseName.stringValue();
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00007ADE File Offset: 0x00005CDE
		public static RespExtensionSet RegisteredResponses
		{
			get
			{
				return LdapExtendedResponse.registeredResponses;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00007AE8 File Offset: 0x00005CE8
		[CLSCompliant(false)]
		public virtual sbyte[] Value
		{
			get
			{
				Asn1OctetString response = ((RfcExtendedResponse)this.message.Response).Response;
				if (response == null)
				{
					return null;
				}
				return response.byteValue();
			}
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00007B16 File Offset: 0x00005D16
		public LdapExtendedResponse(RfcLdapMessage message)
			: base(message)
		{
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00007B1F File Offset: 0x00005D1F
		public static void register(string oid, Type extendedResponseClass)
		{
			LdapExtendedResponse.registeredResponses.registerResponseExtension(oid, extendedResponseClass);
		}

		// Token: 0x040000FD RID: 253
		private static RespExtensionSet registeredResponses = new RespExtensionSet();
	}
}
