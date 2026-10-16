using System;
using System.Collections.Generic;
using XRL.Messages;
using XRL.Rules;
using XRL.World.Anatomy;
using XRL.World.Effects;
using XRL.World.Parts;

namespace XRL.World.AI.GoalHandlers
{
	// Token: 0x02000B9D RID: 2973
	[Serializable]
	public class PaxKlanqMadness : GoalHandler
	{
		// Token: 0x06007695 RID: 30357 RVA: 0x002C9214 File Offset: 0x002C7414
		public override bool Finished()
		{
			return false;
		}

		// Token: 0x06007696 RID: 30358 RVA: 0x002C9217 File Offset: 0x002C7417
		public override bool CanFight()
		{
			return this.bCanFight;
		}

		// Token: 0x06007697 RID: 30359 RVA: 0x002C9220 File Offset: 0x002C7420
		public override void TakeAction()
		{
			if (base.ParentObject.IsPlayer())
			{
				base.Pop();
				return;
			}
			if (this.Style == -1)
			{
				this.Style = Stat.Random(0, 1);
			}
			if (this.Style == 0)
			{
				if (this.Stage == 0)
				{
					if (base.ParentObject.HasPart<Body>())
					{
						Body body = base.ParentObject.Body;
						List<BodyPart> equippedParts = body.GetEquippedParts();
						for (int i = 0; i < equippedParts.Count; i++)
						{
							GameObject equipped = equippedParts[i].Equipped;
							if (base.ParentObject.FireEvent(Event.New("CommandUnequipObject", "BodyPart", equippedParts[i])) && equipped != null && equipped.IsValid() && base.ParentObject.CurrentCell != null && equipped.Equipped == null)
							{
								Cell randomLocalAdjacentCell = base.ParentObject.CurrentCell.GetRandomLocalAdjacentCell();
								if (randomLocalAdjacentCell != null)
								{
									equipped.Physics.CurrentCell = randomLocalAdjacentCell;
									randomLocalAdjacentCell.AddObject(equipped, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
									body.DidXToY("toss", equipped, "aside", null, null, null, null, null, false, false, true, false, false, null, null, null, false, false, false, false, false, null);
								}
							}
						}
					}
					this.Stage++;
					base.ParentObject.UseEnergy(1000);
					return;
				}
				if (this.Stage == 1)
				{
					this.PushGoal(new WanderRandomly(40));
					this.Stage++;
					base.ParentObject.UseEnergy(1000);
					return;
				}
				if (this.Stage == 2)
				{
					base.ParentObject.ApplyEffect(new Asleep(100, false, false, false, true), null);
					this.Stage++;
					base.ParentObject.UseEnergy(1000);
					base.Pop();
					return;
				}
			}
			if (this.Style == 1)
			{
				if (this.Stage == 0)
				{
					if (base.ParentObject.CurrentCell != null)
					{
						GameObject gameObject = base.ParentObject.CurrentCell.ParentZone.FindClosestObjectWithTag(base.ParentObject, "Wall", false);
						if (gameObject != null)
						{
							this.bCanFight = true;
							this.PushGoal(new Kill(gameObject, false));
						}
						this.Stage++;
						base.ParentObject.UseEnergy(1000);
					}
					return;
				}
				if (this.Stage == 1)
				{
					base.ParentObject.ApplyEffect(new Asleep(100, false, false, false, true), null);
					this.Stage++;
					base.ParentObject.UseEnergy(1000);
					base.Pop();
					return;
				}
			}
			if (this.Style == 2)
			{
				if (this.Stage == 0)
				{
					if (base.ParentObject.IsVisible())
					{
						base.ParentObject.ParticleText("KLANQ!", 'O', false, 1.5f, -8f);
						MessageQueue.AddPlayerMessage(base.ParentObject.Does("shout", int.MaxValue, null, null, null, false, false, false, false, true, true, true, false, false, null, false, false, true, null, true, null, false) + " shouts {{O|KLANQ}}!", null, true);
					}
					base.ParentObject.UseEnergy(1000);
					if (5.in100())
					{
						this.Stage++;
					}
					return;
				}
				if (this.Stage == 1)
				{
					base.ParentObject.ApplyEffect(new Asleep(100, false, false, false, true), null);
					this.Stage++;
					base.ParentObject.UseEnergy(1000);
					base.Pop();
					return;
				}
			}
		}

		// Token: 0x04003654 RID: 13908
		private bool bCanFight;

		// Token: 0x04003655 RID: 13909
		private int Style = -1;

		// Token: 0x04003656 RID: 13910
		private int Stage;
	}
}
