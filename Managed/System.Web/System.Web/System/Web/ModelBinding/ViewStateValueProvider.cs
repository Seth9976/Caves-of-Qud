using System;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Represents a value provider for view state values.</summary>
	// Token: 0x0200073C RID: 1852
	public sealed class ViewStateValueProvider : SimpleValueProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ViewStateValueProvider" /> class by using the execution context.</summary>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		// Token: 0x06004C53 RID: 19539 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public ViewStateValueProvider(ModelBindingExecutionContext modelBindingExecutionContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06004C54 RID: 19540 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected override object FetchValue(string key)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
