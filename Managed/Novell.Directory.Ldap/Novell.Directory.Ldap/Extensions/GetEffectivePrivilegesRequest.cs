using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x02000088 RID: 136
	public class GetEffectivePrivilegesRequest : LdapExtendedOperation
	{
		// Token: 0x060003FE RID: 1022 RVA: 0x00012820 File Offset: 0x00010A20
		static GetEffectivePrivilegesRequest()
		{
			try
			{
				LdapExtendedResponse.register("2.16.840.1.113719.1.27.100.34", Type.GetType("Novell.Directory.Ldap.Extensions.GetEffectivePrivilegesResponse"));
			}
			catch (Exception)
			{
				Console.Error.WriteLine("Could not register Extended Response - Class not found");
			}
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00012868 File Offset: 0x00010A68
		public GetEffectivePrivilegesRequest(string dn, string trusteeDN, string attrName)
			: base("2.16.840.1.113719.1.27.100.33", null)
		{
			try
			{
				if (dn == null)
				{
					throw new ArgumentException("PARAM_ERROR");
				}
				MemoryStream memoryStream = new MemoryStream();
				LBEREncoder lberencoder = new LBEREncoder();
				Asn1Object asn1Object = new Asn1OctetString(dn);
				Asn1OctetString asn1OctetString = new Asn1OctetString(trusteeDN);
				Asn1OctetString asn1OctetString2 = new Asn1OctetString(attrName);
				asn1Object.encode(lberencoder, memoryStream);
				asn1OctetString.encode(lberencoder, memoryStream);
				asn1OctetString2.encode(lberencoder, memoryStream);
				this.setValue(SupportClass.ToSByteArray(memoryStream.ToArray()));
			}
			catch (IOException)
			{
				throw new LdapException("ENCODING_ERROR", 83, null);
			}
		}
	}
}
