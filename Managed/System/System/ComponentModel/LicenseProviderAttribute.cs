using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the <see cref="T:System.ComponentModel.LicenseProvider" /> to use with a class. This class cannot be inherited.</summary>
	// Token: 0x020003CD RID: 973
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public sealed class LicenseProviderAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LicenseProviderAttribute" /> class without a license provider.</summary>
		// Token: 0x06001F6C RID: 8044 RVA: 0x0006D3AC File Offset: 0x0006B5AC
		public LicenseProviderAttribute()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LicenseProviderAttribute" /> class with the specified type.</summary>
		/// <param name="typeName">The fully qualified name of the license provider class. </param>
		// Token: 0x06001F6D RID: 8045 RVA: 0x0006D3B5 File Offset: 0x0006B5B5
		public LicenseProviderAttribute(string typeName)
		{
			this._licenseProviderName = typeName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LicenseProviderAttribute" /> class with the specified type of license provider.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of the license provider class. </param>
		// Token: 0x06001F6E RID: 8046 RVA: 0x0006D3C4 File Offset: 0x0006B5C4
		public LicenseProviderAttribute(Type type)
		{
			this._licenseProviderType = type;
		}

		/// <summary>Gets the license provider that must be used with the associated class.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the type of the license provider. The default value is null.</returns>
		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06001F6F RID: 8047 RVA: 0x0006D3D3 File Offset: 0x0006B5D3
		public Type LicenseProvider
		{
			get
			{
				if (this._licenseProviderType == null && this._licenseProviderName != null)
				{
					this._licenseProviderType = Type.GetType(this._licenseProviderName);
				}
				return this._licenseProviderType;
			}
		}

		/// <summary>Indicates a unique ID for this attribute type.</summary>
		/// <returns>A unique ID for this attribute type.</returns>
		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06001F70 RID: 8048 RVA: 0x0006D404 File Offset: 0x0006B604
		public override object TypeId
		{
			get
			{
				string text = this._licenseProviderName;
				if (text == null && this._licenseProviderType != null)
				{
					text = this._licenseProviderType.FullName;
				}
				return base.GetType().FullName + text;
			}
		}

		/// <summary>Indicates whether this instance and a specified object are equal.</summary>
		/// <returns>true if <paramref name="value" /> is equal to this instance; otherwise, false.</returns>
		/// <param name="value">Another object to compare to. </param>
		// Token: 0x06001F71 RID: 8049 RVA: 0x0006D448 File Offset: 0x0006B648
		public override bool Equals(object value)
		{
			if (value is LicenseProviderAttribute && value != null)
			{
				Type licenseProvider = ((LicenseProviderAttribute)value).LicenseProvider;
				if (licenseProvider == this.LicenseProvider)
				{
					return true;
				}
				if (licenseProvider != null && licenseProvider.Equals(this.LicenseProvider))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.LicenseProviderAttribute" />.</returns>
		// Token: 0x06001F72 RID: 8050 RVA: 0x000678DE File Offset: 0x00065ADE
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Specifies the default value, which is no provider. This static field is read-only.</summary>
		// Token: 0x04000F75 RID: 3957
		public static readonly LicenseProviderAttribute Default = new LicenseProviderAttribute();

		// Token: 0x04000F76 RID: 3958
		private Type _licenseProviderType;

		// Token: 0x04000F77 RID: 3959
		private string _licenseProviderName;
	}
}
