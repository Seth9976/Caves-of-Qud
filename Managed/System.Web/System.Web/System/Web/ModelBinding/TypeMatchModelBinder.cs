using System;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Maps a browser request to a data object. This class is used when model binding does not require type conversion. </summary>
	// Token: 0x02000735 RID: 1845
	public sealed class TypeMatchModelBinder : IModelBinder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.TypeMatchModelBinder" /> class.</summary>
		// Token: 0x06004C3D RID: 19517 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public TypeMatchModelBinder()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Binds the model by using the specified execution context and binding context.</summary>
		/// <returns>true if model binding is successful; otherwise, false.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <param name="bindingContext">The binding context.</param>
		// Token: 0x06004C3E RID: 19518 RVA: 0x000C8504 File Offset: 0x000C6704
		public bool BindModel(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}
	}
}
