using System;

namespace XRL.World.Parts
{
	// Token: 0x02000F5B RID: 3931
	[WantLoadBlueprint]
	[Serializable]
	public class HasMakersMark : IPart
	{
		// Token: 0x06009CBC RID: 40124 RVA: 0x0038CCE0 File Offset: 0x0038AEE0
		public static void LoadBlueprint(GameObjectBlueprint Blueprint)
		{
			string text;
			if (Blueprint.TryGetPartParameter<string>("HasMakersMark", "Mark", out text) && !text.IsNullOrEmpty())
			{
				MakersMark.RecordUsage(text);
			}
		}

		// Token: 0x06009CBD RID: 40125 RVA: 0x0038CD0F File Offset: 0x0038AF0F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ReplicaCreatedEvent.ID;
		}

		// Token: 0x06009CBE RID: 40126 RVA: 0x0038CD25 File Offset: 0x0038AF25
		public override bool HandleEvent(ReplicaCreatedEvent E)
		{
			this.Mark = MakersMark.Generate(true);
			this.Color = Crayons.GetRandomColor(null);
			return base.HandleEvent(E);
		}

		// Token: 0x06009CBF RID: 40127 RVA: 0x0038CD46 File Offset: 0x0038AF46
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x040040D0 RID: 16592
		public string Mark;

		// Token: 0x040040D1 RID: 16593
		public string Color;
	}
}
