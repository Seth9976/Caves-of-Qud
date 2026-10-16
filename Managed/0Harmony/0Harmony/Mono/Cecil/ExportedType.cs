using System;

namespace Mono.Cecil
{
	// Token: 0x02000202 RID: 514
	public sealed class ExportedType : IMetadataTokenProvider
	{
		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000A82 RID: 2690 RVA: 0x00026CFB File Offset: 0x00024EFB
		// (set) Token: 0x06000A83 RID: 2691 RVA: 0x00026D03 File Offset: 0x00024F03
		public string Namespace
		{
			get
			{
				return this.@namespace;
			}
			set
			{
				this.@namespace = value;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000A84 RID: 2692 RVA: 0x00026D0C File Offset: 0x00024F0C
		// (set) Token: 0x06000A85 RID: 2693 RVA: 0x00026D14 File Offset: 0x00024F14
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000A86 RID: 2694 RVA: 0x00026D1D File Offset: 0x00024F1D
		// (set) Token: 0x06000A87 RID: 2695 RVA: 0x00026D25 File Offset: 0x00024F25
		public TypeAttributes Attributes
		{
			get
			{
				return (TypeAttributes)this.attributes;
			}
			set
			{
				this.attributes = (uint)value;
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000A88 RID: 2696 RVA: 0x00026D2E File Offset: 0x00024F2E
		// (set) Token: 0x06000A89 RID: 2697 RVA: 0x00026D4A File Offset: 0x00024F4A
		public IMetadataScope Scope
		{
			get
			{
				if (this.declaring_type != null)
				{
					return this.declaring_type.Scope;
				}
				return this.scope;
			}
			set
			{
				if (this.declaring_type != null)
				{
					this.declaring_type.Scope = value;
					return;
				}
				this.scope = value;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000A8A RID: 2698 RVA: 0x00026D68 File Offset: 0x00024F68
		// (set) Token: 0x06000A8B RID: 2699 RVA: 0x00026D70 File Offset: 0x00024F70
		public ExportedType DeclaringType
		{
			get
			{
				return this.declaring_type;
			}
			set
			{
				this.declaring_type = value;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000A8C RID: 2700 RVA: 0x00026D79 File Offset: 0x00024F79
		// (set) Token: 0x06000A8D RID: 2701 RVA: 0x00026D81 File Offset: 0x00024F81
		public MetadataToken MetadataToken
		{
			get
			{
				return this.token;
			}
			set
			{
				this.token = value;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000A8E RID: 2702 RVA: 0x00026D8A File Offset: 0x00024F8A
		// (set) Token: 0x06000A8F RID: 2703 RVA: 0x00026D92 File Offset: 0x00024F92
		public int Identifier
		{
			get
			{
				return this.identifier;
			}
			set
			{
				this.identifier = value;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000A90 RID: 2704 RVA: 0x00026D9B File Offset: 0x00024F9B
		// (set) Token: 0x06000A91 RID: 2705 RVA: 0x00026DAA File Offset: 0x00024FAA
		public bool IsNotPublic
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7U, 0U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7U, 0U, value);
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000A92 RID: 2706 RVA: 0x00026DC0 File Offset: 0x00024FC0
		// (set) Token: 0x06000A93 RID: 2707 RVA: 0x00026DCF File Offset: 0x00024FCF
		public bool IsPublic
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7U, 1U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7U, 1U, value);
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000A94 RID: 2708 RVA: 0x00026DE5 File Offset: 0x00024FE5
		// (set) Token: 0x06000A95 RID: 2709 RVA: 0x00026DF4 File Offset: 0x00024FF4
		public bool IsNestedPublic
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7U, 2U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7U, 2U, value);
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000A96 RID: 2710 RVA: 0x00026E0A File Offset: 0x0002500A
		// (set) Token: 0x06000A97 RID: 2711 RVA: 0x00026E19 File Offset: 0x00025019
		public bool IsNestedPrivate
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7U, 3U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7U, 3U, value);
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000A98 RID: 2712 RVA: 0x00026E2F File Offset: 0x0002502F
		// (set) Token: 0x06000A99 RID: 2713 RVA: 0x00026E3E File Offset: 0x0002503E
		public bool IsNestedFamily
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7U, 4U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7U, 4U, value);
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000A9A RID: 2714 RVA: 0x00026E54 File Offset: 0x00025054
		// (set) Token: 0x06000A9B RID: 2715 RVA: 0x00026E63 File Offset: 0x00025063
		public bool IsNestedAssembly
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7U, 5U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7U, 5U, value);
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000A9C RID: 2716 RVA: 0x00026E79 File Offset: 0x00025079
		// (set) Token: 0x06000A9D RID: 2717 RVA: 0x00026E88 File Offset: 0x00025088
		public bool IsNestedFamilyAndAssembly
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7U, 6U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7U, 6U, value);
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000A9E RID: 2718 RVA: 0x00026E9E File Offset: 0x0002509E
		// (set) Token: 0x06000A9F RID: 2719 RVA: 0x00026EAD File Offset: 0x000250AD
		public bool IsNestedFamilyOrAssembly
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7U, 7U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7U, 7U, value);
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000AA0 RID: 2720 RVA: 0x00026EC3 File Offset: 0x000250C3
		// (set) Token: 0x06000AA1 RID: 2721 RVA: 0x00026ED3 File Offset: 0x000250D3
		public bool IsAutoLayout
		{
			get
			{
				return this.attributes.GetMaskedAttributes(24U, 0U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(24U, 0U, value);
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000AA2 RID: 2722 RVA: 0x00026EEA File Offset: 0x000250EA
		// (set) Token: 0x06000AA3 RID: 2723 RVA: 0x00026EFA File Offset: 0x000250FA
		public bool IsSequentialLayout
		{
			get
			{
				return this.attributes.GetMaskedAttributes(24U, 8U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(24U, 8U, value);
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000AA4 RID: 2724 RVA: 0x00026F11 File Offset: 0x00025111
		// (set) Token: 0x06000AA5 RID: 2725 RVA: 0x00026F22 File Offset: 0x00025122
		public bool IsExplicitLayout
		{
			get
			{
				return this.attributes.GetMaskedAttributes(24U, 16U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(24U, 16U, value);
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000AA6 RID: 2726 RVA: 0x00026F3A File Offset: 0x0002513A
		// (set) Token: 0x06000AA7 RID: 2727 RVA: 0x00026F4A File Offset: 0x0002514A
		public bool IsClass
		{
			get
			{
				return this.attributes.GetMaskedAttributes(32U, 0U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(32U, 0U, value);
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000AA8 RID: 2728 RVA: 0x00026F61 File Offset: 0x00025161
		// (set) Token: 0x06000AA9 RID: 2729 RVA: 0x00026F72 File Offset: 0x00025172
		public bool IsInterface
		{
			get
			{
				return this.attributes.GetMaskedAttributes(32U, 32U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(32U, 32U, value);
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000AAA RID: 2730 RVA: 0x00026F8A File Offset: 0x0002518A
		// (set) Token: 0x06000AAB RID: 2731 RVA: 0x00026F9C File Offset: 0x0002519C
		public bool IsAbstract
		{
			get
			{
				return this.attributes.GetAttributes(128U);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(128U, value);
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000AAC RID: 2732 RVA: 0x00026FB5 File Offset: 0x000251B5
		// (set) Token: 0x06000AAD RID: 2733 RVA: 0x00026FC7 File Offset: 0x000251C7
		public bool IsSealed
		{
			get
			{
				return this.attributes.GetAttributes(256U);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(256U, value);
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000AAE RID: 2734 RVA: 0x00026FE0 File Offset: 0x000251E0
		// (set) Token: 0x06000AAF RID: 2735 RVA: 0x00026FF2 File Offset: 0x000251F2
		public bool IsSpecialName
		{
			get
			{
				return this.attributes.GetAttributes(1024U);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(1024U, value);
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000AB0 RID: 2736 RVA: 0x0002700B File Offset: 0x0002520B
		// (set) Token: 0x06000AB1 RID: 2737 RVA: 0x0002701D File Offset: 0x0002521D
		public bool IsImport
		{
			get
			{
				return this.attributes.GetAttributes(4096U);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(4096U, value);
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000AB2 RID: 2738 RVA: 0x00027036 File Offset: 0x00025236
		// (set) Token: 0x06000AB3 RID: 2739 RVA: 0x00027048 File Offset: 0x00025248
		public bool IsSerializable
		{
			get
			{
				return this.attributes.GetAttributes(8192U);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(8192U, value);
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000AB4 RID: 2740 RVA: 0x00027061 File Offset: 0x00025261
		// (set) Token: 0x06000AB5 RID: 2741 RVA: 0x00027074 File Offset: 0x00025274
		public bool IsAnsiClass
		{
			get
			{
				return this.attributes.GetMaskedAttributes(196608U, 0U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(196608U, 0U, value);
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000AB6 RID: 2742 RVA: 0x0002708E File Offset: 0x0002528E
		// (set) Token: 0x06000AB7 RID: 2743 RVA: 0x000270A5 File Offset: 0x000252A5
		public bool IsUnicodeClass
		{
			get
			{
				return this.attributes.GetMaskedAttributes(196608U, 65536U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(196608U, 65536U, value);
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000AB8 RID: 2744 RVA: 0x000270C3 File Offset: 0x000252C3
		// (set) Token: 0x06000AB9 RID: 2745 RVA: 0x000270DA File Offset: 0x000252DA
		public bool IsAutoClass
		{
			get
			{
				return this.attributes.GetMaskedAttributes(196608U, 131072U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(196608U, 131072U, value);
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000ABA RID: 2746 RVA: 0x000270F8 File Offset: 0x000252F8
		// (set) Token: 0x06000ABB RID: 2747 RVA: 0x0002710A File Offset: 0x0002530A
		public bool IsBeforeFieldInit
		{
			get
			{
				return this.attributes.GetAttributes(1048576U);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(1048576U, value);
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x00027123 File Offset: 0x00025323
		// (set) Token: 0x06000ABD RID: 2749 RVA: 0x00027135 File Offset: 0x00025335
		public bool IsRuntimeSpecialName
		{
			get
			{
				return this.attributes.GetAttributes(2048U);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(2048U, value);
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x0002714E File Offset: 0x0002534E
		// (set) Token: 0x06000ABF RID: 2751 RVA: 0x00027160 File Offset: 0x00025360
		public bool HasSecurity
		{
			get
			{
				return this.attributes.GetAttributes(262144U);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(262144U, value);
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x00027179 File Offset: 0x00025379
		// (set) Token: 0x06000AC1 RID: 2753 RVA: 0x0002718B File Offset: 0x0002538B
		public bool IsForwarder
		{
			get
			{
				return this.attributes.GetAttributes(2097152U);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(2097152U, value);
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x000271A4 File Offset: 0x000253A4
		public string FullName
		{
			get
			{
				string text = (string.IsNullOrEmpty(this.@namespace) ? this.name : (this.@namespace + "." + this.name));
				if (this.declaring_type != null)
				{
					return this.declaring_type.FullName + "/" + text;
				}
				return text;
			}
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x000271FD File Offset: 0x000253FD
		public ExportedType(string @namespace, string name, ModuleDefinition module, IMetadataScope scope)
		{
			this.@namespace = @namespace;
			this.name = name;
			this.scope = scope;
			this.module = module;
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x00027222 File Offset: 0x00025422
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x0002722A File Offset: 0x0002542A
		public TypeDefinition Resolve()
		{
			return this.module.Resolve(this.CreateReference());
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x0002723D File Offset: 0x0002543D
		internal TypeReference CreateReference()
		{
			return new TypeReference(this.@namespace, this.name, this.module, this.scope)
			{
				DeclaringType = ((this.declaring_type != null) ? this.declaring_type.CreateReference() : null)
			};
		}

		// Token: 0x04000303 RID: 771
		private string @namespace;

		// Token: 0x04000304 RID: 772
		private string name;

		// Token: 0x04000305 RID: 773
		private uint attributes;

		// Token: 0x04000306 RID: 774
		private IMetadataScope scope;

		// Token: 0x04000307 RID: 775
		private ModuleDefinition module;

		// Token: 0x04000308 RID: 776
		private int identifier;

		// Token: 0x04000309 RID: 777
		private ExportedType declaring_type;

		// Token: 0x0400030A RID: 778
		internal MetadataToken token;
	}
}
