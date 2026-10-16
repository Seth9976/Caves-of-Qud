using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using XRL.UI;
using XRL.World.Effects;
using XRL.World.Tinkering;

namespace XRL.World.Parts
{
	// Token: 0x02001015 RID: 4117
	[Serializable]
	public class DeployableInfrastructure : IPart
	{
		// Token: 0x0600A410 RID: 42000 RVA: 0x003BFC04 File Offset: 0x003BDE04
		public override bool SameAs(IPart p)
		{
			DeployableInfrastructure deployableInfrastructure = p as DeployableInfrastructure;
			return !(deployableInfrastructure.DeployNoun != this.DeployNoun) && !(deployableInfrastructure.DeployVerb != this.DeployVerb) && !(deployableInfrastructure.ModPart != this.ModPart) && !(deployableInfrastructure.ObjectBlueprint != this.ObjectBlueprint) && !(deployableInfrastructure.SkillRequired != this.SkillRequired) && !(deployableInfrastructure.NoModIfPart != this.NoModIfPart) && !(deployableInfrastructure.SuppressIfPart != this.SuppressIfPart) && deployableInfrastructure.Cells == this.Cells && deployableInfrastructure.MaxCells == this.MaxCells && deployableInfrastructure.EnergyCost == this.EnergyCost && deployableInfrastructure.AllowExistenceSupport == this.AllowExistenceSupport && deployableInfrastructure.MakeUnderstood == this.MakeUnderstood && deployableInfrastructure.RepairDuplicates == this.RepairDuplicates && deployableInfrastructure.SuppressDuplicates == this.SuppressDuplicates && deployableInfrastructure.UsesStack == this.UsesStack && deployableInfrastructure.RequireVisibility == this.RequireVisibility;
		}

		// Token: 0x0600A411 RID: 42001 RVA: 0x003BFD3C File Offset: 0x003BDF3C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x0600A412 RID: 42002 RVA: 0x003BFD5C File Offset: 0x003BDF5C
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (this.SkillRequired.IsNullOrEmpty() || The.Player.HasSkill(this.SkillRequired))
			{
				E.AddAction("Deploy", this.DeployNoun.IsNullOrEmpty() ? "deploy" : ("deploy " + this.DeployNoun), "DeployInfrastructure", null, 'y', false, 0, 0, false, false, false, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A413 RID: 42003 RVA: 0x003BFDD1 File Offset: 0x003BDFD1
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "DeployInfrastructure" && this.AttemptDeploy(E.Actor) && this.EnergyCost > 0)
			{
				E.RequestInterfaceExit();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A414 RID: 42004 RVA: 0x003BFE0C File Offset: 0x003BE00C
		public bool AttemptDeploy(GameObject Actor)
		{
			if (Actor == null)
			{
				return false;
			}
			if (!this.SkillRequired.IsNullOrEmpty() && !Actor.HasSkill(this.SkillRequired))
			{
				return false;
			}
			if (Actor.OnWorldMap())
			{
				return Actor.Fail("You cannot do that on the world map.");
			}
			if (this.ParentObject.IsRusted())
			{
				return Actor.Fail(this.ParentObject.Itis + " rusted...");
			}
			if (this.ParentObject.IsBroken())
			{
				return Actor.Fail(this.ParentObject.Itis + " broken...");
			}
			int num = (this.UsesStack ? this.ParentObject.Count : this.Cells);
			if (this.MaxCells > 0 && num > this.MaxCells)
			{
				num = this.MaxCells;
			}
			if (num <= 0)
			{
				return false;
			}
			List<Cell> list;
			if (num == 1 && !this.UsesStack)
			{
				list = new List<Cell>();
				Cell cell;
				if (this.DeployNoun.IsNullOrEmpty())
				{
					cell = base.PickDirection(false, "Deploy", Actor);
				}
				else
				{
					cell = base.PickDirection(false, "Deploy " + ColorUtility.CapitalizeExceptFormatting(this.DeployNoun), Actor);
				}
				if (cell == null)
				{
					return false;
				}
				list.Add(cell);
			}
			else if (this.DeployNoun.IsNullOrEmpty())
			{
				list = base.PickFieldAdjacent(num, Actor, null, false, this.RequireVisibility);
			}
			else
			{
				list = base.PickFieldAdjacent(num, Actor, ColorUtility.CapitalizeExceptFormatting(this.DeployNoun), false, this.RequireVisibility);
			}
			if (list == null || list.Count == 0)
			{
				return false;
			}
			int num2 = 0;
			foreach (Cell cell2 in list)
			{
				if (this.DeployOne(Actor, cell2, false, false))
				{
					num2++;
				}
			}
			if (num2 == 0)
			{
				Popup.ShowFail(string.Concat(new string[]
				{
					"There is no useful way to ",
					this.DeployVerb,
					" ",
					this.ParentObject.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
					" there."
				}), true, true, true);
				return false;
			}
			Actor.PlayWorldOrUISound(this.DeploySound, null);
			if (!this.MessageByCountSuffix.IsNullOrEmpty())
			{
				Popup.Show(string.Concat(new string[]
				{
					"You ",
					this.DeployVerb,
					" ",
					num2.ToString(),
					this.MessageByCountSuffix
				}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			else
			{
				Popup.Show(string.Concat(new string[]
				{
					"You ",
					this.DeployVerb,
					" ",
					this.ParentObject.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
					"."
				}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			if (this.EnergyCost > 0)
			{
				Actor.UseEnergy(this.EnergyCost, "Tinkering", null, null, false);
			}
			if (this.UsesStack)
			{
				for (int i = 0; i < num2; i++)
				{
					this.ParentObject.Destroy(null, false, false, null);
				}
			}
			else
			{
				this.ParentObject.Destroy(null, false, false, null);
			}
			The.ZoneManager.PaintWalls();
			return true;
		}

		// Token: 0x0600A415 RID: 42005 RVA: 0x003C017C File Offset: 0x003BE37C
		public bool DeployOne(GameObject Actor, Cell Cell, bool Message = false, bool Sound = false)
		{
			bool flag = false;
			if (!this.SuppressIfPart.IsNullOrEmpty() && Cell.HasObjectWithPart(this.SuppressIfPart))
			{
				return flag;
			}
			if (this.RepairDuplicates || this.SuppressDuplicates)
			{
				if (!this.ObjectBlueprint.IsNullOrEmpty())
				{
					GameObject firstObject = Cell.GetFirstObject(this.ObjectBlueprint);
					if (firstObject != null)
					{
						if (this.RepairDuplicates)
						{
							if (firstObject.IsBroken())
							{
								firstObject.RemoveEffect<Broken>();
								flag = true;
							}
							if (firstObject.isDamaged())
							{
								firstObject.RestorePristineHealth(false, false);
								flag = true;
							}
						}
						if (this.SuppressDuplicates)
						{
							goto IL_01A2;
						}
					}
				}
				if (!this.ModPart.IsNullOrEmpty())
				{
					GameObject firstObjectWithPart = Cell.GetFirstObjectWithPart(this.ModPart);
					if (firstObjectWithPart != null)
					{
						if (this.RepairDuplicates)
						{
							if (firstObjectWithPart.IsBroken())
							{
								firstObjectWithPart.RemoveEffect<Broken>();
								flag = true;
							}
							if (firstObjectWithPart.isDamaged())
							{
								firstObjectWithPart.RestorePristineHealth(false, false);
								flag = true;
							}
						}
						if (this.SuppressDuplicates)
						{
							goto IL_01A2;
						}
					}
				}
			}
			GameObject gameObject = null;
			if (!this.ModPart.IsNullOrEmpty())
			{
				gameObject = Cell.GetFirstObjectWithPropertyOrTag("Wall", new Predicate<GameObject>(this.ValidInstall)) ?? Cell.GetFirstObjectWithPart("Physics", new Predicate<GameObject>(this.ValidInstall));
			}
			if (gameObject != null)
			{
				ItemModding.ApplyModification(gameObject, this.ModPart, true, null, false);
				ZoneManager.PaintWalls(Cell.ParentZone, Cell.X - 1, Cell.Y - 1, Cell.X + 1, Cell.Y + 1);
				flag = true;
			}
			else if (!this.ObjectBlueprint.IsNullOrEmpty())
			{
				GameObject gameObject2 = Cell.AddObject(GameObject.CreateUnmodified(this.ObjectBlueprint), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				if (gameObject2 != null)
				{
					if (this.MakeUnderstood)
					{
						gameObject2.MakeUnderstood(false);
					}
					flag = true;
				}
			}
			IL_01A2:
			if (Sound)
			{
				Cell.PlayWorldSound(this.DeploySound, 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			if (Message && flag)
			{
				Actor.EmitMessage(string.Concat(new string[]
				{
					Actor.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
					" ",
					Actor.GetVerb(this.DeployVerb, false, false, false, true, false),
					" ",
					this.ParentObject.an(int.MaxValue, null, null, false, true, false, false, false, false, true, false, false, true, false, null, false, null, false),
					"."
				}), null, null, Actor.IsPlayer());
			}
			return flag;
		}

		// Token: 0x0600A416 RID: 42006 RVA: 0x003C0400 File Offset: 0x003BE600
		private bool ValidInstall(GameObject obj)
		{
			return !obj.IsTakeable() && (obj.ConsiderSolid() || obj.IsDoor()) && obj.IsReal && (this.AllowExistenceSupport || !obj.HasPart<ExistenceSupport>()) && !obj.IsCombatObject() && (this.ModPart.IsNullOrEmpty() || !obj.HasPart(this.ModPart)) && (this.NoModIfPart.IsNullOrEmpty() || !obj.HasPart(this.NoModIfPart)) && (this.SuppressIfPart.IsNullOrEmpty() || !obj.HasPart(this.SuppressIfPart));
		}

		// Token: 0x040043EE RID: 17390
		public string DeployNoun = "tech";

		// Token: 0x040043EF RID: 17391
		public string DeployVerb = "deploy";

		// Token: 0x040043F0 RID: 17392
		public string DeploySound;

		// Token: 0x040043F1 RID: 17393
		public string ModPart;

		// Token: 0x040043F2 RID: 17394
		public string ObjectBlueprint;

		// Token: 0x040043F3 RID: 17395
		public string SkillRequired;

		// Token: 0x040043F4 RID: 17396
		public string NoModIfPart;

		// Token: 0x040043F5 RID: 17397
		public string SuppressIfPart;

		// Token: 0x040043F6 RID: 17398
		public string MessageByCountSuffix;

		// Token: 0x040043F7 RID: 17399
		public int Cells = 1;

		// Token: 0x040043F8 RID: 17400
		public int MaxCells;

		// Token: 0x040043F9 RID: 17401
		public int EnergyCost = 1000;

		// Token: 0x040043FA RID: 17402
		public bool AllowExistenceSupport;

		// Token: 0x040043FB RID: 17403
		public bool MakeUnderstood = true;

		// Token: 0x040043FC RID: 17404
		public bool RepairDuplicates = true;

		// Token: 0x040043FD RID: 17405
		public bool SuppressDuplicates = true;

		// Token: 0x040043FE RID: 17406
		public bool UsesStack;

		// Token: 0x040043FF RID: 17407
		public bool RequireVisibility = true;
	}
}
