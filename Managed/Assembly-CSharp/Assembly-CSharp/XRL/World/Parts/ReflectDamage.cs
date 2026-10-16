using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
	/// <remarks>
	/// overload behavior: if <see cref="!:IsPowerLoadSensitive" /> is true,
	/// which it is not by default, reflection percentage is increased by a
	/// percentage equal to ((power load - 100) / 10), i.e. 30% for
	/// the standard overload power load of 400
	/// </remarks>
	// Token: 0x0200120F RID: 4623
	[Serializable]
	public class ReflectDamage : IActivePart
	{
		// Token: 0x0600B7D6 RID: 47062 RVA: 0x0042E18C File Offset: 0x0042C38C
		public ReflectDamage()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B7D7 RID: 47063 RVA: 0x0042E1A3 File Offset: 0x0042C3A3
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeApplyDamageEvent.ID;
		}

		// Token: 0x0600B7D8 RID: 47064 RVA: 0x0042E1BC File Offset: 0x0042C3BC
		public override bool HandleEvent(BeforeApplyDamageEvent E)
		{
			if (GameObject.Validate(E.Actor) && E.Actor != E.Object && E.Damage.Amount > 0 && !E.Damage.HasAttribute("reflected") && this.IsObjectActivePartSubject(E.Object))
			{
				int num = this.MyPowerLoadLevel();
				int num2 = this.ReflectPercentage;
				int num3 = IComponent<GameObject>.PowerLoadBonus(num, 100, 10);
				if (num3 != 0)
				{
					num2 = num2 * (100 + num3) / 100;
				}
				int num4 = (int)((float)E.Damage.Amount * ((float)num2 / 100f));
				if (num2 > 0 && num4 == 0)
				{
					num4 = 1;
				}
				if (num4 > 0)
				{
					bool flag = true;
					bool flag2 = false;
					bool flag3 = false;
					bool flag4 = false;
					bool flag5 = false;
					bool flag6 = false;
					bool flag7 = false;
					bool flag8 = false;
					bool flag9 = false;
					bool flag10 = false;
					int num5 = 1;
					int? num6 = new int?(num);
					if (base.IsReady(flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, num5, null, false, 0L, num6))
					{
						if (this.ParentObject.IsPlayer())
						{
							IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
							{
								"You reflect ",
								num4.ToString(),
								" damage back at ",
								E.Actor.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
								"."
							}), 'G', true);
						}
						else if (E.Actor.IsPlayer())
						{
							IComponent<GameObject>.AddPlayerMessage(this.ParentObject.Does("reflect", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " " + num4.ToString() + " damage back at you.", 'R', true);
						}
						else if (base.Visible())
						{
							IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
							{
								this.ParentObject.Does("reflect", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
								" ",
								num4.ToString(),
								" damage back at ",
								E.Actor.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
								"."
							}), null, true);
						}
						List<string> list = new List<string>(E.Damage.Attributes);
						if (!list.Contains("reflected"))
						{
							list.Add("reflected");
						}
						E.Actor.TakeDamage(num4, "from %t damage reflection!", string.Join(" ", list.ToArray()), null, null, null, E.Object, null, null, null, false, false, false, false, false, false, false, false, false, 5, null);
						E.Object.FireEvent("ReflectedDamage");
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x040049FC RID: 18940
		public int ReflectPercentage = 100;
	}
}
