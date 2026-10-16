using System;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Represents a factory for validators that are based on the <see cref="T:System.ComponentModel.DataAnnotations.IValidatableObject" /> interface.</summary>
	// Token: 0x0200070D RID: 1805
	public sealed class DataAnnotationsValidatableObjectAdapterFactory : MulticastDelegate
	{
		// Token: 0x06004BB4 RID: 19380 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public DataAnnotationsValidatableObjectAdapterFactory(object @object, IntPtr method)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06004BB5 RID: 19381 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public ModelValidator Invoke(ModelMetadata metadata, ModelBindingExecutionContext context)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06004BB6 RID: 19382 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public IAsyncResult BeginInvoke(ModelMetadata metadata, ModelBindingExecutionContext context, AsyncCallback callback, object @object)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06004BB7 RID: 19383 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public ModelValidator EndInvoke(IAsyncResult result)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
