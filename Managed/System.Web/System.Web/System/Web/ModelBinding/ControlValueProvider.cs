using System;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Represents a value provider for control values.</summary>
	// Token: 0x02000703 RID: 1795
	public sealed class ControlValueProvider : SimpleValueProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ControlValueProvider" /> class.</summary>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <param name="propertyName">The name of the control property to get the value from.</param>
		// Token: 0x06004B84 RID: 19332 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public ControlValueProvider(ModelBindingExecutionContext modelBindingExecutionContext, string propertyName)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the name of the control property to get the value from.</summary>
		/// <returns>The property name.</returns>
		// Token: 0x17001769 RID: 5993
		// (get) Token: 0x06004B85 RID: 19333 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public string PropertyName
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x06004B86 RID: 19334 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected override object FetchValue(string controlId)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
