using System;
using XRL.UI;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x020007C9 RID: 1993
	[GameEvent(Base = true)]
	public abstract class IExamineEvent : IActOnItemEvent
	{
		// Token: 0x0600507D RID: 20605 RVA: 0x00186B6E File Offset: 0x00184D6E
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x0600507E RID: 20606 RVA: 0x00186B84 File Offset: 0x00184D84
		public override void Reset()
		{
			base.Reset();
			this.Pass = 1;
			this.ConfusionBased = false;
			this.Identify = false;
			this.IdentifyIfDestroyed = false;
			this.TinkeringBlueprint = null;
			this.PriorDesc = null;
			this.PriorPastVerbToBe = null;
			this.KnownDesc = null;
			this.Complexity = 0;
		}

		// Token: 0x0600507F RID: 20607 RVA: 0x00186BD8 File Offset: 0x00184DD8
		protected void Setup()
		{
			this.Identify = false;
			this.IdentifyIfDestroyed = false;
			if (GameObject.Validate(ref this.Item))
			{
				this.TinkeringBlueprint = this.Item.GetTinkeringBlueprint();
				this.PriorDesc = this.Item.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false);
				this.PriorPastVerbToBe = this.Item.GetVerb("were", true, false, false, true, false);
				this.KnownDesc = this.Item.an(int.MaxValue, null, null, true, true, false, false, false, false, true, false, false, true, false, null, false, null, false);
				Examiner part = this.Item.GetPart<Examiner>();
				this.Complexity = ((part != null) ? part.Complexity : 0);
			}
		}

		// Token: 0x06005080 RID: 20608 RVA: 0x00186CAC File Offset: 0x00184EAC
		protected bool ProcessIdentify()
		{
			if (this.Identify && GameObject.Validate(ref this.Item) && !this.Item.Understood() && GameObject.Validate(ref this.Actor) && this.Actor.IsVisible())
			{
				Popup.Show(string.Concat(new string[]
				{
					"You realize ",
					this.Item.does("are", int.MaxValue, null, null, null, false, true, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
					" ",
					this.Item.an(int.MaxValue, null, null, true, true, false, false, false, false, true, false, false, true, false, null, false, null, false),
					"!"
				}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
				this.Item.MakeUnderstood(false);
				this.Identify = false;
				return true;
			}
			if (this.IdentifyIfDestroyed && !GameObject.Validate(ref this.Item) && !this.TinkeringBlueprint.IsNullOrEmpty() && !this.PriorDesc.IsNullOrEmpty() && !this.PriorPastVerbToBe.IsNullOrEmpty() && !this.KnownDesc.IsNullOrEmpty() && this.Complexity > 0)
			{
				if (Examiner.MakeBlueprintUnderstood(this.TinkeringBlueprint, this.Complexity))
				{
					Popup.Show(string.Concat(new string[] { "You realize ", this.PriorDesc, this.PriorPastVerbToBe, " ", this.KnownDesc, "!" }), null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				this.IdentifyIfDestroyed = false;
				return true;
			}
			return false;
		}

		// Token: 0x06005081 RID: 20609 RVA: 0x00186E79 File Offset: 0x00185079
		public void IdentifyImmediately()
		{
			this.ProcessIdentify();
		}

		// Token: 0x04002801 RID: 10241
		public int Pass = 1;

		// Token: 0x04002802 RID: 10242
		public bool ConfusionBased;

		// Token: 0x04002803 RID: 10243
		public bool Identify;

		// Token: 0x04002804 RID: 10244
		public bool IdentifyIfDestroyed;

		// Token: 0x04002805 RID: 10245
		public string TinkeringBlueprint;

		// Token: 0x04002806 RID: 10246
		public string PriorDesc;

		// Token: 0x04002807 RID: 10247
		public string PriorPastVerbToBe;

		// Token: 0x04002808 RID: 10248
		public string KnownDesc;

		// Token: 0x04002809 RID: 10249
		public int Complexity;
	}
}
