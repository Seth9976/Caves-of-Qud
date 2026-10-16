using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200014D RID: 333
	[NullableContext(2)]
	[Nullable(0)]
	public struct PreprocessingSymbolInfo : IEquatable<PreprocessingSymbolInfo>
	{
		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000D4A RID: 3402 RVA: 0x0002734C File Offset: 0x0002554C
		public readonly IPreprocessingSymbol Symbol { get; }

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000D4B RID: 3403 RVA: 0x00027354 File Offset: 0x00025554
		public readonly bool IsDefined { get; }

		// Token: 0x06000D4C RID: 3404 RVA: 0x0002735C File Offset: 0x0002555C
		internal PreprocessingSymbolInfo(IPreprocessingSymbol symbol, bool isDefined)
		{
			this = default(PreprocessingSymbolInfo);
			this.Symbol = symbol;
			this.IsDefined = isDefined;
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x00027373 File Offset: 0x00025573
		public bool Equals(PreprocessingSymbolInfo other)
		{
			return object.Equals(this.Symbol, other.Symbol) && object.Equals(this.IsDefined, other.IsDefined);
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x000273A8 File Offset: 0x000255A8
		public override bool Equals(object obj)
		{
			if (obj is PreprocessingSymbolInfo)
			{
				PreprocessingSymbolInfo preprocessingSymbolInfo = (PreprocessingSymbolInfo)obj;
				return this.Equals(preprocessingSymbolInfo);
			}
			return false;
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x000273CD File Offset: 0x000255CD
		public override int GetHashCode()
		{
			return Hash.Combine(this.IsDefined, Hash.Combine<IPreprocessingSymbol>(this.Symbol, 0));
		}

		// Token: 0x040003B1 RID: 945
		internal static readonly PreprocessingSymbolInfo None = new PreprocessingSymbolInfo(null, false);
	}
}
