using System;
using System.ComponentModel.DataAnnotations;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides an adapter for the <see cref="T:System.ComponentModel.DataAnnotations.StringLengthAttribute" /> attribute.</summary>
	// Token: 0x02000732 RID: 1842
	public sealed class StringLengthAttributeAdapter : DataAnnotationsModelValidator<StringLengthAttribute>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.StringLengthAttributeAdapter" /> class.</summary>
		/// <param name="metadata">The metadata.</param>
		/// <param name="context">The execution context.</param>
		/// <param name="attribute">The attribute.</param>
		// Token: 0x06004C37 RID: 19511 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public StringLengthAttributeAdapter(ModelMetadata metadata, ModelBindingExecutionContext context, StringLengthAttribute attribute)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06004C38 RID: 19512 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected override string GetLocalizedErrorMessage(string errorMessage)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
