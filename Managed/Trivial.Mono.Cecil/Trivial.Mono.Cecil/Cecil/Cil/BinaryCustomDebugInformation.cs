using System;

namespace Trivial.Mono.Cecil.Cil
{
	// Token: 0x020000F5 RID: 245
	public sealed class BinaryCustomDebugInformation : CustomDebugInformation
	{
		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x060009B4 RID: 2484 RVA: 0x00020599 File Offset: 0x0001E799
		// (set) Token: 0x060009B5 RID: 2485 RVA: 0x000205A1 File Offset: 0x0001E7A1
		public byte[] Data
		{
			get
			{
				return this.data;
			}
			set
			{
				this.data = value;
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x060009B6 RID: 2486 RVA: 0x000025EF File Offset: 0x000007EF
		public override CustomDebugInformationKind Kind
		{
			get
			{
				return CustomDebugInformationKind.Binary;
			}
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x000205AA File Offset: 0x0001E7AA
		public BinaryCustomDebugInformation(Guid identifier, byte[] data)
			: base(identifier)
		{
			this.data = data;
		}

		// Token: 0x04000422 RID: 1058
		private byte[] data;
	}
}
