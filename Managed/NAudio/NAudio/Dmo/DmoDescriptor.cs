using System;

namespace NAudio.Dmo
{
	// Token: 0x02000085 RID: 133
	public class DmoDescriptor
	{
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060002ED RID: 749 RVA: 0x00009F4A File Offset: 0x0000814A
		// (set) Token: 0x060002EE RID: 750 RVA: 0x00009F52 File Offset: 0x00008152
		public string Name { get; private set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060002EF RID: 751 RVA: 0x00009F5B File Offset: 0x0000815B
		// (set) Token: 0x060002F0 RID: 752 RVA: 0x00009F63 File Offset: 0x00008163
		public Guid Clsid { get; private set; }

		// Token: 0x060002F1 RID: 753 RVA: 0x00009F6C File Offset: 0x0000816C
		public DmoDescriptor(string name, Guid clsid)
		{
			this.Name = name;
			this.Clsid = clsid;
		}
	}
}
