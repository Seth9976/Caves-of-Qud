using System;

namespace System.Web.UI
{
	/// <summary>Defines methods used by ASP.NET server controls to provide programmatic access to any attribute declared in the opening tag of a server control.</summary>
	// Token: 0x02000161 RID: 353
	public interface IAttributeAccessor
	{
		/// <summary>When implemented by a class, retrieves the specified attribute property from the server control.</summary>
		/// <returns>The value of the specified attribute.</returns>
		/// <param name="key">A <see cref="T:System.String" /> that represents the name of the server control attribute. </param>
		// Token: 0x06000F12 RID: 3858
		string GetAttribute(string key);

		/// <summary>When implemented by a class, designates an attribute and its value to assign to the ASP.NET server control.</summary>
		/// <param name="key">The name of the attribute to be set. </param>
		/// <param name="value">The value assigned to the attribute. </param>
		// Token: 0x06000F13 RID: 3859
		void SetAttribute(string key, string value);
	}
}
