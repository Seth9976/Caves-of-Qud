using System;
using XRL.World.Anatomy;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x020011CD RID: 4557
	[Serializable]
	public abstract class IEnergyCell : IRechargeable, IContextRelationManager
	{
		// Token: 0x0600B523 RID: 46371
		public abstract bool HasAnyCharge();

		// Token: 0x0600B524 RID: 46372
		public abstract bool HasCharge(int Amount);

		// Token: 0x0600B525 RID: 46373
		public abstract int GetCharge();

		// Token: 0x0600B526 RID: 46374
		public abstract int GetChargePercentage();

		// Token: 0x0600B527 RID: 46375
		public abstract string ChargeStatus();

		// Token: 0x0600B528 RID: 46376
		public abstract void TinkerInitialize();

		// Token: 0x0600B529 RID: 46377
		public abstract void UseCharge(int Amount);

		// Token: 0x0600B52A RID: 46378
		public abstract void SetChargePercentage(int Percentage);

		// Token: 0x0600B52B RID: 46379
		public abstract void RandomizeCharge();

		// Token: 0x0600B52C RID: 46380
		public abstract void MaximizeCharge();

		// Token: 0x0600B52D RID: 46381 RVA: 0x004218C2 File Offset: 0x0041FAC2
		public virtual string GetSlotTypeName()
		{
			return EnergyStorage.GetSlotTypeName(this.SlotType);
		}

		// Token: 0x0600B52E RID: 46382 RVA: 0x004218CF File Offset: 0x0041FACF
		public virtual string GetSlotTypeShortName()
		{
			return EnergyStorage.GetSlotTypeShortName(this.SlotType);
		}

		// Token: 0x0600B52F RID: 46383 RVA: 0x004218DC File Offset: 0x0041FADC
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetContextEvent>.ID || ID == RemoveFromContextEvent.ID || ID == PooledEvent<ReplaceInContextEvent>.ID || ID == TryRemoveFromContextEvent.ID;
		}

		// Token: 0x0600B530 RID: 46384 RVA: 0x0042190A File Offset: 0x0041FB0A
		public override bool HandleEvent(GetContextEvent E)
		{
			if (GameObject.Validate(ref this.SlottedIn))
			{
				E.ObjectContext = this.SlottedIn;
				E.Relation = 5;
				E.RelationManager = this;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B531 RID: 46385 RVA: 0x0042193C File Offset: 0x0041FB3C
		public override bool HandleEvent(ReplaceInContextEvent E)
		{
			this.ReplaceCell(E.Replacement);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B532 RID: 46386 RVA: 0x00421951 File Offset: 0x0041FB51
		public override bool HandleEvent(RemoveFromContextEvent E)
		{
			this.ReplaceCell(null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B533 RID: 46387 RVA: 0x00421961 File Offset: 0x0041FB61
		public override bool HandleEvent(TryRemoveFromContextEvent E)
		{
			this.ReplaceCell(null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B534 RID: 46388 RVA: 0x00421974 File Offset: 0x0041FB74
		private void ReplaceCell(GameObject Replacement)
		{
			EnergyCellSocket energyCellSocket;
			if (GameObject.Validate(ref this.SlottedIn) && this.SlottedIn.TryGetPart<EnergyCellSocket>(out energyCellSocket) && energyCellSocket.Cell == this.ParentObject)
			{
				energyCellSocket.SetCell(Replacement);
			}
		}

		// Token: 0x0600B535 RID: 46389 RVA: 0x004219B2 File Offset: 0x0041FBB2
		public GameObject GetSlottedIn()
		{
			GameObject.Validate(ref this.SlottedIn);
			return this.SlottedIn;
		}

		// Token: 0x0600B536 RID: 46390 RVA: 0x004219C8 File Offset: 0x0041FBC8
		public bool RestoreContextRelation(GameObject Object, GameObject ObjectContext, Cell CellContext, BodyPart BodyPartContext, int Relation, bool Silent = true)
		{
			if (Relation == 5 && ObjectContext != null)
			{
				EnergyCellSocket part = this.SlottedIn.GetPart<EnergyCellSocket>();
				if (part != null)
				{
					if (part.Cell != Object || this.SlottedIn != ObjectContext)
					{
						part.SetCell(Object);
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x04004909 RID: 18697
		public string SlotType = "EnergyCell";

		// Token: 0x0400490A RID: 18698
		public GameObject SlottedIn;
	}
}
