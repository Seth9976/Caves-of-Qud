using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200058E RID: 1422
	public class UxmlValueBounds : UxmlTypeRestriction
	{
		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x06002A88 RID: 10888 RVA: 0x00098E5C File Offset: 0x0009705C
		// (set) Token: 0x06002A89 RID: 10889 RVA: 0x00098E64 File Offset: 0x00097064
		public string min { get; set; }

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x06002A8A RID: 10890 RVA: 0x00098E6D File Offset: 0x0009706D
		// (set) Token: 0x06002A8B RID: 10891 RVA: 0x00098E75 File Offset: 0x00097075
		public string max { get; set; }

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x06002A8C RID: 10892 RVA: 0x00098E7E File Offset: 0x0009707E
		// (set) Token: 0x06002A8D RID: 10893 RVA: 0x00098E86 File Offset: 0x00097086
		public bool excludeMin { get; set; }

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x06002A8E RID: 10894 RVA: 0x00098E8F File Offset: 0x0009708F
		// (set) Token: 0x06002A8F RID: 10895 RVA: 0x00098E97 File Offset: 0x00097097
		public bool excludeMax { get; set; }

		// Token: 0x06002A90 RID: 10896 RVA: 0x00098EA0 File Offset: 0x000970A0
		public override bool Equals(UxmlTypeRestriction other)
		{
			UxmlValueBounds uxmlValueBounds = other as UxmlValueBounds;
			bool flag = uxmlValueBounds == null;
			return !flag && (this.min == uxmlValueBounds.min && this.max == uxmlValueBounds.max && this.excludeMin == uxmlValueBounds.excludeMin) && this.excludeMax == uxmlValueBounds.excludeMax;
		}
	}
}
