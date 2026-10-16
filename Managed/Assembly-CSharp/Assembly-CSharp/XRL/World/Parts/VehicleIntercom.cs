using System;
using XRL.World.Conversations.Parts;

namespace XRL.World.Parts
{
	// Token: 0x020012B4 RID: 4788
	[Serializable]
	public class VehicleIntercom : IActivePart
	{
		// Token: 0x0600BD48 RID: 48456 RVA: 0x00454717 File Offset: 0x00452917
		public VehicleIntercom()
		{
			this.WorksOnSelf = true;
			this.NameForStatus = "Intercom";
		}

		// Token: 0x0600BD49 RID: 48457 RVA: 0x00454731 File Offset: 0x00452931
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<AttemptConversationEvent>.ID || ID == BeginConversationEvent.ID;
		}

		// Token: 0x0600BD4A RID: 48458 RVA: 0x00454750 File Offset: 0x00452950
		public override bool HandleEvent(AttemptConversationEvent E)
		{
			GameObject activePartFirstSubject = this.GetActivePartFirstSubject();
			Vehicle vehicle;
			if (activePartFirstSubject != null && activePartFirstSubject.TryGetPart<Vehicle>(out vehicle) && vehicle.Pilot != null && E.IsParticipant(activePartFirstSubject))
			{
				ConversationScript conversationScript;
				if (base.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
				{
					if (this.Required)
					{
						return !E.Silent && !this.RequiredMessage.IsNullOrEmpty() && E.Listener.ShowFailure(this.RequiredMessage);
					}
					return base.HandleEvent(E);
				}
				else if (E.Listener == activePartFirstSubject)
				{
					E.Listener = vehicle.Pilot;
					E.Receiver = activePartFirstSubject;
				}
				else if (E.Speaker == activePartFirstSubject && vehicle.Pilot.TryGetPart<ConversationScript>(out conversationScript))
				{
					E.Speaker = vehicle.Pilot;
					E.Transmitter = activePartFirstSubject;
					E.Blueprint = conversationScript.GetActiveConversationBlueprint();
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BD4B RID: 48459 RVA: 0x0045484C File Offset: 0x00452A4C
		public override bool HandleEvent(BeginConversationEvent E)
		{
			GameObject activePartFirstSubject = this.GetActivePartFirstSubject();
			if (E.IsDevice(activePartFirstSubject))
			{
				if (activePartFirstSubject == E.Transmitter && !this.StartText.IsNullOrEmpty())
				{
					E.StartNode.AddPart(new TextInsert
					{
						Prepend = true,
						Text = GameText.VariableReplace(this.StartText, activePartFirstSubject, this.ParentObject, false),
						Spoken = !this.StartText.Contains('['),
						NewLines = 2
					}, true);
				}
				base.ConsumeCharge(null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04004BD7 RID: 19415
		public string StartText;

		// Token: 0x04004BD8 RID: 19416
		public string RequiredMessage;

		// Token: 0x04004BD9 RID: 19417
		public bool Required;
	}
}
