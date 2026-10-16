using System;

namespace Mono.Cecil
{
	// Token: 0x02000254 RID: 596
	public abstract class ParameterReference : IMetadataTokenProvider
	{
		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000E52 RID: 3666 RVA: 0x0002D812 File Offset: 0x0002BA12
		// (set) Token: 0x06000E53 RID: 3667 RVA: 0x0002D81A File Offset: 0x0002BA1A
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

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000E54 RID: 3668 RVA: 0x0002D823 File Offset: 0x0002BA23
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000E55 RID: 3669 RVA: 0x0002D82B File Offset: 0x0002BA2B
		// (set) Token: 0x06000E56 RID: 3670 RVA: 0x0002D833 File Offset: 0x0002BA33
		public TypeReference ParameterType
		{
			get
			{
				return this.parameter_type;
			}
			set
			{
				this.parameter_type = value;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000E57 RID: 3671 RVA: 0x0002D83C File Offset: 0x0002BA3C
		// (set) Token: 0x06000E58 RID: 3672 RVA: 0x0002D844 File Offset: 0x0002BA44
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

		// Token: 0x06000E59 RID: 3673 RVA: 0x0002D84D File Offset: 0x0002BA4D
		internal ParameterReference(string name, TypeReference parameterType)
		{
			if (parameterType == null)
			{
				throw new ArgumentNullException("parameterType");
			}
			this.name = name ?? string.Empty;
			this.parameter_type = parameterType;
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x0002D812 File Offset: 0x0002BA12
		public override string ToString()
		{
			return this.name;
		}

		// Token: 0x06000E5B RID: 3675
		public abstract ParameterDefinition Resolve();

		// Token: 0x04000495 RID: 1173
		private string name;

		// Token: 0x04000496 RID: 1174
		internal int index = -1;

		// Token: 0x04000497 RID: 1175
		protected TypeReference parameter_type;

		// Token: 0x04000498 RID: 1176
		internal MetadataToken token;
	}
}
