using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000022 RID: 34
	public readonly struct BindingResult
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x0000369A File Offset: 0x0000189A
		public BindingStatus status { get; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000AA RID: 170 RVA: 0x000036A2 File Offset: 0x000018A2
		public string message { get; }

		// Token: 0x060000AB RID: 171 RVA: 0x000036AA File Offset: 0x000018AA
		public BindingResult(BindingStatus status, string message = null)
		{
			this.status = status;
			this.message = message;
		}
	}
}
