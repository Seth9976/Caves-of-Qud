using System;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02001287 RID: 4743
	[Serializable]
	public class SmartuseLooks : IPart
	{
		// Token: 0x0600BBA8 RID: 48040 RVA: 0x0044876F File Offset: 0x0044696F
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600BBA9 RID: 48041 RVA: 0x00448772 File Offset: 0x00446972
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CanSmartUse");
			Registrar.Register("CommandSmartUseEarly");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BBAA RID: 48042 RVA: 0x00448794 File Offset: 0x00446994
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CanSmartUse")
			{
				if (E.GetGameObjectParameter("User").IsPlayer())
				{
					return false;
				}
			}
			else if (E.ID == "CommandSmartUseEarly" && E.GetGameObjectParameter("User").IsPlayer())
			{
				Cell currentCell = this.ParentObject.CurrentCell;
				if (currentCell != null)
				{
					Look.ShowLooker(0, currentCell.X, currentCell.Y);
					return false;
				}
			}
			return base.FireEvent(E);
		}
	}
}
