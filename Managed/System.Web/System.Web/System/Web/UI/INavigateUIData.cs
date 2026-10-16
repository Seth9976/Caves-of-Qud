using System;

namespace System.Web.UI
{
	/// <summary>Provides an interface that classes implement to provide navigation user interface data and values to navigation controls. </summary>
	// Token: 0x02000174 RID: 372
	public interface INavigateUIData
	{
		/// <summary>Gets text that represents the description of a navigation node of a navigation control.</summary>
		/// <returns>Text that is the description of a node of a navigation control; otherwise, null.</returns>
		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06000F3E RID: 3902
		string Description { get; }

		/// <summary>Gets the text that represents the name of a navigation node of a navigation control.</summary>
		/// <returns>Text that represents the name of a node of a navigation control; otherwise, null.</returns>
		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06000F3F RID: 3903
		string Name { get; }

		/// <summary>Gets the URL to navigate to when the navigation node is clicked.</summary>
		/// <returns>The URL to navigate to when the node is clicked; otherwise, null.</returns>
		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06000F40 RID: 3904
		string NavigateUrl { get; }

		/// <summary>Gets a non-displayed value that is used to store any additional data about the navigation node.</summary>
		/// <returns>A value that is not displayed and is used to store additional data about the navigation node; otherwise, null.</returns>
		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06000F41 RID: 3905
		string Value { get; }
	}
}
