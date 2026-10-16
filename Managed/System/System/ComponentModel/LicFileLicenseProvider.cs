using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;

namespace System.ComponentModel
{
	/// <summary>Provides an implementation of a <see cref="T:System.ComponentModel.LicenseProvider" />. The provider works in a similar fashion to the Microsoft .NET Framework standard licensing model.</summary>
	// Token: 0x020003C7 RID: 967
	public class LicFileLicenseProvider : LicenseProvider
	{
		/// <summary>Determines whether the key that the <see cref="M:System.ComponentModel.LicFileLicenseProvider.GetLicense(System.ComponentModel.LicenseContext,System.Type,System.Object,System.Boolean)" /> method retrieves is valid for the specified type.</summary>
		/// <returns>true if the key is a valid <see cref="P:System.ComponentModel.License.LicenseKey" /> for the specified type; otherwise, false.</returns>
		/// <param name="key">The <see cref="P:System.ComponentModel.License.LicenseKey" /> to check. </param>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the component requesting the <see cref="T:System.ComponentModel.License" />. </param>
		// Token: 0x06001F47 RID: 8007 RVA: 0x0006CDE2 File Offset: 0x0006AFE2
		protected virtual bool IsKeyValid(string key, Type type)
		{
			return key != null && key.StartsWith(this.GetKey(type));
		}

		/// <summary>Returns a key for the specified type.</summary>
		/// <returns>A confirmation that the <paramref name="type" /> parameter is licensed.</returns>
		/// <param name="type">The object type to return the key. </param>
		// Token: 0x06001F48 RID: 8008 RVA: 0x0006CDF6 File Offset: 0x0006AFF6
		protected virtual string GetKey(Type type)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0} is a licensed component.", type.FullName);
		}

		/// <summary>Returns a license for the instance of the component, if one is available.</summary>
		/// <returns>A valid <see cref="T:System.ComponentModel.License" />. If this method cannot find a valid <see cref="T:System.ComponentModel.License" /> or a valid <paramref name="context" /> parameter, it returns null.</returns>
		/// <param name="context">A <see cref="T:System.ComponentModel.LicenseContext" /> that specifies where you can use the licensed object. </param>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the component requesting the <see cref="T:System.ComponentModel.License" />. </param>
		/// <param name="instance">An object that requests the <see cref="T:System.ComponentModel.License" />. </param>
		/// <param name="allowExceptions">true if a <see cref="T:System.ComponentModel.LicenseException" /> should be thrown when a component cannot be granted a license; otherwise, false. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001F49 RID: 8009 RVA: 0x0006CE10 File Offset: 0x0006B010
		public override License GetLicense(LicenseContext context, Type type, object instance, bool allowExceptions)
		{
			LicFileLicenseProvider.LicFileLicense licFileLicense = null;
			if (context != null)
			{
				if (context.UsageMode == LicenseUsageMode.Runtime)
				{
					string savedLicenseKey = context.GetSavedLicenseKey(type, null);
					if (savedLicenseKey != null && this.IsKeyValid(savedLicenseKey, type))
					{
						licFileLicense = new LicFileLicenseProvider.LicFileLicense(this, savedLicenseKey);
					}
				}
				if (licFileLicense == null)
				{
					string text = null;
					if (context != null)
					{
						ITypeResolutionService typeResolutionService = (ITypeResolutionService)context.GetService(typeof(ITypeResolutionService));
						if (typeResolutionService != null)
						{
							text = typeResolutionService.GetPathOfAssembly(type.Assembly.GetName());
						}
					}
					if (text == null)
					{
						text = type.Module.FullyQualifiedName;
					}
					string text2 = Path.GetDirectoryName(text) + "\\" + type.FullName + ".lic";
					if (File.Exists(text2))
					{
						StreamReader streamReader = new StreamReader(new FileStream(text2, FileMode.Open, FileAccess.Read, FileShare.Read));
						string text3 = streamReader.ReadLine();
						streamReader.Close();
						if (this.IsKeyValid(text3, type))
						{
							licFileLicense = new LicFileLicenseProvider.LicFileLicense(this, this.GetKey(type));
						}
					}
					if (licFileLicense != null)
					{
						context.SetSavedLicenseKey(type, licFileLicense.LicenseKey);
					}
				}
			}
			return licFileLicense;
		}

		// Token: 0x020003C8 RID: 968
		private class LicFileLicense : License
		{
			// Token: 0x06001F4B RID: 8011 RVA: 0x0006CF04 File Offset: 0x0006B104
			public LicFileLicense(LicFileLicenseProvider owner, string key)
			{
				this._owner = owner;
				this.LicenseKey = key;
			}

			// Token: 0x1700065D RID: 1629
			// (get) Token: 0x06001F4C RID: 8012 RVA: 0x0006CF1A File Offset: 0x0006B11A
			public override string LicenseKey { get; }

			// Token: 0x06001F4D RID: 8013 RVA: 0x0006CF22 File Offset: 0x0006B122
			public override void Dispose()
			{
				GC.SuppressFinalize(this);
			}

			// Token: 0x04000F6D RID: 3949
			private LicFileLicenseProvider _owner;
		}
	}
}
