using System;
using XRL.UI;
using XRL.World.Parts;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x02000914 RID: 2324
	[Serializable]
	public class TinkeringSifrahTokenCopperWire : SifrahPrioritizableToken
	{
		// Token: 0x060058A4 RID: 22692 RVA: 0x001AD388 File Offset: 0x001AB588
		public TinkeringSifrahTokenCopperWire()
		{
			this.Description = "use a length of copper wire";
			this.Tile = "Items/sw_copper_wire.bmp";
			this.RenderString = "í";
			this.ColorString = "&r";
			this.DetailColor = 'w';
		}

		// Token: 0x060058A5 RID: 22693 RVA: 0x001AD3C4 File Offset: 0x001AB5C4
		public override int GetPriority()
		{
			return this.GetNumberAvailable(0);
		}

		// Token: 0x060058A6 RID: 22694 RVA: 0x001AD3CD File Offset: 0x001AB5CD
		public override int GetTiebreakerPriority()
		{
			return 0;
		}

		// Token: 0x060058A7 RID: 22695 RVA: 0x001AD3D0 File Offset: 0x001AB5D0
		public int GetNumberAvailable(int Chosen = 0)
		{
			int num = -Chosen;
			foreach (GameObject gameObject in The.Player.GetInventoryAndEquipment())
			{
				if (gameObject.HasPart<Wire>())
				{
					num += gameObject.Count;
				}
			}
			return num;
		}

		// Token: 0x060058A8 RID: 22696 RVA: 0x001AD438 File Offset: 0x001AB638
		public bool IsAvailable(int Chosen = 0)
		{
			int num = 0;
			foreach (GameObject gameObject in The.Player.GetInventoryAndEquipment())
			{
				if (gameObject.HasPart<Wire>())
				{
					num += gameObject.Count;
					if (num > Chosen)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060058A9 RID: 22697 RVA: 0x001AD4A8 File Offset: 0x001AB6A8
		public override string GetDescription(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			return this.Description + " [have {{C|" + this.GetNumberAvailable(Game.GetTimesChosen(this, Slot)).ToString() + "}}]";
		}

		// Token: 0x060058AA RID: 22698 RVA: 0x001AD4E0 File Offset: 0x001AB6E0
		public override bool GetDisabled(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			return !this.IsAvailable(Game.GetTimesChosen(this, Slot)) || base.GetDisabled(Game, Slot, ContextObject);
		}

		// Token: 0x060058AB RID: 22699 RVA: 0x001AD500 File Offset: 0x001AB700
		public override bool CheckTokenUse(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			int timesChosen = Game.GetTimesChosen(this, Slot);
			if (!this.IsAvailable(timesChosen))
			{
				if (timesChosen > 0)
				{
					Popup.ShowFail("You do not have any more copper wire.", true, true, true);
				}
				else
				{
					Popup.ShowFail("You do not have any copper wire.", true, true, true);
				}
				return false;
			}
			return base.CheckTokenUse(Game, Slot, ContextObject);
		}

		// Token: 0x060058AC RID: 22700 RVA: 0x001AD54C File Offset: 0x001AB74C
		public override void UseToken(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			GameObject gameObject = null;
			int maxValue = int.MaxValue;
			foreach (GameObject gameObject2 in The.Player.GetInventoryAndEquipment())
			{
				Wire part = gameObject2.GetPart<Wire>();
				if (part != null && part.Length < maxValue)
				{
					gameObject = gameObject2;
				}
			}
			if (gameObject != null)
			{
				gameObject.Destroy(null, false, false, null);
			}
			base.UseToken(Game, Slot, ContextObject);
		}
	}
}
