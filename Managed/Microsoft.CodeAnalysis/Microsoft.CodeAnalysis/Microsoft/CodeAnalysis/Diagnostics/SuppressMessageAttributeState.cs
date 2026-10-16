using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x0200034D RID: 845
	[NullableContext(1)]
	[Nullable(0)]
	internal class SuppressMessageAttributeState
	{
		// Token: 0x06002458 RID: 9304 RVA: 0x00071444 File Offset: 0x0006F644
		private static bool TryGetTargetScope(SuppressMessageInfo info, out SuppressMessageAttributeState.TargetScope scope)
		{
			return SuppressMessageAttributeState.s_suppressMessageScopeTypes.TryGetValue(info.Scope ?? string.Empty, out scope);
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x00071460 File Offset: 0x0006F660
		internal SuppressMessageAttributeState(Compilation compilation)
		{
			this._compilation = compilation;
			this._localSuppressionsBySymbol = new ConcurrentDictionary<ISymbol, ImmutableDictionary<string, SuppressMessageInfo>>();
		}

		// Token: 0x0600245A RID: 9306 RVA: 0x0007147C File Offset: 0x0006F67C
		public Diagnostic ApplySourceSuppressions(Diagnostic diagnostic)
		{
			if (diagnostic.IsSuppressed)
			{
				return diagnostic;
			}
			SuppressMessageInfo suppressMessageInfo;
			if (this.IsDiagnosticSuppressed(diagnostic, out suppressMessageInfo))
			{
				diagnostic = diagnostic.WithIsSuppressed(true);
			}
			return diagnostic;
		}

		// Token: 0x0600245B RID: 9307 RVA: 0x000714A8 File Offset: 0x0006F6A8
		public bool IsDiagnosticSuppressed(Diagnostic diagnostic, [Nullable(2)] [NotNullWhen(true)] out AttributeData suppressingAttribute)
		{
			SuppressMessageInfo suppressMessageInfo;
			if (this.IsDiagnosticSuppressed(diagnostic, out suppressMessageInfo))
			{
				suppressingAttribute = suppressMessageInfo.Attribute;
				return true;
			}
			suppressingAttribute = null;
			return false;
		}

		// Token: 0x0600245C RID: 9308 RVA: 0x000714D0 File Offset: 0x0006F6D0
		private bool IsDiagnosticSuppressed(Diagnostic diagnostic, out SuppressMessageInfo info)
		{
			SuppressMessageAttributeState.<>c__DisplayClass11_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			info = default(SuppressMessageInfo);
			if (diagnostic.CustomTags.Contains("Compiler", null))
			{
				return false;
			}
			CS$<>8__locals1.id = diagnostic.Id;
			Location location = diagnostic.Location;
			if (this.IsDiagnosticGloballySuppressed(CS$<>8__locals1.id, null, false, out info))
			{
				return true;
			}
			if (location.IsInSource)
			{
				SemanticModel semanticModel = this._compilation.GetSemanticModel(location.SourceTree, false);
				bool flag = true;
				for (SyntaxNode syntaxNode = location.SourceTree.GetRoot(default(CancellationToken)).FindNode(location.SourceSpan, false, true); syntaxNode != null; syntaxNode = syntaxNode.Parent)
				{
					ImmutableArray<ISymbol> declaredSymbolsForNode = semanticModel.GetDeclaredSymbolsForNode(syntaxNode, default(CancellationToken));
					foreach (ISymbol symbol in declaredSymbolsForNode)
					{
						if (symbol.Kind == SymbolKind.Namespace)
						{
							return this.<IsDiagnosticSuppressed>g__hasNamespaceSuppression|11_0((INamespaceSymbol)symbol, flag, ref CS$<>8__locals1);
						}
						if (this.IsDiagnosticLocallySuppressed(CS$<>8__locals1.id, symbol, out info) || this.IsDiagnosticGloballySuppressed(CS$<>8__locals1.id, symbol, flag, out info))
						{
							return true;
						}
					}
					if (!declaredSymbolsForNode.IsEmpty)
					{
						flag = false;
					}
				}
			}
			return false;
		}

		// Token: 0x0600245D RID: 9309 RVA: 0x00071600 File Offset: 0x0006F800
		private bool IsDiagnosticGloballySuppressed(string id, [Nullable(2)] ISymbol symbolOpt, bool isImmediatelyContainingSymbol, out SuppressMessageInfo info)
		{
			SuppressMessageAttributeState.GlobalSuppressions globalSuppressions = this.DecodeGlobalSuppressMessageAttributes();
			return globalSuppressions.HasCompilationWideSuppression(id, out info) || (symbolOpt != null && globalSuppressions.HasGlobalSymbolSuppression(symbolOpt, id, isImmediatelyContainingSymbol, out info));
		}

		// Token: 0x0600245E RID: 9310 RVA: 0x00071631 File Offset: 0x0006F831
		private bool IsDiagnosticLocallySuppressed(string id, ISymbol symbol, out SuppressMessageInfo info)
		{
			return this._localSuppressionsBySymbol.GetOrAdd(symbol, new Func<ISymbol, ImmutableDictionary<string, SuppressMessageInfo>>(this.DecodeLocalSuppressMessageAttributes)).TryGetValue(id, out info);
		}

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x0600245F RID: 9311 RVA: 0x00071652 File Offset: 0x0006F852
		[Nullable(2)]
		private ISymbol SuppressMessageAttribute
		{
			[NullableContext(2)]
			get
			{
				if (this._lazySuppressMessageAttribute == null)
				{
					Interlocked.CompareExchange<StrongBox<ISymbol>>(ref this._lazySuppressMessageAttribute, new StrongBox<ISymbol>(this._compilation.GetTypeByMetadataName("System.Diagnostics.CodeAnalysis.SuppressMessageAttribute")), null);
				}
				return this._lazySuppressMessageAttribute.Value;
			}
		}

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x06002460 RID: 9312 RVA: 0x00071689 File Offset: 0x0006F889
		[Nullable(2)]
		private ISymbol UnconditionalSuppressMessageAttribute
		{
			[NullableContext(2)]
			get
			{
				if (this._lazyUnconditionalSuppressMessageAttribute == null)
				{
					Interlocked.CompareExchange<StrongBox<ISymbol>>(ref this._lazyUnconditionalSuppressMessageAttribute, new StrongBox<ISymbol>(this._compilation.GetTypeByMetadataName("System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessageAttribute")), null);
				}
				return this._lazyUnconditionalSuppressMessageAttribute.Value;
			}
		}

		// Token: 0x06002461 RID: 9313 RVA: 0x000716C0 File Offset: 0x0006F8C0
		private SuppressMessageAttributeState.GlobalSuppressions DecodeGlobalSuppressMessageAttributes()
		{
			if (this._lazyGlobalSuppressions == null)
			{
				SuppressMessageAttributeState.GlobalSuppressions globalSuppressions = new SuppressMessageAttributeState.GlobalSuppressions();
				this.DecodeGlobalSuppressMessageAttributes(this._compilation, this._compilation.Assembly, globalSuppressions);
				foreach (IModuleSymbol moduleSymbol in this._compilation.Assembly.Modules)
				{
					this.DecodeGlobalSuppressMessageAttributes(this._compilation, moduleSymbol, globalSuppressions);
				}
				Interlocked.CompareExchange<SuppressMessageAttributeState.GlobalSuppressions>(ref this._lazyGlobalSuppressions, globalSuppressions, null);
			}
			return this._lazyGlobalSuppressions;
		}

		// Token: 0x06002462 RID: 9314 RVA: 0x00071758 File Offset: 0x0006F958
		private bool IsSuppressionAttribute(AttributeData a)
		{
			return a.AttributeClass == this.SuppressMessageAttribute || a.AttributeClass == this.UnconditionalSuppressMessageAttribute;
		}

		// Token: 0x06002463 RID: 9315 RVA: 0x00071778 File Offset: 0x0006F978
		private ImmutableDictionary<string, SuppressMessageInfo> DecodeLocalSuppressMessageAttributes(ISymbol symbol)
		{
			IEnumerable<AttributeData> enumerable = from a in symbol.GetAttributes()
				where this.IsSuppressionAttribute(a)
				select a;
			return SuppressMessageAttributeState.DecodeLocalSuppressMessageAttributes(symbol, enumerable);
		}

		// Token: 0x06002464 RID: 9316 RVA: 0x000717A4 File Offset: 0x0006F9A4
		private static ImmutableDictionary<string, SuppressMessageInfo> DecodeLocalSuppressMessageAttributes(ISymbol symbol, IEnumerable<AttributeData> attributes)
		{
			ImmutableDictionary<string, SuppressMessageInfo>.Builder builder = ImmutableDictionary.CreateBuilder<string, SuppressMessageInfo>();
			using (IEnumerator<AttributeData> enumerator = attributes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					SuppressMessageInfo suppressMessageInfo;
					if (SuppressMessageAttributeState.TryDecodeSuppressMessageAttributeData(enumerator.Current, out suppressMessageInfo))
					{
						SuppressMessageAttributeState.AddOrUpdate(suppressMessageInfo, builder);
					}
				}
			}
			return builder.ToImmutable();
		}

		// Token: 0x06002465 RID: 9317 RVA: 0x00071800 File Offset: 0x0006FA00
		private static void AddOrUpdate(SuppressMessageInfo info, IDictionary<string, SuppressMessageInfo> builder)
		{
			SuppressMessageInfo suppressMessageInfo;
			if (!builder.TryGetValue(info.Id, ref suppressMessageInfo))
			{
				builder[info.Id] = info;
			}
		}

		// Token: 0x06002466 RID: 9318 RVA: 0x0007182C File Offset: 0x0006FA2C
		private void DecodeGlobalSuppressMessageAttributes(Compilation compilation, ISymbol symbol, SuppressMessageAttributeState.GlobalSuppressions globalSuppressions)
		{
			IEnumerable<AttributeData> enumerable = from a in symbol.GetAttributes()
				where this.IsSuppressionAttribute(a)
				select a;
			SuppressMessageAttributeState.DecodeGlobalSuppressMessageAttributes(compilation, symbol, globalSuppressions, enumerable);
		}

		// Token: 0x06002467 RID: 9319 RVA: 0x0007185C File Offset: 0x0006FA5C
		private static void DecodeGlobalSuppressMessageAttributes(Compilation compilation, ISymbol symbol, SuppressMessageAttributeState.GlobalSuppressions globalSuppressions, IEnumerable<AttributeData> attributes)
		{
			using (IEnumerator<AttributeData> enumerator = attributes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					SuppressMessageInfo suppressMessageInfo;
					SuppressMessageAttributeState.TargetScope targetScope;
					if (SuppressMessageAttributeState.TryDecodeSuppressMessageAttributeData(enumerator.Current, out suppressMessageInfo) && SuppressMessageAttributeState.TryGetTargetScope(suppressMessageInfo, out targetScope))
					{
						if ((targetScope == SuppressMessageAttributeState.TargetScope.Module || targetScope == SuppressMessageAttributeState.TargetScope.None) && suppressMessageInfo.Target == null)
						{
							globalSuppressions.AddCompilationWideSuppression(suppressMessageInfo);
						}
						else if (suppressMessageInfo.Target != null)
						{
							foreach (ISymbol symbol2 in SuppressMessageAttributeState.ResolveTargetSymbols(compilation, suppressMessageInfo.Target, targetScope))
							{
								globalSuppressions.AddGlobalSymbolSuppression(symbol2, suppressMessageInfo);
							}
						}
					}
				}
			}
		}

		// Token: 0x06002468 RID: 9320 RVA: 0x00071904 File Offset: 0x0006FB04
		[return: Nullable(new byte[] { 0, 1 })]
		internal static ImmutableArray<ISymbol> ResolveTargetSymbols(Compilation compilation, string target, SuppressMessageAttributeState.TargetScope scope)
		{
			switch (scope)
			{
			case SuppressMessageAttributeState.TargetScope.Namespace:
			case SuppressMessageAttributeState.TargetScope.Type:
			case SuppressMessageAttributeState.TargetScope.Member:
			{
				bool flag;
				return new SuppressMessageAttributeState.TargetSymbolResolver(compilation, scope, target).Resolve(out flag);
			}
			case SuppressMessageAttributeState.TargetScope.NamespaceAndDescendants:
				return SuppressMessageAttributeState.ResolveTargetSymbols(compilation, target, SuppressMessageAttributeState.TargetScope.Namespace);
			}
			return ImmutableArray<ISymbol>.Empty;
		}

		// Token: 0x06002469 RID: 9321 RVA: 0x00071950 File Offset: 0x0006FB50
		private static bool TryDecodeSuppressMessageAttributeData(AttributeData attribute, out SuppressMessageInfo info)
		{
			info = default(SuppressMessageInfo);
			if (attribute.CommonConstructorArguments.Length < 2)
			{
				return false;
			}
			info.Id = attribute.CommonConstructorArguments[1].ValueInternal as string;
			if (info.Id == null)
			{
				return false;
			}
			int num = info.Id.IndexOf(':');
			if (num != -1)
			{
				info.Id = info.Id.Remove(num);
			}
			info.Scope = attribute.DecodeNamedArgument<string>("Scope", SpecialType.System_String, null);
			info.Target = attribute.DecodeNamedArgument<string>("Target", SpecialType.System_String, null);
			info.MessageId = attribute.DecodeNamedArgument<string>("MessageId", SpecialType.System_String, null);
			info.Attribute = attribute;
			return true;
		}

		// Token: 0x0600246B RID: 9323 RVA: 0x00071A7C File Offset: 0x0006FC7C
		[CompilerGenerated]
		private bool <IsDiagnosticSuppressed>g__hasNamespaceSuppression|11_0(INamespaceSymbol namespaceSymbol, bool inImmediatelyContainingSymbol, ref SuppressMessageAttributeState.<>c__DisplayClass11_0 A_3)
		{
			SuppressMessageInfo suppressMessageInfo;
			while (!this.IsDiagnosticGloballySuppressed(A_3.id, namespaceSymbol, inImmediatelyContainingSymbol, out suppressMessageInfo))
			{
				namespaceSymbol = namespaceSymbol.ContainingNamespace;
				inImmediatelyContainingSymbol = false;
				if (namespaceSymbol == null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000F80 RID: 3968
		private static readonly SmallDictionary<string, SuppressMessageAttributeState.TargetScope> s_suppressMessageScopeTypes = new SmallDictionary<string, SuppressMessageAttributeState.TargetScope>(StringComparer.OrdinalIgnoreCase)
		{
			{
				string.Empty,
				SuppressMessageAttributeState.TargetScope.None
			},
			{
				"module",
				SuppressMessageAttributeState.TargetScope.Module
			},
			{
				"namespace",
				SuppressMessageAttributeState.TargetScope.Namespace
			},
			{
				"resource",
				SuppressMessageAttributeState.TargetScope.Resource
			},
			{
				"type",
				SuppressMessageAttributeState.TargetScope.Type
			},
			{
				"member",
				SuppressMessageAttributeState.TargetScope.Member
			},
			{
				"namespaceanddescendants",
				SuppressMessageAttributeState.TargetScope.NamespaceAndDescendants
			}
		};

		// Token: 0x04000F81 RID: 3969
		private readonly Compilation _compilation;

		// Token: 0x04000F82 RID: 3970
		[Nullable(2)]
		private SuppressMessageAttributeState.GlobalSuppressions _lazyGlobalSuppressions;

		// Token: 0x04000F83 RID: 3971
		private readonly ConcurrentDictionary<ISymbol, ImmutableDictionary<string, SuppressMessageInfo>> _localSuppressionsBySymbol;

		// Token: 0x04000F84 RID: 3972
		[Nullable(2)]
		private StrongBox<ISymbol> _lazySuppressMessageAttribute;

		// Token: 0x04000F85 RID: 3973
		[Nullable(2)]
		private StrongBox<ISymbol> _lazyUnconditionalSuppressMessageAttribute;

		// Token: 0x04000F86 RID: 3974
		[Nullable(0)]
		private const string s_suppressionPrefix = "~";

		// Token: 0x02000718 RID: 1816
		[Nullable(0)]
		private class GlobalSuppressions
		{
			// Token: 0x06003B5D RID: 15197 RVA: 0x000A9F42 File Offset: 0x000A8142
			public void AddCompilationWideSuppression(SuppressMessageInfo info)
			{
				SuppressMessageAttributeState.AddOrUpdate(info, this._compilationWideSuppressions);
			}

			// Token: 0x06003B5E RID: 15198 RVA: 0x000A9F50 File Offset: 0x000A8150
			public void AddGlobalSymbolSuppression(ISymbol symbol, SuppressMessageInfo info)
			{
				Dictionary<string, SuppressMessageInfo> dictionary;
				if (this._globalSymbolSuppressions.TryGetValue(symbol, ref dictionary))
				{
					SuppressMessageAttributeState.AddOrUpdate(info, dictionary);
					return;
				}
				Dictionary<string, SuppressMessageInfo> dictionary2 = new Dictionary<string, SuppressMessageInfo>();
				dictionary2.Add(info.Id, info);
				dictionary = dictionary2;
				this._globalSymbolSuppressions.Add(symbol, dictionary);
			}

			// Token: 0x06003B5F RID: 15199 RVA: 0x000A9F95 File Offset: 0x000A8195
			public bool HasCompilationWideSuppression(string id, out SuppressMessageInfo info)
			{
				return this._compilationWideSuppressions.TryGetValue(id, ref info);
			}

			// Token: 0x06003B60 RID: 15200 RVA: 0x000A9FA4 File Offset: 0x000A81A4
			public bool HasGlobalSymbolSuppression(ISymbol symbol, string id, bool isImmediatelyContainingSymbol, out SuppressMessageInfo info)
			{
				Dictionary<string, SuppressMessageInfo> dictionary;
				if (this._globalSymbolSuppressions.TryGetValue(symbol, ref dictionary) && dictionary.TryGetValue(id, ref info))
				{
					if (symbol.Kind != SymbolKind.Namespace)
					{
						return true;
					}
					SuppressMessageAttributeState.TargetScope targetScope;
					if (SuppressMessageAttributeState.TryGetTargetScope(info, out targetScope))
					{
						if (targetScope == SuppressMessageAttributeState.TargetScope.Namespace)
						{
							return isImmediatelyContainingSymbol;
						}
						if (targetScope == SuppressMessageAttributeState.TargetScope.NamespaceAndDescendants)
						{
							return true;
						}
					}
				}
				info = default(SuppressMessageInfo);
				return false;
			}

			// Token: 0x04001C44 RID: 7236
			private readonly Dictionary<string, SuppressMessageInfo> _compilationWideSuppressions = new Dictionary<string, SuppressMessageInfo>();

			// Token: 0x04001C45 RID: 7237
			private readonly Dictionary<ISymbol, Dictionary<string, SuppressMessageInfo>> _globalSymbolSuppressions = new Dictionary<ISymbol, Dictionary<string, SuppressMessageInfo>>();
		}

		// Token: 0x02000719 RID: 1817
		[NullableContext(0)]
		internal enum TargetScope
		{
			// Token: 0x04001C47 RID: 7239
			None,
			// Token: 0x04001C48 RID: 7240
			Module,
			// Token: 0x04001C49 RID: 7241
			Namespace,
			// Token: 0x04001C4A RID: 7242
			Resource,
			// Token: 0x04001C4B RID: 7243
			Type,
			// Token: 0x04001C4C RID: 7244
			Member,
			// Token: 0x04001C4D RID: 7245
			NamespaceAndDescendants
		}

		// Token: 0x0200071A RID: 1818
		[NullableContext(0)]
		[StructLayout(3)]
		private struct TargetSymbolResolver
		{
			// Token: 0x06003B62 RID: 15202 RVA: 0x000AA01D File Offset: 0x000A821D
			public TargetSymbolResolver(Compilation compilation, SuppressMessageAttributeState.TargetScope scope, string fullyQualifiedName)
			{
				this._compilation = compilation;
				this._scope = scope;
				this._name = fullyQualifiedName;
				this._index = 0;
			}

			// Token: 0x06003B63 RID: 15203 RVA: 0x000AA03C File Offset: 0x000A823C
			private static string RemovePrefix(string id, string prefix)
			{
				if (id != null && prefix != null && id.StartsWith(prefix, 4))
				{
					int length = id.Length;
					int length2 = prefix.Length;
					int num = length - length2;
					return id.Substring(length2, num);
				}
				return id;
			}

			// Token: 0x06003B64 RID: 15204 RVA: 0x000AA074 File Offset: 0x000A8274
			public ImmutableArray<ISymbol> Resolve(out bool resolvedWithDocCommentIdFormat)
			{
				resolvedWithDocCommentIdFormat = false;
				if (string.IsNullOrEmpty(this._name))
				{
					return ImmutableArray<ISymbol>.Empty;
				}
				ImmutableArray<ISymbol> symbolsForDeclarationId = DocumentationCommentId.GetSymbolsForDeclarationId(SuppressMessageAttributeState.TargetSymbolResolver.RemovePrefix(this._name, "~"), this._compilation);
				if (symbolsForDeclarationId.Length > 0)
				{
					resolvedWithDocCommentIdFormat = true;
					return symbolsForDeclarationId;
				}
				ArrayBuilder<ISymbol> instance = ArrayBuilder<ISymbol>.GetInstance();
				bool flag = false;
				if (this._name.Length >= 2 && this._name.get_Chars(0) == 'e' && this._name.get_Chars(1) == ':')
				{
					flag = true;
					this._index = 2;
				}
				INamespaceOrTypeSymbol namespaceOrTypeSymbol = this._compilation.GlobalNamespace;
				bool? flag2 = default(bool?);
				bool flag3;
				ImmutableArray<ISymbol> immutableArray;
				int? num;
				SuppressMessageAttributeState.TargetSymbolResolver.ParameterInfo[] array;
				for (;;)
				{
					string text = this.ParseNextNameSegment();
					flag3 = false;
					if (text == "Item" && this.PeekNextChar() == '[')
					{
						flag3 = true;
						if (this._compilation.Language == "C#")
						{
							text = "this[]";
						}
					}
					immutableArray = namespaceOrTypeSymbol.GetMembers(text);
					if (immutableArray.Length == 0)
					{
						goto IL_03A1;
					}
					if (flag2 != null)
					{
						ImmutableArray<ISymbol> immutableArray2;
						if (!flag2.Value)
						{
							immutableArray2 = immutableArray.Where<ISymbol>((ISymbol s) => s.Kind != SymbolKind.NamedType).ToImmutableArray<ISymbol>();
						}
						else
						{
							immutableArray2 = immutableArray.Where<ISymbol>((ISymbol s) => s.Kind == SymbolKind.NamedType).ToImmutableArray<ISymbol>();
						}
						immutableArray = immutableArray2;
						flag2 = default(bool?);
					}
					num = default(int?);
					array = null;
					if (this._scope != SuppressMessageAttributeState.TargetScope.Namespace && this.PeekNextChar() == '`')
					{
						this._index++;
						num = new int?(this.ReadNextInteger());
					}
					char c = this.PeekNextChar();
					if ((!flag3 && c == '(') || (flag3 && c == '['))
					{
						break;
					}
					if (c != '.' && c != '+')
					{
						goto IL_0242;
					}
					this._index++;
					int? num2 = num;
					int num3 = 0;
					if (((num2.GetValueOrDefault() > num3) & (num2 != null)) || c == '+')
					{
						namespaceOrTypeSymbol = SuppressMessageAttributeState.TargetSymbolResolver.GetFirstMatchingNamedType(immutableArray, num.GetValueOrDefault());
					}
					else
					{
						namespaceOrTypeSymbol = SuppressMessageAttributeState.TargetSymbolResolver.GetFirstMatchingNamespaceOrType(immutableArray);
					}
					if (namespaceOrTypeSymbol == null)
					{
						goto IL_03A1;
					}
					if (namespaceOrTypeSymbol.Kind == SymbolKind.NamedType)
					{
						flag2 = new bool?(c == '+');
					}
				}
				array = this.ParseParameterList();
				if (array == null)
				{
					goto IL_03A1;
				}
				IL_0242:
				if (this._scope == SuppressMessageAttributeState.TargetScope.Member && !flag3 && array != null)
				{
					SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo? typeInfo = default(SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo?);
					if (this.PeekNextChar() == ':')
					{
						this._index++;
						typeInfo = this.ParseNamedType(null);
					}
					foreach (IMethodSymbol methodSymbol in this.GetMatchingMethods(immutableArray, num, array, typeInfo))
					{
						instance.Add(methodSymbol);
					}
				}
				else
				{
					ISymbol symbol;
					switch (this._scope)
					{
					case SuppressMessageAttributeState.TargetScope.Namespace:
						symbol = immutableArray.FirstOrDefault<ISymbol>((ISymbol s) => s.Kind == SymbolKind.Namespace);
						goto IL_0395;
					case SuppressMessageAttributeState.TargetScope.Type:
						symbol = SuppressMessageAttributeState.TargetSymbolResolver.GetFirstMatchingNamedType(immutableArray, num.GetValueOrDefault());
						goto IL_0395;
					case SuppressMessageAttributeState.TargetScope.Member:
						if (flag3)
						{
							symbol = this.GetFirstMatchingIndexer(immutableArray, array);
							goto IL_0395;
						}
						if (flag)
						{
							symbol = immutableArray.FirstOrDefault<ISymbol>((ISymbol s) => s.Kind == SymbolKind.Event);
							goto IL_0395;
						}
						symbol = immutableArray.FirstOrDefault<ISymbol>((ISymbol s) => s.Kind != SymbolKind.Namespace && s.Kind != SymbolKind.NamedType);
						goto IL_0395;
					}
					throw ExceptionUtilities.UnexpectedValue(this._scope);
					IL_0395:
					if (symbol != null)
					{
						instance.Add(symbol);
					}
				}
				IL_03A1:
				return instance.ToImmutableAndFree();
			}

			// Token: 0x06003B65 RID: 15205 RVA: 0x000AA428 File Offset: 0x000A8628
			private string ParseNextNameSegment()
			{
				if (this.PeekNextChar() == '#')
				{
					this._index++;
					if (this.PeekNextChar() == '[')
					{
						foreach (string text in SuppressMessageAttributeState.TargetSymbolResolver.s_callingConventionStrings)
						{
							if (text == this._name.Substring(this._index, text.Length))
							{
								this._index += text.Length;
								break;
							}
						}
					}
				}
				int num = ((this.PeekNextChar() == '.') ? this._name.IndexOfAny(SuppressMessageAttributeState.TargetSymbolResolver.s_nameDelimiters, this._index + 1) : this._name.IndexOfAny(SuppressMessageAttributeState.TargetSymbolResolver.s_nameDelimiters, this._index));
				string text2;
				if (num >= 0)
				{
					string name = this._name;
					int i = this._index;
					int num2 = num - i;
					text2 = name.Substring(i, num2);
					this._index = num;
				}
				else
				{
					string name2 = this._name;
					int length = name2.Length;
					int num2 = this._index;
					int i = length - num2;
					text2 = name2.Substring(num2, i);
					this._index = this._name.Length;
				}
				return text2;
			}

			// Token: 0x06003B66 RID: 15206 RVA: 0x000AA53D File Offset: 0x000A873D
			private char PeekNextChar()
			{
				if (this._index < this._name.Length)
				{
					return this._name.get_Chars(this._index);
				}
				return '\0';
			}

			// Token: 0x06003B67 RID: 15207 RVA: 0x000AA568 File Offset: 0x000A8768
			private int ReadNextInteger()
			{
				int num = 0;
				while (this._index < this._name.Length && char.IsDigit(this._name.get_Chars(this._index)))
				{
					num = num * 10 + (int)(this._name.get_Chars(this._index) - '0');
					this._index++;
				}
				return num;
			}

			// Token: 0x06003B68 RID: 15208 RVA: 0x000AA5D0 File Offset: 0x000A87D0
			private SuppressMessageAttributeState.TargetSymbolResolver.ParameterInfo[] ParseParameterList()
			{
				this._index++;
				char c = this.PeekNextChar();
				if (c == ')' || c == ']')
				{
					this._index++;
					return SuppressMessageAttributeState.TargetSymbolResolver.s_noParameters;
				}
				ArrayBuilder<SuppressMessageAttributeState.TargetSymbolResolver.ParameterInfo> arrayBuilder = new ArrayBuilder<SuppressMessageAttributeState.TargetSymbolResolver.ParameterInfo>();
				for (;;)
				{
					SuppressMessageAttributeState.TargetSymbolResolver.ParameterInfo? parameterInfo = this.ParseParameter();
					if (parameterInfo == null)
					{
						break;
					}
					arrayBuilder.Add(parameterInfo.Value);
					if (this.PeekNextChar() != ',')
					{
						goto IL_007A;
					}
					this._index++;
				}
				arrayBuilder.Free();
				return null;
				IL_007A:
				c = this.PeekNextChar();
				if (c == ')' || c == ']')
				{
					this._index++;
					return arrayBuilder.ToArrayAndFree();
				}
				arrayBuilder.Free();
				return null;
			}

			// Token: 0x06003B69 RID: 15209 RVA: 0x000AA688 File Offset: 0x000A8888
			private SuppressMessageAttributeState.TargetSymbolResolver.ParameterInfo? ParseParameter()
			{
				SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo? typeInfo = this.ParseType(null);
				if (typeInfo == null)
				{
					return default(SuppressMessageAttributeState.TargetSymbolResolver.ParameterInfo?);
				}
				bool flag = this.PeekNextChar() == '&';
				if (flag)
				{
					this._index++;
				}
				return new SuppressMessageAttributeState.TargetSymbolResolver.ParameterInfo?(new SuppressMessageAttributeState.TargetSymbolResolver.ParameterInfo(typeInfo.Value, flag));
			}

			// Token: 0x06003B6A RID: 15210 RVA: 0x000AA6E0 File Offset: 0x000A88E0
			private SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo? ParseType(ISymbol bindingContext)
			{
				this.IgnoreCustomModifierList();
				SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo? typeInfo;
				if (this.PeekNextChar() == '!')
				{
					typeInfo = this.ParseIndexedTypeParameter(bindingContext);
				}
				else
				{
					typeInfo = this.ParseNamedType(bindingContext);
					if (bindingContext != null && typeInfo != null && !typeInfo.Value.IsBound)
					{
						this._index = typeInfo.Value.StartIndex;
						typeInfo = this.ParseNamedTypeParameter(bindingContext);
					}
				}
				if (typeInfo == null)
				{
					return default(SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo?);
				}
				if (typeInfo.Value.IsBound)
				{
					ITypeSymbol typeSymbol = typeInfo.Value.Type;
					for (;;)
					{
						this.IgnoreCustomModifierList();
						char c = this.PeekNextChar();
						if (c == '[')
						{
							typeSymbol = this.ParseArrayType(typeSymbol);
							if (typeSymbol == null)
							{
								break;
							}
						}
						else
						{
							if (c != '*')
							{
								goto IL_00D6;
							}
							this._index++;
							typeSymbol = this._compilation.CreatePointerTypeSymbol(typeSymbol);
						}
					}
					return default(SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo?);
					IL_00D6:
					return new SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo?(SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo.Create(typeSymbol));
				}
				this.IgnorePointerAndArraySpecifiers();
				return typeInfo;
			}

			// Token: 0x06003B6B RID: 15211 RVA: 0x000AA7D8 File Offset: 0x000A89D8
			private void IgnoreCustomModifierList()
			{
				if (this.PeekNextChar() == '{')
				{
					while (this._index < this._name.Length && this._name.get_Chars(this._index) != '}')
					{
						this._index++;
					}
				}
			}

			// Token: 0x06003B6C RID: 15212 RVA: 0x000AA828 File Offset: 0x000A8A28
			private void IgnorePointerAndArraySpecifiers()
			{
				bool flag = false;
				while (this._index < this._name.Length)
				{
					char c = this.PeekNextChar();
					if (c != '*')
					{
						if (c != '[')
						{
							if (c != ']')
							{
								if (!flag)
								{
									return;
								}
							}
							else
							{
								if (!flag)
								{
									return;
								}
								flag = false;
							}
						}
						else
						{
							flag = true;
						}
					}
					this._index++;
				}
			}

			// Token: 0x06003B6D RID: 15213 RVA: 0x000AA884 File Offset: 0x000A8A84
			private SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo? ParseIndexedTypeParameter(ISymbol bindingContext)
			{
				int index = this._index;
				this._index++;
				if (this.PeekNextChar() == '!')
				{
					this._index++;
					int num = this.ReadNextInteger();
					IMethodSymbol methodSymbol = bindingContext as IMethodSymbol;
					if (methodSymbol == null)
					{
						return new SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo?(SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo.CreateUnbound(index));
					}
					int length = methodSymbol.TypeParameters.Length;
					if (length > 0 && num < length)
					{
						return new SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo?(SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo.Create(methodSymbol.TypeParameters[num]));
					}
					return default(SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo?);
				}
				else
				{
					int num2 = this.ReadNextInteger();
					if (bindingContext == null)
					{
						return new SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo?(SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo.CreateUnbound(index));
					}
					ITypeParameterSymbol nthTypeParameter = SuppressMessageAttributeState.TargetSymbolResolver.GetNthTypeParameter(bindingContext.ContainingType, num2);
					if (nthTypeParameter != null)
					{
						return new SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo?(SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo.Create(nthTypeParameter));
					}
					return default(SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo?);
				}
			}

			// Token: 0x06003B6E RID: 15214 RVA: 0x000AA95C File Offset: 0x000A8B5C
			private SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo? ParseNamedTypeParameter(ISymbol bindingContext)
			{
				string text = this.ParseNextNameSegment();
				IMethodSymbol methodSymbol = bindingContext as IMethodSymbol;
				if (methodSymbol != null)
				{
					for (int i = 0; i < methodSymbol.TypeParameters.Length; i++)
					{
						if (methodSymbol.TypeParameters[i].Name == text)
						{
							return new SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo?(SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo.Create(methodSymbol.TypeArguments[i]));
						}
					}
				}
				for (INamedTypeSymbol namedTypeSymbol = bindingContext.ContainingType; namedTypeSymbol != null; namedTypeSymbol = namedTypeSymbol.ContainingType)
				{
					for (int j = 0; j < namedTypeSymbol.TypeParameters.Length; j++)
					{
						if (namedTypeSymbol.TypeParameters[j].Name == text)
						{
							return new SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo?(SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo.Create(namedTypeSymbol.TypeArguments[j]));
						}
					}
				}
				return default(SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo?);
			}

			// Token: 0x06003B6F RID: 15215 RVA: 0x000AAA44 File Offset: 0x000A8C44
			private SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo? ParseNamedType(ISymbol bindingContext)
			{
				INamespaceOrTypeSymbol namespaceOrTypeSymbol = this._compilation.GlobalNamespace;
				int index = this._index;
				ImmutableArray<ISymbol> members;
				int num;
				SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo[] array;
				for (;;)
				{
					string text = this.ParseNextNameSegment();
					members = namespaceOrTypeSymbol.GetMembers(text);
					if (members.Length == 0)
					{
						break;
					}
					num = 0;
					array = null;
					if (this.PeekNextChar() == '`')
					{
						this._index++;
						num = this.ReadNextInteger();
					}
					if (this.PeekNextChar() == '<')
					{
						array = this.ParseTypeArgumentList(bindingContext);
						if (array == null)
						{
							goto Block_4;
						}
						if (Enumerable.Any<SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo>(array, (SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo a) => !a.IsBound))
						{
							goto Block_6;
						}
					}
					char c = this.PeekNextChar();
					if (c != '.' && c != '+')
					{
						goto IL_0103;
					}
					this._index++;
					if (num > 0 || c == '+')
					{
						namespaceOrTypeSymbol = SuppressMessageAttributeState.TargetSymbolResolver.GetFirstMatchingNamedType(members, num);
					}
					else
					{
						namespaceOrTypeSymbol = SuppressMessageAttributeState.TargetSymbolResolver.GetFirstMatchingNamespaceOrType(members);
					}
					if (namespaceOrTypeSymbol == null)
					{
						goto Block_9;
					}
				}
				return new SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo?(SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo.CreateUnbound(index));
				Block_4:
				return default(SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo?);
				Block_6:
				return new SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo?(SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo.CreateUnbound(index));
				Block_9:
				return default(SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo?);
				IL_0103:
				INamedTypeSymbol namedTypeSymbol = SuppressMessageAttributeState.TargetSymbolResolver.GetFirstMatchingNamedType(members, num);
				if (namedTypeSymbol == null)
				{
					return default(SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo?);
				}
				if (array != null)
				{
					namedTypeSymbol = namedTypeSymbol.Construct(Enumerable.ToArray<ITypeSymbol>(Enumerable.Select<SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo, ITypeSymbol>(array, (SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo t) => t.Type)));
				}
				return new SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo?(SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo.Create(namedTypeSymbol));
			}

			// Token: 0x06003B70 RID: 15216 RVA: 0x000AABB4 File Offset: 0x000A8DB4
			private SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo[] ParseTypeArgumentList(ISymbol bindingContext)
			{
				this._index++;
				ArrayBuilder<SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo> arrayBuilder = new ArrayBuilder<SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo>();
				for (;;)
				{
					SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo? typeInfo = this.ParseType(bindingContext);
					if (typeInfo == null)
					{
						break;
					}
					arrayBuilder.Add(typeInfo.Value);
					if (this.PeekNextChar() != ',')
					{
						goto IL_0054;
					}
					this._index++;
				}
				arrayBuilder.Free();
				return null;
				IL_0054:
				if (this.PeekNextChar() == '>')
				{
					this._index++;
					return arrayBuilder.ToArrayAndFree();
				}
				arrayBuilder.Free();
				return null;
			}

			// Token: 0x06003B71 RID: 15217 RVA: 0x000AAC40 File Offset: 0x000A8E40
			private ITypeSymbol ParseArrayType(ITypeSymbol typeSymbol)
			{
				this._index++;
				int num = 1;
				for (;;)
				{
					char c = this.PeekNextChar();
					if (c == ',')
					{
						num++;
					}
					else
					{
						if (c == ']')
						{
							break;
						}
						if (!char.IsDigit(c) && c != '.')
						{
							goto Block_4;
						}
					}
					this._index++;
				}
				this._index++;
				return this._compilation.CreateArrayTypeSymbol(typeSymbol, num);
				Block_4:
				return null;
			}

			// Token: 0x06003B72 RID: 15218 RVA: 0x000AACB0 File Offset: 0x000A8EB0
			private ISymbol GetFirstMatchingIndexer(ImmutableArray<ISymbol> candidateMembers, SuppressMessageAttributeState.TargetSymbolResolver.ParameterInfo[] parameters)
			{
				foreach (ISymbol symbol in candidateMembers)
				{
					IPropertySymbol propertySymbol = symbol as IPropertySymbol;
					if (propertySymbol != null && this.AllParametersMatch(propertySymbol.Parameters, parameters))
					{
						return propertySymbol;
					}
				}
				return null;
			}

			// Token: 0x06003B73 RID: 15219 RVA: 0x000AACF4 File Offset: 0x000A8EF4
			private ImmutableArray<IMethodSymbol> GetMatchingMethods(ImmutableArray<ISymbol> candidateMembers, int? arity, SuppressMessageAttributeState.TargetSymbolResolver.ParameterInfo[] parameters, SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo? returnType)
			{
				ArrayBuilder<IMethodSymbol> arrayBuilder = new ArrayBuilder<IMethodSymbol>();
				foreach (ISymbol symbol in candidateMembers)
				{
					IMethodSymbol methodSymbol = symbol as IMethodSymbol;
					if (methodSymbol != null)
					{
						if (arity != null)
						{
							int arity2 = methodSymbol.Arity;
							int? num = arity;
							if (!((arity2 == num.GetValueOrDefault()) & (num != null)))
							{
								continue;
							}
						}
						if (this.AllParametersMatch(methodSymbol.Parameters, parameters))
						{
							if (returnType == null)
							{
								arrayBuilder.Add(methodSymbol);
							}
							else
							{
								ITypeSymbol typeSymbol = this.BindParameterOrReturnType(methodSymbol, returnType.Value);
								if (typeSymbol != null && methodSymbol.ReturnType.Equals(typeSymbol))
								{
									arrayBuilder.Add(methodSymbol);
								}
							}
						}
					}
				}
				return arrayBuilder.ToImmutableAndFree();
			}

			// Token: 0x06003B74 RID: 15220 RVA: 0x000AADA4 File Offset: 0x000A8FA4
			private bool AllParametersMatch(ImmutableArray<IParameterSymbol> symbolParameters, SuppressMessageAttributeState.TargetSymbolResolver.ParameterInfo[] expectedParameters)
			{
				if (symbolParameters.Length != expectedParameters.Length)
				{
					return false;
				}
				for (int i = 0; i < expectedParameters.Length; i++)
				{
					if (!this.ParameterMatches(symbolParameters[i], expectedParameters[i]))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06003B75 RID: 15221 RVA: 0x000AADE8 File Offset: 0x000A8FE8
			private bool ParameterMatches(IParameterSymbol symbol, SuppressMessageAttributeState.TargetSymbolResolver.ParameterInfo parameterInfo)
			{
				if (symbol.RefKind == RefKind.None == parameterInfo.IsRefOrOut)
				{
					return false;
				}
				ITypeSymbol typeSymbol = this.BindParameterOrReturnType(symbol.ContainingSymbol, parameterInfo.Type);
				return typeSymbol != null && symbol.Type.Equals(typeSymbol);
			}

			// Token: 0x06003B76 RID: 15222 RVA: 0x000AAE2C File Offset: 0x000A902C
			private ITypeSymbol BindParameterOrReturnType(ISymbol bindingContext, SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo type)
			{
				if (type.IsBound)
				{
					return type.Type;
				}
				int index = this._index;
				this._index = type.StartIndex;
				SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo? typeInfo = this.ParseType(bindingContext);
				this._index = index;
				if (typeInfo == null)
				{
					return null;
				}
				return typeInfo.GetValueOrDefault().Type;
			}

			// Token: 0x06003B77 RID: 15223 RVA: 0x000AAE84 File Offset: 0x000A9084
			private static INamedTypeSymbol GetFirstMatchingNamedType(ImmutableArray<ISymbol> candidateMembers, int arity)
			{
				return (INamedTypeSymbol)candidateMembers.FirstOrDefault<ISymbol>((ISymbol s) => s.Kind == SymbolKind.NamedType && ((INamedTypeSymbol)s).Arity == arity);
			}

			// Token: 0x06003B78 RID: 15224 RVA: 0x000AAEB5 File Offset: 0x000A90B5
			private static INamespaceOrTypeSymbol GetFirstMatchingNamespaceOrType(ImmutableArray<ISymbol> candidateMembers)
			{
				return (INamespaceOrTypeSymbol)candidateMembers.FirstOrDefault<ISymbol>((ISymbol s) => s.Kind == SymbolKind.Namespace || s.Kind == SymbolKind.NamedType);
			}

			// Token: 0x06003B79 RID: 15225 RVA: 0x000AAEE4 File Offset: 0x000A90E4
			private static ITypeParameterSymbol GetNthTypeParameter(INamedTypeSymbol typeSymbol, int n)
			{
				int typeParameterCount = SuppressMessageAttributeState.TargetSymbolResolver.GetTypeParameterCount(typeSymbol.ContainingType);
				if (n < typeParameterCount)
				{
					return SuppressMessageAttributeState.TargetSymbolResolver.GetNthTypeParameter(typeSymbol.ContainingType, n);
				}
				return typeSymbol.TypeParameters[n - typeParameterCount];
			}

			// Token: 0x06003B7A RID: 15226 RVA: 0x000AAF20 File Offset: 0x000A9120
			private static int GetTypeParameterCount(INamedTypeSymbol typeSymbol)
			{
				if (typeSymbol == null)
				{
					return 0;
				}
				return typeSymbol.TypeParameters.Length + SuppressMessageAttributeState.TargetSymbolResolver.GetTypeParameterCount(typeSymbol.ContainingType);
			}

			// Token: 0x04001C4E RID: 7246
			private static readonly char[] s_nameDelimiters = new char[]
			{
				':', '.', '+', '(', ')', '<', '>', '[', ']', '{',
				'}', ',', '&', '*', '`'
			};

			// Token: 0x04001C4F RID: 7247
			private static readonly string[] s_callingConventionStrings = new string[] { "[vararg]", "[cdecl]", "[fastcall]", "[stdcall]", "[thiscall]" };

			// Token: 0x04001C50 RID: 7248
			private static readonly SuppressMessageAttributeState.TargetSymbolResolver.ParameterInfo[] s_noParameters = Array.Empty<SuppressMessageAttributeState.TargetSymbolResolver.ParameterInfo>();

			// Token: 0x04001C51 RID: 7249
			private readonly Compilation _compilation;

			// Token: 0x04001C52 RID: 7250
			private readonly SuppressMessageAttributeState.TargetScope _scope;

			// Token: 0x04001C53 RID: 7251
			private readonly string _name;

			// Token: 0x04001C54 RID: 7252
			private int _index;

			// Token: 0x020007EC RID: 2028
			[StructLayout(3)]
			private struct TypeInfo
			{
				// Token: 0x1700119B RID: 4507
				// (get) Token: 0x0600405E RID: 16478 RVA: 0x000B41F6 File Offset: 0x000B23F6
				public bool IsBound
				{
					get
					{
						return this.Type != null;
					}
				}

				// Token: 0x0600405F RID: 16479 RVA: 0x000B4201 File Offset: 0x000B2401
				private TypeInfo(ITypeSymbol type, int startIndex)
				{
					this.Type = type;
					this.StartIndex = startIndex;
				}

				// Token: 0x06004060 RID: 16480 RVA: 0x000B4211 File Offset: 0x000B2411
				public static SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo Create(ITypeSymbol type)
				{
					return new SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo(type, -1);
				}

				// Token: 0x06004061 RID: 16481 RVA: 0x000B421A File Offset: 0x000B241A
				public static SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo CreateUnbound(int startIndex)
				{
					return new SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo(null, startIndex);
				}

				// Token: 0x04001E53 RID: 7763
				public readonly ITypeSymbol Type;

				// Token: 0x04001E54 RID: 7764
				public readonly int StartIndex;
			}

			// Token: 0x020007ED RID: 2029
			[StructLayout(3)]
			private struct ParameterInfo
			{
				// Token: 0x06004062 RID: 16482 RVA: 0x000B4223 File Offset: 0x000B2423
				public ParameterInfo(SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo type, bool isRefOrOut)
				{
					this.Type = type;
					this.IsRefOrOut = isRefOrOut;
				}

				// Token: 0x04001E55 RID: 7765
				public readonly SuppressMessageAttributeState.TargetSymbolResolver.TypeInfo Type;

				// Token: 0x04001E56 RID: 7766
				public readonly bool IsRefOrOut;
			}
		}
	}
}
