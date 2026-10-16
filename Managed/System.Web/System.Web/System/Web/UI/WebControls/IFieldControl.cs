using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a contract that exposes properties that automatically generate fields that are based on data in a data-bound control.</summary>
	// Token: 0x020002D0 RID: 720
	public interface IFieldControl
	{
		/// <summary>Gets or sets the <see cref="T:System.Web.UI.IAutoFieldGenerator" /> interface, which is the interface that generates fields in a data-bound control.</summary>
		/// <returns>The interface that generates fields in data-bound controls.</returns>
		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x06001B36 RID: 6966
		// (set) Token: 0x06001B37 RID: 6967
		IAutoFieldGenerator FieldsGenerator { get; set; }
	}
}
