using System;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000038 RID: 56
	internal sealed class LookupResult
	{
		// Token: 0x06000694 RID: 1684 RVA: 0x0003B2CE File Offset: 0x000394CE
		private LookupResult(ObjectPool<LookupResult> pool)
		{
			this._pool = pool;
			this._kind = LookupResultKind.Empty;
			this._symbolList = new ArrayBuilder<Symbol>();
			this._error = null;
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x0003B2F6 File Offset: 0x000394F6
		internal bool IsClear
		{
			get
			{
				return this._kind == LookupResultKind.Empty && this._error == null && this._symbolList.Count == 0;
			}
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0003B318 File Offset: 0x00039518
		internal void Clear()
		{
			this._kind = LookupResultKind.Empty;
			this._symbolList.Clear();
			this._error = null;
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x0003B333 File Offset: 0x00039533
		internal LookupResultKind Kind
		{
			get
			{
				return this._kind;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000698 RID: 1688 RVA: 0x0003B33B File Offset: 0x0003953B
		internal Symbol SingleSymbolOrDefault
		{
			get
			{
				if (this._symbolList.Count != 1)
				{
					return null;
				}
				return this._symbolList[0];
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x0003B359 File Offset: 0x00039559
		internal ArrayBuilder<Symbol> Symbols
		{
			get
			{
				return this._symbolList;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x0003B361 File Offset: 0x00039561
		internal DiagnosticInfo Error
		{
			get
			{
				return this._error;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x0003B369 File Offset: 0x00039569
		internal bool IsMultiViable
		{
			get
			{
				return this.Kind == LookupResultKind.Viable;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x0003B375 File Offset: 0x00039575
		internal bool IsSingleViable
		{
			get
			{
				return this.Kind == LookupResultKind.Viable && this._symbolList.Count == 1;
			}
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0003B391 File Offset: 0x00039591
		internal static SingleLookupResult Good(Symbol symbol)
		{
			return new SingleLookupResult(LookupResultKind.Viable, symbol, null);
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0003B39C File Offset: 0x0003959C
		internal static SingleLookupResult WrongArity(Symbol symbol, DiagnosticInfo error)
		{
			return new SingleLookupResult(LookupResultKind.WrongArity, symbol, error);
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0003B3A6 File Offset: 0x000395A6
		internal static SingleLookupResult Empty()
		{
			return new SingleLookupResult(LookupResultKind.Empty, null, null);
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0003B3B0 File Offset: 0x000395B0
		internal static SingleLookupResult NotReferencable(Symbol symbol, DiagnosticInfo error)
		{
			return new SingleLookupResult(LookupResultKind.NotReferencable, symbol, error);
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0003B3BA File Offset: 0x000395BA
		internal static SingleLookupResult StaticInstanceMismatch(Symbol symbol, DiagnosticInfo error)
		{
			return new SingleLookupResult(LookupResultKind.StaticInstanceMismatch, symbol, error);
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0003B3C5 File Offset: 0x000395C5
		internal static SingleLookupResult Inaccessible(Symbol symbol, DiagnosticInfo error)
		{
			return new SingleLookupResult(LookupResultKind.Inaccessible, symbol, error);
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0003B3D0 File Offset: 0x000395D0
		internal static SingleLookupResult NotInvocable(Symbol unwrappedSymbol, Symbol symbol, bool diagnose)
		{
			CSDiagnosticInfo csdiagnosticInfo = (diagnose ? new CSDiagnosticInfo(ErrorCode.ERR_NonInvocableMemberCalled, new object[] { unwrappedSymbol }) : null);
			return new SingleLookupResult(LookupResultKind.NotInvocable, symbol, csdiagnosticInfo);
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0003B401 File Offset: 0x00039601
		internal static SingleLookupResult NotLabel(Symbol symbol, DiagnosticInfo error)
		{
			return new SingleLookupResult(LookupResultKind.NotLabel, symbol, error);
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0003B40C File Offset: 0x0003960C
		internal static SingleLookupResult NotTypeOrNamespace(Symbol symbol, DiagnosticInfo error)
		{
			return new SingleLookupResult(LookupResultKind.NotATypeOrNamespace, symbol, error);
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0003B418 File Offset: 0x00039618
		internal static SingleLookupResult NotTypeOrNamespace(Symbol unwrappedSymbol, Symbol symbol, bool diagnose)
		{
			CSDiagnosticInfo csdiagnosticInfo = (diagnose ? new CSDiagnosticInfo(ErrorCode.ERR_BadSKknown, new object[]
			{
				unwrappedSymbol.Name,
				unwrappedSymbol.GetKindText(),
				MessageID.IDS_SK_TYPE.Localize()
			}) : null);
			return new SingleLookupResult(LookupResultKind.NotATypeOrNamespace, symbol, csdiagnosticInfo);
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0003B46A File Offset: 0x0003966A
		internal static SingleLookupResult NotAnAttributeType(Symbol symbol, DiagnosticInfo error)
		{
			return new SingleLookupResult(LookupResultKind.NotAnAttributeType, symbol, error);
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0003B474 File Offset: 0x00039674
		internal void SetFrom(SingleLookupResult other)
		{
			this._kind = other.Kind;
			this._symbolList.Clear();
			this._symbolList.Add(other.Symbol);
			this._error = other.Error;
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x0003B4AA File Offset: 0x000396AA
		internal void SetFrom(LookupResult other)
		{
			this._kind = other._kind;
			this._symbolList.Clear();
			this._symbolList.AddRange(other._symbolList);
			this._error = other._error;
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0003B4E0 File Offset: 0x000396E0
		internal void SetFrom(DiagnosticInfo error)
		{
			this.Clear();
			this._error = error;
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0003B4EF File Offset: 0x000396EF
		internal void MergePrioritized(LookupResult other)
		{
			if (other.Kind > this.Kind)
			{
				this.SetFrom(other);
			}
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0003B508 File Offset: 0x00039708
		internal void MergeEqual(LookupResult other)
		{
			if (this.Kind > other.Kind)
			{
				return;
			}
			if (other.Kind > this.Kind)
			{
				this.SetFrom(other);
				return;
			}
			if (this.Kind != LookupResultKind.Viable)
			{
				return;
			}
			this._symbolList.AddRange(other._symbolList);
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0003B556 File Offset: 0x00039756
		internal void MergeEqual(SingleLookupResult result)
		{
			if (this.Kind <= result.Kind)
			{
				if (result.Kind > this.Kind)
				{
					this.SetFrom(result);
					return;
				}
				if (result.Symbol != null)
				{
					this._symbolList.Add(result.Symbol);
				}
			}
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0003B595 File Offset: 0x00039795
		internal static ObjectPool<LookupResult> CreatePool()
		{
			ObjectPool<LookupResult> pool = null;
			pool = new ObjectPool<LookupResult>(() => new LookupResult(pool), 128);
			return pool;
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0003B5C4 File Offset: 0x000397C4
		internal static LookupResult GetInstance()
		{
			return LookupResult.s_poolInstance.Allocate();
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0003B5D0 File Offset: 0x000397D0
		internal void Free()
		{
			this.Clear();
			if (this._pool != null)
			{
				this._pool.Free(this);
			}
		}

		// Token: 0x040000A8 RID: 168
		private LookupResultKind _kind;

		// Token: 0x040000A9 RID: 169
		private readonly ArrayBuilder<Symbol> _symbolList;

		// Token: 0x040000AA RID: 170
		private DiagnosticInfo _error;

		// Token: 0x040000AB RID: 171
		private readonly ObjectPool<LookupResult> _pool;

		// Token: 0x040000AC RID: 172
		private static readonly ObjectPool<LookupResult> s_poolInstance = LookupResult.CreatePool();
	}
}
