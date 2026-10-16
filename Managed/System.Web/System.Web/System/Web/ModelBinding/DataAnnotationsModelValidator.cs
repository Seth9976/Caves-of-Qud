using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides a model validator.</summary>
	// Token: 0x0200070B RID: 1803
	public class DataAnnotationsModelValidator : ModelValidator
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.DataAnnotationsModelValidator" /> class.</summary>
		/// <param name="metadata">The metadata.</param>
		/// <param name="context">The execution context.</param>
		/// <param name="attribute">The validation attribute.</param>
		// Token: 0x06004BA1 RID: 19361 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public DataAnnotationsModelValidator(ModelMetadata metadata, ModelBindingExecutionContext context, ValidationAttribute attribute)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the validation attribute for the model validator.</summary>
		/// <returns>The validation attribute.</returns>
		// Token: 0x1700176D RID: 5997
		// (get) Token: 0x06004BA2 RID: 19362 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected internal ValidationAttribute Attribute
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the error message for a validation failure.</summary>
		/// <returns>The error message.</returns>
		// Token: 0x1700176E RID: 5998
		// (get) Token: 0x06004BA3 RID: 19363 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected internal string ErrorMessage
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a value that indicates whether the model is required (that is, whether the validation attribute in the <see cref="P:System.Web.ModelBinding.DataAnnotationsModelValidator.Attribute" /> property is a <see cref="T:System.ComponentModel.DataAnnotations.RequiredAttribute" /> attribute).</summary>
		/// <returns>true if the model is required; otherwise, false.</returns>
		// Token: 0x1700176F RID: 5999
		// (get) Token: 0x06004BA4 RID: 19364 RVA: 0x000C829C File Offset: 0x000C649C
		public override bool IsRequired
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		// Token: 0x06004BA5 RID: 19365 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected virtual string GetLocalizedErrorMessage(string errorMessage)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06004BA6 RID: 19366 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected string GetLocalizedString(string name, object[] arguments)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns a list of validation error messages for the model.</summary>
		/// <returns>The list of validation error messages, or an empty list if no errors are found.</returns>
		/// <param name="container">The container for the model.</param>
		// Token: 0x06004BA7 RID: 19367 RVA: 0x0000FA7F File Offset: 0x0000DC7F
		public override IEnumerable<ModelValidationResult> Validate(object container)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}
	}
}
