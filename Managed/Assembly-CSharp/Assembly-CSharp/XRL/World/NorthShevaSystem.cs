using System;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x02000883 RID: 2179
	[Serializable]
	public class NorthShevaSystem : IPlayerSystem
	{
		// Token: 0x06005661 RID: 22113 RVA: 0x0019AFCD File Offset: 0x001991CD
		public override void Register(XRLGame Game, IEventRegistrar Registrar)
		{
			Registrar.Register(PooledEvent<CanTravelEvent>.ID, 0, false);
			Registrar.Register(PooledEvent<BeforeTravelDownEvent>.ID, 0, false);
			Registrar.Register(PooledEvent<GenericQueryEvent>.ID, 0, false);
		}

		// Token: 0x06005662 RID: 22114 RVA: 0x0019AFF8 File Offset: 0x001991F8
		public override bool HandleEvent(CanTravelEvent E)
		{
			Zone currentZone = E.Object.CurrentZone;
			if (((currentZone != null) ? currentZone.ZoneWorld : null) != "NorthSheva")
			{
				return true;
			}
			Vehicle vehicle;
			if (!E.Object.TryGetPart<Vehicle>(out vehicle) || vehicle.Type != "Mover")
			{
				return E.Object.ShowFailure("You can't travel in this environment.");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06005663 RID: 22115 RVA: 0x0019B064 File Offset: 0x00199264
		public override bool HandleEvent(BeforeTravelDownEvent E)
		{
			if (E.World == "NorthSheva")
			{
				GameObject firstObject = E.Cell.GetFirstObject((GameObject x) => x.GetBlueprint(true).DescendsFrom("TerrainMoverStop"));
				if (firstObject == null)
				{
					return E.Actor.ShowFailure("You can't land here.");
				}
				if (firstObject.GetBlueprint(true).DescendsFrom("TerrainMoverStopRuined"))
				{
					return E.Actor.ShowFailure("That stop is too ruined for the mover to land.");
				}
			}
			return true;
		}

		// Token: 0x06005664 RID: 22116 RVA: 0x0019B0E8 File Offset: 0x001992E8
		public override bool HandleEvent(GenericQueryEvent E)
		{
			if (E.Query == "ActivateSpiralBorer")
			{
				GameObject subject = E.Subject;
				string text;
				if (subject == null)
				{
					text = null;
				}
				else
				{
					Zone currentZone = subject.CurrentZone;
					text = ((currentZone != null) ? currentZone.ZoneWorld : null);
				}
				if (text == "NorthSheva")
				{
					return E.Result = E.Subject.ShowFailure("You cannot do that here.");
				}
			}
			return true;
		}
	}
}
