using System;

namespace XRL.World.Parts
{
	// Token: 0x02000FA8 RID: 4008
	public class LifeGate : IPart
	{
		// Token: 0x06009F55 RID: 40789 RVA: 0x0039E80D File Offset: 0x0039CA0D
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AttemptDoorUnlock");
		}

		// Token: 0x06009F56 RID: 40790 RVA: 0x0039E81C File Offset: 0x0039CA1C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AttemptDoorUnlock")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Actor");
				if (!The.Game.HasQuest("Reclamation"))
				{
					return gameObjectParameter.ShowFailure("The gates are sealed for eternity.");
				}
				if (!The.Game.HasFinishedQuest("Reclamation"))
				{
					return gameObjectParameter.ShowFailure("The gates are secured shut until the threat to Omonporch is eliminated.");
				}
				Door door;
				if (this.ParentObject.TryGetPart<Door>(out door) && door.Locked)
				{
					door.Unlock();
					door.AttemptOpen(null, false, false, false, false, false, false, null);
				}
				this.ParentObject.RemovePart(this);
			}
			return base.FireEvent(E);
		}
	}
}
