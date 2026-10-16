using System;
using System.Text;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013D4 RID: 5076
	[Serializable]
	public class IrritableGenome : BaseMutation
	{
		// Token: 0x0600C426 RID: 50214 RVA: 0x0047789D File Offset: 0x00475A9D
		public override bool CanLevel()
		{
			return false;
		}

		// Token: 0x0600C427 RID: 50215 RVA: 0x004778A0 File Offset: 0x00475AA0
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C428 RID: 50216 RVA: 0x004778B6 File Offset: 0x00475AB6
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("chance", this.BaseElementWeight);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C429 RID: 50217 RVA: 0x004778DE File Offset: 0x00475ADE
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("GainedMP");
			Registrar.Register("UsedMP");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C42A RID: 50218 RVA: 0x004778FE File Offset: 0x00475AFE
		public override string GetDescription()
		{
			return "Your genome is irritable and unpredictable.\n\nWhenever you spend a mutation point, the next mutation point you gain will be spent randomly.\nWhenever you buy a new mutation, you get a random one instead of a choice of three.";
		}

		// Token: 0x0600C42B RID: 50219 RVA: 0x00477905 File Offset: 0x00475B05
		public override string GetLevelText(int Level)
		{
			return "";
		}

		// Token: 0x0600C42C RID: 50220 RVA: 0x0047790C File Offset: 0x00475B0C
		public void TrySpending()
		{
			if (this.MPSpentMemory > 0)
			{
				this.insidetry = true;
				try
				{
					int num = this.ParentObject.Stat("MP", 0);
					int num2 = Math.Min(num, this.MPSpentMemory);
					StringBuilder stringBuilder = Event.NewStringBuilder(null);
					if (this.ParentObject.IsPlayer())
					{
						stringBuilder = new StringBuilder();
					}
					this.ParentObject.RandomlySpendPoints(0, 0, num2, stringBuilder);
					int num3 = num - this.ParentObject.Stat("MP", 0);
					if (num3 > 0)
					{
						this.ParentObject.PlayWorldSound("sfx_characterTrigger_irritableGenome", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
						IComponent<GameObject>.EmitMessage(this.ParentObject, this.ParentObject.Poss("irritable genome acts up." + stringBuilder.ToString(), true), ' ', true, false, false, null, null);
						this.MPSpentMemory -= num3;
					}
				}
				catch (Exception ex)
				{
					MetricsManager.LogException("IrritableGenome::GainedMP", ex, "game_exception");
				}
				this.insidetry = false;
			}
		}

		// Token: 0x0600C42D RID: 50221 RVA: 0x00477A24 File Offset: 0x00475C24
		public override bool FireEvent(Event E)
		{
			if (E.ID == "UsedMP" && !this.insidetry && E.GetStringParameter("Context", null) != "BuyNew")
			{
				this.MPSpentMemory += E.GetIntParameter("Amount", 0);
				this.TrySpending();
			}
			if (E.ID == "GainedMP")
			{
				this.TrySpending();
			}
			return true;
		}

		// Token: 0x0600C42E RID: 50222 RVA: 0x00477A9B File Offset: 0x00475C9B
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x04004DCF RID: 19919
		[NonSerialized]
		private bool insidetry;

		// Token: 0x04004DD0 RID: 19920
		public int MPSpentMemory;
	}
}
