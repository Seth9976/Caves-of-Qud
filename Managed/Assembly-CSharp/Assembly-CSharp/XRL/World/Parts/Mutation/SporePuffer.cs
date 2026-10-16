using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using Wintellect.PowerCollections;
using XRL.Rules;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001425 RID: 5157
	[Serializable]
	public class SporePuffer : BaseMutation
	{
		// Token: 0x0600C8EE RID: 51438 RVA: 0x00493304 File Offset: 0x00491504
		public override string GetLevelText(int Level)
		{
			return "You puff with the best of them.\n";
		}

		// Token: 0x0600C8EF RID: 51439 RVA: 0x0049330B File Offset: 0x0049150B
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == GetAdjacentNavigationWeightEvent.ID;
		}

		// Token: 0x0600C8F0 RID: 51440 RVA: 0x0049332C File Offset: 0x0049152C
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			if (this.PuffObject.Length == 1)
			{
				Stat.ReseedFrom("PufferType", false);
				string[] array = Algorithms.RandomShuffle<string>(SporePuffer.InfectionList);
				this.PuffObject = array[Convert.ToInt32(this.PuffObject)];
			}
			if (!this.ParentObject.IsPlayer())
			{
				base.UseEnergy(this.EnergyCost, "Fungus Puff");
			}
			this.nCooldown--;
			if (this.nCooldown <= 0 && this.Chance > 0 && (this.Chance >= 100 || Stat.Random(1, 100) < this.Chance))
			{
				bool flag = false;
				List<Cell> localAdjacentCells = this.ParentObject.Physics.CurrentCell.GetLocalAdjacentCells();
				if (localAdjacentCells != null)
				{
					foreach (Cell cell in localAdjacentCells)
					{
						if (cell.HasObjectWithPart("Brain"))
						{
							foreach (GameObject gameObject in cell.GetObjectsWithPart("Brain"))
							{
								if (this.ParentObject.Brain == null || !this.ParentObject.Brain.IsAlliedTowards(gameObject))
								{
									flag = true;
									break;
								}
							}
						}
					}
				}
				if (flag)
				{
					this.ParentObject.ParticleBlip("&W*", 10, 0L, false);
					for (int i = 0; i < localAdjacentCells.Count; i++)
					{
						Gas part = localAdjacentCells[i].AddObject(this.PuffObject, null, null, null, null).GetPart<Gas>();
						part.ColorString = this.ColorString;
						part.Creator = this.ParentObject;
					}
					this.nCooldown = 20 + Stat.Random(1, 6);
				}
			}
			return (this.EnergyCost == 0 || this.ParentObject.IsPlayer()) && base.HandleEvent(E);
		}

		// Token: 0x0600C8F1 RID: 51441 RVA: 0x00493530 File Offset: 0x00491730
		public override bool HandleEvent(GetAdjacentNavigationWeightEvent E)
		{
			if (!this.ParentObject.IsAlliedTowards(E.Actor))
			{
				E.MinWeight(97);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C8F2 RID: 51442 RVA: 0x00493554 File Offset: 0x00491754
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ApplySpores");
			Registrar.Register("VillageInit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C8F3 RID: 51443 RVA: 0x00493574 File Offset: 0x00491774
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ApplySpores")
			{
				return false;
			}
			if (E.ID == "VillageInit" && this.ParentObject.GetBlueprint(true).DescendsFrom("FungusPuffer"))
			{
				char c = ColorUtility.FindLastForeground(this.ColorString) ?? 'M';
				if (!this.ParentObject.Render.ColorString.Contains(c))
				{
					this.ParentObject.Render.DetailColor = c.ToString();
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C8F4 RID: 51444 RVA: 0x00493616 File Offset: 0x00491816
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C8F5 RID: 51445 RVA: 0x0049361F File Offset: 0x0049181F
		public override bool Mutate(GameObject GO, int Level)
		{
			this.ColorString = GO.Render.ColorString;
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C8F6 RID: 51446 RVA: 0x0049363A File Offset: 0x0049183A
		public override bool Unmutate(GameObject GO)
		{
			return base.Unmutate(GO);
		}

		// Token: 0x04004EA1 RID: 20129
		public int Chance = 100;

		// Token: 0x04004EA2 RID: 20130
		public int EnergyCost = 1000;

		// Token: 0x04004EA3 RID: 20131
		public int nCooldown;

		// Token: 0x04004EA4 RID: 20132
		public string PuffObject = "1";

		// Token: 0x04004EA5 RID: 20133
		public string ColorString = "&G";

		// Token: 0x04004EA6 RID: 20134
		public static List<string> InfectionList = new List<string> { "FungalSporeGasLuminous", "FungalSporeGasPuff", "FungalSporeGasWax", "FungalSporeGasMumbles" };

		// Token: 0x04004EA7 RID: 20135
		public static List<string> InfectionObjectList = new List<string> { "LuminousInfection", "PuffInfection", "WaxInfection", "MumblesInfection" };

		// Token: 0x04004EA8 RID: 20136
		public static List<string> PufferList = new List<string> { "FungusPuffer1", "FungusPuffer2", "FungusPuffer3", "FungusPuffer4" };
	}
}
