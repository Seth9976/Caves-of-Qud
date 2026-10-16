using System;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Represents an attribute that specifies that values for model binding are provided by a user profile.</summary>
	// Token: 0x02000737 RID: 1847
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
	public sealed class UserProfileAttribute : Attribute, IValueProviderSource
	{
		/// <summary>Gets the value provider.</summary>
		/// <returns>A new instance of the <see cref="T:System.Web.ModelBinding.UserProfileValueProvider" /> class.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		// Token: 0x06004C42 RID: 19522 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public IValueProvider GetValueProvider(ModelBindingExecutionContext modelBindingExecutionContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
