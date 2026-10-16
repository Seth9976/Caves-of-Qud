using System;

namespace XRL.World
{
	// Token: 0x02000934 RID: 2356
	public interface IEvent
	{
		// Token: 0x06005B11 RID: 23313
		void RequestInterfaceExit();

		// Token: 0x06005B12 RID: 23314
		bool InterfaceExitRequested();

		// Token: 0x06005B13 RID: 23315
		void PreprocessChildEvent(IEvent E);

		// Token: 0x06005B14 RID: 23316
		void ProcessChildEvent(IEvent E);

		// Token: 0x06005B15 RID: 23317
		bool ActuateOn(GameObject obj);
	}
}
