using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000565 RID: 1381
	internal class UxmlAttributeBindingPathAttribute : Attribute
	{
		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x060029DB RID: 10715 RVA: 0x000970BB File Offset: 0x000952BB
		// (set) Token: 0x060029DC RID: 10716 RVA: 0x000970C3 File Offset: 0x000952C3
		public string path { get; private set; }

		// Token: 0x060029DD RID: 10717 RVA: 0x000970CC File Offset: 0x000952CC
		public UxmlAttributeBindingPathAttribute(string bindingPath)
		{
			this.path = bindingPath;
		}
	}
}
