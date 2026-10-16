using System;
using XRL.World.Parts;

namespace XRL.World.AI.GoalHandlers
{
	// Token: 0x02000B8A RID: 2954
	[Serializable]
	public class MoveToExterior : IMovementGoal
	{
		// Token: 0x06007618 RID: 30232 RVA: 0x002C5D12 File Offset: 0x002C3F12
		public MoveToExterior()
		{
		}

		// Token: 0x06007619 RID: 30233 RVA: 0x002C5D1A File Offset: 0x002C3F1A
		public MoveToExterior(GameObject Target)
			: this()
		{
			this.Part = Target.GetPart<InteriorPortal>();
		}

		// Token: 0x0600761A RID: 30234 RVA: 0x002C5D2E File Offset: 0x002C3F2E
		public override void Create()
		{
			this.Interior = base.CurrentZone as InteriorZone;
		}

		// Token: 0x0600761B RID: 30235 RVA: 0x002C5D41 File Offset: 0x002C3F41
		public override bool Finished()
		{
			return this.Interior == null || base.CurrentZone != this.Interior;
		}

		// Token: 0x0600761C RID: 30236 RVA: 0x002C5D5E File Offset: 0x002C3F5E
		public bool IsValidExit(GameObject Object)
		{
			return Object.HasPart(typeof(InteriorPortal));
		}

		// Token: 0x0600761D RID: 30237 RVA: 0x002C5D70 File Offset: 0x002C3F70
		public override void TakeAction()
		{
			if (this.Part == null || !GameObject.Validate(this.Part.ParentObject))
			{
				GameObject gameObject = base.CurrentZone.FindClosestObject(base.ParentObject, new Predicate<GameObject>(this.IsValidExit), false, true, true);
				this.Part = ((gameObject != null) ? gameObject.GetPart<InteriorPortal>() : null);
				if (this.Part == null)
				{
					base.FailToParent();
					return;
				}
			}
			if (base.ParentObject.DistanceTo(this.Part.ParentObject) > 1)
			{
				this.PushChildGoal(new MoveTo(this.Part.ParentObject, false, false, 1, false, false, false, -1, -1, 95));
				return;
			}
			if (!this.Part.TryExit(base.ParentObject))
			{
				base.FailToParent();
			}
		}

		// Token: 0x04003617 RID: 13847
		[NonSerialized]
		public InteriorPortal Part;

		// Token: 0x04003618 RID: 13848
		[NonSerialized]
		public InteriorZone Interior;
	}
}
