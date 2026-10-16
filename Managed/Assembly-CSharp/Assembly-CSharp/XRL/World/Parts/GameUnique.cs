using System;
using System.Collections.Generic;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02000FBD RID: 4029
	[Serializable]
	public class GameUnique : IPart
	{
		// Token: 0x0600A01E RID: 40990 RVA: 0x003A46C1 File Offset: 0x003A28C1
		public override void Initialize()
		{
			if (this.State.IsNullOrEmpty())
			{
				this.State = this.ParentObject.Blueprint;
			}
		}

		// Token: 0x0600A01F RID: 40991 RVA: 0x003A46E1 File Offset: 0x003A28E1
		public override IPart DeepCopy(GameObject Parent)
		{
			GameUnique gameUnique = (GameUnique)base.DeepCopy(Parent);
			gameUnique.Activated = false;
			return gameUnique;
		}

		// Token: 0x0600A020 RID: 40992 RVA: 0x003A46F6 File Offset: 0x003A28F6
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ObjectCreatedEvent.ID || ID == ZoneActivatedEvent.ID || ID == ZoneDeactivatedEvent.ID || ID == AfterDieEvent.ID || ID == ReplicaCreatedEvent.ID;
		}

		// Token: 0x0600A021 RID: 40993 RVA: 0x003A472C File Offset: 0x003A292C
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			this.OnCreated(E.Context);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A022 RID: 40994 RVA: 0x003A4741 File Offset: 0x003A2941
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			this.CheckState();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A023 RID: 40995 RVA: 0x003A4750 File Offset: 0x003A2950
		public override bool HandleEvent(ZoneDeactivatedEvent E)
		{
			this.CheckState();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A024 RID: 40996 RVA: 0x003A475F File Offset: 0x003A295F
		public override bool HandleEvent(AfterDieEvent E)
		{
			if (!this.Triggered)
			{
				this.SetDead(E.Killer);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A025 RID: 40997 RVA: 0x003A477C File Offset: 0x003A297C
		public override bool HandleEvent(ReplicaCreatedEvent E)
		{
			if (E.Object == this.ParentObject)
			{
				E.WantToRemove(this);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A026 RID: 40998 RVA: 0x003A479A File Offset: 0x003A299A
		public override void Remove()
		{
			this.State = null;
		}

		// Token: 0x0600A027 RID: 40999 RVA: 0x003A47A4 File Offset: 0x003A29A4
		public void OnCreated(string Context = null)
		{
			XRLGame game = The.Game;
			if (game == null || this.State.IsNullOrEmpty())
			{
				return;
			}
			string stringGameState = game.GetStringGameState(this.State, null);
			if (Context == "Wish" && stringGameState != null && Popup.ShowYesNo(this.ParentObject.T(2147483647, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " (" + this.ParentObject.Blueprint + ") is considered unique, are you sure you want to create another?", "Sounds/UI/ui_notification", true, DialogResult.No, null) == DialogResult.Yes)
			{
				game.SetStringGameState(this.State, this.ParentObject.ID);
				if (!this.DeathState.IsNullOrEmpty())
				{
					game.RemoveBooleanGameState(this.DeathState);
				}
			}
			GameManager.Instance.gameQueue.queueTask(new Action(this.CheckState), 0);
		}

		// Token: 0x0600A028 RID: 41000 RVA: 0x003A488C File Offset: 0x003A2A8C
		public void CheckState()
		{
			if (this.State.IsNullOrEmpty() || this.ParentObject == null || this.ParentObject.IsTemporary)
			{
				this.Activated = true;
				return;
			}
			string stringGameState = The.Game.GetStringGameState(this.State, null);
			if (this.Activated)
			{
				if (!stringGameState.HasDelimitedSubstring(',', this.ParentObject.ID, StringComparison.Ordinal))
				{
					this.Obliterate();
					return;
				}
			}
			else
			{
				if (stringGameState != null && (!this.Replace || stringGameState.HasDelimitedSubstring(',', "Dead", StringComparison.Ordinal) || (stringGameState != this.ParentObject.ID && The.ActiveZone.FindObjectByID(stringGameState) != null)))
				{
					this.Obliterate();
				}
				else
				{
					The.Game.SetStringGameState(this.State, this.ParentObject.ID);
				}
				this.Activated = true;
			}
		}

		// Token: 0x0600A029 RID: 41001 RVA: 0x003A495F File Offset: 0x003A2B5F
		public bool IsBondedCompanion(GameObject Object)
		{
			return Object.Brain != null && Object.IsBondedBy(this.ParentObject);
		}

		// Token: 0x0600A02A RID: 41002 RVA: 0x003A4978 File Offset: 0x003A2B78
		public void Obliterate()
		{
			Zone currentZone = this.ParentObject.CurrentZone;
			List<GameObject> list = ((currentZone != null) ? currentZone.FindObjects(new Predicate<GameObject>(this.IsBondedCompanion)) : null);
			if (!list.IsNullOrEmpty<GameObject>())
			{
				foreach (GameObject gameObject in list)
				{
					gameObject.Obliterate(null, true, null);
				}
			}
			this.ParentObject.Obliterate(null, true, null);
		}

		// Token: 0x0600A02B RID: 41003 RVA: 0x003A4A04 File Offset: 0x003A2C04
		public void SetDead(GameObject Killer = null)
		{
			if (!this.State.IsNullOrEmpty() && !this.ParentObject.IsTemporary)
			{
				The.Game.TryAddDelimitedGameState(this.State, ',', "Dead");
				if (Killer != null && Killer.IsPlayerControlled())
				{
					The.Game.TryAddDelimitedGameState(this.State, ',', "KilledByPlayer");
				}
				if (!this.DeathState.IsNullOrEmpty())
				{
					The.Game.SetBooleanGameState(this.DeathState, true);
				}
			}
			this.Triggered = true;
		}

		// Token: 0x04004292 RID: 17042
		public string State;

		// Token: 0x04004293 RID: 17043
		public string DeathState;

		// Token: 0x04004294 RID: 17044
		public bool Replace = true;

		// Token: 0x04004295 RID: 17045
		public bool Activated;

		// Token: 0x04004296 RID: 17046
		[NonSerialized]
		private bool Triggered;
	}
}
