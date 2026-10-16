using System;

namespace XRL.World.Parts
{
	// Token: 0x0200120D RID: 4621
	[Serializable]
	public class QuestStepFinisher : IPart
	{
		// Token: 0x0600B7C1 RID: 47041 RVA: 0x0042D829 File Offset: 0x0042BA29
		public QuestStepFinisher()
		{
			this.Trigger = "Created";
		}

		// Token: 0x0600B7C2 RID: 47042 RVA: 0x0042D83C File Offset: 0x0042BA3C
		public override void Attach()
		{
			this.WantBeforeRenderEvent = this.Trigger == "Created";
		}

		// Token: 0x0600B7C3 RID: 47043 RVA: 0x0042D854 File Offset: 0x0042BA54
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x0600B7C4 RID: 47044 RVA: 0x0042D857 File Offset: 0x0042BA57
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (ID == BeforeRenderEvent.ID && this.WantBeforeRenderEvent);
		}

		// Token: 0x0600B7C5 RID: 47045 RVA: 0x0042D878 File Offset: 0x0042BA78
		public override bool HandleEvent(BeforeRenderEvent E)
		{
			if (this.Trigger == "Created")
			{
				GameObject parentObject = this.ParentObject;
				string text;
				if (parentObject == null)
				{
					text = null;
				}
				else
				{
					Zone currentZone = parentObject.CurrentZone;
					text = ((currentZone != null) ? currentZone.ZoneID : null);
				}
				GameObject player = The.Player;
				string text2;
				if (player == null)
				{
					text2 = null;
				}
				else
				{
					Zone currentZone2 = player.CurrentZone;
					text2 = ((currentZone2 != null) ? currentZone2.ZoneID : null);
				}
				if (text == text2)
				{
					GameObject parentObject2 = this.ParentObject;
					bool flag;
					if (parentObject2 == null)
					{
						flag = null != null;
					}
					else
					{
						Zone currentZone3 = parentObject2.CurrentZone;
						flag = ((currentZone3 != null) ? currentZone3.ZoneID : null) != null;
					}
					if (flag)
					{
						this.Activate();
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B7C6 RID: 47046 RVA: 0x0042D908 File Offset: 0x0042BB08
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			if (this.Trigger == "OnScreen")
			{
				Registrar.Register("EndTurn");
			}
			if (this.Trigger == "Taken")
			{
				Registrar.Register("Taken");
				Registrar.Register("Equipped");
			}
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B7C7 RID: 47047 RVA: 0x0042D962 File Offset: 0x0042BB62
		public override bool Render(RenderEvent E)
		{
			if (this.Trigger == "Seen")
			{
				this.Activate();
			}
			return base.Render(E);
		}

		// Token: 0x0600B7C8 RID: 47048 RVA: 0x0042D984 File Offset: 0x0042BB84
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EndTurn")
			{
				if (this.Trigger == "OnScreen")
				{
					this.Activate();
				}
			}
			else if ((E.ID == "Taken" || E.ID == "Equipped") && this.Trigger == "Taken")
			{
				GameObject gameObject = E.GetGameObjectParameter("TakingObject") ?? E.GetGameObjectParameter("EquippingObject");
				if (gameObject != null && gameObject.IsPlayer())
				{
					this.Activate();
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600B7C9 RID: 47049 RVA: 0x0042DA28 File Offset: 0x0042BC28
		public void Activate()
		{
			if (this.Activated)
			{
				return;
			}
			if (string.IsNullOrEmpty(this.IfFinishedQuestStep) || The.Game.FinishedQuestStep(this.IfFinishedQuestStep))
			{
				if (!The.Game.FinishQuestStep(this.Quest, this.Step, -1, true, null))
				{
					return;
				}
				this.Activated = true;
				if (this.ParentObject.HasTag("Non"))
				{
					this.ParentObject.Obliterate(null, true, null);
					return;
				}
				GameManager.Instance.gameQueue.queueTask(delegate
				{
					GameObject parentObject = this.ParentObject;
					if (parentObject == null)
					{
						return;
					}
					parentObject.RemovePart(this);
				}, 0);
			}
		}

		// Token: 0x040049E7 RID: 18919
		public string IfFinishedQuestStep;

		// Token: 0x040049E8 RID: 18920
		public string Quest;

		// Token: 0x040049E9 RID: 18921
		public string Step;

		// Token: 0x040049EA RID: 18922
		public string Trigger;

		// Token: 0x040049EB RID: 18923
		[NonSerialized]
		private bool Activated;

		// Token: 0x040049EC RID: 18924
		[NonSerialized]
		private bool WantBeforeRenderEvent;
	}
}
