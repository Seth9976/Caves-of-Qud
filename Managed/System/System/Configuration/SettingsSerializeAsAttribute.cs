using System;

namespace System.Configuration
{
	/// <summary>Specifies the serialization mechanism that the settings provider should use. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001DB RID: 475
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
	public sealed class SettingsSerializeAsAttribute : Attribute
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.SettingsSerializeAsAttribute" /> class.</summary>
		/// <param name="serializeAs">A <see cref="T:System.Configuration.SettingsSerializeAs" /> enumerated value that specifies the serialization scheme.</param>
		// Token: 0x06000C5E RID: 3166 RVA: 0x0003273A File Offset: 0x0003093A
		public SettingsSerializeAsAttribute(SettingsSerializeAs serializeAs)
		{
			this.serializeAs = serializeAs;
		}

		/// <summary>Gets the <see cref="T:System.Configuration.SettingsSerializeAs" /> enumeration value that specifies the serialization scheme.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsSerializeAs" /> enumerated value that specifies the serialization scheme.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000C5F RID: 3167 RVA: 0x00032749 File Offset: 0x00030949
		public SettingsSerializeAs SerializeAs
		{
			get
			{
				return this.serializeAs;
			}
		}

		// Token: 0x040007DA RID: 2010
		private SettingsSerializeAs serializeAs;
	}
}
