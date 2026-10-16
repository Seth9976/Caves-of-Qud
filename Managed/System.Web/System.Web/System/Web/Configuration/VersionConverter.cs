using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;

namespace System.Web.Configuration
{
	// Token: 0x020005E6 RID: 1510
	internal sealed class VersionConverter : ConfigurationConverterBase
	{
		// Token: 0x0600417C RID: 16764 RVA: 0x000A3ED4 File Offset: 0x000A20D4
		public VersionConverter()
		{
		}

		// Token: 0x0600417D RID: 16765 RVA: 0x000A932F File Offset: 0x000A752F
		public VersionConverter(int minMajor, int minMinor, string exceptionText = null)
		{
			this.minVersion = new Version(minMajor, minMinor);
			this.exceptionText = exceptionText;
		}

		// Token: 0x0600417E RID: 16766 RVA: 0x000A934C File Offset: 0x000A754C
		public override object ConvertFrom(ITypeDescriptorContext ctx, CultureInfo ci, object data)
		{
			string text = data as string;
			if (string.IsNullOrEmpty(text))
			{
				throw new ConfigurationErrorsException("The input string is too short or null.");
			}
			Version version;
			if (!Version.TryParse(text, out version))
			{
				throw new ConfigurationErrorsException("The input string wasn't in correct format.");
			}
			if (this.minVersion != null && version < this.minVersion)
			{
				throw new ConfigurationErrorsException(string.Format(this.exceptionText, version, this.minVersion));
			}
			return version;
		}

		// Token: 0x0600417F RID: 16767 RVA: 0x000A93BC File Offset: 0x000A75BC
		public override object ConvertTo(ITypeDescriptorContext ctx, CultureInfo ci, object value, Type type)
		{
			Version version = value as Version;
			if (version == null)
			{
				throw new ArgumentException("Is not an instance of the Version type", "value");
			}
			if (type == typeof(string))
			{
				return version.ToString();
			}
			if (type == typeof(Version))
			{
				return version.Clone();
			}
			throw new ConfigurationErrorsException("Conversion to type '" + ((type != null) ? type.ToString() : null) + "' is not supported.");
		}

		// Token: 0x0400233C RID: 9020
		private Version minVersion;

		// Token: 0x0400233D RID: 9021
		private string exceptionText;
	}
}
