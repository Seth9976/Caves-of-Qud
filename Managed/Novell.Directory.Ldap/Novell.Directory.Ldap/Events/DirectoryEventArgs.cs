using System;

namespace Novell.Directory.Ldap.Events
{
	// Token: 0x020000A4 RID: 164
	public class DirectoryEventArgs : BaseEventArgs
	{
		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x00013E70 File Offset: 0x00012070
		// (set) Token: 0x06000432 RID: 1074 RVA: 0x00013E78 File Offset: 0x00012078
		public EventClassifiers EventClassification
		{
			get
			{
				return this.eClassification;
			}
			set
			{
				this.eClassification = value;
			}
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00013E81 File Offset: 0x00012081
		public DirectoryEventArgs(LdapMessage sourceMessage, EventClassifiers aClassification)
			: base(sourceMessage)
		{
			this.eClassification = aClassification;
		}

		// Token: 0x04000303 RID: 771
		protected EventClassifiers eClassification;
	}
}
