using System;

namespace System.Web.Compilation
{
	/// <summary>Represents an enumeration that specifies the target directory that a <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> object applies to.</summary>
	// Token: 0x02000601 RID: 1537
	[Flags]
	public enum FolderLevelBuildProviderAppliesTo
	{
		/// <summary>Specifies that a <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> object does not apply to any directory.</summary>
		// Token: 0x040023A9 RID: 9129
		None = 0,
		/// <summary>Specifies that a <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> object applies to a folder that contains code.</summary>
		// Token: 0x040023AA RID: 9130
		Code = 1,
		/// <summary>Specifies that a <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> object applies to the Web content directory.</summary>
		// Token: 0x040023AB RID: 9131
		WebReferences = 2,
		/// <summary>Specifies that a <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> object applies to the local resources directory.</summary>
		// Token: 0x040023AC RID: 9132
		LocalResources = 4,
		/// <summary>Specifies that a <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> object applies to the global resources directory.</summary>
		// Token: 0x040023AD RID: 9133
		GlobalResources = 8
	}
}
