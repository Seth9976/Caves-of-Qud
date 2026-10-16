using System;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Represents an attribute that specifies that values for model binding are provided by a profile.</summary>
	// Token: 0x02000727 RID: 1831
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
	public sealed class ProfileAttribute : ValueProviderSourceAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ProfileAttribute" /> class.</summary>
		// Token: 0x06004C12 RID: 19474 RVA: 0x00003946 File Offset: 0x00001B46
		public ProfileAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ProfileAttribute" /> class, using the profile key.</summary>
		/// <param name="key">The key.</param>
		// Token: 0x06004C13 RID: 19475 RVA: 0x00003946 File Offset: 0x00001B46
		public ProfileAttribute(string key)
		{
		}

		/// <summary>Gets the profile key.</summary>
		/// <returns>The profile key.</returns>
		// Token: 0x17001783 RID: 6019
		// (get) Token: 0x06004C14 RID: 19476 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public string Key
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
		// Token: 0x06004C15 RID: 19477 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public override string GetModelName()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets the value provider.</summary>
		/// <returns>A new instance of the <see cref="T:System.Web.ModelBinding.ProfileValueProvider" /> class.</returns>
		/// <param name="modelBindingExecutionContext">the execution context.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="modelBindingExecutionContext" /> parameter is null.</exception>
		// Token: 0x06004C16 RID: 19478 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public override IValueProvider GetValueProvider(ModelBindingExecutionContext modelBindingExecutionContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
