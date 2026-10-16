using System;
using System.Collections.Generic;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides an adapter for objects that implement the <see cref="T:System.ComponentModel.DataAnnotations.IValidatableObject" /> interface.</summary>
	// Token: 0x02000739 RID: 1849
	public class ValidatableObjectAdapter : ModelValidator
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ValidatableObjectAdapter" /> class.</summary>
		/// <param name="metadata">The metadata.</param>
		/// <param name="context">The execution context.</param>
		// Token: 0x06004C45 RID: 19525 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public ValidatableObjectAdapter(ModelMetadata metadata, ModelBindingExecutionContext context)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Validates the specified object.</summary>
		/// <returns>The validation results.</returns>
		/// <param name="container">The container for the object to be validated.</param>
		// Token: 0x06004C46 RID: 19526 RVA: 0x0000FA7F File Offset: 0x0000DC7F
		public override IEnumerable<ModelValidationResult> Validate(object container)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}
	}
}
