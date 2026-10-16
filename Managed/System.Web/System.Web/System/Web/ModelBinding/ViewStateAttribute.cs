using System;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Represents an attribute that specifies that values for model binding are provided by view state.</summary>
	// Token: 0x0200073B RID: 1851
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
	public sealed class ViewStateAttribute : ValueProviderSourceAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ViewStateAttribute" /> class.</summary>
		// Token: 0x06004C4E RID: 19534 RVA: 0x00003946 File Offset: 0x00001B46
		public ViewStateAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ViewStateAttribute" /> class using the specified view state key.</summary>
		/// <param name="key">The key.</param>
		// Token: 0x06004C4F RID: 19535 RVA: 0x00003946 File Offset: 0x00001B46
		public ViewStateAttribute(string key)
		{
		}

		/// <summary>Gets the view state key.</summary>
		/// <returns>The view state key.</returns>
		// Token: 0x1700178A RID: 6026
		// (get) Token: 0x06004C50 RID: 19536 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public string Key
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Returns the model name.</summary>
		/// <returns>The model name.</returns>
		// Token: 0x06004C51 RID: 19537 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public override string GetModelName()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns the value provider.</summary>
		/// <returns>The value provider.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="modelBindingExecutionContext" /> parameter is null.</exception>
		// Token: 0x06004C52 RID: 19538 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public override IValueProvider GetValueProvider(ModelBindingExecutionContext modelBindingExecutionContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
