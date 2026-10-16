using System;

namespace Trivial.Mono.Cecil.Cil
{
	// Token: 0x020000FA RID: 250
	public abstract class CustomDebugInformation : DebugInformation
	{
		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000A12 RID: 2578 RVA: 0x0002242B File Offset: 0x0002062B
		public Guid Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000A13 RID: 2579
		public abstract CustomDebugInformationKind Kind { get; }

		// Token: 0x06000A14 RID: 2580 RVA: 0x00022433 File Offset: 0x00020633
		internal CustomDebugInformation(Guid identifier)
		{
			this.identifier = identifier;
			this.token = new MetadataToken(TokenType.CustomDebugInformation);
		}

		// Token: 0x0400050B RID: 1291
		private Guid identifier;
	}
}
