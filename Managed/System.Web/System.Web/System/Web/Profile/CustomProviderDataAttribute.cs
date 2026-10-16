using System;

namespace System.Web.Profile
{
	/// <summary>Provides a string of custom data to the provider for a profile property.</summary>
	// Token: 0x02000501 RID: 1281
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class CustomProviderDataAttribute : Attribute
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Web.Profile.CustomProviderDataAttribute" /> class and specifies a string of custom data.</summary>
		/// <param name="customProviderData">The string of custom data to supply to the provider.</param>
		// Token: 0x06003902 RID: 14594 RVA: 0x0009765D File Offset: 0x0009585D
		public CustomProviderDataAttribute(string customProviderData)
		{
			this.customProviderData = customProviderData;
		}

		/// <summary>Gets a value indicating whether the <see cref="P:System.Web.Profile.CustomProviderDataAttribute.CustomProviderData" /> property is set to the default value.</summary>
		/// <returns>true if the <see cref="P:System.Web.Profile.CustomProviderDataAttribute.CustomProviderData" /> property is set to the default value; otherwise, false.</returns>
		// Token: 0x06003903 RID: 14595 RVA: 0x0009766C File Offset: 0x0009586C
		public override bool IsDefaultAttribute()
		{
			return string.IsNullOrEmpty(this.CustomProviderData);
		}

		/// <summary>Gets a string of custom data for the profile property provider.</summary>
		/// <returns>A string of custom data for the profile property provider. The default is null.</returns>
		// Token: 0x170011CB RID: 4555
		// (get) Token: 0x06003904 RID: 14596 RVA: 0x00097679 File Offset: 0x00095879
		public string CustomProviderData
		{
			get
			{
				return this.customProviderData;
			}
		}

		// Token: 0x04001F17 RID: 7959
		private string customProviderData;
	}
}
