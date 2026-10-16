using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.RuntimeMembers;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004A4 RID: 1188
	internal sealed class AnonymousTypeManager : CommonAnonymousTypeManager
	{
		// Token: 0x06006242 RID: 25154 RVA: 0x00179918 File Offset: 0x00177B18
		internal AnonymousTypeManager(CSharpCompilation compilation)
		{
			this.Compilation = compilation;
		}

		// Token: 0x17001713 RID: 5907
		// (get) Token: 0x06006243 RID: 25155 RVA: 0x00179927 File Offset: 0x00177B27
		public CSharpCompilation Compilation { get; }

		// Token: 0x06006244 RID: 25156 RVA: 0x0017992F File Offset: 0x00177B2F
		public NamedTypeSymbol ConstructAnonymousTypeSymbol(AnonymousTypeDescriptor typeDescr)
		{
			return new AnonymousTypeManager.AnonymousTypePublicSymbol(this, typeDescr);
		}

		// Token: 0x06006245 RID: 25157 RVA: 0x00179938 File Offset: 0x00177B38
		internal static PropertySymbol GetAnonymousTypeProperty(NamedTypeSymbol type, int index)
		{
			return ((AnonymousTypeManager.AnonymousTypePublicSymbol)type).Properties[index];
		}

		// Token: 0x06006246 RID: 25158 RVA: 0x00179959 File Offset: 0x00177B59
		internal static ImmutableArray<TypeWithAnnotations> GetAnonymousTypePropertyTypesWithAnnotations(NamedTypeSymbol type)
		{
			return ((AnonymousTypeManager.AnonymousTypePublicSymbol)type).TypeDescriptor.Fields.SelectAsArray<AnonymousTypeField, TypeWithAnnotations>((AnonymousTypeField f) => f.TypeWithAnnotations);
		}

		// Token: 0x06006247 RID: 25159 RVA: 0x00179990 File Offset: 0x00177B90
		public static NamedTypeSymbol ConstructAnonymousTypeSymbol(NamedTypeSymbol type, ImmutableArray<TypeWithAnnotations> newFieldTypes)
		{
			AnonymousTypeManager.AnonymousTypePublicSymbol anonymousTypePublicSymbol = (AnonymousTypeManager.AnonymousTypePublicSymbol)type;
			return anonymousTypePublicSymbol.Manager.ConstructAnonymousTypeSymbol(anonymousTypePublicSymbol.TypeDescriptor.WithNewFieldsTypes(newFieldTypes));
		}

		// Token: 0x06006248 RID: 25160 RVA: 0x001799BC File Offset: 0x00177BBC
		public bool ReportMissingOrErroneousSymbols(BindingDiagnosticBag diagnostics)
		{
			bool flag = false;
			AnonymousTypeManager.ReportErrorOnSymbol(this.System_Object, diagnostics, ref flag);
			AnonymousTypeManager.ReportErrorOnSymbol(this.System_Void, diagnostics, ref flag);
			AnonymousTypeManager.ReportErrorOnSymbol(this.System_Boolean, diagnostics, ref flag);
			AnonymousTypeManager.ReportErrorOnSymbol(this.System_String, diagnostics, ref flag);
			AnonymousTypeManager.ReportErrorOnSymbol(this.System_Int32, diagnostics, ref flag);
			AnonymousTypeManager.ReportErrorOnSpecialMember(this.System_Object__Equals, SpecialMember.System_Object__Equals, diagnostics, ref flag);
			AnonymousTypeManager.ReportErrorOnSpecialMember(this.System_Object__ToString, SpecialMember.System_Object__ToString, diagnostics, ref flag);
			AnonymousTypeManager.ReportErrorOnSpecialMember(this.System_Object__GetHashCode, SpecialMember.System_Object__GetHashCode, diagnostics, ref flag);
			AnonymousTypeManager.ReportErrorOnWellKnownMember(this.System_String__Format_IFormatProvider, WellKnownMember.System_String__Format_IFormatProvider, diagnostics, ref flag);
			AnonymousTypeManager.ReportErrorOnWellKnownMember(this.System_Collections_Generic_EqualityComparer_T__Equals, WellKnownMember.System_Collections_Generic_EqualityComparer_T__Equals, diagnostics, ref flag);
			AnonymousTypeManager.ReportErrorOnWellKnownMember(this.System_Collections_Generic_EqualityComparer_T__GetHashCode, WellKnownMember.System_Collections_Generic_EqualityComparer_T__GetHashCode, diagnostics, ref flag);
			AnonymousTypeManager.ReportErrorOnWellKnownMember(this.System_Collections_Generic_EqualityComparer_T__get_Default, WellKnownMember.System_Collections_Generic_EqualityComparer_T__get_Default, diagnostics, ref flag);
			return flag;
		}

		// Token: 0x06006249 RID: 25161 RVA: 0x00179A85 File Offset: 0x00177C85
		private static void ReportErrorOnSymbol(Symbol symbol, BindingDiagnosticBag diagnostics, ref bool hasError)
		{
			if (symbol == null)
			{
				return;
			}
			hasError |= diagnostics.ReportUseSite(symbol, NoLocation.Singleton);
		}

		// Token: 0x0600624A RID: 25162 RVA: 0x00179A9C File Offset: 0x00177C9C
		private static void ReportErrorOnSpecialMember(Symbol symbol, SpecialMember member, BindingDiagnosticBag diagnostics, ref bool hasError)
		{
			if (symbol == null)
			{
				MemberDescriptor descriptor = SpecialMembers.GetDescriptor(member);
				diagnostics.Add(ErrorCode.ERR_MissingPredefinedMember, NoLocation.Singleton, new object[] { descriptor.DeclaringTypeMetadataName, descriptor.Name });
				hasError = true;
				return;
			}
			AnonymousTypeManager.ReportErrorOnSymbol(symbol, diagnostics, ref hasError);
		}

		// Token: 0x0600624B RID: 25163 RVA: 0x00179AEC File Offset: 0x00177CEC
		private static void ReportErrorOnWellKnownMember(Symbol symbol, WellKnownMember member, BindingDiagnosticBag diagnostics, ref bool hasError)
		{
			if (symbol == null)
			{
				MemberDescriptor descriptor = WellKnownMembers.GetDescriptor(member);
				diagnostics.Add(ErrorCode.ERR_MissingPredefinedMember, NoLocation.Singleton, new object[] { descriptor.DeclaringTypeMetadataName, descriptor.Name });
				hasError = true;
				return;
			}
			AnonymousTypeManager.ReportErrorOnSymbol(symbol, diagnostics, ref hasError);
			AnonymousTypeManager.ReportErrorOnSymbol(symbol.ContainingType, diagnostics, ref hasError);
		}

		// Token: 0x17001714 RID: 5908
		// (get) Token: 0x0600624C RID: 25164 RVA: 0x00179B46 File Offset: 0x00177D46
		public NamedTypeSymbol System_Object
		{
			get
			{
				return this.Compilation.GetSpecialType(SpecialType.System_Object);
			}
		}

		// Token: 0x17001715 RID: 5909
		// (get) Token: 0x0600624D RID: 25165 RVA: 0x00179B54 File Offset: 0x00177D54
		public NamedTypeSymbol System_Void
		{
			get
			{
				return this.Compilation.GetSpecialType(SpecialType.System_Void);
			}
		}

		// Token: 0x17001716 RID: 5910
		// (get) Token: 0x0600624E RID: 25166 RVA: 0x00179B62 File Offset: 0x00177D62
		public NamedTypeSymbol System_Boolean
		{
			get
			{
				return this.Compilation.GetSpecialType(SpecialType.System_Boolean);
			}
		}

		// Token: 0x17001717 RID: 5911
		// (get) Token: 0x0600624F RID: 25167 RVA: 0x00179B70 File Offset: 0x00177D70
		public NamedTypeSymbol System_String
		{
			get
			{
				return this.Compilation.GetSpecialType(SpecialType.System_String);
			}
		}

		// Token: 0x17001718 RID: 5912
		// (get) Token: 0x06006250 RID: 25168 RVA: 0x00179B7F File Offset: 0x00177D7F
		public NamedTypeSymbol System_Int32
		{
			get
			{
				return this.Compilation.GetSpecialType(SpecialType.System_Int32);
			}
		}

		// Token: 0x17001719 RID: 5913
		// (get) Token: 0x06006251 RID: 25169 RVA: 0x00179B8E File Offset: 0x00177D8E
		public NamedTypeSymbol System_Diagnostics_DebuggerBrowsableState
		{
			get
			{
				return this.Compilation.GetWellKnownType(WellKnownType.System_Diagnostics_DebuggerBrowsableState);
			}
		}

		// Token: 0x1700171A RID: 5914
		// (get) Token: 0x06006252 RID: 25170 RVA: 0x00179BA0 File Offset: 0x00177DA0
		public MethodSymbol System_Object__Equals
		{
			get
			{
				return this.Compilation.GetSpecialTypeMember(SpecialMember.System_Object__Equals) as MethodSymbol;
			}
		}

		// Token: 0x1700171B RID: 5915
		// (get) Token: 0x06006253 RID: 25171 RVA: 0x00179BB4 File Offset: 0x00177DB4
		public MethodSymbol System_Object__ToString
		{
			get
			{
				return this.Compilation.GetSpecialTypeMember(SpecialMember.System_Object__ToString) as MethodSymbol;
			}
		}

		// Token: 0x1700171C RID: 5916
		// (get) Token: 0x06006254 RID: 25172 RVA: 0x00179BC8 File Offset: 0x00177DC8
		public MethodSymbol System_Object__GetHashCode
		{
			get
			{
				return this.Compilation.GetSpecialTypeMember(SpecialMember.System_Object__GetHashCode) as MethodSymbol;
			}
		}

		// Token: 0x1700171D RID: 5917
		// (get) Token: 0x06006255 RID: 25173 RVA: 0x00179BDC File Offset: 0x00177DDC
		public MethodSymbol System_Collections_Generic_EqualityComparer_T__Equals
		{
			get
			{
				return this.Compilation.GetWellKnownTypeMember(WellKnownMember.System_Collections_Generic_EqualityComparer_T__Equals) as MethodSymbol;
			}
		}

		// Token: 0x1700171E RID: 5918
		// (get) Token: 0x06006256 RID: 25174 RVA: 0x00179BF0 File Offset: 0x00177DF0
		public MethodSymbol System_Collections_Generic_EqualityComparer_T__GetHashCode
		{
			get
			{
				return this.Compilation.GetWellKnownTypeMember(WellKnownMember.System_Collections_Generic_EqualityComparer_T__GetHashCode) as MethodSymbol;
			}
		}

		// Token: 0x1700171F RID: 5919
		// (get) Token: 0x06006257 RID: 25175 RVA: 0x00179C04 File Offset: 0x00177E04
		public MethodSymbol System_Collections_Generic_EqualityComparer_T__get_Default
		{
			get
			{
				return this.Compilation.GetWellKnownTypeMember(WellKnownMember.System_Collections_Generic_EqualityComparer_T__get_Default) as MethodSymbol;
			}
		}

		// Token: 0x17001720 RID: 5920
		// (get) Token: 0x06006258 RID: 25176 RVA: 0x00179C18 File Offset: 0x00177E18
		public MethodSymbol System_String__Format_IFormatProvider
		{
			get
			{
				return this.Compilation.GetWellKnownTypeMember(WellKnownMember.System_String__Format_IFormatProvider) as MethodSymbol;
			}
		}

		// Token: 0x06006259 RID: 25177 RVA: 0x00179C2F File Offset: 0x00177E2F
		[Conditional("DEBUG")]
		private void CheckSourceLocationSeen(AnonymousTypeManager.AnonymousTypePublicSymbol anonymous)
		{
		}

		// Token: 0x17001721 RID: 5921
		// (get) Token: 0x0600625A RID: 25178 RVA: 0x00179C34 File Offset: 0x00177E34
		private ConcurrentDictionary<string, AnonymousTypeManager.AnonymousTypeTemplateSymbol> AnonymousTypeTemplates
		{
			get
			{
				if (this._lazyAnonymousTypeTemplates == null)
				{
					CSharpCompilation previousSubmission = this.Compilation.PreviousSubmission;
					ConcurrentDictionary<string, AnonymousTypeManager.AnonymousTypeTemplateSymbol> concurrentDictionary = ((previousSubmission == null) ? null : previousSubmission.AnonymousTypeManager.AnonymousTypeTemplates);
					Interlocked.CompareExchange<ConcurrentDictionary<string, AnonymousTypeManager.AnonymousTypeTemplateSymbol>>(ref this._lazyAnonymousTypeTemplates, (concurrentDictionary == null) ? new ConcurrentDictionary<string, AnonymousTypeManager.AnonymousTypeTemplateSymbol>() : new ConcurrentDictionary<string, AnonymousTypeManager.AnonymousTypeTemplateSymbol>(concurrentDictionary), null);
				}
				return this._lazyAnonymousTypeTemplates;
			}
		}

		// Token: 0x17001722 RID: 5922
		// (get) Token: 0x0600625B RID: 25179 RVA: 0x00179C8C File Offset: 0x00177E8C
		private ConcurrentDictionary<AnonymousTypeManager.SynthesizedDelegateKey, AnonymousTypeManager.SynthesizedDelegateValue> SynthesizedDelegates
		{
			get
			{
				if (this._lazySynthesizedDelegates == null)
				{
					CSharpCompilation previousSubmission = this.Compilation.PreviousSubmission;
					ConcurrentDictionary<AnonymousTypeManager.SynthesizedDelegateKey, AnonymousTypeManager.SynthesizedDelegateValue> concurrentDictionary = ((previousSubmission == null) ? null : previousSubmission.AnonymousTypeManager._lazySynthesizedDelegates);
					Interlocked.CompareExchange<ConcurrentDictionary<AnonymousTypeManager.SynthesizedDelegateKey, AnonymousTypeManager.SynthesizedDelegateValue>>(ref this._lazySynthesizedDelegates, (concurrentDictionary == null) ? new ConcurrentDictionary<AnonymousTypeManager.SynthesizedDelegateKey, AnonymousTypeManager.SynthesizedDelegateValue>() : new ConcurrentDictionary<AnonymousTypeManager.SynthesizedDelegateKey, AnonymousTypeManager.SynthesizedDelegateValue>(concurrentDictionary), null);
				}
				return this._lazySynthesizedDelegates;
			}
		}

		// Token: 0x0600625C RID: 25180 RVA: 0x00179CE4 File Offset: 0x00177EE4
		internal SynthesizedDelegateSymbol SynthesizeDelegate(int parameterCount, BitVector byRefParameters, bool returnsVoid, int generation)
		{
			AnonymousTypeManager.SynthesizedDelegateKey synthesizedDelegateKey = new AnonymousTypeManager.SynthesizedDelegateKey(parameterCount, byRefParameters, returnsVoid, generation);
			AnonymousTypeManager.SynthesizedDelegateValue synthesizedDelegateValue;
			if (this.SynthesizedDelegates.TryGetValue(synthesizedDelegateKey, ref synthesizedDelegateValue))
			{
				return synthesizedDelegateValue.Delegate;
			}
			return this.SynthesizedDelegates.GetOrAdd(synthesizedDelegateKey, new AnonymousTypeManager.SynthesizedDelegateValue(this, new SynthesizedDelegateSymbol(this.Compilation.Assembly.GlobalNamespace, synthesizedDelegateKey.MakeTypeName(), this.System_Object, this.Compilation.GetSpecialType(SpecialType.System_IntPtr), returnsVoid ? this.Compilation.GetSpecialType(SpecialType.System_Void) : null, parameterCount, byRefParameters))).Delegate;
		}

		// Token: 0x0600625D RID: 25181 RVA: 0x00179D70 File Offset: 0x00177F70
		private NamedTypeSymbol ConstructAnonymousTypeImplementationSymbol(AnonymousTypeManager.AnonymousTypePublicSymbol anonymous)
		{
			AnonymousTypeDescriptor typeDescriptor = anonymous.TypeDescriptor;
			AnonymousTypeManager.AnonymousTypeTemplateSymbol orAdd;
			if (!this.AnonymousTypeTemplates.TryGetValue(typeDescriptor.Key, ref orAdd))
			{
				orAdd = this.AnonymousTypeTemplates.GetOrAdd(typeDescriptor.Key, new AnonymousTypeManager.AnonymousTypeTemplateSymbol(this, typeDescriptor));
			}
			if (orAdd.Manager == this)
			{
				orAdd.AdjustLocation(typeDescriptor.Location);
			}
			if (orAdd.Arity == 0)
			{
				return orAdd;
			}
			ImmutableArray<TypeSymbol> immutableArray = typeDescriptor.Fields.SelectAsArray<AnonymousTypeField, TypeSymbol>((AnonymousTypeField f) => f.Type);
			return orAdd.Construct(immutableArray);
		}

		// Token: 0x0600625E RID: 25182 RVA: 0x00179E04 File Offset: 0x00178004
		private AnonymousTypeManager.AnonymousTypeTemplateSymbol CreatePlaceholderTemplate(AnonymousTypeKey key)
		{
			ImmutableArray<AnonymousTypeField> immutableArray = key.Fields.SelectAsArray<AnonymousTypeKeyField, AnonymousTypeField>((AnonymousTypeKeyField f) => new AnonymousTypeField(f.Name, Location.None, default(TypeWithAnnotations)));
			AnonymousTypeDescriptor anonymousTypeDescriptor = new AnonymousTypeDescriptor(immutableArray, Location.None);
			return new AnonymousTypeManager.AnonymousTypeTemplateSymbol(this, anonymousTypeDescriptor);
		}

		// Token: 0x0600625F RID: 25183 RVA: 0x00179E50 File Offset: 0x00178050
		public void AssignTemplatesNamesAndCompile(MethodCompiler compiler, PEModuleBuilder moduleBeingBuilt, BindingDiagnosticBag diagnostics)
		{
			ImmutableArray<AnonymousTypeKey>.Enumerator enumerator = moduleBeingBuilt.GetPreviousAnonymousTypes().GetEnumerator();
			while (enumerator.MoveNext())
			{
				AnonymousTypeKey key = enumerator.Current;
				string text = AnonymousTypeDescriptor.ComputeKey<AnonymousTypeKeyField>(key.Fields, (AnonymousTypeKeyField f) => f.Name);
				this.AnonymousTypeTemplates.GetOrAdd(text, (string k) => this.CreatePlaceholderTemplate(key));
			}
			ArrayBuilder<AnonymousTypeManager.AnonymousTypeTemplateSymbol> instance = ArrayBuilder<AnonymousTypeManager.AnonymousTypeTemplateSymbol>.GetInstance();
			this.GetCreatedAnonymousTypeTemplates(instance);
			if (!base.AreTemplatesSealed)
			{
				string text2;
				if (moduleBeingBuilt.OutputKind == OutputKind.NetModule)
				{
					text2 = moduleBeingBuilt.Name;
					string defaultExtension = OutputKind.NetModule.GetDefaultExtension();
					if (text2.EndsWith(defaultExtension, 5))
					{
						text2 = text2.Substring(0, text2.Length - defaultExtension.Length);
					}
					text2 = MetadataHelpers.MangleForTypeNameIfNeeded(text2);
				}
				else
				{
					text2 = string.Empty;
				}
				int nextAnonymousTypeIndex = moduleBeingBuilt.GetNextAnonymousTypeIndex();
				foreach (AnonymousTypeManager.AnonymousTypeTemplateSymbol anonymousTypeTemplateSymbol in instance)
				{
					string text3;
					int num;
					if (!moduleBeingBuilt.TryGetAnonymousTypeName(anonymousTypeTemplateSymbol, out text3, out num))
					{
						num = nextAnonymousTypeIndex++;
						text3 = GeneratedNames.MakeAnonymousTypeTemplateName(num, this.Compilation.GetSubmissionSlotIndex(), text2);
					}
					anonymousTypeTemplateSymbol.NameAndIndex = new AnonymousTypeManager.NameAndIndex(text3, num);
				}
				base.SealTemplates();
			}
			if (instance.Count > 0 && !this.ReportMissingOrErroneousSymbols(diagnostics))
			{
				foreach (AnonymousTypeManager.AnonymousTypeTemplateSymbol anonymousTypeTemplateSymbol2 in instance)
				{
					foreach (MethodSymbol methodSymbol in anonymousTypeTemplateSymbol2.SpecialMembers)
					{
						moduleBeingBuilt.AddSynthesizedDefinition(anonymousTypeTemplateSymbol2, methodSymbol.GetCciAdapter());
					}
					compiler.Visit(anonymousTypeTemplateSymbol2, null);
				}
			}
			instance.Free();
			ArrayBuilder<SynthesizedDelegateSymbol> instance2 = ArrayBuilder<SynthesizedDelegateSymbol>.GetInstance();
			this.GetCreatedSynthesizedDelegates(instance2);
			foreach (SynthesizedDelegateSymbol synthesizedDelegateSymbol in instance2)
			{
				compiler.Visit(synthesizedDelegateSymbol, null);
			}
			instance2.Free();
		}

		// Token: 0x06006260 RID: 25184 RVA: 0x0017A05C File Offset: 0x0017825C
		private void GetCreatedAnonymousTypeTemplates(ArrayBuilder<AnonymousTypeManager.AnonymousTypeTemplateSymbol> builder)
		{
			ConcurrentDictionary<string, AnonymousTypeManager.AnonymousTypeTemplateSymbol> lazyAnonymousTypeTemplates = this._lazyAnonymousTypeTemplates;
			if (lazyAnonymousTypeTemplates != null)
			{
				foreach (AnonymousTypeManager.AnonymousTypeTemplateSymbol anonymousTypeTemplateSymbol in lazyAnonymousTypeTemplates.Values)
				{
					if (anonymousTypeTemplateSymbol.Manager == this)
					{
						builder.Add(anonymousTypeTemplateSymbol);
					}
				}
				builder.Sort(new AnonymousTypeManager.AnonymousTypeComparer(this.Compilation));
			}
		}

		// Token: 0x06006261 RID: 25185 RVA: 0x0017A0D0 File Offset: 0x001782D0
		private void GetCreatedSynthesizedDelegates(ArrayBuilder<SynthesizedDelegateSymbol> builder)
		{
			ConcurrentDictionary<AnonymousTypeManager.SynthesizedDelegateKey, AnonymousTypeManager.SynthesizedDelegateValue> lazySynthesizedDelegates = this._lazySynthesizedDelegates;
			if (lazySynthesizedDelegates != null)
			{
				foreach (AnonymousTypeManager.SynthesizedDelegateValue synthesizedDelegateValue in lazySynthesizedDelegates.Values)
				{
					if (synthesizedDelegateValue.Manager == this)
					{
						builder.Add(synthesizedDelegateValue.Delegate);
					}
				}
				builder.Sort(AnonymousTypeManager.SynthesizedDelegateSymbolComparer.Instance);
			}
		}

		// Token: 0x06006262 RID: 25186 RVA: 0x0017A140 File Offset: 0x00178340
		internal IReadOnlyDictionary<AnonymousTypeKey, AnonymousTypeValue> GetAnonymousTypeMap()
		{
			Dictionary<AnonymousTypeKey, AnonymousTypeValue> dictionary = new Dictionary<AnonymousTypeKey, AnonymousTypeValue>();
			ArrayBuilder<AnonymousTypeManager.AnonymousTypeTemplateSymbol> instance = ArrayBuilder<AnonymousTypeManager.AnonymousTypeTemplateSymbol>.GetInstance();
			this.GetCreatedAnonymousTypeTemplates(instance);
			foreach (AnonymousTypeManager.AnonymousTypeTemplateSymbol anonymousTypeTemplateSymbol in instance)
			{
				AnonymousTypeManager.NameAndIndex nameAndIndex = anonymousTypeTemplateSymbol.NameAndIndex;
				AnonymousTypeKey anonymousTypeKey = anonymousTypeTemplateSymbol.GetAnonymousTypeKey();
				AnonymousTypeValue anonymousTypeValue = new AnonymousTypeValue(nameAndIndex.Name, nameAndIndex.Index, anonymousTypeTemplateSymbol.GetCciAdapter());
				dictionary.Add(anonymousTypeKey, anonymousTypeValue);
			}
			instance.Free();
			return dictionary;
		}

		// Token: 0x06006263 RID: 25187 RVA: 0x0017A1B8 File Offset: 0x001783B8
		internal ImmutableArray<NamedTypeSymbol> GetAllCreatedTemplates()
		{
			ArrayBuilder<NamedTypeSymbol> instance = ArrayBuilder<NamedTypeSymbol>.GetInstance();
			ArrayBuilder<AnonymousTypeManager.AnonymousTypeTemplateSymbol> instance2 = ArrayBuilder<AnonymousTypeManager.AnonymousTypeTemplateSymbol>.GetInstance();
			this.GetCreatedAnonymousTypeTemplates(instance2);
			instance.AddRange<AnonymousTypeManager.AnonymousTypeTemplateSymbol>(instance2);
			instance2.Free();
			ArrayBuilder<SynthesizedDelegateSymbol> instance3 = ArrayBuilder<SynthesizedDelegateSymbol>.GetInstance();
			this.GetCreatedSynthesizedDelegates(instance3);
			instance.AddRange<SynthesizedDelegateSymbol>(instance3);
			instance3.Free();
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06006264 RID: 25188 RVA: 0x0017A203 File Offset: 0x00178403
		internal static bool IsAnonymousTypeTemplate(NamedTypeSymbol type)
		{
			return type is AnonymousTypeManager.AnonymousTypeTemplateSymbol;
		}

		// Token: 0x06006265 RID: 25189 RVA: 0x0017A20E File Offset: 0x0017840E
		internal static ImmutableArray<MethodSymbol> GetAnonymousTypeHiddenMethods(NamedTypeSymbol type)
		{
			return ((AnonymousTypeManager.AnonymousTypeTemplateSymbol)type).SpecialMembers;
		}

		// Token: 0x06006266 RID: 25190 RVA: 0x0017A21C File Offset: 0x0017841C
		internal static NamedTypeSymbol TranslateAnonymousTypeSymbol(NamedTypeSymbol type)
		{
			AnonymousTypeManager.AnonymousTypePublicSymbol anonymousTypePublicSymbol = (AnonymousTypeManager.AnonymousTypePublicSymbol)type;
			return anonymousTypePublicSymbol.Manager.ConstructAnonymousTypeImplementationSymbol(anonymousTypePublicSymbol);
		}

		// Token: 0x06006267 RID: 25191 RVA: 0x0017A23C File Offset: 0x0017843C
		internal static MethodSymbol TranslateAnonymousTypeMethodSymbol(MethodSymbol method)
		{
			NamedTypeSymbol namedTypeSymbol = AnonymousTypeManager.TranslateAnonymousTypeSymbol(method.ContainingType);
			foreach (Symbol symbol in namedTypeSymbol.OriginalDefinition.GetMembers(method.Name))
			{
				if (symbol.Kind == SymbolKind.Method)
				{
					return ((MethodSymbol)symbol).AsMember(namedTypeSymbol);
				}
			}
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x0400197D RID: 6525
		private ConcurrentDictionary<string, AnonymousTypeManager.AnonymousTypeTemplateSymbol> _lazyAnonymousTypeTemplates;

		// Token: 0x0400197E RID: 6526
		private ConcurrentDictionary<AnonymousTypeManager.SynthesizedDelegateKey, AnonymousTypeManager.SynthesizedDelegateValue> _lazySynthesizedDelegates;

		// Token: 0x020008B7 RID: 2231
		private sealed class AnonymousTypeConstructorSymbol : AnonymousTypeManager.SynthesizedMethodBase
		{
			// Token: 0x06008CCD RID: 36045 RVA: 0x001F4380 File Offset: 0x001F2580
			internal override void GenerateMethodBody(TypeCompilationState compilationState, BindingDiagnosticBag diagnostics)
			{
				SyntheticBoundNodeFactory syntheticBoundNodeFactory = base.CreateBoundNodeFactory(compilationState, diagnostics);
				int parameterCount = this.ParameterCount;
				BoundStatement[] array = new BoundStatement[parameterCount + 2];
				int num = 0;
				BoundExpression boundExpression = MethodCompiler.GenerateBaseParameterlessConstructorInitializer(this, diagnostics);
				if (boundExpression == null)
				{
					return;
				}
				array[num++] = syntheticBoundNodeFactory.ExpressionStatement(boundExpression);
				if (parameterCount > 0)
				{
					AnonymousTypeManager.AnonymousTypeTemplateSymbol anonymousTypeTemplateSymbol = (AnonymousTypeManager.AnonymousTypeTemplateSymbol)this.ContainingType;
					for (int i = 0; i < this.ParameterCount; i++)
					{
						array[num++] = syntheticBoundNodeFactory.Assignment(syntheticBoundNodeFactory.Field(syntheticBoundNodeFactory.This(), anonymousTypeTemplateSymbol.Properties[i].BackingField), syntheticBoundNodeFactory.Parameter(this._parameters[i]), false);
					}
				}
				array[num++] = syntheticBoundNodeFactory.Return(null);
				syntheticBoundNodeFactory.CloseMethod(syntheticBoundNodeFactory.Block(array));
			}

			// Token: 0x1700275A RID: 10074
			// (get) Token: 0x06008CCE RID: 36046 RVA: 0x001F444E File Offset: 0x001F264E
			internal override bool HasSpecialName
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06008CCF RID: 36047 RVA: 0x001F4454 File Offset: 0x001F2654
			internal AnonymousTypeConstructorSymbol(NamedTypeSymbol container, ImmutableArray<AnonymousTypeManager.AnonymousTypePropertySymbol> properties)
				: base(container, ".ctor")
			{
				int length = properties.Length;
				if (length > 0)
				{
					ParameterSymbol[] array = new ParameterSymbol[length];
					for (int i = 0; i < length; i++)
					{
						PropertySymbol propertySymbol = properties[i];
						array[i] = SynthesizedParameterSymbol.Create(this, propertySymbol.TypeWithAnnotations, i, RefKind.None, propertySymbol.Name, default(ImmutableArray<CustomModifier>), null);
					}
					this._parameters = array.AsImmutableOrNull<ParameterSymbol>();
					return;
				}
				this._parameters = ImmutableArray<ParameterSymbol>.Empty;
			}

			// Token: 0x1700275B RID: 10075
			// (get) Token: 0x06008CD0 RID: 36048 RVA: 0x001F44CF File Offset: 0x001F26CF
			public override MethodKind MethodKind
			{
				get
				{
					return MethodKind.Constructor;
				}
			}

			// Token: 0x1700275C RID: 10076
			// (get) Token: 0x06008CD1 RID: 36049 RVA: 0x001F44D2 File Offset: 0x001F26D2
			public override bool ReturnsVoid
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700275D RID: 10077
			// (get) Token: 0x06008CD2 RID: 36050 RVA: 0x001F44D5 File Offset: 0x001F26D5
			public override RefKind RefKind
			{
				get
				{
					return RefKind.None;
				}
			}

			// Token: 0x1700275E RID: 10078
			// (get) Token: 0x06008CD3 RID: 36051 RVA: 0x001F44D8 File Offset: 0x001F26D8
			public override TypeWithAnnotations ReturnTypeWithAnnotations
			{
				get
				{
					return TypeWithAnnotations.Create(base.Manager.System_Void, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
				}
			}

			// Token: 0x1700275F RID: 10079
			// (get) Token: 0x06008CD4 RID: 36052 RVA: 0x001F44FF File Offset: 0x001F26FF
			public override ImmutableArray<ParameterSymbol> Parameters
			{
				get
				{
					return this._parameters;
				}
			}

			// Token: 0x17002760 RID: 10080
			// (get) Token: 0x06008CD5 RID: 36053 RVA: 0x001F4507 File Offset: 0x001F2707
			public override bool IsOverride
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06008CD6 RID: 36054 RVA: 0x001F450A File Offset: 0x001F270A
			internal sealed override bool IsMetadataVirtual(bool ignoreInterfaceImplementationChanges = false)
			{
				return false;
			}

			// Token: 0x17002761 RID: 10081
			// (get) Token: 0x06008CD7 RID: 36055 RVA: 0x001F450D File Offset: 0x001F270D
			internal override bool IsMetadataFinal
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002762 RID: 10082
			// (get) Token: 0x06008CD8 RID: 36056 RVA: 0x001F4510 File Offset: 0x001F2710
			public override ImmutableArray<Location> Locations
			{
				get
				{
					return this.ContainingSymbol.Locations;
				}
			}

			// Token: 0x04002624 RID: 9764
			private readonly ImmutableArray<ParameterSymbol> _parameters;
		}

		// Token: 0x020008B8 RID: 2232
		private sealed class AnonymousTypePropertyGetAccessorSymbol : AnonymousTypeManager.SynthesizedMethodBase
		{
			// Token: 0x06008CD9 RID: 36057 RVA: 0x001F4520 File Offset: 0x001F2720
			internal override void GenerateMethodBody(TypeCompilationState compilationState, BindingDiagnosticBag diagnostics)
			{
				SyntheticBoundNodeFactory syntheticBoundNodeFactory = base.CreateBoundNodeFactory(compilationState, diagnostics);
				syntheticBoundNodeFactory.CloseMethod(syntheticBoundNodeFactory.Block(new BoundStatement[] { syntheticBoundNodeFactory.Return(syntheticBoundNodeFactory.Field(syntheticBoundNodeFactory.This(), this._property.BackingField)) }));
			}

			// Token: 0x17002763 RID: 10083
			// (get) Token: 0x06008CDA RID: 36058 RVA: 0x001F4568 File Offset: 0x001F2768
			internal override bool HasSpecialName
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06008CDB RID: 36059 RVA: 0x001F456B File Offset: 0x001F276B
			internal AnonymousTypePropertyGetAccessorSymbol(AnonymousTypeManager.AnonymousTypePropertySymbol property)
				: base(property.ContainingType, SourcePropertyAccessorSymbol.GetAccessorName(property.Name, true, false))
			{
				this._property = property;
			}

			// Token: 0x17002764 RID: 10084
			// (get) Token: 0x06008CDC RID: 36060 RVA: 0x001F458D File Offset: 0x001F278D
			public override MethodKind MethodKind
			{
				get
				{
					return MethodKind.PropertyGet;
				}
			}

			// Token: 0x17002765 RID: 10085
			// (get) Token: 0x06008CDD RID: 36061 RVA: 0x001F4591 File Offset: 0x001F2791
			public override bool ReturnsVoid
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002766 RID: 10086
			// (get) Token: 0x06008CDE RID: 36062 RVA: 0x001F4594 File Offset: 0x001F2794
			public override RefKind RefKind
			{
				get
				{
					return RefKind.None;
				}
			}

			// Token: 0x17002767 RID: 10087
			// (get) Token: 0x06008CDF RID: 36063 RVA: 0x001F4597 File Offset: 0x001F2797
			public override TypeWithAnnotations ReturnTypeWithAnnotations
			{
				get
				{
					return this._property.TypeWithAnnotations;
				}
			}

			// Token: 0x17002768 RID: 10088
			// (get) Token: 0x06008CE0 RID: 36064 RVA: 0x001F45A4 File Offset: 0x001F27A4
			public override ImmutableArray<ParameterSymbol> Parameters
			{
				get
				{
					return ImmutableArray<ParameterSymbol>.Empty;
				}
			}

			// Token: 0x17002769 RID: 10089
			// (get) Token: 0x06008CE1 RID: 36065 RVA: 0x001F45AB File Offset: 0x001F27AB
			public override Symbol AssociatedSymbol
			{
				get
				{
					return this._property;
				}
			}

			// Token: 0x1700276A RID: 10090
			// (get) Token: 0x06008CE2 RID: 36066 RVA: 0x001F45B3 File Offset: 0x001F27B3
			public override ImmutableArray<Location> Locations
			{
				get
				{
					return this._property.Locations;
				}
			}

			// Token: 0x1700276B RID: 10091
			// (get) Token: 0x06008CE3 RID: 36067 RVA: 0x001F45C0 File Offset: 0x001F27C0
			public override bool IsOverride
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06008CE4 RID: 36068 RVA: 0x001F45C3 File Offset: 0x001F27C3
			internal sealed override bool IsMetadataVirtual(bool ignoreInterfaceImplementationChanges = false)
			{
				return false;
			}

			// Token: 0x1700276C RID: 10092
			// (get) Token: 0x06008CE5 RID: 36069 RVA: 0x001F45C6 File Offset: 0x001F27C6
			internal override bool IsMetadataFinal
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06008CE6 RID: 36070 RVA: 0x001F45C9 File Offset: 0x001F27C9
			internal override void AddSynthesizedAttributes(PEModuleBuilder moduleBuilder, ref ArrayBuilder<SynthesizedAttributeData> attributes)
			{
			}

			// Token: 0x04002625 RID: 9765
			private readonly AnonymousTypeManager.AnonymousTypePropertySymbol _property;
		}

		// Token: 0x020008B9 RID: 2233
		private sealed class AnonymousTypeEqualsMethodSymbol : AnonymousTypeManager.SynthesizedMethodBase
		{
			// Token: 0x06008CE7 RID: 36071 RVA: 0x001F45CC File Offset: 0x001F27CC
			internal override void GenerateMethodBody(TypeCompilationState compilationState, BindingDiagnosticBag diagnostics)
			{
				AnonymousTypeManager manager = ((AnonymousTypeManager.AnonymousTypeTemplateSymbol)this.ContainingType).Manager;
				SyntheticBoundNodeFactory syntheticBoundNodeFactory = base.CreateBoundNodeFactory(compilationState, diagnostics);
				AnonymousTypeManager.AnonymousTypeTemplateSymbol anonymousTypeTemplateSymbol = (AnonymousTypeManager.AnonymousTypeTemplateSymbol)this.ContainingType;
				BoundAssignmentOperator boundAssignmentOperator;
				BoundLocal boundLocal = syntheticBoundNodeFactory.StoreToTemp(syntheticBoundNodeFactory.As(syntheticBoundNodeFactory.Parameter(this._parameters[0]), anonymousTypeTemplateSymbol), out boundAssignmentOperator, RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
				BoundStatement boundStatement = syntheticBoundNodeFactory.ExpressionStatement(boundAssignmentOperator);
				BoundExpression boundExpression = syntheticBoundNodeFactory.Binary(BinaryOperatorKind.ObjectNotEqual, manager.System_Boolean, syntheticBoundNodeFactory.Convert(manager.System_Object, boundLocal), syntheticBoundNodeFactory.Null(manager.System_Object));
				if (anonymousTypeTemplateSymbol.Properties.Length > 0)
				{
					ArrayBuilder<FieldSymbol> instance = ArrayBuilder<FieldSymbol>.GetInstance(anonymousTypeTemplateSymbol.Properties.Length);
					foreach (AnonymousTypeManager.AnonymousTypePropertySymbol anonymousTypePropertySymbol in anonymousTypeTemplateSymbol.Properties)
					{
						instance.Add(anonymousTypePropertySymbol.BackingField);
					}
					boundExpression = MethodBodySynthesizer.GenerateFieldEquals(boundExpression, boundLocal, instance, syntheticBoundNodeFactory);
					instance.Free();
				}
				boundExpression = syntheticBoundNodeFactory.LogicalOr(syntheticBoundNodeFactory.ObjectEqual(syntheticBoundNodeFactory.This(), boundLocal), boundExpression);
				BoundStatement boundStatement2 = syntheticBoundNodeFactory.Return(boundExpression);
				syntheticBoundNodeFactory.CloseMethod(syntheticBoundNodeFactory.Block(ImmutableArray.Create<LocalSymbol>(boundLocal.LocalSymbol), new BoundStatement[] { boundStatement, boundStatement2 }));
			}

			// Token: 0x1700276D RID: 10093
			// (get) Token: 0x06008CE8 RID: 36072 RVA: 0x001F471A File Offset: 0x001F291A
			internal override bool HasSpecialName
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06008CE9 RID: 36073 RVA: 0x001F4720 File Offset: 0x001F2920
			internal AnonymousTypeEqualsMethodSymbol(NamedTypeSymbol container)
				: base(container, "Equals")
			{
				this._parameters = ImmutableArray.Create<ParameterSymbol>(SynthesizedParameterSymbol.Create(this, TypeWithAnnotations.Create(base.Manager.System_Object, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), 0, RefKind.None, "value", default(ImmutableArray<CustomModifier>), null));
			}

			// Token: 0x1700276E RID: 10094
			// (get) Token: 0x06008CEA RID: 36074 RVA: 0x001F4775 File Offset: 0x001F2975
			public override MethodKind MethodKind
			{
				get
				{
					return MethodKind.Ordinary;
				}
			}

			// Token: 0x1700276F RID: 10095
			// (get) Token: 0x06008CEB RID: 36075 RVA: 0x001F4779 File Offset: 0x001F2979
			public override bool ReturnsVoid
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002770 RID: 10096
			// (get) Token: 0x06008CEC RID: 36076 RVA: 0x001F477C File Offset: 0x001F297C
			public override RefKind RefKind
			{
				get
				{
					return RefKind.None;
				}
			}

			// Token: 0x17002771 RID: 10097
			// (get) Token: 0x06008CED RID: 36077 RVA: 0x001F4780 File Offset: 0x001F2980
			public override TypeWithAnnotations ReturnTypeWithAnnotations
			{
				get
				{
					return TypeWithAnnotations.Create(base.Manager.System_Boolean, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
				}
			}

			// Token: 0x17002772 RID: 10098
			// (get) Token: 0x06008CEE RID: 36078 RVA: 0x001F47A7 File Offset: 0x001F29A7
			public override ImmutableArray<ParameterSymbol> Parameters
			{
				get
				{
					return this._parameters;
				}
			}

			// Token: 0x17002773 RID: 10099
			// (get) Token: 0x06008CEF RID: 36079 RVA: 0x001F47AF File Offset: 0x001F29AF
			public override bool IsOverride
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06008CF0 RID: 36080 RVA: 0x001F47B2 File Offset: 0x001F29B2
			internal sealed override bool IsMetadataVirtual(bool ignoreInterfaceImplementationChanges = false)
			{
				return true;
			}

			// Token: 0x17002774 RID: 10100
			// (get) Token: 0x06008CF1 RID: 36081 RVA: 0x001F47B5 File Offset: 0x001F29B5
			internal override bool IsMetadataFinal
			{
				get
				{
					return false;
				}
			}

			// Token: 0x04002626 RID: 9766
			private readonly ImmutableArray<ParameterSymbol> _parameters;
		}

		// Token: 0x020008BA RID: 2234
		private sealed class AnonymousTypeGetHashCodeMethodSymbol : AnonymousTypeManager.SynthesizedMethodBase
		{
			// Token: 0x06008CF2 RID: 36082 RVA: 0x001F47B8 File Offset: 0x001F29B8
			internal override void GenerateMethodBody(TypeCompilationState compilationState, BindingDiagnosticBag diagnostics)
			{
				AnonymousTypeManager manager = ((AnonymousTypeManager.AnonymousTypeTemplateSymbol)this.ContainingType).Manager;
				SyntheticBoundNodeFactory syntheticBoundNodeFactory = base.CreateBoundNodeFactory(compilationState, diagnostics);
				AnonymousTypeManager.AnonymousTypeTemplateSymbol anonymousTypeTemplateSymbol = (AnonymousTypeManager.AnonymousTypeTemplateSymbol)this.ContainingType;
				int num = 0;
				foreach (AnonymousTypeManager.AnonymousTypePropertySymbol anonymousTypePropertySymbol in anonymousTypeTemplateSymbol.Properties)
				{
					num = num * -1521134295 + Hash.GetFNVHashCode(anonymousTypePropertySymbol.BackingField.Name);
				}
				BoundExpression boundExpression = syntheticBoundNodeFactory.Literal(num);
				MethodSymbol system_Collections_Generic_EqualityComparer_T__GetHashCode = manager.System_Collections_Generic_EqualityComparer_T__GetHashCode;
				MethodSymbol system_Collections_Generic_EqualityComparer_T__get_Default = manager.System_Collections_Generic_EqualityComparer_T__get_Default;
				BoundLiteral boundLiteral = null;
				for (int i = 0; i < anonymousTypeTemplateSymbol.Properties.Length; i++)
				{
					boundExpression = MethodBodySynthesizer.GenerateHashCombine(boundExpression, system_Collections_Generic_EqualityComparer_T__GetHashCode, system_Collections_Generic_EqualityComparer_T__get_Default, ref boundLiteral, syntheticBoundNodeFactory.Field(syntheticBoundNodeFactory.This(), anonymousTypeTemplateSymbol.Properties[i].BackingField), syntheticBoundNodeFactory);
				}
				syntheticBoundNodeFactory.CloseMethod(syntheticBoundNodeFactory.Block(new BoundStatement[] { syntheticBoundNodeFactory.Return(boundExpression) }));
			}

			// Token: 0x17002775 RID: 10101
			// (get) Token: 0x06008CF3 RID: 36083 RVA: 0x001F48B5 File Offset: 0x001F2AB5
			internal override bool HasSpecialName
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06008CF4 RID: 36084 RVA: 0x001F48B8 File Offset: 0x001F2AB8
			internal AnonymousTypeGetHashCodeMethodSymbol(NamedTypeSymbol container)
				: base(container, "GetHashCode")
			{
			}

			// Token: 0x17002776 RID: 10102
			// (get) Token: 0x06008CF5 RID: 36085 RVA: 0x001F48C6 File Offset: 0x001F2AC6
			public override MethodKind MethodKind
			{
				get
				{
					return MethodKind.Ordinary;
				}
			}

			// Token: 0x17002777 RID: 10103
			// (get) Token: 0x06008CF6 RID: 36086 RVA: 0x001F48CA File Offset: 0x001F2ACA
			public override bool ReturnsVoid
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002778 RID: 10104
			// (get) Token: 0x06008CF7 RID: 36087 RVA: 0x001F48CD File Offset: 0x001F2ACD
			public override RefKind RefKind
			{
				get
				{
					return RefKind.None;
				}
			}

			// Token: 0x17002779 RID: 10105
			// (get) Token: 0x06008CF8 RID: 36088 RVA: 0x001F48D0 File Offset: 0x001F2AD0
			public override TypeWithAnnotations ReturnTypeWithAnnotations
			{
				get
				{
					return TypeWithAnnotations.Create(base.Manager.System_Int32, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
				}
			}

			// Token: 0x1700277A RID: 10106
			// (get) Token: 0x06008CF9 RID: 36089 RVA: 0x001F48F7 File Offset: 0x001F2AF7
			public override ImmutableArray<ParameterSymbol> Parameters
			{
				get
				{
					return ImmutableArray<ParameterSymbol>.Empty;
				}
			}

			// Token: 0x1700277B RID: 10107
			// (get) Token: 0x06008CFA RID: 36090 RVA: 0x001F48FE File Offset: 0x001F2AFE
			public override bool IsOverride
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06008CFB RID: 36091 RVA: 0x001F4901 File Offset: 0x001F2B01
			internal sealed override bool IsMetadataVirtual(bool ignoreInterfaceImplementationChanges = false)
			{
				return true;
			}

			// Token: 0x1700277C RID: 10108
			// (get) Token: 0x06008CFC RID: 36092 RVA: 0x001F4904 File Offset: 0x001F2B04
			internal override bool IsMetadataFinal
			{
				get
				{
					return false;
				}
			}
		}

		// Token: 0x020008BB RID: 2235
		private sealed class AnonymousTypeToStringMethodSymbol : AnonymousTypeManager.SynthesizedMethodBase
		{
			// Token: 0x06008CFD RID: 36093 RVA: 0x001F4908 File Offset: 0x001F2B08
			internal override void GenerateMethodBody(TypeCompilationState compilationState, BindingDiagnosticBag diagnostics)
			{
				AnonymousTypeManager manager = ((AnonymousTypeManager.AnonymousTypeTemplateSymbol)this.ContainingType).Manager;
				SyntheticBoundNodeFactory syntheticBoundNodeFactory = base.CreateBoundNodeFactory(compilationState, diagnostics);
				AnonymousTypeManager.AnonymousTypeTemplateSymbol anonymousTypeTemplateSymbol = (AnonymousTypeManager.AnonymousTypeTemplateSymbol)this.ContainingType;
				int length = anonymousTypeTemplateSymbol.Properties.Length;
				BoundExpression boundExpression2;
				if (length > 0)
				{
					BoundExpression[] array = new BoundExpression[length];
					PooledStringBuilder instance = PooledStringBuilder.GetInstance();
					for (int i = 0; i < length; i++)
					{
						AnonymousTypeManager.AnonymousTypePropertySymbol anonymousTypePropertySymbol = anonymousTypeTemplateSymbol.Properties[i];
						instance.Builder.AppendFormat((i == 0) ? "{{{{ {0} = {{{1}}}" : ", {0} = {{{1}}}", anonymousTypePropertySymbol.Name, i);
						array[i] = syntheticBoundNodeFactory.Convert(manager.System_Object, new BoundLoweredConditionalAccess(syntheticBoundNodeFactory.Syntax, syntheticBoundNodeFactory.Field(syntheticBoundNodeFactory.This(), anonymousTypePropertySymbol.BackingField), null, syntheticBoundNodeFactory.Call(new BoundConditionalReceiver(syntheticBoundNodeFactory.Syntax, i, anonymousTypePropertySymbol.BackingField.Type), manager.System_Object__ToString), null, i, manager.System_String, false), Conversion.ImplicitReference, false);
					}
					instance.Builder.Append(" }}");
					BoundExpression boundExpression = syntheticBoundNodeFactory.Literal(instance.ToStringAndFree());
					MethodSymbol system_String__Format_IFormatProvider = manager.System_String__Format_IFormatProvider;
					boundExpression2 = syntheticBoundNodeFactory.StaticCall(manager.System_String, system_String__Format_IFormatProvider, new BoundExpression[]
					{
						syntheticBoundNodeFactory.Null(system_String__Format_IFormatProvider.Parameters[0].Type),
						boundExpression,
						syntheticBoundNodeFactory.ArrayOrEmpty(manager.System_Object, array)
					});
				}
				else
				{
					boundExpression2 = syntheticBoundNodeFactory.Literal("{ }");
				}
				syntheticBoundNodeFactory.CloseMethod(syntheticBoundNodeFactory.Block(new BoundStatement[] { syntheticBoundNodeFactory.Return(boundExpression2) }));
			}

			// Token: 0x1700277D RID: 10109
			// (get) Token: 0x06008CFE RID: 36094 RVA: 0x001F4ABD File Offset: 0x001F2CBD
			internal override bool HasSpecialName
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06008CFF RID: 36095 RVA: 0x001F4AC0 File Offset: 0x001F2CC0
			internal AnonymousTypeToStringMethodSymbol(NamedTypeSymbol container)
				: base(container, "ToString")
			{
			}

			// Token: 0x1700277E RID: 10110
			// (get) Token: 0x06008D00 RID: 36096 RVA: 0x001F4ACE File Offset: 0x001F2CCE
			public override MethodKind MethodKind
			{
				get
				{
					return MethodKind.Ordinary;
				}
			}

			// Token: 0x1700277F RID: 10111
			// (get) Token: 0x06008D01 RID: 36097 RVA: 0x001F4AD2 File Offset: 0x001F2CD2
			public override bool ReturnsVoid
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002780 RID: 10112
			// (get) Token: 0x06008D02 RID: 36098 RVA: 0x001F4AD5 File Offset: 0x001F2CD5
			public override RefKind RefKind
			{
				get
				{
					return RefKind.None;
				}
			}

			// Token: 0x17002781 RID: 10113
			// (get) Token: 0x06008D03 RID: 36099 RVA: 0x001F4AD8 File Offset: 0x001F2CD8
			public override TypeWithAnnotations ReturnTypeWithAnnotations
			{
				get
				{
					return TypeWithAnnotations.Create(base.Manager.System_String, NullableAnnotation.NotAnnotated, default(ImmutableArray<CustomModifier>));
				}
			}

			// Token: 0x17002782 RID: 10114
			// (get) Token: 0x06008D04 RID: 36100 RVA: 0x001F4AFF File Offset: 0x001F2CFF
			public override ImmutableArray<ParameterSymbol> Parameters
			{
				get
				{
					return ImmutableArray<ParameterSymbol>.Empty;
				}
			}

			// Token: 0x17002783 RID: 10115
			// (get) Token: 0x06008D05 RID: 36101 RVA: 0x001F4B06 File Offset: 0x001F2D06
			public override bool IsOverride
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06008D06 RID: 36102 RVA: 0x001F4B09 File Offset: 0x001F2D09
			internal sealed override bool IsMetadataVirtual(bool ignoreInterfaceImplementationChanges = false)
			{
				return true;
			}

			// Token: 0x17002784 RID: 10116
			// (get) Token: 0x06008D07 RID: 36103 RVA: 0x001F4B0C File Offset: 0x001F2D0C
			internal override bool IsMetadataFinal
			{
				get
				{
					return false;
				}
			}
		}

		// Token: 0x020008BC RID: 2236
		private struct SynthesizedDelegateKey : IEquatable<AnonymousTypeManager.SynthesizedDelegateKey>
		{
			// Token: 0x06008D08 RID: 36104 RVA: 0x001F4B0F File Offset: 0x001F2D0F
			public SynthesizedDelegateKey(int parameterCount, BitVector byRefs, bool returnsVoid, int generation)
			{
				this._parameterCount = (ushort)parameterCount;
				this._returnsVoid = returnsVoid;
				this._generation = generation;
				this._byRefs = byRefs;
			}

			// Token: 0x06008D09 RID: 36105 RVA: 0x001F4B2F File Offset: 0x001F2D2F
			public string MakeTypeName()
			{
				return GeneratedNames.MakeDynamicCallSiteDelegateName(this._byRefs, this._returnsVoid, this._generation);
			}

			// Token: 0x06008D0A RID: 36106 RVA: 0x001F4B48 File Offset: 0x001F2D48
			public override bool Equals(object obj)
			{
				return obj is AnonymousTypeManager.SynthesizedDelegateKey && this.Equals((AnonymousTypeManager.SynthesizedDelegateKey)obj);
			}

			// Token: 0x06008D0B RID: 36107 RVA: 0x001F4B60 File Offset: 0x001F2D60
			public bool Equals(AnonymousTypeManager.SynthesizedDelegateKey other)
			{
				return this._parameterCount == other._parameterCount && this._returnsVoid == other._returnsVoid && this._generation == other._generation && this._byRefs.Equals(other._byRefs);
			}

			// Token: 0x06008D0C RID: 36108 RVA: 0x001F4BB0 File Offset: 0x001F2DB0
			public override int GetHashCode()
			{
				return Hash.Combine(Hash.Combine((int)this._parameterCount, this._generation), Hash.Combine(this._returnsVoid.GetHashCode(), this._byRefs.GetHashCode()));
			}

			// Token: 0x04002627 RID: 9767
			private readonly BitVector _byRefs;

			// Token: 0x04002628 RID: 9768
			private readonly ushort _parameterCount;

			// Token: 0x04002629 RID: 9769
			private readonly bool _returnsVoid;

			// Token: 0x0400262A RID: 9770
			private readonly int _generation;
		}

		// Token: 0x020008BD RID: 2237
		private struct SynthesizedDelegateValue
		{
			// Token: 0x06008D0D RID: 36109 RVA: 0x001F4BFA File Offset: 0x001F2DFA
			public SynthesizedDelegateValue(AnonymousTypeManager manager, SynthesizedDelegateSymbol @delegate)
			{
				this.Manager = manager;
				this.Delegate = @delegate;
			}

			// Token: 0x0400262B RID: 9771
			public readonly SynthesizedDelegateSymbol Delegate;

			// Token: 0x0400262C RID: 9772
			public readonly AnonymousTypeManager Manager;
		}

		// Token: 0x020008BE RID: 2238
		private class SynthesizedDelegateSymbolComparer : IComparer<SynthesizedDelegateSymbol>
		{
			// Token: 0x06008D0E RID: 36110 RVA: 0x001F4C0A File Offset: 0x001F2E0A
			public int Compare(SynthesizedDelegateSymbol x, SynthesizedDelegateSymbol y)
			{
				return x.MetadataName.CompareTo(y.MetadataName);
			}

			// Token: 0x0400262D RID: 9773
			public static readonly AnonymousTypeManager.SynthesizedDelegateSymbolComparer Instance = new AnonymousTypeManager.SynthesizedDelegateSymbolComparer();
		}

		// Token: 0x020008BF RID: 2239
		private sealed class AnonymousTypeComparer : IComparer<AnonymousTypeManager.AnonymousTypeTemplateSymbol>
		{
			// Token: 0x06008D11 RID: 36113 RVA: 0x001F4C31 File Offset: 0x001F2E31
			public AnonymousTypeComparer(CSharpCompilation compilation)
			{
				this._compilation = compilation;
			}

			// Token: 0x06008D12 RID: 36114 RVA: 0x001F4C40 File Offset: 0x001F2E40
			public int Compare(AnonymousTypeManager.AnonymousTypeTemplateSymbol x, AnonymousTypeManager.AnonymousTypeTemplateSymbol y)
			{
				if (x == y)
				{
					return 0;
				}
				int num = this.CompareLocations(x.SmallestLocation, y.SmallestLocation);
				if (num == 0)
				{
					num = string.CompareOrdinal(x.TypeDescriptorKey, y.TypeDescriptorKey);
				}
				return num;
			}

			// Token: 0x06008D13 RID: 36115 RVA: 0x001F4C7C File Offset: 0x001F2E7C
			private int CompareLocations(Location x, Location y)
			{
				if (x == y)
				{
					return 0;
				}
				if (x == Location.None)
				{
					return -1;
				}
				if (y == Location.None)
				{
					return 1;
				}
				return this._compilation.CompareSourceLocations(x, y);
			}

			// Token: 0x0400262E RID: 9774
			private readonly CSharpCompilation _compilation;
		}

		// Token: 0x020008C0 RID: 2240
		internal sealed class AnonymousTypePublicSymbol : NamedTypeSymbol
		{
			// Token: 0x06008D14 RID: 36116 RVA: 0x001F4CB4 File Offset: 0x001F2EB4
			internal AnonymousTypePublicSymbol(AnonymousTypeManager manager, AnonymousTypeDescriptor typeDescr)
				: base(null)
			{
				this.Manager = manager;
				this.TypeDescriptor = typeDescr;
				ImmutableArray<AnonymousTypeField> fields = typeDescr.Fields;
				ImmutableArray<AnonymousTypeManager.AnonymousTypePropertySymbol> immutableArray = fields.SelectAsArray((AnonymousTypeField field, int i, AnonymousTypeManager.AnonymousTypePublicSymbol type) => new AnonymousTypeManager.AnonymousTypePropertySymbol(type, field, i), this);
				ArrayBuilder<Symbol> instance = ArrayBuilder<Symbol>.GetInstance(fields.Length * 2 + 1);
				foreach (AnonymousTypeManager.AnonymousTypePropertySymbol anonymousTypePropertySymbol in immutableArray)
				{
					instance.Add(anonymousTypePropertySymbol);
					instance.Add(anonymousTypePropertySymbol.GetMethod);
				}
				this.Properties = immutableArray;
				instance.Add(new AnonymousTypeManager.AnonymousTypeConstructorSymbol(this, immutableArray));
				this._members = instance.ToImmutableAndFree();
				foreach (Symbol symbol in this._members)
				{
					this._nameToSymbols.Add(symbol.Name, symbol);
				}
			}

			// Token: 0x06008D15 RID: 36117 RVA: 0x001F4DA7 File Offset: 0x001F2FA7
			protected override NamedTypeSymbol WithTupleDataCore(NamedTypeSymbol.TupleExtraData newData)
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x06008D16 RID: 36118 RVA: 0x001F4DAE File Offset: 0x001F2FAE
			public override ImmutableArray<Symbol> GetMembers()
			{
				return this._members;
			}

			// Token: 0x06008D17 RID: 36119 RVA: 0x001F4DB6 File Offset: 0x001F2FB6
			internal override IEnumerable<FieldSymbol> GetFieldsToEmit()
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x17002785 RID: 10117
			// (get) Token: 0x06008D18 RID: 36120 RVA: 0x001F4DBD File Offset: 0x001F2FBD
			internal override ImmutableArray<TypeWithAnnotations> TypeArgumentsWithAnnotationsNoUseSiteDiagnostics
			{
				get
				{
					return ImmutableArray<TypeWithAnnotations>.Empty;
				}
			}

			// Token: 0x17002786 RID: 10118
			// (get) Token: 0x06008D19 RID: 36121 RVA: 0x001F4DC4 File Offset: 0x001F2FC4
			internal override bool HasCodeAnalysisEmbeddedAttribute
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06008D1A RID: 36122 RVA: 0x001F4DC8 File Offset: 0x001F2FC8
			public override ImmutableArray<Symbol> GetMembers(string name)
			{
				MultiDictionary<string, Symbol>.ValueSet valueSet = this._nameToSymbols[name];
				ArrayBuilder<Symbol> instance = ArrayBuilder<Symbol>.GetInstance(valueSet.Count);
				foreach (Symbol symbol in valueSet)
				{
					instance.Add(symbol);
				}
				return instance.ToImmutableAndFree();
			}

			// Token: 0x06008D1B RID: 36123 RVA: 0x001F4E38 File Offset: 0x001F3038
			internal override ImmutableArray<Symbol> GetEarlyAttributeDecodingMembers()
			{
				return this.GetMembersUnordered();
			}

			// Token: 0x06008D1C RID: 36124 RVA: 0x001F4E40 File Offset: 0x001F3040
			internal override ImmutableArray<Symbol> GetEarlyAttributeDecodingMembers(string name)
			{
				return this.GetMembers(name);
			}

			// Token: 0x17002787 RID: 10119
			// (get) Token: 0x06008D1D RID: 36125 RVA: 0x001F4E49 File Offset: 0x001F3049
			public override IEnumerable<string> MemberNames
			{
				get
				{
					return this._nameToSymbols.Keys;
				}
			}

			// Token: 0x17002788 RID: 10120
			// (get) Token: 0x06008D1E RID: 36126 RVA: 0x001F4E56 File Offset: 0x001F3056
			public override Symbol ContainingSymbol
			{
				get
				{
					return this.Manager.Compilation.SourceModule.GlobalNamespace;
				}
			}

			// Token: 0x17002789 RID: 10121
			// (get) Token: 0x06008D1F RID: 36127 RVA: 0x001F4E6D File Offset: 0x001F306D
			public override string Name
			{
				get
				{
					return string.Empty;
				}
			}

			// Token: 0x1700278A RID: 10122
			// (get) Token: 0x06008D20 RID: 36128 RVA: 0x001F4E74 File Offset: 0x001F3074
			public override string MetadataName
			{
				get
				{
					return string.Empty;
				}
			}

			// Token: 0x1700278B RID: 10123
			// (get) Token: 0x06008D21 RID: 36129 RVA: 0x001F4E7B File Offset: 0x001F307B
			internal override bool MangleName
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700278C RID: 10124
			// (get) Token: 0x06008D22 RID: 36130 RVA: 0x001F4E7E File Offset: 0x001F307E
			public override int Arity
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x1700278D RID: 10125
			// (get) Token: 0x06008D23 RID: 36131 RVA: 0x001F4E81 File Offset: 0x001F3081
			public override bool IsImplicitlyDeclared
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700278E RID: 10126
			// (get) Token: 0x06008D24 RID: 36132 RVA: 0x001F4E84 File Offset: 0x001F3084
			public override ImmutableArray<TypeParameterSymbol> TypeParameters
			{
				get
				{
					return ImmutableArray<TypeParameterSymbol>.Empty;
				}
			}

			// Token: 0x1700278F RID: 10127
			// (get) Token: 0x06008D25 RID: 36133 RVA: 0x001F4E8B File Offset: 0x001F308B
			public override bool IsAbstract
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002790 RID: 10128
			// (get) Token: 0x06008D26 RID: 36134 RVA: 0x001F4E8E File Offset: 0x001F308E
			public sealed override bool IsRefLikeType
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002791 RID: 10129
			// (get) Token: 0x06008D27 RID: 36135 RVA: 0x001F4E91 File Offset: 0x001F3091
			public sealed override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002792 RID: 10130
			// (get) Token: 0x06008D28 RID: 36136 RVA: 0x001F4E94 File Offset: 0x001F3094
			public override bool IsSealed
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17002793 RID: 10131
			// (get) Token: 0x06008D29 RID: 36137 RVA: 0x001F4E97 File Offset: 0x001F3097
			public override bool MightContainExtensionMethods
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002794 RID: 10132
			// (get) Token: 0x06008D2A RID: 36138 RVA: 0x001F4E9A File Offset: 0x001F309A
			internal override bool HasSpecialName
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06008D2B RID: 36139 RVA: 0x001F4E9D File Offset: 0x001F309D
			public override ImmutableArray<NamedTypeSymbol> GetTypeMembers()
			{
				return ImmutableArray<NamedTypeSymbol>.Empty;
			}

			// Token: 0x06008D2C RID: 36140 RVA: 0x001F4EA4 File Offset: 0x001F30A4
			public override ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name)
			{
				return ImmutableArray<NamedTypeSymbol>.Empty;
			}

			// Token: 0x06008D2D RID: 36141 RVA: 0x001F4EAB File Offset: 0x001F30AB
			public override ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name, int arity)
			{
				return ImmutableArray<NamedTypeSymbol>.Empty;
			}

			// Token: 0x17002795 RID: 10133
			// (get) Token: 0x06008D2E RID: 36142 RVA: 0x001F4EB2 File Offset: 0x001F30B2
			public override Accessibility DeclaredAccessibility
			{
				get
				{
					return Accessibility.Internal;
				}
			}

			// Token: 0x06008D2F RID: 36143 RVA: 0x001F4EB5 File Offset: 0x001F30B5
			internal override ImmutableArray<NamedTypeSymbol> InterfacesNoUseSiteDiagnostics(ConsList<TypeSymbol> basesBeingResolved)
			{
				return ImmutableArray<NamedTypeSymbol>.Empty;
			}

			// Token: 0x06008D30 RID: 36144 RVA: 0x001F4EBC File Offset: 0x001F30BC
			internal override ImmutableArray<NamedTypeSymbol> GetInterfacesToEmit()
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x17002796 RID: 10134
			// (get) Token: 0x06008D31 RID: 36145 RVA: 0x001F4EC3 File Offset: 0x001F30C3
			internal override NamedTypeSymbol BaseTypeNoUseSiteDiagnostics
			{
				get
				{
					return this.Manager.System_Object;
				}
			}

			// Token: 0x17002797 RID: 10135
			// (get) Token: 0x06008D32 RID: 36146 RVA: 0x001F4ED0 File Offset: 0x001F30D0
			public override TypeKind TypeKind
			{
				get
				{
					return TypeKind.Class;
				}
			}

			// Token: 0x17002798 RID: 10136
			// (get) Token: 0x06008D33 RID: 36147 RVA: 0x001F4ED3 File Offset: 0x001F30D3
			internal override bool IsInterface
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002799 RID: 10137
			// (get) Token: 0x06008D34 RID: 36148 RVA: 0x001F4ED6 File Offset: 0x001F30D6
			public override ImmutableArray<Location> Locations
			{
				get
				{
					return ImmutableArray.Create<Location>(this.TypeDescriptor.Location);
				}
			}

			// Token: 0x1700279A RID: 10138
			// (get) Token: 0x06008D35 RID: 36149 RVA: 0x001F4EE8 File Offset: 0x001F30E8
			public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
			{
				get
				{
					return Symbol.GetDeclaringSyntaxReferenceHelper<AnonymousObjectCreationExpressionSyntax>(this.Locations);
				}
			}

			// Token: 0x1700279B RID: 10139
			// (get) Token: 0x06008D36 RID: 36150 RVA: 0x001F4EF5 File Offset: 0x001F30F5
			public override bool IsStatic
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700279C RID: 10140
			// (get) Token: 0x06008D37 RID: 36151 RVA: 0x001F4EF8 File Offset: 0x001F30F8
			public override bool IsAnonymousType
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700279D RID: 10141
			// (get) Token: 0x06008D38 RID: 36152 RVA: 0x001F4EFB File Offset: 0x001F30FB
			public override NamedTypeSymbol ConstructedFrom
			{
				get
				{
					return this;
				}
			}

			// Token: 0x1700279E RID: 10142
			// (get) Token: 0x06008D39 RID: 36153 RVA: 0x001F4EFE File Offset: 0x001F30FE
			internal override bool ShouldAddWinRTMembers
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700279F RID: 10143
			// (get) Token: 0x06008D3A RID: 36154 RVA: 0x001F4F01 File Offset: 0x001F3101
			internal override bool IsWindowsRuntimeImport
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170027A0 RID: 10144
			// (get) Token: 0x06008D3B RID: 36155 RVA: 0x001F4F04 File Offset: 0x001F3104
			internal override bool IsComImport
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170027A1 RID: 10145
			// (get) Token: 0x06008D3C RID: 36156 RVA: 0x001F4F07 File Offset: 0x001F3107
			internal sealed override ObsoleteAttributeData ObsoleteAttributeData
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170027A2 RID: 10146
			// (get) Token: 0x06008D3D RID: 36157 RVA: 0x001F4F0C File Offset: 0x001F310C
			internal override TypeLayout Layout
			{
				get
				{
					return default(TypeLayout);
				}
			}

			// Token: 0x170027A3 RID: 10147
			// (get) Token: 0x06008D3E RID: 36158 RVA: 0x001F4F22 File Offset: 0x001F3122
			internal override CharSet MarshallingCharSet
			{
				get
				{
					return base.DefaultMarshallingCharSet;
				}
			}

			// Token: 0x170027A4 RID: 10148
			// (get) Token: 0x06008D3F RID: 36159 RVA: 0x001F4F2A File Offset: 0x001F312A
			public override bool IsSerializable
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170027A5 RID: 10149
			// (get) Token: 0x06008D40 RID: 36160 RVA: 0x001F4F2D File Offset: 0x001F312D
			public sealed override bool AreLocalsZeroed
			{
				get
				{
					throw ExceptionUtilities.Unreachable;
				}
			}

			// Token: 0x170027A6 RID: 10150
			// (get) Token: 0x06008D41 RID: 36161 RVA: 0x001F4F34 File Offset: 0x001F3134
			internal override bool HasDeclarativeSecurity
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06008D42 RID: 36162 RVA: 0x001F4F37 File Offset: 0x001F3137
			internal override IEnumerable<SecurityAttribute> GetSecurityInformation()
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x06008D43 RID: 36163 RVA: 0x001F4F3E File Offset: 0x001F313E
			internal override ImmutableArray<string> GetAppliedConditionalSymbols()
			{
				return ImmutableArray<string>.Empty;
			}

			// Token: 0x06008D44 RID: 36164 RVA: 0x001F4F45 File Offset: 0x001F3145
			internal override AttributeUsageInfo GetAttributeUsageInfo()
			{
				return AttributeUsageInfo.Null;
			}

			// Token: 0x06008D45 RID: 36165 RVA: 0x001F4F4C File Offset: 0x001F314C
			internal override NamedTypeSymbol GetDeclaredBaseType(ConsList<TypeSymbol> basesBeingResolved)
			{
				return this.Manager.System_Object;
			}

			// Token: 0x06008D46 RID: 36166 RVA: 0x001F4F59 File Offset: 0x001F3159
			internal override ImmutableArray<NamedTypeSymbol> GetDeclaredInterfaces(ConsList<TypeSymbol> basesBeingResolved)
			{
				return ImmutableArray<NamedTypeSymbol>.Empty;
			}

			// Token: 0x06008D47 RID: 36167 RVA: 0x001F4F60 File Offset: 0x001F3160
			internal sealed override NamedTypeSymbol AsNativeInteger()
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x170027A7 RID: 10151
			// (get) Token: 0x06008D48 RID: 36168 RVA: 0x001F4F67 File Offset: 0x001F3167
			internal sealed override NamedTypeSymbol NativeIntegerUnderlyingType
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170027A8 RID: 10152
			// (get) Token: 0x06008D49 RID: 36169 RVA: 0x001F4F6A File Offset: 0x001F316A
			internal override bool IsRecord
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170027A9 RID: 10153
			// (get) Token: 0x06008D4A RID: 36170 RVA: 0x001F4F6D File Offset: 0x001F316D
			internal override bool IsRecordStruct
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06008D4B RID: 36171 RVA: 0x001F4F70 File Offset: 0x001F3170
			internal override bool Equals(TypeSymbol t2, TypeCompareKind comparison)
			{
				if (this == t2)
				{
					return true;
				}
				AnonymousTypeManager.AnonymousTypePublicSymbol anonymousTypePublicSymbol = t2 as AnonymousTypeManager.AnonymousTypePublicSymbol;
				return anonymousTypePublicSymbol != null && this.TypeDescriptor.Equals(anonymousTypePublicSymbol.TypeDescriptor, comparison);
			}

			// Token: 0x06008D4C RID: 36172 RVA: 0x001F4FA1 File Offset: 0x001F31A1
			public override int GetHashCode()
			{
				return this.TypeDescriptor.GetHashCode();
			}

			// Token: 0x06008D4D RID: 36173 RVA: 0x001F4FB4 File Offset: 0x001F31B4
			internal override bool HasPossibleWellKnownCloneMethod()
			{
				return false;
			}

			// Token: 0x0400262F RID: 9775
			private readonly ImmutableArray<Symbol> _members;

			// Token: 0x04002630 RID: 9776
			internal readonly ImmutableArray<AnonymousTypeManager.AnonymousTypePropertySymbol> Properties;

			// Token: 0x04002631 RID: 9777
			private readonly MultiDictionary<string, Symbol> _nameToSymbols = new MultiDictionary<string, Symbol>();

			// Token: 0x04002632 RID: 9778
			internal readonly AnonymousTypeManager Manager;

			// Token: 0x04002633 RID: 9779
			internal readonly AnonymousTypeDescriptor TypeDescriptor;
		}

		// Token: 0x020008C1 RID: 2241
		private sealed class AnonymousTypeFieldSymbol : FieldSymbol
		{
			// Token: 0x06008D4E RID: 36174 RVA: 0x001F4FB7 File Offset: 0x001F31B7
			public AnonymousTypeFieldSymbol(PropertySymbol property)
			{
				this._property = property;
			}

			// Token: 0x06008D4F RID: 36175 RVA: 0x001F4FC6 File Offset: 0x001F31C6
			internal override TypeWithAnnotations GetFieldType(ConsList<FieldSymbol> fieldsBeingBound)
			{
				return this._property.TypeWithAnnotations;
			}

			// Token: 0x170027AA RID: 10154
			// (get) Token: 0x06008D50 RID: 36176 RVA: 0x001F4FD3 File Offset: 0x001F31D3
			public override string Name
			{
				get
				{
					return GeneratedNames.MakeAnonymousTypeBackingFieldName(this._property.Name);
				}
			}

			// Token: 0x170027AB RID: 10155
			// (get) Token: 0x06008D51 RID: 36177 RVA: 0x001F4FE5 File Offset: 0x001F31E5
			public override FlowAnalysisAnnotations FlowAnalysisAnnotations
			{
				get
				{
					return FlowAnalysisAnnotations.None;
				}
			}

			// Token: 0x170027AC RID: 10156
			// (get) Token: 0x06008D52 RID: 36178 RVA: 0x001F4FE8 File Offset: 0x001F31E8
			internal override bool HasSpecialName
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170027AD RID: 10157
			// (get) Token: 0x06008D53 RID: 36179 RVA: 0x001F4FEB File Offset: 0x001F31EB
			internal override bool HasRuntimeSpecialName
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170027AE RID: 10158
			// (get) Token: 0x06008D54 RID: 36180 RVA: 0x001F4FEE File Offset: 0x001F31EE
			internal override bool IsNotSerialized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170027AF RID: 10159
			// (get) Token: 0x06008D55 RID: 36181 RVA: 0x001F4FF1 File Offset: 0x001F31F1
			internal override MarshalPseudoCustomAttributeData MarshallingInformation
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170027B0 RID: 10160
			// (get) Token: 0x06008D56 RID: 36182 RVA: 0x001F4FF4 File Offset: 0x001F31F4
			internal override int? TypeLayoutOffset
			{
				get
				{
					return default(int?);
				}
			}

			// Token: 0x170027B1 RID: 10161
			// (get) Token: 0x06008D57 RID: 36183 RVA: 0x001F500A File Offset: 0x001F320A
			public override Symbol AssociatedSymbol
			{
				get
				{
					return this._property;
				}
			}

			// Token: 0x170027B2 RID: 10162
			// (get) Token: 0x06008D58 RID: 36184 RVA: 0x001F5012 File Offset: 0x001F3212
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170027B3 RID: 10163
			// (get) Token: 0x06008D59 RID: 36185 RVA: 0x001F5015 File Offset: 0x001F3215
			public override bool IsVolatile
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170027B4 RID: 10164
			// (get) Token: 0x06008D5A RID: 36186 RVA: 0x001F5018 File Offset: 0x001F3218
			public override bool IsConst
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170027B5 RID: 10165
			// (get) Token: 0x06008D5B RID: 36187 RVA: 0x001F501B File Offset: 0x001F321B
			internal sealed override ObsoleteAttributeData ObsoleteAttributeData
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06008D5C RID: 36188 RVA: 0x001F501E File Offset: 0x001F321E
			internal override ConstantValue GetConstantValue(ConstantFieldsInProgress inProgress, bool earlyDecodingWellKnownAttributes)
			{
				return null;
			}

			// Token: 0x170027B6 RID: 10166
			// (get) Token: 0x06008D5D RID: 36189 RVA: 0x001F5021 File Offset: 0x001F3221
			public override Symbol ContainingSymbol
			{
				get
				{
					return this._property.ContainingType;
				}
			}

			// Token: 0x170027B7 RID: 10167
			// (get) Token: 0x06008D5E RID: 36190 RVA: 0x001F502E File Offset: 0x001F322E
			public override NamedTypeSymbol ContainingType
			{
				get
				{
					return this._property.ContainingType;
				}
			}

			// Token: 0x170027B8 RID: 10168
			// (get) Token: 0x06008D5F RID: 36191 RVA: 0x001F503B File Offset: 0x001F323B
			public override ImmutableArray<Location> Locations
			{
				get
				{
					return ImmutableArray<Location>.Empty;
				}
			}

			// Token: 0x170027B9 RID: 10169
			// (get) Token: 0x06008D60 RID: 36192 RVA: 0x001F5042 File Offset: 0x001F3242
			public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
			{
				get
				{
					return ImmutableArray<SyntaxReference>.Empty;
				}
			}

			// Token: 0x170027BA RID: 10170
			// (get) Token: 0x06008D61 RID: 36193 RVA: 0x001F5049 File Offset: 0x001F3249
			public override Accessibility DeclaredAccessibility
			{
				get
				{
					return Accessibility.Private;
				}
			}

			// Token: 0x170027BB RID: 10171
			// (get) Token: 0x06008D62 RID: 36194 RVA: 0x001F504C File Offset: 0x001F324C
			public override bool IsStatic
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170027BC RID: 10172
			// (get) Token: 0x06008D63 RID: 36195 RVA: 0x001F504F File Offset: 0x001F324F
			public override bool IsImplicitlyDeclared
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06008D64 RID: 36196 RVA: 0x001F5054 File Offset: 0x001F3254
			internal override void AddSynthesizedAttributes(PEModuleBuilder moduleBuilder, ref ArrayBuilder<SynthesizedAttributeData> attributes)
			{
				base.AddSynthesizedAttributes(moduleBuilder, ref attributes);
				AnonymousTypeManager manager = ((AnonymousTypeManager.AnonymousTypeTemplateSymbol)this.ContainingSymbol).Manager;
				Symbol.AddSynthesizedAttribute(ref attributes, manager.Compilation.TrySynthesizeAttribute(WellKnownMember.System_Diagnostics_DebuggerBrowsableAttribute__ctor, ImmutableArray.Create<TypedConstant>(new TypedConstant(manager.System_Diagnostics_DebuggerBrowsableState, TypedConstantKind.Enum, 0)), default(ImmutableArray<KeyValuePair<WellKnownMember, TypedConstant>>), false));
			}

			// Token: 0x04002634 RID: 9780
			private readonly PropertySymbol _property;
		}

		// Token: 0x020008C2 RID: 2242
		internal sealed class AnonymousTypePropertySymbol : PropertySymbol
		{
			// Token: 0x06008D65 RID: 36197 RVA: 0x001F50AE File Offset: 0x001F32AE
			internal AnonymousTypePropertySymbol(AnonymousTypeManager.AnonymousTypeTemplateSymbol container, AnonymousTypeField field, TypeWithAnnotations fieldTypeWithAnnotations, int index)
				: this(container, field, fieldTypeWithAnnotations, index, ImmutableArray<Location>.Empty, true)
			{
			}

			// Token: 0x06008D66 RID: 36198 RVA: 0x001F50C1 File Offset: 0x001F32C1
			internal AnonymousTypePropertySymbol(AnonymousTypeManager.AnonymousTypePublicSymbol container, AnonymousTypeField field, int index)
				: this(container, field, field.TypeWithAnnotations, index, ImmutableArray.Create<Location>(field.Location), false)
			{
			}

			// Token: 0x06008D67 RID: 36199 RVA: 0x001F50E0 File Offset: 0x001F32E0
			private AnonymousTypePropertySymbol(NamedTypeSymbol container, AnonymousTypeField field, TypeWithAnnotations fieldTypeWithAnnotations, int index, ImmutableArray<Location> locations, bool includeBackingField)
			{
				this._containingType = container;
				this._typeWithAnnotations = fieldTypeWithAnnotations;
				this._name = field.Name;
				this._index = index;
				this._locations = locations;
				this._getMethod = new AnonymousTypeManager.AnonymousTypePropertyGetAccessorSymbol(this);
				this._backingField = (includeBackingField ? new AnonymousTypeManager.AnonymousTypeFieldSymbol(this) : null);
			}

			// Token: 0x170027BD RID: 10173
			// (get) Token: 0x06008D68 RID: 36200 RVA: 0x001F513C File Offset: 0x001F333C
			internal override int? MemberIndexOpt
			{
				get
				{
					return new int?(this._index);
				}
			}

			// Token: 0x170027BE RID: 10174
			// (get) Token: 0x06008D69 RID: 36201 RVA: 0x001F5149 File Offset: 0x001F3349
			public override RefKind RefKind
			{
				get
				{
					return RefKind.None;
				}
			}

			// Token: 0x170027BF RID: 10175
			// (get) Token: 0x06008D6A RID: 36202 RVA: 0x001F514C File Offset: 0x001F334C
			public override TypeWithAnnotations TypeWithAnnotations
			{
				get
				{
					return this._typeWithAnnotations;
				}
			}

			// Token: 0x170027C0 RID: 10176
			// (get) Token: 0x06008D6B RID: 36203 RVA: 0x001F5154 File Offset: 0x001F3354
			public override string Name
			{
				get
				{
					return this._name;
				}
			}

			// Token: 0x170027C1 RID: 10177
			// (get) Token: 0x06008D6C RID: 36204 RVA: 0x001F515C File Offset: 0x001F335C
			internal override bool HasSpecialName
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170027C2 RID: 10178
			// (get) Token: 0x06008D6D RID: 36205 RVA: 0x001F515F File Offset: 0x001F335F
			public override bool IsImplicitlyDeclared
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170027C3 RID: 10179
			// (get) Token: 0x06008D6E RID: 36206 RVA: 0x001F5162 File Offset: 0x001F3362
			public override ImmutableArray<Location> Locations
			{
				get
				{
					return this._locations;
				}
			}

			// Token: 0x170027C4 RID: 10180
			// (get) Token: 0x06008D6F RID: 36207 RVA: 0x001F516A File Offset: 0x001F336A
			public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
			{
				get
				{
					return Symbol.GetDeclaringSyntaxReferenceHelper<AnonymousObjectMemberDeclaratorSyntax>(this.Locations);
				}
			}

			// Token: 0x170027C5 RID: 10181
			// (get) Token: 0x06008D70 RID: 36208 RVA: 0x001F5177 File Offset: 0x001F3377
			public override bool IsStatic
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170027C6 RID: 10182
			// (get) Token: 0x06008D71 RID: 36209 RVA: 0x001F517A File Offset: 0x001F337A
			public override bool IsOverride
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170027C7 RID: 10183
			// (get) Token: 0x06008D72 RID: 36210 RVA: 0x001F517D File Offset: 0x001F337D
			public override bool IsVirtual
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170027C8 RID: 10184
			// (get) Token: 0x06008D73 RID: 36211 RVA: 0x001F5180 File Offset: 0x001F3380
			public override bool IsIndexer
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170027C9 RID: 10185
			// (get) Token: 0x06008D74 RID: 36212 RVA: 0x001F5183 File Offset: 0x001F3383
			public override bool IsSealed
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170027CA RID: 10186
			// (get) Token: 0x06008D75 RID: 36213 RVA: 0x001F5186 File Offset: 0x001F3386
			public override bool IsAbstract
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170027CB RID: 10187
			// (get) Token: 0x06008D76 RID: 36214 RVA: 0x001F5189 File Offset: 0x001F3389
			internal sealed override ObsoleteAttributeData ObsoleteAttributeData
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170027CC RID: 10188
			// (get) Token: 0x06008D77 RID: 36215 RVA: 0x001F518C File Offset: 0x001F338C
			public override ImmutableArray<ParameterSymbol> Parameters
			{
				get
				{
					return ImmutableArray<ParameterSymbol>.Empty;
				}
			}

			// Token: 0x170027CD RID: 10189
			// (get) Token: 0x06008D78 RID: 36216 RVA: 0x001F5193 File Offset: 0x001F3393
			public override MethodSymbol SetMethod
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170027CE RID: 10190
			// (get) Token: 0x06008D79 RID: 36217 RVA: 0x001F5196 File Offset: 0x001F3396
			public override ImmutableArray<CustomModifier> RefCustomModifiers
			{
				get
				{
					return ImmutableArray<CustomModifier>.Empty;
				}
			}

			// Token: 0x170027CF RID: 10191
			// (get) Token: 0x06008D7A RID: 36218 RVA: 0x001F519D File Offset: 0x001F339D
			internal override CallingConvention CallingConvention
			{
				get
				{
					return CallingConvention.HasThis;
				}
			}

			// Token: 0x170027D0 RID: 10192
			// (get) Token: 0x06008D7B RID: 36219 RVA: 0x001F51A1 File Offset: 0x001F33A1
			public override ImmutableArray<PropertySymbol> ExplicitInterfaceImplementations
			{
				get
				{
					return ImmutableArray<PropertySymbol>.Empty;
				}
			}

			// Token: 0x170027D1 RID: 10193
			// (get) Token: 0x06008D7C RID: 36220 RVA: 0x001F51A8 File Offset: 0x001F33A8
			public override Symbol ContainingSymbol
			{
				get
				{
					return this._containingType;
				}
			}

			// Token: 0x170027D2 RID: 10194
			// (get) Token: 0x06008D7D RID: 36221 RVA: 0x001F51B0 File Offset: 0x001F33B0
			public override NamedTypeSymbol ContainingType
			{
				get
				{
					return this._containingType;
				}
			}

			// Token: 0x170027D3 RID: 10195
			// (get) Token: 0x06008D7E RID: 36222 RVA: 0x001F51B8 File Offset: 0x001F33B8
			public override Accessibility DeclaredAccessibility
			{
				get
				{
					return Accessibility.Public;
				}
			}

			// Token: 0x170027D4 RID: 10196
			// (get) Token: 0x06008D7F RID: 36223 RVA: 0x001F51BB File Offset: 0x001F33BB
			internal override bool MustCallMethodsDirectly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170027D5 RID: 10197
			// (get) Token: 0x06008D80 RID: 36224 RVA: 0x001F51BE File Offset: 0x001F33BE
			public override bool IsExtern
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170027D6 RID: 10198
			// (get) Token: 0x06008D81 RID: 36225 RVA: 0x001F51C1 File Offset: 0x001F33C1
			public override MethodSymbol GetMethod
			{
				get
				{
					return this._getMethod;
				}
			}

			// Token: 0x170027D7 RID: 10199
			// (get) Token: 0x06008D82 RID: 36226 RVA: 0x001F51C9 File Offset: 0x001F33C9
			public FieldSymbol BackingField
			{
				get
				{
					return this._backingField;
				}
			}

			// Token: 0x06008D83 RID: 36227 RVA: 0x001F51D4 File Offset: 0x001F33D4
			public override bool Equals(Symbol obj, TypeCompareKind compareKind)
			{
				if (obj == null)
				{
					return false;
				}
				if (this == obj)
				{
					return true;
				}
				AnonymousTypeManager.AnonymousTypePropertySymbol anonymousTypePropertySymbol = obj as AnonymousTypeManager.AnonymousTypePropertySymbol;
				return anonymousTypePropertySymbol != null && (anonymousTypePropertySymbol != null && anonymousTypePropertySymbol.Name == this.Name) && anonymousTypePropertySymbol.ContainingType.Equals(this.ContainingType, compareKind);
			}

			// Token: 0x06008D84 RID: 36228 RVA: 0x001F5228 File Offset: 0x001F3428
			public override int GetHashCode()
			{
				return Hash.Combine(this.ContainingType.GetHashCode(), this.Name.GetHashCode());
			}

			// Token: 0x04002635 RID: 9781
			private readonly NamedTypeSymbol _containingType;

			// Token: 0x04002636 RID: 9782
			private readonly TypeWithAnnotations _typeWithAnnotations;

			// Token: 0x04002637 RID: 9783
			private readonly string _name;

			// Token: 0x04002638 RID: 9784
			private readonly int _index;

			// Token: 0x04002639 RID: 9785
			private readonly ImmutableArray<Location> _locations;

			// Token: 0x0400263A RID: 9786
			private readonly AnonymousTypeManager.AnonymousTypePropertyGetAccessorSymbol _getMethod;

			// Token: 0x0400263B RID: 9787
			private readonly FieldSymbol _backingField;
		}

		// Token: 0x020008C3 RID: 2243
		private abstract class SynthesizedMethodBase : SynthesizedInstanceMethodSymbol
		{
			// Token: 0x06008D85 RID: 36229 RVA: 0x001F5245 File Offset: 0x001F3445
			public SynthesizedMethodBase(NamedTypeSymbol containingType, string name)
			{
				this._containingType = containingType;
				this._name = name;
			}

			// Token: 0x170027D8 RID: 10200
			// (get) Token: 0x06008D86 RID: 36230 RVA: 0x001F525B File Offset: 0x001F345B
			internal sealed override bool GenerateDebugInfo
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170027D9 RID: 10201
			// (get) Token: 0x06008D87 RID: 36231 RVA: 0x001F525E File Offset: 0x001F345E
			public sealed override int Arity
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x170027DA RID: 10202
			// (get) Token: 0x06008D88 RID: 36232 RVA: 0x001F5261 File Offset: 0x001F3461
			public sealed override Symbol ContainingSymbol
			{
				get
				{
					return this._containingType;
				}
			}

			// Token: 0x170027DB RID: 10203
			// (get) Token: 0x06008D89 RID: 36233 RVA: 0x001F5269 File Offset: 0x001F3469
			public override NamedTypeSymbol ContainingType
			{
				get
				{
					return this._containingType;
				}
			}

			// Token: 0x170027DC RID: 10204
			// (get) Token: 0x06008D8A RID: 36234 RVA: 0x001F5271 File Offset: 0x001F3471
			public override ImmutableArray<Location> Locations
			{
				get
				{
					return ImmutableArray<Location>.Empty;
				}
			}

			// Token: 0x170027DD RID: 10205
			// (get) Token: 0x06008D8B RID: 36235 RVA: 0x001F5278 File Offset: 0x001F3478
			public sealed override Accessibility DeclaredAccessibility
			{
				get
				{
					return Accessibility.Public;
				}
			}

			// Token: 0x170027DE RID: 10206
			// (get) Token: 0x06008D8C RID: 36236 RVA: 0x001F527B File Offset: 0x001F347B
			public sealed override bool IsStatic
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170027DF RID: 10207
			// (get) Token: 0x06008D8D RID: 36237 RVA: 0x001F527E File Offset: 0x001F347E
			public sealed override bool IsVirtual
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170027E0 RID: 10208
			// (get) Token: 0x06008D8E RID: 36238 RVA: 0x001F5281 File Offset: 0x001F3481
			public sealed override bool IsAsync
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170027E1 RID: 10209
			// (get) Token: 0x06008D8F RID: 36239 RVA: 0x001F5284 File Offset: 0x001F3484
			internal sealed override MethodImplAttributes ImplementationAttributes
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x170027E2 RID: 10210
			// (get) Token: 0x06008D90 RID: 36240 RVA: 0x001F5287 File Offset: 0x001F3487
			internal sealed override CallingConvention CallingConvention
			{
				get
				{
					return CallingConvention.HasThis;
				}
			}

			// Token: 0x170027E3 RID: 10211
			// (get) Token: 0x06008D91 RID: 36241 RVA: 0x001F528B File Offset: 0x001F348B
			public sealed override bool IsExtensionMethod
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170027E4 RID: 10212
			// (get) Token: 0x06008D92 RID: 36242 RVA: 0x001F528E File Offset: 0x001F348E
			public sealed override bool HidesBaseMethodsByName
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170027E5 RID: 10213
			// (get) Token: 0x06008D93 RID: 36243 RVA: 0x001F5291 File Offset: 0x001F3491
			public sealed override bool IsVararg
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170027E6 RID: 10214
			// (get) Token: 0x06008D94 RID: 36244 RVA: 0x001F5294 File Offset: 0x001F3494
			public sealed override FlowAnalysisAnnotations ReturnTypeFlowAnalysisAnnotations
			{
				get
				{
					return FlowAnalysisAnnotations.None;
				}
			}

			// Token: 0x170027E7 RID: 10215
			// (get) Token: 0x06008D95 RID: 36245 RVA: 0x001F5297 File Offset: 0x001F3497
			public sealed override ImmutableHashSet<string> ReturnNotNullIfParameterNotNull
			{
				get
				{
					return ImmutableHashSet<string>.Empty;
				}
			}

			// Token: 0x170027E8 RID: 10216
			// (get) Token: 0x06008D96 RID: 36246 RVA: 0x001F529E File Offset: 0x001F349E
			public sealed override ImmutableArray<TypeWithAnnotations> TypeArgumentsWithAnnotations
			{
				get
				{
					return ImmutableArray<TypeWithAnnotations>.Empty;
				}
			}

			// Token: 0x170027E9 RID: 10217
			// (get) Token: 0x06008D97 RID: 36247 RVA: 0x001F52A5 File Offset: 0x001F34A5
			public sealed override ImmutableArray<TypeParameterSymbol> TypeParameters
			{
				get
				{
					return ImmutableArray<TypeParameterSymbol>.Empty;
				}
			}

			// Token: 0x170027EA RID: 10218
			// (get) Token: 0x06008D98 RID: 36248 RVA: 0x001F52AC File Offset: 0x001F34AC
			internal sealed override bool IsExplicitInterfaceImplementation
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170027EB RID: 10219
			// (get) Token: 0x06008D99 RID: 36249 RVA: 0x001F52AF File Offset: 0x001F34AF
			public sealed override ImmutableArray<MethodSymbol> ExplicitInterfaceImplementations
			{
				get
				{
					return ImmutableArray<MethodSymbol>.Empty;
				}
			}

			// Token: 0x170027EC RID: 10220
			// (get) Token: 0x06008D9A RID: 36250 RVA: 0x001F52B6 File Offset: 0x001F34B6
			internal sealed override bool IsDeclaredReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170027ED RID: 10221
			// (get) Token: 0x06008D9B RID: 36251 RVA: 0x001F52B9 File Offset: 0x001F34B9
			internal sealed override bool IsInitOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170027EE RID: 10222
			// (get) Token: 0x06008D9C RID: 36252 RVA: 0x001F52BC File Offset: 0x001F34BC
			public sealed override ImmutableArray<CustomModifier> RefCustomModifiers
			{
				get
				{
					return ImmutableArray<CustomModifier>.Empty;
				}
			}

			// Token: 0x170027EF RID: 10223
			// (get) Token: 0x06008D9D RID: 36253 RVA: 0x001F52C3 File Offset: 0x001F34C3
			public override Symbol AssociatedSymbol
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170027F0 RID: 10224
			// (get) Token: 0x06008D9E RID: 36254 RVA: 0x001F52C6 File Offset: 0x001F34C6
			public sealed override bool IsAbstract
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170027F1 RID: 10225
			// (get) Token: 0x06008D9F RID: 36255 RVA: 0x001F52C9 File Offset: 0x001F34C9
			public sealed override bool IsSealed
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170027F2 RID: 10226
			// (get) Token: 0x06008DA0 RID: 36256 RVA: 0x001F52CC File Offset: 0x001F34CC
			public sealed override bool IsExtern
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170027F3 RID: 10227
			// (get) Token: 0x06008DA1 RID: 36257 RVA: 0x001F52CF File Offset: 0x001F34CF
			public sealed override string Name
			{
				get
				{
					return this._name;
				}
			}

			// Token: 0x06008DA2 RID: 36258 RVA: 0x001F52D7 File Offset: 0x001F34D7
			internal sealed override bool IsMetadataNewSlot(bool ignoreInterfaceImplementationChanges = false)
			{
				return false;
			}

			// Token: 0x06008DA3 RID: 36259 RVA: 0x001F52DC File Offset: 0x001F34DC
			internal override void AddSynthesizedAttributes(PEModuleBuilder moduleBuilder, ref ArrayBuilder<SynthesizedAttributeData> attributes)
			{
				base.AddSynthesizedAttributes(moduleBuilder, ref attributes);
				Symbol.AddSynthesizedAttribute(ref attributes, this.Manager.Compilation.TrySynthesizeAttribute(WellKnownMember.System_Diagnostics_DebuggerHiddenAttribute__ctor, default(ImmutableArray<TypedConstant>), default(ImmutableArray<KeyValuePair<WellKnownMember, TypedConstant>>), false));
			}

			// Token: 0x170027F4 RID: 10228
			// (get) Token: 0x06008DA4 RID: 36260 RVA: 0x001F531C File Offset: 0x001F351C
			protected AnonymousTypeManager Manager
			{
				get
				{
					AnonymousTypeManager.AnonymousTypeTemplateSymbol anonymousTypeTemplateSymbol = this._containingType as AnonymousTypeManager.AnonymousTypeTemplateSymbol;
					if (anonymousTypeTemplateSymbol == null)
					{
						return ((AnonymousTypeManager.AnonymousTypePublicSymbol)this._containingType).Manager;
					}
					return anonymousTypeTemplateSymbol.Manager;
				}
			}

			// Token: 0x170027F5 RID: 10229
			// (get) Token: 0x06008DA5 RID: 36261 RVA: 0x001F534F File Offset: 0x001F354F
			internal sealed override bool RequiresSecurityObject
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06008DA6 RID: 36262 RVA: 0x001F5352 File Offset: 0x001F3552
			public sealed override DllImportData GetDllImportData()
			{
				return null;
			}

			// Token: 0x170027F6 RID: 10230
			// (get) Token: 0x06008DA7 RID: 36263 RVA: 0x001F5355 File Offset: 0x001F3555
			internal sealed override MarshalPseudoCustomAttributeData ReturnValueMarshallingInformation
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170027F7 RID: 10231
			// (get) Token: 0x06008DA8 RID: 36264 RVA: 0x001F5358 File Offset: 0x001F3558
			internal sealed override bool HasDeclarativeSecurity
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06008DA9 RID: 36265 RVA: 0x001F535B File Offset: 0x001F355B
			internal sealed override IEnumerable<SecurityAttribute> GetSecurityInformation()
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x06008DAA RID: 36266 RVA: 0x001F5362 File Offset: 0x001F3562
			internal sealed override ImmutableArray<string> GetAppliedConditionalSymbols()
			{
				return ImmutableArray<string>.Empty;
			}

			// Token: 0x170027F8 RID: 10232
			// (get) Token: 0x06008DAB RID: 36267 RVA: 0x001F5369 File Offset: 0x001F3569
			internal override bool SynthesizesLoweredBoundBody
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06008DAC RID: 36268 RVA: 0x001F536C File Offset: 0x001F356C
			protected SyntheticBoundNodeFactory CreateBoundNodeFactory(TypeCompilationState compilationState, BindingDiagnosticBag diagnostics)
			{
				return new SyntheticBoundNodeFactory(this, this.GetNonNullSyntaxNode(), compilationState, diagnostics)
				{
					CurrentFunction = this
				};
			}

			// Token: 0x06008DAD RID: 36269 RVA: 0x001F5383 File Offset: 0x001F3583
			internal sealed override int CalculateLocalSyntaxOffset(int localPosition, SyntaxTree localTree)
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x0400263C RID: 9788
			private readonly NamedTypeSymbol _containingType;

			// Token: 0x0400263D RID: 9789
			private readonly string _name;
		}

		// Token: 0x020008C4 RID: 2244
		internal sealed class NameAndIndex
		{
			// Token: 0x06008DAE RID: 36270 RVA: 0x001F538A File Offset: 0x001F358A
			public NameAndIndex(string name, int index)
			{
				this.Name = name;
				this.Index = index;
			}

			// Token: 0x0400263E RID: 9790
			public readonly string Name;

			// Token: 0x0400263F RID: 9791
			public readonly int Index;
		}

		// Token: 0x020008C5 RID: 2245
		internal sealed class AnonymousTypeTemplateSymbol : NamedTypeSymbol
		{
			// Token: 0x06008DAF RID: 36271 RVA: 0x001F53A0 File Offset: 0x001F35A0
			internal AnonymousTypeTemplateSymbol(AnonymousTypeManager manager, AnonymousTypeDescriptor typeDescr)
				: base(null)
			{
				this.Manager = manager;
				this.TypeDescriptorKey = typeDescr.Key;
				this._smallestLocation = typeDescr.Location;
				this._nameAndIndex = null;
				int length = typeDescr.Fields.Length;
				ArrayBuilder<Symbol> instance = ArrayBuilder<Symbol>.GetInstance(length * 3 + 1);
				ArrayBuilder<AnonymousTypeManager.AnonymousTypePropertySymbol> instance2 = ArrayBuilder<AnonymousTypeManager.AnonymousTypePropertySymbol>.GetInstance(length);
				ArrayBuilder<TypeParameterSymbol> instance3 = ArrayBuilder<TypeParameterSymbol>.GetInstance(length);
				for (int i = 0; i < length; i++)
				{
					AnonymousTypeField anonymousTypeField = typeDescr.Fields[i];
					AnonymousTypeManager.AnonymousTypeParameterSymbol anonymousTypeParameterSymbol = new AnonymousTypeManager.AnonymousTypeParameterSymbol(this, i, GeneratedNames.MakeAnonymousTypeParameterName(anonymousTypeField.Name));
					instance3.Add(anonymousTypeParameterSymbol);
					AnonymousTypeManager.AnonymousTypePropertySymbol anonymousTypePropertySymbol = new AnonymousTypeManager.AnonymousTypePropertySymbol(this, anonymousTypeField, TypeWithAnnotations.Create(anonymousTypeParameterSymbol, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), i);
					instance2.Add(anonymousTypePropertySymbol);
					instance.Add(anonymousTypePropertySymbol);
					instance.Add(anonymousTypePropertySymbol.BackingField);
					instance.Add(anonymousTypePropertySymbol.GetMethod);
				}
				this._typeParameters = instance3.ToImmutableAndFree();
				this.Properties = instance2.ToImmutableAndFree();
				instance.Add(new AnonymousTypeManager.AnonymousTypeConstructorSymbol(this, this.Properties));
				this._members = instance.ToImmutableAndFree();
				foreach (Symbol symbol in this._members)
				{
					this._nameToSymbols.Add(symbol.Name, symbol);
				}
				this.SpecialMembers = ImmutableArray.Create<MethodSymbol>(new AnonymousTypeManager.AnonymousTypeEqualsMethodSymbol(this), new AnonymousTypeManager.AnonymousTypeGetHashCodeMethodSymbol(this), new AnonymousTypeManager.AnonymousTypeToStringMethodSymbol(this));
			}

			// Token: 0x06008DB0 RID: 36272 RVA: 0x001F5523 File Offset: 0x001F3723
			protected override NamedTypeSymbol WithTupleDataCore(NamedTypeSymbol.TupleExtraData newData)
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x06008DB1 RID: 36273 RVA: 0x001F552A File Offset: 0x001F372A
			internal AnonymousTypeKey GetAnonymousTypeKey()
			{
				return new AnonymousTypeKey(this.Properties.SelectAsArray<AnonymousTypeManager.AnonymousTypePropertySymbol, AnonymousTypeKeyField>((AnonymousTypeManager.AnonymousTypePropertySymbol p) => new AnonymousTypeKeyField(p.Name, false, false)), false);
			}

			// Token: 0x170027F9 RID: 10233
			// (get) Token: 0x06008DB2 RID: 36274 RVA: 0x001F555C File Offset: 0x001F375C
			internal Location SmallestLocation
			{
				get
				{
					return this._smallestLocation;
				}
			}

			// Token: 0x170027FA RID: 10234
			// (get) Token: 0x06008DB3 RID: 36275 RVA: 0x001F5564 File Offset: 0x001F3764
			// (set) Token: 0x06008DB4 RID: 36276 RVA: 0x001F556C File Offset: 0x001F376C
			internal AnonymousTypeManager.NameAndIndex NameAndIndex
			{
				get
				{
					return this._nameAndIndex;
				}
				set
				{
					Interlocked.CompareExchange<AnonymousTypeManager.NameAndIndex>(ref this._nameAndIndex, value, null);
				}
			}

			// Token: 0x06008DB5 RID: 36277 RVA: 0x001F557C File Offset: 0x001F377C
			internal void AdjustLocation(Location location)
			{
				for (;;)
				{
					Location smallestLocation = this._smallestLocation;
					if (smallestLocation != null && this.Manager.Compilation.CompareSourceLocations(smallestLocation, location) < 0)
					{
						break;
					}
					if (Interlocked.CompareExchange<Location>(ref this._smallestLocation, location, smallestLocation) == smallestLocation)
					{
						return;
					}
				}
			}

			// Token: 0x06008DB6 RID: 36278 RVA: 0x001F55BF File Offset: 0x001F37BF
			public override ImmutableArray<Symbol> GetMembers()
			{
				return this._members;
			}

			// Token: 0x06008DB7 RID: 36279 RVA: 0x001F55C7 File Offset: 0x001F37C7
			internal override IEnumerable<FieldSymbol> GetFieldsToEmit()
			{
				foreach (Symbol symbol in this.GetMembers())
				{
					if (symbol.Kind == SymbolKind.Field)
					{
						yield return (FieldSymbol)symbol;
					}
				}
				ImmutableArray<Symbol>.Enumerator enumerator = default(ImmutableArray<Symbol>.Enumerator);
				yield break;
			}

			// Token: 0x170027FB RID: 10235
			// (get) Token: 0x06008DB8 RID: 36280 RVA: 0x001F55D7 File Offset: 0x001F37D7
			internal override bool HasCodeAnalysisEmbeddedAttribute
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170027FC RID: 10236
			// (get) Token: 0x06008DB9 RID: 36281 RVA: 0x001F55DA File Offset: 0x001F37DA
			internal override ImmutableArray<TypeWithAnnotations> TypeArgumentsWithAnnotationsNoUseSiteDiagnostics
			{
				get
				{
					return base.GetTypeParametersAsTypeArguments();
				}
			}

			// Token: 0x06008DBA RID: 36282 RVA: 0x001F55E4 File Offset: 0x001F37E4
			public override ImmutableArray<Symbol> GetMembers(string name)
			{
				MultiDictionary<string, Symbol>.ValueSet valueSet = this._nameToSymbols[name];
				ArrayBuilder<Symbol> instance = ArrayBuilder<Symbol>.GetInstance(valueSet.Count);
				foreach (Symbol symbol in valueSet)
				{
					instance.Add(symbol);
				}
				return instance.ToImmutableAndFree();
			}

			// Token: 0x06008DBB RID: 36283 RVA: 0x001F5654 File Offset: 0x001F3854
			internal override ImmutableArray<Symbol> GetEarlyAttributeDecodingMembers()
			{
				return this.GetMembersUnordered();
			}

			// Token: 0x06008DBC RID: 36284 RVA: 0x001F565C File Offset: 0x001F385C
			internal override ImmutableArray<Symbol> GetEarlyAttributeDecodingMembers(string name)
			{
				return this.GetMembers(name);
			}

			// Token: 0x170027FD RID: 10237
			// (get) Token: 0x06008DBD RID: 36285 RVA: 0x001F5665 File Offset: 0x001F3865
			public override IEnumerable<string> MemberNames
			{
				get
				{
					return this._nameToSymbols.Keys;
				}
			}

			// Token: 0x170027FE RID: 10238
			// (get) Token: 0x06008DBE RID: 36286 RVA: 0x001F5672 File Offset: 0x001F3872
			public override Symbol ContainingSymbol
			{
				get
				{
					return this.Manager.Compilation.SourceModule.GlobalNamespace;
				}
			}

			// Token: 0x170027FF RID: 10239
			// (get) Token: 0x06008DBF RID: 36287 RVA: 0x001F5689 File Offset: 0x001F3889
			public override string Name
			{
				get
				{
					return this._nameAndIndex.Name;
				}
			}

			// Token: 0x17002800 RID: 10240
			// (get) Token: 0x06008DC0 RID: 36288 RVA: 0x001F5696 File Offset: 0x001F3896
			internal override bool HasSpecialName
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002801 RID: 10241
			// (get) Token: 0x06008DC1 RID: 36289 RVA: 0x001F5699 File Offset: 0x001F3899
			internal override bool MangleName
			{
				get
				{
					return this.Arity > 0;
				}
			}

			// Token: 0x17002802 RID: 10242
			// (get) Token: 0x06008DC2 RID: 36290 RVA: 0x001F56A4 File Offset: 0x001F38A4
			public override int Arity
			{
				get
				{
					return this._typeParameters.Length;
				}
			}

			// Token: 0x17002803 RID: 10243
			// (get) Token: 0x06008DC3 RID: 36291 RVA: 0x001F56BF File Offset: 0x001F38BF
			public override bool IsImplicitlyDeclared
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17002804 RID: 10244
			// (get) Token: 0x06008DC4 RID: 36292 RVA: 0x001F56C2 File Offset: 0x001F38C2
			public override ImmutableArray<TypeParameterSymbol> TypeParameters
			{
				get
				{
					return this._typeParameters;
				}
			}

			// Token: 0x17002805 RID: 10245
			// (get) Token: 0x06008DC5 RID: 36293 RVA: 0x001F56CA File Offset: 0x001F38CA
			public override bool IsAbstract
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002806 RID: 10246
			// (get) Token: 0x06008DC6 RID: 36294 RVA: 0x001F56CD File Offset: 0x001F38CD
			public sealed override bool IsRefLikeType
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002807 RID: 10247
			// (get) Token: 0x06008DC7 RID: 36295 RVA: 0x001F56D0 File Offset: 0x001F38D0
			public sealed override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002808 RID: 10248
			// (get) Token: 0x06008DC8 RID: 36296 RVA: 0x001F56D3 File Offset: 0x001F38D3
			public override bool IsSealed
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17002809 RID: 10249
			// (get) Token: 0x06008DC9 RID: 36297 RVA: 0x001F56D6 File Offset: 0x001F38D6
			public override bool MightContainExtensionMethods
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700280A RID: 10250
			// (get) Token: 0x06008DCA RID: 36298 RVA: 0x001F56D9 File Offset: 0x001F38D9
			public sealed override bool AreLocalsZeroed
			{
				get
				{
					return this.ContainingModule.AreLocalsZeroed;
				}
			}

			// Token: 0x06008DCB RID: 36299 RVA: 0x001F56E6 File Offset: 0x001F38E6
			public override ImmutableArray<NamedTypeSymbol> GetTypeMembers()
			{
				return ImmutableArray<NamedTypeSymbol>.Empty;
			}

			// Token: 0x06008DCC RID: 36300 RVA: 0x001F56ED File Offset: 0x001F38ED
			public override ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name)
			{
				return ImmutableArray<NamedTypeSymbol>.Empty;
			}

			// Token: 0x06008DCD RID: 36301 RVA: 0x001F56F4 File Offset: 0x001F38F4
			public override ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name, int arity)
			{
				return ImmutableArray<NamedTypeSymbol>.Empty;
			}

			// Token: 0x1700280B RID: 10251
			// (get) Token: 0x06008DCE RID: 36302 RVA: 0x001F56FB File Offset: 0x001F38FB
			public override Accessibility DeclaredAccessibility
			{
				get
				{
					return Accessibility.Internal;
				}
			}

			// Token: 0x06008DCF RID: 36303 RVA: 0x001F56FE File Offset: 0x001F38FE
			internal override ImmutableArray<NamedTypeSymbol> InterfacesNoUseSiteDiagnostics(ConsList<TypeSymbol> basesBeingResolved)
			{
				return ImmutableArray<NamedTypeSymbol>.Empty;
			}

			// Token: 0x06008DD0 RID: 36304 RVA: 0x001F5705 File Offset: 0x001F3905
			internal override ImmutableArray<NamedTypeSymbol> GetInterfacesToEmit()
			{
				return ImmutableArray<NamedTypeSymbol>.Empty;
			}

			// Token: 0x1700280C RID: 10252
			// (get) Token: 0x06008DD1 RID: 36305 RVA: 0x001F570C File Offset: 0x001F390C
			internal override NamedTypeSymbol BaseTypeNoUseSiteDiagnostics
			{
				get
				{
					return this.Manager.System_Object;
				}
			}

			// Token: 0x1700280D RID: 10253
			// (get) Token: 0x06008DD2 RID: 36306 RVA: 0x001F5719 File Offset: 0x001F3919
			public override TypeKind TypeKind
			{
				get
				{
					return TypeKind.Class;
				}
			}

			// Token: 0x1700280E RID: 10254
			// (get) Token: 0x06008DD3 RID: 36307 RVA: 0x001F571C File Offset: 0x001F391C
			internal override bool IsInterface
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700280F RID: 10255
			// (get) Token: 0x06008DD4 RID: 36308 RVA: 0x001F571F File Offset: 0x001F391F
			public override ImmutableArray<Location> Locations
			{
				get
				{
					return ImmutableArray<Location>.Empty;
				}
			}

			// Token: 0x17002810 RID: 10256
			// (get) Token: 0x06008DD5 RID: 36309 RVA: 0x001F5726 File Offset: 0x001F3926
			public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
			{
				get
				{
					return ImmutableArray<SyntaxReference>.Empty;
				}
			}

			// Token: 0x17002811 RID: 10257
			// (get) Token: 0x06008DD6 RID: 36310 RVA: 0x001F572D File Offset: 0x001F392D
			public override bool IsStatic
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002812 RID: 10258
			// (get) Token: 0x06008DD7 RID: 36311 RVA: 0x001F5730 File Offset: 0x001F3930
			public override NamedTypeSymbol ConstructedFrom
			{
				get
				{
					return this;
				}
			}

			// Token: 0x06008DD8 RID: 36312 RVA: 0x001F5733 File Offset: 0x001F3933
			internal override NamedTypeSymbol GetDeclaredBaseType(ConsList<TypeSymbol> basesBeingResolved)
			{
				return this.Manager.System_Object;
			}

			// Token: 0x06008DD9 RID: 36313 RVA: 0x001F5740 File Offset: 0x001F3940
			internal override ImmutableArray<NamedTypeSymbol> GetDeclaredInterfaces(ConsList<TypeSymbol> basesBeingResolved)
			{
				return ImmutableArray<NamedTypeSymbol>.Empty;
			}

			// Token: 0x17002813 RID: 10259
			// (get) Token: 0x06008DDA RID: 36314 RVA: 0x001F5747 File Offset: 0x001F3947
			internal override bool ShouldAddWinRTMembers
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002814 RID: 10260
			// (get) Token: 0x06008DDB RID: 36315 RVA: 0x001F574A File Offset: 0x001F394A
			internal override bool IsWindowsRuntimeImport
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002815 RID: 10261
			// (get) Token: 0x06008DDC RID: 36316 RVA: 0x001F574D File Offset: 0x001F394D
			internal override bool IsComImport
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002816 RID: 10262
			// (get) Token: 0x06008DDD RID: 36317 RVA: 0x001F5750 File Offset: 0x001F3950
			internal sealed override ObsoleteAttributeData ObsoleteAttributeData
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17002817 RID: 10263
			// (get) Token: 0x06008DDE RID: 36318 RVA: 0x001F5754 File Offset: 0x001F3954
			internal override TypeLayout Layout
			{
				get
				{
					return default(TypeLayout);
				}
			}

			// Token: 0x17002818 RID: 10264
			// (get) Token: 0x06008DDF RID: 36319 RVA: 0x001F576A File Offset: 0x001F396A
			internal override CharSet MarshallingCharSet
			{
				get
				{
					return base.DefaultMarshallingCharSet;
				}
			}

			// Token: 0x17002819 RID: 10265
			// (get) Token: 0x06008DE0 RID: 36320 RVA: 0x001F5772 File Offset: 0x001F3972
			public override bool IsSerializable
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700281A RID: 10266
			// (get) Token: 0x06008DE1 RID: 36321 RVA: 0x001F5775 File Offset: 0x001F3975
			internal override bool HasDeclarativeSecurity
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06008DE2 RID: 36322 RVA: 0x001F5778 File Offset: 0x001F3978
			internal override IEnumerable<SecurityAttribute> GetSecurityInformation()
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x06008DE3 RID: 36323 RVA: 0x001F577F File Offset: 0x001F397F
			internal override ImmutableArray<string> GetAppliedConditionalSymbols()
			{
				return ImmutableArray<string>.Empty;
			}

			// Token: 0x06008DE4 RID: 36324 RVA: 0x001F5786 File Offset: 0x001F3986
			internal override AttributeUsageInfo GetAttributeUsageInfo()
			{
				return AttributeUsageInfo.Null;
			}

			// Token: 0x06008DE5 RID: 36325 RVA: 0x001F578D File Offset: 0x001F398D
			internal sealed override NamedTypeSymbol AsNativeInteger()
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x1700281B RID: 10267
			// (get) Token: 0x06008DE6 RID: 36326 RVA: 0x001F5794 File Offset: 0x001F3994
			internal sealed override NamedTypeSymbol NativeIntegerUnderlyingType
			{
				get
				{
					return null;
				}
			}

			// Token: 0x1700281C RID: 10268
			// (get) Token: 0x06008DE7 RID: 36327 RVA: 0x001F5797 File Offset: 0x001F3997
			internal override bool IsRecord
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700281D RID: 10269
			// (get) Token: 0x06008DE8 RID: 36328 RVA: 0x001F579A File Offset: 0x001F399A
			internal override bool IsRecordStruct
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06008DE9 RID: 36329 RVA: 0x001F57A0 File Offset: 0x001F39A0
			internal override void AddSynthesizedAttributes(PEModuleBuilder moduleBuilder, ref ArrayBuilder<SynthesizedAttributeData> attributes)
			{
				base.AddSynthesizedAttributes(moduleBuilder, ref attributes);
				Symbol.AddSynthesizedAttribute(ref attributes, this.Manager.Compilation.TrySynthesizeAttribute(WellKnownMember.System_Runtime_CompilerServices_CompilerGeneratedAttribute__ctor, default(ImmutableArray<TypedConstant>), default(ImmutableArray<KeyValuePair<WellKnownMember, TypedConstant>>), false));
				if (this.Manager.Compilation.Options.OptimizationLevel == OptimizationLevel.Debug)
				{
					Symbol.AddSynthesizedAttribute(ref attributes, this.TrySynthesizeDebuggerDisplayAttribute());
				}
			}

			// Token: 0x06008DEA RID: 36330 RVA: 0x001F5804 File Offset: 0x001F3A04
			private SynthesizedAttributeData TrySynthesizeDebuggerDisplayAttribute()
			{
				string text;
				if (this.Properties.Length == 0)
				{
					text = "\\{ }";
				}
				else
				{
					PooledStringBuilder instance = PooledStringBuilder.GetInstance();
					StringBuilder builder = instance.Builder;
					builder.Append("\\{ ");
					int num = Math.Min(this.Properties.Length, 10);
					for (int i = 0; i < num; i++)
					{
						string name = this.Properties[i].Name;
						if (i > 0)
						{
							builder.Append(", ");
						}
						builder.Append(name);
						builder.Append(" = {");
						builder.Append(name);
						builder.Append("}");
					}
					if (this.Properties.Length > num)
					{
						builder.Append(" ...");
					}
					builder.Append(" }");
					text = instance.ToStringAndFree();
				}
				return this.Manager.Compilation.TrySynthesizeAttribute(WellKnownMember.System_Diagnostics_DebuggerDisplayAttribute__ctor, ImmutableArray.Create<TypedConstant>(new TypedConstant(this.Manager.System_String, TypedConstantKind.Primitive, text)), ImmutableArray.Create<KeyValuePair<WellKnownMember, TypedConstant>>(new KeyValuePair<WellKnownMember, TypedConstant>(WellKnownMember.System_Diagnostics_DebuggerDisplayAttribute__Type, new TypedConstant(this.Manager.System_String, TypedConstantKind.Primitive, "<Anonymous Type>"))), false);
			}

			// Token: 0x06008DEB RID: 36331 RVA: 0x001F593C File Offset: 0x001F3B3C
			internal override bool HasPossibleWellKnownCloneMethod()
			{
				return false;
			}

			// Token: 0x04002640 RID: 9792
			private AnonymousTypeManager.NameAndIndex _nameAndIndex;

			// Token: 0x04002641 RID: 9793
			private readonly ImmutableArray<TypeParameterSymbol> _typeParameters;

			// Token: 0x04002642 RID: 9794
			private readonly ImmutableArray<Symbol> _members;

			// Token: 0x04002643 RID: 9795
			internal readonly ImmutableArray<MethodSymbol> SpecialMembers;

			// Token: 0x04002644 RID: 9796
			internal readonly ImmutableArray<AnonymousTypeManager.AnonymousTypePropertySymbol> Properties;

			// Token: 0x04002645 RID: 9797
			private readonly MultiDictionary<string, Symbol> _nameToSymbols = new MultiDictionary<string, Symbol>();

			// Token: 0x04002646 RID: 9798
			internal readonly AnonymousTypeManager Manager;

			// Token: 0x04002647 RID: 9799
			private Location _smallestLocation;

			// Token: 0x04002648 RID: 9800
			internal readonly string TypeDescriptorKey;
		}

		// Token: 0x020008C6 RID: 2246
		internal sealed class AnonymousTypeParameterSymbol : TypeParameterSymbol
		{
			// Token: 0x06008DEC RID: 36332 RVA: 0x001F593F File Offset: 0x001F3B3F
			public AnonymousTypeParameterSymbol(Symbol container, int ordinal, string name)
			{
				this._container = container;
				this._ordinal = ordinal;
				this._name = name;
			}

			// Token: 0x1700281E RID: 10270
			// (get) Token: 0x06008DED RID: 36333 RVA: 0x001F595C File Offset: 0x001F3B5C
			public override TypeParameterKind TypeParameterKind
			{
				get
				{
					return TypeParameterKind.Type;
				}
			}

			// Token: 0x1700281F RID: 10271
			// (get) Token: 0x06008DEE RID: 36334 RVA: 0x001F595F File Offset: 0x001F3B5F
			public override ImmutableArray<Location> Locations
			{
				get
				{
					return ImmutableArray<Location>.Empty;
				}
			}

			// Token: 0x17002820 RID: 10272
			// (get) Token: 0x06008DEF RID: 36335 RVA: 0x001F5966 File Offset: 0x001F3B66
			public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
			{
				get
				{
					return ImmutableArray<SyntaxReference>.Empty;
				}
			}

			// Token: 0x17002821 RID: 10273
			// (get) Token: 0x06008DF0 RID: 36336 RVA: 0x001F596D File Offset: 0x001F3B6D
			public override int Ordinal
			{
				get
				{
					return this._ordinal;
				}
			}

			// Token: 0x17002822 RID: 10274
			// (get) Token: 0x06008DF1 RID: 36337 RVA: 0x001F5975 File Offset: 0x001F3B75
			public override string Name
			{
				get
				{
					return this._name;
				}
			}

			// Token: 0x17002823 RID: 10275
			// (get) Token: 0x06008DF2 RID: 36338 RVA: 0x001F597D File Offset: 0x001F3B7D
			public override bool HasConstructorConstraint
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002824 RID: 10276
			// (get) Token: 0x06008DF3 RID: 36339 RVA: 0x001F5980 File Offset: 0x001F3B80
			public override bool HasReferenceTypeConstraint
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002825 RID: 10277
			// (get) Token: 0x06008DF4 RID: 36340 RVA: 0x001F5983 File Offset: 0x001F3B83
			public override bool IsReferenceTypeFromConstraintTypes
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002826 RID: 10278
			// (get) Token: 0x06008DF5 RID: 36341 RVA: 0x001F5986 File Offset: 0x001F3B86
			internal override bool? ReferenceTypeConstraintIsNullable
			{
				get
				{
					return new bool?(false);
				}
			}

			// Token: 0x17002827 RID: 10279
			// (get) Token: 0x06008DF6 RID: 36342 RVA: 0x001F598E File Offset: 0x001F3B8E
			public override bool HasNotNullConstraint
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002828 RID: 10280
			// (get) Token: 0x06008DF7 RID: 36343 RVA: 0x001F5994 File Offset: 0x001F3B94
			internal override bool? IsNotNullable
			{
				get
				{
					return default(bool?);
				}
			}

			// Token: 0x17002829 RID: 10281
			// (get) Token: 0x06008DF8 RID: 36344 RVA: 0x001F59AA File Offset: 0x001F3BAA
			public override bool HasValueTypeConstraint
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700282A RID: 10282
			// (get) Token: 0x06008DF9 RID: 36345 RVA: 0x001F59AD File Offset: 0x001F3BAD
			public override bool IsValueTypeFromConstraintTypes
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700282B RID: 10283
			// (get) Token: 0x06008DFA RID: 36346 RVA: 0x001F59B0 File Offset: 0x001F3BB0
			public override bool HasUnmanagedTypeConstraint
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700282C RID: 10284
			// (get) Token: 0x06008DFB RID: 36347 RVA: 0x001F59B3 File Offset: 0x001F3BB3
			public override bool IsImplicitlyDeclared
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700282D RID: 10285
			// (get) Token: 0x06008DFC RID: 36348 RVA: 0x001F59B6 File Offset: 0x001F3BB6
			public override VarianceKind Variance
			{
				get
				{
					return VarianceKind.None;
				}
			}

			// Token: 0x06008DFD RID: 36349 RVA: 0x001F59B9 File Offset: 0x001F3BB9
			internal override void EnsureAllConstraintsAreResolved()
			{
			}

			// Token: 0x06008DFE RID: 36350 RVA: 0x001F59BB File Offset: 0x001F3BBB
			internal override ImmutableArray<TypeWithAnnotations> GetConstraintTypes(ConsList<TypeParameterSymbol> inProgress)
			{
				return ImmutableArray<TypeWithAnnotations>.Empty;
			}

			// Token: 0x1700282E RID: 10286
			// (get) Token: 0x06008DFF RID: 36351 RVA: 0x001F59C2 File Offset: 0x001F3BC2
			public override Symbol ContainingSymbol
			{
				get
				{
					return this._container;
				}
			}

			// Token: 0x06008E00 RID: 36352 RVA: 0x001F59CA File Offset: 0x001F3BCA
			internal override ImmutableArray<NamedTypeSymbol> GetInterfaces(ConsList<TypeParameterSymbol> inProgress)
			{
				return ImmutableArray<NamedTypeSymbol>.Empty;
			}

			// Token: 0x06008E01 RID: 36353 RVA: 0x001F59D1 File Offset: 0x001F3BD1
			internal override NamedTypeSymbol GetEffectiveBaseClass(ConsList<TypeParameterSymbol> inProgress)
			{
				return null;
			}

			// Token: 0x06008E02 RID: 36354 RVA: 0x001F59D4 File Offset: 0x001F3BD4
			internal override TypeSymbol GetDeducedBaseType(ConsList<TypeParameterSymbol> inProgress)
			{
				return null;
			}

			// Token: 0x04002649 RID: 9801
			private readonly Symbol _container;

			// Token: 0x0400264A RID: 9802
			private readonly int _ordinal;

			// Token: 0x0400264B RID: 9803
			private readonly string _name;
		}
	}
}
