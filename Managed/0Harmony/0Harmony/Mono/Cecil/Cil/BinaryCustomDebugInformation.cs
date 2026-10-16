using System;

namespace Mono.Cecil.Cil
{
	// Token: 0x020002E3 RID: 739
	internal sealed class BinaryCustomDebugInformation : CustomDebugInformation
	{
		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x060012BF RID: 4799 RVA: 0x0003CBA2 File Offset: 0x0003ADA2
		// (set) Token: 0x060012C0 RID: 4800 RVA: 0x0003CBAA File Offset: 0x0003ADAA
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

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x060012C1 RID: 4801 RVA: 0x00018318 File Offset: 0x00016518
		public override CustomDebugInformationKind Kind
		{
			get
			{
				return CustomDebugInformationKind.Binary;
			}
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x0003CBB3 File Offset: 0x0003ADB3
		public BinaryCustomDebugInformation(Guid identifier, byte[] data)
			: base(identifier)
		{
			this.data = data;
		}

		// Token: 0x0400098B RID: 2443
		private byte[] data;
	}
}
