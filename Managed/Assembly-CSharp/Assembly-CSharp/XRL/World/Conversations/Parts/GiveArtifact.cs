using System;
using System.Collections.Generic;
using XRL.UI;
using XRL.World.Parts;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000BFA RID: 3066
	public class GiveArtifact : IConversationPart
	{
		// Token: 0x06007B44 RID: 31556 RVA: 0x002E51DC File Offset: 0x002E33DC
		public static bool IsArtifact(GameObject Object)
		{
			Examiner examiner;
			return Object.HasPart<TinkerItem>() && Object.TryGetPart<Examiner>(out examiner) && examiner.Complexity > 0;
		}

		// Token: 0x06007B45 RID: 31557 RVA: 0x002E5208 File Offset: 0x002E3408
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == GetChoiceTagEvent.ID || ID == EnterElementEvent.ID;
		}

		// Token: 0x06007B46 RID: 31558 RVA: 0x002E5226 File Offset: 0x002E3426
		public override bool HandleEvent(GetChoiceTagEvent E)
		{
			E.Tag = "{{g|[give artifact]}}";
			return false;
		}

		// Token: 0x06007B47 RID: 31559 RVA: 0x002E5234 File Offset: 0x002E3434
		public override bool HandleEvent(EnterElementEvent E)
		{
			List<GameObject> objects = The.Player.Inventory.GetObjects(new Predicate<GameObject>(GiveArtifact.IsArtifact));
			if (objects.Count == 0)
			{
				Popup.Show("You have no artifacts to give.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				return false;
			}
			GameObject gameObject = Popup.PickGameObject("Choose an artifact to give.", objects, true, false, true, null, 0, false);
			if (gameObject == null)
			{
				return false;
			}
			gameObject.SplitStack(1, The.Player, false);
			if (!The.Player.FireEvent(Event.New("CommandRemoveObject", "Object", gameObject)))
			{
				Popup.Show("You can't give that object.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				return false;
			}
			return base.HandleEvent(E);
		}
	}
}
