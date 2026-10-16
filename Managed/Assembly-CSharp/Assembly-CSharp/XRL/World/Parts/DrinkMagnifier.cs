using System;

namespace XRL.World.Parts
{
	// Token: 0x02001020 RID: 4128
	[Serializable]
	public class DrinkMagnifier : IActivePart
	{
		// Token: 0x0600A465 RID: 42085 RVA: 0x003C1C28 File Offset: 0x003BFE28
		public DrinkMagnifier()
		{
			this.WorksOnHolder = true;
			this.WorksOnCarrier = true;
		}

		// Token: 0x0600A466 RID: 42086 RVA: 0x003C1C49 File Offset: 0x003BFE49
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetShortDescriptionEvent.ID || ID == EquippedEvent.ID || ID == UnequippedEvent.ID || ID == DroppedEvent.ID || ID == TakenEvent.ID;
		}

		// Token: 0x0600A467 RID: 42087 RVA: 0x003C1C80 File Offset: 0x003BFE80
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.Chance > 0)
			{
				E.Postfix.Compound("{{rules|", '\n').Append(this.Chance).Append('%')
					.Append(" chance for liquid-drinking effects to apply twice}}");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A468 RID: 42088 RVA: 0x003C1CCC File Offset: 0x003BFECC
		public override bool HandleEvent(EquippedEvent E)
		{
			this.EvaluateAsSubject(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A469 RID: 42089 RVA: 0x003C1CE1 File Offset: 0x003BFEE1
		public override bool HandleEvent(UnequippedEvent E)
		{
			this.EvaluateAsSubject(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A46A RID: 42090 RVA: 0x003C1CF6 File Offset: 0x003BFEF6
		public override bool HandleEvent(TakenEvent E)
		{
			this.EvaluateAsSubject(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A46B RID: 42091 RVA: 0x003C1D0B File Offset: 0x003BFF0B
		public override bool HandleEvent(DroppedEvent E)
		{
			this.EvaluateAsSubject(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A46C RID: 42092 RVA: 0x003C1D20 File Offset: 0x003BFF20
		public void EvaluateAsSubject(GameObject Object)
		{
			if (this.IsObjectActivePartSubject(Object))
			{
				Object.RegisterPartEvent(this, "ModifyDrinkEffects");
				return;
			}
			Object.UnregisterPartEvent(this, "ModifyDrinkEffects");
		}

		// Token: 0x0600A46D RID: 42093 RVA: 0x003C1D44 File Offset: 0x003BFF44
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ModifyDrinkEffects" && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) && this.Chance.in100())
			{
				int intParameter = E.GetIntParameter("Magnitude", 0);
				E.SetParameter("Magnitude", intParameter * this.Percent / 100);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600A46E RID: 42094 RVA: 0x003C1DC4 File Offset: 0x003BFFC4
		public static int Magnify(GameObject Target, int Magnitude = 100)
		{
			if (Target.HasRegisteredEvent("ModifyDrinkEffects"))
			{
				Event @event = Event.New("ModifyDrinkEffects", "Magnitude", Magnitude);
				Target.FireEvent(@event);
				Magnitude = @event.GetIntParameter("Magnitude", 0);
			}
			return Magnitude;
		}

		// Token: 0x04004425 RID: 17445
		public int Chance;

		// Token: 0x04004426 RID: 17446
		public int Percent = 200;
	}
}
