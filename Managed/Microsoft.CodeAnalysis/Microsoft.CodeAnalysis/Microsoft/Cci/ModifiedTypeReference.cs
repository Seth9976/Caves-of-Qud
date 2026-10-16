using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.Cci
{
	// Token: 0x020000B9 RID: 185
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class ModifiedTypeReference : IModifiedTypeReference, ITypeReference, IReference
	{
		// Token: 0x060005F0 RID: 1520 RVA: 0x00016044 File Offset: 0x00014244
		[NullableContext(1)]
		public ModifiedTypeReference(ITypeReference modifiedType, [Nullable(new byte[] { 0, 1 })] ImmutableArray<ICustomModifier> customModifiers)
		{
			this._modifiedType = modifiedType;
			this._customModifiers = customModifiers;
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x0001605A File Offset: 0x0001425A
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<ICustomModifier> IModifiedTypeReference.CustomModifiers
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._customModifiers;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x00016062 File Offset: 0x00014262
		[Nullable(1)]
		ITypeReference IModifiedTypeReference.UnmodifiedType
		{
			[NullableContext(1)]
			get
			{
				return this._modifiedType;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x0001606A File Offset: 0x0001426A
		bool ITypeReference.IsEnum
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x00016071 File Offset: 0x00014271
		bool ITypeReference.IsValueType
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x00016078 File Offset: 0x00014278
		[NullableContext(1)]
		ITypeDefinition ITypeReference.GetResolvedType(EmitContext context)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x0001607F File Offset: 0x0001427F
		PrimitiveTypeCode ITypeReference.TypeCode
		{
			get
			{
				return PrimitiveTypeCode.NotPrimitive;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x00016083 File Offset: 0x00014283
		TypeDefinitionHandle ITypeReference.TypeDef
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0001608A File Offset: 0x0001428A
		[NullableContext(1)]
		IEnumerable<ICustomAttribute> IReference.GetAttributes(EmitContext context)
		{
			return SpecializedCollections.EmptyEnumerable<ICustomAttribute>();
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00016091 File Offset: 0x00014291
		[NullableContext(1)]
		void IReference.Dispatch(MetadataVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x0001609A File Offset: 0x0001429A
		IGenericMethodParameterReference ITypeReference.AsGenericMethodParameterReference
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x0001609D File Offset: 0x0001429D
		IGenericTypeInstanceReference ITypeReference.AsGenericTypeInstanceReference
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x000160A0 File Offset: 0x000142A0
		IGenericTypeParameterReference ITypeReference.AsGenericTypeParameterReference
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x000160A3 File Offset: 0x000142A3
		INamespaceTypeDefinition ITypeReference.AsNamespaceTypeDefinition(EmitContext context)
		{
			return null;
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x000160A6 File Offset: 0x000142A6
		INamespaceTypeReference ITypeReference.AsNamespaceTypeReference
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x000160A9 File Offset: 0x000142A9
		INestedTypeDefinition ITypeReference.AsNestedTypeDefinition(EmitContext context)
		{
			return null;
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000600 RID: 1536 RVA: 0x000160AC File Offset: 0x000142AC
		INestedTypeReference ITypeReference.AsNestedTypeReference
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x000160AF File Offset: 0x000142AF
		ISpecializedNestedTypeReference ITypeReference.AsSpecializedNestedTypeReference
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x000160B2 File Offset: 0x000142B2
		ITypeDefinition ITypeReference.AsTypeDefinition(EmitContext context)
		{
			return null;
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x000160B5 File Offset: 0x000142B5
		IDefinition IReference.AsDefinition(EmitContext context)
		{
			return null;
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x000160B8 File Offset: 0x000142B8
		ISymbolInternal IReference.GetInternalSymbol()
		{
			return null;
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x000160BB File Offset: 0x000142BB
		public sealed override bool Equals(object obj)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x000160C2 File Offset: 0x000142C2
		public sealed override int GetHashCode()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x040001EC RID: 492
		[Nullable(1)]
		private readonly ITypeReference _modifiedType;

		// Token: 0x040001ED RID: 493
		[Nullable(new byte[] { 0, 1 })]
		private readonly ImmutableArray<ICustomModifier> _customModifiers;
	}
}
