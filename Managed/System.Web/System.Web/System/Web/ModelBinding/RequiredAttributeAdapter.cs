using System;
using System.ComponentModel.DataAnnotations;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides an adapter for the <see cref="T:System.ComponentModel.DataAnnotations.RequiredAttribute" /> attribute.</summary>
	// Token: 0x0200072D RID: 1837
	public sealed class RequiredAttributeAdapter : DataAnnotationsModelValidator<RequiredAttribute>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.RequiredAttributeAdapter" /> class.</summary>
		/// <param name="metadata">The metadata.</param>
		/// <param name="context">The execution context.</param>
		/// <param name="attribute">The attribute.</param>
		// Token: 0x06004C25 RID: 19493 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public RequiredAttributeAdapter(ModelMetadata metadata, ModelBindingExecutionContext context, RequiredAttribute attribute)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
