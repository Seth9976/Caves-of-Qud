using System;

namespace System.Resources
{
	// Token: 0x02000873 RID: 2163
	internal class NameOrId
	{
		// Token: 0x06004810 RID: 18448 RVA: 0x000ED03F File Offset: 0x000EB23F
		public NameOrId(string name)
		{
			this.name = name;
		}

		// Token: 0x06004811 RID: 18449 RVA: 0x000ED04E File Offset: 0x000EB24E
		public NameOrId(int id)
		{
			this.id = id;
		}

		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x06004812 RID: 18450 RVA: 0x000ED05D File Offset: 0x000EB25D
		public bool IsName
		{
			get
			{
				return this.name != null;
			}
		}

		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x06004813 RID: 18451 RVA: 0x000ED068 File Offset: 0x000EB268
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x06004814 RID: 18452 RVA: 0x000ED070 File Offset: 0x000EB270
		public int Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x06004815 RID: 18453 RVA: 0x000ED078 File Offset: 0x000EB278
		public override string ToString()
		{
			if (this.name != null)
			{
				return "Name(" + this.name + ")";
			}
			return "Id(" + this.id.ToString() + ")";
		}

		// Token: 0x04002E28 RID: 11816
		private string name;

		// Token: 0x04002E29 RID: 11817
		private int id;
	}
}
