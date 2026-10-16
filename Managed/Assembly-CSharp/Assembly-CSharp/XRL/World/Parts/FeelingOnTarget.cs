using System;
using XRL.World.AI;

namespace XRL.World.Parts
{
	// Token: 0x02001029 RID: 4137
	[Serializable]
	public class FeelingOnTarget : IActivePart
	{
		// Token: 0x0600A4C9 RID: 42185 RVA: 0x003C4396 File Offset: 0x003C2596
		public FeelingOnTarget()
		{
			this.WorksOnHolder = true;
			this.WorksOnCarrier = true;
		}

		// Token: 0x0600A4CA RID: 42186 RVA: 0x003C43B4 File Offset: 0x003C25B4
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetShortDescriptionEvent.ID || ID == EquippedEvent.ID || ID == UnequippedEvent.ID || ID == DroppedEvent.ID || ID == TakenEvent.ID;
		}

		// Token: 0x0600A4CB RID: 42187 RVA: 0x003C43EC File Offset: 0x003C25EC
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.Chance > 0)
			{
				E.Postfix.Compound("{{rules|", '\n').Append(this.Chance).Append('%')
					.Append(" chance for hostile creatures to wander away disinterestedly when they first notice you}}");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A4CC RID: 42188 RVA: 0x003C4438 File Offset: 0x003C2638
		public override bool HandleEvent(EquippedEvent E)
		{
			this.EvaluateAsSubject(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A4CD RID: 42189 RVA: 0x003C444D File Offset: 0x003C264D
		public override bool HandleEvent(UnequippedEvent E)
		{
			this.EvaluateAsSubject(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A4CE RID: 42190 RVA: 0x003C4462 File Offset: 0x003C2662
		public override bool HandleEvent(TakenEvent E)
		{
			this.EvaluateAsSubject(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A4CF RID: 42191 RVA: 0x003C4477 File Offset: 0x003C2677
		public override bool HandleEvent(DroppedEvent E)
		{
			this.EvaluateAsSubject(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A4D0 RID: 42192 RVA: 0x003C448C File Offset: 0x003C268C
		public void EvaluateAsSubject(GameObject Object)
		{
			if (this.IsObjectActivePartSubject(Object))
			{
				Object.RegisterPartEvent(this, "AITargetCreateKill");
				return;
			}
			Object.UnregisterPartEvent(this, "AITargetCreateKill");
		}

		// Token: 0x0600A4D1 RID: 42193 RVA: 0x003C44B0 File Offset: 0x003C26B0
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AITargetCreateKill")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Actor");
				if (gameObjectParameter == null || gameObjectParameter.HasIntProperty("AsteriskNeutral"))
				{
					return true;
				}
				GameObject activePartFirstSubject = this.GetActivePartFirstSubject();
				if (activePartFirstSubject == null || base.IsDisabled(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
				{
					return true;
				}
				if (!this.CalmHate)
				{
					Faction ifExists = Factions.GetIfExists(gameObjectParameter.GetPrimaryFaction(false));
					if (ifExists != null && ifExists.HatesPlayer)
					{
						return true;
					}
				}
				gameObjectParameter.SetIntProperty("AsteriskNeutral", 1, false);
				if (this.Chance.in100())
				{
					gameObjectParameter.StopFighting(activePartFirstSubject, false);
					gameObjectParameter.AddOpinion<OpinionMollify>(activePartFirstSubject, (float)this.Feeling);
					IComponent<GameObject>.XDidYToZ(gameObjectParameter, "decide", gameObjectParameter.it + gameObjectParameter.GetVerb("aren't", true, true, false, true, false) + " angry at", activePartFirstSubject, null, null, null, null, null, null, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
					return false;
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004439 RID: 17465
		public int Chance;

		// Token: 0x0400443A RID: 17466
		public int Feeling = 25;

		// Token: 0x0400443B RID: 17467
		public bool CalmHate;
	}
}
