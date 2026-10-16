using System;
using XRL.World.Parts;

namespace XRL.World.AI.GoalHandlers
{
	// Token: 0x02000B8C RID: 2956
	[Serializable]
	public class MoveToInterior : IMovementGoal
	{
		// Token: 0x06007625 RID: 30245 RVA: 0x002C64C4 File Offset: 0x002C46C4
		public MoveToInterior()
		{
		}

		// Token: 0x06007626 RID: 30246 RVA: 0x002C64CC File Offset: 0x002C46CC
		public MoveToInterior(GameObject Target)
			: this()
		{
			this.Target = Target;
			this.Interior = ((Target != null) ? Target.GetPart<Interior>() : null);
		}

		// Token: 0x06007627 RID: 30247 RVA: 0x002C64ED File Offset: 0x002C46ED
		public override bool Finished()
		{
			return !GameObject.Validate(this.Target) || base.CurrentZone.ZoneID == this.Interior.ZoneID;
		}

		// Token: 0x06007628 RID: 30248 RVA: 0x002C651C File Offset: 0x002C471C
		public override void TakeAction()
		{
			int num = base.ParentObject.DistanceTo(this.Target);
			if (num == 9999999)
			{
				this.Target = null;
				return;
			}
			if (num > 1)
			{
				this.PushChildGoal(new MoveTo(this.Target, false, false, 1, false, false, false, -1, -1, 95));
				return;
			}
			if (!this.Interior.TryEnter(base.ParentObject, false, base.ParentObject.IsPlayerControlled()))
			{
				this.Target = null;
			}
		}

		// Token: 0x04003621 RID: 13857
		[NonSerialized]
		public GameObject Target;

		// Token: 0x04003622 RID: 13858
		[NonSerialized]
		public Interior Interior;
	}
}
