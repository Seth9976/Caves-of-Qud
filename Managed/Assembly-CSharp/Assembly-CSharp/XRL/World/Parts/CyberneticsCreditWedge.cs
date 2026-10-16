using System;

namespace XRL.World.Parts
{
	// Token: 0x0200100F RID: 4111
	[Serializable]
	public class CyberneticsCreditWedge : IPart
	{
		// Token: 0x0600A3C0 RID: 41920 RVA: 0x003BD2DD File Offset: 0x003BB4DD
		public override bool SameAs(IPart p)
		{
			return (p as CyberneticsCreditWedge).Credits == this.Credits && base.SameAs(p);
		}

		// Token: 0x0600A3C1 RID: 41921 RVA: 0x003BD2FB File Offset: 0x003BB4FB
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600A3C2 RID: 41922 RVA: 0x003BD319 File Offset: 0x003BB519
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			E.AddBase("{{C|" + this.Credits.ToString() + "\u009b}}", 20, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A3C3 RID: 41923 RVA: 0x003BD345 File Offset: 0x003BB545
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.Append("\nCredits remaining: {{C|").Append(this.Credits).Append('\u009b')
				.Append("}}");
			return base.HandleEvent(E);
		}

		// Token: 0x040043CC RID: 17356
		public int Credits;
	}
}
