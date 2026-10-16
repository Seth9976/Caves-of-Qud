using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x02000091 RID: 145
	public class ListReplicasRequest : LdapExtendedOperation
	{
		// Token: 0x06000419 RID: 1049 RVA: 0x000134FC File Offset: 0x000116FC
		static ListReplicasRequest()
		{
			try
			{
				LdapExtendedResponse.register("2.16.840.1.113719.1.27.100.20", Type.GetType("Novell.Directory.Ldap.Extensions.ListReplicasResponse"));
			}
			catch (Exception)
			{
				Console.Error.WriteLine("Could not register Extended Response - Class not found");
			}
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00013544 File Offset: 0x00011744
		public ListReplicasRequest(string serverName)
			: base("2.16.840.1.113719.1.27.100.19", null)
		{
			try
			{
				if (serverName == null)
				{
					throw new ArgumentException("PARAM_ERROR");
				}
				MemoryStream memoryStream = new MemoryStream();
				LBEREncoder lberencoder = new LBEREncoder();
				new Asn1OctetString(serverName).encode(lberencoder, memoryStream);
				this.setValue(SupportClass.ToSByteArray(memoryStream.ToArray()));
			}
			catch (IOException)
			{
				throw new LdapException("ENCODING_ERROR", 83, null);
			}
		}
	}
}
