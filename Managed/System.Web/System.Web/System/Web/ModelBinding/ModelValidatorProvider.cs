using System;
using System.Collections.Generic;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides a collection of model validators.</summary>
	// Token: 0x020006FA RID: 1786
	public abstract class ModelValidatorProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ModelValidatorProvider" /> class.</summary>
		// Token: 0x06004B69 RID: 19305 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected ModelValidatorProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a collection of validators for the model.</summary>
		/// <returns>The validators.</returns>
		/// <param name="metadata">The metadata.</param>
		/// <param name="context">The execution context.</param>
		// Token: 0x06004B6A RID: 19306
		public abstract IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ModelBindingExecutionContext context);
	}
}
