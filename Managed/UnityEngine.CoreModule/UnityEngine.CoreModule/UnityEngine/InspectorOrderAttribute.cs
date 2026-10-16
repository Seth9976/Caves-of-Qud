using System;

namespace UnityEngine
{
	// Token: 0x0200029B RID: 667
	[AttributeUsage(AttributeTargets.Enum)]
	public sealed class InspectorOrderAttribute : PropertyAttribute
	{
		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06002130 RID: 8496 RVA: 0x000406C7 File Offset: 0x0003E8C7
		// (set) Token: 0x06002131 RID: 8497 RVA: 0x000406CF File Offset: 0x0003E8CF
		internal InspectorSort m_inspectorSort { get; private set; }

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06002132 RID: 8498 RVA: 0x000406D8 File Offset: 0x0003E8D8
		// (set) Token: 0x06002133 RID: 8499 RVA: 0x000406E0 File Offset: 0x0003E8E0
		internal InspectorSortDirection m_sortDirection { get; private set; }

		// Token: 0x06002134 RID: 8500 RVA: 0x000406E9 File Offset: 0x0003E8E9
		public InspectorOrderAttribute(InspectorSort inspectorSort = InspectorSort.ByName, InspectorSortDirection sortDirection = InspectorSortDirection.Ascending)
		{
			this.m_inspectorSort = inspectorSort;
			this.m_sortDirection = sortDirection;
		}
	}
}
