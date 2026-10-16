using System;

namespace XRL.World.Parts
{
	// Token: 0x0200120C RID: 4620
	[Serializable]
	public class QuestStarter : IPart
	{
		// Token: 0x0600B7B7 RID: 47031 RVA: 0x0042D4B3 File Offset: 0x0042B6B3
		public QuestStarter()
		{
			this.Trigger = "Taken";
		}

		// Token: 0x0600B7B8 RID: 47032 RVA: 0x0042D4C6 File Offset: 0x0042B6C6
		public override void Attach()
		{
			this.WantBeforeRenderEvent = this.Trigger == "Created";
		}

		// Token: 0x0600B7B9 RID: 47033 RVA: 0x0042D4DE File Offset: 0x0042B6DE
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x0600B7BA RID: 47034 RVA: 0x0042D4E1 File Offset: 0x0042B6E1
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (ID == BeforeRenderEvent.ID && this.WantBeforeRenderEvent);
		}

		// Token: 0x0600B7BB RID: 47035 RVA: 0x0042D500 File Offset: 0x0042B700
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

		// Token: 0x0600B7BC RID: 47036 RVA: 0x0042D590 File Offset: 0x0042B790
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

		// Token: 0x0600B7BD RID: 47037 RVA: 0x0042D5EC File Offset: 0x0042B7EC
		public override bool Render(RenderEvent E)
		{
			if (this.Trigger == "Seen")
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
			return base.Render(E);
		}

		// Token: 0x0600B7BE RID: 47038 RVA: 0x0042D67C File Offset: 0x0042B87C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EndTurn")
			{
				if (this.Trigger == "OnScreen")
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

		// Token: 0x0600B7BF RID: 47039 RVA: 0x0042D78C File Offset: 0x0042B98C
		public void Activate()
		{
			if (this.Activated)
			{
				return;
			}
			if (string.IsNullOrEmpty(this.IfFinishedQuestStep) || The.Game.FinishedQuestStep(this.IfFinishedQuestStep))
			{
				this.Activated = true;
				The.Game.StartQuest(this.Quest, null, null, null);
				if (this.ParentObject.HasTag("Non"))
				{
					this.ParentObject.Obliterate(null, true, null);
					return;
				}
				GameManager.Instance.gameQueue.queueTask(delegate
				{
					this.ParentObject.RemovePart(this);
				}, 0);
			}
		}

		// Token: 0x040049E2 RID: 18914
		public string IfFinishedQuestStep;

		// Token: 0x040049E3 RID: 18915
		public string Quest;

		// Token: 0x040049E4 RID: 18916
		public string Trigger;

		// Token: 0x040049E5 RID: 18917
		[NonSerialized]
		private bool Activated;

		// Token: 0x040049E6 RID: 18918
		[NonSerialized]
		private bool WantBeforeRenderEvent;
	}
}
