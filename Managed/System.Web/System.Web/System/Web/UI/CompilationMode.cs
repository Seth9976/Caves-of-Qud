using System;

namespace System.Web.UI
{
	/// <summary>Defines constants that specify how ASP.NET should compile .aspx pages and .ascx controls.</summary>
	// Token: 0x0200018A RID: 394
	public enum CompilationMode
	{
		/// <summary>ASP.NET will not compile the page, if possible.</summary>
		// Token: 0x04001313 RID: 4883
		Auto,
		/// <summary>The page or control should never be dynamically compiled.</summary>
		// Token: 0x04001314 RID: 4884
		Never,
		/// <summary>The page should always be compiled.</summary>
		// Token: 0x04001315 RID: 4885
		Always
	}
}
