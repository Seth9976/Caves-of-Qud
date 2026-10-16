using System;

namespace XRL.World.Parts
{
	// Token: 0x02000F64 RID: 3940
	[Serializable]
	public class SizeAdjective : IPart
	{
		// Token: 0x06009CF7 RID: 40183 RVA: 0x0038DAC4 File Offset: 0x0038BCC4
		public override bool SameAs(IPart p)
		{
			SizeAdjective sizeAdjective = p as SizeAdjective;
			return !(sizeAdjective.Adjective != this.Adjective) && sizeAdjective.DescriptionPriority == this.DescriptionPriority && sizeAdjective.OrderAdjust == this.OrderAdjust && base.SameAs(p);
		}

		// Token: 0x06009CF8 RID: 40184 RVA: 0x0038DB14 File Offset: 0x0038BD14
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID;
		}

		// Token: 0x06009CF9 RID: 40185 RVA: 0x0038DB2A File Offset: 0x0038BD2A
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			E.ApplySizeAdjective(this.Adjective, this.DescriptionPriority, this.OrderAdjust);
			return base.HandleEvent(E);
		}

		// Token: 0x040040E4 RID: 16612
		public string Adjective;

		// Token: 0x040040E5 RID: 16613
		public int DescriptionPriority = 20;

		// Token: 0x040040E6 RID: 16614
		public int OrderAdjust;
	}
}
