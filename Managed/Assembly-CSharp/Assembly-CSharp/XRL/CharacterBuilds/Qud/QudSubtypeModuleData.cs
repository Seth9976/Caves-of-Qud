using System;

namespace XRL.CharacterBuilds.Qud
{
	// Token: 0x020005B0 RID: 1456
	public class QudSubtypeModuleData : AbstractEmbarkBuilderModuleData
	{
		// Token: 0x06003E06 RID: 15878 RVA: 0x00130C8F File Offset: 0x0012EE8F
		public QudSubtypeModuleData()
		{
		}

		// Token: 0x06003E07 RID: 15879 RVA: 0x00130C97 File Offset: 0x0012EE97
		public QudSubtypeModuleData(string subtype)
		{
			this.Subtype = subtype;
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06003E08 RID: 15880 RVA: 0x00130CA6 File Offset: 0x0012EEA6
		[Obsolete]
		public SubtypeEntry info
		{
			get
			{
				return SubtypeFactory.GetSubtypeEntry(this.Subtype);
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06003E09 RID: 15881 RVA: 0x00130CB3 File Offset: 0x0012EEB3
		public SubtypeEntry Entry
		{
			get
			{
				return SubtypeFactory.GetSubtypeEntry(this.Subtype);
			}
		}

		// Token: 0x04001EBB RID: 7867
		public string Subtype;
	}
}
