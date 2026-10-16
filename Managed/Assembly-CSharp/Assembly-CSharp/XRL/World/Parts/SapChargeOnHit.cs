using System;
using System.Collections.Generic;
using System.Text;
using ConsoleLib.Console;
using XRL.Language;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x020010F3 RID: 4339
	[Serializable]
	public class SapChargeOnHit : IActivePart
	{
		// Token: 0x0600ABD6 RID: 43990 RVA: 0x003EF348 File Offset: 0x003ED548
		public SapChargeOnHit()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600ABD7 RID: 43991 RVA: 0x003EF3C4 File Offset: 0x003ED5C4
		public override bool SameAs(IPart p)
		{
			SapChargeOnHit sapChargeOnHit = p as SapChargeOnHit;
			return sapChargeOnHit.Chance == this.Chance && sapChargeOnHit.ChanceEach == this.ChanceEach && !(sapChargeOnHit.SaveStat != this.SaveStat) && !(sapChargeOnHit.SaveDifficultyStat != this.SaveDifficultyStat) && sapChargeOnHit.SaveTarget == this.SaveTarget && !(sapChargeOnHit.SaveVs != this.SaveVs) && !(sapChargeOnHit.Amount != this.Amount) && !(sapChargeOnHit.RequireDamageAttribute != this.RequireDamageAttribute) && !(sapChargeOnHit.LossAmount != this.LossAmount) && !(sapChargeOnHit.LossFactor != this.LossFactor) && sapChargeOnHit.RequireAvailableStorage == this.RequireAvailableStorage && sapChargeOnHit.TransientStorageSatisfies == this.TransientStorageSatisfies && sapChargeOnHit.Single == this.Single && sapChargeOnHit.LiveChargeOnly == this.LiveChargeOnly && !(sapChargeOnHit.ChargeStoredMessage != this.ChargeStoredMessage) && !(sapChargeOnHit.ChargeNotStoredMessage != this.ChargeNotStoredMessage) && sapChargeOnHit.DrawZapOnMessage == this.DrawZapOnMessage && sapChargeOnHit.ForceCharge == this.ForceCharge && !(sapChargeOnHit.BehaviorDescription != this.BehaviorDescription) && base.SameAs(p);
		}

		// Token: 0x0600ABD8 RID: 43992 RVA: 0x003EF541 File Offset: 0x003ED741
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (ID == GetShortDescriptionEvent.ID && !string.IsNullOrEmpty(this.BehaviorDescription));
		}

		// Token: 0x0600ABD9 RID: 43993 RVA: 0x003EF567 File Offset: 0x003ED767
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (!string.IsNullOrEmpty(this.BehaviorDescription))
			{
				E.Postfix.AppendRules(this.BehaviorDescription);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600ABDA RID: 43994 RVA: 0x003EF58F File Offset: 0x003ED78F
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600ABDB RID: 43995 RVA: 0x003EF592 File Offset: 0x003ED792
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AttackerHit");
			Registrar.Register("WeaponHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600ABDC RID: 43996 RVA: 0x003EF5B2 File Offset: 0x003ED7B2
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AttackerHit" || E.ID == "WeaponHit")
			{
				this.CheckApply(E);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600ABDD RID: 43997 RVA: 0x003EF5E8 File Offset: 0x003ED7E8
		public GameObject SapCharge(GameObject obj, ref int ChargeStored, int useChanceEach)
		{
			if (!useChanceEach.in100())
			{
				return null;
			}
			int num = obj.QueryCharge(this.LiveChargeOnly, 0L, true, true);
			if (num <= 0)
			{
				return null;
			}
			int num2 = this.Amount.RollCached();
			if (this.RequireAvailableStorage)
			{
				int num3 = this.ParentObject.QueryChargeStorage(this.TransientStorageSatisfies, true, null);
				if (num3 < num2)
				{
					num2 = num3;
					if (num2 <= 0)
					{
						return null;
					}
				}
			}
			if (num2 > num)
			{
				num2 = num;
			}
			obj.SplitFromStack();
			if (!obj.UseCharge(num2, this.LiveChargeOnly, 0L, true, true, null))
			{
				return null;
			}
			if (!string.IsNullOrEmpty(this.LossFactor))
			{
				num2 /= this.LossFactor.RollCached();
			}
			if (!string.IsNullOrEmpty(this.LossAmount))
			{
				num2 -= this.LossAmount.RollCached();
			}
			if (num2 > 0)
			{
				ChargeStored += this.ParentObject.ChargeAvailable(num2, 0L, 1, this.ForceCharge, null);
			}
			return obj;
		}

		// Token: 0x0600ABDE RID: 43998 RVA: 0x003EF6CC File Offset: 0x003ED8CC
		public void PerformSapCharge(GameObject obj, ref int ChargeStored, int useChanceEach, List<GameObject> Affected = null, bool Single = false)
		{
			int count = obj.Count;
			if (count > 1)
			{
				for (int i = 0; i < count; i++)
				{
					GameObject gameObject = this.SapCharge(obj, ref ChargeStored, useChanceEach);
					if (Affected != null && gameObject != null && gameObject.IsValid())
					{
						Affected.Add(gameObject);
					}
					if (obj.IsInvalid() || obj == gameObject || (Single && gameObject != null))
					{
						return;
					}
				}
				return;
			}
			GameObject gameObject2 = this.SapCharge(obj, ref ChargeStored, useChanceEach);
			if (Affected != null && gameObject2 != null)
			{
				Affected.Add(gameObject2);
			}
		}

		// Token: 0x0600ABDF RID: 43999 RVA: 0x003EF740 File Offset: 0x003ED940
		public bool CheckApply(Event E)
		{
			if (!string.IsNullOrEmpty(this.RequireDamageAttribute))
			{
				Damage damage = E.GetParameter("Damage") as Damage;
				if (damage == null || !damage.HasAttribute(this.RequireDamageAttribute))
				{
					return false;
				}
			}
			if (base.IsDisabled(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				return false;
			}
			GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
			GameObject gameObjectParameter2 = E.GetGameObjectParameter("Defender");
			GameObject gameObjectParameter3 = E.GetGameObjectParameter("Projectile");
			GameObject gameObject = gameObjectParameter;
			GameObject parentObject = this.ParentObject;
			string text = "Part SapChargeOnHit Activation Main";
			GameObject gameObject2 = gameObjectParameter2;
			GameObject gameObject3 = gameObjectParameter3;
			if (!GetSpecialEffectChanceEvent.GetFor(gameObject, parentObject, text, this.Chance, gameObject2, gameObject3, true, false).in100())
			{
				return false;
			}
			if (!string.IsNullOrEmpty(this.SaveStat) && gameObjectParameter2.MakeSave(this.SaveStat, this.SaveTarget, this.ParentObject.Equipped ?? this.ParentObject, this.SaveDifficultyStat, this.SaveVs, false, false, false, false, this.ParentObject))
			{
				return false;
			}
			GameObject gameObject4 = gameObjectParameter;
			GameObject parentObject2 = this.ParentObject;
			string text2 = "SapChargeOnHitActivationEach";
			gameObject3 = gameObjectParameter2;
			gameObject2 = gameObjectParameter3;
			int @for = GetSpecialEffectChanceEvent.GetFor(gameObject4, parentObject2, text2, this.ChanceEach, gameObject3, gameObject2, true, false);
			bool flag = (!string.IsNullOrEmpty(this.ChargeStoredMessage) || !string.IsNullOrEmpty(this.ChargeNotStoredMessage)) && !E.IsSilent() && (gameObjectParameter.IsPlayer() || gameObjectParameter2.IsPlayer() || IComponent<GameObject>.Visible(gameObjectParameter2));
			List<GameObject> list = Event.NewGameObjectList();
			list.Add(gameObjectParameter2);
			Inventory inventory = gameObjectParameter2.Inventory;
			if (inventory != null)
			{
				list.AddRange(inventory.Objects);
			}
			Body body = gameObjectParameter2.Body;
			if (body != null)
			{
				body.GetEquippedObjects(list);
				body.GetInstalledCybernetics(list);
			}
			if (this.Single)
			{
				list.ShuffleInPlace<GameObject>(null);
			}
			int num = 0;
			List<GameObject> list2 = Event.NewGameObjectList();
			int i = 0;
			int num2 = list.Count;
			while (i < num2)
			{
				GameObject gameObject5 = list[i];
				this.PerformSapCharge(gameObject5, ref num, @for, list2, this.Single);
				if (this.Single && list2.Count > 0)
				{
					break;
				}
				if (num2 != list.Count)
				{
					num2 = list.Count;
					if (i < num2 && list[i] != gameObject5)
					{
						i--;
					}
				}
				i++;
			}
			if (flag && list2.Count > 0)
			{
				bool flag2 = list2.Contains(gameObjectParameter2);
				if (flag2)
				{
					list2.Remove(gameObjectParameter2);
				}
				string text3 = ((num > 0) ? this.ChargeStoredMessage : this.ChargeNotStoredMessage);
				if (text3 == "")
				{
					text3 = null;
				}
				if (text3 != null && (text3.Contains("%o") || text3.Contains("%O")))
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(list2.Count);
					Dictionary<string, GameObject> dictionary2 = new Dictionary<string, GameObject>(list2.Count);
					foreach (GameObject gameObject6 in list2)
					{
						if (gameObject6.IsValid())
						{
							string shortDisplayName = gameObject6.ShortDisplayName;
							if (dictionary.ContainsKey(shortDisplayName))
							{
								Dictionary<string, int> dictionary3 = dictionary;
								string text4 = shortDisplayName;
								dictionary3[text4] += gameObject6.Count;
							}
							else
							{
								dictionary.Add(shortDisplayName, gameObject6.Count);
							}
							if (!dictionary2.ContainsKey(shortDisplayName))
							{
								dictionary2.Add(shortDisplayName, gameObject6);
							}
						}
					}
					List<string> list3 = new List<string>(dictionary.Count);
					StringBuilder stringBuilder = Event.NewStringBuilder(null);
					foreach (string text5 in dictionary.Keys)
					{
						int num3 = dictionary[text5];
						stringBuilder.Length = 0;
						if (num3 == 1)
						{
							stringBuilder.Append(dictionary2[text5].a).Append(text5);
						}
						else
						{
							stringBuilder.Append(Grammar.Cardinal(num3)).Append(' ').Append(dictionary2[text5].GetPluralName(false, false, false, false));
						}
						list3.Add(stringBuilder.ToString());
					}
					if (flag2 || list3.Count > 0)
					{
						StringBuilder stringBuilder2 = Event.NewStringBuilder(null);
						if (flag2)
						{
							stringBuilder2.Append(gameObjectParameter2.IsPlayer() ? "you" : gameObjectParameter2.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false));
							if (list3.Count > 0)
							{
								stringBuilder2.Append(" and ").Append(Grammar.MakeAndList(list3, true)).Append(" on ")
									.Append(gameObjectParameter2.them);
							}
						}
						else
						{
							stringBuilder2.Append(Grammar.MakeAndList(list3, true)).Append(" on ").Append(gameObjectParameter2.IsPlayer() ? "your person" : gameObjectParameter2.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false));
						}
						if (text3.Contains("%o"))
						{
							text3 = text3.Replace("%o", stringBuilder2.ToString());
						}
						if (text3.Contains("%O"))
						{
							text3 = text3.Replace("%O", ColorUtility.CapitalizeExceptFormatting(stringBuilder2.ToString()));
						}
					}
					else
					{
						text3 = null;
					}
				}
				if (text3 != null)
				{
					if (text3.Contains("%a"))
					{
						text3 = text3.Replace("%a", gameObjectParameter.IsPlayer() ? "you" : gameObjectParameter.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false));
					}
					if (text3.Contains("%A"))
					{
						text3 = text3.Replace("%A", gameObjectParameter.IsPlayer() ? "You" : gameObjectParameter.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false));
					}
					if (text3.Contains("%d"))
					{
						text3 = text3.Replace("%d", gameObjectParameter2.IsPlayer() ? "you" : gameObjectParameter2.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false));
					}
					if (text3.Contains("%D"))
					{
						text3 = text3.Replace("%D", gameObjectParameter2.IsPlayer() ? "You" : gameObjectParameter2.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false));
					}
					if (text3.Contains("%w") || text3.Contains("%W"))
					{
						if (this.ParentObject.IsPlayer())
						{
							text3 = text3.Replace("%w", "you").Replace("%W", "You");
						}
						else
						{
							GameObject equipped = this.ParentObject.Equipped;
							string text6;
							if (equipped != null && !this.ParentObject.HasProperName)
							{
								text6 = equipped.poss(this.ParentObject, true, null);
							}
							else
							{
								text6 = this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false);
							}
							if (text3.Contains("%w"))
							{
								text3 = text3.Replace("%w", text6);
							}
							if (text3.Contains("%W"))
							{
								text3 = text3.Replace("%W", ColorUtility.CapitalizeExceptFormatting(text6));
							}
						}
					}
					IComponent<GameObject>.AddPlayerMessage(text3, null, true);
					if (this.DrawZapOnMessage)
					{
						this.DrawZap(gameObjectParameter2, this.ParentObject);
					}
				}
			}
			E.SetParameter("DidSpecialEffect", 1);
			return true;
		}

		// Token: 0x0600ABE0 RID: 44000 RVA: 0x003EFF38 File Offset: 0x003EE138
		private void DrawZap(Cell O, Cell D)
		{
			if (O == null || D == null)
			{
				return;
			}
			List<Point> list = Zone.Line(O.X, O.Y, D.X, D.Y, true);
			for (int i = 0; i < list.Count; i++)
			{
				Cell cell = D.ParentZone.GetCell(list[i].X, list[i].Y);
				if (cell != null && cell.IsVisible())
				{
					GameObject firstObject = cell.GetFirstObject();
					if (firstObject != null)
					{
						if (Stat.RandomCosmetic(0, 1) == 0)
						{
							firstObject.ParticleBlip("&W" + ((char)Stat.RandomCosmetic(191, 198)).ToString(), 30, 0L, false);
						}
						else
						{
							firstObject.ParticleBlip("&Y" + ((char)Stat.RandomCosmetic(191, 198)).ToString(), 30, 0L, false);
						}
					}
				}
			}
		}

		// Token: 0x0600ABE1 RID: 44001 RVA: 0x003F0022 File Offset: 0x003EE222
		private void DrawZap(GameObject O, GameObject D)
		{
			this.DrawZap(O.CurrentCell, D.CurrentCell);
		}

		// Token: 0x04004706 RID: 18182
		public int Chance = 100;

		// Token: 0x04004707 RID: 18183
		public int ChanceEach = 100;

		// Token: 0x04004708 RID: 18184
		public string SaveStat;

		// Token: 0x04004709 RID: 18185
		public string SaveDifficultyStat;

		// Token: 0x0400470A RID: 18186
		public int SaveTarget = 15;

		// Token: 0x0400470B RID: 18187
		public string SaveVs = "ChargeDrain";

		// Token: 0x0400470C RID: 18188
		public string Amount = "2d6";

		// Token: 0x0400470D RID: 18189
		public string RequireDamageAttribute;

		// Token: 0x0400470E RID: 18190
		public string LossAmount;

		// Token: 0x0400470F RID: 18191
		public string LossFactor;

		// Token: 0x04004710 RID: 18192
		public bool RequireAvailableStorage;

		// Token: 0x04004711 RID: 18193
		public bool TransientStorageSatisfies = true;

		// Token: 0x04004712 RID: 18194
		public bool Single;

		// Token: 0x04004713 RID: 18195
		public bool LiveChargeOnly = true;

		// Token: 0x04004714 RID: 18196
		public string ChargeStoredMessage = "A {{W|ribbon of electricity}} leaps from %o to %w.";

		// Token: 0x04004715 RID: 18197
		public string ChargeNotStoredMessage = "A {{W|ribbon of electricity}} leaps from %o into the air.";

		// Token: 0x04004716 RID: 18198
		public bool DrawZapOnMessage = true;

		// Token: 0x04004717 RID: 18199
		public bool ForceCharge = true;

		// Token: 0x04004718 RID: 18200
		public string BehaviorDescription;
	}
}
