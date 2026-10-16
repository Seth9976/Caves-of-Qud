using System;

namespace System.Web.UI
{
	/// <summary>Specifies the type of resource referenced by a parsed virtual path.</summary>
	// Token: 0x02000248 RID: 584
	public enum VirtualReferenceType
	{
		/// <summary>The parsed virtual path references an ASP.NET page.</summary>
		// Token: 0x04001605 RID: 5637
		Page,
		/// <summary>The parsed virtual path references an ASP.NET user control.</summary>
		// Token: 0x04001606 RID: 5638
		UserControl,
		/// <summary>The parsed virtual path references a master page file.</summary>
		// Token: 0x04001607 RID: 5639
		Master,
		/// <summary>The parsed virtual path references a code file that is compiled using a specific language compiler.</summary>
		// Token: 0x04001608 RID: 5640
		SourceFile,
		/// <summary>The parsed virtual path references a resource that is not an ASP.NET page, master page, user control, or language-specific code file.</summary>
		// Token: 0x04001609 RID: 5641
		Other
	}
}
