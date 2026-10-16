using System;

namespace AiUnity.Common.Log
{
	// Token: 0x020002D5 RID: 725
	public interface IVariablesContext
	{
		// Token: 0x060018F0 RID: 6384
		void Set(string key, object value);

		// Token: 0x060018F1 RID: 6385
		object Get(string key);

		// Token: 0x060018F2 RID: 6386
		bool Contains(string key);

		// Token: 0x060018F3 RID: 6387
		void Remove(string key);

		// Token: 0x060018F4 RID: 6388
		void Clear();
	}
}
