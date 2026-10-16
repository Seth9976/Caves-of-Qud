using System;

namespace System.Web.Compilation
{
	/// <summary>Defines an attribute that specifies the scope where a <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> object should be applied when a resource is located. </summary>
	// Token: 0x02000602 RID: 1538
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class FolderLevelBuildProviderAppliesToAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.FolderLevelBuildProviderAppliesToAttribute" /> class.</summary>
		/// <param name="appliesTo">The target directory that a <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> object applies to.</param>
		// Token: 0x0600426E RID: 17006 RVA: 0x000AD1E3 File Offset: 0x000AB3E3
		public FolderLevelBuildProviderAppliesToAttribute(FolderLevelBuildProviderAppliesTo appliesTo)
		{
			this._appliesTo = appliesTo;
		}

		/// <summary>Gets or sets the target directory that a <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> object applies to.</summary>
		/// <returns>The directory that a <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> object applies to.</returns>
		// Token: 0x17001524 RID: 5412
		// (get) Token: 0x0600426F RID: 17007 RVA: 0x000AD1F2 File Offset: 0x000AB3F2
		public FolderLevelBuildProviderAppliesTo AppliesTo
		{
			get
			{
				return this._appliesTo;
			}
		}

		// Token: 0x040023AE RID: 9134
		private FolderLevelBuildProviderAppliesTo _appliesTo;
	}
}
