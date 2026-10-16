using System;
using System.Collections.Generic;
using System.Threading;
using ConsoleLib.Console;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.World.Anatomy;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001408 RID: 5128
	[Serializable]
	public class BreatherBase : BaseMutation
	{
		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x0600C725 RID: 50981 RVA: 0x0048999A File Offset: 0x00487B9A
		// (set) Token: 0x0600C726 RID: 50982 RVA: 0x004899A2 File Offset: 0x00487BA2
		[Obsolete("Use ActivatedAbilityID - will not remove before Q1 2025")]
		public Guid ActivatedAbilityId
		{
			get
			{
				return this.ActivatedAbilityID;
			}
			set
			{
				this.ActivatedAbilityID = value;
			}
		}

		// Token: 0x0600C727 RID: 50983 RVA: 0x004899AB File Offset: 0x00487BAB
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			base.Read(Basis, Reader);
			if (this.SaveCompatSlot != Guid.Empty && this.ActivatedAbilityID == Guid.Empty)
			{
				this.ActivatedAbilityID = this.SaveCompatSlot;
			}
		}

		// Token: 0x0600C728 RID: 50984 RVA: 0x004899E5 File Offset: 0x00487BE5
		public override bool GeneratesEquipment()
		{
			return this.CreateObject && this.GetFaceObject() != null;
		}

		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x0600C72A RID: 50986 RVA: 0x00489A1F File Offset: 0x00487C1F
		public string CommandID
		{
			get
			{
				if (this._CommandID == null)
				{
					this._CommandID = "CommandBreathe_" + base.GetType().Name;
				}
				return this._CommandID;
			}
		}

		// Token: 0x0600C72B RID: 50987 RVA: 0x00489A4A File Offset: 0x00487C4A
		public virtual string GetFaceObject()
		{
			return null;
		}

		// Token: 0x0600C72C RID: 50988 RVA: 0x00489A4D File Offset: 0x00487C4D
		public virtual string GetGasBlueprint()
		{
			return null;
		}

		// Token: 0x0600C72D RID: 50989 RVA: 0x00489A50 File Offset: 0x00487C50
		public virtual string GetBreathName()
		{
			return null;
		}

		// Token: 0x0600C72E RID: 50990 RVA: 0x00489A53 File Offset: 0x00487C53
		public virtual string GetCommandDisplayName()
		{
			return "[BreatherBase::GetCommandDisplayName]";
		}

		// Token: 0x0600C72F RID: 50991 RVA: 0x00489A5A File Offset: 0x00487C5A
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID;
		}

		// Token: 0x0600C730 RID: 50992 RVA: 0x00489A70 File Offset: 0x00487C70
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			base.CollectStats(stats, Level);
			ActivatedAbilityEntry activatedAbilityEntry = base.MyActivatedAbility(this.ActivatedAbilityID, this.ParentObject);
			if (activatedAbilityEntry != null)
			{
				stats.CollectCooldownTurns(activatedAbilityEntry, this.GetCooldownTurns(Level), 0);
				stats.Set("ConeLength", this.GetConeLength(Level), false, 0);
				stats.Set("ConeAngle", this.GetConeAngle(Level), false, 0);
			}
		}

		// Token: 0x0600C731 RID: 50993 RVA: 0x00489AD4 File Offset: 0x00487CD4
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (E.Distance <= this.GetConeLength(-1) - 2 && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && GameObject.Validate(E.Target) && E.Actor.HasLOSTo(E.Target, true, false, true, true, null))
			{
				E.Add(this.CommandID, 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C732 RID: 50994 RVA: 0x00489B3F File Offset: 0x00487D3F
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register(this.CommandID);
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C733 RID: 50995 RVA: 0x00489B55 File Offset: 0x00487D55
		public virtual int GetCooldownTurns(int Level)
		{
			return 15;
		}

		// Token: 0x0600C734 RID: 50996 RVA: 0x00489B59 File Offset: 0x00487D59
		public virtual int GetConeLength(int L = -1)
		{
			if (L == -1)
			{
				return 4 + base.Level;
			}
			return 4 + L;
		}

		// Token: 0x0600C735 RID: 50997 RVA: 0x00489B6B File Offset: 0x00487D6B
		public virtual int GetConeAngle(int L = -1)
		{
			if (L == -1)
			{
				return 20 + 2 * base.Level;
			}
			return 20 + 2 * L;
		}

		// Token: 0x0600C736 RID: 50998 RVA: 0x00489B83 File Offset: 0x00487D83
		public virtual void BreatheInCell(Cell C, ScreenBuffer Buffer, bool doEffect = true)
		{
		}

		// Token: 0x0600C737 RID: 50999 RVA: 0x00489B88 File Offset: 0x00487D88
		public void DrawBreathInCell(Cell C, ScreenBuffer Buffer, string color1, string color2, string color3)
		{
			Buffer.Goto(C.X, C.Y);
			string text = "&G";
			int num = Stat.Random(1, 3);
			if (num == 1)
			{
				text = "&" + color1;
			}
			if (num == 2)
			{
				text = "&" + color2;
			}
			if (num == 3)
			{
				text = "&" + color3;
			}
			int num2 = Stat.Random(1, 3);
			if (num2 == 1)
			{
				text = text + "^" + color1;
			}
			if (num2 == 2)
			{
				text = text + "^" + color2;
			}
			if (num2 == 3)
			{
				text = text + "^" + color3;
			}
			if (C.ParentZone == XRLCore.Core.Game.ZoneManager.ActiveZone)
			{
				Stat.Random(1, 3);
				Buffer.Write(text + ((char)(219 + Stat.Random(0, 4))).ToString(), true, false, false, null, -1);
				Popup._TextConsole.DrawBuffer(Buffer, null, false);
				Thread.Sleep(Math.Max(1, 15 - base.Level));
			}
		}

		// Token: 0x0600C738 RID: 51000 RVA: 0x00489C90 File Offset: 0x00487E90
		public virtual Gas BreatheGasInCell(Cell C, Event CreatorEvent)
		{
			string gasBlueprint = this.GetGasBlueprint();
			if (gasBlueprint == null)
			{
				return null;
			}
			Gas part = C.AddObject(gasBlueprint, null, null, null, null).GetPart<Gas>();
			part.Creator = this.ParentObject;
			CreatorEvent.SetParameter("Gas", part);
			return part;
		}

		// Token: 0x0600C739 RID: 51001 RVA: 0x00489CD4 File Offset: 0x00487ED4
		public virtual int SortCells(Cell c1, Cell c2)
		{
			return c1.PathDistanceTo(this.ParentObject.Physics.CurrentCell).CompareTo(c2.PathDistanceTo(this.ParentObject.Physics.CurrentCell));
		}

		// Token: 0x0600C73A RID: 51002 RVA: 0x00489D18 File Offset: 0x00487F18
		public static bool Cast(BreatherBase mutation = null)
		{
			if (mutation == null)
			{
				return false;
			}
			ScreenBuffer scrapBuffer = ScreenBuffer.GetScrapBuffer1(true);
			XRLCore.Core.RenderMapToBuffer(scrapBuffer);
			List<Cell> list = mutation.PickCone(mutation.GetConeLength(-1), mutation.GetConeAngle(-1), AllowVis.Any, null, ((mutation != null) ? mutation.GetCommandDisplayName() : null) ?? "Breathe");
			if (list == null || list.Count <= 0)
			{
				return false;
			}
			if (list.Count == 1 && mutation.ParentObject.IsPlayer() && Popup.ShowYesNoCancel("Are you sure you want to target " + mutation.ParentObject.itself + "?", "Sounds/UI/ui_notification", true, DialogResult.Cancel) != DialogResult.Yes)
			{
				return false;
			}
			mutation.UseEnergy(1000);
			mutation.CooldownMyActivatedAbility(mutation.ActivatedAbilityID, mutation.GetCooldownTurns(mutation.Level), null, null, false);
			list.Sort(new Comparison<Cell>(mutation.SortCells));
			if (mutation != null)
			{
				GameObject parentObject = mutation.ParentObject;
				if (parentObject != null)
				{
					parentObject.PlayWorldSound("Sounds/Abilities/sfx_ability_gas_breathe", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				}
			}
			if (mutation.ParentObject.IsPlayer())
			{
				IComponent<GameObject>.XDidY(mutation.ParentObject, "breath", "a cone of " + mutation.GetBreathName(), "!", null, null, mutation.ParentObject, null, false, false, null, null, false, false, false, false, false, null);
			}
			else
			{
				IComponent<GameObject>.XDidY(mutation.ParentObject, "breath", "a cone of " + mutation.GetBreathName(), "!", null, null, mutation.ParentObject, null, false, false, null, null, true, false, false, false, false, null);
			}
			string gasBlueprint = mutation.GetGasBlueprint();
			Event @event = ((gasBlueprint != null) ? Event.New("CreatorModifyGas", "Gas", null) : null);
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				if ((i == 0 || mutation.ParentObject.HasLOSTo(list[i], true, false, false, true, null)) && (list.Count == 1 || list[i] != mutation.ParentObject.CurrentCell))
				{
					if (gasBlueprint != null)
					{
						mutation.BreatheGasInCell(list[i], @event);
						mutation.ParentObject.FireEvent(@event);
					}
					mutation.BreatheInCell(list[i], scrapBuffer, true);
				}
				i++;
			}
			return true;
		}

		// Token: 0x0600C73B RID: 51003 RVA: 0x00489F4B File Offset: 0x0048814B
		public override bool FireEvent(Event E)
		{
			return (!(E.ID == this.CommandID) || BreatherBase.Cast(this)) && base.FireEvent(E);
		}

		// Token: 0x0600C73C RID: 51004 RVA: 0x00489F71 File Offset: 0x00488171
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C73D RID: 51005 RVA: 0x00489F7A File Offset: 0x0048817A
		public virtual bool OnMutate(GameObject GO)
		{
			return true;
		}

		// Token: 0x0600C73E RID: 51006 RVA: 0x00489F7D File Offset: 0x0048817D
		public virtual bool OnUnmutate(GameObject GO)
		{
			return true;
		}

		// Token: 0x0600C73F RID: 51007 RVA: 0x00489F80 File Offset: 0x00488180
		public override bool Mutate(GameObject GO, int Level)
		{
			this.Unmutate(GO);
			this.OnMutate(GO);
			if (this.CreateObject && this.GetFaceObject() != null)
			{
				Body body = GO.Body;
				if (body != null)
				{
					foreach (BodyPart bodyPart in body.GetPart(this.BodyPartType))
					{
						if (bodyPart.ForceUnequip(true, false, false))
						{
							this.FaceObject = GameObject.Create(this.GetFaceObject());
							this.FaceObject.GetPart<Armor>().WornOn = bodyPart.Type;
							GO.ForceEquipObject(this.FaceObject, bodyPart, true, new int?(0));
							break;
						}
					}
				}
			}
			this.ActivatedAbilityID = base.AddMyActivatedAbility(this.GetCommandDisplayName(), this.CommandID, "Physical Mutations", this.GetDescription(), "\u00ad", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C740 RID: 51008 RVA: 0x0048A090 File Offset: 0x00488290
		public override bool Unmutate(GameObject GO)
		{
			this.OnUnmutate(GO);
			base.CleanUpMutationEquipment(GO, ref this.FaceObject);
			if (GameObject.Validate(ref this.FaceObject))
			{
				this.FaceObject.Obliterate(null, false, null);
			}
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.Unmutate(GO);
		}

		// Token: 0x04004E59 RID: 20057
		public string BodyPartType = "Face";

		// Token: 0x04004E5A RID: 20058
		public bool CreateObject = true;

		// Token: 0x04004E5B RID: 20059
		public GameObject FaceObject;

		// Token: 0x04004E5C RID: 20060
		[Obsolete("Save Compat Only")]
		public Guid SaveCompatSlot = Guid.Empty;

		// Token: 0x04004E5D RID: 20061
		[NonSerialized]
		public string _CommandID;
	}
}
