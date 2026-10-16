using System;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x02000204 RID: 516
	public sealed class FieldDefinition : FieldReference, IMemberDefinition, ICustomAttributeProvider, IMetadataTokenProvider, IConstantProvider, IMarshalInfoProvider
	{
		// Token: 0x06000AC7 RID: 2759 RVA: 0x00027278 File Offset: 0x00025478
		private void ResolveLayout()
		{
			if (this.offset != -2)
			{
				return;
			}
			if (!base.HasImage)
			{
				this.offset = -1;
				return;
			}
			object syncRoot = this.Module.SyncRoot;
			lock (syncRoot)
			{
				if (this.offset == -2)
				{
					this.offset = this.Module.Read<FieldDefinition, int>(this, (FieldDefinition field, MetadataReader reader) => reader.ReadFieldLayout(field));
				}
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000AC8 RID: 2760 RVA: 0x00027310 File Offset: 0x00025510
		public bool HasLayoutInfo
		{
			get
			{
				if (this.offset >= 0)
				{
					return true;
				}
				this.ResolveLayout();
				return this.offset >= 0;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x0002732F File Offset: 0x0002552F
		// (set) Token: 0x06000ACA RID: 2762 RVA: 0x00027358 File Offset: 0x00025558
		public int Offset
		{
			get
			{
				if (this.offset >= 0)
				{
					return this.offset;
				}
				this.ResolveLayout();
				if (this.offset < 0)
				{
					return -1;
				}
				return this.offset;
			}
			set
			{
				this.offset = value;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x00027361 File Offset: 0x00025561
		// (set) Token: 0x06000ACC RID: 2764 RVA: 0x0002736E File Offset: 0x0002556E
		internal FieldDefinitionProjection WindowsRuntimeProjection
		{
			get
			{
				return (FieldDefinitionProjection)this.projection;
			}
			set
			{
				this.projection = value;
			}
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x00027378 File Offset: 0x00025578
		private void ResolveRVA()
		{
			if (this.rva != -2)
			{
				return;
			}
			if (!base.HasImage)
			{
				return;
			}
			object syncRoot = this.Module.SyncRoot;
			lock (syncRoot)
			{
				if (this.rva == -2)
				{
					this.rva = this.Module.Read<FieldDefinition, int>(this, (FieldDefinition field, MetadataReader reader) => reader.ReadFieldRVA(field));
				}
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000ACE RID: 2766 RVA: 0x00027408 File Offset: 0x00025608
		public int RVA
		{
			get
			{
				if (this.rva > 0)
				{
					return this.rva;
				}
				this.ResolveRVA();
				if (this.rva <= 0)
				{
					return 0;
				}
				return this.rva;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x00027431 File Offset: 0x00025631
		// (set) Token: 0x06000AD0 RID: 2768 RVA: 0x00027461 File Offset: 0x00025661
		public byte[] InitialValue
		{
			get
			{
				if (this.initial_value != null)
				{
					return this.initial_value;
				}
				this.ResolveRVA();
				if (this.initial_value == null)
				{
					this.initial_value = Empty<byte>.Array;
				}
				return this.initial_value;
			}
			set
			{
				this.initial_value = value;
				this.HasFieldRVA = !this.initial_value.IsNullOrEmpty<byte>();
				this.rva = 0;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x00027485 File Offset: 0x00025685
		// (set) Token: 0x06000AD2 RID: 2770 RVA: 0x0002748D File Offset: 0x0002568D
		public FieldAttributes Attributes
		{
			get
			{
				return (FieldAttributes)this.attributes;
			}
			set
			{
				if (base.IsWindowsRuntimeProjection && value != (FieldAttributes)this.attributes)
				{
					throw new InvalidOperationException();
				}
				this.attributes = (ushort)value;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x000274AD File Offset: 0x000256AD
		// (set) Token: 0x06000AD4 RID: 2772 RVA: 0x000274D1 File Offset: 0x000256D1
		public bool HasConstant
		{
			get
			{
				this.ResolveConstant(ref this.constant, this.Module);
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

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000AD5 RID: 2773 RVA: 0x000274E1 File Offset: 0x000256E1
		// (set) Token: 0x06000AD6 RID: 2774 RVA: 0x000274F3 File Offset: 0x000256F3
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

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000AD7 RID: 2775 RVA: 0x000274FC File Offset: 0x000256FC
		public bool HasCustomAttributes
		{
			get
			{
				if (this.custom_attributes != null)
				{
					return this.custom_attributes.Count > 0;
				}
				return this.GetHasCustomAttributes(this.Module);
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x00027521 File Offset: 0x00025721
		public Collection<CustomAttribute> CustomAttributes
		{
			get
			{
				return this.custom_attributes ?? this.GetCustomAttributes(ref this.custom_attributes, this.Module);
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000AD9 RID: 2777 RVA: 0x0002753F File Offset: 0x0002573F
		public bool HasMarshalInfo
		{
			get
			{
				return this.marshal_info != null || this.GetHasMarshalInfo(this.Module);
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x00027557 File Offset: 0x00025757
		// (set) Token: 0x06000ADB RID: 2779 RVA: 0x00027575 File Offset: 0x00025775
		public MarshalInfo MarshalInfo
		{
			get
			{
				return this.marshal_info ?? this.GetMarshalInfo(ref this.marshal_info, this.Module);
			}
			set
			{
				this.marshal_info = value;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x0002757E File Offset: 0x0002577E
		// (set) Token: 0x06000ADD RID: 2781 RVA: 0x0002758D File Offset: 0x0002578D
		public bool IsCompilerControlled
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7, 0U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7, 0U, value);
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x000275A3 File Offset: 0x000257A3
		// (set) Token: 0x06000ADF RID: 2783 RVA: 0x000275B2 File Offset: 0x000257B2
		public bool IsPrivate
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7, 1U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7, 1U, value);
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x000275C8 File Offset: 0x000257C8
		// (set) Token: 0x06000AE1 RID: 2785 RVA: 0x000275D7 File Offset: 0x000257D7
		public bool IsFamilyAndAssembly
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7, 2U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7, 2U, value);
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000AE2 RID: 2786 RVA: 0x000275ED File Offset: 0x000257ED
		// (set) Token: 0x06000AE3 RID: 2787 RVA: 0x000275FC File Offset: 0x000257FC
		public bool IsAssembly
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7, 3U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7, 3U, value);
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x00027612 File Offset: 0x00025812
		// (set) Token: 0x06000AE5 RID: 2789 RVA: 0x00027621 File Offset: 0x00025821
		public bool IsFamily
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7, 4U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7, 4U, value);
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000AE6 RID: 2790 RVA: 0x00027637 File Offset: 0x00025837
		// (set) Token: 0x06000AE7 RID: 2791 RVA: 0x00027646 File Offset: 0x00025846
		public bool IsFamilyOrAssembly
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7, 5U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7, 5U, value);
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000AE8 RID: 2792 RVA: 0x0002765C File Offset: 0x0002585C
		// (set) Token: 0x06000AE9 RID: 2793 RVA: 0x0002766B File Offset: 0x0002586B
		public bool IsPublic
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7, 6U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7, 6U, value);
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000AEA RID: 2794 RVA: 0x00027681 File Offset: 0x00025881
		// (set) Token: 0x06000AEB RID: 2795 RVA: 0x00027690 File Offset: 0x00025890
		public bool IsStatic
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

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000AEC RID: 2796 RVA: 0x000276A6 File Offset: 0x000258A6
		// (set) Token: 0x06000AED RID: 2797 RVA: 0x000276B5 File Offset: 0x000258B5
		public bool IsInitOnly
		{
			get
			{
				return this.attributes.GetAttributes(32);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(32, value);
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000AEE RID: 2798 RVA: 0x000276CB File Offset: 0x000258CB
		// (set) Token: 0x06000AEF RID: 2799 RVA: 0x000276DA File Offset: 0x000258DA
		public bool IsLiteral
		{
			get
			{
				return this.attributes.GetAttributes(64);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(64, value);
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x000276F0 File Offset: 0x000258F0
		// (set) Token: 0x06000AF1 RID: 2801 RVA: 0x00027702 File Offset: 0x00025902
		public bool IsNotSerialized
		{
			get
			{
				return this.attributes.GetAttributes(128);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(128, value);
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x0002771B File Offset: 0x0002591B
		// (set) Token: 0x06000AF3 RID: 2803 RVA: 0x0002772D File Offset: 0x0002592D
		public bool IsSpecialName
		{
			get
			{
				return this.attributes.GetAttributes(512);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(512, value);
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x00027746 File Offset: 0x00025946
		// (set) Token: 0x06000AF5 RID: 2805 RVA: 0x00027758 File Offset: 0x00025958
		public bool IsPInvokeImpl
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

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000AF6 RID: 2806 RVA: 0x00027771 File Offset: 0x00025971
		// (set) Token: 0x06000AF7 RID: 2807 RVA: 0x00027783 File Offset: 0x00025983
		public bool IsRuntimeSpecialName
		{
			get
			{
				return this.attributes.GetAttributes(1024);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(1024, value);
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000AF8 RID: 2808 RVA: 0x0002779C File Offset: 0x0002599C
		// (set) Token: 0x06000AF9 RID: 2809 RVA: 0x000277AE File Offset: 0x000259AE
		public bool HasDefault
		{
			get
			{
				return this.attributes.GetAttributes(32768);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(32768, value);
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000AFA RID: 2810 RVA: 0x000277C7 File Offset: 0x000259C7
		// (set) Token: 0x06000AFB RID: 2811 RVA: 0x000277D9 File Offset: 0x000259D9
		public bool HasFieldRVA
		{
			get
			{
				return this.attributes.GetAttributes(256);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(256, value);
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000AFC RID: 2812 RVA: 0x00018941 File Offset: 0x00016B41
		public override bool IsDefinition
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000AFD RID: 2813 RVA: 0x00026BD0 File Offset: 0x00024DD0
		// (set) Token: 0x06000AFE RID: 2814 RVA: 0x00026BDD File Offset: 0x00024DDD
		public new TypeDefinition DeclaringType
		{
			get
			{
				return (TypeDefinition)base.DeclaringType;
			}
			set
			{
				base.DeclaringType = value;
			}
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x000277F2 File Offset: 0x000259F2
		public FieldDefinition(string name, FieldAttributes attributes, TypeReference fieldType)
			: base(name, fieldType)
		{
			this.attributes = (ushort)attributes;
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x00018380 File Offset: 0x00016580
		public override FieldDefinition Resolve()
		{
			return this;
		}

		// Token: 0x0400031E RID: 798
		private ushort attributes;

		// Token: 0x0400031F RID: 799
		private Collection<CustomAttribute> custom_attributes;

		// Token: 0x04000320 RID: 800
		private int offset = -2;

		// Token: 0x04000321 RID: 801
		internal int rva = -2;

		// Token: 0x04000322 RID: 802
		private byte[] initial_value;

		// Token: 0x04000323 RID: 803
		private object constant = Mixin.NotResolved;

		// Token: 0x04000324 RID: 804
		private MarshalInfo marshal_info;
	}
}
