using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000095 RID: 149
	public class TreeViewExpansionChangedArgs
	{
		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x0001B495 File Offset: 0x00019695
		// (set) Token: 0x060005B7 RID: 1463 RVA: 0x0001B49D File Offset: 0x0001969D
		public int id { get; set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x0001B4A6 File Offset: 0x000196A6
		// (set) Token: 0x060005B9 RID: 1465 RVA: 0x0001B4AE File Offset: 0x000196AE
		public bool isExpanded { get; set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x0001B4B7 File Offset: 0x000196B7
		// (set) Token: 0x060005BB RID: 1467 RVA: 0x0001B4BF File Offset: 0x000196BF
		public bool isAppliedToAllChildren { get; set; }
	}
}
