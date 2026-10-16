using System;

namespace Mono.Cecil
{
	// Token: 0x020001B7 RID: 439
	public sealed class AssemblyNameDefinition : AssemblyNameReference
	{
		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060007D3 RID: 2003 RVA: 0x0001AEA1 File Offset: 0x000190A1
		public override byte[] Hash
		{
			get
			{
				return Empty<byte>.Array;
			}
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x0001AEA8 File Offset: 0x000190A8
		internal AssemblyNameDefinition()
		{
			this.token = new MetadataToken(TokenType.Assembly, 1);
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0001AEC1 File Offset: 0x000190C1
		public AssemblyNameDefinition(string name, Version version)
			: base(name, version)
		{
			this.token = new MetadataToken(TokenType.Assembly, 1);
		}
	}
}
