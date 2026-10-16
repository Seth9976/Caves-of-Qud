using System;
using UnityEngine.Bindings;

namespace Unity.Hierarchy
{
	// Token: 0x02000013 RID: 19
	[VisibleToOtherModules(new string[] { "UnityEditor.HierarchyModule" })]
	internal interface IHierarchySearchQueryParser
	{
		// Token: 0x0600007A RID: 122
		HierarchySearchQueryDescriptor ParseQuery(string query);
	}
}
