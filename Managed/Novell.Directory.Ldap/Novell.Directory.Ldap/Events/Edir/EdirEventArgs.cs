using System;

namespace Novell.Directory.Ldap.Events.Edir
{
	// Token: 0x020000AE RID: 174
	public class EdirEventArgs : DirectoryEventArgs
	{
		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x00014694 File Offset: 0x00012894
		public EdirEventIntermediateResponse IntermediateResponse
		{
			get
			{
				if (this.ldap_message is EdirEventIntermediateResponse)
				{
					return (EdirEventIntermediateResponse)this.ldap_message;
				}
				return null;
			}
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x000146B0 File Offset: 0x000128B0
		public EdirEventArgs(LdapMessage sourceMessage, EventClassifiers aClassification)
			: base(sourceMessage, aClassification)
		{
		}
	}
}
