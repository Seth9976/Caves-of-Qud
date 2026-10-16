using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000258 RID: 600
	[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
	public readonly struct NullabilityInfo : IEquatable<NullabilityInfo>
	{
		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06001719 RID: 5913 RVA: 0x000458B3 File Offset: 0x00043AB3
		public NullableAnnotation Annotation { get; }

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x0600171A RID: 5914 RVA: 0x000458BB File Offset: 0x00043ABB
		public NullableFlowState FlowState { get; }

		// Token: 0x0600171B RID: 5915 RVA: 0x000458C3 File Offset: 0x00043AC3
		internal NullabilityInfo(NullableAnnotation annotation, NullableFlowState flowState)
		{
			this.Annotation = annotation;
			this.FlowState = flowState;
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x000458D3 File Offset: 0x00043AD3
		[NullableContext(1)]
		private string GetDebuggerDisplay()
		{
			return string.Format("{{Annotation: {0}, Flow State: {1}}}", this.Annotation, this.FlowState);
		}

		// Token: 0x0600171D RID: 5917 RVA: 0x000458F8 File Offset: 0x00043AF8
		[NullableContext(2)]
		public override bool Equals(object other)
		{
			if (other is NullabilityInfo)
			{
				NullabilityInfo nullabilityInfo = (NullabilityInfo)other;
				return this.Equals(nullabilityInfo);
			}
			return false;
		}

		// Token: 0x0600171E RID: 5918 RVA: 0x00045920 File Offset: 0x00043B20
		public override int GetHashCode()
		{
			return Hash.Combine(this.Annotation.GetHashCode(), this.FlowState.GetHashCode());
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x0004595A File Offset: 0x00043B5A
		public bool Equals(NullabilityInfo other)
		{
			return this.Annotation == other.Annotation && this.FlowState == other.FlowState;
		}
	}
}
