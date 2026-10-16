using System;
using XRL.Collections;
using XRL.UI;
using XRL.World.Tinkering;

namespace XRL.World.Parts.Skill
{
	// Token: 0x020013BF RID: 5055
	[Serializable]
	public class Tinkering_Tinker1 : BaseSkill
	{
		// Token: 0x0600C31B RID: 49947 RVA: 0x00473DE7 File Offset: 0x00471FE7
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C31C RID: 49948 RVA: 0x00473DFD File Offset: 0x00471FFD
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("scholarship", 2);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C31D RID: 49949 RVA: 0x00473E20 File Offset: 0x00472020
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CommandRechargeObject");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C31E RID: 49950 RVA: 0x00473E38 File Offset: 0x00472038
		public bool Recharge(GameObject Object, IEvent FromEvent = null)
		{
			if (!this.ParentObject.CanMoveExtremities("Recharge", true, false, false))
			{
				return false;
			}
			bool AnyParts = false;
			bool AnyRechargeable = false;
			bool AnyRecharged = false;
			Object.ForeachPartDescendedFrom<IRechargeable>(delegate(IRechargeable rcg)
			{
				AnyParts = true;
				if (!rcg.CanBeRecharged())
				{
					return true;
				}
				AnyRechargeable = true;
				int rechargeAmount = rcg.GetRechargeAmount();
				if (rechargeAmount <= 0)
				{
					return true;
				}
				char rechargeBit = rcg.GetRechargeBit();
				int rechargeValue = rcg.GetRechargeValue();
				string @string = rechargeBit.GetString();
				int num = rechargeAmount / rechargeValue;
				if (num < 1)
				{
					num = 1;
				}
				int bitCount = BitLocker.GetBitCount(this.ParentObject, rechargeBit);
				if (bitCount == 0)
				{
					return this.ParentObject.Fail("You don't have any " + BitType.GetString(@string) + " bits, which are required for recharging.");
				}
				int num2 = Math.Min(bitCount, num);
				int valueOrDefault = Popup.AskNumber(string.Concat(new string[]
				{
					"It would take {{C|",
					num.ToString(),
					"}} ",
					BitType.GetString(@string),
					" ",
					(num == 1) ? "bit" : "bits",
					" to fully recharge ",
					Object.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
					". You have {{C|",
					bitCount.ToString(),
					"}}. How many do you want to use?"
				}), "Sounds/UI/ui_notification", "", num2, 0, num2).GetValueOrDefault();
				if (valueOrDefault > 0)
				{
					BitLocker.UseBits(this.ParentObject, rechargeBit, valueOrDefault);
					Object.SplitFromStack();
					rcg.AddCharge((valueOrDefault < num) ? (valueOrDefault * rechargeValue) : rechargeAmount);
					IComponent<GameObject>.PlayUISound("Sounds/Abilities/sfx_ability_energyCell_recharge", 1f, false, SoundRequest.SoundEffectType.None);
					Popup.Show(string.Concat(new string[]
					{
						"You have ",
						(valueOrDefault < num) ? "partially " : "",
						"recharged ",
						Object.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
						"."
					}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
					Object.CheckStack();
					AnyRecharged = true;
				}
				return true;
			});
			if (!AnyParts)
			{
				Popup.ShowFail("That isn't an energy cell and does not have a rechargeable capacitor.", true, true, true);
			}
			else if (!AnyRechargeable)
			{
				Popup.ShowFail(Object.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " can't be recharged that way.", true, true, true);
			}
			if (AnyRecharged)
			{
				this.ParentObject.UseEnergy(1000, "Skill Tinkering Recharge", null, null, false);
				if (FromEvent != null)
				{
					FromEvent.RequestInterfaceExit();
				}
			}
			return AnyRecharged;
		}

		// Token: 0x0600C31F RID: 49951 RVA: 0x00473F30 File Offset: 0x00472130
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CommandRechargeObject")
			{
				if (!this.ParentObject.CanMoveExtremities("Recharge", true, false, false))
				{
					return false;
				}
				using (ScopeDisposedList<GameObject> fromPool = ScopeDisposedList<GameObject>.GetFromPool())
				{
					using (ScopeDisposedList<GameObject> fromPool2 = ScopeDisposedList<GameObject>.GetFromPool())
					{
						this.ParentObject.GetContents(fromPool2);
						foreach (GameObject gameObject in fromPool2)
						{
							if (gameObject.Understood() && gameObject.NeedsRecharge())
							{
								fromPool.Add(gameObject);
							}
						}
						if (fromPool.Count == 0)
						{
							if (this.ParentObject.IsPlayer())
							{
								Popup.ShowFail("You have no items that require charging.", true, true, true);
							}
						}
						else
						{
							GameObject gameObject2 = Popup.PickGameObject("Select an item to charge.", fromPool, true, true, true, null, 0, false);
							if (gameObject2 != null)
							{
								this.Recharge(gameObject2, E);
							}
						}
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C320 RID: 49952 RVA: 0x00474050 File Offset: 0x00472250
		public override bool AddSkill(GameObject GO)
		{
			if (GO.GetIntProperty("ReceivedTinker1Recipe", 0) <= 0 && (GO.CurrentCell != null || GO.IsOriginalPlayerBody()))
			{
				Tinkering.LearnNewRecipe(GO, 0, 3);
				GO.SetIntProperty("ReceivedTinker1Recipe", 1, false);
			}
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Recharge", "CommandRechargeObject", "Skills", null, "\u009b", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			if (GO.IsPlayer())
			{
				TinkeringSifrah.AwardInsight();
			}
			return base.AddSkill(GO);
		}

		// Token: 0x0600C321 RID: 49953 RVA: 0x004740DA File Offset: 0x004722DA
		public override bool RemoveSkill(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.RemoveSkill(GO);
		}

		// Token: 0x04004DAE RID: 19886
		public Guid ActivatedAbilityID = Guid.Empty;
	}
}
