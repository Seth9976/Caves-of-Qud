using System;

namespace XRL.World.Parts
{
	// Token: 0x02000FCC RID: 4044
	public abstract class IGasBehavior : IPart
	{
		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x0600A0D1 RID: 41169 RVA: 0x003A857C File Offset: 0x003A677C
		public Gas BaseGas
		{
			get
			{
				Gas gas;
				if ((gas = this._BaseGas) == null)
				{
					gas = (this._BaseGas = this.ParentObject.GetPart<Gas>());
				}
				return gas;
			}
		}

		// Token: 0x0600A0D2 RID: 41170 RVA: 0x003A85A7 File Offset: 0x003A67A7
		public int GasDensity()
		{
			Gas baseGas = this.BaseGas;
			if (baseGas == null)
			{
				return 0;
			}
			return baseGas.Density;
		}

		// Token: 0x0600A0D3 RID: 41171 RVA: 0x003A85BA File Offset: 0x003A67BA
		public int GasDensityStepped(int Step = 5)
		{
			return base.StepValue(this.GasDensity(), Step);
		}

		// Token: 0x040042C0 RID: 17088
		[NonSerialized]
		private Gas _BaseGas;
	}
}
