using System;
using System.Collections;
using System.Reflection;

namespace System.ComponentModel.Design
{
	/// <summary>Represents a design-time license context that can support a license provider at design time.</summary>
	// Token: 0x02000450 RID: 1104
	public class DesigntimeLicenseContext : LicenseContext
	{
		/// <summary>Gets the license usage mode.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.LicenseUsageMode" /> indicating the licensing mode for the context.</returns>
		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x060023E0 RID: 9184 RVA: 0x0000390E File Offset: 0x00001B0E
		public override LicenseUsageMode UsageMode
		{
			get
			{
				return LicenseUsageMode.Designtime;
			}
		}

		/// <summary>Gets a saved license key.</summary>
		/// <returns>The saved license key that matches the specified type.</returns>
		/// <param name="type">The type of the license key. </param>
		/// <param name="resourceAssembly">The assembly to get the key from. </param>
		// Token: 0x060023E1 RID: 9185 RVA: 0x00002F6A File Offset: 0x0000116A
		public override string GetSavedLicenseKey(Type type, Assembly resourceAssembly)
		{
			return null;
		}

		/// <summary>Sets a saved license key.</summary>
		/// <param name="type">The type of the license key. </param>
		/// <param name="key">The license key. </param>
		// Token: 0x060023E2 RID: 9186 RVA: 0x00081434 File Offset: 0x0007F634
		public override void SetSavedLicenseKey(Type type, string key)
		{
			this.savedLicenseKeys[type.AssemblyQualifiedName] = key;
		}

		// Token: 0x040010E2 RID: 4322
		internal Hashtable savedLicenseKeys = new Hashtable();
	}
}
