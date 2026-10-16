using System;

namespace JetBrains.Annotations
{
	// Token: 0x020000D0 RID: 208
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = true)]
	public sealed class ValueProviderAttribute : Attribute
	{
		// Token: 0x06000445 RID: 1093 RVA: 0x0000854B File Offset: 0x0000674B
		public ValueProviderAttribute([NotNull] string name)
		{
			this.Name = name;
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x0000855C File Offset: 0x0000675C
		[NotNull]
		public string Name { get; }
	}
}
