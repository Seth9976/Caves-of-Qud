using System;

namespace XRL.World.Parts
{
	// Token: 0x020011AD RID: 4525
	[Serializable]
	public class PreachOnSit : IPart
	{
		// Token: 0x0600B3C4 RID: 46020 RVA: 0x004193EE File Offset: 0x004175EE
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeingSatOn");
			Registrar.Register("CanPreach");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B3C5 RID: 46021 RVA: 0x00419410 File Offset: 0x00417610
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CanPreach")
			{
				return this.ParentObject.Physics.CurrentCell.GetObjectCountWithPart("Combat") > 1;
			}
			if (E.ID == "BeingSatOn" && this.ParentObject.HasPart<Preacher>())
			{
				this.ParentObject.GetPart<Preacher>().PreacherHomily(false);
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004886 RID: 18566
		public bool leaderInCombat;
	}
}
