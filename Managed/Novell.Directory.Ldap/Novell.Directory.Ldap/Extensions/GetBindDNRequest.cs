using System;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x02000086 RID: 134
	public class GetBindDNRequest : LdapExtendedOperation
	{
		// Token: 0x060003FA RID: 1018 RVA: 0x00012738 File Offset: 0x00010938
		static GetBindDNRequest()
		{
			try
			{
				LdapExtendedResponse.register("2.16.840.1.113719.1.27.100.32", Type.GetType("Novell.Directory.Ldap.Extensions.GetBindDNResponse"));
			}
			catch (Exception)
			{
				Console.Error.WriteLine("Could not register Extended Response - Class not found");
			}
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00012780 File Offset: 0x00010980
		public GetBindDNRequest()
			: base("2.16.840.1.113719.1.27.100.31", null)
		{
		}
	}
}
