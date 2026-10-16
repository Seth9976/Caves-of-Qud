using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200058D RID: 1421
	public class UxmlValueMatches : UxmlTypeRestriction
	{
		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x06002A84 RID: 10884 RVA: 0x00098E0A File Offset: 0x0009700A
		// (set) Token: 0x06002A85 RID: 10885 RVA: 0x00098E12 File Offset: 0x00097012
		public string regex { get; set; }

		// Token: 0x06002A86 RID: 10886 RVA: 0x00098E1C File Offset: 0x0009701C
		public override bool Equals(UxmlTypeRestriction other)
		{
			UxmlValueMatches uxmlValueMatches = other as UxmlValueMatches;
			bool flag = uxmlValueMatches == null;
			return !flag && this.regex == uxmlValueMatches.regex;
		}
	}
}
