using System;
using System.Runtime.InteropServices;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.Symbols;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000230 RID: 560
	internal sealed class MarshalPseudoCustomAttributeData : IMarshallingInformation
	{
		// Token: 0x060015DD RID: 5597 RVA: 0x000450BB File Offset: 0x000432BB
		internal void SetMarshalAsCustom(object typeSymbolOrName, string cookie)
		{
			this._marshalType = 44;
			this._marshalTypeNameOrSymbol = typeSymbolOrName;
			this._marshalCookie = cookie;
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x000450D4 File Offset: 0x000432D4
		internal void SetMarshalAsComInterface(UnmanagedType unmanagedType, int? parameterIndex)
		{
			this._marshalType = unmanagedType;
			this._marshalParameterIndex = parameterIndex ?? (-1);
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x00045104 File Offset: 0x00043304
		internal void SetMarshalAsArray(UnmanagedType? elementType, int? elementCount, short? parameterIndex)
		{
			this._marshalType = 42;
			this._marshalArrayElementType = elementType ?? 80;
			this._marshalArrayElementCount = elementCount ?? (-1);
			this._marshalParameterIndex = (int)(parameterIndex ?? -1);
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x0004516C File Offset: 0x0004336C
		internal void SetMarshalAsFixedArray(UnmanagedType? elementType, int? elementCount)
		{
			this._marshalType = 30;
			this._marshalArrayElementType = elementType ?? (-1);
			this._marshalArrayElementCount = elementCount ?? (-1);
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x000451B8 File Offset: 0x000433B8
		internal void SetMarshalAsSafeArray(VarEnum? elementType, ITypeSymbolInternal elementTypeSymbol)
		{
			this._marshalType = 29;
			this._marshalArrayElementType = (int)(elementType ?? ((VarEnum)(-1)));
			this._marshalTypeNameOrSymbol = elementTypeSymbol;
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x000451EF File Offset: 0x000433EF
		internal void SetMarshalAsFixedString(int elementCount)
		{
			this._marshalType = 23;
			this._marshalArrayElementCount = elementCount;
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x00045200 File Offset: 0x00043400
		internal void SetMarshalAsSimpleType(UnmanagedType type)
		{
			this._marshalType = type;
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x060015E4 RID: 5604 RVA: 0x00045209 File Offset: 0x00043409
		public UnmanagedType UnmanagedType
		{
			get
			{
				return this._marshalType;
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x060015E5 RID: 5605 RVA: 0x00045211 File Offset: 0x00043411
		int IMarshallingInformation.IidParameterIndex
		{
			get
			{
				return this._marshalParameterIndex;
			}
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x0004521C File Offset: 0x0004341C
		object IMarshallingInformation.GetCustomMarshaller(EmitContext context)
		{
			ITypeSymbolInternal typeSymbolInternal = this._marshalTypeNameOrSymbol as ITypeSymbolInternal;
			if (typeSymbolInternal != null)
			{
				return context.Module.Translate(typeSymbolInternal, context.SyntaxNode, context.Diagnostics);
			}
			return this._marshalTypeNameOrSymbol;
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x060015E7 RID: 5607 RVA: 0x00045257 File Offset: 0x00043457
		string IMarshallingInformation.CustomMarshallerRuntimeArgument
		{
			get
			{
				return this._marshalCookie;
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x060015E8 RID: 5608 RVA: 0x0004525F File Offset: 0x0004345F
		int IMarshallingInformation.NumberOfElements
		{
			get
			{
				return this._marshalArrayElementCount;
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x060015E9 RID: 5609 RVA: 0x00045267 File Offset: 0x00043467
		short IMarshallingInformation.ParamIndex
		{
			get
			{
				return (short)this._marshalParameterIndex;
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x060015EA RID: 5610 RVA: 0x00045270 File Offset: 0x00043470
		UnmanagedType IMarshallingInformation.ElementType
		{
			get
			{
				return this._marshalArrayElementType;
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x060015EB RID: 5611 RVA: 0x00045278 File Offset: 0x00043478
		VarEnum IMarshallingInformation.SafeArrayElementSubtype
		{
			get
			{
				return (VarEnum)this._marshalArrayElementType;
			}
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x00045280 File Offset: 0x00043480
		ITypeReference IMarshallingInformation.GetSafeArrayElementUserDefinedSubtype(EmitContext context)
		{
			if (this._marshalTypeNameOrSymbol == null)
			{
				return null;
			}
			return context.Module.Translate((ITypeSymbolInternal)this._marshalTypeNameOrSymbol, context.SyntaxNode, context.Diagnostics);
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x000452B0 File Offset: 0x000434B0
		internal MarshalPseudoCustomAttributeData WithTranslatedTypes<TTypeSymbol, TArg>(Func<TTypeSymbol, TArg, TTypeSymbol> translator, TArg arg) where TTypeSymbol : ITypeSymbolInternal
		{
			if (this._marshalType != 29 || this._marshalTypeNameOrSymbol == null)
			{
				return this;
			}
			TTypeSymbol ttypeSymbol = translator.Invoke((TTypeSymbol)((object)this._marshalTypeNameOrSymbol), arg);
			if (ttypeSymbol == this._marshalTypeNameOrSymbol)
			{
				return this;
			}
			MarshalPseudoCustomAttributeData marshalPseudoCustomAttributeData = new MarshalPseudoCustomAttributeData();
			marshalPseudoCustomAttributeData.SetMarshalAsSafeArray(new VarEnum?((VarEnum)this._marshalArrayElementType), ttypeSymbol);
			return marshalPseudoCustomAttributeData;
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x00045310 File Offset: 0x00043510
		internal ITypeSymbolInternal TryGetSafeArrayElementUserDefinedSubtype()
		{
			return this._marshalTypeNameOrSymbol as ITypeSymbolInternal;
		}

		// Token: 0x040008FA RID: 2298
		private UnmanagedType _marshalType;

		// Token: 0x040008FB RID: 2299
		private int _marshalArrayElementType;

		// Token: 0x040008FC RID: 2300
		private int _marshalArrayElementCount;

		// Token: 0x040008FD RID: 2301
		private int _marshalParameterIndex;

		// Token: 0x040008FE RID: 2302
		private object _marshalTypeNameOrSymbol;

		// Token: 0x040008FF RID: 2303
		private string _marshalCookie;

		// Token: 0x04000900 RID: 2304
		internal const int Invalid = -1;

		// Token: 0x04000901 RID: 2305
		private const UnmanagedType InvalidUnmanagedType = -1;

		// Token: 0x04000902 RID: 2306
		private const VarEnum InvalidVariantType = (VarEnum)(-1);

		// Token: 0x04000903 RID: 2307
		internal const int MaxMarshalInteger = 536870911;
	}
}
