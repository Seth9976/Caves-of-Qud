using System;

namespace System.Web.Util
{
	/// <summary>Provides the interface for implementing property accessors.</summary>
	// Token: 0x0200013C RID: 316
	public interface IWebPropertyAccessor
	{
		/// <summary>Gets the value of a specified property.</summary>
		/// <returns>The value of the specified property.</returns>
		/// <param name="target">The property from which the value is retrieved.</param>
		// Token: 0x06000E62 RID: 3682
		object GetProperty(object target);

		/// <summary>Sets the specified property with the given value.</summary>
		/// <param name="target">The property for which <paramref name="value" /> is set.</param>
		/// <param name="value">The object containing the value of the property.</param>
		// Token: 0x06000E63 RID: 3683
		void SetProperty(object target, object value);
	}
}
