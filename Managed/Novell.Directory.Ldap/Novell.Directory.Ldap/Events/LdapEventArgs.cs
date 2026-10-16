using System;
using System.Text;

namespace Novell.Directory.Ldap.Events
{
	// Token: 0x020000A6 RID: 166
	public class LdapEventArgs : DirectoryEventArgs
	{
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x00013EA9 File Offset: 0x000120A9
		// (set) Token: 0x06000437 RID: 1079 RVA: 0x00013EB1 File Offset: 0x000120B1
		public LdapEventType EventType
		{
			get
			{
				return this.eType;
			}
			set
			{
				this.eType = value;
			}
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00013EBA File Offset: 0x000120BA
		public LdapEventArgs(LdapMessage sourceMessage, EventClassifiers aClassification, LdapEventType aType)
			: base(sourceMessage, aClassification)
		{
			this.eType = aType;
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00013ECC File Offset: 0x000120CC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[");
			stringBuilder.AppendFormat("{0}:", base.GetType());
			stringBuilder.AppendFormat("(Classification={0})", this.eClassification);
			stringBuilder.AppendFormat("(Type={0})", this.eType);
			stringBuilder.AppendFormat("(EventInformation:{0})", this.ldap_message);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x04000305 RID: 773
		protected LdapEventType eType;
	}
}
