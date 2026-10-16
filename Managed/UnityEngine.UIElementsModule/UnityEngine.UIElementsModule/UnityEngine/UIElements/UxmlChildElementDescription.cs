using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200056A RID: 1386
	public class UxmlChildElementDescription
	{
		// Token: 0x060029E5 RID: 10725 RVA: 0x00097154 File Offset: 0x00095354
		public UxmlChildElementDescription(Type t)
		{
			bool flag = t == null;
			if (flag)
			{
				throw new ArgumentNullException("t");
			}
			this.elementName = t.Name;
			this.elementNamespace = t.Namespace;
		}

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x060029E6 RID: 10726 RVA: 0x0009719A File Offset: 0x0009539A
		// (set) Token: 0x060029E7 RID: 10727 RVA: 0x000971A2 File Offset: 0x000953A2
		public string elementName { get; protected set; }

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x060029E8 RID: 10728 RVA: 0x000971AB File Offset: 0x000953AB
		// (set) Token: 0x060029E9 RID: 10729 RVA: 0x000971B3 File Offset: 0x000953B3
		public string elementNamespace { get; protected set; }
	}
}
