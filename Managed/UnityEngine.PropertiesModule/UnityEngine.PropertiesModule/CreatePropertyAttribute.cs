using System;
using UnityEngine.Scripting;

namespace Unity.Properties
{
	// Token: 0x02000011 RID: 17
	[RequireAttributeUsages]
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class CreatePropertyAttribute : RequiredMemberAttribute
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002DC5 File Offset: 0x00000FC5
		// (set) Token: 0x0600004E RID: 78 RVA: 0x00002DCD File Offset: 0x00000FCD
		public bool ReadOnly { get; set; } = false;
	}
}
