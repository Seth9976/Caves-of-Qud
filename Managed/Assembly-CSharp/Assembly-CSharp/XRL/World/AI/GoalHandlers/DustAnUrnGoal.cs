using System;
using XRL.World.Parts;

namespace XRL.World.AI.GoalHandlers
{
	// Token: 0x02000B77 RID: 2935
	[Serializable]
	public class DustAnUrnGoal : GoalHandler
	{
		// Token: 0x06007592 RID: 30098 RVA: 0x002C1F0F File Offset: 0x002C010F
		public DustAnUrnGoal()
		{
		}

		// Token: 0x06007593 RID: 30099 RVA: 0x002C1F17 File Offset: 0x002C0117
		public DustAnUrnGoal(GlobalLocation Target)
		{
			this.Target = Target;
		}

		// Token: 0x06007594 RID: 30100 RVA: 0x002C1F28 File Offset: 0x002C0128
		public DustAnUrnGoal(string ZoneID, int Wx, int Wy, int Xx, int Yx, int Zx, int X, int Y)
		{
			this.Target = new GlobalLocation(ZoneID, Wx, Wy, Xx, Yx, Zx, X, Y);
		}

		// Token: 0x06007595 RID: 30101 RVA: 0x002C1F53 File Offset: 0x002C0153
		public override bool Finished()
		{
			return this.Dusted;
		}

		// Token: 0x06007596 RID: 30102 RVA: 0x002C1F5B File Offset: 0x002C015B
		public override void Create()
		{
		}

		// Token: 0x06007597 RID: 30103 RVA: 0x002C1F60 File Offset: 0x002C0160
		public void MoveToAndDustUrn()
		{
			if (this.Dusted)
			{
				base.FailToParent();
				return;
			}
			Cell cell = base.ParentObject.CurrentZone.GetCell(this.Target.CellX, this.Target.CellY);
			if (base.ParentObject.DistanceTo(cell) > 1)
			{
				this.ParentBrain.PushGoal(new MoveTo(cell, false, false, 1, false, false, false, -1, -1, 95));
				return;
			}
			GameObject gameObject = null;
			int i = 0;
			int count = cell.Objects.Count;
			while (i < count)
			{
				GameObject gameObject2 = cell.Objects[i];
				if (gameObject2.HasPart("EaterUrn"))
				{
					gameObject = gameObject2;
					break;
				}
				i++;
			}
			if (gameObject != null)
			{
				this.ParentBrain.DidXToY("dust", gameObject, null, null, null, null, null, null, false, false, true, false, false, null, null, null, false, false, false, false, false, null);
				gameObject.DustPuff("&y", 15);
				this.Dusted = true;
				return;
			}
			AIUrnDuster.UrnFoundDestroyed(base.ParentObject.CurrentZone);
			base.FailToParent();
		}

		// Token: 0x06007598 RID: 30104 RVA: 0x002C2060 File Offset: 0x002C0260
		public override void TakeAction()
		{
			if (base.ParentObject.InZone(this.Target.ZoneID))
			{
				this.MoveToAndDustUrn();
				return;
			}
			this.ParentBrain.PushGoal(new MoveToZone(this.Target, false, 95));
		}

		// Token: 0x040035D4 RID: 13780
		public GlobalLocation Target;

		// Token: 0x040035D5 RID: 13781
		public bool Dusted;
	}
}
