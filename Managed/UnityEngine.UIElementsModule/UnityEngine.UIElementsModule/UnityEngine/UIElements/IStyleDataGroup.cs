using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200044B RID: 1099
	internal interface IStyleDataGroup<T>
	{
		// Token: 0x06002227 RID: 8743
		T Copy();

		// Token: 0x06002228 RID: 8744
		void CopyFrom(ref T other);
	}
}
