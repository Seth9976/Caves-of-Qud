using System;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001457 RID: 5207
	[Serializable]
	public class ThickFur : BaseMutation
	{
		// Token: 0x0600CBCF RID: 52175 RVA: 0x004A1BCD File Offset: 0x0049FDCD
		public override bool CanLevel()
		{
			return false;
		}

		// Token: 0x0600CBD0 RID: 52176 RVA: 0x004A1BD0 File Offset: 0x0049FDD0
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			base.Register(Object, Registrar);
		}

		// Token: 0x0600CBD1 RID: 52177 RVA: 0x004A1BDA File Offset: 0x0049FDDA
		public override string GetDescription()
		{
			return "" + "You are covered in a thick coat of fur, which protects you from the elements.\n\n" + "+5 Heat Resistance\n+5 Cold Resistance\n+100 reputation with {{w|apes}}, {{w|baboons}}, {{w|bears}}, and {{w|grazing hedonists}}";
		}

		// Token: 0x0600CBD2 RID: 52178 RVA: 0x004A1BF5 File Offset: 0x0049FDF5
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetExtraPhysicalFeaturesEvent>.ID;
		}

		// Token: 0x0600CBD3 RID: 52179 RVA: 0x004A1C0B File Offset: 0x0049FE0B
		public override bool HandleEvent(GetExtraPhysicalFeaturesEvent E)
		{
			E.Features.Add("thick fur");
			return base.HandleEvent(E);
		}

		// Token: 0x0600CBD4 RID: 52180 RVA: 0x004A1C24 File Offset: 0x0049FE24
		public override string GetLevelText(int Level)
		{
			return "";
		}

		// Token: 0x0600CBD5 RID: 52181 RVA: 0x004A1C2B File Offset: 0x0049FE2B
		public override bool FireEvent(Event E)
		{
			return base.FireEvent(E);
		}

		// Token: 0x0600CBD6 RID: 52182 RVA: 0x004A1C34 File Offset: 0x0049FE34
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600CBD7 RID: 52183 RVA: 0x004A1C3D File Offset: 0x0049FE3D
		public override bool Mutate(GameObject GO, int Level)
		{
			base.StatShifter.SetStatShift(GO, "HeatResistance", 5, true);
			base.StatShifter.SetStatShift(GO, "ColdResistance", 5, true);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600CBD8 RID: 52184 RVA: 0x004A1C6F File Offset: 0x0049FE6F
		public override bool Unmutate(GameObject GO)
		{
			base.StatShifter.RemoveStatShifts(GO);
			return base.Unmutate(GO);
		}
	}
}
