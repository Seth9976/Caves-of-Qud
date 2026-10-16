using System;
using UnityEngine;

namespace LaundryBear.EditorAttributes
{
	// Token: 0x02000007 RID: 7
	public class EditableObjectListAttribute : PropertyAttribute
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020F5 File Offset: 0x000002F5
		public string[] DisplayNames
		{
			get
			{
				return this.m_displayNames;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000020FD File Offset: 0x000002FD
		public Func<ScriptableObject>[] FactoryMethods
		{
			get
			{
				return this.m_factoryMethods;
			}
		}

		// Token: 0x04000002 RID: 2
		private string[] m_displayNames;

		// Token: 0x04000003 RID: 3
		private Func<ScriptableObject>[] m_factoryMethods;
	}
}
