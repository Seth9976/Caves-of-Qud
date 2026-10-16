using System;
using System.Collections;

namespace System.Web.UI
{
	/// <summary>Defines a method that automatically generates fields for data-bound controls that use ASP.NET Dynamic Data features.</summary>
	// Token: 0x02000162 RID: 354
	public interface IAutoFieldGenerator
	{
		/// <summary>Automatically generates <see cref="T:System.Web.DynamicData.DynamicField" /> objects based on metadata information for the table.</summary>
		/// <returns>A collection of <see cref="T:System.Web.DynamicData.DynamicField" /> objects.</returns>
		/// <param name="control">The data-bound control that will contain the <see cref="T:System.Web.DynamicData.DynamicField" /> objects.</param>
		// Token: 0x06000F14 RID: 3860
		ICollection GenerateFields(Control control);
	}
}
