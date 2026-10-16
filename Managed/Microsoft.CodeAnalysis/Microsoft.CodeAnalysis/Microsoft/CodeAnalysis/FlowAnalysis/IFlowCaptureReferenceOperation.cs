using System;

namespace Microsoft.CodeAnalysis.FlowAnalysis
{
	// Token: 0x020002D1 RID: 721
	public interface IFlowCaptureReferenceOperation : IOperation
	{
		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x06001E2E RID: 7726
		CaptureId Id { get; }
	}
}
