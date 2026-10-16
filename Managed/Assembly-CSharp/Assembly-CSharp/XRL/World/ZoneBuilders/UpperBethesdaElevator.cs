using System;
using XRL.World.Parts;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A4D RID: 2637
	public class UpperBethesdaElevator
	{
		// Token: 0x06006DE7 RID: 28135 RVA: 0x00254D14 File Offset: 0x00252F14
		public bool BuildZone(Zone Z)
		{
			Box box = new Box(27, 9, 36, 15);
			int num = 30;
			int num2 = 12;
			Z.ClearBox(box, null);
			Z.GetCell(num, num2).AddObject("OpenShaft", null, null, null, null);
			GameObject gameObject = GameObjectFactory.Factory.CreateObject("ElevatorSwitch");
			ElevatorSwitch part = gameObject.GetPart<ElevatorSwitch>();
			part.TopLevel = 15;
			part.FloorLevel = 21;
			Z.GetCell(num, num2 - 1).AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			if (Z.Z == 15)
			{
				Z.GetCell(num, num2).AddObject("Platform", null, null, null, null);
			}
			Z.BuildReachableMap(num, num2);
			return true;
		}
	}
}
