using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.UIElements
{
	// Token: 0x0200058F RID: 1423
	public class UxmlEnumeration : UxmlTypeRestriction
	{
		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x06002A92 RID: 10898 RVA: 0x00098F0C File Offset: 0x0009710C
		// (set) Token: 0x06002A93 RID: 10899 RVA: 0x00098F24 File Offset: 0x00097124
		public IEnumerable<string> values
		{
			get
			{
				return this.m_Values;
			}
			set
			{
				this.m_Values = value.ToList<string>();
			}
		}

		// Token: 0x06002A94 RID: 10900 RVA: 0x00098F34 File Offset: 0x00097134
		public override bool Equals(UxmlTypeRestriction other)
		{
			UxmlEnumeration uxmlEnumeration = other as UxmlEnumeration;
			bool flag = uxmlEnumeration == null;
			return !flag && this.values.All<string>(new Func<string, bool>(uxmlEnumeration.values.Contains<string>)) && this.values.Count<string>() == uxmlEnumeration.values.Count<string>();
		}

		// Token: 0x04001316 RID: 4886
		private List<string> m_Values = new List<string>();
	}
}
