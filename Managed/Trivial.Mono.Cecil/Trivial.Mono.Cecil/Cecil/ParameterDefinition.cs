using System;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000099 RID: 153
	public sealed class ParameterDefinition : ParameterReference, ICustomAttributeProvider, IMetadataTokenProvider, IConstantProvider, IMarshalInfoProvider
	{
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x0001448A File Offset: 0x0001268A
		// (set) Token: 0x06000657 RID: 1623 RVA: 0x00014492 File Offset: 0x00012692
		public ParameterAttributes Attributes
		{
			get
			{
				return (ParameterAttributes)this.attributes;
			}
			set
			{
				this.attributes = (ushort)value;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000658 RID: 1624 RVA: 0x0001449B File Offset: 0x0001269B
		public IMethodSignature Method
		{
			get
			{
				return this.method;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x000144A3 File Offset: 0x000126A3
		public int Sequence
		{
			get
			{
				if (this.method == null)
				{
					return -1;
				}
				if (!this.method.HasImplicitThis())
				{
					return this.index;
				}
				return this.index + 1;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x0600065A RID: 1626 RVA: 0x000144CB File Offset: 0x000126CB
		// (set) Token: 0x0600065B RID: 1627 RVA: 0x000144F4 File Offset: 0x000126F4
		public bool HasConstant
		{
			get
			{
				this.ResolveConstant(ref this.constant, this.parameter_type.Module);
				return this.constant != Mixin.NoValue;
			}
			set
			{
				if (!value)
				{
					this.constant = Mixin.NoValue;
				}
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x00014504 File Offset: 0x00012704
		// (set) Token: 0x0600065D RID: 1629 RVA: 0x00014516 File Offset: 0x00012716
		public object Constant
		{
			get
			{
				if (!this.HasConstant)
				{
					return null;
				}
				return this.constant;
			}
			set
			{
				this.constant = value;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x0001451F File Offset: 0x0001271F
		public bool HasCustomAttributes
		{
			get
			{
				if (this.custom_attributes != null)
				{
					return this.custom_attributes.Count > 0;
				}
				return this.GetHasCustomAttributes(this.parameter_type.Module);
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x00014549 File Offset: 0x00012749
		public Collection<CustomAttribute> CustomAttributes
		{
			get
			{
				return this.custom_attributes ?? this.GetCustomAttributes(ref this.custom_attributes, this.parameter_type.Module);
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x0001456C File Offset: 0x0001276C
		public bool HasMarshalInfo
		{
			get
			{
				return this.marshal_info != null || this.GetHasMarshalInfo(this.parameter_type.Module);
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x00014589 File Offset: 0x00012789
		// (set) Token: 0x06000662 RID: 1634 RVA: 0x000145AC File Offset: 0x000127AC
		public MarshalInfo MarshalInfo
		{
			get
			{
				return this.marshal_info ?? this.GetMarshalInfo(ref this.marshal_info, this.parameter_type.Module);
			}
			set
			{
				this.marshal_info = value;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x000145B5 File Offset: 0x000127B5
		// (set) Token: 0x06000664 RID: 1636 RVA: 0x000145C3 File Offset: 0x000127C3
		public bool IsIn
		{
			get
			{
				return this.attributes.GetAttributes(1);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(1, value);
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000665 RID: 1637 RVA: 0x000145D8 File Offset: 0x000127D8
		// (set) Token: 0x06000666 RID: 1638 RVA: 0x000145E6 File Offset: 0x000127E6
		public bool IsOut
		{
			get
			{
				return this.attributes.GetAttributes(2);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(2, value);
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000667 RID: 1639 RVA: 0x000145FB File Offset: 0x000127FB
		// (set) Token: 0x06000668 RID: 1640 RVA: 0x00014609 File Offset: 0x00012809
		public bool IsLcid
		{
			get
			{
				return this.attributes.GetAttributes(4);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(4, value);
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x0001461E File Offset: 0x0001281E
		// (set) Token: 0x0600066A RID: 1642 RVA: 0x0001462C File Offset: 0x0001282C
		public bool IsReturnValue
		{
			get
			{
				return this.attributes.GetAttributes(8);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(8, value);
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x00014641 File Offset: 0x00012841
		// (set) Token: 0x0600066C RID: 1644 RVA: 0x00014650 File Offset: 0x00012850
		public bool IsOptional
		{
			get
			{
				return this.attributes.GetAttributes(16);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(16, value);
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x00014666 File Offset: 0x00012866
		// (set) Token: 0x0600066E RID: 1646 RVA: 0x00014678 File Offset: 0x00012878
		public bool HasDefault
		{
			get
			{
				return this.attributes.GetAttributes(4096);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(4096, value);
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x00014691 File Offset: 0x00012891
		// (set) Token: 0x06000670 RID: 1648 RVA: 0x000146A3 File Offset: 0x000128A3
		public bool HasFieldMarshal
		{
			get
			{
				return this.attributes.GetAttributes(8192);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(8192, value);
			}
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x000146BC File Offset: 0x000128BC
		internal ParameterDefinition(TypeReference parameterType, IMethodSignature method)
			: this(string.Empty, ParameterAttributes.None, parameterType)
		{
			this.method = method;
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x000146D2 File Offset: 0x000128D2
		public ParameterDefinition(TypeReference parameterType)
			: this(string.Empty, ParameterAttributes.None, parameterType)
		{
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x000146E1 File Offset: 0x000128E1
		public ParameterDefinition(string name, ParameterAttributes attributes, TypeReference parameterType)
			: base(name, parameterType)
		{
			this.attributes = (ushort)attributes;
			this.token = new MetadataToken(TokenType.Param);
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x00002654 File Offset: 0x00000854
		public override ParameterDefinition Resolve()
		{
			return this;
		}

		// Token: 0x04000211 RID: 529
		private ushort attributes;

		// Token: 0x04000212 RID: 530
		internal IMethodSignature method;

		// Token: 0x04000213 RID: 531
		private object constant = Mixin.NotResolved;

		// Token: 0x04000214 RID: 532
		private Collection<CustomAttribute> custom_attributes;

		// Token: 0x04000215 RID: 533
		private MarshalInfo marshal_info;
	}
}
