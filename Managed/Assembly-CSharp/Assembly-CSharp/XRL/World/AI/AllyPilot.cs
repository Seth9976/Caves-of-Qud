using System;
using XRL.World.Parts;

namespace XRL.World.AI
{
	// Token: 0x02000B61 RID: 2913
	public class AllyPilot : IAllyReason
	{
		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06007514 RID: 29972 RVA: 0x002BF175 File Offset: 0x002BD375
		public override IAllyReason.ReplaceTarget Replace
		{
			get
			{
				return IAllyReason.ReplaceTarget.Type;
			}
		}

		// Token: 0x06007515 RID: 29973 RVA: 0x002BF178 File Offset: 0x002BD378
		public override string GetText(GameObject Actor)
		{
			Vehicle vehicle;
			if (Actor.TryGetPart<Vehicle>(out vehicle) && !vehicle.PilotID.IsNullOrEmpty())
			{
				return "I am being piloted by " + vehicle.Pilot.an(int.MaxValue, null, null, false, false, false, false, true, false, true, true, false, false, false, null, false, null, false) + ".";
			}
			return "I am being piloted.";
		}
	}
}
