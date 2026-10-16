using System;
using XRL.World.Tinkering;

namespace XRL.World.Parts
{
	// Token: 0x0200104B RID: 4171
	[Serializable]
	public class HighBitBonus : IActivePart
	{
		// Token: 0x0600A60B RID: 42507 RVA: 0x003CC8B6 File Offset: 0x003CAAB6
		public HighBitBonus()
		{
			this.WorksOnHolder = true;
			this.WorksOnCarrier = true;
		}

		// Token: 0x0600A60C RID: 42508 RVA: 0x003CC8CC File Offset: 0x003CAACC
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetShortDescriptionEvent.ID || ID == EquippedEvent.ID || ID == UnequippedEvent.ID || ID == DroppedEvent.ID || ID == TakenEvent.ID;
		}

		// Token: 0x0600A60D RID: 42509 RVA: 0x003CC904 File Offset: 0x003CAB04
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.Chance > 0 && this.Amount > 0)
			{
				E.Postfix.Compound("{{rules|", '\n').Append(this.Chance).Append("% chance when disassembling an artifact that you receive ")
					.Append(this.Amount)
					.Append(" extra bit of the highest tier");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A60E RID: 42510 RVA: 0x003CC967 File Offset: 0x003CAB67
		public override bool HandleEvent(EquippedEvent E)
		{
			this.EvaluateAsSubject(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A60F RID: 42511 RVA: 0x003CC97C File Offset: 0x003CAB7C
		public override bool HandleEvent(UnequippedEvent E)
		{
			this.EvaluateAsSubject(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A610 RID: 42512 RVA: 0x003CC991 File Offset: 0x003CAB91
		public override bool HandleEvent(TakenEvent E)
		{
			this.EvaluateAsSubject(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A611 RID: 42513 RVA: 0x003CC9A6 File Offset: 0x003CABA6
		public override bool HandleEvent(DroppedEvent E)
		{
			this.EvaluateAsSubject(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A612 RID: 42514 RVA: 0x003CC9BB File Offset: 0x003CABBB
		public void EvaluateAsSubject(GameObject Object)
		{
			if (this.IsObjectActivePartSubject(Object))
			{
				Object.RegisterPartEvent(this, "ModifyBitsReceived");
				return;
			}
			Object.UnregisterPartEvent(this, "ModifyBitsReceived");
		}

		// Token: 0x0600A613 RID: 42515 RVA: 0x003CC9E0 File Offset: 0x003CABE0
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ModifyBitsReceived" && this.Chance.in100())
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Item");
				if (gameObjectParameter == null || gameObjectParameter == this.ParentObject)
				{
					return true;
				}
				if (gameObjectParameter.HasIntProperty("TinkeredItem"))
				{
					return true;
				}
				string stringParameter = E.GetStringParameter("Bits", null);
				if (string.IsNullOrEmpty(stringParameter))
				{
					return true;
				}
				if (base.IsDisabled(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
				{
					return true;
				}
				char c = '\0';
				int num = -1;
				foreach (char c2 in stringParameter)
				{
					BitType bitType;
					if (BitType.BitMap.TryGetValue(c2, out bitType) && bitType.Level > num)
					{
						c = c2;
						num = bitType.Level;
					}
				}
				if (num >= 0)
				{
					E.SetParameter("Bits", stringParameter + new string(c, this.Amount));
					IComponent<GameObject>.PlayUISound("Sounds/Interact/sfx_artifact_examination_success_total", 1f, false, SoundRequest.SoundEffectType.None);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004497 RID: 17559
		public int Chance;

		// Token: 0x04004498 RID: 17560
		public int Amount;
	}
}
