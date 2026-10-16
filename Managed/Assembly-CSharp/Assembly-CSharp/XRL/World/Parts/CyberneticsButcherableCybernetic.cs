using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.UI;
using XRL.World.Parts.Skill;

namespace XRL.World.Parts
{
	// Token: 0x02000F1D RID: 3869
	[Serializable]
	public class CyberneticsButcherableCybernetic : IPart
	{
		// Token: 0x06009A36 RID: 39478 RVA: 0x0037F7F4 File Offset: 0x0037D9F4
		public override IPart DeepCopy(GameObject Parent)
		{
			CyberneticsButcherableCybernetic cyberneticsButcherableCybernetic = base.DeepCopy(Parent) as CyberneticsButcherableCybernetic;
			cyberneticsButcherableCybernetic.Cybernetics = new List<GameObject>(this.Cybernetics.Count);
			foreach (GameObject gameObject in this.Cybernetics)
			{
				cyberneticsButcherableCybernetic.Cybernetics.Add(gameObject.DeepCopy(false, false, null));
			}
			return cyberneticsButcherableCybernetic;
		}

		// Token: 0x06009A37 RID: 39479 RVA: 0x0037F878 File Offset: 0x0037DA78
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			Writer.WriteGameObjectList(this.Cybernetics);
			base.Write(Basis, Writer);
		}

		// Token: 0x06009A38 RID: 39480 RVA: 0x0037F88E File Offset: 0x0037DA8E
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			Reader.ReadGameObjectList(this.Cybernetics, null);
			base.Read(Basis, Reader);
		}

		// Token: 0x06009A39 RID: 39481 RVA: 0x0037F8A5 File Offset: 0x0037DAA5
		public CyberneticsButcherableCybernetic()
		{
		}

		// Token: 0x06009A3A RID: 39482 RVA: 0x0037F8C1 File Offset: 0x0037DAC1
		public CyberneticsButcherableCybernetic(GameObject cybernetic)
		{
			this.Cybernetics.Add(cybernetic);
		}

		// Token: 0x06009A3B RID: 39483 RVA: 0x0037F8E9 File Offset: 0x0037DAE9
		public CyberneticsButcherableCybernetic(List<GameObject> cybernetics)
		{
			this.Cybernetics.AddRange(cybernetics);
		}

		// Token: 0x06009A3C RID: 39484 RVA: 0x0037F911 File Offset: 0x0037DB11
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009A3D RID: 39485 RVA: 0x0037F914 File Offset: 0x0037DB14
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID || ID == ObjectEnteringCellEvent.ID;
		}

		// Token: 0x06009A3E RID: 39486 RVA: 0x0037F93C File Offset: 0x0037DB3C
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (this.IsButcherable() && IComponent<GameObject>.ThePlayer.HasSkill("CookingAndGathering_Butchery"))
			{
				E.AddAction("Butcher", "butcher", "Butcher", null, 'b', false, 20, 0, false, false, false, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009A3F RID: 39487 RVA: 0x0037F990 File Offset: 0x0037DB90
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "Butcher" && this.AttemptButcher(E.Actor, E.Auto, false, false, 0, E.FromCell, E.Generated))
			{
				E.RequestInterfaceExit();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009A40 RID: 39488 RVA: 0x0037F9DF File Offset: 0x0037DBDF
		public override bool HandleEvent(ObjectEnteringCellEvent E)
		{
			this.AttemptButcher(E.Object, true, false, false, 0, E.Cell, null);
			return base.HandleEvent(E);
		}

		// Token: 0x06009A41 RID: 39489 RVA: 0x0037FA00 File Offset: 0x0037DC00
		public bool AttemptButcher(GameObject who, bool Automatic = false, bool SkipSkill = false, bool IntoInventory = false, int ChanceMod = 0, Cell FromCell = null, List<GameObject> Tracking = null)
		{
			if (!this.IsButcherable())
			{
				return false;
			}
			if (!SkipSkill && !who.HasSkill("CookingAndGathering_Butchery"))
			{
				return false;
			}
			if (Automatic)
			{
				if (this.ParentObject.IsImportant())
				{
					return false;
				}
				if (who.IsPlayer())
				{
					if (who.ArePerceptibleHostilesNearby(false, false, null, null, null, Options.AutoexploreIgnoreEasyEnemies, Options.AutoexploreIgnoreDistantEnemies, false, false))
					{
						return false;
					}
				}
				else if (who.Target != null)
				{
					return false;
				}
				CookingAndGathering_Butchery part = who.GetPart<CookingAndGathering_Butchery>();
				if (part != null && !part.IsMyActivatedAbilityToggledOn(part.ActivatedAbilityID, null))
				{
					return false;
				}
			}
			if (!who.CheckFrozen(false, false, false, null))
			{
				return false;
			}
			if (!this.ParentObject.ConfirmUseImportant(who, "butcher", null, -1))
			{
				return false;
			}
			bool flag = this.ParentObject.GetIntProperty("StoredByPlayer", 0) > 0;
			Cell currentCell = this.ParentObject.GetCurrentCell();
			Cell cell = FromCell ?? who.GetCurrentCell();
			string text = null;
			if (currentCell != null && currentCell != cell)
			{
				text = Directions.GetDirectionDescription(who, cell.GetDirectionFromCell(currentCell, false));
			}
			int num = this.BaseSuccessChance + ChanceMod;
			base.PlayWorldSound("Sounds/Interact/sfx_interact_cyberneticImplant_butcher", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
			foreach (GameObject gameObject in this.Cybernetics)
			{
				bool flag2 = IntoInventory || (who.IsPlayer() && gameObject.ShouldAutoget());
				if (num.in100() && !this.ParentObject.IsTemporary && !gameObject.IsTemporary)
				{
					gameObject.DeleteStringProperty("CannotEquip");
					if (flag)
					{
						gameObject.SetIntProperty("FromStoredByPlayer", 1, false);
					}
					Event @event = Event.New("ObjectExtracted", 0, 0, 0);
					@event.SetParameter("Object", gameObject);
					@event.SetParameter("Source", this.ParentObject);
					@event.SetParameter("Actor", who);
					@event.SetParameter("Action", "Butcher");
					gameObject.FireEvent(@event);
					if (who.IsPlayer())
					{
						IComponent<GameObject>.EmitMessage(who, string.Concat(new string[]
						{
							"{{g|You butcher ",
							gameObject.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
							" from ",
							this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
							(text == null) ? "" : (" " + text),
							".}}"
						}), ' ', !Automatic, false, false, null, null);
					}
					else if (IComponent<GameObject>.Visible(who))
					{
						IComponent<GameObject>.EmitMessage(who, string.Concat(new string[]
						{
							who.Does("butcher", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
							" ",
							gameObject.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
							" from ",
							this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
							(text == null) ? "" : (" " + text),
							"."
						}), ' ', !Automatic, false, false, null, null);
					}
					if (flag2)
					{
						who.TakeObject(gameObject, false, true, new int?(0), null, Tracking);
					}
					else
					{
						currentCell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					}
				}
				else if (who.IsPlayer())
				{
					IComponent<GameObject>.EmitMessage(who, string.Concat(new string[]
					{
						"{{r|You rip ",
						gameObject.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
						" out of ",
						this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
						(text == null) ? "" : (" " + text),
						", but destroy ",
						gameObject.them,
						" in the process.}}"
					}), ' ', !Automatic, false, false, null, null);
				}
				else if (IComponent<GameObject>.Visible(who))
				{
					IComponent<GameObject>.EmitMessage(who, string.Concat(new string[]
					{
						who.Does("rip", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
						" ",
						gameObject.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
						" out of ",
						this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
						(text == null) ? "" : (" " + text),
						", but ",
						who.GetVerb("destroy", false, false, false, true, false),
						" ",
						gameObject.them,
						" in the process."
					}), ' ', !Automatic, false, false, null, null);
				}
			}
			this.Cybernetics.Clear();
			this.ParentObject.Bloodsplatter(false);
			this.ParentObject.Destroy(null, false, false, null);
			who.UseEnergy(1000, "Skill", null, null, false);
			return true;
		}

		// Token: 0x06009A42 RID: 39490 RVA: 0x0038001C File Offset: 0x0037E21C
		public bool IsButcherable()
		{
			return this.Cybernetics.Count > 0 && this.ParentObject.Render.Visible && !this.ParentObject.HasPart<HologramMaterial>();
		}

		// Token: 0x04004035 RID: 16437
		public int BaseSuccessChance = 80;

		// Token: 0x04004036 RID: 16438
		[NonSerialized]
		public List<GameObject> Cybernetics = new List<GameObject>(1);
	}
}
