using System;
using XRL.World.Tinkering;

namespace XRL.World.Parts
{
	// Token: 0x020011EA RID: 4586
	[Serializable]
	public class ExtradimensionalLoot : IPart
	{
		// Token: 0x0600B6AE RID: 46766 RVA: 0x00429EC6 File Offset: 0x004280C6
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B6AF RID: 46767 RVA: 0x00429EC9 File Offset: 0x004280C9
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeforeDeathRemoval");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B6B0 RID: 46768 RVA: 0x00429EE0 File Offset: 0x004280E0
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeforeDeathRemoval")
			{
				try
				{
					Cell currentCell = this.ParentObject.GetCurrentCell();
					if (currentCell != null && 5.in100())
					{
						GameObject mostValuableItem = this.ParentObject.GetMostValuableItem();
						if (mostValuableItem != null)
						{
							mostValuableItem.SplitStack(1, this.ParentObject, false);
							mostValuableItem.RemovePart<Temporary>();
							mostValuableItem.RemovePart<ExistenceSupport>();
							if (!mostValuableItem.HasPart<ModExtradimensional>())
							{
								Extradimensional part = this.ParentObject.GetPart<Extradimensional>();
								ItemModding.ApplyModification(mostValuableItem, new ModExtradimensional(part.WeaponModIndex, part.MissileWeaponModIndex, part.ArmorModIndex, part.ShieldModIndex, part.MiscModIndex, part.Training, part.DimensionName, part.SecretID), true, null, false);
								mostValuableItem.SetStringProperty("NeverStack", "1", false);
							}
							if (base.Visible())
							{
								IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
								{
									this.ParentObject.Does("drop", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
									" ",
									mostValuableItem.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
									", and by sheer chance ",
									mostValuableItem.does("quantum tunnel", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, true, true, null, true, null, false),
									" and fully",
									mostValuableItem.GetVerb("materialize", true, false, false, true, false),
									" in this dimension."
								}), null, true);
							}
							currentCell.AddObject(mostValuableItem, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						}
					}
				}
				catch (Exception ex)
				{
					MetricsManager.LogException("PsychicHunter loot", ex, "game_exception");
				}
				return true;
			}
			return true;
		}
	}
}
