using System;
using System.ComponentModel.DataAnnotations;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides an adapter for the <see cref="T:System.ComponentModel.DataAnnotations.RangeAttribute" /> attribute.</summary>
	// Token: 0x0200072B RID: 1835
	public sealed class RangeAttributeAdapter : DataAnnotationsModelValidator<RangeAttribute>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.RangeAttributeAdapter" /> class.</summary>
		/// <param name="metadata">The metadata.</param>
		/// <param name="context">The context.</param>
		/// <param name="attribute">The attribute.</param>
		// Token: 0x06004C21 RID: 19489 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public RangeAttributeAdapter(ModelMetadata metadata, ModelBindingExecutionContext context, RangeAttribute attribute)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06004C22 RID: 19490 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected override string GetLocalizedErrorMessage(string errorMessage)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
