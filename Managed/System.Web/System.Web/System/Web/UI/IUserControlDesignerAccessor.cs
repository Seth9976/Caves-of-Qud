using System;

namespace System.Web.UI
{
	/// <summary>Defines the properties that allow the designer to access information about a user control at design time.</summary>
	// Token: 0x02000183 RID: 387
	public interface IUserControlDesignerAccessor
	{
		/// <summary>When implemented, gets or sets text between the opening and closing tags of a user control.</summary>
		/// <returns>The text placed between the opening and closing tags of a user control.</returns>
		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06000F5E RID: 3934
		// (set) Token: 0x06000F5F RID: 3935
		string InnerText { get; set; }

		/// <summary>When implemented, gets or sets the full tag name of the user control.</summary>
		/// <returns>The full tag name of the user control.</returns>
		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06000F60 RID: 3936
		// (set) Token: 0x06000F61 RID: 3937
		string TagName { get; set; }
	}
}
