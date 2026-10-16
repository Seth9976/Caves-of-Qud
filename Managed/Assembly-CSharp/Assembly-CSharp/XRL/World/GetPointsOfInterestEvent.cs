using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using Genkit;
using XRL.World.Effects;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x02000774 RID: 1908
	[GameEvent(Cascade = 256, Cache = MinEvent.Cache.Pool)]
	public class GetPointsOfInterestEvent : PooledEvent<GetPointsOfInterestEvent>
	{
		// Token: 0x06004E6F RID: 20079 RVA: 0x0018073C File Offset: 0x0017E93C
		public override int GetCascadeLevel()
		{
			return GetPointsOfInterestEvent.CascadeLevel;
		}

		// Token: 0x06004E70 RID: 20080 RVA: 0x00180743 File Offset: 0x0017E943
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004E71 RID: 20081 RVA: 0x0018074C File Offset: 0x0017E94C
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Zone = null;
			this.List.Clear();
		}

		// Token: 0x06004E72 RID: 20082 RVA: 0x00180770 File Offset: 0x0017E970
		public PointOfInterest Find(GameObject Object)
		{
			foreach (PointOfInterest pointOfInterest in this.List)
			{
				if (pointOfInterest.Object == Object)
				{
					return pointOfInterest;
				}
			}
			return null;
		}

		// Token: 0x06004E73 RID: 20083 RVA: 0x001807CC File Offset: 0x0017E9CC
		public PointOfInterest Find(string Key)
		{
			foreach (PointOfInterest pointOfInterest in this.List)
			{
				if (pointOfInterest.Key == Key)
				{
					return pointOfInterest;
				}
			}
			return null;
		}

		// Token: 0x06004E74 RID: 20084 RVA: 0x00180830 File Offset: 0x0017EA30
		public void Remove(PointOfInterest p)
		{
			this.List.Remove(p);
		}

		// Token: 0x06004E75 RID: 20085 RVA: 0x00180840 File Offset: 0x0017EA40
		public PointOfInterest Add(GameObject Object = null, string DisplayName = null, string Explanation = null, string Key = null, string Preposition = null, Location2D Location = null, Cell Cell = null, int Radius = -1, IRenderable Icon = null, int Order = 0)
		{
			PointOfInterest pointOfInterest = new PointOfInterest();
			pointOfInterest.Object = Object;
			pointOfInterest.DisplayName = DisplayName;
			pointOfInterest.Explanation = Explanation;
			pointOfInterest.Key = Key;
			if (!string.IsNullOrEmpty(Preposition))
			{
				pointOfInterest.Preposition = Preposition;
			}
			pointOfInterest.Location = Location ?? ((Cell != null) ? Cell.Location : null);
			pointOfInterest.Radius = Radius;
			pointOfInterest.Icon = Icon;
			pointOfInterest.Order = Order;
			this.List.Add(pointOfInterest);
			return pointOfInterest;
		}

		// Token: 0x06004E76 RID: 20086 RVA: 0x001808C0 File Offset: 0x0017EAC0
		public bool StandardChecks(IPart Part = null, GameObject Actor = null, GameObject Object = null)
		{
			GameObject gameObject = Object ?? ((Part != null) ? Part.ParentObject : null);
			if (!GameObject.Validate(ref gameObject))
			{
				return false;
			}
			GameObject gameObject2 = Actor ?? this.Actor;
			if (gameObject == gameObject2)
			{
				return false;
			}
			if (((Part != null) ? Part.Name : null) != "Interesting" && gameObject.HasPart<Interesting>())
			{
				return false;
			}
			if (gameObject.HasPart<FungalVision>() && FungalVisionary.VisionLevel <= 0)
			{
				return false;
			}
			Render render = gameObject.Render;
			if (render == null || !render.Visible)
			{
				return false;
			}
			Cell currentCell = gameObject.CurrentCell;
			return currentCell != null && currentCell.IsExplored() && this.Find(gameObject) == null && !gameObject.IsHostileTowards(gameObject2) && !gameObject.IsLedBy(gameObject2) && gameObject.Understood();
		}

		// Token: 0x06004E77 RID: 20087 RVA: 0x00180990 File Offset: 0x0017EB90
		public static List<PointOfInterest> GetFor(GameObject Actor, Zone Z = null)
		{
			if (Z == null)
			{
				Z = ((Actor != null) ? Actor.CurrentZone : null);
			}
			List<PointOfInterest> list = null;
			if (Z != null)
			{
				GetPointsOfInterestEvent getPointsOfInterestEvent = PooledEvent<GetPointsOfInterestEvent>.FromPool();
				getPointsOfInterestEvent.Actor = Actor;
				getPointsOfInterestEvent.Zone = Z;
				getPointsOfInterestEvent.List.Clear();
				Z.HandleEvent(getPointsOfInterestEvent);
				if (getPointsOfInterestEvent.List.Count > 0)
				{
					list = new List<PointOfInterest>(getPointsOfInterestEvent.List);
				}
			}
			return list;
		}

		// Token: 0x06004E78 RID: 20088 RVA: 0x001809F8 File Offset: 0x0017EBF8
		public static PointOfInterest GetOne(GameObject Actor, string Key, Zone Z = null)
		{
			if (Z == null)
			{
				Z = ((Actor != null) ? Actor.CurrentZone : null);
			}
			if (Z != null)
			{
				GetPointsOfInterestEvent getPointsOfInterestEvent = PooledEvent<GetPointsOfInterestEvent>.FromPool();
				getPointsOfInterestEvent.Actor = Actor;
				getPointsOfInterestEvent.Zone = Z;
				getPointsOfInterestEvent.List.Clear();
				Z.HandleEvent(getPointsOfInterestEvent);
				return getPointsOfInterestEvent.Find(Key);
			}
			return null;
		}

		// Token: 0x06004E79 RID: 20089 RVA: 0x00180A4C File Offset: 0x0017EC4C
		public static bool Check(GameObject Object, GameObject Actor, Zone Z = null)
		{
			if (GameObject.Validate(ref Object))
			{
				if (Z == null)
				{
					Z = ((Actor != null) ? Actor.CurrentZone : null);
				}
				if (Z != null)
				{
					GetPointsOfInterestEvent getPointsOfInterestEvent = PooledEvent<GetPointsOfInterestEvent>.FromPool();
					getPointsOfInterestEvent.Actor = Actor;
					getPointsOfInterestEvent.Zone = Z;
					getPointsOfInterestEvent.List.Clear();
					Object.HandleEvent(getPointsOfInterestEvent);
					if (getPointsOfInterestEvent.Find(Object) != null)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06004E7A RID: 20090 RVA: 0x00180AAC File Offset: 0x0017ECAC
		public static bool AnyFor(GameObject Actor, Zone Z = null)
		{
			bool flag = false;
			if (Z == null)
			{
				Z = ((Actor != null) ? Actor.CurrentZone : null);
			}
			if (Z != null)
			{
				GetPointsOfInterestEvent getPointsOfInterestEvent = PooledEvent<GetPointsOfInterestEvent>.FromPool();
				getPointsOfInterestEvent.Actor = Actor;
				getPointsOfInterestEvent.Zone = Z;
				getPointsOfInterestEvent.List.Clear();
				Z.HandleEvent(getPointsOfInterestEvent);
				if (getPointsOfInterestEvent.List.Count > 0)
				{
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x0400264D RID: 9805
		public new static readonly int CascadeLevel = 256;

		// Token: 0x0400264E RID: 9806
		public GameObject Actor;

		// Token: 0x0400264F RID: 9807
		public Zone Zone;

		// Token: 0x04002650 RID: 9808
		public List<PointOfInterest> List = new List<PointOfInterest>();
	}
}
