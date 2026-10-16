using System;
using System.Collections.Generic;
using System.Threading;
using ConsoleLib.Console;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.World.AI;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013E2 RID: 5090
	[Serializable]
	public class Burgeoning : BaseMutation
	{
		// Token: 0x0600C4A6 RID: 50342 RVA: 0x004791CD File Offset: 0x004773CD
		public Burgeoning()
		{
			base.Type = "Mental";
		}

		// Token: 0x0600C4A7 RID: 50343 RVA: 0x004791E0 File Offset: 0x004773E0
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID;
		}

		// Token: 0x0600C4A8 RID: 50344 RVA: 0x004791F8 File Offset: 0x004773F8
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (E.Distance <= 8 && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && E.Actor.HasLOSTo(E.Target, false, false, false, true, null))
			{
				E.Add("CommandBurgeoning", 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C4A9 RID: 50345 RVA: 0x0047924D File Offset: 0x0047744D
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CommandBurgeoning");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C4AA RID: 50346 RVA: 0x00479262 File Offset: 0x00477462
		public override string GetDescription()
		{
			return "You cause plants to spontaneously grow in a nearby area, hindering your enemies.";
		}

		// Token: 0x0600C4AB RID: 50347 RVA: 0x0047926C File Offset: 0x0047746C
		public override string GetLevelText(int Level)
		{
			int num = 115 - 10 * Level;
			if (num < 5)
			{
				num = 5;
			}
			string text = "";
			text += "Range: 8\n";
			text += "Area: 3x3 + growth into adjacent tiles\n";
			text = text + "Cooldown: {{rules|" + num.ToString() + "}} rounds\n";
			if (Level != base.Level)
			{
				text += "More powerful plants summoned\n";
			}
			return text + "+200 reputation with {{w|the Consortium of Phyta}}";
		}

		// Token: 0x0600C4AC RID: 50348 RVA: 0x004792E0 File Offset: 0x004774E0
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			stats.Set("Area", "3x3", false, 0);
			stats.Set("Range", "8", false, 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), this.GetCooldown(Level), 0);
		}

		// Token: 0x0600C4AD RID: 50349 RVA: 0x0047932C File Offset: 0x0047752C
		public static string GetRandomRainbowColor()
		{
			return Burgeoning.ColorList.GetRandomElement<string>(null);
		}

		// Token: 0x0600C4AE RID: 50350 RVA: 0x0047933C File Offset: 0x0047753C
		public static void GrowPlant(Cell C, bool bFriendly, GameObject Owner, int Level, bool QudzuOnly, string primePlant = null)
		{
			if (C.ParentZone.IsActive() && C.IsVisible())
			{
				TextConsole.LoadScrapBuffers();
				ScreenBuffer scrapBuffer = TextConsole.ScrapBuffer;
				XRLCore.Core.RenderMapToBuffer(TextConsole.ScrapBuffer);
				scrapBuffer.Goto(C.X, C.Y);
				scrapBuffer.Write(Burgeoning.GetRandomRainbowColor() + "\a", true, false, false, null, -1);
				Popup._TextConsole.DrawBuffer(scrapBuffer, null, false);
				Thread.Sleep(50);
				XRLCore.Core.RenderMapToBuffer(scrapBuffer);
				scrapBuffer.Goto(C.X, C.Y);
				scrapBuffer.Write(Burgeoning.GetRandomRainbowColor() + "\u000f", true, false, false, null, -1);
				Popup._TextConsole.DrawBuffer(scrapBuffer, null, false);
				Thread.Sleep(25);
				if (Stat.Random(0, 1) == 0)
				{
					XRLCore.ParticleManager.AddSinusoidal(Burgeoning.GetRandomRainbowColor() + "\r", (float)C.X, (float)C.Y, 1.5f * (float)Stat.Random(1, 6), 0.1f * (float)Stat.Random(1, 60), 0.1f + 0.025f * (float)Stat.Random(0, 4), 1f, 0f, 0f, -0.1f - 0.05f * (float)Stat.Random(1, 6), 999, 0L);
				}
				else
				{
					XRLCore.ParticleManager.AddSinusoidal(Burgeoning.GetRandomRainbowColor() + "\u000e", (float)C.X, (float)C.Y, 1.5f * (float)Stat.Random(1, 6), 0.1f * (float)Stat.Random(1, 60), 0.1f + 0.025f * (float)Stat.Random(0, 4), 1f, 0f, 0f, -0.1f - 0.05f * (float)Stat.Random(1, 6), 999, 0L);
				}
			}
			GameObject gameObject = ((primePlant == null) ? GameObject.Create(Burgeoning.GetPlantByTier(Level, QudzuOnly)) : GameObject.Create(primePlant));
			Brain brain = gameObject.Brain;
			gameObject.IsTrifling = true;
			if (brain != null)
			{
				if (bFriendly)
				{
					brain.SetAlliedLeader<AllySummon>(Owner, 0, true);
				}
				else
				{
					brain.Allegiance.Add("GerminatedPlants", 100);
				}
				gameObject.MakeActive(false);
				if (gameObject.HasStat("XPValue"))
				{
					gameObject.GetStat("XPValue").BaseValue = 0;
				}
			}
			C.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			C.PlayWorldSound("sfx_creature_appear_summon", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			gameObject.PlayWorldSoundTag("AmbientIdleSound", null, null, 0.5f);
		}

		// Token: 0x0600C4AF RID: 50351 RVA: 0x004795E8 File Offset: 0x004777E8
		public static void PlantSummoning(List<Cell> Cells, bool bFriendly, GameObject Owner, int Level, bool bQudzuOnly)
		{
			List<Cell> list = new List<Cell>(32);
			string plantByTier = Burgeoning.GetPlantByTier(Level, false);
			if (Owner != null)
			{
				Owner.PlayWorldSound("Sounds/Abilities/sfx_ability_mutation_burgeoning_plantGrow", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			foreach (Cell cell in Cells)
			{
				if (!cell.IsOccluding())
				{
					if (50.in100())
					{
						Burgeoning.GrowPlant(cell, bFriendly, Owner, Level, bQudzuOnly, plantByTier);
					}
					else
					{
						Burgeoning.GrowPlant(cell, bFriendly, Owner, Level, bQudzuOnly, null);
					}
				}
				foreach (Cell cell2 in cell.GetAdjacentCells(true))
				{
					if (!list.CleanContains(cell2) && !Cells.CleanContains(cell2))
					{
						list.Add(cell2);
					}
				}
			}
			foreach (Cell cell3 in list)
			{
				if (!cell3.IsOccluding() && 20.in100())
				{
					if (50.in100())
					{
						Burgeoning.GrowPlant(cell3, bFriendly, Owner, Level, bQudzuOnly, plantByTier);
					}
					else
					{
						Burgeoning.GrowPlant(cell3, bFriendly, Owner, Level, bQudzuOnly, null);
					}
				}
			}
		}

		// Token: 0x0600C4B0 RID: 50352 RVA: 0x00479768 File Offset: 0x00477968
		public static string GetPlantByTier(int Level, bool bQudzuOnly = false)
		{
			int num = (int)(Math.Ceiling((double)((float)Level / 2f)) + (double)Stat.Random(-1, 2));
			if (num < 1)
			{
				num = 1;
			}
			if (num > 9)
			{
				num = 9;
			}
			GameObject gameObject;
			if (bQudzuOnly)
			{
				gameObject = GameObject.Create("Qudzu");
			}
			else
			{
				gameObject = GameObject.Create(PopulationManager.RollOneFrom("PlantSummoning" + num.ToString(), null, null).Blueprint);
			}
			return gameObject.Blueprint;
		}

		// Token: 0x0600C4B1 RID: 50353 RVA: 0x004797D8 File Offset: 0x004779D8
		public bool Burgeon()
		{
			List<Cell> list = base.PickBurst(1, 8, false, AllowVis.OnlyVisible, "Burgeoning");
			if (list == null)
			{
				return false;
			}
			using (List<Cell>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.DistanceTo(this.ParentObject) > 9)
					{
						if (this.ParentObject.IsPlayer())
						{
							Popup.ShowFail("That is out of range! (8 squares)", true, true, true);
						}
						return false;
					}
				}
			}
			base.UseEnergy(1000, "Mental Mutation Burgeoning");
			Burgeoning.PlantSummoning(list, true, this.ParentObject, base.Level, this.QudzuOnly);
			int cooldown = this.GetCooldown(base.Level);
			base.CooldownMyActivatedAbility(this.ActivatedAbilityID, cooldown, null, null, false);
			base.DidX("impel", "plants to burgeon from the ground", "!", null, null, this.ParentObject, null, false, false, null, null, false, false, false, false, false, null);
			return true;
		}

		// Token: 0x0600C4B2 RID: 50354 RVA: 0x004798D8 File Offset: 0x00477AD8
		public int GetCooldown(int Level)
		{
			return Math.Max(115 - 10 * Level, 5);
		}

		// Token: 0x0600C4B3 RID: 50355 RVA: 0x004798E7 File Offset: 0x00477AE7
		public override bool FireEvent(Event E)
		{
			return (!(E.ID == "CommandBurgeoning") || this.Burgeon()) && base.FireEvent(E);
		}

		// Token: 0x0600C4B4 RID: 50356 RVA: 0x0047990C File Offset: 0x00477B0C
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C4B5 RID: 50357 RVA: 0x00479918 File Offset: 0x00477B18
		public override bool Mutate(GameObject GO, int Level)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Burgeoning", "CommandBurgeoning", "Mental Mutations", null, "\r", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C4B6 RID: 50358 RVA: 0x00479961 File Offset: 0x00477B61
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.Unmutate(GO);
		}

		// Token: 0x04004DDA RID: 19930
		public const int PRIME_PLANT_CHANCE = 50;

		// Token: 0x04004DDB RID: 19931
		public bool QudzuOnly;

		// Token: 0x04004DDC RID: 19932
		[NonSerialized]
		public static string[] ColorList = new string[] { "&R", "&G", "&B", "&M", "&Y", "&W" };
	}
}
