using System;

namespace System.ComponentModel.Design
{
	/// <summary>Defines identifiers for a set of technologies that designer hosts support.</summary>
	// Token: 0x0200047C RID: 1148
	public enum ViewTechnology
	{
		/// <summary>Represents a mode in which the view object is passed directly to the development environment. </summary>
		// Token: 0x040014A7 RID: 5287
		[Obsolete("This value has been deprecated. Use ViewTechnology.Default instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		Passthrough,
		/// <summary>Represents a mode in which a Windows Forms control object provides the display for the root designer. </summary>
		// Token: 0x040014A8 RID: 5288
		[Obsolete("This value has been deprecated. Use ViewTechnology.Default instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		WindowsForms,
		/// <summary>Specifies the default view technology support. </summary>
		// Token: 0x040014A9 RID: 5289
		Default
	}
}
