using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides a container for a collection of validation providers.</summary>
	// Token: 0x02000723 RID: 1827
	public class ModelValidatorProviderCollection : Collection<ModelValidatorProvider>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ModelValidatorProviderCollection" /> class.</summary>
		// Token: 0x06004C03 RID: 19459 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public ModelValidatorProviderCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ModelValidatorProviderCollection" /> class using an existing collection.</summary>
		/// <param name="list">The collection of validator providers.</param>
		// Token: 0x06004C04 RID: 19460 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public ModelValidatorProviderCollection(IList<ModelValidatorProvider> list)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns the collection of validators.</summary>
		/// <returns>The validators.</returns>
		/// <param name="metadata">The metadata.</param>
		/// <param name="context">The execution context.</param>
		// Token: 0x06004C05 RID: 19461 RVA: 0x0000FA7F File Offset: 0x0000DC7F
		public IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ModelBindingExecutionContext context)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Inserts a validator provider into the collection.</summary>
		/// <param name="index">The zero-based index at which item should be inserted.</param>
		/// <param name="item">The model-validator provider object to insert.</param>
		// Token: 0x06004C06 RID: 19462 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected override void InsertItem(int index, ModelValidatorProvider item)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Replaces the model-validator provider element at the specified index.</summary>
		/// <param name="index">The zero-based index of the model-validator provider element to replace.</param>
		/// <param name="item">The new value for the model-validator provider element.</param>
		// Token: 0x06004C07 RID: 19463 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected override void SetItem(int index, ModelValidatorProvider item)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
