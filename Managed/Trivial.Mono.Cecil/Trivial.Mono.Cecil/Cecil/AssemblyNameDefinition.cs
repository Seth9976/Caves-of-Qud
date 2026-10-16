using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000012 RID: 18
	public sealed class AssemblyNameDefinition : AssemblyNameReference
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000030FE File Offset: 0x000012FE
		public override byte[] Hash
		{
			get
			{
				return Empty<byte>.Array;
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003105 File Offset: 0x00001305
		internal AssemblyNameDefinition()
		{
			this.token = new MetadataToken(TokenType.Assembly, 1);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000311E File Offset: 0x0000131E
		public AssemblyNameDefinition(string name, Version version)
			: base(name, version)
		{
			this.token = new MetadataToken(TokenType.Assembly, 1);
		}
	}
}
