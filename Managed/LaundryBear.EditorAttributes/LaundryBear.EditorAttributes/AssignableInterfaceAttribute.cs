using System;
using UnityEngine;

namespace LaundryBear.EditorAttributes
{
	// Token: 0x02000003 RID: 3
	[AttributeUsage(AttributeTargets.Field)]
	public class AssignableInterfaceAttribute : PropertyAttribute
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public Type Type
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020C6 File Offset: 0x000002C6
		public AssignableInterfaceAttribute()
		{
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020CE File Offset: 0x000002CE
		public AssignableInterfaceAttribute(Type type)
		{
			this.m_type = type;
		}

		// Token: 0x04000001 RID: 1
		private Type m_type;
	}
}
