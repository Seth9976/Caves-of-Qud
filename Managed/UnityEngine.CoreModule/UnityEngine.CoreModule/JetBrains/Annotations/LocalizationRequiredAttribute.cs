using System;

namespace JetBrains.Annotations
{
	// Token: 0x020000D4 RID: 212
	[AttributeUsage(AttributeTargets.All)]
	public sealed class LocalizationRequiredAttribute : Attribute
	{
		// Token: 0x0600044F RID: 1103 RVA: 0x000085B1 File Offset: 0x000067B1
		public LocalizationRequiredAttribute()
			: this(true)
		{
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x000085BC File Offset: 0x000067BC
		public LocalizationRequiredAttribute(bool required)
		{
			this.Required = required;
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x000085CD File Offset: 0x000067CD
		public bool Required { get; }
	}
}
