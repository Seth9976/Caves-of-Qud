using System;

namespace UnityEngine.Categorization
{
	// Token: 0x02000361 RID: 865
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	public sealed class CategoryInfoAttribute : Attribute
	{
		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x060028B0 RID: 10416 RVA: 0x0004E8B9 File Offset: 0x0004CAB9
		// (set) Token: 0x060028B1 RID: 10417 RVA: 0x0004E8C1 File Offset: 0x0004CAC1
		public int Order { get; set; } = int.MaxValue;

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x060028B2 RID: 10418 RVA: 0x0004E8CA File Offset: 0x0004CACA
		// (set) Token: 0x060028B3 RID: 10419 RVA: 0x0004E8D2 File Offset: 0x0004CAD2
		public string Name { get; set; } = null;
	}
}
