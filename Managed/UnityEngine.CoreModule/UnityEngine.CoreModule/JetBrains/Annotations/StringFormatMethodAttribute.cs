using System;

namespace JetBrains.Annotations
{
	// Token: 0x020000CF RID: 207
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Delegate)]
	public sealed class StringFormatMethodAttribute : Attribute
	{
		// Token: 0x06000443 RID: 1091 RVA: 0x00008532 File Offset: 0x00006732
		public StringFormatMethodAttribute([NotNull] string formatParameterName)
		{
			this.FormatParameterName = formatParameterName;
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x00008543 File Offset: 0x00006743
		[NotNull]
		public string FormatParameterName { get; }
	}
}
