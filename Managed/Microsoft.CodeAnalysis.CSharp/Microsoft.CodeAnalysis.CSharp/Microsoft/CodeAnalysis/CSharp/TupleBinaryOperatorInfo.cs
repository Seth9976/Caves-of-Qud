using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000F6 RID: 246
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class TupleBinaryOperatorInfo
	{
		// Token: 0x1700035C RID: 860
		// (get) Token: 0x060012CF RID: 4815
		internal abstract TupleBinaryOperatorInfoKind InfoKind { get; }

		// Token: 0x060012D0 RID: 4816 RVA: 0x00063CDA File Offset: 0x00061EDA
		private TupleBinaryOperatorInfo(TypeSymbol leftConvertedTypeOpt, TypeSymbol rightConvertedTypeOpt)
		{
			this.LeftConvertedTypeOpt = leftConvertedTypeOpt;
			this.RightConvertedTypeOpt = rightConvertedTypeOpt;
		}

		// Token: 0x040005D1 RID: 1489
		internal readonly TypeSymbol LeftConvertedTypeOpt;

		// Token: 0x040005D2 RID: 1490
		internal readonly TypeSymbol RightConvertedTypeOpt;

		// Token: 0x0200068A RID: 1674
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal class Single : TupleBinaryOperatorInfo
		{
			// Token: 0x0600832C RID: 33580 RVA: 0x001DBA53 File Offset: 0x001D9C53
			internal Single(TypeSymbol leftConvertedTypeOpt, TypeSymbol rightConvertedTypeOpt, BinaryOperatorKind kind, MethodSymbol methodSymbolOpt, Conversion conversionForBool, UnaryOperatorSignature boolOperator)
				: base(leftConvertedTypeOpt, rightConvertedTypeOpt)
			{
				this.Kind = kind;
				this.MethodSymbolOpt = methodSymbolOpt;
				this.ConversionForBool = conversionForBool;
				this.BoolOperator = boolOperator;
			}

			// Token: 0x1700268E RID: 9870
			// (get) Token: 0x0600832D RID: 33581 RVA: 0x001DBA7C File Offset: 0x001D9C7C
			internal override TupleBinaryOperatorInfoKind InfoKind
			{
				get
				{
					return TupleBinaryOperatorInfoKind.Single;
				}
			}

			// Token: 0x0600832E RID: 33582 RVA: 0x001DBA7F File Offset: 0x001D9C7F
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			public override string ToString()
			{
				return string.Format("binaryOperatorKind: {0}", this.Kind);
			}

			// Token: 0x040020A3 RID: 8355
			internal readonly BinaryOperatorKind Kind;

			// Token: 0x040020A4 RID: 8356
			internal readonly MethodSymbol MethodSymbolOpt;

			// Token: 0x040020A5 RID: 8357
			internal readonly Conversion ConversionForBool;

			// Token: 0x040020A6 RID: 8358
			internal readonly UnaryOperatorSignature BoolOperator;
		}

		// Token: 0x0200068B RID: 1675
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal class Multiple : TupleBinaryOperatorInfo
		{
			// Token: 0x0600832F RID: 33583 RVA: 0x001DBA96 File Offset: 0x001D9C96
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			internal Multiple([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<TupleBinaryOperatorInfo> operators, TypeSymbol leftConvertedTypeOpt, TypeSymbol rightConvertedTypeOpt)
				: base(leftConvertedTypeOpt, rightConvertedTypeOpt)
			{
				this.Operators = operators;
			}

			// Token: 0x1700268F RID: 9871
			// (get) Token: 0x06008330 RID: 33584 RVA: 0x001DBAA7 File Offset: 0x001D9CA7
			internal override TupleBinaryOperatorInfoKind InfoKind
			{
				get
				{
					return TupleBinaryOperatorInfoKind.Multiple;
				}
			}

			// Token: 0x040020A7 RID: 8359
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			internal readonly ImmutableArray<TupleBinaryOperatorInfo> Operators;

			// Token: 0x040020A8 RID: 8360
			[global::System.Runtime.CompilerServices.Nullable(1)]
			internal static readonly TupleBinaryOperatorInfo.Multiple ErrorInstance = new TupleBinaryOperatorInfo.Multiple(ImmutableArray<TupleBinaryOperatorInfo>.Empty, null, null);
		}

		// Token: 0x0200068C RID: 1676
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal class NullNull : TupleBinaryOperatorInfo
		{
			// Token: 0x06008332 RID: 33586 RVA: 0x001DBABD File Offset: 0x001D9CBD
			internal NullNull(BinaryOperatorKind kind)
				: base(null, null)
			{
				this.Kind = kind;
			}

			// Token: 0x17002690 RID: 9872
			// (get) Token: 0x06008333 RID: 33587 RVA: 0x001DBACE File Offset: 0x001D9CCE
			internal override TupleBinaryOperatorInfoKind InfoKind
			{
				get
				{
					return TupleBinaryOperatorInfoKind.NullNull;
				}
			}

			// Token: 0x040020A9 RID: 8361
			internal readonly BinaryOperatorKind Kind;
		}
	}
}
