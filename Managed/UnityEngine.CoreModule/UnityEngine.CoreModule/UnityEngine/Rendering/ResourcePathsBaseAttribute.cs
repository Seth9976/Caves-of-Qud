using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020004D2 RID: 1234
	[AttributeUsage(AttributeTargets.Field, Inherited = true)]
	public abstract class ResourcePathsBaseAttribute : Attribute
	{
		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06002E46 RID: 11846 RVA: 0x00057B40 File Offset: 0x00055D40
		public SearchType location
		{
			get
			{
				Debug.LogWarning("ResourcePathsBaseAttribute.location cannot be reliable at runtime as data is not stored.");
				return SearchType.ProjectPath;
			}
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06002E47 RID: 11847 RVA: 0x00057B60 File Offset: 0x00055D60
		public string[] paths
		{
			get
			{
				Debug.LogWarning("ResourcePathsBaseAttribute.paths cannot be reliable at runtime as data is not stored.");
				return null;
			}
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06002E48 RID: 11848 RVA: 0x00057B80 File Offset: 0x00055D80
		public bool isField
		{
			get
			{
				Debug.LogWarning("ResourcePathsBaseAttribute.isField cannot be reliable at runtime as data is not stored.");
				return false;
			}
		}

		// Token: 0x06002E49 RID: 11849 RVA: 0x00002082 File Offset: 0x00000282
		protected ResourcePathsBaseAttribute(string[] paths, bool isField, SearchType location)
		{
		}
	}
}
