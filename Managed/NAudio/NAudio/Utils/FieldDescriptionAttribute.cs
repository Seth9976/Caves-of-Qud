using System;

namespace NAudio.Utils
{
	// Token: 0x02000046 RID: 70
	[AttributeUsage(256)]
	public class FieldDescriptionAttribute : Attribute
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00006DBD File Offset: 0x00004FBD
		// (set) Token: 0x06000129 RID: 297 RVA: 0x00006DC5 File Offset: 0x00004FC5
		public string Description { get; private set; }

		// Token: 0x0600012A RID: 298 RVA: 0x00006DCE File Offset: 0x00004FCE
		public FieldDescriptionAttribute(string description)
		{
			this.Description = description;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00006DDD File Offset: 0x00004FDD
		public override string ToString()
		{
			return this.Description;
		}
	}
}
