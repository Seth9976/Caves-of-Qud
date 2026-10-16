using System;

namespace XRL.World.Parts
{
	// Token: 0x020011E9 RID: 4585
	[Serializable]
	public class Extradimensional : IPart
	{
		// Token: 0x0600B6A9 RID: 46761 RVA: 0x00429E0E File Offset: 0x0042800E
		public Extradimensional()
		{
			this.DimensionName = "";
		}

		// Token: 0x0600B6AA RID: 46762 RVA: 0x00429E24 File Offset: 0x00428024
		public Extradimensional(string DimensionName, int WeaponModIndex, int MissileWeaponModIndex, int ArmorModIndex, int ShieldModIndex, int MiscModIndex, string Training, string SecretID)
			: this()
		{
			this.DimensionName = DimensionName;
			this.WeaponModIndex = WeaponModIndex;
			this.MissileWeaponModIndex = MissileWeaponModIndex;
			this.ArmorModIndex = ArmorModIndex;
			this.ShieldModIndex = ShieldModIndex;
			this.Training = Training;
			this.SecretID = SecretID;
			this.MiscModIndex = MiscModIndex;
		}

		// Token: 0x0600B6AB RID: 46763 RVA: 0x00429E74 File Offset: 0x00428074
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600B6AC RID: 46764 RVA: 0x00429E77 File Offset: 0x00428077
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600B6AD RID: 46765 RVA: 0x00429E8D File Offset: 0x0042808D
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (!string.IsNullOrEmpty(this.DimensionName))
			{
				E.Postfix.Compound("This creature is native to the dimension known as " + this.DimensionName + ".", '\n');
			}
			return base.HandleEvent(E);
		}

		// Token: 0x040049AA RID: 18858
		public string DimensionName;

		// Token: 0x040049AB RID: 18859
		public int WeaponModIndex;

		// Token: 0x040049AC RID: 18860
		public int MissileWeaponModIndex;

		// Token: 0x040049AD RID: 18861
		public int ArmorModIndex;

		// Token: 0x040049AE RID: 18862
		public int ShieldModIndex;

		// Token: 0x040049AF RID: 18863
		public int MiscModIndex;

		// Token: 0x040049B0 RID: 18864
		public string Training;

		// Token: 0x040049B1 RID: 18865
		public string SecretID;
	}
}
