using System;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Represents an attribute that specifies that values for model binding are provided by a form field.</summary>
	// Token: 0x02000714 RID: 1812
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
	public sealed class FormAttribute : ValueProviderSourceAttribute, IUnvalidatedValueProviderSource
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.FormAttribute" /> class.</summary>
		// Token: 0x06004BC6 RID: 19398 RVA: 0x00003946 File Offset: 0x00001B46
		public FormAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.FormAttribute" /> class using the specified form field name.</summary>
		/// <param name="fieldName">The name of the form field.</param>
		// Token: 0x06004BC7 RID: 19399 RVA: 0x00003946 File Offset: 0x00001B46
		public FormAttribute(string fieldName)
		{
		}

		/// <summary>Gets the name of the form field.</summary>
		/// <returns>The name of the form field.</returns>
		// Token: 0x17001773 RID: 6003
		// (get) Token: 0x06004BC8 RID: 19400 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public string FieldName
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the form data is validated.</summary>
		/// <returns>true if the form data is validated; otherwise, false.</returns>
		// Token: 0x17001774 RID: 6004
		// (get) Token: 0x06004BC9 RID: 19401 RVA: 0x000C8344 File Offset: 0x000C6544
		// (set) Token: 0x06004BCA RID: 19402 RVA: 0x00003946 File Offset: 0x00001B46
		public bool ValidateInput
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
			}
		}

		/// <summary>Gets the model name.</summary>
		/// <returns>The model name.</returns>
		// Token: 0x06004BCB RID: 19403 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public override string GetModelName()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets the value provider.</summary>
		/// <returns>The value provider.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		// Token: 0x06004BCC RID: 19404 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public override IValueProvider GetValueProvider(ModelBindingExecutionContext modelBindingExecutionContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
