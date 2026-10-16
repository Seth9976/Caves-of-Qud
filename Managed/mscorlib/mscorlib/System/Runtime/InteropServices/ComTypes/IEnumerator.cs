using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020007A4 RID: 1956
	[Guid("496B0ABF-CDEE-11d3-88E8-00902754C43A")]
	internal interface IEnumerator
	{
		// Token: 0x06004511 RID: 17681
		bool MoveNext();

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x06004512 RID: 17682
		object Current { get; }

		// Token: 0x06004513 RID: 17683
		void Reset();
	}
}
