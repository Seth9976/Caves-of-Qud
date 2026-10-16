using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000F7B RID: 3963
	[Serializable]
	public class FlyingWhitelistArea : IPart
	{
		// Token: 0x06009DB3 RID: 40371 RVA: 0x00391D6C File Offset: 0x0038FF6C
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009DB4 RID: 40372 RVA: 0x00391D6F File Offset: 0x0038FF6F
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ClimbUp");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009DB5 RID: 40373 RVA: 0x00391D84 File Offset: 0x0038FF84
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeRenderEvent.ID;
		}

		// Token: 0x06009DB6 RID: 40374 RVA: 0x00391D9C File Offset: 0x0038FF9C
		public override bool HandleEvent(BeforeRenderEvent E)
		{
			if (Calendar.IsDay())
			{
				GameObject parentObject = this.ParentObject;
				if (parentObject != null)
				{
					Cell currentCell = parentObject.CurrentCell;
					if (currentCell != null)
					{
						Zone parentZone = currentCell.ParentZone;
						if (parentZone != null)
						{
							parentZone.AddLight(this.ParentObject.CurrentCell.X, this.ParentObject.CurrentCell.Y, 1, LightLevel.Light, false);
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06009DB7 RID: 40375 RVA: 0x00391E00 File Offset: 0x00390000
		public override bool FireEvent(Event E)
		{
			if (!(E.ID == "ClimbUp"))
			{
				return base.FireEvent(E);
			}
			GameObject gameObjectParameter = E.GetGameObjectParameter("GO");
			if (gameObjectParameter == null || !gameObjectParameter.HasEffect<Flying>() || !gameObjectParameter.IsPlayer())
			{
				return true;
			}
			Cell currentCell = gameObjectParameter.GetCurrentCell();
			Cell cell = ((currentCell != null) ? currentCell.GetCellFromDirection("U", false) : null);
			if (cell == null)
			{
				return true;
			}
			if (!cell.HasObjectWithPart("StairsDown") && !cell.HasObjectWithTag("FlyingWhitelistArea"))
			{
				return true;
			}
			gameObjectParameter.Move("U", false, false, false, false, false, true, null, null, false, null, null, null, false, false, null, null, 0);
			return false;
		}
	}
}
