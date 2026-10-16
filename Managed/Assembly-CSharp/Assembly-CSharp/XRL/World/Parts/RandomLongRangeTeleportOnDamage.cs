using System;

namespace XRL.World.Parts
{
	// Token: 0x02001087 RID: 4231
	[Serializable]
	public class RandomLongRangeTeleportOnDamage : IPoweredPart
	{
		// Token: 0x0600A85B RID: 43099 RVA: 0x003D91BD File Offset: 0x003D73BD
		public RandomLongRangeTeleportOnDamage()
		{
			this.ChargeUse = 0;
			this.IsRealityDistortionBased = true;
			this.WorksOnEquipper = true;
		}

		// Token: 0x0600A85C RID: 43100 RVA: 0x003D91E4 File Offset: 0x003D73E4
		public override bool SameAs(IPart p)
		{
			RandomLongRangeTeleportOnDamage randomLongRangeTeleportOnDamage = p as RandomLongRangeTeleportOnDamage;
			return randomLongRangeTeleportOnDamage.Chance == this.Chance && !(randomLongRangeTeleportOnDamage.Message != this.Message) && randomLongRangeTeleportOnDamage.AffectsUnavoidable == this.AffectsUnavoidable && randomLongRangeTeleportOnDamage.ShowInShortDescription == this.ShowInShortDescription && base.SameAs(p);
		}

		// Token: 0x0600A85D RID: 43101 RVA: 0x003D9244 File Offset: 0x003D7444
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ExamineCriticalFailureEvent.ID || ID == ExamineFailureEvent.ID || ID == TookDamageEvent.ID || (ID == GetShortDescriptionEvent.ID && this.ShowInShortDescription);
		}

		// Token: 0x0600A85E RID: 43102 RVA: 0x003D927C File Offset: 0x003D747C
		public override bool HandleEvent(TookDamageEvent E)
		{
			if (this.IsObjectActivePartSubject(E.Object) && (this.AffectsUnavoidable || !E.Damage.HasAttribute("Unavoidable")) && this.Chance.in100() && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				this.PerformTeleport(E.Object, true, E);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A85F RID: 43103 RVA: 0x003D92FC File Offset: 0x003D74FC
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.ShowInShortDescription)
			{
				string text = ((this.IsPowerSwitchSensitive && this.ParentObject.HasPart<PowerSwitch>()) ? "When activated, " : "");
				if (this.Chance > 0)
				{
					E.Postfix.AppendRules(string.Concat(new string[]
					{
						text,
						this.Chance.ToString(),
						"% chance of being randomly long-range teleported when taking ",
						this.AffectsUnavoidable ? "" : "avoidable ",
						"damage."
					}), base.GetEventSensitiveAddStatusSummary(E));
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A860 RID: 43104 RVA: 0x003D939D File Offset: 0x003D759D
		public override bool HandleEvent(ExamineFailureEvent E)
		{
			return !this.ExamineFailure(E, 25) && base.HandleEvent(E);
		}

		// Token: 0x0600A861 RID: 43105 RVA: 0x003D93B3 File Offset: 0x003D75B3
		public override bool HandleEvent(ExamineCriticalFailureEvent E)
		{
			return !this.ExamineFailure(E, 50) && base.HandleEvent(E);
		}

		// Token: 0x0600A862 RID: 43106 RVA: 0x003D93C9 File Offset: 0x003D75C9
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600A863 RID: 43107 RVA: 0x003D93CC File Offset: 0x003D75CC
		private bool ExamineFailure(IExamineEvent E, int Chance)
		{
			if (E.Pass == 1 && GlobalConfig.GetBoolSetting("ContextualExamineFailures", false) && Chance.in100() && base.IsReady(true, false, false, false, false, false, false, false, true, false, 1, null, false, 0L, null) && this.PerformTeleport(E.Actor, E.Actor.IsPlayer(), E))
			{
				E.Identify = true;
				E.IdentifyIfDestroyed = true;
				return true;
			}
			return false;
		}

		// Token: 0x0600A864 RID: 43108 RVA: 0x003D944C File Offset: 0x003D764C
		private bool PerformTeleport(GameObject Subject, bool UsePopups = true, IEvent FromEvent = null)
		{
			Cell anyBasisCell = this.GetAnyBasisCell();
			string text;
			if (anyBasisCell == null)
			{
				text = null;
			}
			else
			{
				Zone parentZone = anyBasisCell.ParentZone;
				text = ((parentZone != null) ? parentZone.GetZoneWorld() : null);
			}
			string randomDestinationZoneID = SpaceTimeVortex.GetRandomDestinationZoneID(text, true);
			if (randomDestinationZoneID == null)
			{
				return false;
			}
			if (!this.Message.IsNullOrEmpty())
			{
				string text2 = GameText.VariableReplace(this.Message, Subject, this.ParentObject, UsePopups);
				if (!text2.IsNullOrEmpty())
				{
					base.EmitMessage(text2, ' ', false, UsePopups, false, null, null);
				}
			}
			string text3 = randomDestinationZoneID;
			int num = -1;
			int num2 = -1;
			GameObject parentObject = this.ParentObject;
			return Subject.ZoneTeleport(text3, num, num2, FromEvent, parentObject, null, null, null, true, null);
		}

		// Token: 0x0400458B RID: 17803
		public int Chance;

		// Token: 0x0400458C RID: 17804
		public string Message;

		// Token: 0x0400458D RID: 17805
		public bool AffectsUnavoidable;

		// Token: 0x0400458E RID: 17806
		public bool ShowInShortDescription = true;
	}
}
