using System;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02000DE4 RID: 3556
	[Serializable]
	public class Nectar_Tonic_Applicator : IPart
	{
		// Token: 0x06009065 RID: 36965 RVA: 0x00341780 File Offset: 0x0033F980
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ApplyTonic");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009066 RID: 36966 RVA: 0x00341795 File Offset: 0x0033F995
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetUtilityScoreEvent.ID;
		}

		// Token: 0x06009067 RID: 36967 RVA: 0x003417AB File Offset: 0x0033F9AB
		public override bool HandleEvent(GetUtilityScoreEvent E)
		{
			if (E.Damage == null && (E.ForPermission || E.Actor.Health() < 0.1))
			{
				E.ApplyScore(1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009068 RID: 36968 RVA: 0x003417E4 File Offset: 0x0033F9E4
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ApplyTonic")
			{
				int intParameter = E.GetIntParameter("Dosage", 0);
				GameObject gameObjectParameter = E.GetGameObjectParameter("Subject");
				string text = "";
				if (intParameter <= 0 || (this.ParentObject != null && this.ParentObject.IsTemporary && !gameObjectParameter.IsTemporary))
				{
					text += "The experience is fleeting.";
				}
				else
				{
					gameObjectParameter.PermuteRandomMutationBuys();
					if (gameObjectParameter.IsTrueKin())
					{
						if (gameObjectParameter.HasStat("AP"))
						{
							gameObjectParameter.GetStat("AP").BaseValue += intParameter;
							text = text + "{{C|You gain " + intParameter.Things("attribute point", null) + "!}}";
						}
					}
					else if (50.in100())
					{
						int num = Stat.Random(1, 6);
						string text2 = "Strength";
						if (num == 1)
						{
							text2 = "Strength";
						}
						if (num == 2)
						{
							text2 = "Intelligence";
						}
						if (num == 3)
						{
							text2 = "Ego";
						}
						if (num == 4)
						{
							text2 = "Agility";
						}
						if (num == 5)
						{
							text2 = "Willpower";
						}
						if (num == 6)
						{
							text2 = "Toughness";
						}
						if (gameObjectParameter.HasStat(text2))
						{
							gameObjectParameter.GetStat(text2).BaseValue += intParameter;
							text = string.Concat(new string[]
							{
								text,
								"{{C|You gain ",
								intParameter.Things("point", null),
								" of ",
								text2,
								"!}}"
							});
						}
					}
					else if (gameObjectParameter.HasStat("MP"))
					{
						gameObjectParameter.GainMP(intParameter);
						text = text + "{{C|You gain " + intParameter.Things("mutation point", null) + "!}}";
					}
					if (gameObjectParameter.IsPlayer())
					{
						string text3 = "You taste life as it was distilled by the Eaters, Qud's primordial masons.";
						if (!text.IsNullOrEmpty())
						{
							text3 = text3 + "\n\n" + text;
						}
						Popup.Show(text3, null, "Sounds/UI/ui_notification", true, true, true, true, null);
						gameObjectParameter.GetPart<Leveler>().SifrahInsights();
					}
				}
			}
			return base.FireEvent(E);
		}
	}
}
