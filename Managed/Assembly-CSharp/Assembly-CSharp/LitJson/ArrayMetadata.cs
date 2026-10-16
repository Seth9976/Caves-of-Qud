using System;

namespace LitJson
{
	// Token: 0x0200031F RID: 799
	internal struct ArrayMetadata
	{
		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06001DA1 RID: 7585 RVA: 0x00091214 File Offset: 0x0008F414
		// (set) Token: 0x06001DA2 RID: 7586 RVA: 0x00091235 File Offset: 0x0008F435
		public Type ElementType
		{
			get
			{
				if (this.element_type == null)
				{
					return typeof(JsonData);
				}
				return this.element_type;
			}
			set
			{
				this.element_type = value;
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06001DA3 RID: 7587 RVA: 0x0009123E File Offset: 0x0008F43E
		// (set) Token: 0x06001DA4 RID: 7588 RVA: 0x00091246 File Offset: 0x0008F446
		public bool IsArray
		{
			get
			{
				return this.is_array;
			}
			set
			{
				this.is_array = value;
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06001DA5 RID: 7589 RVA: 0x0009124F File Offset: 0x0008F44F
		// (set) Token: 0x06001DA6 RID: 7590 RVA: 0x00091257 File Offset: 0x0008F457
		public bool IsList
		{
			get
			{
				return this.is_list;
			}
			set
			{
				this.is_list = value;
			}
		}

		// Token: 0x04000F86 RID: 3974
		private Type element_type;

		// Token: 0x04000F87 RID: 3975
		private bool is_array;

		// Token: 0x04000F88 RID: 3976
		private bool is_list;
	}
}
