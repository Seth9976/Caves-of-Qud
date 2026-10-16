using System;
using XRL.UI;
using XRL.World.Effects;
using XRL.World.Tinkering;

namespace XRL.World.Parts
{
	// Token: 0x0200108F RID: 4239
	[Serializable]
	public class Repair : IPart, IRepairSifrahHandler
	{
		// Token: 0x0600A8AB RID: 43179 RVA: 0x003DB2D0 File Offset: 0x003D94D0
		public override bool SameAs(IPart p)
		{
			Repair repair = p as Repair;
			return !(repair.FixedBlueprint != this.FixedBlueprint) && repair.Difficulty == this.Difficulty && base.SameAs(p);
		}

		// Token: 0x0600A8AC RID: 43180 RVA: 0x003DB310 File Offset: 0x003D9510
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID || ID == PooledEvent<IsRepairableEvent>.ID || ID == PooledEvent<RepairedEvent>.ID;
		}

		// Token: 0x0600A8AD RID: 43181 RVA: 0x003DB33E File Offset: 0x003D953E
		public override bool HandleEvent(IsRepairableEvent E)
		{
			return false;
		}

		// Token: 0x0600A8AE RID: 43182 RVA: 0x003DB344 File Offset: 0x003D9544
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			E.AddAction("Repair", "repair", "Repair", null, 'r', false, 25, 0, false, false, false, false, true, null, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A8AF RID: 43183 RVA: 0x003DB37C File Offset: 0x003D957C
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "Repair")
			{
				if (E.Actor.GetTotalConfusion() > 0)
				{
					E.Actor.ShowFailure("You're too confused to do that.");
					return false;
				}
				if (this.ParentObject.CurrentCell != null)
				{
					if (!E.Actor.FlightMatches(this.ParentObject))
					{
						E.Actor.ShowFailure(string.Concat(new string[]
						{
							"You cannot reach ",
							this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
							" to repair ",
							this.ParentObject.them,
							"."
						}));
						return false;
					}
					if (!E.Actor.PhaseMatches(this.ParentObject))
					{
						E.Actor.ShowFailure(string.Concat(new string[]
						{
							"You are out of phase with ",
							this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
							" and cannot repair ",
							this.ParentObject.them,
							"."
						}));
						return false;
					}
				}
				GameObject parentObject = this.ParentObject;
				if (parentObject != null && parentObject.HasTagOrProperty("NoRepair"))
				{
					E.Actor.ShowFailure("You can't repair that.");
					return false;
				}
				if (E.Actor.AreHostilesNearby() && E.Actor.FireEvent("CombatPreventsRepair"))
				{
					E.Actor.ShowFailure("You can't repair with hostile creatures nearby.");
					return false;
				}
				if (Options.SifrahRepair)
				{
					if (E.Actor.IsPlayer())
					{
						if (!string.IsNullOrEmpty(this.ParentObject.Owner) && !this.ParentObject.HasPropertyOrTag("DontWarnOnRepair") && Popup.ShowYesNoCancel(string.Concat(new string[]
						{
							this.ParentObject.Does("are", 2147483647, null, null, null, false, true, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
							" not owned by you, and trying to repair ",
							this.ParentObject.them,
							" risks damaging ",
							this.ParentObject.them,
							". Are you sure you want to do so?"
						}), "Sounds/UI/ui_notification", true, DialogResult.Cancel) != DialogResult.Yes)
						{
							return false;
						}
						GameObject inInventory = this.ParentObject.InInventory;
						if (inInventory != null && inInventory != E.Actor && !string.IsNullOrEmpty(inInventory.Owner) && inInventory.Owner != this.ParentObject.Owner && !inInventory.HasPropertyOrTag("DontWarnOnRepair") && Popup.ShowYesNoCancel(string.Concat(new string[]
						{
							inInventory.Does("are", 2147483647, null, null, null, false, true, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
							" not owned by you, and trying to repair ",
							this.ParentObject.an(2147483647, null, null, false, true, false, false, false, false, true, false, false, true, false, null, false, null, false),
							" inside ",
							inInventory.them,
							" risks causing damage. Are you sure you want to do so?"
						}), "Sounds/UI/ui_notification", true, DialogResult.Cancel) != DialogResult.Yes)
						{
							return false;
						}
					}
					RepairSifrah repairSifrah = new RepairSifrah(this.ParentObject, this.ParentObject.GetComplexity(), this.Difficulty, E.Actor.Stat("Intelligence", 0));
					repairSifrah.HandlerID = this.ParentObject.ID;
					repairSifrah.HandlerPartName = base.Name;
					repairSifrah.Play(this.ParentObject);
					if (repairSifrah.InterfaceExitRequested)
					{
						E.RequestInterfaceExit();
					}
				}
				else if (this.Difficulty <= 0 || E.Actor.MakeSave("Intelligence", this.Difficulty, null, null, "Tinkering Repair", false, false, false, false, this.ParentObject))
				{
					this.RepairResultSuccess(E.Actor, this.ParentObject);
				}
				else
				{
					this.RepairResultFailure(E.Actor, this.ParentObject);
				}
				E.Actor.UseEnergy(1000, "Item Repair", null, null, false);
				E.RequestInterfaceExit();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A8B0 RID: 43184 RVA: 0x003DB7CD File Offset: 0x003D99CD
		public override bool HandleEvent(RepairedEvent E)
		{
			this.ParentObject.ReplaceWith(GameObject.CreateUnmodified(this.FixedBlueprint));
			return false;
		}

		// Token: 0x0600A8B1 RID: 43185 RVA: 0x003DB7E7 File Offset: 0x003D99E7
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600A8B2 RID: 43186 RVA: 0x003DB7EC File Offset: 0x003D99EC
		public void RepairResultSuccess(GameObject who, GameObject obj)
		{
			who.PlayWorldOrUISound("Sounds/Misc/sfx_interact_artifact_repair", null);
			if (who.IsPlayer())
			{
				Popup.ShowBlock("You repair " + obj.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			RepairedEvent.Send(who, obj, null, null, 0);
		}

		// Token: 0x0600A8B3 RID: 43187 RVA: 0x003DB864 File Offset: 0x003D9A64
		public void RepairResultExceptionalSuccess(GameObject who, GameObject obj)
		{
			this.RepairResultSuccess(who, obj);
			string randomBits = BitType.GetRandomBits("3d6".Roll(), obj.GetComplexity());
			if (!string.IsNullOrEmpty(randomBits))
			{
				who.RequirePart<BitLocker>(false).AddBits(randomBits);
				if (who.IsPlayer())
				{
					Popup.Show("You receive tinkering bits <{{|" + BitType.GetDisplayString(randomBits) + "}}>", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
			}
		}

		// Token: 0x0600A8B4 RID: 43188 RVA: 0x003DB8D4 File Offset: 0x003D9AD4
		public void RepairResultPartialSuccess(GameObject who, GameObject obj)
		{
			if (who.IsPlayer())
			{
				Popup.Show("You make some progress repairing " + obj.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
		}

		// Token: 0x0600A8B5 RID: 43189 RVA: 0x003DB930 File Offset: 0x003D9B30
		public void RepairResultFailure(GameObject who, GameObject obj)
		{
			if (who.IsPlayer())
			{
				Popup.Show("You can't figure out how to fix " + this.ParentObject.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
		}

		// Token: 0x0600A8B6 RID: 43190 RVA: 0x003DB990 File Offset: 0x003D9B90
		public void RepairResultCriticalFailure(GameObject who, GameObject obj)
		{
			if (RepairCriticalFailureEvent.Check(who, obj))
			{
				if (obj.IsBroken())
				{
					if (!obj.HasPropertyOrTag("CantDestroyOnRepair"))
					{
						obj.PotentiallyAngerOwner(who, "DontWarnOnRepair");
						IComponent<GameObject>.XDidYToZ(who, "accidentally", "destroy", obj, null, "!", null, null, null, who, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
						obj.Destroy(null, false, false, null);
						return;
					}
					this.RepairResultFailure(who, obj);
					return;
				}
				else
				{
					string text = "You think you broke " + obj.them + "...";
					if (obj.ApplyEffect(new Broken(), null) && obj.IsBroken())
					{
						if (who.IsPlayer())
						{
							Popup.Show(text, null, "Sounds/UI/ui_notification", true, true, true, true, null);
						}
						obj.PotentiallyAngerOwner(who, "DontWarnOnRepair");
						return;
					}
					this.RepairResultFailure(who, obj);
				}
			}
		}

		// Token: 0x040045BA RID: 17850
		public const string OWNER_ANGER_SUPPRESS = "DontWarnOnRepair";

		// Token: 0x040045BB RID: 17851
		public string FixedBlueprint;

		// Token: 0x040045BC RID: 17852
		public int Difficulty;
	}
}
