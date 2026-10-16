using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.FlowAnalysis
{
	// Token: 0x020002D0 RID: 720
	[NullableContext(1)]
	public interface IFlowCaptureOperation : IOperation
	{
		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06001E2C RID: 7724
		CaptureId Id { get; }

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x06001E2D RID: 7725
		IOperation Value { get; }
	}
}
