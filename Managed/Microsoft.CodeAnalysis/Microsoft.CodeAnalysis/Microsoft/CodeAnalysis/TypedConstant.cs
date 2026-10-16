using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000265 RID: 613
	[NullableContext(2)]
	[Nullable(0)]
	public struct TypedConstant : IEquatable<TypedConstant>
	{
		// Token: 0x06001761 RID: 5985 RVA: 0x00045F2D File Offset: 0x0004412D
		internal TypedConstant(ITypeSymbolInternal type, TypedConstantKind kind, object value)
		{
			this._kind = kind;
			this._type = type;
			this._value = value;
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x00045F44 File Offset: 0x00044144
		[NullableContext(0)]
		internal TypedConstant([Nullable(1)] ITypeSymbolInternal type, ImmutableArray<TypedConstant> array)
		{
			this = new TypedConstant(type, TypedConstantKind.Array, array.IsDefault ? null : array);
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06001763 RID: 5987 RVA: 0x00045F60 File Offset: 0x00044160
		public TypedConstantKind Kind
		{
			get
			{
				return this._kind;
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06001764 RID: 5988 RVA: 0x00045F68 File Offset: 0x00044168
		public ITypeSymbol Type
		{
			get
			{
				ITypeSymbolInternal type = this._type;
				if (type == null)
				{
					return null;
				}
				return type.GetITypeSymbol();
			}
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06001765 RID: 5989 RVA: 0x00045F7B File Offset: 0x0004417B
		internal ITypeSymbolInternal TypeInternal
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06001766 RID: 5990 RVA: 0x00045F83 File Offset: 0x00044183
		public bool IsNull
		{
			get
			{
				return this._value == null;
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06001767 RID: 5991 RVA: 0x00045F90 File Offset: 0x00044190
		public object Value
		{
			get
			{
				object valueInternal = this.ValueInternal;
				ISymbolInternal symbolInternal = valueInternal as ISymbolInternal;
				if (symbolInternal != null)
				{
					return symbolInternal.GetISymbol();
				}
				return valueInternal;
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06001768 RID: 5992 RVA: 0x00045FB6 File Offset: 0x000441B6
		internal object ValueInternal
		{
			get
			{
				if (this.Kind == TypedConstantKind.Array)
				{
					throw new InvalidOperationException("TypedConstant is an array. Use Values property.");
				}
				return this._value;
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06001769 RID: 5993 RVA: 0x00045FD4 File Offset: 0x000441D4
		[Nullable(0)]
		public ImmutableArray<TypedConstant> Values
		{
			[NullableContext(0)]
			get
			{
				if (this.Kind != TypedConstantKind.Array)
				{
					throw new InvalidOperationException("TypedConstant is not an array. Use Value property.");
				}
				if (this.IsNull)
				{
					return default(ImmutableArray<TypedConstant>);
				}
				return (ImmutableArray<TypedConstant>)this._value;
			}
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x00046014 File Offset: 0x00044214
		internal T DecodeValue<T>(SpecialType specialType)
		{
			T t;
			this.TryDecodeValue<T>(specialType, out t);
			return t;
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x0004602C File Offset: 0x0004422C
		[NullableContext(1)]
		internal bool TryDecodeValue<[Nullable(2)] T>(SpecialType specialType, [MaybeNullWhen(false)] out T value)
		{
			if (this._kind == TypedConstantKind.Error)
			{
				value = default(T);
				return false;
			}
			if (this._type.SpecialType == specialType || (this._type.TypeKind == TypeKind.Enum && specialType == SpecialType.System_Enum))
			{
				value = (T)((object)this._value);
				return true;
			}
			value = default(T);
			return false;
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x00046088 File Offset: 0x00044288
		[NullableContext(1)]
		internal static TypedConstantKind GetTypedConstantKind(ITypeSymbolInternal type, Compilation compilation)
		{
			switch (type.SpecialType)
			{
			case SpecialType.System_Object:
			case SpecialType.System_Boolean:
			case SpecialType.System_Char:
			case SpecialType.System_SByte:
			case SpecialType.System_Byte:
			case SpecialType.System_Int16:
			case SpecialType.System_UInt16:
			case SpecialType.System_Int32:
			case SpecialType.System_UInt32:
			case SpecialType.System_Int64:
			case SpecialType.System_UInt64:
			case SpecialType.System_Single:
			case SpecialType.System_Double:
			case SpecialType.System_String:
				return TypedConstantKind.Primitive;
			}
			TypeKind typeKind = type.TypeKind;
			if (typeKind == TypeKind.Array)
			{
				return TypedConstantKind.Array;
			}
			if (typeKind == TypeKind.Enum)
			{
				return TypedConstantKind.Enum;
			}
			if (typeKind == TypeKind.Error)
			{
				return TypedConstantKind.Error;
			}
			if (compilation != null && compilation.IsSystemTypeReference(type))
			{
				return TypedConstantKind.Type;
			}
			return TypedConstantKind.Error;
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x00046122 File Offset: 0x00044322
		public override bool Equals(object obj)
		{
			return obj is TypedConstant && this.Equals((TypedConstant)obj);
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x0004613A File Offset: 0x0004433A
		public bool Equals(TypedConstant other)
		{
			return this._kind == other._kind && object.Equals(this._value, other._value) && object.Equals(this._type, other._type);
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x00046170 File Offset: 0x00044370
		public override int GetHashCode()
		{
			return Hash.Combine<object>(this._value, Hash.Combine<ITypeSymbolInternal>(this._type, (int)this.Kind));
		}

		// Token: 0x0400097F RID: 2431
		private readonly TypedConstantKind _kind;

		// Token: 0x04000980 RID: 2432
		private readonly ITypeSymbolInternal _type;

		// Token: 0x04000981 RID: 2433
		private readonly object _value;
	}
}
