using System;

namespace UnityEngine.Categorization
{
	// Token: 0x02000360 RID: 864
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	public sealed class ElementInfoAttribute : Attribute
	{
		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x060028AB RID: 10411 RVA: 0x0004E87C File Offset: 0x0004CA7C
		// (set) Token: 0x060028AC RID: 10412 RVA: 0x0004E884 File Offset: 0x0004CA84
		public int Order { get; set; } = int.MaxValue;

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x060028AD RID: 10413 RVA: 0x0004E88D File Offset: 0x0004CA8D
		// (set) Token: 0x060028AE RID: 10414 RVA: 0x0004E895 File Offset: 0x0004CA95
		public string Name { get; set; } = null;
	}
}
