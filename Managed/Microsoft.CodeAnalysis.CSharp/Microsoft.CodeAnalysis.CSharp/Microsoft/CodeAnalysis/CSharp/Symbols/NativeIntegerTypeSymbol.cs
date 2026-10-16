using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004FD RID: 1277
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class NativeIntegerTypeSymbol : WrappedNamedTypeSymbol, IReference
	{
		// Token: 0x06006C0C RID: 27660 RVA: 0x0018FCBC File Offset: 0x0018DEBC
		internal NativeIntegerTypeSymbol(NamedTypeSymbol underlyingType)
			: base(underlyingType, null)
		{
		}

		// Token: 0x17001BE1 RID: 7137
		// (get) Token: 0x06006C0D RID: 27661 RVA: 0x0018FCC6 File Offset: 0x0018DEC6
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<TypeParameterSymbol> TypeParameters
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<TypeParameterSymbol>.Empty;
			}
		}

		// Token: 0x17001BE2 RID: 7138
		// (get) Token: 0x06006C0E RID: 27662 RVA: 0x0018FCCD File Offset: 0x0018DECD
		public override NamedTypeSymbol ConstructedFrom
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17001BE3 RID: 7139
		// (get) Token: 0x06006C0F RID: 27663 RVA: 0x0018FCD0 File Offset: 0x0018DED0
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._underlyingType.ContainingSymbol;
			}
		}

		// Token: 0x17001BE4 RID: 7140
		// (get) Token: 0x06006C10 RID: 27664 RVA: 0x0018FCDD File Offset: 0x0018DEDD
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal override ImmutableArray<TypeWithAnnotations> TypeArgumentsWithAnnotationsNoUseSiteDiagnostics
		{
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			get
			{
				return ImmutableArray<TypeWithAnnotations>.Empty;
			}
		}

		// Token: 0x17001BE5 RID: 7141
		// (get) Token: 0x06006C11 RID: 27665 RVA: 0x0018FCE4 File Offset: 0x0018DEE4
		internal override bool IsComImport
		{
			get
			{
				return this._underlyingType.IsComImport;
			}
		}

		// Token: 0x17001BE6 RID: 7142
		// (get) Token: 0x06006C12 RID: 27666 RVA: 0x0018FCF1 File Offset: 0x0018DEF1
		internal override NamedTypeSymbol BaseTypeNoUseSiteDiagnostics
		{
			get
			{
				return this._underlyingType.BaseTypeNoUseSiteDiagnostics;
			}
		}

		// Token: 0x17001BE7 RID: 7143
		// (get) Token: 0x06006C13 RID: 27667 RVA: 0x0018FCFE File Offset: 0x0018DEFE
		public override SpecialType SpecialType
		{
			get
			{
				return this._underlyingType.SpecialType;
			}
		}

		// Token: 0x17001BE8 RID: 7144
		// (get) Token: 0x06006C14 RID: 27668 RVA: 0x0018FD0B File Offset: 0x0018DF0B
		public override IEnumerable<string> MemberNames
		{
			get
			{
				return from m in this.GetMembers()
					select m.Name;
			}
		}

		// Token: 0x06006C15 RID: 27669 RVA: 0x0018FD37 File Offset: 0x0018DF37
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<Symbol> GetMembers()
		{
			if (this._lazyMembers.IsDefault)
			{
				ImmutableInterlocked.InterlockedInitialize<Symbol>(ref this._lazyMembers, this.<GetMembers>g__makeMembers|20_0(this._underlyingType.GetMembers()));
			}
			return this._lazyMembers;
		}

		// Token: 0x06006C16 RID: 27670 RVA: 0x0018FD69 File Offset: 0x0018DF69
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<Symbol> GetMembers(string name)
		{
			return this.GetMembers().WhereAsArray((Symbol member, string name) => member.Name == name, name);
		}

		// Token: 0x06006C17 RID: 27671 RVA: 0x0018FD96 File Offset: 0x0018DF96
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<NamedTypeSymbol> GetTypeMembers()
		{
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x06006C18 RID: 27672 RVA: 0x0018FD9D File Offset: 0x0018DF9D
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name)
		{
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x06006C19 RID: 27673 RVA: 0x0018FDA4 File Offset: 0x0018DFA4
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name, int arity)
		{
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x06006C1A RID: 27674 RVA: 0x0018FDAB File Offset: 0x0018DFAB
		internal override NamedTypeSymbol GetDeclaredBaseType(ConsList<TypeSymbol> basesBeingResolved)
		{
			return this._underlyingType.GetDeclaredBaseType(basesBeingResolved);
		}

		// Token: 0x06006C1B RID: 27675 RVA: 0x0018FDB9 File Offset: 0x0018DFB9
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal override ImmutableArray<NamedTypeSymbol> GetDeclaredInterfaces(ConsList<TypeSymbol> basesBeingResolved)
		{
			return this.GetInterfaces(basesBeingResolved);
		}

		// Token: 0x06006C1C RID: 27676 RVA: 0x0018FDC4 File Offset: 0x0018DFC4
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal override ImmutableArray<Symbol> GetEarlyAttributeDecodingMembers()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06006C1D RID: 27677 RVA: 0x0018FDD8 File Offset: 0x0018DFD8
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal override ImmutableArray<Symbol> GetEarlyAttributeDecodingMembers(string name)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06006C1E RID: 27678 RVA: 0x0018FDEA File Offset: 0x0018DFEA
		internal override IEnumerable<FieldSymbol> GetFieldsToEmit()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06006C1F RID: 27679 RVA: 0x0018FDF4 File Offset: 0x0018DFF4
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal override ImmutableArray<NamedTypeSymbol> GetInterfacesToEmit()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06006C20 RID: 27680 RVA: 0x0018FE06 File Offset: 0x0018E006
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal override ImmutableArray<NamedTypeSymbol> InterfacesNoUseSiteDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ConsList<TypeSymbol> basesBeingResolved = null)
		{
			return this.GetInterfaces(basesBeingResolved);
		}

		// Token: 0x06006C21 RID: 27681 RVA: 0x0018FE0F File Offset: 0x0018E00F
		protected override NamedTypeSymbol WithTupleDataCore(NamedTypeSymbol.TupleExtraData newData)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06006C22 RID: 27682 RVA: 0x0018FE16 File Offset: 0x0018E016
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal override UseSiteInfo<AssemblySymbol> GetUseSiteInfo()
		{
			return this._underlyingType.GetUseSiteInfo();
		}

		// Token: 0x17001BE9 RID: 7145
		// (get) Token: 0x06006C23 RID: 27683 RVA: 0x0018FE23 File Offset: 0x0018E023
		public override bool AreLocalsZeroed
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001BEA RID: 7146
		// (get) Token: 0x06006C24 RID: 27684 RVA: 0x0018FE2A File Offset: 0x0018E02A
		internal override bool IsNativeIntegerType
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06006C25 RID: 27685 RVA: 0x0018FE2D File Offset: 0x0018E02D
		internal sealed override NamedTypeSymbol AsNativeInteger()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x17001BEB RID: 7147
		// (get) Token: 0x06006C26 RID: 27686 RVA: 0x0018FE34 File Offset: 0x0018E034
		internal sealed override NamedTypeSymbol NativeIntegerUnderlyingType
		{
			get
			{
				return this._underlyingType;
			}
		}

		// Token: 0x17001BEC RID: 7148
		// (get) Token: 0x06006C27 RID: 27687 RVA: 0x0018FE3C File Offset: 0x0018E03C
		internal sealed override bool IsRecord
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001BED RID: 7149
		// (get) Token: 0x06006C28 RID: 27688 RVA: 0x0018FE3F File Offset: 0x0018E03F
		internal sealed override bool IsRecordStruct
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006C29 RID: 27689 RVA: 0x0018FE42 File Offset: 0x0018E042
		internal sealed override bool HasPossibleWellKnownCloneMethod()
		{
			return false;
		}

		// Token: 0x06006C2A RID: 27690 RVA: 0x0018FE45 File Offset: 0x0018E045
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override bool Equals(TypeSymbol other, TypeCompareKind comparison)
		{
			return other != null && (this == other || (this._underlyingType.Equals(other, comparison) && ((comparison & TypeCompareKind.IgnoreNativeIntegers) != TypeCompareKind.ConsiderEverything || other.IsNativeIntegerType)));
		}

		// Token: 0x06006C2B RID: 27691 RVA: 0x0018FE71 File Offset: 0x0018E071
		public override int GetHashCode()
		{
			return this._underlyingType.GetHashCode();
		}

		// Token: 0x06006C2C RID: 27692 RVA: 0x0018FE7E File Offset: 0x0018E07E
		void IReference.Dispatch(MetadataVisitor visitor)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06006C2D RID: 27693 RVA: 0x0018FE88 File Offset: 0x0018E088
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<NamedTypeSymbol> GetInterfaces([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ConsList<TypeSymbol> basesBeingResolved)
		{
			if (this._lazyInterfaces.IsDefault)
			{
				ImmutableArray<NamedTypeSymbol> immutableArray = this._underlyingType.InterfacesNoUseSiteDiagnostics(basesBeingResolved).SelectAsArray((NamedTypeSymbol type, NativeIntegerTypeSymbol.NativeIntegerTypeMap map) => map.SubstituteNamedType(type), this.GetTypeMap());
				ImmutableInterlocked.InterlockedInitialize<NamedTypeSymbol>(ref this._lazyInterfaces, immutableArray);
			}
			return this._lazyInterfaces;
		}

		// Token: 0x06006C2E RID: 27694 RVA: 0x0018FEEC File Offset: 0x0018E0EC
		private NativeIntegerTypeSymbol.NativeIntegerTypeMap GetTypeMap()
		{
			if (this._lazyTypeMap == null)
			{
				Interlocked.CompareExchange<NativeIntegerTypeSymbol.NativeIntegerTypeMap>(ref this._lazyTypeMap, new NativeIntegerTypeSymbol.NativeIntegerTypeMap(this), null);
			}
			return this._lazyTypeMap;
		}

		// Token: 0x06006C2F RID: 27695 RVA: 0x0018FF0F File Offset: 0x0018E10F
		internal TypeWithAnnotations SubstituteUnderlyingType(TypeWithAnnotations type)
		{
			return type.SubstituteType(this.GetTypeMap());
		}

		// Token: 0x06006C30 RID: 27696 RVA: 0x0018FF1E File Offset: 0x0018E11E
		internal NamedTypeSymbol SubstituteUnderlyingType(NamedTypeSymbol type)
		{
			return this.GetTypeMap().SubstituteNamedType(type);
		}

		// Token: 0x06006C31 RID: 27697 RVA: 0x0018FF2C File Offset: 0x0018E12C
		internal static bool EqualsHelper<[global::System.Runtime.CompilerServices.Nullable(0)] TSymbol>(TSymbol symbol, [global::System.Runtime.CompilerServices.Nullable(2)] Symbol other, TypeCompareKind comparison, Func<TSymbol, Symbol> getUnderlyingSymbol) where TSymbol : Symbol
		{
			return other != null && (symbol == other || (getUnderlyingSymbol.Invoke(symbol).Equals(other, comparison) && ((comparison & TypeCompareKind.IgnoreNativeIntegers) != TypeCompareKind.ConsiderEverything || other is TSymbol)));
		}

		// Token: 0x06006C32 RID: 27698 RVA: 0x0018FF61 File Offset: 0x0018E161
		[Conditional("DEBUG")]
		internal static void VerifyEquality(Symbol symbolA, Symbol symbolB)
		{
		}

		// Token: 0x06006C33 RID: 27699 RVA: 0x0018FF64 File Offset: 0x0018E164
		[CompilerGenerated]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<Symbol> <GetMembers>g__makeMembers|20_0([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<Symbol> underlyingMembers)
		{
			ArrayBuilder<Symbol> instance = ArrayBuilder<Symbol>.GetInstance();
			instance.Add(new SynthesizedInstanceConstructor(this));
			foreach (Symbol symbol in underlyingMembers)
			{
				if (symbol.DeclaredAccessibility == Accessibility.Public)
				{
					MethodSymbol methodSymbol = symbol as MethodSymbol;
					if (methodSymbol == null)
					{
						PropertySymbol propertySymbol = symbol as PropertySymbol;
						if (propertySymbol != null)
						{
							if (propertySymbol.ParameterCount == 0 && propertySymbol.Name != "Size")
							{
								NativeIntegerPropertySymbol nativeIntegerPropertySymbol = new NativeIntegerPropertySymbol(this, propertySymbol, delegate(NativeIntegerTypeSymbol container, NativeIntegerPropertySymbol property, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol underlyingAccessor)
								{
									if (underlyingAccessor != null)
									{
										return new NativeIntegerMethodSymbol(container, underlyingAccessor, property);
									}
									return null;
								});
								instance.Add(nativeIntegerPropertySymbol);
								instance.AddIfNotNull(nativeIntegerPropertySymbol.GetMethod);
								instance.AddIfNotNull(nativeIntegerPropertySymbol.SetMethod);
							}
						}
					}
					else if (!methodSymbol.IsGenericMethod && !methodSymbol.IsAccessor() && methodSymbol.MethodKind == MethodKind.Ordinary)
					{
						string name = methodSymbol.Name;
						uint num = global::<PrivateImplementationDetails>.ComputeStringHash(name);
						if (num <= 851977407U)
						{
							if (num != 24911021U)
							{
								if (num != 172651214U)
								{
									if (num == 851977407U)
									{
										if (name == "ToInt64")
										{
											continue;
										}
									}
								}
								else if (name == "ToUInt64")
								{
									continue;
								}
							}
							else if (name == "ToPointer")
							{
								continue;
							}
						}
						else if (num <= 2388032397U)
						{
							if (num != 1016266113U)
							{
								if (num == 2388032397U)
								{
									if (name == "ToUInt32")
									{
										continue;
									}
								}
							}
							else if (name == "Subtract")
							{
								continue;
							}
						}
						else if (num != 2646845972U)
						{
							if (num == 3201285352U)
							{
								if (name == "ToInt32")
								{
									continue;
								}
							}
						}
						else if (name == "Add")
						{
							continue;
						}
						instance.Add(new NativeIntegerMethodSymbol(this, methodSymbol, null));
					}
				}
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x04001ABF RID: 6847
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<NamedTypeSymbol> _lazyInterfaces;

		// Token: 0x04001AC0 RID: 6848
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<Symbol> _lazyMembers;

		// Token: 0x04001AC1 RID: 6849
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private NativeIntegerTypeSymbol.NativeIntegerTypeMap _lazyTypeMap;

		// Token: 0x020008FF RID: 2303
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private sealed class NativeIntegerTypeMap : AbstractTypeMap
		{
			// Token: 0x06008F74 RID: 36724 RVA: 0x001F9112 File Offset: 0x001F7312
			internal NativeIntegerTypeMap(NativeIntegerTypeSymbol type)
			{
				this._type = type;
				this._specialType = this._type.UnderlyingNamedType.SpecialType;
			}

			// Token: 0x06008F75 RID: 36725 RVA: 0x001F9137 File Offset: 0x001F7337
			internal override NamedTypeSymbol SubstituteTypeDeclaration(NamedTypeSymbol previous)
			{
				if (previous.SpecialType != this._specialType)
				{
					return base.SubstituteTypeDeclaration(previous);
				}
				return this._type;
			}

			// Token: 0x06008F76 RID: 36726 RVA: 0x001F9155 File Offset: 0x001F7355
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			internal override ImmutableArray<CustomModifier> SubstituteCustomModifiers([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<CustomModifier> customModifiers)
			{
				return customModifiers;
			}

			// Token: 0x04002735 RID: 10037
			private readonly NativeIntegerTypeSymbol _type;

			// Token: 0x04002736 RID: 10038
			private readonly SpecialType _specialType;
		}
	}
}
