using System;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001417 RID: 5143
	[Serializable]
	public class FattyHump : BaseMutation
	{
		// Token: 0x0600C7F0 RID: 51184 RVA: 0x0048CB74 File Offset: 0x0048AD74
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C7F1 RID: 51185 RVA: 0x0048CB8A File Offset: 0x0048AD8A
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("salt", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C7F2 RID: 51186 RVA: 0x0048CBAD File Offset: 0x0048ADAD
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C7F3 RID: 51187 RVA: 0x0048CBB7 File Offset: 0x0048ADB7
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C7F4 RID: 51188 RVA: 0x0048CBC0 File Offset: 0x0048ADC0
		public override bool Mutate(GameObject GO, int Level)
		{
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C7F5 RID: 51189 RVA: 0x0048CBCA File Offset: 0x0048ADCA
		public override bool Unmutate(GameObject GO)
		{
			return base.Unmutate(GO);
		}
	}
}
