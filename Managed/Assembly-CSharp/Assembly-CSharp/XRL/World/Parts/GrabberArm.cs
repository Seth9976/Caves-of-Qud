using System;
using XRL.Core;
using XRL.Rules;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x0200126C RID: 4716
	[Serializable]
	public class GrabberArm : IPart
	{
		// Token: 0x0600BAE9 RID: 47849 RVA: 0x004440B0 File Offset: 0x004422B0
		public override bool SameAs(IPart p)
		{
			return base.SameAs(p);
		}

		// Token: 0x0600BAEA RID: 47850 RVA: 0x004440B9 File Offset: 0x004422B9
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EndTurn");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BAEB RID: 47851 RVA: 0x004440D0 File Offset: 0x004422D0
		public override bool Render(RenderEvent E)
		{
			if (this.Counter == this.Frequency - 1)
			{
				int num = XRLCore.CurrentFrame % 30;
				if (num > 0 && num < 15)
				{
					E.Tile = null;
					E.RenderString = Directions.GetArrowForDirection(this.Direction);
					E.ColorString = "&R";
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600BAEC RID: 47852 RVA: 0x00444128 File Offset: 0x00442328
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EndTurn")
			{
				this.Counter++;
				if (this.Counter >= this.Frequency)
				{
					this.Counter = 0;
					Cell cellFromDirection = base.currentCell.GetCellFromDirection(this.Direction, true);
					GameObject gameObject = cellFromDirection.GetCombatObject();
					if (gameObject == null)
					{
						for (int i = 0; i < cellFromDirection.Objects.Count; i++)
						{
							GameObject gameObject2 = cellFromDirection.Objects[i];
							if (gameObject2.Physics != null && gameObject2.Physics.Takeable)
							{
								gameObject = gameObject2;
							}
						}
					}
					if (gameObject != null)
					{
						if (gameObject.IsPlayer())
						{
							base.DidXToY("grab", The.Player, "and holds you in place", null, null, null, null, The.Player, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
						}
						gameObject.ApplyEffect(new Stuck(3, 25, "Grab Stuck Restraint", null, "stuck", "in", this.ParentObject.ID, "Move", 0.25f, 1f, 1f, false, false), null);
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004B00 RID: 19200
		public string Direction;

		// Token: 0x04004B01 RID: 19201
		public int Frequency;

		// Token: 0x04004B02 RID: 19202
		public int Counter;
	}
}
