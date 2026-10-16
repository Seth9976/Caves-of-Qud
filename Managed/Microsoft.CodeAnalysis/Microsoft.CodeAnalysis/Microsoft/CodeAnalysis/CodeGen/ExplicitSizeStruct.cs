using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.Emit;

namespace Microsoft.CodeAnalysis.CodeGen
{
	// Token: 0x020004D8 RID: 1240
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class ExplicitSizeStruct : DefaultTypeDef, INestedTypeDefinition, INamedTypeDefinition, ITypeDefinition, IDefinition, IReference, ITypeReference, INamedTypeReference, INamedEntity, ITypeDefinitionMember, ITypeMemberReference, INestedTypeReference
	{
		// Token: 0x060031E2 RID: 12770 RVA: 0x0008E6EB File Offset: 0x0008C8EB
		internal ExplicitSizeStruct(uint size, PrivateImplementationDetails containingType, ITypeReference sysValueType)
		{
			this._size = size;
			this._containingType = containingType;
			this._sysValueType = sysValueType;
		}

		// Token: 0x060031E3 RID: 12771 RVA: 0x0008E708 File Offset: 0x0008C908
		public override string ToString()
		{
			return this._containingType.ToString() + "." + this.Name;
		}

		// Token: 0x17000E4F RID: 3663
		// (get) Token: 0x060031E4 RID: 12772 RVA: 0x0008E725 File Offset: 0x0008C925
		public override ushort Alignment
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x060031E5 RID: 12773 RVA: 0x0008E728 File Offset: 0x0008C928
		public override ITypeReference GetBaseClass(EmitContext context)
		{
			return this._sysValueType;
		}

		// Token: 0x17000E50 RID: 3664
		// (get) Token: 0x060031E6 RID: 12774 RVA: 0x0008E730 File Offset: 0x0008C930
		public override LayoutKind Layout
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000E51 RID: 3665
		// (get) Token: 0x060031E7 RID: 12775 RVA: 0x0008E733 File Offset: 0x0008C933
		public override uint SizeOf
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x060031E8 RID: 12776 RVA: 0x0008E73B File Offset: 0x0008C93B
		public override void Dispatch(MetadataVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x17000E52 RID: 3666
		// (get) Token: 0x060031E9 RID: 12777 RVA: 0x0008E744 File Offset: 0x0008C944
		public string Name
		{
			get
			{
				return "__StaticArrayInitTypeSize=" + this._size.ToString();
			}
		}

		// Token: 0x17000E53 RID: 3667
		// (get) Token: 0x060031EA RID: 12778 RVA: 0x0008E769 File Offset: 0x0008C969
		public ITypeDefinition ContainingTypeDefinition
		{
			get
			{
				return this._containingType;
			}
		}

		// Token: 0x17000E54 RID: 3668
		// (get) Token: 0x060031EB RID: 12779 RVA: 0x0008E771 File Offset: 0x0008C971
		public TypeMemberVisibility Visibility
		{
			get
			{
				return TypeMemberVisibility.Private;
			}
		}

		// Token: 0x17000E55 RID: 3669
		// (get) Token: 0x060031EC RID: 12780 RVA: 0x0008E774 File Offset: 0x0008C974
		public override bool IsValueType
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060031ED RID: 12781 RVA: 0x0008E777 File Offset: 0x0008C977
		public ITypeReference GetContainingType(EmitContext context)
		{
			return this._containingType;
		}

		// Token: 0x060031EE RID: 12782 RVA: 0x0008E77F File Offset: 0x0008C97F
		public override INestedTypeDefinition AsNestedTypeDefinition(EmitContext context)
		{
			return this;
		}

		// Token: 0x17000E56 RID: 3670
		// (get) Token: 0x060031EF RID: 12783 RVA: 0x0008E782 File Offset: 0x0008C982
		public override INestedTypeReference AsNestedTypeReference
		{
			get
			{
				return this;
			}
		}

		// Token: 0x04001354 RID: 4948
		private readonly uint _size;

		// Token: 0x04001355 RID: 4949
		private readonly INamedTypeDefinition _containingType;

		// Token: 0x04001356 RID: 4950
		private readonly ITypeReference _sysValueType;
	}
}
