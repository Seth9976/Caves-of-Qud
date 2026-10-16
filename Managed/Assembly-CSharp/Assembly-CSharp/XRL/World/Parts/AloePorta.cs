using System;
using System.Collections.Generic;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02000E67 RID: 3687
	[Serializable]
	public class AloePorta : IActivePart
	{
		// Token: 0x060094FC RID: 38140 RVA: 0x0035E094 File Offset: 0x0035C294
		public AloePorta()
		{
			this.WorksOnCellContents = true;
		}

		// Token: 0x060094FD RID: 38141 RVA: 0x0035E11D File Offset: 0x0035C31D
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EndTurn");
			Registrar.Register("ObjectEnteredCell");
			Registrar.Register("ObjectCreated");
			base.Register(Object, Registrar);
		}

		// Token: 0x060094FE RID: 38142 RVA: 0x0035E148 File Offset: 0x0035C348
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<InterruptAutowalkEvent>.ID;
		}

		// Token: 0x060094FF RID: 38143 RVA: 0x0035E15E File Offset: 0x0035C35E
		public override bool HandleEvent(InterruptAutowalkEvent E)
		{
			if (!this.ParentObject.IsHidden)
			{
				E.IndicateObject = this.ParentObject;
			}
			return false;
		}

		// Token: 0x06009500 RID: 38144 RVA: 0x0035E17C File Offset: 0x0035C37C
		public override bool SameAs(IPart p)
		{
			DischargeOnStep dischargeOnStep = p as DischargeOnStep;
			return dischargeOnStep.Chance == this.Chance && !(dischargeOnStep.SaveStat != this.SaveStat) && !(dischargeOnStep.SaveDifficultyStat != this.SaveDifficultyStat) && dischargeOnStep.SaveTarget == this.SaveTarget && !(dischargeOnStep.SaveVs != this.SaveVs) && dischargeOnStep.CooldownLeft == this.CooldownLeft && dischargeOnStep.TriggerOnSaveSuccess == this.TriggerOnSaveSuccess && !(dischargeOnStep.Cooldown != this.Cooldown) && base.SameAs(p);
		}

		// Token: 0x06009501 RID: 38145 RVA: 0x0035E22C File Offset: 0x0035C42C
		public void SyncColor()
		{
			if (!this.DoCooldownRecolor)
			{
				return;
			}
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

		// Token: 0x06009502 RID: 38146 RVA: 0x0035E321 File Offset: 0x0035C521
		public bool ValidStepTarget(GameObject obj)
		{
			return obj.IsCombat && obj.FlightMatches(this.ParentObject);
		}

		// Token: 0x06009503 RID: 38147 RVA: 0x0035E33C File Offset: 0x0035C53C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EndTurn" && E.ID == "EndTurn" && this.CooldownLeft > 0)
			{
				this.CooldownLeft--;
				if (this.CooldownLeft <= 0)
				{
					this.SyncColor();
				}
			}
			if (E.ID == "ObjectEnteredCell")
			{
				if (this.CooldownLeft > 0 || this.GetActivePartFirstSubject(new Predicate<GameObject>(this.ValidStepTarget)) == null || !base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) || (this.Chance < 100 && Stat.Random(1, 100) >= this.Chance))
				{
					goto IL_022A;
				}
				using (List<GameObject>.Enumerator enumerator = this.GetActivePartSubjects(new Predicate<GameObject>(this.ValidStepTarget)).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						GameObject gameObject = enumerator.Current;
						if (!gameObject.HasStringProperty("aloevoltatarget") && (string.IsNullOrEmpty(this.SaveStat) || gameObject.MakeSave(this.SaveStat, this.SaveTarget, this.ParentObject, this.SaveDifficultyStat, null, false, false, false, false, null) == this.TriggerOnSaveSuccess))
						{
							List<GameObject> objects = this.ParentObject.Physics.CurrentCell.ParentZone.GetObjects(delegate(GameObject o)
							{
								if (o != this.ParentObject && o.HasPart<AloePorta>() && o.GetPart<AloePorta>().GroupKey == this.GroupKey && o.GetPart<AloePorta>().CooldownLeft <= 0)
								{
									if (o.Physics.CurrentCell.IsSolid())
									{
										return false;
									}
									if (o.Physics.CurrentCell.IsPassable(o, true))
									{
										return true;
									}
								}
								return false;
							});
							if (objects.Count > 0)
							{
								GameObject randomElement = objects.GetRandomElement<GameObject>(null);
								this.CooldownLeft = Stat.Roll(this.Cooldown, null);
								randomElement.GetPart<AloePorta>().CooldownLeft = Stat.Roll(randomElement.GetPart<AloePorta>().Cooldown, null);
								gameObject.TeleportSwirl(null, "&C", false, null, 'ù', true);
								gameObject.TeleportTo(randomElement.Physics.CurrentCell, new int?(0), true, false, false, false, "disappear", "appear");
								gameObject.TeleportSwirl(null, "&C", false, null, 'ù', false);
							}
						}
					}
					goto IL_022A;
				}
			}
			if (E.ID == "ObjectCreated")
			{
				this.SyncColor();
			}
			IL_022A:
			return base.FireEvent(E);
		}

		// Token: 0x04003DB5 RID: 15797
		public string GroupKey = "AloePorta";

		// Token: 0x04003DB6 RID: 15798
		public int CooldownLeft;

		// Token: 0x04003DB7 RID: 15799
		public string Cooldown = "1";

		// Token: 0x04003DB8 RID: 15800
		public int Chance = 100;

		// Token: 0x04003DB9 RID: 15801
		public string SaveStat;

		// Token: 0x04003DBA RID: 15802
		public string SaveDifficultyStat;

		// Token: 0x04003DBB RID: 15803
		public int SaveTarget = 15;

		// Token: 0x04003DBC RID: 15804
		public string SaveVs;

		// Token: 0x04003DBD RID: 15805
		public bool TriggerOnSaveSuccess = true;

		// Token: 0x04003DBE RID: 15806
		public bool DoCooldownRecolor;

		// Token: 0x04003DBF RID: 15807
		public string CooldownColorString = "&w";

		// Token: 0x04003DC0 RID: 15808
		public new string ReadyColorString = "&W";

		// Token: 0x04003DC1 RID: 15809
		public string CooldownTileColor = "&w";

		// Token: 0x04003DC2 RID: 15810
		public string ReadyTileColor = "&W";

		// Token: 0x04003DC3 RID: 15811
		public string CooldownDetailColor = "b";

		// Token: 0x04003DC4 RID: 15812
		public new string ReadyDetailColor = "B";
	}
}
