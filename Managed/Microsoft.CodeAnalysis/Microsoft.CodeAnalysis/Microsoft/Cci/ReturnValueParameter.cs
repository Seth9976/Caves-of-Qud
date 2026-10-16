using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.Cci
{
	// Token: 0x020000C0 RID: 192
	[NullableContext(1)]
	[Nullable(0)]
	internal class ReturnValueParameter : IParameterDefinition, IDefinition, IReference, INamedEntity, IParameterTypeInformation, IParameterListEntry
	{
		// Token: 0x0600064F RID: 1615 RVA: 0x00017673 File Offset: 0x00015873
		internal ReturnValueParameter(IMethodDefinition containingMethod)
		{
			this._containingMethod = containingMethod;
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x00017682 File Offset: 0x00015882
		public IEnumerable<ICustomAttribute> GetAttributes(EmitContext context)
		{
			return this._containingMethod.GetReturnValueAttributes(context);
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000651 RID: 1617 RVA: 0x00017690 File Offset: 0x00015890
		public ISignature ContainingSignature
		{
			get
			{
				return this._containingMethod;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x00017698 File Offset: 0x00015898
		[Nullable(2)]
		public MetadataConstant Constant
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x0001769B File Offset: 0x0001589B
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<ICustomModifier> RefCustomModifiers
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._containingMethod.RefCustomModifiers;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x000176A8 File Offset: 0x000158A8
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<ICustomModifier> CustomModifiers
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._containingMethod.ReturnValueCustomModifiers;
			}
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x000176B5 File Offset: 0x000158B5
		[NullableContext(2)]
		public MetadataConstant GetDefaultValue(EmitContext context)
		{
			return null;
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x000176B8 File Offset: 0x000158B8
		public void Dispatch(MetadataVisitor visitor)
		{
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000657 RID: 1623 RVA: 0x000176BA File Offset: 0x000158BA
		public bool HasDefaultValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000658 RID: 1624 RVA: 0x000176BD File Offset: 0x000158BD
		public ushort Index
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x000176C0 File Offset: 0x000158C0
		public bool IsIn
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600065A RID: 1626 RVA: 0x000176C3 File Offset: 0x000158C3
		public bool IsByReference
		{
			get
			{
				return this._containingMethod.ReturnValueIsByRef;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x000176D0 File Offset: 0x000158D0
		public bool IsMarshalledExplicitly
		{
			get
			{
				return this._containingMethod.ReturnValueIsMarshalledExplicitly;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x000176DD File Offset: 0x000158DD
		public bool IsOptional
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x000176E0 File Offset: 0x000158E0
		public bool IsOut
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x000176E3 File Offset: 0x000158E3
		public IMarshallingInformation MarshallingInformation
		{
			get
			{
				return this._containingMethod.ReturnValueMarshallingInformation;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x000176F0 File Offset: 0x000158F0
		[Nullable(0)]
		public ImmutableArray<byte> MarshallingDescriptor
		{
			[NullableContext(0)]
			get
			{
				return this._containingMethod.ReturnValueMarshallingDescriptor;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x000176FD File Offset: 0x000158FD
		public string Name
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x00017704 File Offset: 0x00015904
		public ITypeReference GetType(EmitContext context)
		{
			return this._containingMethod.GetType(context);
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00017712 File Offset: 0x00015912
		public IDefinition AsDefinition(EmitContext context)
		{
			return this;
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x00017715 File Offset: 0x00015915
		[NullableContext(2)]
		ISymbolInternal IReference.GetInternalSymbol()
		{
			return null;
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00017718 File Offset: 0x00015918
		[NullableContext(2)]
		public sealed override bool Equals(object obj)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0001771F File Offset: 0x0001591F
		public sealed override int GetHashCode()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x040001F7 RID: 503
		private readonly IMethodDefinition _containingMethod;
	}
}
