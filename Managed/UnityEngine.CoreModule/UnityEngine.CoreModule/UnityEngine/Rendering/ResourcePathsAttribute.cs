using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020004D4 RID: 1236
	public sealed class ResourcePathsAttribute : ResourcePathsBaseAttribute
	{
		// Token: 0x06002E4B RID: 11851 RVA: 0x00057BAB File Offset: 0x00055DAB
		public ResourcePathsAttribute(string[] paths, SearchType location = SearchType.ProjectPath)
			: base(paths, false, location)
		{
		}
	}
}
