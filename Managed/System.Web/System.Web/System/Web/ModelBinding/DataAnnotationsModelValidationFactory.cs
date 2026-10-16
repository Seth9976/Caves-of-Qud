using System;
using System.ComponentModel.DataAnnotations;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Represents the method that creates a <see cref="T:System.Web.ModelBinding.DataAnnotationsModelValidatorProvider" /> instance.</summary>
	// Token: 0x0200070A RID: 1802
	public sealed class DataAnnotationsModelValidationFactory : MulticastDelegate
	{
		// Token: 0x06004B9D RID: 19357 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public DataAnnotationsModelValidationFactory(object @object, IntPtr method)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06004B9E RID: 19358 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public ModelValidator Invoke(ModelMetadata metadata, ModelBindingExecutionContext context, ValidationAttribute attribute)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06004B9F RID: 19359 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public IAsyncResult BeginInvoke(ModelMetadata metadata, ModelBindingExecutionContext context, ValidationAttribute attribute, AsyncCallback callback, object @object)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06004BA0 RID: 19360 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public ModelValidator EndInvoke(IAsyncResult result)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
