using System;

namespace XRL.World.Parts
{
	// Token: 0x02000F0B RID: 3851
	[Serializable]
	public class Sweeper : IPart
	{
		// Token: 0x0600999D RID: 39325 RVA: 0x0037CA0B File Offset: 0x0037AC0B
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EnteredCell");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600999E RID: 39326 RVA: 0x0037CA20 File Offset: 0x0037AC20
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EnteredCell")
			{
				foreach (GameObject gameObject in this.ParentObject.CurrentCell.GetObjectsWithPart("Physics"))
				{
					if (gameObject != this.ParentObject && !gameObject.IsCombat && gameObject.IsTakeable() && gameObject.Weight < 100)
					{
						this.ParentObject.FireEvent(Event.New("CommandTakeObject", "Object", gameObject).SetSilent(true));
						base.DidXToY("consume", gameObject, null, null, null, null, null, null, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
					}
				}
			}
			return base.FireEvent(E);
		}
	}
}
