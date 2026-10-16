using System;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Maps a browser request to a data object. This type is used when model binding requires conversions using a .NET Framework type converter.</summary>
	// Token: 0x02000733 RID: 1843
	public sealed class TypeConverterModelBinder : IModelBinder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.TypeConverterModelBinder" /> class.</summary>
		// Token: 0x06004C39 RID: 19513 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public TypeConverterModelBinder()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Binds the model by using the specified controller context and binding context.</summary>
		/// <returns>true if model binding is successful; otherwise, false.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <param name="bindingContext">The binding context.</param>
		// Token: 0x06004C3A RID: 19514 RVA: 0x000C84E8 File Offset: 0x000C66E8
		public bool BindModel(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}
	}
}
