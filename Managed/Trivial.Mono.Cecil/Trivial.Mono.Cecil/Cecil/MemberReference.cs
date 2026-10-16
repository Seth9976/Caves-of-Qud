using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x0200006C RID: 108
	public abstract class MemberReference : IMetadataTokenProvider
	{
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x0000773B File Offset: 0x0000593B
		// (set) Token: 0x060002EA RID: 746 RVA: 0x00007743 File Offset: 0x00005943
		public virtual string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				if (this.IsWindowsRuntimeProjection && value != this.name)
				{
					throw new InvalidOperationException();
				}
				this.name = value;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060002EB RID: 747
		public abstract string FullName { get; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060002EC RID: 748 RVA: 0x00007768 File Offset: 0x00005968
		// (set) Token: 0x060002ED RID: 749 RVA: 0x00007770 File Offset: 0x00005970
		public virtual TypeReference DeclaringType
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

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060002EE RID: 750 RVA: 0x00007779 File Offset: 0x00005979
		// (set) Token: 0x060002EF RID: 751 RVA: 0x00007781 File Offset: 0x00005981
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

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000778A File Offset: 0x0000598A
		public bool IsWindowsRuntimeProjection
		{
			get
			{
				return this.projection != null;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x00007795 File Offset: 0x00005995
		internal bool HasImage
		{
			get
			{
				ModuleDefinition module = this.Module;
				return module != null && module.HasImage;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x000077A8 File Offset: 0x000059A8
		public virtual ModuleDefinition Module
		{
			get
			{
				if (this.declaring_type == null)
				{
					return null;
				}
				return this.declaring_type.Module;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x000025EF File Offset: 0x000007EF
		public virtual bool IsDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x000077BF File Offset: 0x000059BF
		public virtual bool ContainsGenericParameter
		{
			get
			{
				return this.declaring_type != null && this.declaring_type.ContainsGenericParameter;
			}
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00002F5F File Offset: 0x0000115F
		internal MemberReference()
		{
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x000077D6 File Offset: 0x000059D6
		internal MemberReference(string name)
		{
			this.name = name ?? string.Empty;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x000077EE File Offset: 0x000059EE
		internal string MemberFullName()
		{
			if (this.declaring_type == null)
			{
				return this.name;
			}
			return this.declaring_type.FullName + "::" + this.name;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000781A File Offset: 0x00005A1A
		public IMemberDefinition Resolve()
		{
			return this.ResolveDefinition();
		}

		// Token: 0x060002F9 RID: 761
		protected abstract IMemberDefinition ResolveDefinition();

		// Token: 0x060002FA RID: 762 RVA: 0x00007822 File Offset: 0x00005A22
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x040000AF RID: 175
		private string name;

		// Token: 0x040000B0 RID: 176
		private TypeReference declaring_type;

		// Token: 0x040000B1 RID: 177
		internal MetadataToken token;

		// Token: 0x040000B2 RID: 178
		internal object projection;
	}
}
