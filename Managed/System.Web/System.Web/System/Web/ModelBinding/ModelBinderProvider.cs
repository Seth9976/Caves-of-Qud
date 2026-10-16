using System;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides an abstract base class for model binder providers.</summary>
	// Token: 0x020006EB RID: 1771
	public abstract class ModelBinderProvider
	{
		/// <summary>When overridden in a derived class, initializes a new instance of the class that derives from <see cref="T:System.Web.ModelBinding.ModelBinderProvider" />.</summary>
		// Token: 0x06004ADF RID: 19167 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected ModelBinderProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>When overridden in a derived class, gets a model binder. </summary>
		/// <returns>A model binder.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <param name="bindingContext">The binding context.</param>
		// Token: 0x06004AE0 RID: 19168
		public abstract IModelBinder GetBinder(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext);
	}
}
