using System;

namespace XRL.World.Capabilities
{
	// Token: 0x020009E9 RID: 2537
	public interface IFlightSource
	{
		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06006C31 RID: 27697
		int FlightLevel { get; }

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x06006C32 RID: 27698
		int FlightBaseFallChance { get; }

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06006C33 RID: 27699
		bool FlightRequiresOngoingEffort { get; }

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06006C34 RID: 27700
		string FlightEvent { get; }

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06006C35 RID: 27701
		string FlightActivatedAbilityClass { get; }

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06006C36 RID: 27702
		string FlightSourceDescription { get; }

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x06006C38 RID: 27704
		// (set) Token: 0x06006C37 RID: 27703
		bool FlightFlying { get; set; }

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06006C3A RID: 27706
		// (set) Token: 0x06006C39 RID: 27705
		Guid FlightActivatedAbilityID { get; set; }
	}
}
