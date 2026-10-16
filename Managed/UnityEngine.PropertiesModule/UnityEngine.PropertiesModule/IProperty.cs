using System;
using System.Collections.Generic;

namespace Unity.Properties
{
	// Token: 0x02000024 RID: 36
	public interface IProperty
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000076 RID: 118
		string Name { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000077 RID: 119
		bool IsReadOnly { get; }

		// Token: 0x06000078 RID: 120
		Type DeclaredValueType();

		// Token: 0x06000079 RID: 121
		bool HasAttribute<TAttribute>() where TAttribute : Attribute;

		// Token: 0x0600007A RID: 122
		TAttribute GetAttribute<TAttribute>() where TAttribute : Attribute;

		// Token: 0x0600007B RID: 123
		IEnumerable<TAttribute> GetAttributes<TAttribute>() where TAttribute : Attribute;

		// Token: 0x0600007C RID: 124
		IEnumerable<Attribute> GetAttributes();
	}
}
