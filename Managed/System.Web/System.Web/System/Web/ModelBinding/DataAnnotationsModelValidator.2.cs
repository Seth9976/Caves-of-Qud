using System;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides a model validator for a specified validation type.</summary>
	/// <typeparam name="TAttribute">The type of the validation attribute.</typeparam>
	// Token: 0x0200070E RID: 1806
	public class DataAnnotationsModelValidator<TAttribute> : DataAnnotationsModelValidator
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.DataAnnotationsModelValidator`1" /> class.</summary>
		/// <param name="metadata">The metadata.</param>
		/// <param name="context">The execution context.</param>
		/// <param name="attribute">The validation attribute.</param>
		// Token: 0x06004BB8 RID: 19384 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public DataAnnotationsModelValidator(ModelMetadata metadata, ModelBindingExecutionContext context, TAttribute attribute)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the validation attribute.</summary>
		/// <returns>The validation attribute.</returns>
		// Token: 0x17001771 RID: 6001
		// (get) Token: 0x06004BB9 RID: 19385 RVA: 0x000C82D4 File Offset: 0x000C64D4
		protected new TAttribute Attribute
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(TAttribute);
			}
		}
	}
}
