using System;

namespace Mono.Cecil.Cil
{
	// Token: 0x020002E2 RID: 738
	public abstract class CustomDebugInformation : DebugInformation
	{
		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x060012BC RID: 4796 RVA: 0x0003CB7B File Offset: 0x0003AD7B
		public Guid Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x060012BD RID: 4797
		public abstract CustomDebugInformationKind Kind { get; }

		// Token: 0x060012BE RID: 4798 RVA: 0x0003CB83 File Offset: 0x0003AD83
		internal CustomDebugInformation(Guid identifier)
		{
			this.identifier = identifier;
			this.token = new MetadataToken(TokenType.CustomDebugInformation);
		}

		// Token: 0x0400098A RID: 2442
		private Guid identifier;
	}
}
