using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000060 RID: 96
	internal sealed class AnalyzedArguments
	{
		// Token: 0x060008FA RID: 2298 RVA: 0x00048C54 File Offset: 0x00046E54
		internal AnalyzedArguments()
		{
			this.Arguments = new ArrayBuilder<BoundExpression>(32);
			this.Names = new ArrayBuilder<IdentifierNameSyntax>(32);
			this.RefKinds = new ArrayBuilder<RefKind>(32);
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x00048C83 File Offset: 0x00046E83
		public void Clear()
		{
			this.Arguments.Clear();
			this.Names.Clear();
			this.RefKinds.Clear();
			this.IsExtensionMethodInvocation = false;
			this._lazyHasDynamicArgument = ThreeState.Unknown;
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x00048CB4 File Offset: 0x00046EB4
		public BoundExpression Argument(int i)
		{
			return this.Arguments[i];
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x00048CC4 File Offset: 0x00046EC4
		public string Name(int i)
		{
			if (this.Names.Count == 0)
			{
				return null;
			}
			IdentifierNameSyntax identifierNameSyntax = this.Names[i];
			if (identifierNameSyntax != null)
			{
				return identifierNameSyntax.Identifier.ValueText;
			}
			return null;
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x00048D00 File Offset: 0x00046F00
		public ImmutableArray<string> GetNames()
		{
			if (this.Names.Count == 0)
			{
				return default(ImmutableArray<string>);
			}
			ArrayBuilder<string> instance = ArrayBuilder<string>.GetInstance(this.Names.Count);
			for (int i = 0; i < this.Names.Count; i++)
			{
				instance.Add(this.Name(i));
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x00048D5E File Offset: 0x00046F5E
		public RefKind RefKind(int i)
		{
			if (this.RefKinds.Count <= 0)
			{
				return Microsoft.CodeAnalysis.RefKind.None;
			}
			return this.RefKinds[i];
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x00048D7C File Offset: 0x00046F7C
		public bool IsExtensionMethodThisArgument(int i)
		{
			return i == 0 && this.IsExtensionMethodInvocation;
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x00048D8C File Offset: 0x00046F8C
		public bool HasDynamicArgument
		{
			get
			{
				if (this._lazyHasDynamicArgument.HasValue())
				{
					return this._lazyHasDynamicArgument.Value();
				}
				bool flag = this.RefKinds.Count > 0;
				for (int i = 0; i < this.Arguments.Count; i++)
				{
					BoundExpression boundExpression = this.Arguments[i];
					if (boundExpression.Type != null && boundExpression.Type.IsDynamic() && (!flag || this.RefKinds[i] == Microsoft.CodeAnalysis.RefKind.None))
					{
						this._lazyHasDynamicArgument = ThreeState.True;
						return true;
					}
				}
				this._lazyHasDynamicArgument = ThreeState.False;
				return false;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000902 RID: 2306 RVA: 0x00048E1C File Offset: 0x0004701C
		public bool HasErrors
		{
			get
			{
				ArrayBuilder<BoundExpression>.Enumerator enumerator = this.Arguments.GetEnumerator();
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.HasAnyErrors)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x00048E51 File Offset: 0x00047051
		public static AnalyzedArguments GetInstance()
		{
			return AnalyzedArguments.Pool.Allocate();
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x00048E60 File Offset: 0x00047060
		public static AnalyzedArguments GetInstance(AnalyzedArguments original)
		{
			AnalyzedArguments instance = AnalyzedArguments.GetInstance();
			instance.Arguments.AddRange(original.Arguments);
			instance.Names.AddRange(original.Names);
			instance.RefKinds.AddRange(original.RefKinds);
			instance.IsExtensionMethodInvocation = original.IsExtensionMethodInvocation;
			instance._lazyHasDynamicArgument = original._lazyHasDynamicArgument;
			return instance;
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x00048EC0 File Offset: 0x000470C0
		public static AnalyzedArguments GetInstance(ImmutableArray<BoundExpression> arguments, ImmutableArray<RefKind> argumentRefKindsOpt, ImmutableArray<IdentifierNameSyntax> argumentNamesOpt)
		{
			AnalyzedArguments instance = AnalyzedArguments.GetInstance();
			instance.Arguments.AddRange(arguments);
			if (!argumentRefKindsOpt.IsDefault)
			{
				instance.RefKinds.AddRange(argumentRefKindsOpt);
			}
			if (!argumentNamesOpt.IsDefault)
			{
				instance.Names.AddRange(argumentNamesOpt);
			}
			return instance;
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x00048F0A File Offset: 0x0004710A
		public void Free()
		{
			this.Clear();
			AnalyzedArguments.Pool.Free(this);
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x00048F1D File Offset: 0x0004711D
		private static ObjectPool<AnalyzedArguments> CreatePool()
		{
			return new ObjectPool<AnalyzedArguments>(() => new AnalyzedArguments(), 10);
		}

		// Token: 0x04000406 RID: 1030
		public readonly ArrayBuilder<BoundExpression> Arguments;

		// Token: 0x04000407 RID: 1031
		public readonly ArrayBuilder<IdentifierNameSyntax> Names;

		// Token: 0x04000408 RID: 1032
		public readonly ArrayBuilder<RefKind> RefKinds;

		// Token: 0x04000409 RID: 1033
		public bool IsExtensionMethodInvocation;

		// Token: 0x0400040A RID: 1034
		private ThreeState _lazyHasDynamicArgument;

		// Token: 0x0400040B RID: 1035
		public static readonly ObjectPool<AnalyzedArguments> Pool = AnalyzedArguments.CreatePool();
	}
}
