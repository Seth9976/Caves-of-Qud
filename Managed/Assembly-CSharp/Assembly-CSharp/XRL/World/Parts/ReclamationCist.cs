using System;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02000FAF RID: 4015
	[Serializable]
	public class ReclamationCist : IPoweredPart
	{
		// Token: 0x06009FAF RID: 40879 RVA: 0x003A083C File Offset: 0x0039EA3C
		public ReclamationCist()
		{
			this.ChargeUse = 500;
			this.WorksOnInventory = true;
			this.NameForStatus = "ReclamationSystems";
		}

		// Token: 0x06009FB0 RID: 40880 RVA: 0x003A0877 File Offset: 0x0039EA77
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EndTurn");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009FB1 RID: 40881 RVA: 0x003A088C File Offset: 0x0039EA8C
		private bool PerformReclamationOf(GameObject obj)
		{
			GameObjectBlueprint blueprint = obj.GetBlueprint(true);
			if (!blueprint.DescendsFrom("Corpse"))
			{
				return true;
			}
			if (!this.RequireGenotype.IsNullOrEmpty())
			{
				string text = obj.GetPropertyOrTag("FromGenotype", null);
				DismemberedProperties dismemberedProperties;
				if (obj.TryGetPart<DismemberedProperties>(out dismemberedProperties) && !dismemberedProperties.SourceGenotype.IsNullOrEmpty() && dismemberedProperties.BodyPart.Native)
				{
					text = dismemberedProperties.SourceGenotype;
				}
				if (text != this.RequireGenotype)
				{
					return true;
				}
			}
			bool flag = blueprint.DescendsFrom("BaseLimb");
			CyberneticsButcherableCybernetic part = obj.GetPart<CyberneticsButcherableCybernetic>();
			if (part != null && part.AttemptButcher(this.ParentObject, false, true, true, 10, null, null))
			{
				if (!this.ProduceBlueprint.IsNullOrEmpty())
				{
					this.ParentObject.ReceiveObject(this.ProduceBlueprint, flag ? Stat.Random(1, 3) : Stat.Random(5, 10), false, 0, 0, null, null, null, null);
				}
				return false;
			}
			obj = obj.RemoveOne();
			if (base.Visible())
			{
				IComponent<GameObject>.AddPlayerMessage(this.ParentObject.Does("reclaim", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " " + obj.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false) + ".", null, true);
			}
			obj.Destroy(null, false, false, null);
			if (!this.ProduceBlueprint.IsNullOrEmpty())
			{
				this.ParentObject.ReceiveObject(this.ProduceBlueprint, flag ? Stat.Random(2, 4) : Stat.Random(6, 12), false, 0, 0, null, null, null, null);
			}
			return false;
		}

		// Token: 0x06009FB2 RID: 40882 RVA: 0x003A0A34 File Offset: 0x0039EC34
		public bool PerformReclamation()
		{
			bool flag = this.ForeachActivePartSubjectWhile(new Predicate<GameObject>(this.PerformReclamationOf), true);
			if (!flag)
			{
				for (int i = Stat.Random(1, 5); i >= 0; i--)
				{
					this.ParentObject.Bloodsplatter();
				}
				if (this.ChargeUse > 0)
				{
					this.ParentObject.UseCharge(this.ChargeUse, false, 0L, true, true, null);
				}
			}
			return flag;
		}

		// Token: 0x06009FB3 RID: 40883 RVA: 0x003A0AA0 File Offset: 0x0039ECA0
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EndTurn" && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				this.PerformReclamation();
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004247 RID: 16967
		public string ProduceBlueprint = "Food Cube";

		// Token: 0x04004248 RID: 16968
		public string RequireGenotype = "True Kin";
	}
}
