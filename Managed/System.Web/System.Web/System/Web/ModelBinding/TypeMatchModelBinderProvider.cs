using System;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides a model binder for a model that does not require type conversion.</summary>
	// Token: 0x02000736 RID: 1846
	[ModelBinderProviderOptions(FrontOfList = true)]
	public sealed class TypeMatchModelBinderProvider : ModelBinderProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.TypeMatchModelBinderProvider" /> class.</summary>
		// Token: 0x06004C3F RID: 19519 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public TypeMatchModelBinderProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns a model binder for a model that does not require type conversion.</summary>
		/// <returns>The model binder, or null if the attempt to get a model binder is unsuccessful.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <param name="bindingContext">The binding context.</param>
		// Token: 0x06004C40 RID: 19520 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public override IModelBinder GetBinder(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
