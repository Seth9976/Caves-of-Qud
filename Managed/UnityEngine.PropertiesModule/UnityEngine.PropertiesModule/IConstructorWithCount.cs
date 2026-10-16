using System;

namespace Unity.Properties
{
	// Token: 0x0200008F RID: 143
	internal interface IConstructorWithCount<out T> : IConstructor
	{
		// Token: 0x0600030A RID: 778
		T InstantiateWithCount(int count);
	}
}
