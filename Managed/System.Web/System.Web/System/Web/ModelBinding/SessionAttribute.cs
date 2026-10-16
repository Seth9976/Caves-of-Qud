using System;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Represents an attribute that specifies that values for model binding are provided by session state.</summary>
	// Token: 0x02000730 RID: 1840
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
	public sealed class SessionAttribute : ValueProviderSourceAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.SessionAttribute" /> class.</summary>
		// Token: 0x06004C2C RID: 19500 RVA: 0x00003946 File Offset: 0x00001B46
		public SessionAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.SessionAttribute" /> class, using the session state name.</summary>
		/// <param name="name">The name.</param>
		// Token: 0x06004C2D RID: 19501 RVA: 0x00003946 File Offset: 0x00001B46
		public SessionAttribute(string name)
		{
		}

		/// <summary>Gets the session state name.</summary>
		/// <returns>The session state name.</returns>
		// Token: 0x17001787 RID: 6023
		// (get) Token: 0x06004C2E RID: 19502 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public string Name
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the model name.</summary>
		/// <returns>The model name.</returns>
		// Token: 0x06004C2F RID: 19503 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public override string GetModelName()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets the value provider.</summary>
		/// <returns>A new instance of the <see cref="T:System.Web.ModelBinding.DictionaryValueProvider`1" /> that contains session state values.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		// Token: 0x06004C30 RID: 19504 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public override IValueProvider GetValueProvider(ModelBindingExecutionContext modelBindingExecutionContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
