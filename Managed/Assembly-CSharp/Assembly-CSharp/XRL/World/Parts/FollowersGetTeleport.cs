using System;
using System.Collections.Generic;
using System.Linq;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x0200102F RID: 4143
	[Serializable]
	public class FollowersGetTeleport : IActivePart
	{
		// Token: 0x0600A4EC RID: 42220 RVA: 0x003C4A6D File Offset: 0x003C2C6D
		public FollowersGetTeleport()
		{
			this.ChargeUse = 0;
			this.IsBootSensitive = true;
			this.IsEMPSensitive = true;
			this.MustBeUnderstood = true;
			this.WorksOnWearer = true;
		}

		// Token: 0x0600A4ED RID: 42221 RVA: 0x003C4A98 File Offset: 0x003C2C98
		public virtual void ApplyEffectTo(GameObject go)
		{
		}

		// Token: 0x0600A4EE RID: 42222 RVA: 0x003C4A9A File Offset: 0x003C2C9A
		public virtual void UnapplyEffectTo(GameObject go)
		{
		}

		// Token: 0x0600A4EF RID: 42223 RVA: 0x003C4A9C File Offset: 0x003C2C9C
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600A4F0 RID: 42224 RVA: 0x003C4A9F File Offset: 0x003C2C9F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<EndTurnEvent>.ID || ID == EquippedEvent.ID || (ID == GetShortDescriptionEvent.ID && !string.IsNullOrEmpty(this.BehaviorDescription)) || ID == UnequippedEvent.ID;
		}

		// Token: 0x0600A4F1 RID: 42225 RVA: 0x003C4ADA File Offset: 0x003C2CDA
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(this.BehaviorDescription);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A4F2 RID: 42226 RVA: 0x003C4AF5 File Offset: 0x003C2CF5
		public override bool HandleEvent(UnequippedEvent E)
		{
			return base.HandleEvent(E);
		}

		// Token: 0x0600A4F3 RID: 42227 RVA: 0x003C4AFE File Offset: 0x003C2CFE
		public override bool HandleEvent(EquippedEvent E)
		{
			return base.HandleEvent(E);
		}

		// Token: 0x0600A4F4 RID: 42228 RVA: 0x003C4B08 File Offset: 0x003C2D08
		public override bool HandleEvent(EndTurnEvent E)
		{
			if (base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				GameObject equipped = this.ParentObject.Equipped;
				if (equipped != null)
				{
					Cell currentCell = equipped.Physics.CurrentCell;
					if (currentCell != null)
					{
						IEnumerable<GameObject> objectsWithPart = currentCell.ParentZone.GetObjectsWithPart("Combat");
						Func<GameObject, bool> func;
						Func<GameObject, bool> <>9__0;
						if ((func = <>9__0) == null)
						{
							func = (<>9__0 = (GameObject o) => o.Brain != null && o.Brain.PartyLeader == equipped);
						}
						foreach (GameObject gameObject in objectsWithPart.Where<GameObject>(func))
						{
							IsAFollowerGettingTeleport isAFollowerGettingTeleport;
							if (gameObject.TryGetEffect<IsAFollowerGettingTeleport>(out isAFollowerGettingTeleport))
							{
								isAFollowerGettingTeleport.Duration = 3;
							}
							else
							{
								gameObject.ApplyEffect(new IsAFollowerGettingTeleport(), null);
							}
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04004443 RID: 17475
		public string BehaviorDescription;
	}
}
