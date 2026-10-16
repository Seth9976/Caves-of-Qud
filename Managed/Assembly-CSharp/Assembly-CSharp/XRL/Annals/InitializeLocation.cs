using System;
using HistoryKit;
using XRL.Names;

namespace XRL.Annals
{
	// Token: 0x020005EC RID: 1516
	[Serializable]
	public class InitializeLocation : HistoricEvent
	{
		// Token: 0x0600414F RID: 16719 RVA: 0x00148984 File Offset: 0x00146B84
		public InitializeLocation(string _region, int _period)
		{
			this.region = _region;
			this.period = _period;
		}

		// Token: 0x06004150 RID: 16720 RVA: 0x0014899C File Offset: 0x00146B9C
		public override void Generate()
		{
			base.SetEntityProperty("type", "location");
			base.SetEntityProperty("region", this.region);
			base.SetEntityProperty("period", this.period.ToString());
			string text;
			do
			{
				text = NameMaker.MakeName(null, null, null, null, "Qudish", null, null, null, null, null, null, "Site", null, false, false, null, null);
				if (50.in100())
				{
					int num = base.Random(0, 14);
					if (num == 0)
					{
						text += " Spire";
					}
					else if (num == 1)
					{
						text = "the Hamlet of " + text;
					}
					else if (num == 2)
					{
						text = "the Shrine at " + text;
					}
					else if (num == 3)
					{
						text += " Steeple";
					}
					else if (num == 4)
					{
						text += " Mesh";
					}
					else if (num == 5)
					{
						text = "New " + text;
					}
					else if (num == 6)
					{
						text = "Old " + text;
					}
					else if (num == 7)
					{
						text += " Grotto";
					}
					else if (num == 8)
					{
						text += " Cave";
					}
					else if (num == 9)
					{
						text += " Den";
					}
					else if (num == 10)
					{
						text += " Hollow";
					}
					else if (num == 11)
					{
						text += " Dune";
					}
					else if (num == 12)
					{
						text += " Tangle";
					}
					else if (num == 13)
					{
						text += " Morass";
					}
					else if (num == 14)
					{
						text += " Knot";
					}
				}
			}
			while (this.history.GetEntitiesWherePropertyEquals("name", text).Count > 0);
			base.SetEntityProperty("name", text);
			this.duration = 0L;
		}

		// Token: 0x04002005 RID: 8197
		private string region;

		// Token: 0x04002006 RID: 8198
		private int period;
	}
}
