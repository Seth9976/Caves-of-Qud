using System;
using System.Collections.Immutable;
using System.Threading;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000594 RID: 1428
	internal abstract class SynthesizedInstanceMethodSymbol : MethodSymbol
	{
		// Token: 0x17002197 RID: 8599
		// (get) Token: 0x06007763 RID: 30563 RVA: 0x001B81C5 File Offset: 0x001B63C5
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				return ImmutableArray<SyntaxReference>.Empty;
			}
		}

		// Token: 0x17002198 RID: 8600
		// (get) Token: 0x06007764 RID: 30564 RVA: 0x001B81CC File Offset: 0x001B63CC
		public sealed override bool IsImplicitlyDeclared
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002199 RID: 8601
		// (get) Token: 0x06007765 RID: 30565 RVA: 0x001B81CF File Offset: 0x001B63CF
		public sealed override bool AreLocalsZeroed
		{
			get
			{
				return this.ContainingType.AreLocalsZeroed;
			}
		}

		// Token: 0x06007766 RID: 30566 RVA: 0x001B81DC File Offset: 0x001B63DC
		internal override bool TryGetThisParameter(out ParameterSymbol thisParameter)
		{
			if (this._lazyThisParameter == null)
			{
				Interlocked.CompareExchange<ParameterSymbol>(ref this._lazyThisParameter, new ThisParameterSymbol(this), null);
			}
			thisParameter = this._lazyThisParameter;
			return true;
		}

		// Token: 0x1700219A RID: 8602
		// (get) Token: 0x06007767 RID: 30567 RVA: 0x001B8202 File Offset: 0x001B6402
		internal sealed override ObsoleteAttributeData ObsoleteAttributeData
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06007768 RID: 30568 RVA: 0x001B8205 File Offset: 0x001B6405
		internal sealed override UnmanagedCallersOnlyAttributeData GetUnmanagedCallersOnlyAttributeData(bool forceComplete)
		{
			return null;
		}

		// Token: 0x06007769 RID: 30569 RVA: 0x001B8208 File Offset: 0x001B6408
		internal override int CalculateLocalSyntaxOffset(int localPosition, SyntaxTree localTree)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x1700219B RID: 8603
		// (get) Token: 0x0600776A RID: 30570 RVA: 0x001B820F File Offset: 0x001B640F
		internal override bool IsDeclaredReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700219C RID: 8604
		// (get) Token: 0x0600776B RID: 30571 RVA: 0x001B8212 File Offset: 0x001B6412
		internal override bool IsInitOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700219D RID: 8605
		// (get) Token: 0x0600776C RID: 30572 RVA: 0x001B8215 File Offset: 0x001B6415
		public sealed override FlowAnalysisAnnotations FlowAnalysisAnnotations
		{
			get
			{
				return FlowAnalysisAnnotations.None;
			}
		}

		// Token: 0x0600776D RID: 30573 RVA: 0x001B8218 File Offset: 0x001B6418
		internal override bool IsNullableAnalysisEnabled()
		{
			return false;
		}

		// Token: 0x04001CDB RID: 7387
		private ParameterSymbol _lazyThisParameter;
	}
}
