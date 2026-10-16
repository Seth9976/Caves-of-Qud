using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.Symbols;
using Microsoft.DiaSymReader;

namespace Microsoft.CodeAnalysis.CodeGen
{
	// Token: 0x020004C1 RID: 1217
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class CompilationTestData
	{
		// Token: 0x06003125 RID: 12581 RVA: 0x0008C8E4 File Offset: 0x0008AAE4
		public ILBuilder GetIL(Func<IMethodSymbolInternal, bool> predicate)
		{
			return Enumerable.Single<KeyValuePair<IMethodSymbolInternal, CompilationTestData.MethodData>>(this.Methods, (KeyValuePair<IMethodSymbolInternal, CompilationTestData.MethodData> p) => predicate.Invoke(p.Key)).Value.ILBuilder;
		}

		// Token: 0x06003126 RID: 12582 RVA: 0x0008C924 File Offset: 0x0008AB24
		public ImmutableDictionary<string, CompilationTestData.MethodData> GetMethodsByName()
		{
			if (this._lazyMethodsByName == null)
			{
				Dictionary<string, CompilationTestData.MethodData> dictionary = new Dictionary<string, CompilationTestData.MethodData>();
				foreach (KeyValuePair<IMethodSymbolInternal, CompilationTestData.MethodData> keyValuePair in this.Methods)
				{
					string methodName = CompilationTestData.GetMethodName(keyValuePair.Key);
					if (dictionary.ContainsKey(methodName))
					{
						dictionary[methodName] = default(CompilationTestData.MethodData);
					}
					else
					{
						dictionary.Add(methodName, keyValuePair.Value);
					}
				}
				ImmutableDictionary<string, CompilationTestData.MethodData> immutableDictionary = Enumerable.Where<KeyValuePair<string, CompilationTestData.MethodData>>(dictionary, (KeyValuePair<string, CompilationTestData.MethodData> p) => p.Value.Method != null).ToImmutableDictionary<string, CompilationTestData.MethodData>();
				Interlocked.CompareExchange<ImmutableDictionary<string, CompilationTestData.MethodData>>(ref this._lazyMethodsByName, immutableDictionary, null);
			}
			return this._lazyMethodsByName;
		}

		// Token: 0x06003127 RID: 12583 RVA: 0x0008C9F8 File Offset: 0x0008ABF8
		private static string GetMethodName(IMethodSymbolInternal methodSymbol)
		{
			IMethodSymbol methodSymbol2 = (IMethodSymbol)methodSymbol.GetISymbol();
			SymbolDisplayFormat symbolDisplayFormat = ((methodSymbol2.MethodKind == MethodKind.UserDefinedOperator) ? CompilationTestData._testDataOperatorKeyFormat : CompilationTestData._testDataKeyFormat);
			return methodSymbol2.ToDisplayString(symbolDisplayFormat);
		}

		// Token: 0x040012EF RID: 4847
		public readonly ConcurrentDictionary<IMethodSymbolInternal, CompilationTestData.MethodData> Methods = new ConcurrentDictionary<IMethodSymbolInternal, CompilationTestData.MethodData>();

		// Token: 0x040012F0 RID: 4848
		[Nullable(2)]
		public CommonPEModuleBuilder Module;

		// Token: 0x040012F1 RID: 4849
		[Nullable(new byte[] { 2, 1, 1 })]
		public Func<ISymWriterMetadataProvider, SymUnmanagedWriter> SymWriterFactory;

		// Token: 0x040012F2 RID: 4850
		[Nullable(new byte[] { 2, 1 })]
		private ImmutableDictionary<string, CompilationTestData.MethodData> _lazyMethodsByName;

		// Token: 0x040012F3 RID: 4851
		private static readonly SymbolDisplayFormat _testDataKeyFormat = new SymbolDisplayFormat(SymbolDisplayCompilerInternalOptions.UseMetadataMethodNames | SymbolDisplayCompilerInternalOptions.UseValueTuple, SymbolDisplayGlobalNamespaceStyle.OmittedAsContaining, SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces, SymbolDisplayGenericsOptions.IncludeTypeParameters | SymbolDisplayGenericsOptions.IncludeVariance, SymbolDisplayMemberOptions.IncludeExplicitInterface | SymbolDisplayMemberOptions.IncludeParameters | SymbolDisplayMemberOptions.IncludeContainingType, SymbolDisplayParameterOptions.IncludeExtensionThis | SymbolDisplayParameterOptions.IncludeParamsRefOut | SymbolDisplayParameterOptions.IncludeType, SymbolDisplayDelegateStyle.NameOnly, SymbolDisplayExtensionMethodStyle.Default, SymbolDisplayPropertyStyle.NameOnly, SymbolDisplayLocalOptions.None, SymbolDisplayKindOptions.None, SymbolDisplayMiscellaneousOptions.UseSpecialTypes | SymbolDisplayMiscellaneousOptions.EscapeKeywordIdentifiers | SymbolDisplayMiscellaneousOptions.UseAsterisksInMultiDimensionalArrays | SymbolDisplayMiscellaneousOptions.UseErrorTypeSymbolName);

		// Token: 0x040012F4 RID: 4852
		private static readonly SymbolDisplayFormat _testDataOperatorKeyFormat = new SymbolDisplayFormat(CompilationTestData._testDataKeyFormat.CompilerInternalOptions, CompilationTestData._testDataKeyFormat.GlobalNamespaceStyle, CompilationTestData._testDataKeyFormat.TypeQualificationStyle, CompilationTestData._testDataKeyFormat.GenericsOptions, CompilationTestData._testDataKeyFormat.MemberOptions | SymbolDisplayMemberOptions.IncludeType, CompilationTestData._testDataKeyFormat.ParameterOptions, CompilationTestData._testDataKeyFormat.DelegateStyle, CompilationTestData._testDataKeyFormat.ExtensionMethodStyle, CompilationTestData._testDataKeyFormat.PropertyStyle, CompilationTestData._testDataKeyFormat.LocalOptions, CompilationTestData._testDataKeyFormat.KindOptions, CompilationTestData._testDataKeyFormat.MiscellaneousOptions);

		// Token: 0x02000778 RID: 1912
		[Nullable(0)]
		internal struct MethodData
		{
			// Token: 0x06003EC1 RID: 16065 RVA: 0x000B0C1B File Offset: 0x000AEE1B
			public MethodData(ILBuilder ilBuilder, IMethodSymbolInternal method)
			{
				this.ILBuilder = ilBuilder;
				this.Method = method;
			}

			// Token: 0x04001D6D RID: 7533
			public readonly ILBuilder ILBuilder;

			// Token: 0x04001D6E RID: 7534
			public readonly IMethodSymbolInternal Method;
		}
	}
}
