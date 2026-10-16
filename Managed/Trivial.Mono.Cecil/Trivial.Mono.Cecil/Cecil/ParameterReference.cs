using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x0200009B RID: 155
	public abstract class ParameterReference : IMetadataTokenProvider
	{
		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x000147C6 File Offset: 0x000129C6
		// (set) Token: 0x0600067C RID: 1660 RVA: 0x000147CE File Offset: 0x000129CE
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

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x0600067D RID: 1661 RVA: 0x000147D7 File Offset: 0x000129D7
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x000147DF File Offset: 0x000129DF
		// (set) Token: 0x0600067F RID: 1663 RVA: 0x000147E7 File Offset: 0x000129E7
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

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x000147F0 File Offset: 0x000129F0
		// (set) Token: 0x06000681 RID: 1665 RVA: 0x000147F8 File Offset: 0x000129F8
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

		// Token: 0x06000682 RID: 1666 RVA: 0x00014801 File Offset: 0x00012A01
		internal ParameterReference(string name, TypeReference parameterType)
		{
			if (parameterType == null)
			{
				throw new ArgumentNullException("parameterType");
			}
			this.name = name ?? string.Empty;
			this.parameter_type = parameterType;
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x000147C6 File Offset: 0x000129C6
		public override string ToString()
		{
			return this.name;
		}

		// Token: 0x06000684 RID: 1668
		public abstract ParameterDefinition Resolve();

		// Token: 0x04000217 RID: 535
		private string name;

		// Token: 0x04000218 RID: 536
		internal int index = -1;

		// Token: 0x04000219 RID: 537
		protected TypeReference parameter_type;

		// Token: 0x0400021A RID: 538
		internal MetadataToken token;
	}
}
