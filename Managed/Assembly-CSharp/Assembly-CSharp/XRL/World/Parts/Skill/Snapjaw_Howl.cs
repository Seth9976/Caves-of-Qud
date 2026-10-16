using System;
using System.Collections.Generic;
using System.Text;
using ConsoleLib.Console;
using XRL.Language;
using XRL.World.Effects;

namespace XRL.World.Parts.Skill
{
	// Token: 0x0200132B RID: 4907
	[Serializable]
	public class Snapjaw_Howl : BaseSkill
	{
		// Token: 0x0600BFDA RID: 49114 RVA: 0x00464438 File Offset: 0x00462638
		public void CollectStats(Templates.StatCollector stats)
		{
			stats.Set("Radius", this.Radius, false, 0);
			stats.Set("Duration", this.Duration, false, 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), Snapjaw_Howl.COOLDOWN, 0);
		}

		// Token: 0x0600BFDB RID: 49115 RVA: 0x00464484 File Offset: 0x00462684
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x0600BFDC RID: 49116 RVA: 0x004644A2 File Offset: 0x004626A2
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600BFDD RID: 49117 RVA: 0x004644C5 File Offset: 0x004626C5
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null))
			{
				E.Add("CommandSnapjawHowl", 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BFDE RID: 49118 RVA: 0x004644EE File Offset: 0x004626EE
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CommandSnapjawHowl");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BFDF RID: 49119 RVA: 0x00464504 File Offset: 0x00462704
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CommandSnapjawHowl")
			{
				if (!this.ParentObject.IsValid() || this.ParentObject.CurrentCell == null)
				{
					return false;
				}
				this.ParentObject.PlayWorldSound("Sounds/Abilities/sfx_ability_snapjaw_howl", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				base.DidX("howl", null, "!", null, null, this.ParentObject, null, false, false, null, null, false, false, false, false, false, null);
				List<GameObject> list = new List<GameObject>(16);
				using (List<Cell>.Enumerator enumerator = this.ParentObject.CurrentCell.GetLocalAdjacentCells(this.Radius, false).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						GameObject firstObjectWithPropertyOrTag = enumerator.Current.GetFirstObjectWithPropertyOrTag("Snapjaw", (GameObject obj) => !obj.HasEffect<Frenzied>());
						if (firstObjectWithPropertyOrTag != null)
						{
							bool flag = false;
							if (firstObjectWithPropertyOrTag.IsVisible())
							{
								if (firstObjectWithPropertyOrTag.IsPlayer())
								{
									IComponent<GameObject>.AddPlayerMessage("You are frenzied by the howl!", 'G', true);
								}
								else
								{
									flag = true;
								}
							}
							if (firstObjectWithPropertyOrTag.ApplyEffect(new Frenzied(this.Duration.RollCached(), 10, 10, 5, false, false, false), null) && flag)
							{
								list.Add(firstObjectWithPropertyOrTag);
							}
						}
					}
				}
				if (list.Count > 0)
				{
					int num = 0;
					int num2 = 0;
					GameObject gameObject = null;
					Dictionary<string, int> dictionary = new Dictionary<string, int>(list.Count);
					Dictionary<string, GameObject> dictionary2 = new Dictionary<string, GameObject>(list.Count);
					foreach (GameObject gameObject2 in list)
					{
						string shortDisplayName = gameObject2.ShortDisplayName;
						if (dictionary.ContainsKey(shortDisplayName))
						{
							Dictionary<string, int> dictionary3 = dictionary;
							string text = shortDisplayName;
							int num3 = dictionary3[text];
							dictionary3[text] = num3 + 1;
						}
						else
						{
							dictionary.Add(shortDisplayName, 1);
						}
						if (!dictionary2.ContainsKey(shortDisplayName))
						{
							dictionary2.Add(shortDisplayName, gameObject2);
						}
						if (gameObject == null)
						{
							gameObject = gameObject2;
						}
						num++;
						if (gameObject2.IsHostileTowards(IComponent<GameObject>.ThePlayer))
						{
							num2++;
						}
					}
					List<string> list2 = new List<string>(dictionary.Count);
					StringBuilder stringBuilder = Event.NewStringBuilder(null);
					foreach (string text2 in dictionary.Keys)
					{
						int num4 = dictionary[text2];
						stringBuilder.Length = 0;
						if (num4 > 1)
						{
							stringBuilder.Append(Grammar.Cardinal(num4)).Append(' ').Append(Grammar.Pluralize(text2));
						}
						else
						{
							stringBuilder.Append(dictionary2[text2].a).Append(text2);
						}
						list2.Add(stringBuilder.ToString());
					}
					string text3;
					if (num2 >= num / 2)
					{
						text3 = "&R";
					}
					else if (num2 > 0)
					{
						text3 = "&r";
					}
					else
					{
						text3 = "&g";
					}
					stringBuilder.Length = 0;
					stringBuilder.Append(text3).Append(ColorUtility.CapitalizeExceptFormatting(Grammar.MakeAndList(list2, true))).Append(text3)
						.Append((num == 1) ? gameObject.Is : " are")
						.Append(" frenzied by the howl!");
					IComponent<GameObject>.AddPlayerMessage(stringBuilder.ToString(), null, true);
				}
				this.ParentObject.Soundwave();
				this.ParentObject.UseEnergy(1000, "Skill Howl", null, null, false);
				base.CooldownMyActivatedAbility(this.ActivatedAbilityID, Snapjaw_Howl.COOLDOWN, null, null, false);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600BFE0 RID: 49120 RVA: 0x004648D0 File Offset: 0x00462AD0
		public override bool AddSkill(GameObject GO)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Howl", "CommandSnapjawHowl", "Skills", null, "\u000e", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.AddSkill(GO);
		}

		// Token: 0x0600BFE1 RID: 49121 RVA: 0x00464918 File Offset: 0x00462B18
		public override bool RemoveSkill(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.RemoveSkill(GO);
		}

		// Token: 0x04004D36 RID: 19766
		public int Radius = 5;

		// Token: 0x04004D37 RID: 19767
		public string Duration = "10-20";

		// Token: 0x04004D38 RID: 19768
		public Guid ActivatedAbilityID = Guid.Empty;

		// Token: 0x04004D39 RID: 19769
		public static readonly int COOLDOWN = 100;
	}
}
