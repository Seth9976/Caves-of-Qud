using System;
using System.Collections.Generic;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000F26 RID: 3878
	[Serializable]
	public class CyberneticsElectromagneticSensor : IPart
	{
		// Token: 0x06009AA1 RID: 39585 RVA: 0x0038181F File Offset: 0x0037FA1F
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009AA2 RID: 39586 RVA: 0x00381822 File Offset: 0x0037FA22
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x06009AA3 RID: 39587 RVA: 0x00381828 File Offset: 0x0037FA28
		public override void TurnTick(long TimeTick, int Amount)
		{
			GameObject implantee = this.ParentObject.Implantee;
			if (implantee != null && implantee.IsPlayer())
			{
				List<GameObject> heardObjects = this.HeardObjects;
				Cell currentCell = implantee.CurrentCell;
				if (currentCell != null)
				{
					int radius = this.GetRadius(implantee);
					this.HeardObjects = currentCell.ParentZone.FastSquareSearch(currentCell.X, currentCell.Y, radius, "Combat");
					foreach (GameObject gameObject in heardObjects)
					{
						if (!this.HeardObjects.Contains(gameObject))
						{
							gameObject.RemoveEffect(typeof(SenseRobotEffect), new Predicate<Effect>(this.IsOurs));
						}
					}
					int i = 0;
					int count = this.HeardObjects.Count;
					while (i < count)
					{
						GameObject gameObject2 = this.HeardObjects[i];
						if (!gameObject2.HasEffect(typeof(SenseRobotEffect), new Predicate<Effect>(this.IsOurs)) && CyberneticsElectromagneticSensor.WillSense(gameObject2))
						{
							gameObject2.ApplyEffect(new SenseRobotEffect(radius, implantee, this.ParentObject), null);
						}
						i++;
					}
				}
			}
		}

		// Token: 0x06009AA4 RID: 39588 RVA: 0x00381968 File Offset: 0x0037FB68
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<ExtraHostilePerceptionEvent>.ID || ID == GetShortDescriptionEvent.ID || ID == UnimplantedEvent.ID;
		}

		// Token: 0x06009AA5 RID: 39589 RVA: 0x00381990 File Offset: 0x0037FB90
		public override bool HandleEvent(ExtraHostilePerceptionEvent E)
		{
			GameObject implantee = this.ParentObject.Implantee;
			if (implantee != null && E.Actor == implantee && implantee.IsPlayer())
			{
				Cell currentCell = implantee.CurrentCell;
				if (currentCell != null)
				{
					List<GameObject> list = currentCell.ParentZone.FastSquareSearch(currentCell.X, currentCell.Y, this.GetRadius(), "Combat");
					int i = 0;
					int count = list.Count;
					while (i < count)
					{
						GameObject gameObject = list[i];
						SenseRobotEffect senseRobotEffect = gameObject.GetEffect(typeof(SenseRobotEffect), new Predicate<Effect>(this.IsOurs)) as SenseRobotEffect;
						if (senseRobotEffect != null && senseRobotEffect.Listener == implantee && senseRobotEffect.Identified && implantee.IsRelevantHostile(gameObject))
						{
							E.Hostile = gameObject;
							E.PerceiveVerb = "detect";
						}
						i++;
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009AA6 RID: 39590 RVA: 0x00381A73 File Offset: 0x0037FC73
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules("Compute power on the local lattice increases this item's range.");
			return base.HandleEvent(E);
		}

		// Token: 0x06009AA7 RID: 39591 RVA: 0x00381A90 File Offset: 0x0037FC90
		public override bool HandleEvent(UnimplantedEvent E)
		{
			foreach (GameObject gameObject in this.HeardObjects)
			{
				gameObject.RemoveEffect(typeof(SenseRobotEffect), new Predicate<Effect>(this.IsOurs));
			}
			this.HeardObjects.Clear();
			return base.HandleEvent(E);
		}

		// Token: 0x06009AA8 RID: 39592 RVA: 0x00381B0C File Offset: 0x0037FD0C
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009AA9 RID: 39593 RVA: 0x00381B0F File Offset: 0x0037FD0F
		public static bool WillSense(GameObject obj)
		{
			return obj.HasPart<Robot>() || obj.HasTag("Robot");
		}

		// Token: 0x06009AAA RID: 39594 RVA: 0x00381B2C File Offset: 0x0037FD2C
		public bool IsOurs(Effect FX)
		{
			SenseRobotEffect senseRobotEffect = FX as SenseRobotEffect;
			return senseRobotEffect != null && senseRobotEffect.Device == this.ParentObject;
		}

		// Token: 0x06009AAB RID: 39595 RVA: 0x00381B53 File Offset: 0x0037FD53
		public int GetRadius(GameObject who)
		{
			return GetAvailableComputePowerEvent.AdjustUp(who, 9);
		}

		// Token: 0x06009AAC RID: 39596 RVA: 0x00381B5D File Offset: 0x0037FD5D
		public int GetRadius()
		{
			return this.GetRadius(this.ParentObject.Implantee);
		}

		// Token: 0x0400404C RID: 16460
		[NonSerialized]
		public List<GameObject> HeardObjects = new List<GameObject>();
	}
}
