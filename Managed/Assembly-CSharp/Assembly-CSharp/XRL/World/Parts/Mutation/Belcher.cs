using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using ConsoleLib.Console;
using XRL.Language;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001407 RID: 5127
	[Serializable]
	public class Belcher : BaseMutation
	{
		// Token: 0x0600C714 RID: 50964 RVA: 0x004890FC File Offset: 0x004872FC
		public Belcher()
		{
			base.Type = "Physical";
		}

		// Token: 0x0600C715 RID: 50965 RVA: 0x00489137 File Offset: 0x00487337
		public int GetMaxDistance()
		{
			return base.Level * 5;
		}

		// Token: 0x0600C716 RID: 50966 RVA: 0x00489141 File Offset: 0x00487341
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID;
		}

		// Token: 0x0600C717 RID: 50967 RVA: 0x00489158 File Offset: 0x00487358
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (this.puffs < this.MaxPuffs && E.Distance <= this.GetRange(base.Level) && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null))
			{
				E.Add(this.EventKey, 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C718 RID: 50968 RVA: 0x004891AF File Offset: 0x004873AF
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register(this.EventKey);
			Registrar.Register("BeforeDie");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C719 RID: 50969 RVA: 0x004891D0 File Offset: 0x004873D0
		public override bool FireEvent(Event E)
		{
			if (E.ID == this.EventKey)
			{
				this.puffs++;
				if (!Belcher.Cast(this, base.Level.ToString(), true, false))
				{
					return false;
				}
			}
			else if (E.ID == "BeforeDie")
			{
				Belcher.Cast(this, base.Level.ToString(), true, true);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C71A RID: 50970 RVA: 0x00489248 File Offset: 0x00487448
		public virtual int GetRange(int Level)
		{
			return 3 + Level;
		}

		// Token: 0x0600C71B RID: 50971 RVA: 0x0048924D File Offset: 0x0048744D
		public virtual int GetRadius()
		{
			return 4;
		}

		// Token: 0x0600C71C RID: 50972 RVA: 0x00489250 File Offset: 0x00487450
		public virtual int GetNumber(int Level)
		{
			return Stat.Roll(1, 2) + Level / 4;
		}

		// Token: 0x0600C71D RID: 50973 RVA: 0x0048925D File Offset: 0x0048745D
		public virtual int GetCooldown(int Level)
		{
			return 210 - 10 * Level;
		}

		// Token: 0x0600C71E RID: 50974 RVA: 0x00489269 File Offset: 0x00487469
		public virtual string GetBelchObject()
		{
			return PopulationManager.RollOneFrom(this.BelchTable, null, null).Blueprint;
		}

		// Token: 0x0600C71F RID: 50975 RVA: 0x0048927D File Offset: 0x0048747D
		public override string GetDescription()
		{
			return this.Description;
		}

		// Token: 0x0600C720 RID: 50976 RVA: 0x00489285 File Offset: 0x00487485
		public override string GetLevelText(int Level)
		{
			return this.Description;
		}

		// Token: 0x0600C721 RID: 50977 RVA: 0x00489290 File Offset: 0x00487490
		public static bool Cast(Belcher mutation = null, string level = "5-6", bool doEffect = true, bool self = false)
		{
			if (mutation == null)
			{
				mutation = new Belcher();
				mutation.Level = level.RollCached();
				mutation.ParentObject = IComponent<GameObject>.ThePlayer;
			}
			GameObject who = mutation.ParentObject;
			if (!GameObject.Validate(who))
			{
				return false;
			}
			Cell currentCell = who.CurrentCell;
			if (currentCell == null)
			{
				return false;
			}
			List<Cell> list;
			if (self)
			{
				list = currentCell.GetLocalAdjacentCells(mutation.GetRadius(), false);
			}
			else
			{
				list = mutation.PickBurst(mutation.GetRadius(), mutation.GetRange(mutation.Level), false, AllowVis.OnlyVisible, ((mutation != null) ? mutation.CommandName : null) ?? "Belch");
				if (list == null)
				{
					return false;
				}
				using (List<Cell>.Enumerator enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.DistanceTo(who) > mutation.GetRadius() + mutation.GetRange(mutation.Level))
						{
							if (who.IsPlayer())
							{
								Popup.Show("That is out of range! (" + mutation.GetRange(mutation.Level).ToString() + " squares)", null, "Sounds/UI/ui_notification", true, true, true, true, null);
							}
							return false;
						}
					}
				}
			}
			list = list.FindAll((Cell c) => c.IsEmpty() && who.HasLOSTo(c, true, false, false, true, null));
			if (list == null)
			{
				return false;
			}
			if (list.Count <= 0)
			{
				return false;
			}
			list.ShuffleInPlace<Cell>(null);
			mutation.UseEnergy(1000, "Physical Mutation Belch Urchin");
			mutation.CooldownMyActivatedAbility(mutation.ActivatedAbilityID, mutation.GetCooldown(mutation.Level), null, null, false);
			int num = Math.Min(mutation.GetNumber(mutation.Level), list.Count);
			List<GameObject> list2 = Event.NewGameObjectList();
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = GameObject.Create(mutation.GetBelchObject());
				list[i].AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				gameObject.MakeActive(false);
				if (!string.IsNullOrEmpty(mutation.SpawnEvent))
				{
					gameObject.FireEvent(mutation.SpawnEvent);
				}
				list2.Add(gameObject);
			}
			if (IComponent<GameObject>.Visible(who))
			{
				if (list2.Count > 0 && !string.IsNullOrEmpty(mutation.BelchVerb))
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(list2.Count);
					Dictionary<string, GameObject> dictionary2 = new Dictionary<string, GameObject>(list2.Count);
					foreach (GameObject gameObject2 in list2)
					{
						string shortDisplayName = gameObject2.ShortDisplayName;
						if (dictionary.ContainsKey(shortDisplayName))
						{
							Dictionary<string, int> dictionary3 = dictionary;
							string text = shortDisplayName;
							int num2 = dictionary3[text];
							dictionary3[text] = num2 + 1;
						}
						else
						{
							dictionary.Add(shortDisplayName, 1);
						}
						if (!dictionary2.ContainsKey(shortDisplayName))
						{
							dictionary2.Add(shortDisplayName, gameObject2);
						}
					}
					List<string> list3 = new List<string>(dictionary.Count);
					StringBuilder stringBuilder = Event.NewStringBuilder(null);
					foreach (string text2 in dictionary.Keys)
					{
						stringBuilder.Clear();
						int num3 = dictionary[text2];
						if (num3 == 1)
						{
							stringBuilder.Append(dictionary2[text2].a).Append(text2);
							list3.Add(stringBuilder.ToString());
						}
						else
						{
							stringBuilder.Append(Grammar.Cardinal(num3)).Append(' ').Append(Grammar.Pluralize(text2));
							list3.Add(stringBuilder.ToString());
						}
					}
					if (who.IsPlayer())
					{
						stringBuilder.Clear().Append("You").Compound(mutation.BelchAdverb)
							.Append(who.GetVerb(mutation.BelchVerb, true, false, false, true, false))
							.Compound(mutation.BelchPreposition)
							.Compound(Grammar.MakeAndList(list3, true))
							.Append(who.IsHostileTowards(IComponent<GameObject>.ThePlayer) ? '!' : '.');
					}
					else
					{
						stringBuilder.Clear().Append(who.The).Append(who.ShortDisplayName)
							.Compound(mutation.BelchAdverb)
							.Append(who.GetVerb(mutation.BelchVerb, true, false, false, true, false))
							.Compound(mutation.BelchPreposition)
							.Compound(Grammar.MakeAndList(list3, true))
							.Append(who.IsHostileTowards(IComponent<GameObject>.ThePlayer) ? '!' : '.');
					}
					IComponent<GameObject>.AddPlayerMessage(stringBuilder.ToString(), null, true);
				}
				if (doEffect)
				{
					ScreenBuffer scrapBuffer = ScreenBuffer.GetScrapBuffer1(false);
					int num4 = 0;
					List<List<Tuple<Cell, char>>> list4 = new List<List<Tuple<Cell, char>>>();
					foreach (GameObject gameObject3 in list2)
					{
						List<Tuple<Cell, char>> lineTo = who.GetLineTo(gameObject3, true, false);
						if (lineTo.Count > num4)
						{
							num4 = lineTo.Count;
						}
						list4.Add(lineTo);
					}
					for (int j = 0; j < num4; j++)
					{
						scrapBuffer.RenderBase();
						foreach (List<Tuple<Cell, char>> list5 in list4)
						{
							int num5 = num4 - list5.Count;
							int num6 = j - num5;
							if (num6 >= 0 && num6 < list5.Count)
							{
								scrapBuffer.Goto(list5[num6].Item1.X, list5[num6].Item1.Y);
								scrapBuffer.Write("&G*", true, false, false, null, -1);
								int num7 = num6 - 1;
								while (num7 >= num6 - 2 && num7 >= 0 && num7 < list5.Count)
								{
									scrapBuffer.Goto(list5[num7].Item1.X, list5[num7].Item1.Y);
									scrapBuffer.Write("&g.", true, false, false, null, -1);
									num7++;
								}
							}
						}
						scrapBuffer.Draw();
						Thread.Sleep(10);
					}
				}
			}
			return true;
		}

		// Token: 0x0600C722 RID: 50978 RVA: 0x0048992C File Offset: 0x00487B2C
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C723 RID: 50979 RVA: 0x00489938 File Offset: 0x00487B38
		public override bool Mutate(GameObject GO, int Level)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility(this.CommandName, this.EventKey, "Physical Mutations", null, "*", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C724 RID: 50980 RVA: 0x00489983 File Offset: 0x00487B83
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.Unmutate(GO);
		}

		// Token: 0x04004E4E RID: 20046
		public string EventKey;

		// Token: 0x04004E4F RID: 20047
		public string Description;

		// Token: 0x04004E50 RID: 20048
		public string BelchTable;

		// Token: 0x04004E51 RID: 20049
		public string CommandName;

		// Token: 0x04004E52 RID: 20050
		public string CommandDescription;

		// Token: 0x04004E53 RID: 20051
		public string SpawnEvent = "PuffPlease";

		// Token: 0x04004E54 RID: 20052
		public string BelchVerb = "belch";

		// Token: 0x04004E55 RID: 20053
		public string BelchPreposition = "forth";

		// Token: 0x04004E56 RID: 20054
		public string BelchAdverb;

		// Token: 0x04004E57 RID: 20055
		public int puffs;

		// Token: 0x04004E58 RID: 20056
		public int MaxPuffs = 6;
	}
}
