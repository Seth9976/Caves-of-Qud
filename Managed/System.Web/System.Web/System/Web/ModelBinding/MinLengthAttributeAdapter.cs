using System;
using System.ComponentModel.DataAnnotations;
using Unity;

namespace System.Web.ModelBinding
{
	// Token: 0x0200071C RID: 1820
	public sealed class MinLengthAttributeAdapter : DataAnnotationsModelValidator<MinLengthAttribute>
	{
		// Token: 0x06004BE1 RID: 19425 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public MinLengthAttributeAdapter(ModelMetadata metadata, ModelBindingExecutionContext context, MinLengthAttribute attribute)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06004BE2 RID: 19426 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected override string GetLocalizedErrorMessage(string errorMessage)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
