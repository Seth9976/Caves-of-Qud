using System;
using System.Collections.Generic;
using XRL.World.Anatomy;

namespace XRL.World.Parts
{
	// Token: 0x02000EBC RID: 3772
	[Serializable]
	public class GiantHands : IActivePart
	{
		// Token: 0x06009750 RID: 38736 RVA: 0x0036E548 File Offset: 0x0036C748
		public GiantHands()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x06009751 RID: 38737 RVA: 0x0036E557 File Offset: 0x0036C757
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetSlotsRequiredEvent>.ID || ID == ImplantedEvent.ID || ID == UnimplantedEvent.ID;
		}

		// Token: 0x06009752 RID: 38738 RVA: 0x0036E580 File Offset: 0x0036C780
		public override bool HandleEvent(GetSlotsRequiredEvent E)
		{
			if (Array.IndexOf<string>(GiantHands.AffectedSlotTypes, E.SlotType) >= 0 && this.IsObjectActivePartSubject(E.Actor) && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				E.Decreases++;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009753 RID: 38739 RVA: 0x0036E5EB File Offset: 0x0036C7EB
		public override bool HandleEvent(ImplantedEvent E)
		{
			GameObject implantee = E.Implantee;
			BodyPart part = E.Part;
			this.CheckAffected(implantee, (part != null) ? part.ParentBody : null);
			return base.HandleEvent(E);
		}

		// Token: 0x06009754 RID: 38740 RVA: 0x0036E612 File Offset: 0x0036C812
		public override bool HandleEvent(UnimplantedEvent E)
		{
			GameObject implantee = E.Implantee;
			BodyPart part = E.Part;
			this.CheckAffected(implantee, (part != null) ? part.ParentBody : null);
			return base.HandleEvent(E);
		}

		// Token: 0x06009755 RID: 38741 RVA: 0x0036E63C File Offset: 0x0036C83C
		public void CheckAffected(GameObject Actor, Body Body)
		{
			if (Actor == null || Body == null)
			{
				return;
			}
			List<GameObject> list = Event.NewGameObjectList();
			foreach (BodyPart bodyPart in Body.LoopParts())
			{
				if (Array.IndexOf<string>(GiantHands.AffectedSlotTypes, bodyPart.Type) >= 0)
				{
					GameObject equipped = bodyPart.Equipped;
					if (equipped != null && !list.Contains(equipped))
					{
						list.Add(equipped);
						int partCountEquippedOn = Body.GetPartCountEquippedOn(equipped);
						int slotsRequiredFor = equipped.GetSlotsRequiredFor(Actor, bodyPart.Type, true);
						if (partCountEquippedOn != slotsRequiredFor && bodyPart.TryUnequip(true, true, false, false) && partCountEquippedOn > slotsRequiredFor)
						{
							equipped.SplitFromStack();
							bodyPart.Equip(equipped, new int?(0), true, false, false, true);
						}
					}
				}
			}
		}

		// Token: 0x06009756 RID: 38742 RVA: 0x0036E70C File Offset: 0x0036C90C
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x04003EEA RID: 16106
		private static readonly string[] AffectedSlotTypes = new string[] { "Hand", "Hands", "Missile Weapon" };
	}
}
