using System;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides a model binder for a collection of key/value pairs.</summary>
	// Token: 0x02000719 RID: 1817
	public sealed class KeyValuePairModelBinderProvider : ModelBinderProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.KeyValuePairModelBinderProvider" /> class.</summary>
		// Token: 0x06004BDB RID: 19419 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public KeyValuePairModelBinderProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns a model binder for a key/value pair collection.</summary>
		/// <returns>The model binder, or null if the value provider does not contain a key and a value.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <param name="bindingContext">The model binding context.</param>
		// Token: 0x06004BDC RID: 19420 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public override IModelBinder GetBinder(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
