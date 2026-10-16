using System;
using System.Collections.Generic;

namespace LitJson
{
	// Token: 0x02000320 RID: 800
	internal struct ObjectMetadata
	{
		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06001DA7 RID: 7591 RVA: 0x00091260 File Offset: 0x0008F460
		// (set) Token: 0x06001DA8 RID: 7592 RVA: 0x00091281 File Offset: 0x0008F481
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

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06001DA9 RID: 7593 RVA: 0x0009128A File Offset: 0x0008F48A
		// (set) Token: 0x06001DAA RID: 7594 RVA: 0x00091292 File Offset: 0x0008F492
		public bool IsDictionary
		{
			get
			{
				return this.is_dictionary;
			}
			set
			{
				this.is_dictionary = value;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06001DAB RID: 7595 RVA: 0x0009129B File Offset: 0x0008F49B
		// (set) Token: 0x06001DAC RID: 7596 RVA: 0x000912A3 File Offset: 0x0008F4A3
		public IDictionary<string, PropertyMetadata> Properties
		{
			get
			{
				return this.properties;
			}
			set
			{
				this.properties = value;
			}
		}

		// Token: 0x04000F89 RID: 3977
		private Type element_type;

		// Token: 0x04000F8A RID: 3978
		private bool is_dictionary;

		// Token: 0x04000F8B RID: 3979
		private IDictionary<string, PropertyMetadata> properties;
	}
}
