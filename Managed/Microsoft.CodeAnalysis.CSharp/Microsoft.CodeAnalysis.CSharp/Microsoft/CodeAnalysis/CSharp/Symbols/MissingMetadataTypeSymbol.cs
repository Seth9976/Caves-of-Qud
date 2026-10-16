using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Threading;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004F5 RID: 1269
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class MissingMetadataTypeSymbol : ErrorTypeSymbol
	{
		// Token: 0x06006B93 RID: 27539 RVA: 0x0018F369 File Offset: 0x0018D569
		private MissingMetadataTypeSymbol(string name, int arity, bool mangleName, [global::System.Runtime.CompilerServices.Nullable(2)] NamedTypeSymbol.TupleExtraData tupleData = null)
			: base(tupleData)
		{
			this.name = name;
			this.arity = arity;
			this.mangleName = mangleName && arity > 0;
		}

		// Token: 0x17001BA3 RID: 7075
		// (get) Token: 0x06006B94 RID: 27540 RVA: 0x0018F391 File Offset: 0x0018D591
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17001BA4 RID: 7076
		// (get) Token: 0x06006B95 RID: 27541 RVA: 0x0018F399 File Offset: 0x0018D599
		internal override bool MangleName
		{
			get
			{
				return this.mangleName;
			}
		}

		// Token: 0x17001BA5 RID: 7077
		// (get) Token: 0x06006B96 RID: 27542 RVA: 0x0018F3A1 File Offset: 0x0018D5A1
		public override int Arity
		{
			get
			{
				return this.arity;
			}
		}

		// Token: 0x17001BA6 RID: 7078
		// (get) Token: 0x06006B97 RID: 27543 RVA: 0x0018F3AC File Offset: 0x0018D5AC
		internal override DiagnosticInfo ErrorInfo
		{
			get
			{
				AssemblySymbol containingAssembly = this.ContainingAssembly;
				if (containingAssembly.IsMissing)
				{
					return new CSDiagnosticInfo(ErrorCode.ERR_NoTypeDef, new object[] { this, containingAssembly.Identity });
				}
				ModuleSymbol containingModule = this.ContainingModule;
				if (containingModule.IsMissing)
				{
					return new CSDiagnosticInfo(ErrorCode.ERR_NoTypeDefFromModule, new object[] { this, containingModule.Name });
				}
				if (containingAssembly.Dangerous_IsFromSomeCompilation)
				{
					return new CSDiagnosticInfo(ErrorCode.ERR_MissingTypeInSource, new object[] { this });
				}
				return new CSDiagnosticInfo(ErrorCode.ERR_MissingTypeInAssembly, new object[] { this, containingAssembly.Name });
			}
		}

		// Token: 0x04001AB4 RID: 6836
		protected readonly string name;

		// Token: 0x04001AB5 RID: 6837
		protected readonly int arity;

		// Token: 0x04001AB6 RID: 6838
		protected readonly bool mangleName;

		// Token: 0x020008FC RID: 2300
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal sealed class TopLevel : MissingMetadataTypeSymbol
		{
			// Token: 0x06008F54 RID: 36692 RVA: 0x001F8BA8 File Offset: 0x001F6DA8
			public TopLevel(ModuleSymbol module, string @namespace, string name, int arity, bool mangleName)
				: this(module, @namespace, name, arity, mangleName, false, null, null, -1, null)
			{
			}

			// Token: 0x06008F55 RID: 36693 RVA: 0x001F8BC7 File Offset: 0x001F6DC7
			public TopLevel(ModuleSymbol module, ref MetadataTypeName fullName, [global::System.Runtime.CompilerServices.Nullable(2)] DiagnosticInfo errorInfo = null)
				: this(module, ref fullName, -1, errorInfo)
			{
			}

			// Token: 0x06008F56 RID: 36694 RVA: 0x001F8BD3 File Offset: 0x001F6DD3
			public TopLevel(ModuleSymbol module, ref MetadataTypeName fullName, SpecialType specialType, [global::System.Runtime.CompilerServices.Nullable(2)] DiagnosticInfo errorInfo = null)
				: this(module, ref fullName, (int)specialType, errorInfo)
			{
			}

			// Token: 0x06008F57 RID: 36695 RVA: 0x001F8BE0 File Offset: 0x001F6DE0
			public TopLevel(ModuleSymbol module, ref MetadataTypeName fullName, WellKnownType wellKnownType, [global::System.Runtime.CompilerServices.Nullable(2)] DiagnosticInfo errorInfo = null)
				: this(module, ref fullName, (int)wellKnownType, errorInfo)
			{
			}

			// Token: 0x06008F58 RID: 36696 RVA: 0x001F8BED File Offset: 0x001F6DED
			private TopLevel(ModuleSymbol module, ref MetadataTypeName fullName, int typeId, [global::System.Runtime.CompilerServices.Nullable(2)] DiagnosticInfo errorInfo)
				: this(module, ref fullName, fullName.ForcedArity == -1 || fullName.ForcedArity == fullName.InferredArity, errorInfo, typeId)
			{
			}

			// Token: 0x06008F59 RID: 36697 RVA: 0x001F8C14 File Offset: 0x001F6E14
			private TopLevel(ModuleSymbol module, ref MetadataTypeName fullName, bool mangleName, [global::System.Runtime.CompilerServices.Nullable(2)] DiagnosticInfo errorInfo, int typeId)
				: this(module, fullName.NamespaceName, mangleName ? fullName.UnmangledTypeName : fullName.TypeName, mangleName ? fullName.InferredArity : fullName.ForcedArity, mangleName, false, errorInfo, null, typeId, null)
			{
			}

			// Token: 0x06008F5A RID: 36698 RVA: 0x001F8C58 File Offset: 0x001F6E58
			private TopLevel(ModuleSymbol module, string @namespace, string name, int arity, bool mangleName, bool isNativeInt, [global::System.Runtime.CompilerServices.Nullable(2)] DiagnosticInfo errorInfo, [global::System.Runtime.CompilerServices.Nullable(2)] NamespaceSymbol containingNamespace, int typeId, [global::System.Runtime.CompilerServices.Nullable(2)] NamedTypeSymbol.TupleExtraData tupleData)
				: base(name, arity, mangleName, tupleData)
			{
				this._namespaceName = @namespace;
				this._containingModule = module;
				this._isNativeInt = isNativeInt;
				this._lazyErrorInfo = errorInfo;
				this._lazyContainingNamespace = containingNamespace;
				this._lazyTypeId = typeId;
			}

			// Token: 0x06008F5B RID: 36699 RVA: 0x001F8C98 File Offset: 0x001F6E98
			protected override NamedTypeSymbol WithTupleDataCore(NamedTypeSymbol.TupleExtraData newData)
			{
				return new MissingMetadataTypeSymbol.TopLevel(this._containingModule, this._namespaceName, this.name, this.arity, this.mangleName, this._isNativeInt, this._lazyErrorInfo, this._lazyContainingNamespace, this._lazyTypeId, newData);
			}

			// Token: 0x1700287F RID: 10367
			// (get) Token: 0x06008F5C RID: 36700 RVA: 0x001F8CE1 File Offset: 0x001F6EE1
			public string NamespaceName
			{
				get
				{
					return this._namespaceName;
				}
			}

			// Token: 0x17002880 RID: 10368
			// (get) Token: 0x06008F5D RID: 36701 RVA: 0x001F8CE9 File Offset: 0x001F6EE9
			internal override ModuleSymbol ContainingModule
			{
				get
				{
					return this._containingModule;
				}
			}

			// Token: 0x17002881 RID: 10369
			// (get) Token: 0x06008F5E RID: 36702 RVA: 0x001F8CF1 File Offset: 0x001F6EF1
			public override AssemblySymbol ContainingAssembly
			{
				get
				{
					return this._containingModule.ContainingAssembly;
				}
			}

			// Token: 0x17002882 RID: 10370
			// (get) Token: 0x06008F5F RID: 36703 RVA: 0x001F8D00 File Offset: 0x001F6F00
			public override Symbol ContainingSymbol
			{
				get
				{
					if (this._lazyContainingNamespace == null)
					{
						NamespaceSymbol namespaceSymbol = this._containingModule.GlobalNamespace;
						if (this._namespaceName.Length > 0)
						{
							ImmutableArray<string> immutableArray = MetadataHelpers.SplitQualifiedName(this._namespaceName);
							for (int i = 0; i < immutableArray.Length; i++)
							{
								NamespaceSymbol namespaceSymbol2 = null;
								foreach (Symbol symbol in namespaceSymbol.GetMembers(immutableArray[i]))
								{
									NamespaceOrTypeSymbol namespaceOrTypeSymbol = (NamespaceOrTypeSymbol)symbol;
									if (namespaceOrTypeSymbol.Kind == SymbolKind.Namespace)
									{
										namespaceSymbol2 = (NamespaceSymbol)namespaceOrTypeSymbol;
										break;
									}
								}
								if (namespaceSymbol2 == null)
								{
									IL_00A9:
									while (i < immutableArray.Length)
									{
										namespaceSymbol = new MissingNamespaceSymbol(namespaceSymbol, immutableArray[i]);
										i++;
									}
									goto IL_00B3;
								}
								namespaceSymbol = namespaceSymbol2;
							}
							goto IL_00A9;
						}
						IL_00B3:
						Interlocked.CompareExchange<NamespaceSymbol>(ref this._lazyContainingNamespace, namespaceSymbol, null);
					}
					return this._lazyContainingNamespace;
				}
			}

			// Token: 0x17002883 RID: 10371
			// (get) Token: 0x06008F60 RID: 36704 RVA: 0x001F8DD4 File Offset: 0x001F6FD4
			private int TypeId
			{
				get
				{
					if (this._lazyTypeId == -1)
					{
						SpecialType specialType = SpecialType.None;
						AssemblySymbol containingAssembly = this._containingModule.ContainingAssembly;
						if ((this.Arity == 0 || this.MangleName) && containingAssembly != null && containingAssembly == containingAssembly.CorLibrary && this._containingModule.Ordinal == 0)
						{
							specialType = SpecialTypes.GetTypeFromMetadataName(MetadataHelpers.BuildQualifiedName(this._namespaceName, this.MetadataName));
						}
						Interlocked.CompareExchange(ref this._lazyTypeId, (int)specialType, -1);
					}
					return this._lazyTypeId;
				}
			}

			// Token: 0x17002884 RID: 10372
			// (get) Token: 0x06008F61 RID: 36705 RVA: 0x001F8E4C File Offset: 0x001F704C
			public override SpecialType SpecialType
			{
				get
				{
					if (this.TypeId < 46)
					{
						return (SpecialType)this._lazyTypeId;
					}
					return SpecialType.None;
				}
			}

			// Token: 0x17002885 RID: 10373
			// (get) Token: 0x06008F62 RID: 36706 RVA: 0x001F8E64 File Offset: 0x001F7064
			internal override DiagnosticInfo ErrorInfo
			{
				get
				{
					if (this._lazyErrorInfo == null)
					{
						DiagnosticInfo diagnosticInfo = ((this.TypeId != 0) ? new CSDiagnosticInfo(ErrorCode.ERR_PredefinedTypeNotFound, new object[] { MetadataHelpers.BuildQualifiedName(this._namespaceName, this.MetadataName) }) : base.ErrorInfo);
						Interlocked.CompareExchange<DiagnosticInfo>(ref this._lazyErrorInfo, diagnosticInfo, null);
					}
					return this._lazyErrorInfo;
				}
			}

			// Token: 0x06008F63 RID: 36707 RVA: 0x001F8EC2 File Offset: 0x001F70C2
			public override int GetHashCode()
			{
				if (this.SpecialType == SpecialType.System_Object)
				{
					return 1;
				}
				return Hash.Combine<string>(this.MetadataName, Hash.Combine<ModuleSymbol>(this._containingModule, Hash.Combine<string>(this._namespaceName, this.arity)));
			}

			// Token: 0x06008F64 RID: 36708 RVA: 0x001F8EF6 File Offset: 0x001F70F6
			internal sealed override NamedTypeSymbol AsNativeInteger()
			{
				return this.AsNativeInteger(true);
			}

			// Token: 0x06008F65 RID: 36709 RVA: 0x001F8F00 File Offset: 0x001F7100
			private MissingMetadataTypeSymbol.TopLevel AsNativeInteger(bool asNativeInt)
			{
				if (asNativeInt == this._isNativeInt)
				{
					return this;
				}
				return new MissingMetadataTypeSymbol.TopLevel(this._containingModule, this._namespaceName, this.name, this.arity, this.mangleName, asNativeInt, this._lazyErrorInfo, this._lazyContainingNamespace, this._lazyTypeId, base.TupleData);
			}

			// Token: 0x17002886 RID: 10374
			// (get) Token: 0x06008F66 RID: 36710 RVA: 0x001F8F54 File Offset: 0x001F7154
			internal sealed override bool IsNativeIntegerType
			{
				get
				{
					return this._isNativeInt;
				}
			}

			// Token: 0x17002887 RID: 10375
			// (get) Token: 0x06008F67 RID: 36711 RVA: 0x001F8F5C File Offset: 0x001F715C
			[global::System.Runtime.CompilerServices.Nullable(2)]
			internal sealed override NamedTypeSymbol NativeIntegerUnderlyingType
			{
				[global::System.Runtime.CompilerServices.NullableContext(2)]
				get
				{
					if (!this._isNativeInt)
					{
						return null;
					}
					return this.AsNativeInteger(false);
				}
			}

			// Token: 0x06008F68 RID: 36712 RVA: 0x001F8F70 File Offset: 0x001F7170
			internal override bool Equals(TypeSymbol t2, TypeCompareKind comparison)
			{
				if (this == t2)
				{
					return true;
				}
				if ((comparison & TypeCompareKind.IgnoreDynamic) != TypeCompareKind.ConsiderEverything && t2 != null && t2.TypeKind == TypeKind.Dynamic && this.SpecialType == SpecialType.System_Object)
				{
					return true;
				}
				MissingMetadataTypeSymbol.TopLevel topLevel = t2 as MissingMetadataTypeSymbol.TopLevel;
				return topLevel != null && ((comparison & TypeCompareKind.IgnoreNativeIntegers) != TypeCompareKind.ConsiderEverything || this._isNativeInt == topLevel._isNativeInt) && (string.Equals(this.MetadataName, topLevel.MetadataName, 4) && this.arity == topLevel.arity && string.Equals(this._namespaceName, topLevel.NamespaceName, 4)) && this._containingModule.Equals(topLevel._containingModule);
			}

			// Token: 0x0400272C RID: 10028
			private readonly string _namespaceName;

			// Token: 0x0400272D RID: 10029
			private readonly ModuleSymbol _containingModule;

			// Token: 0x0400272E RID: 10030
			private readonly bool _isNativeInt;

			// Token: 0x0400272F RID: 10031
			[global::System.Runtime.CompilerServices.Nullable(2)]
			private DiagnosticInfo _lazyErrorInfo;

			// Token: 0x04002730 RID: 10032
			[global::System.Runtime.CompilerServices.Nullable(2)]
			private NamespaceSymbol _lazyContainingNamespace;

			// Token: 0x04002731 RID: 10033
			private int _lazyTypeId;
		}

		// Token: 0x020008FD RID: 2301
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal sealed class Nested : MissingMetadataTypeSymbol
		{
			// Token: 0x06008F69 RID: 36713 RVA: 0x001F900A File Offset: 0x001F720A
			public Nested(NamedTypeSymbol containingType, string name, int arity, bool mangleName)
				: base(name, arity, mangleName, null)
			{
				this._containingType = containingType;
			}

			// Token: 0x06008F6A RID: 36714 RVA: 0x001F901E File Offset: 0x001F721E
			public Nested(NamedTypeSymbol containingType, ref MetadataTypeName emittedName)
				: this(containingType, ref emittedName, emittedName.ForcedArity == -1 || emittedName.ForcedArity == emittedName.InferredArity)
			{
			}

			// Token: 0x06008F6B RID: 36715 RVA: 0x001F9042 File Offset: 0x001F7242
			private Nested(NamedTypeSymbol containingType, ref MetadataTypeName emittedName, bool mangleName)
				: this(containingType, mangleName ? emittedName.UnmangledTypeName : emittedName.TypeName, mangleName ? emittedName.InferredArity : emittedName.ForcedArity, mangleName)
			{
			}

			// Token: 0x17002888 RID: 10376
			// (get) Token: 0x06008F6C RID: 36716 RVA: 0x001F906E File Offset: 0x001F726E
			public override Symbol ContainingSymbol
			{
				get
				{
					return this._containingType;
				}
			}

			// Token: 0x17002889 RID: 10377
			// (get) Token: 0x06008F6D RID: 36717 RVA: 0x001F9076 File Offset: 0x001F7276
			public override SpecialType SpecialType
			{
				get
				{
					return SpecialType.None;
				}
			}

			// Token: 0x06008F6E RID: 36718 RVA: 0x001F9079 File Offset: 0x001F7279
			protected override NamedTypeSymbol WithTupleDataCore(NamedTypeSymbol.TupleExtraData newData)
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x06008F6F RID: 36719 RVA: 0x001F9080 File Offset: 0x001F7280
			public override int GetHashCode()
			{
				return Hash.Combine<NamedTypeSymbol>(this._containingType, Hash.Combine<string>(this.MetadataName, this.arity));
			}

			// Token: 0x06008F70 RID: 36720 RVA: 0x001F90A0 File Offset: 0x001F72A0
			internal override bool Equals(TypeSymbol t2, TypeCompareKind comparison)
			{
				if (this == t2)
				{
					return true;
				}
				MissingMetadataTypeSymbol.Nested nested = t2 as MissingMetadataTypeSymbol.Nested;
				return nested != null && string.Equals(this.MetadataName, nested.MetadataName, 4) && this.arity == nested.arity && this._containingType.Equals(nested._containingType, comparison);
			}

			// Token: 0x04002732 RID: 10034
			private readonly NamedTypeSymbol _containingType;
		}
	}
}
