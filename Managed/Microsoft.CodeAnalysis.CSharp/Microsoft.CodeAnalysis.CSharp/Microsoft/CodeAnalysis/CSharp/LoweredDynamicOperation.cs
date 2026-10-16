using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000205 RID: 517
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal struct LoweredDynamicOperation
	{
		// Token: 0x06002558 RID: 9560 RVA: 0x000CB2C4 File Offset: 0x000C94C4
		public LoweredDynamicOperation([global::System.Runtime.CompilerServices.Nullable(2)] SyntheticBoundNodeFactory factory, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression siteInitialization, BoundExpression siteInvocation, TypeSymbol resultType, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> temps)
		{
			this._factory = factory;
			this._resultType = resultType;
			this._temps = temps;
			this.SiteInitialization = siteInitialization;
			this.SiteInvocation = siteInvocation;
		}

		// Token: 0x06002559 RID: 9561 RVA: 0x000CB2EC File Offset: 0x000C94EC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static LoweredDynamicOperation Bad(BoundExpression loweredReceiver, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> loweredArguments, BoundExpression loweredRight, [global::System.Runtime.CompilerServices.Nullable(1)] TypeSymbol resultType)
		{
			ArrayBuilder<BoundExpression> instance = ArrayBuilder<BoundExpression>.GetInstance();
			instance.AddOptional(loweredReceiver);
			instance.AddRange(loweredArguments);
			instance.AddOptional(loweredRight);
			return LoweredDynamicOperation.Bad(resultType, instance.ToImmutableAndFree());
		}

		// Token: 0x0600255A RID: 9562 RVA: 0x000CB320 File Offset: 0x000C9520
		public static LoweredDynamicOperation Bad(TypeSymbol resultType, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> children)
		{
			BoundBadExpression boundBadExpression = new BoundBadExpression(children[0].Syntax, LookupResultKind.Empty, ImmutableArray<Symbol>.Empty, children, resultType);
			return new LoweredDynamicOperation(null, null, boundBadExpression, resultType, default(ImmutableArray<LocalSymbol>));
		}

		// Token: 0x0600255B RID: 9563 RVA: 0x000CB35C File Offset: 0x000C955C
		public BoundExpression ToExpression()
		{
			if (this._factory == null)
			{
				return this.SiteInvocation;
			}
			if (this._temps.IsDefaultOrEmpty)
			{
				return this._factory.Sequence(new BoundExpression[] { this.SiteInitialization }, this.SiteInvocation, this._resultType);
			}
			return new BoundSequence(this._factory.Syntax, this._temps, ImmutableArray.Create<BoundExpression>(this.SiteInitialization), this.SiteInvocation, this._resultType, false)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x040010D9 RID: 4313
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private readonly SyntheticBoundNodeFactory _factory;

		// Token: 0x040010DA RID: 4314
		private readonly TypeSymbol _resultType;

		// Token: 0x040010DB RID: 4315
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private readonly ImmutableArray<LocalSymbol> _temps;

		// Token: 0x040010DC RID: 4316
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public readonly BoundExpression SiteInitialization;

		// Token: 0x040010DD RID: 4317
		public readonly BoundExpression SiteInvocation;
	}
}
