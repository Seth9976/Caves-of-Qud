using System;

namespace Mono.Cecil
{
	// Token: 0x02000279 RID: 633
	internal abstract class TypeSpecification : TypeReference
	{
		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06000FC5 RID: 4037 RVA: 0x00030C4E File Offset: 0x0002EE4E
		public TypeReference ElementType
		{
			get
			{
				return this.element_type;
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06000FC6 RID: 4038 RVA: 0x00030C56 File Offset: 0x0002EE56
		// (set) Token: 0x06000FC7 RID: 4039 RVA: 0x000189AE File Offset: 0x00016BAE
		public override string Name
		{
			get
			{
				return this.element_type.Name;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06000FC8 RID: 4040 RVA: 0x00030C63 File Offset: 0x0002EE63
		// (set) Token: 0x06000FC9 RID: 4041 RVA: 0x000189AE File Offset: 0x00016BAE
		public override string Namespace
		{
			get
			{
				return this.element_type.Namespace;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06000FCA RID: 4042 RVA: 0x00030C70 File Offset: 0x0002EE70
		// (set) Token: 0x06000FCB RID: 4043 RVA: 0x000189AE File Offset: 0x00016BAE
		public override IMetadataScope Scope
		{
			get
			{
				return this.element_type.Scope;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06000FCC RID: 4044 RVA: 0x00030C7D File Offset: 0x0002EE7D
		public override ModuleDefinition Module
		{
			get
			{
				return this.element_type.Module;
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06000FCD RID: 4045 RVA: 0x00030C8A File Offset: 0x0002EE8A
		public override string FullName
		{
			get
			{
				return this.element_type.FullName;
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06000FCE RID: 4046 RVA: 0x00030C97 File Offset: 0x0002EE97
		public override bool ContainsGenericParameter
		{
			get
			{
				return this.element_type.ContainsGenericParameter;
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06000FCF RID: 4047 RVA: 0x00027E03 File Offset: 0x00026003
		public override MetadataType MetadataType
		{
			get
			{
				return (MetadataType)this.etype;
			}
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x00030CA4 File Offset: 0x0002EEA4
		internal TypeSpecification(TypeReference type)
			: base(null, null)
		{
			this.element_type = type;
			this.token = new MetadataToken(TokenType.TypeSpec);
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x00030CC5 File Offset: 0x0002EEC5
		public override TypeReference GetElementType()
		{
			return this.element_type.GetElementType();
		}

		// Token: 0x04000581 RID: 1409
		private readonly TypeReference element_type;
	}
}
