using System;
using System.Collections.Generic;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Maps a browser request to a dictionary data object.</summary>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	/// <typeparam name="TValue">The type of the value.</typeparam>
	// Token: 0x02000711 RID: 1809
	public class DictionaryModelBinder<TKey, TValue> : CollectionModelBinder<KeyValuePair<TKey, TValue>>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.DictionaryModelBinder`2" /> class.</summary>
		// Token: 0x06004BBF RID: 19391 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public DictionaryModelBinder()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Converts the collection to a dictionary.</summary>
		/// <returns>true in all cases.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <param name="bindingContext">The binding context.</param>
		/// <param name="newCollection">The new collection.</param>
		// Token: 0x06004BC0 RID: 19392 RVA: 0x000C830C File Offset: 0x000C650C
		protected override bool CreateOrReplaceCollection(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext, IList<KeyValuePair<TKey, TValue>> newCollection)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}
	}
}
