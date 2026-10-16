using System;

namespace System.Web.ModelBinding
{
	/// <summary>Defines the method that is required for a value provider source.</summary>
	// Token: 0x0200051A RID: 1306
	public interface IValueProviderSource
	{
		/// <summary>Returns a value provider.</summary>
		/// <returns>The value provider.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		// Token: 0x060039AB RID: 14763
		IValueProvider GetValueProvider(ModelBindingExecutionContext modelBindingExecutionContext);
	}
}
