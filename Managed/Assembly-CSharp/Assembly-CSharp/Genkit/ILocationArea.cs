using System;
using System.Collections.Generic;

namespace Genkit
{
	// Token: 0x02000378 RID: 888
	public interface ILocationArea
	{
		// Token: 0x06002130 RID: 8496
		IEnumerable<Location2D> EnumerateLocations();

		// Token: 0x06002131 RID: 8497
		IEnumerable<Location2D> EnumerateBorderLocations();

		// Token: 0x06002132 RID: 8498
		IEnumerable<Location2D> EnumerateNonBorderLocations();

		// Token: 0x06002133 RID: 8499
		Location2D GetCenter();

		// Token: 0x06002134 RID: 8500
		bool PointIn(Location2D location);
	}
}
