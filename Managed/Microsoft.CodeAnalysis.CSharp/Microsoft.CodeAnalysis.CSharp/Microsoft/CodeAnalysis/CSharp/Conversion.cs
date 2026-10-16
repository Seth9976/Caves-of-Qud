using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000048 RID: 72
	public struct Conversion : IEquatable<Conversion>, IConvertibleConversion
	{
		// Token: 0x06000711 RID: 1809 RVA: 0x0003D824 File Offset: 0x0003BA24
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private Conversion(ConversionKind kind, Conversion.UncommonData uncommonData)
		{
			this._kind = kind;
			this._uncommonData = uncommonData;
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0003D834 File Offset: 0x0003BA34
		private Conversion(ConversionKind kind)
		{
			this = new Conversion(kind, null);
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0003D840 File Offset: 0x0003BA40
		internal Conversion(UserDefinedConversionResult conversionResult, bool isImplicit)
		{
			this._kind = ((conversionResult.Kind == UserDefinedConversionResultKind.NoApplicableOperators) ? ConversionKind.NoConversion : (isImplicit ? ConversionKind.ImplicitUserDefined : ConversionKind.ExplicitUserDefined));
			this._uncommonData = new Conversion.UncommonData(false, false, conversionResult, null, default(ImmutableArray<Conversion>));
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x0003D880 File Offset: 0x0003BA80
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal Conversion(ConversionKind kind, MethodSymbol conversionMethod, bool isExtensionMethod)
		{
			this._kind = kind;
			this._uncommonData = new Conversion.UncommonData(isExtensionMethod, false, default(UserDefinedConversionResult), conversionMethod, default(ImmutableArray<Conversion>));
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0003D8B4 File Offset: 0x0003BAB4
		internal Conversion(ConversionKind kind, ImmutableArray<Conversion> nestedConversions)
		{
			this._kind = kind;
			this._uncommonData = new Conversion.UncommonData(false, false, default(UserDefinedConversionResult), null, nestedConversions);
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x0003D8E0 File Offset: 0x0003BAE0
		internal Conversion(ConversionKind kind, DeconstructMethodInfo deconstructMethodInfo, ImmutableArray<Conversion> nestedConversions)
		{
			this._kind = kind;
			this._uncommonData = new Conversion.DeconstructionUncommonData(deconstructMethodInfo, nestedConversions);
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x0003D8F6 File Offset: 0x0003BAF6
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal Conversion SetConversionMethod(MethodSymbol conversionMethod)
		{
			return new Conversion(this.Kind, conversionMethod, this.IsExtensionMethod);
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0003D90C File Offset: 0x0003BB0C
		internal Conversion SetArrayIndexConversionForDynamic()
		{
			return new Conversion(this._kind, new Conversion.UncommonData(false, true, default(UserDefinedConversionResult), null, default(ImmutableArray<Conversion>)));
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0003D940 File Offset: 0x0003BB40
		[Conditional("DEBUG")]
		private static void AssertTrivialConversion(ConversionKind kind)
		{
			switch (kind)
			{
			default:
				return;
			}
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0003D9F5 File Offset: 0x0003BBF5
		internal static Conversion GetTrivialConversion(ConversionKind kind)
		{
			return new Conversion(kind);
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x0003D9FD File Offset: 0x0003BBFD
		internal static Conversion UnsetConversion
		{
			get
			{
				return new Conversion(ConversionKind.UnsetConversionKind);
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x0003DA05 File Offset: 0x0003BC05
		internal static Conversion NoConversion
		{
			get
			{
				return new Conversion(ConversionKind.NoConversion);
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x0003DA0D File Offset: 0x0003BC0D
		internal static Conversion Identity
		{
			get
			{
				return new Conversion(ConversionKind.Identity);
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x0003DA15 File Offset: 0x0003BC15
		internal static Conversion ImplicitConstant
		{
			get
			{
				return new Conversion(ConversionKind.ImplicitConstant);
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x0003DA1E File Offset: 0x0003BC1E
		internal static Conversion ImplicitNumeric
		{
			get
			{
				return new Conversion(ConversionKind.ImplicitNumeric);
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x0003DA26 File Offset: 0x0003BC26
		internal static Conversion ImplicitReference
		{
			get
			{
				return new Conversion(ConversionKind.ImplicitReference);
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000721 RID: 1825 RVA: 0x0003DA2F File Offset: 0x0003BC2F
		internal static Conversion ImplicitEnumeration
		{
			get
			{
				return new Conversion(ConversionKind.ImplicitEnumeration);
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x0003DA37 File Offset: 0x0003BC37
		internal static Conversion ImplicitThrow
		{
			get
			{
				return new Conversion(ConversionKind.ImplicitThrow);
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000723 RID: 1827 RVA: 0x0003DA3F File Offset: 0x0003BC3F
		internal static Conversion ObjectCreation
		{
			get
			{
				return new Conversion(ConversionKind.ObjectCreation);
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000724 RID: 1828 RVA: 0x0003DA48 File Offset: 0x0003BC48
		internal static Conversion AnonymousFunction
		{
			get
			{
				return new Conversion(ConversionKind.AnonymousFunction);
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x0003DA51 File Offset: 0x0003BC51
		internal static Conversion Boxing
		{
			get
			{
				return new Conversion(ConversionKind.Boxing);
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000726 RID: 1830 RVA: 0x0003DA5A File Offset: 0x0003BC5A
		internal static Conversion NullLiteral
		{
			get
			{
				return new Conversion(ConversionKind.NullLiteral);
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000727 RID: 1831 RVA: 0x0003DA63 File Offset: 0x0003BC63
		internal static Conversion DefaultLiteral
		{
			get
			{
				return new Conversion(ConversionKind.DefaultLiteral);
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000728 RID: 1832 RVA: 0x0003DA6C File Offset: 0x0003BC6C
		internal static Conversion NullToPointer
		{
			get
			{
				return new Conversion(ConversionKind.ImplicitNullToPointer);
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x0003DA75 File Offset: 0x0003BC75
		internal static Conversion PointerToVoid
		{
			get
			{
				return new Conversion(ConversionKind.ImplicitPointerToVoid);
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600072A RID: 1834 RVA: 0x0003DA7E File Offset: 0x0003BC7E
		internal static Conversion PointerToPointer
		{
			get
			{
				return new Conversion(ConversionKind.ExplicitPointerToPointer);
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600072B RID: 1835 RVA: 0x0003DA87 File Offset: 0x0003BC87
		internal static Conversion PointerToInteger
		{
			get
			{
				return new Conversion(ConversionKind.ExplicitPointerToInteger);
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600072C RID: 1836 RVA: 0x0003DA90 File Offset: 0x0003BC90
		internal static Conversion IntegerToPointer
		{
			get
			{
				return new Conversion(ConversionKind.ExplicitIntegerToPointer);
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x0003DA99 File Offset: 0x0003BC99
		internal static Conversion Unboxing
		{
			get
			{
				return new Conversion(ConversionKind.Unboxing);
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600072E RID: 1838 RVA: 0x0003DAA2 File Offset: 0x0003BCA2
		internal static Conversion ExplicitReference
		{
			get
			{
				return new Conversion(ConversionKind.ExplicitReference);
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x0003DAAB File Offset: 0x0003BCAB
		internal static Conversion IntPtr
		{
			get
			{
				return new Conversion(ConversionKind.IntPtr);
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000730 RID: 1840 RVA: 0x0003DAB4 File Offset: 0x0003BCB4
		internal static Conversion ExplicitEnumeration
		{
			get
			{
				return new Conversion(ConversionKind.ExplicitEnumeration);
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000731 RID: 1841 RVA: 0x0003DABD File Offset: 0x0003BCBD
		internal static Conversion ExplicitNumeric
		{
			get
			{
				return new Conversion(ConversionKind.ExplicitNumeric);
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000732 RID: 1842 RVA: 0x0003DAC6 File Offset: 0x0003BCC6
		internal static Conversion ImplicitDynamic
		{
			get
			{
				return new Conversion(ConversionKind.ImplicitDynamic);
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x0003DACF File Offset: 0x0003BCCF
		internal static Conversion ExplicitDynamic
		{
			get
			{
				return new Conversion(ConversionKind.ExplicitDynamic);
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x0003DAD8 File Offset: 0x0003BCD8
		internal static Conversion InterpolatedString
		{
			get
			{
				return new Conversion(ConversionKind.InterpolatedString);
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x0003DAE1 File Offset: 0x0003BCE1
		internal static Conversion Deconstruction
		{
			get
			{
				return new Conversion(ConversionKind.Deconstruction);
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x0003DAEA File Offset: 0x0003BCEA
		internal static Conversion PinnedObjectToPointer
		{
			get
			{
				return new Conversion(ConversionKind.PinnedObjectToPointer);
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x0003DAF3 File Offset: 0x0003BCF3
		internal static Conversion ImplicitPointer
		{
			get
			{
				return new Conversion(ConversionKind.ImplicitPointer);
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000738 RID: 1848 RVA: 0x0003DAFC File Offset: 0x0003BCFC
		internal static ImmutableArray<Conversion> IdentityUnderlying
		{
			get
			{
				return Conversion.ConversionSingletons.IdentityUnderlying;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x0003DB03 File Offset: 0x0003BD03
		internal static ImmutableArray<Conversion> ImplicitConstantUnderlying
		{
			get
			{
				return Conversion.ConversionSingletons.ImplicitConstantUnderlying;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x0003DB0A File Offset: 0x0003BD0A
		internal static ImmutableArray<Conversion> ImplicitNumericUnderlying
		{
			get
			{
				return Conversion.ConversionSingletons.ImplicitNumericUnderlying;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x0003DB11 File Offset: 0x0003BD11
		internal static ImmutableArray<Conversion> ExplicitNumericUnderlying
		{
			get
			{
				return Conversion.ConversionSingletons.ExplicitNumericUnderlying;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600073C RID: 1852 RVA: 0x0003DB18 File Offset: 0x0003BD18
		internal static ImmutableArray<Conversion> ExplicitEnumerationUnderlying
		{
			get
			{
				return Conversion.ConversionSingletons.ExplicitEnumerationUnderlying;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600073D RID: 1853 RVA: 0x0003DB1F File Offset: 0x0003BD1F
		internal static ImmutableArray<Conversion> PointerToIntegerUnderlying
		{
			get
			{
				return Conversion.ConversionSingletons.PointerToIntegerUnderlying;
			}
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x0003DB26 File Offset: 0x0003BD26
		internal static Conversion MakeStackAllocToPointerType(Conversion underlyingConversion)
		{
			return new Conversion(ConversionKind.StackAllocToPointerType, ImmutableArray.Create<Conversion>(underlyingConversion));
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0003DB35 File Offset: 0x0003BD35
		internal static Conversion MakeStackAllocToSpanType(Conversion underlyingConversion)
		{
			return new Conversion(ConversionKind.StackAllocToSpanType, ImmutableArray.Create<Conversion>(underlyingConversion));
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0003DB44 File Offset: 0x0003BD44
		internal static Conversion MakeNullableConversion(ConversionKind kind, Conversion nestedConversion)
		{
			ConversionKind kind2 = nestedConversion.Kind;
			ImmutableArray<Conversion> immutableArray;
			if (kind2 <= ConversionKind.ImplicitConstant)
			{
				if (kind2 == ConversionKind.Identity)
				{
					immutableArray = Conversion.IdentityUnderlying;
					goto IL_0064;
				}
				if (kind2 == ConversionKind.ImplicitNumeric)
				{
					immutableArray = Conversion.ImplicitNumericUnderlying;
					goto IL_0064;
				}
				if (kind2 == ConversionKind.ImplicitConstant)
				{
					immutableArray = Conversion.ImplicitConstantUnderlying;
					goto IL_0064;
				}
			}
			else
			{
				if (kind2 == ConversionKind.ExplicitNumeric)
				{
					immutableArray = Conversion.ExplicitNumericUnderlying;
					goto IL_0064;
				}
				if (kind2 == ConversionKind.ExplicitEnumeration)
				{
					immutableArray = Conversion.ExplicitEnumerationUnderlying;
					goto IL_0064;
				}
				if (kind2 == ConversionKind.ExplicitPointerToInteger)
				{
					immutableArray = Conversion.PointerToIntegerUnderlying;
					goto IL_0064;
				}
			}
			immutableArray = ImmutableArray.Create<Conversion>(nestedConversion);
			IL_0064:
			return new Conversion(kind, immutableArray);
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0003DBBC File Offset: 0x0003BDBC
		internal static Conversion MakeSwitchExpression(ImmutableArray<Conversion> innerConversions)
		{
			return new Conversion(ConversionKind.SwitchExpression, innerConversions);
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0003DBC6 File Offset: 0x0003BDC6
		internal static Conversion MakeConditionalExpression(ImmutableArray<Conversion> innerConversions)
		{
			return new Conversion(ConversionKind.ConditionalExpression, innerConversions);
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x0003DBD0 File Offset: 0x0003BDD0
		internal ConversionKind Kind
		{
			get
			{
				return this._kind;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x0003DBD8 File Offset: 0x0003BDD8
		internal bool IsExtensionMethod
		{
			get
			{
				Conversion.UncommonData uncommonData = this._uncommonData;
				return uncommonData != null && uncommonData.IsExtensionMethod;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x0003DBEB File Offset: 0x0003BDEB
		internal bool IsArrayIndex
		{
			get
			{
				Conversion.UncommonData uncommonData = this._uncommonData;
				return uncommonData != null && uncommonData.IsArrayIndex;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000746 RID: 1862 RVA: 0x0003DC00 File Offset: 0x0003BE00
		internal ImmutableArray<Conversion> UnderlyingConversions
		{
			get
			{
				Conversion.UncommonData uncommonData = this._uncommonData;
				if (uncommonData == null)
				{
					return default(ImmutableArray<Conversion>);
				}
				return uncommonData._nestedConversionsOpt;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x0003DC28 File Offset: 0x0003BE28
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal MethodSymbol Method
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				Conversion.UncommonData uncommonData = this._uncommonData;
				if (uncommonData != null)
				{
					if (uncommonData._conversionMethod != null)
					{
						return uncommonData._conversionMethod;
					}
					UserDefinedConversionResult conversionResult = uncommonData._conversionResult;
					if (conversionResult.Kind == UserDefinedConversionResultKind.Valid)
					{
						return conversionResult.Results[conversionResult.Best].Operator;
					}
					Conversion.DeconstructionUncommonData deconstructionUncommonData = uncommonData as Conversion.DeconstructionUncommonData;
					if (deconstructionUncommonData != null)
					{
						BoundCall boundCall = deconstructionUncommonData.DeconstructMethodInfo.Invocation as BoundCall;
						if (boundCall != null)
						{
							return boundCall.Method;
						}
					}
				}
				return null;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000748 RID: 1864 RVA: 0x0003DCA0 File Offset: 0x0003BEA0
		internal DeconstructMethodInfo DeconstructionInfo
		{
			get
			{
				Conversion.DeconstructionUncommonData deconstructionUncommonData = (Conversion.DeconstructionUncommonData)this._uncommonData;
				if (deconstructionUncommonData != null)
				{
					return deconstructionUncommonData.DeconstructMethodInfo;
				}
				return default(DeconstructMethodInfo);
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000749 RID: 1865 RVA: 0x0003DCCC File Offset: 0x0003BECC
		internal bool IsValid
		{
			get
			{
				if (!this.Exists)
				{
					return false;
				}
				Conversion.UncommonData uncommonData = this._uncommonData;
				ImmutableArray<Conversion>? immutableArray = ((uncommonData != null) ? new ImmutableArray<Conversion>?(uncommonData._nestedConversionsOpt) : default(ImmutableArray<Conversion>?));
				if (immutableArray != default(ImmutableArray<Conversion>?))
				{
					foreach (Conversion conversion in immutableArray.Value)
					{
						if (!conversion.IsValid)
						{
							return false;
						}
					}
					return true;
				}
				if (this.IsUserDefined && this.Method == null)
				{
					Conversion.UncommonData uncommonData2 = this._uncommonData;
					return uncommonData2 != null && uncommonData2._conversionResult.Kind == UserDefinedConversionResultKind.Valid;
				}
				return true;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600074A RID: 1866 RVA: 0x0003DD70 File Offset: 0x0003BF70
		public bool Exists
		{
			get
			{
				return this.Kind != ConversionKind.NoConversion;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600074B RID: 1867 RVA: 0x0003DD7E File Offset: 0x0003BF7E
		public bool IsImplicit
		{
			get
			{
				return this.Kind.IsImplicitConversion();
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600074C RID: 1868 RVA: 0x0003DD8B File Offset: 0x0003BF8B
		public bool IsExplicit
		{
			get
			{
				return this.Exists && !this.IsImplicit;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600074D RID: 1869 RVA: 0x0003DDA0 File Offset: 0x0003BFA0
		public bool IsIdentity
		{
			get
			{
				return this.Kind == ConversionKind.Identity;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600074E RID: 1870 RVA: 0x0003DDAB File Offset: 0x0003BFAB
		public bool IsStackAlloc
		{
			get
			{
				return this.Kind == ConversionKind.StackAllocToPointerType || this.Kind == ConversionKind.StackAllocToSpanType;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600074F RID: 1871 RVA: 0x0003DDC3 File Offset: 0x0003BFC3
		public bool IsNumeric
		{
			get
			{
				return this.Kind == ConversionKind.ImplicitNumeric || this.Kind == ConversionKind.ExplicitNumeric;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000750 RID: 1872 RVA: 0x0003DDDA File Offset: 0x0003BFDA
		public bool IsEnumeration
		{
			get
			{
				return this.Kind == ConversionKind.ImplicitEnumeration || this.Kind == ConversionKind.ExplicitEnumeration;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000751 RID: 1873 RVA: 0x0003DDF1 File Offset: 0x0003BFF1
		public bool IsThrow
		{
			get
			{
				return this.Kind == ConversionKind.ImplicitThrow;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000752 RID: 1874 RVA: 0x0003DDFC File Offset: 0x0003BFFC
		internal bool IsObjectCreation
		{
			get
			{
				return this.Kind == ConversionKind.ObjectCreation;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x0003DE08 File Offset: 0x0003C008
		public bool IsSwitchExpression
		{
			get
			{
				return this.Kind == ConversionKind.SwitchExpression;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x0003DE14 File Offset: 0x0003C014
		public bool IsConditionalExpression
		{
			get
			{
				return this.Kind == ConversionKind.ConditionalExpression;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000755 RID: 1877 RVA: 0x0003DE20 File Offset: 0x0003C020
		public bool IsInterpolatedString
		{
			get
			{
				return this.Kind == ConversionKind.InterpolatedString;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000756 RID: 1878 RVA: 0x0003DE2C File Offset: 0x0003C02C
		public bool IsNullable
		{
			get
			{
				return this.Kind == ConversionKind.ImplicitNullable || this.Kind == ConversionKind.ExplicitNullable;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000757 RID: 1879 RVA: 0x0003DE44 File Offset: 0x0003C044
		public bool IsTupleLiteralConversion
		{
			get
			{
				return this.Kind == ConversionKind.ImplicitTupleLiteral || this.Kind == ConversionKind.ExplicitTupleLiteral;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000758 RID: 1880 RVA: 0x0003DE5A File Offset: 0x0003C05A
		public bool IsTupleConversion
		{
			get
			{
				return this.Kind == ConversionKind.ImplicitTuple || this.Kind == ConversionKind.ExplicitTuple;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000759 RID: 1881 RVA: 0x0003DE71 File Offset: 0x0003C071
		public bool IsReference
		{
			get
			{
				return this.Kind == ConversionKind.ImplicitReference || this.Kind == ConversionKind.ExplicitReference;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600075A RID: 1882 RVA: 0x0003DE89 File Offset: 0x0003C089
		public bool IsUserDefined
		{
			get
			{
				return this.Kind.IsUserDefinedConversion();
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600075B RID: 1883 RVA: 0x0003DE96 File Offset: 0x0003C096
		public bool IsBoxing
		{
			get
			{
				return this.Kind == ConversionKind.Boxing;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600075C RID: 1884 RVA: 0x0003DEA2 File Offset: 0x0003C0A2
		public bool IsUnboxing
		{
			get
			{
				return this.Kind == ConversionKind.Unboxing;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x0003DEAE File Offset: 0x0003C0AE
		public bool IsNullLiteral
		{
			get
			{
				return this.Kind == ConversionKind.NullLiteral;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x0003DEBA File Offset: 0x0003C0BA
		public bool IsDefaultLiteral
		{
			get
			{
				return this.Kind == ConversionKind.DefaultLiteral;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x0003DEC6 File Offset: 0x0003C0C6
		public bool IsDynamic
		{
			get
			{
				return this.Kind.IsDynamic();
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000760 RID: 1888 RVA: 0x0003DED3 File Offset: 0x0003C0D3
		public bool IsConstantExpression
		{
			get
			{
				return this.Kind == ConversionKind.ImplicitConstant;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x0003DEDF File Offset: 0x0003C0DF
		public bool IsAnonymousFunction
		{
			get
			{
				return this.Kind == ConversionKind.AnonymousFunction;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000762 RID: 1890 RVA: 0x0003DEEB File Offset: 0x0003C0EB
		public bool IsMethodGroup
		{
			get
			{
				return this.Kind == ConversionKind.MethodGroup;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x0003DEF7 File Offset: 0x0003C0F7
		public bool IsPointer
		{
			get
			{
				return this.Kind.IsPointerConversion();
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000764 RID: 1892 RVA: 0x0003DF04 File Offset: 0x0003C104
		public bool IsIntPtr
		{
			get
			{
				return this.Kind == ConversionKind.IntPtr;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000765 RID: 1893 RVA: 0x0003DF10 File Offset: 0x0003C110
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public IMethodSymbol MethodSymbol
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.Method.GetPublicSymbol();
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000766 RID: 1894 RVA: 0x0003DF20 File Offset: 0x0003C120
		internal LookupResultKind ResultKind
		{
			get
			{
				Conversion.UncommonData uncommonData = this._uncommonData;
				UserDefinedConversionResult userDefinedConversionResult = ((uncommonData != null) ? uncommonData._conversionResult : default(UserDefinedConversionResult));
				switch (userDefinedConversionResult.Kind)
				{
				case UserDefinedConversionResultKind.NoApplicableOperators:
					if (!userDefinedConversionResult.Results.IsDefaultOrEmpty)
					{
						return LookupResultKind.OverloadResolutionFailure;
					}
					if (this.Kind != ConversionKind.NoConversion)
					{
						return LookupResultKind.Viable;
					}
					return LookupResultKind.Empty;
				case UserDefinedConversionResultKind.NoBestSourceType:
				case UserDefinedConversionResultKind.NoBestTargetType:
				case UserDefinedConversionResultKind.Ambiguous:
					return LookupResultKind.OverloadResolutionFailure;
				case UserDefinedConversionResultKind.Valid:
					return LookupResultKind.Viable;
				default:
					throw ExceptionUtilities.UnexpectedValue(userDefinedConversionResult.Kind);
				}
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000767 RID: 1895 RVA: 0x0003DFA4 File Offset: 0x0003C1A4
		internal Conversion UserDefinedFromConversion
		{
			get
			{
				UserDefinedConversionAnalysis bestUserDefinedConversionAnalysis = this.BestUserDefinedConversionAnalysis;
				if (bestUserDefinedConversionAnalysis != null)
				{
					return bestUserDefinedConversionAnalysis.SourceConversion;
				}
				return Conversion.NoConversion;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000768 RID: 1896 RVA: 0x0003DFC8 File Offset: 0x0003C1C8
		internal Conversion UserDefinedToConversion
		{
			get
			{
				UserDefinedConversionAnalysis bestUserDefinedConversionAnalysis = this.BestUserDefinedConversionAnalysis;
				if (bestUserDefinedConversionAnalysis != null)
				{
					return bestUserDefinedConversionAnalysis.TargetConversion;
				}
				return Conversion.NoConversion;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000769 RID: 1897 RVA: 0x0003DFEC File Offset: 0x0003C1EC
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal ImmutableArray<MethodSymbol> OriginalUserDefinedConversions
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				if (this._uncommonData == null)
				{
					return ImmutableArray<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol>.Empty;
				}
				UserDefinedConversionResult conversionResult = this._uncommonData._conversionResult;
				if (conversionResult.Kind == UserDefinedConversionResultKind.NoApplicableOperators)
				{
					return ImmutableArray<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol>.Empty;
				}
				ArrayBuilder<MethodSymbol> instance = ArrayBuilder<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol>.GetInstance();
				foreach (UserDefinedConversionAnalysis userDefinedConversionAnalysis in conversionResult.Results)
				{
					instance.Add(userDefinedConversionAnalysis.Operator);
				}
				return instance.ToImmutableAndFree();
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600076A RID: 1898 RVA: 0x0003E060 File Offset: 0x0003C260
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal UserDefinedConversionAnalysis BestUserDefinedConversionAnalysis
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				if (this._uncommonData == null)
				{
					return null;
				}
				UserDefinedConversionResult conversionResult = this._uncommonData._conversionResult;
				if (conversionResult.Kind == UserDefinedConversionResultKind.Valid)
				{
					return conversionResult.Results[conversionResult.Best];
				}
				return null;
			}
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0003E0A4 File Offset: 0x0003C2A4
		public CommonConversion ToCommonConversion()
		{
			IMethodSymbol methodSymbol = (this.IsUserDefined ? this.MethodSymbol : null);
			return new CommonConversion(this.Exists, this.IsIdentity, this.IsNumeric, this.IsReference, this.IsImplicit, this.IsNullable, methodSymbol);
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0003E0F0 File Offset: 0x0003C2F0
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public override string ToString()
		{
			return this.Kind.ToString();
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x0003E111 File Offset: 0x0003C311
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is Conversion && this.Equals((Conversion)obj);
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0003E129 File Offset: 0x0003C329
		public bool Equals(Conversion other)
		{
			return this.Kind == other.Kind && this.Method == other.Method;
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0003E14E File Offset: 0x0003C34E
		public override int GetHashCode()
		{
			return Hash.Combine<MethodSymbol>(this.Method, (int)this.Kind);
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0003E161 File Offset: 0x0003C361
		public static bool operator ==(Conversion left, Conversion right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0003E16B File Offset: 0x0003C36B
		public static bool operator !=(Conversion left, Conversion right)
		{
			return !(left == right);
		}

		// Token: 0x040000E0 RID: 224
		private readonly ConversionKind _kind;

		// Token: 0x040000E1 RID: 225
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private readonly Conversion.UncommonData _uncommonData;

		// Token: 0x0200064E RID: 1614
		private class UncommonData
		{
			// Token: 0x0600827F RID: 33407 RVA: 0x001DA633 File Offset: 0x001D8833
			public UncommonData(bool isExtensionMethod, bool isArrayIndex, UserDefinedConversionResult conversionResult, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol conversionMethod, ImmutableArray<Conversion> nestedConversions)
			{
				this._conversionMethod = conversionMethod;
				this._conversionResult = conversionResult;
				this._nestedConversionsOpt = nestedConversions;
				this._flags = (isExtensionMethod ? 1 : 0);
				if (isArrayIndex)
				{
					this._flags |= 2;
				}
			}

			// Token: 0x17002680 RID: 9856
			// (get) Token: 0x06008280 RID: 33408 RVA: 0x001DA671 File Offset: 0x001D8871
			internal bool IsExtensionMethod
			{
				get
				{
					return (this._flags & 1) > 0;
				}
			}

			// Token: 0x17002681 RID: 9857
			// (get) Token: 0x06008281 RID: 33409 RVA: 0x001DA67E File Offset: 0x001D887E
			internal bool IsArrayIndex
			{
				get
				{
					return (this._flags & 2) > 0;
				}
			}

			// Token: 0x04001FA6 RID: 8102
			[global::System.Runtime.CompilerServices.Nullable(2)]
			internal readonly MethodSymbol _conversionMethod;

			// Token: 0x04001FA7 RID: 8103
			internal readonly ImmutableArray<Conversion> _nestedConversionsOpt;

			// Token: 0x04001FA8 RID: 8104
			internal readonly UserDefinedConversionResult _conversionResult;

			// Token: 0x04001FA9 RID: 8105
			private const byte IsExtensionMethodMask = 1;

			// Token: 0x04001FAA RID: 8106
			private const byte IsArrayIndexMask = 2;

			// Token: 0x04001FAB RID: 8107
			private readonly byte _flags;
		}

		// Token: 0x0200064F RID: 1615
		private class DeconstructionUncommonData : Conversion.UncommonData
		{
			// Token: 0x06008282 RID: 33410 RVA: 0x001DA68C File Offset: 0x001D888C
			internal DeconstructionUncommonData(DeconstructMethodInfo deconstructMethodInfoOpt, ImmutableArray<Conversion> nestedConversions)
				: base(false, false, default(UserDefinedConversionResult), null, nestedConversions)
			{
				this.DeconstructMethodInfo = deconstructMethodInfoOpt;
			}

			// Token: 0x04001FAC RID: 8108
			internal readonly DeconstructMethodInfo DeconstructMethodInfo;
		}

		// Token: 0x02000650 RID: 1616
		private static class ConversionSingletons
		{
			// Token: 0x04001FAD RID: 8109
			internal static ImmutableArray<Conversion> IdentityUnderlying = ImmutableArray.Create<Conversion>(Conversion.Identity);

			// Token: 0x04001FAE RID: 8110
			internal static ImmutableArray<Conversion> ImplicitConstantUnderlying = ImmutableArray.Create<Conversion>(Conversion.ImplicitConstant);

			// Token: 0x04001FAF RID: 8111
			internal static ImmutableArray<Conversion> ImplicitNumericUnderlying = ImmutableArray.Create<Conversion>(Conversion.ImplicitNumeric);

			// Token: 0x04001FB0 RID: 8112
			internal static ImmutableArray<Conversion> ExplicitNumericUnderlying = ImmutableArray.Create<Conversion>(Conversion.ExplicitNumeric);

			// Token: 0x04001FB1 RID: 8113
			internal static ImmutableArray<Conversion> ExplicitEnumerationUnderlying = ImmutableArray.Create<Conversion>(Conversion.ExplicitEnumeration);

			// Token: 0x04001FB2 RID: 8114
			internal static ImmutableArray<Conversion> PointerToIntegerUnderlying = ImmutableArray.Create<Conversion>(Conversion.PointerToInteger);
		}
	}
}
