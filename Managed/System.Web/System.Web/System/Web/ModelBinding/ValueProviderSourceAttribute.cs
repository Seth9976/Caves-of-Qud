using System;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides a base class for value-provider attributes that can be used in method parameters to specify the source of values for model binding, such as <see cref="T:System.Web.ModelBinding.FormAttribute" />, <see cref="T:System.Web.ModelBinding.QueryStringAttribute" />, and <see cref="T:System.Web.ModelBinding.ViewStateAttribute" />.</summary>
	// Token: 0x02000702 RID: 1794
	public abstract class ValueProviderSourceAttribute : Attribute, IModelNameProvider, IValueProviderSource
	{
		/// <summary>When implemented in a derived class, returns the name of the model. The default is null.</summary>
		/// <returns>The name of the model.</returns>
		// Token: 0x06004B82 RID: 19330 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public virtual string GetModelName()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns the value provider.</summary>
		/// <returns>The value provider.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		// Token: 0x06004B83 RID: 19331
		public abstract IValueProvider GetValueProvider(ModelBindingExecutionContext modelBindingExecutionContext);
	}
}
