using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x020000C8 RID: 200
	public abstract class TypeSpecification : TypeReference
	{
		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000862 RID: 2146 RVA: 0x0001A2DE File Offset: 0x000184DE
		public TypeReference ElementType
		{
			get
			{
				return this.element_type;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000863 RID: 2147 RVA: 0x0001A2E6 File Offset: 0x000184E6
		// (set) Token: 0x06000864 RID: 2148 RVA: 0x00002B65 File Offset: 0x00000D65
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

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000865 RID: 2149 RVA: 0x0001A2F3 File Offset: 0x000184F3
		// (set) Token: 0x06000866 RID: 2150 RVA: 0x00002B65 File Offset: 0x00000D65
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

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000867 RID: 2151 RVA: 0x0001A300 File Offset: 0x00018500
		// (set) Token: 0x06000868 RID: 2152 RVA: 0x00002B65 File Offset: 0x00000D65
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

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000869 RID: 2153 RVA: 0x0001A30D File Offset: 0x0001850D
		public override ModuleDefinition Module
		{
			get
			{
				return this.element_type.Module;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x0600086A RID: 2154 RVA: 0x0001A31A File Offset: 0x0001851A
		public override string FullName
		{
			get
			{
				return this.element_type.FullName;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x0600086B RID: 2155 RVA: 0x0001A327 File Offset: 0x00018527
		public override bool ContainsGenericParameter
		{
			get
			{
				return this.element_type.ContainsGenericParameter;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x0600086C RID: 2156 RVA: 0x00006397 File Offset: 0x00004597
		public override MetadataType MetadataType
		{
			get
			{
				return (MetadataType)this.etype;
			}
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x0001A334 File Offset: 0x00018534
		internal TypeSpecification(TypeReference type)
			: base(null, null)
		{
			this.element_type = type;
			this.token = new MetadataToken(TokenType.TypeSpec);
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x0001A355 File Offset: 0x00018555
		public override TypeReference GetElementType()
		{
			return this.element_type.GetElementType();
		}

		// Token: 0x040002FA RID: 762
		private readonly TypeReference element_type;
	}
}
