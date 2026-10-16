using System;

namespace XRL.World.Parts
{
	// Token: 0x02000EFB RID: 3835
	[Serializable]
	public class Rummager : IPart
	{
		// Token: 0x06009911 RID: 39185 RVA: 0x00378A9D File Offset: 0x00376C9D
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009912 RID: 39186 RVA: 0x00378AA0 File Offset: 0x00376CA0
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("VillageInit");
			Registrar.Register("BeginTakeAction");
			Registrar.Register("EnteredCell");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009913 RID: 39187 RVA: 0x00378ACC File Offset: 0x00376CCC
		public override bool FireEvent(Event E)
		{
			if (E.ID == "VillageInit")
			{
				this.Active = false;
			}
			if (E.ID == "BeginTakeAction")
			{
				if (this.CheckPickUp())
				{
					return false;
				}
			}
			else if (E.ID == "EnteredCell")
			{
				this.CheckPickUp();
			}
			return base.FireEvent(E);
		}

		// Token: 0x06009914 RID: 39188 RVA: 0x00378B30 File Offset: 0x00376D30
		private bool CheckPickUp()
		{
			if (this.Active && !this.ParentObject.IsPlayerControlled() && this.ParentObject.Target == null)
			{
				foreach (GameObject gameObject in this.ParentObject.CurrentCell.GetObjectsWithPart("Physics"))
				{
					if (gameObject != this.ParentObject && !gameObject.IsCombat && gameObject.IsTakeable() && this.ParentObject.GetCarriedWeight() + gameObject.Weight < this.ParentObject.GetMaxCarriedWeight() && this.ParentObject.TakeObject(gameObject, false, true, new int?(0), null, null))
					{
						base.DidXToY("pick", "up", gameObject, null, null, null, null, null, null, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x04003FC5 RID: 16325
		public bool Active = true;
	}
}
