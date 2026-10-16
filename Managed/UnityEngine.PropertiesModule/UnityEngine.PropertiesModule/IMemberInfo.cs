using System;
using System.Collections.Generic;

namespace Unity.Properties
{
	// Token: 0x0200002D RID: 45
	internal interface IMemberInfo
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000D4 RID: 212
		string Name { get; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000D5 RID: 213
		bool IsReadOnly { get; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000D6 RID: 214
		Type ValueType { get; }

		// Token: 0x060000D7 RID: 215
		object GetValue(object obj);

		// Token: 0x060000D8 RID: 216
		void SetValue(object obj, object value);

		// Token: 0x060000D9 RID: 217
		IEnumerable<Attribute> GetCustomAttributes();
	}
}
