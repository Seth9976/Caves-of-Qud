using System;
using System.Text;

namespace XRL.World.Parts
{
	// Token: 0x02000FA5 RID: 4005
	[Serializable]
	public class FoodProcessor : IPoweredPart
	{
		// Token: 0x06009F23 RID: 40739 RVA: 0x0039C91A File Offset: 0x0039AB1A
		public FoodProcessor()
		{
			this.ChargeUse = 500;
			this.WorksOnInventory = true;
			this.NameForStatus = "FoodProcessing";
		}

		// Token: 0x06009F24 RID: 40740 RVA: 0x0039C93F File Offset: 0x0039AB3F
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EndTurn");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009F25 RID: 40741 RVA: 0x0039C954 File Offset: 0x0039AB54
		private bool ProcessFoodItem(GameObject obj)
		{
			Butcherable part = obj.GetPart<Butcherable>();
			if (part != null && part.AttemptButcher(this.ParentObject, false, true, true, null, null, null))
			{
				return false;
			}
			if (obj.GetPart<PreservableItem>() != null)
			{
				StringBuilder stringBuilder = Event.NewStringBuilder(null);
				stringBuilder.Append(this.ParentObject.The).Append(this.ParentObject.DisplayNameOnly).Append(this.ParentObject.GetVerb("preserve", true, false, false, true, false))
					.Append(' ');
				if (Campfire.PerformPreserve(obj, stringBuilder, this.ParentObject, false, false))
				{
					if (base.Visible())
					{
						stringBuilder.Append('.');
						IComponent<GameObject>.AddPlayerMessage(stringBuilder.ToString(), null, true);
					}
					return false;
				}
			}
			return true;
		}

		// Token: 0x06009F26 RID: 40742 RVA: 0x0039CA08 File Offset: 0x0039AC08
		public bool ProcessFood()
		{
			bool flag = this.ForeachActivePartSubjectWhile(new Predicate<GameObject>(this.ProcessFoodItem), true);
			if (!flag)
			{
				base.ConsumeCharge(null, null);
			}
			return flag;
		}

		// Token: 0x06009F27 RID: 40743 RVA: 0x0039CA44 File Offset: 0x0039AC44
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EndTurn" && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				this.ProcessFood();
			}
			return base.FireEvent(E);
		}
	}
}
