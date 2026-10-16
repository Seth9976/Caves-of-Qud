using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200002C RID: 44
	[DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
	internal sealed class Imports
	{
		// Token: 0x060005F5 RID: 1525 RVA: 0x00038A80 File Offset: 0x00036C80
		private Imports(ImmutableDictionary<string, AliasAndUsingDirective> usingAliases, ImmutableArray<NamespaceOrTypeAndUsingDirective> usings, ImmutableArray<AliasAndExternAliasDirective> externs)
		{
			this.UsingAliases = usingAliases;
			this.Usings = usings;
			this.ExternAliases = externs;
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00038AA0 File Offset: 0x00036CA0
		internal string GetDebuggerDisplay()
		{
			return string.Join("; ", Enumerable.Concat<string>(Enumerable.Concat<string>(Enumerable.Select<KeyValuePair<string, AliasAndUsingDirective>, string>(Enumerable.OrderBy<KeyValuePair<string, AliasAndUsingDirective>, int>(this.UsingAliases, (KeyValuePair<string, AliasAndUsingDirective> x) => x.Value.UsingDirective.Location.SourceSpan.Start), (KeyValuePair<string, AliasAndUsingDirective> ua) => string.Format("{0} = {1}", ua.Key, ua.Value.Alias.Target)), this.Usings.Select<NamespaceOrTypeAndUsingDirective, string>((NamespaceOrTypeAndUsingDirective u) => u.NamespaceOrType.ToString())), this.ExternAliases.Select<AliasAndExternAliasDirective, string>((AliasAndExternAliasDirective ea) => "extern alias " + ea.Alias.Name)));
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00038B64 File Offset: 0x00036D64
		internal static Imports ExpandPreviousSubmissionImports(Imports previousSubmissionImports, CSharpCompilation newSubmission)
		{
			if (previousSubmissionImports == Imports.Empty)
			{
				return Imports.Empty;
			}
			ImmutableDictionary<string, AliasAndUsingDirective> immutableDictionary = ImmutableDictionary<string, AliasAndUsingDirective>.Empty;
			if (!previousSubmissionImports.UsingAliases.IsEmpty)
			{
				ImmutableDictionary<string, AliasAndUsingDirective>.Builder builder = ImmutableDictionary.CreateBuilder<string, AliasAndUsingDirective>();
				foreach (KeyValuePair<string, AliasAndUsingDirective> keyValuePair in previousSubmissionImports.UsingAliases)
				{
					string key = keyValuePair.Key;
					AliasAndUsingDirective value = keyValuePair.Value;
					builder.Add(key, new AliasAndUsingDirective(value.Alias.ToNewSubmission(newSubmission), value.UsingDirective));
				}
				immutableDictionary = builder.ToImmutable();
			}
			ImmutableArray<NamespaceOrTypeAndUsingDirective> immutableArray = Imports.ExpandPreviousSubmissionImports(previousSubmissionImports.Usings, newSubmission);
			return Imports.Create(immutableDictionary, immutableArray, previousSubmissionImports.ExternAliases);
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00038C2C File Offset: 0x00036E2C
		internal static ImmutableArray<NamespaceOrTypeAndUsingDirective> ExpandPreviousSubmissionImports(ImmutableArray<NamespaceOrTypeAndUsingDirective> previousSubmissionUsings, CSharpCompilation newSubmission)
		{
			if (!previousSubmissionUsings.IsEmpty)
			{
				ArrayBuilder<NamespaceOrTypeAndUsingDirective> instance = ArrayBuilder<NamespaceOrTypeAndUsingDirective>.GetInstance(previousSubmissionUsings.Length);
				NamespaceSymbol globalNamespace = newSubmission.GlobalNamespace;
				foreach (NamespaceOrTypeAndUsingDirective namespaceOrTypeAndUsingDirective in previousSubmissionUsings)
				{
					NamespaceOrTypeSymbol namespaceOrType = namespaceOrTypeAndUsingDirective.NamespaceOrType;
					if (namespaceOrType.IsType)
					{
						instance.Add(namespaceOrTypeAndUsingDirective);
					}
					else
					{
						NamespaceSymbol namespaceSymbol = Imports.ExpandPreviousSubmissionNamespace((NamespaceSymbol)namespaceOrType, globalNamespace);
						instance.Add(new NamespaceOrTypeAndUsingDirective(namespaceSymbol, namespaceOrTypeAndUsingDirective.UsingDirective, default(ImmutableArray<AssemblySymbol>)));
					}
				}
				return instance.ToImmutableAndFree();
			}
			return previousSubmissionUsings;
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00038CC0 File Offset: 0x00036EC0
		internal static NamespaceSymbol ExpandPreviousSubmissionNamespace(NamespaceSymbol originalNamespace, NamespaceSymbol expandedGlobalNamespace)
		{
			ArrayBuilder<string> instance = ArrayBuilder<string>.GetInstance();
			NamespaceSymbol namespaceSymbol = originalNamespace;
			while (!namespaceSymbol.IsGlobalNamespace)
			{
				instance.Add(namespaceSymbol.Name);
				namespaceSymbol = namespaceSymbol.ContainingNamespace;
			}
			NamespaceSymbol namespaceSymbol2 = expandedGlobalNamespace;
			for (int i = instance.Count - 1; i >= 0; i--)
			{
				namespaceSymbol2 = Enumerable.Single<NamespaceSymbol>(namespaceSymbol2.GetMembers(instance[i]).OfType<NamespaceSymbol>());
			}
			instance.Free();
			return namespaceSymbol2;
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x00038D2A File Offset: 0x00036F2A
		public static Imports Create(ImmutableDictionary<string, AliasAndUsingDirective> usingAliases, ImmutableArray<NamespaceOrTypeAndUsingDirective> usings, ImmutableArray<AliasAndExternAliasDirective> externs)
		{
			if (usingAliases.IsEmpty && usings.IsEmpty && externs.IsEmpty)
			{
				return Imports.Empty;
			}
			return new Imports(usingAliases, usings, externs);
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x00038D54 File Offset: 0x00036F54
		internal Imports Concat(Imports otherImports)
		{
			if (this == Imports.Empty)
			{
				return otherImports;
			}
			if (otherImports == Imports.Empty)
			{
				return this;
			}
			ImmutableDictionary<string, AliasAndUsingDirective> immutableDictionary = this.UsingAliases.SetItems(otherImports.UsingAliases);
			ImmutableArray<NamespaceOrTypeAndUsingDirective> immutableArray = this.Usings.AddRange(otherImports.Usings).Distinct<NamespaceOrTypeAndUsingDirective>(Imports.UsingTargetComparer.Instance);
			ImmutableArray<AliasAndExternAliasDirective> immutableArray2 = Imports.ConcatExternAliases(this.ExternAliases, otherImports.ExternAliases);
			return Imports.Create(immutableDictionary, immutableArray, immutableArray2);
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00038DC0 File Offset: 0x00036FC0
		private static ImmutableArray<AliasAndExternAliasDirective> ConcatExternAliases(ImmutableArray<AliasAndExternAliasDirective> externs1, ImmutableArray<AliasAndExternAliasDirective> externs2)
		{
			if (externs1.Length == 0)
			{
				return externs2;
			}
			if (externs2.Length == 0)
			{
				return externs1;
			}
			PooledHashSet<string> instance = PooledHashSet<string>.GetInstance();
			instance.AddAll<string>(externs2.Select<AliasAndExternAliasDirective, string>((AliasAndExternAliasDirective e) => e.Alias.Name));
			return externs1.WhereAsArray((AliasAndExternAliasDirective e, PooledHashSet<string> replacedExternAliases) => !replacedExternAliases.Contains(e.Alias.Name), instance).AddRange(externs2);
		}

		// Token: 0x0400007D RID: 125
		internal static readonly Imports Empty = new Imports(ImmutableDictionary<string, AliasAndUsingDirective>.Empty, ImmutableArray<NamespaceOrTypeAndUsingDirective>.Empty, ImmutableArray<AliasAndExternAliasDirective>.Empty);

		// Token: 0x0400007E RID: 126
		public readonly ImmutableDictionary<string, AliasAndUsingDirective> UsingAliases;

		// Token: 0x0400007F RID: 127
		public readonly ImmutableArray<NamespaceOrTypeAndUsingDirective> Usings;

		// Token: 0x04000080 RID: 128
		public readonly ImmutableArray<AliasAndExternAliasDirective> ExternAliases;

		// Token: 0x02000640 RID: 1600
		private class UsingTargetComparer : IEqualityComparer<NamespaceOrTypeAndUsingDirective>
		{
			// Token: 0x0600825D RID: 33373 RVA: 0x001DA273 File Offset: 0x001D8473
			private UsingTargetComparer()
			{
			}

			// Token: 0x0600825E RID: 33374 RVA: 0x001DA27B File Offset: 0x001D847B
			bool IEqualityComparer<NamespaceOrTypeAndUsingDirective>.Equals(NamespaceOrTypeAndUsingDirective x, NamespaceOrTypeAndUsingDirective y)
			{
				return x.NamespaceOrType.Equals(y.NamespaceOrType);
			}

			// Token: 0x0600825F RID: 33375 RVA: 0x001DA28E File Offset: 0x001D848E
			int IEqualityComparer<NamespaceOrTypeAndUsingDirective>.GetHashCode(NamespaceOrTypeAndUsingDirective obj)
			{
				return obj.NamespaceOrType.GetHashCode();
			}

			// Token: 0x04001F83 RID: 8067
			public static readonly IEqualityComparer<NamespaceOrTypeAndUsingDirective> Instance = new Imports.UsingTargetComparer();
		}
	}
}
