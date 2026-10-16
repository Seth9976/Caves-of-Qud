using System;
using System.Collections;
using System.Collections.Generic;

namespace NUnit.Framework.Interfaces
{
	// Token: 0x020000CE RID: 206
	public interface IPropertyBag : IXmlNodeBuilder
	{
		// Token: 0x060006B3 RID: 1715
		void Add(string key, object value);

		// Token: 0x060006B4 RID: 1716
		void Set(string key, object value);

		// Token: 0x060006B5 RID: 1717
		object Get(string key);

		// Token: 0x060006B6 RID: 1718
		bool ContainsKey(string key);

		// Token: 0x17000159 RID: 345
		IList this[string key] { get; set; }

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060006B9 RID: 1721
		ICollection<string> Keys { get; }
	}
}
