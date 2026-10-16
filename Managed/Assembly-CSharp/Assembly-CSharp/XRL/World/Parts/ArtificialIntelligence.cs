using System;

namespace XRL.World.Parts
{
	// Token: 0x020011B5 RID: 4533
	[Serializable]
	public class ArtificialIntelligence : IPoweredPart
	{
		// Token: 0x0600B400 RID: 46080 RVA: 0x0041AC05 File Offset: 0x00418E05
		public ArtificialIntelligence()
		{
			this.WorksOnSelf = true;
			this.IsRustSensitive = false;
		}

		// Token: 0x0600B401 RID: 46081 RVA: 0x0041AC1B File Offset: 0x00418E1B
		public override bool SameAs(IPart p)
		{
			return base.SameAs(p);
		}

		// Token: 0x0600B402 RID: 46082 RVA: 0x0041AC24 File Offset: 0x00418E24
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == GetHostileWalkRadiusEvent.ID || (ID == PooledEvent<GetDisplayNameEvent>.ID && this.ShowAsDeactivated) || (ID == GetShortDescriptionEvent.ID && this.ShowAsDeactivated) || ID == PooledEvent<IsConversationallyResponsiveEvent>.ID;
		}

		// Token: 0x0600B403 RID: 46083 RVA: 0x0041AC78 File Offset: 0x00418E78
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			return (this.ParentObject.IsPlayer() || !base.IsDisabled(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null)) && base.HandleEvent(E);
		}

		// Token: 0x0600B404 RID: 46084 RVA: 0x0041ACC4 File Offset: 0x00418EC4
		public override bool HandleEvent(IsConversationallyResponsiveEvent E)
		{
			if (E.Speaker == this.ParentObject && base.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				E.Message = this.ParentObject.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + this.ParentObject.Is + " utterly unresponsive.";
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B405 RID: 46085 RVA: 0x0041AD58 File Offset: 0x00418F58
		public override bool HandleEvent(GetHostileWalkRadiusEvent E)
		{
			if (base.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				E.Radius = 0;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B406 RID: 46086 RVA: 0x0041ADA0 File Offset: 0x00418FA0
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (this.ShowAsDeactivated && base.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				E.AddTag("{{y|[{{K|deactivated}}]}}", 20);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B407 RID: 46087 RVA: 0x0041ADF4 File Offset: 0x00418FF4
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.ShowAsDeactivated)
			{
				ActivePartStatus activePartStatus = base.GetActivePartStatus(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null);
				if (activePartStatus != ActivePartStatus.Operational)
				{
					string text;
					if (activePartStatus == ActivePartStatus.Unpowered)
					{
						text = "Deactivated: Currently without power.";
					}
					else if (activePartStatus == ActivePartStatus.SwitchedOff)
					{
						text = "Deactivated: Currently switched off.";
					}
					else
					{
						text = "Deactivated: Currently non-functional.";
					}
					E.Postfix.AppendRules(text);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04004892 RID: 18578
		public bool ShowAsDeactivated;
	}
}
