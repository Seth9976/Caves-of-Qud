using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ConsoleLib.Console;
using XRL.Core;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02000E60 RID: 3680
	[Serializable]
	public class CrossFlameOnStep : IActivePart
	{
		// Token: 0x060094D4 RID: 38100 RVA: 0x0035D1BC File Offset: 0x0035B3BC
		public CrossFlameOnStep()
		{
			this.WorksOnCellContents = true;
		}

		// Token: 0x060094D5 RID: 38101 RVA: 0x0035D258 File Offset: 0x0035B458
		public override bool SameAs(IPart p)
		{
			CrossFlameOnStep crossFlameOnStep = p as CrossFlameOnStep;
			return crossFlameOnStep.Chance == this.Chance && !(crossFlameOnStep.SaveStat != this.SaveStat) && !(crossFlameOnStep.SaveDifficultyStat != this.SaveDifficultyStat) && crossFlameOnStep.SaveTarget == this.SaveTarget && !(crossFlameOnStep.SaveVs != this.SaveVs) && crossFlameOnStep.CooldownLeft == this.CooldownLeft && crossFlameOnStep.TriggerOnSaveSuccess == this.TriggerOnSaveSuccess && !(crossFlameOnStep.Cooldown != this.Cooldown) && crossFlameOnStep.Level == this.Level && !(crossFlameOnStep.Length != this.Length) && !(crossFlameOnStep.CooldownColorString != this.CooldownColorString) && !(crossFlameOnStep.ReadyColorString != this.ReadyColorString) && !(crossFlameOnStep.CooldownTileColor != this.CooldownTileColor) && !(crossFlameOnStep.ReadyTileColor != this.ReadyTileColor) && !(crossFlameOnStep.CooldownDetailColor != this.CooldownDetailColor) && !(crossFlameOnStep.ReadyDetailColor != this.ReadyDetailColor) && base.SameAs(p);
		}

		// Token: 0x060094D6 RID: 38102 RVA: 0x0035D3AA File Offset: 0x0035B5AA
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x060094D7 RID: 38103 RVA: 0x0035D3AD File Offset: 0x0035B5AD
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (this.CooldownLeft > 0)
			{
				this.CooldownLeft--;
				if (this.CooldownLeft <= 0)
				{
					this.SyncColor();
				}
			}
			this.CheckActivate();
		}

		// Token: 0x060094D8 RID: 38104 RVA: 0x0035D3DB File Offset: 0x0035B5DB
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetNavigationWeightEvent.ID || ID == PooledEvent<InterruptAutowalkEvent>.ID || ID == ObjectCreatedEvent.ID || ID == ObjectEnteredCellEvent.ID;
		}

		// Token: 0x060094D9 RID: 38105 RVA: 0x0035D409 File Offset: 0x0035B609
		public override bool HandleEvent(GetNavigationWeightEvent E)
		{
			if ((!E.Flying || this.ParentObject.IsFlying) && E.PhaseMatches(this.ParentObject))
			{
				E.MinWeight(98);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060094DA RID: 38106 RVA: 0x0035D43D File Offset: 0x0035B63D
		public override bool HandleEvent(InterruptAutowalkEvent E)
		{
			E.IndicateObject = this.ParentObject;
			return false;
		}

		// Token: 0x060094DB RID: 38107 RVA: 0x0035D44C File Offset: 0x0035B64C
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			this.SyncColor();
			return base.HandleEvent(E);
		}

		// Token: 0x060094DC RID: 38108 RVA: 0x0035D45B File Offset: 0x0035B65B
		public override bool HandleEvent(ObjectEnteredCellEvent E)
		{
			this.CheckActivate();
			return base.HandleEvent(E);
		}

		// Token: 0x060094DD RID: 38109 RVA: 0x0035D46C File Offset: 0x0035B66C
		public void CheckActivate()
		{
			if (this.CooldownLeft <= 0 && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) && this.Chance.in100())
			{
				List<GameObject> activePartSubjects = this.GetActivePartSubjects();
				for (int i = 0; i < activePartSubjects.Count; i++)
				{
					GameObject gameObject = activePartSubjects[i];
					if (this.ValidStepTarget(gameObject) && (string.IsNullOrEmpty(this.SaveStat) || gameObject.MakeSave(this.SaveStat, this.SaveTarget, this.ParentObject, this.SaveDifficultyStat, null, false, false, false, false, null) == this.TriggerOnSaveSuccess))
					{
						this.Discharge();
						return;
					}
				}
			}
		}

		// Token: 0x060094DE RID: 38110 RVA: 0x0035D524 File Offset: 0x0035B724
		public void SyncColor()
		{
			if (this.CooldownLeft > 0)
			{
				if (!string.IsNullOrEmpty(this.CooldownColorString))
				{
					this.ParentObject.Render.ColorString = this.CooldownColorString;
				}
				if (!string.IsNullOrEmpty(this.CooldownTileColor))
				{
					this.ParentObject.Render.TileColor = this.CooldownTileColor;
				}
				if (!string.IsNullOrEmpty(this.CooldownDetailColor))
				{
					this.ParentObject.Render.DetailColor = this.CooldownDetailColor;
					return;
				}
			}
			else
			{
				if (!string.IsNullOrEmpty(this.ReadyColorString))
				{
					this.ParentObject.Render.ColorString = this.ReadyColorString;
				}
				if (!string.IsNullOrEmpty(this.ReadyTileColor))
				{
					this.ParentObject.Render.TileColor = this.ReadyTileColor;
				}
				if (!string.IsNullOrEmpty(this.ReadyDetailColor))
				{
					this.ParentObject.Render.DetailColor = this.ReadyDetailColor;
				}
			}
		}

		// Token: 0x060094DF RID: 38111 RVA: 0x0035D610 File Offset: 0x0035B810
		public bool ValidStepTarget(GameObject obj)
		{
			return obj.IsCombatObject() && this.ParentObject.PhaseAndFlightMatches(obj);
		}

		// Token: 0x060094E0 RID: 38112 RVA: 0x0035D628 File Offset: 0x0035B828
		public void Discharge()
		{
			if (this.ParentObject.CurrentCell == null)
			{
				return;
			}
			if (this.CooldownLeft > 0)
			{
				return;
			}
			this.CooldownLeft = this.Cooldown.RollCached();
			string text = this.Level.ToString() + "d4+1";
			List<Cell> list = new List<Cell>();
			int num = this.Length.RollCached();
			foreach (string text2 in Directions.CardinalDirectionList)
			{
				Cell cell = this.ParentObject.Physics.CurrentCell.GetLocalCellFromDirection(text2, true);
				int num2 = 0;
				while (num2 < num && cell != null)
				{
					list.Add(cell);
					cell = cell.GetLocalCellFromDirection(text2, true);
					num2++;
				}
			}
			bool flag = false;
			if (CrossFlameOnStep.Buffer == null)
			{
				flag = true;
				CrossFlameOnStep.Buffer = ScreenBuffer.GetScrapBuffer1(true);
				XRLCore.Core.RenderMapToBuffer(CrossFlameOnStep.Buffer);
			}
			bool flag2 = false;
			foreach (Cell cell2 in list)
			{
				if (cell2 != null)
				{
					foreach (GameObject gameObject in cell2.GetObjectsInCell())
					{
						if (gameObject.PhaseMatches(this.ParentObject))
						{
							gameObject.TemperatureChange(310 + 25 * this.Level, this.ParentObject, false, false, false, false, 0, null, null);
							int num3 = Stat.Random(1, 3);
							if (num3 == 1)
							{
								gameObject.ParticleText("&r" + ((char)(219 + Stat.Random(0, 4))).ToString(), 0f, 1);
							}
							if (num3 == 2)
							{
								gameObject.ParticleText("&R" + ((char)(219 + Stat.Random(0, 4))).ToString(), 0f, 1);
							}
							if (num3 == 3)
							{
								gameObject.ParticleText("&W" + ((char)(219 + Stat.Random(0, 4))).ToString(), 0f, 1);
							}
						}
					}
					foreach (GameObject gameObject2 in cell2.GetObjectsWithPartReadonly("Combat"))
					{
						if (gameObject2.PhaseMatches(this.ParentObject) && !gameObject2.HasPart<CrossFlameOnStep>())
						{
							gameObject2.TakeDamage(text.RollCached(), "from %t flames!", "Fire Heat", null, null, null, this.ParentObject, null, null, null, false, false, false, false, false, false, false, false, false, 5, null);
						}
					}
				}
				if (cell2.IsVisible() && CrossFlameOnStep.Buffer != null)
				{
					cell2.Flameburst(CrossFlameOnStep.Buffer);
					flag2 = true;
				}
			}
			if (CrossFlameOnStep.Buffer != null && flag2)
			{
				Thread.Sleep(10);
			}
			list.ForEach(delegate(Cell c)
			{
				c.GetObjectsWithPartReadonly("CrossFlameOnStep").ForEach(delegate(GameObject o)
				{
					o.GetPart<CrossFlameOnStep>().Discharge();
				});
			});
			base.ConsumeChargeIfOperational(false, false, false, false, false, false, false, false, false, 1, null, false, 0, false, null);
			this.SyncColor();
			if (flag)
			{
				CrossFlameOnStep.Buffer = null;
			}
		}

		// Token: 0x060094E1 RID: 38113 RVA: 0x0035D9C0 File Offset: 0x0035BBC0
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeforeTookDamage");
			base.Register(Object, Registrar);
		}

		// Token: 0x060094E2 RID: 38114 RVA: 0x0035D9D8 File Offset: 0x0035BBD8
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeforeTookDamage" && !string.IsNullOrEmpty(this.DamageTriggerTypes) && this.ParentObject.CurrentCell != null)
			{
				Damage damage = E.GetParameter("Damage") as Damage;
				if (damage != null && damage.Attributes.Any<string>((string s) => this.DamageTriggerTypes.Contains(s)))
				{
					this.Discharge();
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003D9B RID: 15771
		public int CooldownLeft;

		// Token: 0x04003D9C RID: 15772
		public string Cooldown = "1d6";

		// Token: 0x04003D9D RID: 15773
		public string DamageTriggerTypes = "Heat;Fire";

		// Token: 0x04003D9E RID: 15774
		public int Chance = 100;

		// Token: 0x04003D9F RID: 15775
		public string SaveStat;

		// Token: 0x04003DA0 RID: 15776
		public string SaveDifficultyStat;

		// Token: 0x04003DA1 RID: 15777
		public int SaveTarget = 15;

		// Token: 0x04003DA2 RID: 15778
		public string SaveVs;

		// Token: 0x04003DA3 RID: 15779
		public bool TriggerOnSaveSuccess = true;

		// Token: 0x04003DA4 RID: 15780
		public int Level = 4;

		// Token: 0x04003DA5 RID: 15781
		public string Length = "4";

		// Token: 0x04003DA6 RID: 15782
		public string CooldownColorString = "&r";

		// Token: 0x04003DA7 RID: 15783
		public new string ReadyColorString = "&R";

		// Token: 0x04003DA8 RID: 15784
		public string CooldownTileColor = "&w";

		// Token: 0x04003DA9 RID: 15785
		public string ReadyTileColor = "&W";

		// Token: 0x04003DAA RID: 15786
		public string CooldownDetailColor = "r";

		// Token: 0x04003DAB RID: 15787
		public new string ReadyDetailColor = "R";

		// Token: 0x04003DAC RID: 15788
		public static ScreenBuffer Buffer;
	}
}
