using System;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides a model binder for arrays.</summary>
	// Token: 0x020006EA RID: 1770
	public sealed class ArrayModelBinderProvider : ModelBinderProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ArrayModelBinderProvider" /> class.</summary>
		// Token: 0x06004ADD RID: 19165 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public ArrayModelBinderProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns a model binder for arrays.</summary>
		/// <returns>A model binder object, or null if the attempt to get a model binder is unsuccessful.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <param name="bindingContext">The binding context.</param>
		// Token: 0x06004ADE RID: 19166 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public override IModelBinder GetBinder(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
