using System;

namespace XRL.World.Parts
{
	// Token: 0x02001305 RID: 4869
	[Serializable]
	public class PetWraithSummoner : IPart
	{
		// Token: 0x0600BEBA RID: 48826 RVA: 0x0045D692 File Offset: 0x0045B892
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BEBB RID: 48827 RVA: 0x0045D695 File Offset: 0x0045B895
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EnteredCell");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BEBC RID: 48828 RVA: 0x0045D6AC File Offset: 0x0045B8AC
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EnteredCell" && IComponent<GameObject>.ThePlayer != null)
			{
				GameObject gameObject = GameObject.Create("BrokenPhylactery");
				IComponent<GameObject>.ThePlayer.ReceiveObject(gameObject, false, null);
				InventoryActionEvent.Check(gameObject, IComponent<GameObject>.ThePlayer, gameObject, "ActivateTemplarPhylactery", false, false, false, false, false, 0, 0, 0, null, null, null, null);
				this.ParentObject.Obliterate(null, false, null);
			}
			return base.FireEvent(E);
		}
	}
}
