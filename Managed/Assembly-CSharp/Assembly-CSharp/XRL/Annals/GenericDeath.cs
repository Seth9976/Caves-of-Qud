using System;
using HistoryKit;

namespace XRL.Annals
{
	// Token: 0x02000603 RID: 1539
	[Serializable]
	public class GenericDeath : HistoricEvent
	{
		// Token: 0x06004185 RID: 16773 RVA: 0x0014D060 File Offset: 0x0014B260
		public override void Generate()
		{
			this.duration = 0L;
			string text = QudHistoryHelpers.ListAllCognomen(this.entity.GetSnapshotAtYear(this.entity.lastYear));
			string text2 = string.Concat(new string[]
			{
				"In %",
				this.year.ToString(),
				"%, <entity.name>",
				text,
				" died of natural causes. <entity.subjectPronoun.capitalize> was ",
				((int)(this.year - this.entity.firstYear)).ToString(),
				" years old."
			});
			string text3 = string.Format("In the {0}, {1}{2} laid {3} body to rest and crossed into Brightsheol. {4} was {5} years old.", new object[]
			{
				QudHistoryHelpers.GenerateSultanateYearName(),
				"<entity.name>",
				text,
				"<entity.possessivePronoun>",
				"<entity.subjectPronoun.capitalize>",
				(int)(this.year - this.entity.firstYear)
			});
			base.SetEventProperty("gospel", text2);
			base.SetEventProperty("tombInscription", text3);
			base.SetEventProperty("tombInscriptionCategory", "Dies");
		}
	}
}
