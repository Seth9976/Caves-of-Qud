using System;
using XRL.Language;

namespace XRL.World.Parts
{
	// Token: 0x02001009 RID: 4105
	[Serializable]
	public class CompanionCapacity : IActivePart
	{
		// Token: 0x0600A390 RID: 41872 RVA: 0x003BC459 File Offset: 0x003BA659
		public CompanionCapacity()
		{
			this.WorksOnHolder = true;
			this.WorksOnCarrier = true;
		}

		// Token: 0x0600A391 RID: 41873 RVA: 0x003BC46F File Offset: 0x003BA66F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetShortDescriptionEvent.ID || ID == PooledEvent<GetCompanionLimitEvent>.ID;
		}

		// Token: 0x0600A392 RID: 41874 RVA: 0x003BC490 File Offset: 0x003BA690
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.Proselytized != 0)
			{
				string text = ((Math.Abs(this.Proselytized) == 1) ? "follower" : "followers");
				string text2 = ((this.Proselytized < 0) ? "fewer" : "additional");
				E.Postfix.Compound("{{rules|", '\n').Append(string.Concat(new string[]
				{
					"You may Proselytize ",
					Grammar.Cardinal(Math.Abs(this.Proselytized)),
					" ",
					text2,
					" ",
					text,
					".}}"
				}));
			}
			if (this.Beguiled > 0)
			{
				string text3 = ((Math.Abs(this.Beguiled) == 1) ? "follower" : "followers");
				string text4 = ((this.Beguiled < 0) ? "fewer" : "additional");
				E.Postfix.Compound("{{rules|", '\n').Append(string.Concat(new string[]
				{
					"You may Beguile ",
					Grammar.Cardinal(Math.Abs(this.Beguiled)),
					" ",
					text4,
					" ",
					text3,
					".}}"
				}));
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A393 RID: 41875 RVA: 0x003BC5DC File Offset: 0x003BA7DC
		public override bool HandleEvent(GetCompanionLimitEvent E)
		{
			if (E.Means == "Beguiling")
			{
				if (this.Beguiled != 0 && this.IsObjectActivePartSubject(E.Actor) && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
				{
					E.Limit += this.Beguiled;
				}
			}
			else if (E.Means == "Proselytize" && this.Proselytized != 0 && this.IsObjectActivePartSubject(E.Actor) && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				E.Limit += this.Proselytized;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x040043C2 RID: 17346
		public int Proselytized;

		// Token: 0x040043C3 RID: 17347
		public int Beguiled;
	}
}
