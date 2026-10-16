using System;

namespace System.Web.Routing
{
	// Token: 0x020004E1 RID: 1249
	internal sealed class ParameterSubsegment : PathSubsegment
	{
		// Token: 0x0600382C RID: 14380 RVA: 0x00094EFC File Offset: 0x000930FC
		public ParameterSubsegment(string parameterName)
		{
			if (parameterName.StartsWith("*", StringComparison.Ordinal))
			{
				this.ParameterName = parameterName.Substring(1);
				this.IsCatchAll = true;
				return;
			}
			this.ParameterName = parameterName;
		}

		// Token: 0x1700119B RID: 4507
		// (get) Token: 0x0600382D RID: 14381 RVA: 0x00094F2E File Offset: 0x0009312E
		// (set) Token: 0x0600382E RID: 14382 RVA: 0x00094F36 File Offset: 0x00093136
		public bool IsCatchAll { get; private set; }

		// Token: 0x1700119C RID: 4508
		// (get) Token: 0x0600382F RID: 14383 RVA: 0x00094F3F File Offset: 0x0009313F
		// (set) Token: 0x06003830 RID: 14384 RVA: 0x00094F47 File Offset: 0x00093147
		public string ParameterName { get; private set; }
	}
}
