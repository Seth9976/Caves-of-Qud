using System;
using Qud.API;

namespace XRL.World.Parts
{
	// Token: 0x02000F8E RID: 3982
	[Serializable]
	public class StatOnEat : IPart
	{
		// Token: 0x06009E0A RID: 40458 RVA: 0x0039358F File Offset: 0x0039178F
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x06009E0B RID: 40459 RVA: 0x00393592 File Offset: 0x00391792
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("OnEat");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009E0C RID: 40460 RVA: 0x003935A8 File Offset: 0x003917A8
		public override bool FireEvent(Event E)
		{
			if (E.ID == "OnEat")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Eater");
				if (gameObjectParameter != null)
				{
					string[] array = this.Stats.Split(';', StringSplitOptions.None);
					for (int i = 0; i < array.Length; i++)
					{
						string[] array2 = array[i].Split(':', StringSplitOptions.None);
						int num = Convert.ToInt32(array2[1]);
						if (gameObjectParameter.HasStat(array2[0]))
						{
							gameObjectParameter.GetStat(array2[0]).BaseValue += num;
							if (gameObjectParameter.IsPlayer() && this.LogInChronology)
							{
								JournalAPI.AddAccomplishment("You ate " + this.ParentObject.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false) + ".", "=name= ate " + this.ParentObject.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false) + ". What can we infer from this?", string.Concat(new string[]
								{
									"While traveling through ",
									The.Player.CurrentZone.GetTerrainDisplayName(true),
									", =name= stopped at a tavern near ",
									JournalAPI.GetLandmarkNearestPlayer().Text,
									". There =name= ate ",
									this.ParentObject.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
									" and left the tavern with ",
									The.Player.GetPronounProvider(false).PossessiveAdjective,
									" perspective dramatically altered."
								}), null, "general", MuralCategory.BodyExperienceNeutral, MuralWeight.Medium, null, -1L, true);
							}
						}
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0400415D RID: 16733
		public string Stats = "";

		// Token: 0x0400415E RID: 16734
		public bool LogInChronology = true;
	}
}
