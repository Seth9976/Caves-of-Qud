using System;
using System.Linq;
using XRL.Rules;

namespace XRL.World.Parts
{
	/// When parent object takes a total of <c>DamagePer</c> <c>DamageTriggerTypes</c> over its lifetime
	/// it has a <c>Chance</c> in 100 chance of creating and placing <c>Number</c> copies of <c>Blueprint</c>.
	// Token: 0x02000E51 RID: 3665
	[Serializable]
	public class BurnOffGas : IPart
	{
		// Token: 0x0600946B RID: 37995 RVA: 0x00356CD0 File Offset: 0x00354ED0
		public override bool SameAs(IPart p)
		{
			BurnOffGas burnOffGas = p as BurnOffGas;
			return burnOffGas.DamagePer == this.DamagePer && burnOffGas.Chance == this.Chance && !(burnOffGas.Number != this.Number) && !(burnOffGas.Blueprint != this.Blueprint) && burnOffGas.PopulationRollsAreStatic == this.PopulationRollsAreStatic && burnOffGas.SpawnAsDropColor == this.SpawnAsDropColor && base.SameAs(p);
		}

		// Token: 0x0600946C RID: 37996 RVA: 0x00356D55 File Offset: 0x00354F55
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeforeTookDamage");
			base.Register(Object, Registrar);
		}

		/// Handles:
		///   "BeforeTookDamage" - checks parameter Damage.Attributes for type match, increases counter, and does spawn logic.
		// Token: 0x0600946D RID: 37997 RVA: 0x00356D6C File Offset: 0x00354F6C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeforeTookDamage")
			{
				Physics physics = this.ParentObject.Physics;
				if (physics == null || physics.CurrentCell == null)
				{
					return true;
				}
				if (!E.GetParameter<Damage>("Damage").Attributes.Any<string>((string s) => this.DamageTriggerTypes.Contains(s)))
				{
					return true;
				}
				this.DamageTaken += E.GetParameter<Damage>("Damage").Amount;
				while (this.DamageTaken >= this.DamagePer)
				{
					this.DamageTaken -= this.DamagePer;
					if (this.Chance.in100() && !this.Blueprint.IsNullOrEmpty() && this.ParentObject.CurrentCell != null)
					{
						int i = 0;
						int num = Stat.Roll(this.Number, null);
						while (i < num)
						{
							string text = this.Blueprint;
							if (text.StartsWith("@"))
							{
								text = PopulationManager.RollOneFrom(text.Substring(1), null, null).Blueprint;
								if (this.PopulationRollsAreStatic)
								{
									this.Blueprint = text;
								}
							}
							GameObject gameObject = GameObject.Create(text);
							this.ParentObject.CurrentCell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
							if (this.ParentObject.IsVisible())
							{
								IComponent<GameObject>.XDidY(this.ParentObject, "burn", "off " + gameObject.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false), null, null, null, null, null, false, false, null, null, false, false, false, false, false, null);
							}
							i++;
						}
					}
				}
			}
			return base.FireEvent(E);
		}

		/// Total damage taken (will never exceed <c>DamagePer</c>).
		// Token: 0x04003D57 RID: 15703
		public int DamageTaken;

		/// Amount of damage needed to trigger
		// Token: 0x04003D58 RID: 15704
		public int DamagePer = 10;

		/// Chance in 100 to trigger
		// Token: 0x04003D59 RID: 15705
		public int Chance = 100;

		/// Number of things to spawn. Accepts roll formula i.e. "1d3-1" could spawn 0, 1, 2 things.
		// Token: 0x04003D5A RID: 15706
		public string Number = "1";

		/// Damage tags that will count toward counter
		// Token: 0x04003D5B RID: 15707
		public string DamageTriggerTypes = "Heat;Fire";

		/// Blueprint to spawn.  If first character is an '@' it rolls an item from the population table
		// Token: 0x04003D5C RID: 15708
		public string Blueprint;

		/// If using a population <c>Blueprint</c> this will "save" the first roll and not roll again when true.
		// Token: 0x04003D5D RID: 15709
		public bool PopulationRollsAreStatic = true;

		/// Unused???
		// Token: 0x04003D5E RID: 15710
		public bool SpawnAsDropColor = true;
	}
}
