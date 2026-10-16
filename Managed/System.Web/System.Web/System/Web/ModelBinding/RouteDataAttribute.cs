using System;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Represents an attribute that specifies that values for model binding are provided by route data.</summary>
	// Token: 0x0200072E RID: 1838
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
	public sealed class RouteDataAttribute : ValueProviderSourceAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.RouteDataAttribute" /> class.</summary>
		// Token: 0x06004C26 RID: 19494 RVA: 0x00003946 File Offset: 0x00001B46
		public RouteDataAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.RouteDataAttribute" /> class, using the route data key.</summary>
		/// <param name="key">The key.</param>
		// Token: 0x06004C27 RID: 19495 RVA: 0x00003946 File Offset: 0x00001B46
		public RouteDataAttribute(string key)
		{
		}

		/// <summary>Gets the route data key.</summary>
		/// <returns>The route data key.</returns>
		// Token: 0x17001786 RID: 6022
		// (get) Token: 0x06004C28 RID: 19496 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
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
		// Token: 0x06004C29 RID: 19497 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public override string GetModelName()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets the value provider.</summary>
		/// <returns>A new instance of the <see cref="T:System.Web.ModelBinding.RouteDataValueProvider" /> class.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		// Token: 0x06004C2A RID: 19498 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public override IValueProvider GetValueProvider(ModelBindingExecutionContext modelBindingExecutionContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
