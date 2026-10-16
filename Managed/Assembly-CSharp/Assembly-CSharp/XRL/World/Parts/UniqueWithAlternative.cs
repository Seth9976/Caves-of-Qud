using System;
using UnityEngine;
using XRL.Core;

namespace XRL.World.Parts
{
	// Token: 0x020010BA RID: 4282
	[Serializable]
	public class UniqueWithAlternative : IPart
	{
		// Token: 0x0600AA96 RID: 43670 RVA: 0x003E6646 File Offset: 0x003E4846
		public override bool SameAs(IPart p)
		{
			return !((p as UniqueWithAlternative).Alternative != this.Alternative);
		}

		// Token: 0x0600AA97 RID: 43671 RVA: 0x003E6663 File Offset: 0x003E4863
		public override bool CanGenerateStacked()
		{
			return false;
		}

		// Token: 0x0600AA98 RID: 43672 RVA: 0x003E6666 File Offset: 0x003E4866
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EnteredCell");
			Registrar.Register("Equipped");
			Registrar.Register("AddedToInventory");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AA99 RID: 43673 RVA: 0x003E6694 File Offset: 0x003E4894
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EnteredCell" || E.ID == "Equipped" || E.ID == "AddedToInventory")
			{
				if (XRLCore.Core.Game == null)
				{
					return true;
				}
				if (this.ParentObject == null)
				{
					Debug.LogError("UniqueWithAlternative part had no parent object on " + E.ID);
					return true;
				}
				if (XRLCore.Core.Game.GetIntGameState("UniqueAlternative_" + this.ParentObject.Blueprint, 0) > 0)
				{
					this.ParentObject.ReplaceWith(GameObjectFactory.Factory.CreateObject(this.Alternative.Contains(",") ? this.Alternative.Split(',', StringSplitOptions.None).GetRandomElement<string>(null) : this.Alternative));
				}
				else
				{
					XRLCore.Core.Game.SetIntGameState("UniqueAlternative_" + this.ParentObject.Blueprint, 1);
					this.ParentObject.RemovePart(this);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004654 RID: 18004
		public string Alternative = "Campfire";
	}
}
