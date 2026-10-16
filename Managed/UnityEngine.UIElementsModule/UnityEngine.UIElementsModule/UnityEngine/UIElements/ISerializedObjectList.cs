using System;
using System.Collections;

namespace UnityEngine.UIElements
{
	// Token: 0x02000069 RID: 105
	internal interface ISerializedObjectList : IList, ICollection, IEnumerable
	{
		// Token: 0x060003CA RID: 970
		void ApplyChanges();

		// Token: 0x060003CB RID: 971
		void RemoveAt(int index, int listCount);

		// Token: 0x060003CC RID: 972
		void Move(int srcIndex, int destIndex);

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060003CD RID: 973
		int minArraySize { get; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060003CE RID: 974
		// (set) Token: 0x060003CF RID: 975
		int arraySize { get; set; }
	}
}
