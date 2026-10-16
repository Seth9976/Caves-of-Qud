using System;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Represents an attribute that specifies that values for model binding are provided by a control.</summary>
	// Token: 0x02000701 RID: 1793
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
	public sealed class ControlAttribute : ValueProviderSourceAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ControlAttribute" /> class.</summary>
		// Token: 0x06004B7A RID: 19322 RVA: 0x00003946 File Offset: 0x00001B46
		public ControlAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ControlAttribute" /> class using the specified control ID.</summary>
		/// <param name="controlID">The control ID.</param>
		// Token: 0x06004B7B RID: 19323 RVA: 0x00003946 File Offset: 0x00001B46
		public ControlAttribute(string controlID)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ControlAttribute" /> class using the specified control ID and property name.</summary>
		/// <param name="controlID">The control ID.</param>
		/// <param name="propertyName">The property name.</param>
		// Token: 0x06004B7C RID: 19324 RVA: 0x00003946 File Offset: 0x00001B46
		public ControlAttribute(string controlID, string propertyName)
		{
		}

		/// <summary>Gets the control ID.</summary>
		/// <returns>The control ID.</returns>
		// Token: 0x17001767 RID: 5991
		// (get) Token: 0x06004B7D RID: 19325 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public string ControlID
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the property name.</summary>
		/// <returns>The property name.</returns>
		// Token: 0x17001768 RID: 5992
		// (get) Token: 0x06004B7E RID: 19326 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public string PropertyName
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
		// Token: 0x06004B7F RID: 19327 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public override string GetModelName()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets the value provider.</summary>
		/// <returns>The value provider.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="modelBindingExecutionContext" /> parameter is null.</exception>
		// Token: 0x06004B80 RID: 19328 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public override IValueProvider GetValueProvider(ModelBindingExecutionContext modelBindingExecutionContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
