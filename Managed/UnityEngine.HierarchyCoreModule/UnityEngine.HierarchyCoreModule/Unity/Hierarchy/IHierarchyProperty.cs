using System;

namespace Unity.Hierarchy
{
	// Token: 0x02000018 RID: 24
	public interface IHierarchyProperty<T>
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000089 RID: 137
		bool IsCreated { get; }

		// Token: 0x0600008A RID: 138
		T GetValue(in HierarchyNode node);

		// Token: 0x0600008B RID: 139
		void SetValue(in HierarchyNode node, T value);

		// Token: 0x0600008C RID: 140
		void ClearValue(in HierarchyNode node);
	}
}
