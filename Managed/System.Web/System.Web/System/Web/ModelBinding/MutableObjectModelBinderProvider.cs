using System;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides a model binder for mutable objects.</summary>
	// Token: 0x02000726 RID: 1830
	public sealed class MutableObjectModelBinderProvider : ModelBinderProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.MutableObjectModelBinderProvider" /> class.</summary>
		// Token: 0x06004C10 RID: 19472 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public MutableObjectModelBinderProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns a model binder for a mutable object.</summary>
		/// <returns>The model binder.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <param name="bindingContext">The binding context.</param>
		// Token: 0x06004C11 RID: 19473 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public override IModelBinder GetBinder(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
