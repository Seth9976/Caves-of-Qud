using System;
using System.ComponentModel;

namespace AiUnity.Common.Attributes
{
	// Token: 0x020002EE RID: 750
	[AttributeUsage(AttributeTargets.All)]
	public sealed class ResourceAttribute : DescriptionAttribute
	{
		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06001989 RID: 6537 RVA: 0x00081826 File Offset: 0x0007FA26
		// (set) Token: 0x0600198A RID: 6538 RVA: 0x0008182E File Offset: 0x0007FA2E
		public Uri HelpUri { get; private set; }

		// Token: 0x0600198B RID: 6539 RVA: 0x00081837 File Offset: 0x0007FA37
		public ResourceAttribute(string description, string helpLink = null)
			: base(description)
		{
			this.HelpUri = ((helpLink != null) ? new Uri(helpLink) : null);
		}
	}
}
