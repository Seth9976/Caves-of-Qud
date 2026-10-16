using System;

namespace XRL.World.Parts
{
	// Token: 0x020011CA RID: 4554
	[Serializable]
	public class GenericPowerTransmission : IPowerTransmission
	{
		// Token: 0x0600B512 RID: 46354 RVA: 0x00421559 File Offset: 0x0041F759
		public override string GetPowerTransmissionType()
		{
			return this.Type;
		}

		// Token: 0x04004904 RID: 18692
		public string Type = "generic";
	}
}
