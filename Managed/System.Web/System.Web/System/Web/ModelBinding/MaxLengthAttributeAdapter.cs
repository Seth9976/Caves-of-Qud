using System;
using System.ComponentModel.DataAnnotations;
using Unity;

namespace System.Web.ModelBinding
{
	// Token: 0x0200071B RID: 1819
	public sealed class MaxLengthAttributeAdapter : DataAnnotationsModelValidator<MaxLengthAttribute>
	{
		// Token: 0x06004BDF RID: 19423 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public MaxLengthAttributeAdapter(ModelMetadata metadata, ModelBindingExecutionContext context, MaxLengthAttribute attribute)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06004BE0 RID: 19424 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected override string GetLocalizedErrorMessage(string errorMessage)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
