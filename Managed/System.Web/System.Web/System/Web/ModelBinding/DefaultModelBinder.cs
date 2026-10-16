using System;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Maps a browser request to a data object. This class provides a concrete implementation of a model binder.</summary>
	// Token: 0x0200070F RID: 1807
	public class DefaultModelBinder : IModelBinder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.DefaultModelBinder" /> class.</summary>
		// Token: 0x06004BBA RID: 19386 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public DefaultModelBinder()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns the collection of model binder providers.</summary>
		/// <returns>A collection of model binder providers.</returns>
		// Token: 0x17001772 RID: 6002
		// (get) Token: 0x06004BBB RID: 19387 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public ModelBinderProviderCollection Providers
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Binds the model using the specified execution context and binding context.</summary>
		/// <returns>true if model binding is successful; otherwise, false.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <param name="bindingContext">The binding context.</param>
		// Token: 0x06004BBC RID: 19388 RVA: 0x000C82F0 File Offset: 0x000C64F0
		public bool BindModel(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}
	}
}
