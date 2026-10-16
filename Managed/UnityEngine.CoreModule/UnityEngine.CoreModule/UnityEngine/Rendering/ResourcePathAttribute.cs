using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020004D3 RID: 1235
	public sealed class ResourcePathAttribute : ResourcePathsBaseAttribute
	{
		// Token: 0x06002E4A RID: 11850 RVA: 0x00057B9E File Offset: 0x00055D9E
		public ResourcePathAttribute(string path, SearchType location = SearchType.ProjectPath)
			: base(null, true, location)
		{
		}
	}
}
