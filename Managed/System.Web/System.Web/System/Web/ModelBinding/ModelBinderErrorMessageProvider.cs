using System;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Represents a method that provides a model-binding error message.</summary>
	// Token: 0x0200071E RID: 1822
	public sealed class ModelBinderErrorMessageProvider : MulticastDelegate
	{
		// Token: 0x06004BF7 RID: 19447 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public ModelBinderErrorMessageProvider(object @object, IntPtr method)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06004BF8 RID: 19448 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public string Invoke(ModelBindingExecutionContext modelBindingExecutionContext, ModelMetadata modelMetadata, object incomingValue)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06004BF9 RID: 19449 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public IAsyncResult BeginInvoke(ModelBindingExecutionContext modelBindingExecutionContext, ModelMetadata modelMetadata, object incomingValue, AsyncCallback callback, object @object)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06004BFA RID: 19450 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public string EndInvoke(IAsyncResult result)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
