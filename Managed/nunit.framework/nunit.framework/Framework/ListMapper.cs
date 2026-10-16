using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace NUnit.Framework
{
	// Token: 0x02000058 RID: 88
	public class ListMapper
	{
		// Token: 0x060002CE RID: 718 RVA: 0x00006A8F File Offset: 0x00004C8F
		public ListMapper(ICollection original)
		{
			this.original = original;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00006AA0 File Offset: 0x00004CA0
		public ICollection Property(string name)
		{
			List<object> list = new List<object>();
			foreach (object obj in this.original)
			{
				PropertyInfo property = obj.GetType().GetProperty(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				if (property == null)
				{
					throw new ArgumentException(string.Format("{0} does not have a {1} property", obj, name));
				}
				list.Add(property.GetValue(obj, null));
			}
			return list;
		}

		// Token: 0x0400005C RID: 92
		private ICollection original;
	}
}
