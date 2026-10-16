using System;

namespace Mono.Cecil
{
	// Token: 0x0200022F RID: 559
	public abstract class MemberReference : IMetadataTokenProvider
	{
		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000C1D RID: 3101 RVA: 0x000299BC File Offset: 0x00027BBC
		// (set) Token: 0x06000C1E RID: 3102 RVA: 0x000299C4 File Offset: 0x00027BC4
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

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000C1F RID: 3103
		public abstract string FullName { get; }

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000C20 RID: 3104 RVA: 0x000299E9 File Offset: 0x00027BE9
		// (set) Token: 0x06000C21 RID: 3105 RVA: 0x000299F1 File Offset: 0x00027BF1
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

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000C22 RID: 3106 RVA: 0x000299FA File Offset: 0x00027BFA
		// (set) Token: 0x06000C23 RID: 3107 RVA: 0x00029A02 File Offset: 0x00027C02
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

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x00029A0B File Offset: 0x00027C0B
		public bool IsWindowsRuntimeProjection
		{
			get
			{
				return this.projection != null;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000C25 RID: 3109 RVA: 0x00029A18 File Offset: 0x00027C18
		internal bool HasImage
		{
			get
			{
				ModuleDefinition module = this.Module;
				return module != null && module.HasImage;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000C26 RID: 3110 RVA: 0x00029A37 File Offset: 0x00027C37
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

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000C27 RID: 3111 RVA: 0x00018318 File Offset: 0x00016518
		public virtual bool IsDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000C28 RID: 3112 RVA: 0x00029A4E File Offset: 0x00027C4E
		public virtual bool ContainsGenericParameter
		{
			get
			{
				return this.declaring_type != null && this.declaring_type.ContainsGenericParameter;
			}
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x00002AED File Offset: 0x00000CED
		internal MemberReference()
		{
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x00029A65 File Offset: 0x00027C65
		internal MemberReference(string name)
		{
			this.name = name ?? string.Empty;
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x00029A7D File Offset: 0x00027C7D
		internal string MemberFullName()
		{
			if (this.declaring_type == null)
			{
				return this.name;
			}
			return this.declaring_type.FullName + "::" + this.name;
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x00029AA9 File Offset: 0x00027CA9
		public IMemberDefinition Resolve()
		{
			return this.ResolveDefinition();
		}

		// Token: 0x06000C2D RID: 3117
		protected abstract IMemberDefinition ResolveDefinition();

		// Token: 0x06000C2E RID: 3118 RVA: 0x00029AB1 File Offset: 0x00027CB1
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x04000369 RID: 873
		private string name;

		// Token: 0x0400036A RID: 874
		private TypeReference declaring_type;

		// Token: 0x0400036B RID: 875
		internal MetadataToken token;

		// Token: 0x0400036C RID: 876
		internal object projection;
	}
}
