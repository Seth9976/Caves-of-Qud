using System;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Maps a browser request to a key/value pair data object.</summary>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	/// <typeparam name="TValue">The type of the value.</typeparam>
	// Token: 0x0200071A RID: 1818
	public sealed class KeyValuePairModelBinder<TKey, TValue> : IModelBinder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.KeyValuePairModelBinder`2" /> class.</summary>
		// Token: 0x06004BDD RID: 19421 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public KeyValuePairModelBinder()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Binds the model by using the specified execution context and binding context.</summary>
		/// <returns>true if model binding is successful; otherwise, false.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <param name="bindingContext">The binding context.</param>
		// Token: 0x06004BDE RID: 19422 RVA: 0x000C8398 File Offset: 0x000C6598
		public bool BindModel(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}
	}
}
