using System;
using System.Collections.Generic;
using System.Text;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x020011D3 RID: 4563
	[Serializable]
	public class IntegratedPowerSystems : IPoweredPart
	{
		// Token: 0x0600B5A5 RID: 46501 RVA: 0x004249E2 File Offset: 0x00422BE2
		public IntegratedPowerSystems()
		{
			this.ChargeUse = 0;
			this.IsBootSensitive = false;
			this.WorksOnEquipper = true;
		}

		// Token: 0x0600B5A6 RID: 46502 RVA: 0x00424A16 File Offset: 0x00422C16
		public override bool WorksForEveryone()
		{
			return this.RequiresPart.IsNullOrEmpty() && this.RequiresTagOrProperty.IsNullOrEmpty() && this.RequiresEvent.IsNullOrEmpty() && base.WorksForEveryone();
		}

		// Token: 0x0600B5A7 RID: 46503 RVA: 0x00424A4C File Offset: 0x00422C4C
		public override bool WorksFor(GameObject obj)
		{
			return (this.RequiresPart.IsNullOrEmpty() || (obj != null && obj.HasPart(this.RequiresPart))) && (this.RequiresTagOrProperty.IsNullOrEmpty() || (obj != null && obj.HasTagOrProperty(this.RequiresTagOrProperty))) && (this.RequiresEvent.IsNullOrEmpty() || (obj != null && !obj.FireEvent(this.RequiresEvent))) && base.WorksFor(obj);
		}

		// Token: 0x0600B5A8 RID: 46504 RVA: 0x00424AC0 File Offset: 0x00422CC0
		public bool HasCharge(int Amount)
		{
			if (this.ActivePartHasMultipleSubjects())
			{
				using (List<GameObject>.Enumerator enumerator = this.GetActivePartSubjects().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.TestCharge(Amount, false, 0L, true, true))
						{
							return true;
						}
					}
					return false;
				}
			}
			GameObject activePartFirstSubject = this.GetActivePartFirstSubject();
			if (activePartFirstSubject.TestCharge(Amount, false, 0L, true, true))
			{
				return true;
			}
			return false;
		}

		// Token: 0x0600B5A9 RID: 46505 RVA: 0x00424B40 File Offset: 0x00422D40
		public int GetCharge()
		{
			int num = 0;
			if (this.ActivePartHasMultipleSubjects())
			{
				using (List<GameObject>.Enumerator enumerator = this.GetActivePartSubjects().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						GameObject gameObject = enumerator.Current;
						num += gameObject.QueryCharge(false, 0L, true, true);
					}
					return num;
				}
			}
			GameObject activePartFirstSubject = this.GetActivePartFirstSubject();
			num += activePartFirstSubject.QueryCharge(false, 0L, true, true);
			return num;
		}

		// Token: 0x0600B5AA RID: 46506 RVA: 0x00424BBC File Offset: 0x00422DBC
		public void UseCharge(int Amount)
		{
			if (Amount <= 0)
			{
				return;
			}
			if (this.ActivePartHasMultipleSubjects())
			{
				List<GameObject> activePartSubjects = this.GetActivePartSubjects();
				if (activePartSubjects.Count == 0)
				{
					return;
				}
				bool flag = false;
				int i = 0;
				int count = activePartSubjects.Count;
				while (i < count)
				{
					if (UsingChargeEvent.Wanted(activePartSubjects[i]))
					{
						flag = true;
						break;
					}
					i++;
				}
				if (flag)
				{
					UsingChargeEvent usingChargeEvent = UsingChargeEvent.FromPool();
					usingChargeEvent.Source = this.ParentObject;
					usingChargeEvent.Amount = Amount;
					int j = 0;
					int count2 = activePartSubjects.Count;
					while (j < count2)
					{
						UsingChargeEvent.Process(activePartSubjects[j], usingChargeEvent);
						j++;
					}
				}
				bool flag2 = false;
				int k = 0;
				int count3 = activePartSubjects.Count;
				while (k < count3)
				{
					if (UseChargeEvent.Wanted(activePartSubjects[k]))
					{
						flag2 = true;
						break;
					}
					k++;
				}
				int num = 0;
				if (flag2)
				{
					UseChargeEvent useChargeEvent = UseChargeEvent.FromPool();
					useChargeEvent.Source = this.ParentObject;
					useChargeEvent.Amount = Amount;
					int l = 0;
					int count4 = activePartSubjects.Count;
					while (l < count4)
					{
						if (!UseChargeEvent.Process(activePartSubjects[l], useChargeEvent))
						{
							return;
						}
						l++;
					}
					num = Amount - useChargeEvent.Amount;
				}
				bool flag3 = false;
				int m = 0;
				int count5 = activePartSubjects.Count;
				while (m < count5)
				{
					if (ChargeUsedEvent.Wanted(activePartSubjects[m]))
					{
						flag3 = true;
						break;
					}
					m++;
				}
				if (flag3)
				{
					ChargeUsedEvent chargeUsedEvent = ChargeUsedEvent.FromPool();
					chargeUsedEvent.Source = this.ParentObject;
					chargeUsedEvent.DesiredAmount = Amount;
					chargeUsedEvent.Amount = num;
					int n = 0;
					int count6 = activePartSubjects.Count;
					while (n < count6)
					{
						ChargeUsedEvent.Process(activePartSubjects[n], chargeUsedEvent);
						n++;
					}
					return;
				}
			}
			else
			{
				GameObject activePartFirstSubject = this.GetActivePartFirstSubject();
				if (UsingChargeEvent.Wanted(activePartFirstSubject))
				{
					UsingChargeEvent usingChargeEvent2 = UsingChargeEvent.FromPool();
					usingChargeEvent2.Source = this.ParentObject;
					usingChargeEvent2.Amount = Amount;
					UsingChargeEvent.Process(activePartFirstSubject, usingChargeEvent2);
				}
				int num2 = 0;
				if (UseChargeEvent.Wanted(activePartFirstSubject))
				{
					UseChargeEvent useChargeEvent2 = UseChargeEvent.FromPool();
					useChargeEvent2.Source = this.ParentObject;
					useChargeEvent2.Amount = Amount;
					UseChargeEvent.Process(activePartFirstSubject, useChargeEvent2);
					num2 = Amount - useChargeEvent2.Amount;
				}
				if (ChargeUsedEvent.Wanted(activePartFirstSubject))
				{
					ChargeUsedEvent chargeUsedEvent2 = ChargeUsedEvent.FromPool();
					chargeUsedEvent2.Source = this.ParentObject;
					chargeUsedEvent2.DesiredAmount = Amount;
					chargeUsedEvent2.Amount = num2;
					ChargeUsedEvent.Process(activePartFirstSubject, chargeUsedEvent2);
				}
			}
		}

		// Token: 0x0600B5AB RID: 46507 RVA: 0x00424E14 File Offset: 0x00423014
		public void AddCharge(int Amount)
		{
			if (Amount <= 0)
			{
				return;
			}
			if (this.ActivePartHasMultipleSubjects())
			{
				using (List<GameObject>.Enumerator enumerator = this.GetActivePartSubjects().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						GameObject gameObject = enumerator.Current;
						int num = gameObject.ChargeAvailable(Amount, 0L, 1, false, null);
						if (num > 0)
						{
							Amount -= num;
							if (Amount <= 0)
							{
								break;
							}
						}
					}
					return;
				}
			}
			GameObject activePartFirstSubject = this.GetActivePartFirstSubject();
			activePartFirstSubject.ChargeAvailable(Amount, 0L, 1, false, null);
		}

		// Token: 0x0600B5AC RID: 46508 RVA: 0x00424E9C File Offset: 0x0042309C
		public int GetChargeLevel()
		{
			return EnergyStorage.GetChargeLevel(this.GetCharge(), 0);
		}

		// Token: 0x0600B5AD RID: 46509 RVA: 0x00424EAA File Offset: 0x004230AA
		public bool UseAltChargeDisplayStyle()
		{
			return IComponent<GameObject>.ThePlayer != null && !this.AltChargeDisplayProperty.IsNullOrEmpty() && IComponent<GameObject>.ThePlayer.GetIntProperty(this.AltChargeDisplayProperty, 0) > 0;
		}

		// Token: 0x0600B5AE RID: 46510 RVA: 0x00424EDC File Offset: 0x004230DC
		public string ChargeStatus()
		{
			string text = (this.UseAltChargeDisplayStyle() ? this.AltChargeDisplayStyle : this.ChargeDisplayStyle);
			if (text.IsNullOrEmpty())
			{
				return null;
			}
			if (base.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				return null;
			}
			return EnergyStorage.GetChargeStatus(this.GetCharge(), 0, text);
		}

		// Token: 0x0600B5AF RID: 46511 RVA: 0x00424F44 File Offset: 0x00423144
		public override bool SameAs(IPart p)
		{
			IntegratedPowerSystems integratedPowerSystems = p as IntegratedPowerSystems;
			return !(integratedPowerSystems.RequiresPart != this.RequiresPart) && !(integratedPowerSystems.RequiresTagOrProperty != this.RequiresTagOrProperty) && !(integratedPowerSystems.RequiresEvent != this.RequiresEvent) && !(integratedPowerSystems.ChargeDisplayStyle != this.ChargeDisplayStyle) && !(integratedPowerSystems.AltChargeDisplayStyle != this.AltChargeDisplayStyle) && !(integratedPowerSystems.AltChargeDisplayProperty != this.AltChargeDisplayProperty) && !(integratedPowerSystems.Description != this.Description) && base.SameAs(p);
		}

		// Token: 0x0600B5B0 RID: 46512 RVA: 0x00424FF4 File Offset: 0x004231F4
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ChargeAvailableEvent.ID || ID == FinishChargeAvailableEvent.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == QueryChargeEvent.ID || ID == QueryChargeStorageEvent.ID || ID == PooledEvent<QueryDrawEvent>.ID || ID == TestChargeEvent.ID || ID == UseChargeEvent.ID;
		}

		// Token: 0x0600B5B1 RID: 46513 RVA: 0x00425050 File Offset: 0x00423250
		public override bool HandleEvent(ChargeAvailableEvent E)
		{
			if (base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				if (this.ActivePartHasMultipleSubjects())
				{
					List<GameObject> activePartSubjects = this.GetActivePartSubjects();
					int i = 0;
					int count = activePartSubjects.Count;
					while (i < count)
					{
						if (!ChargeAvailableEvent.Process(activePartSubjects[i], E))
						{
							return false;
						}
						i++;
					}
				}
				else
				{
					GameObject activePartFirstSubject = this.GetActivePartFirstSubject();
					if (!ChargeAvailableEvent.Process(activePartFirstSubject, E))
					{
						return false;
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B5B2 RID: 46514 RVA: 0x004250D8 File Offset: 0x004232D8
		public override bool HandleEvent(FinishChargeAvailableEvent E)
		{
			if (base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				if (this.ActivePartHasMultipleSubjects())
				{
					List<GameObject> activePartSubjects = this.GetActivePartSubjects();
					int i = 0;
					int count = activePartSubjects.Count;
					while (i < count)
					{
						if (!FinishChargeAvailableEvent.Process(activePartSubjects[i], E))
						{
							return false;
						}
						i++;
					}
				}
				else
				{
					GameObject activePartFirstSubject = this.GetActivePartFirstSubject();
					if (!FinishChargeAvailableEvent.Process(activePartFirstSubject, E))
					{
						return false;
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B5B3 RID: 46515 RVA: 0x00425160 File Offset: 0x00423360
		public override bool HandleEvent(QueryChargeEvent E)
		{
			if (base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				if (this.ActivePartHasMultipleSubjects())
				{
					List<GameObject> activePartSubjects = this.GetActivePartSubjects();
					int i = 0;
					int count = activePartSubjects.Count;
					while (i < count)
					{
						if (!QueryChargeEvent.Process(activePartSubjects[i], E))
						{
							return false;
						}
						i++;
					}
				}
				else
				{
					GameObject activePartFirstSubject = this.GetActivePartFirstSubject();
					if (!QueryChargeEvent.Process(activePartFirstSubject, E))
					{
						return false;
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B5B4 RID: 46516 RVA: 0x004251E8 File Offset: 0x004233E8
		public override bool HandleEvent(TestChargeEvent E)
		{
			if (base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				if (this.ActivePartHasMultipleSubjects())
				{
					List<GameObject> activePartSubjects = this.GetActivePartSubjects();
					int i = 0;
					int count = activePartSubjects.Count;
					while (i < count)
					{
						if (!TestChargeEvent.Process(activePartSubjects[i], E))
						{
							return false;
						}
						i++;
					}
				}
				else
				{
					GameObject activePartFirstSubject = this.GetActivePartFirstSubject();
					if (!TestChargeEvent.Process(activePartFirstSubject, E))
					{
						return false;
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B5B5 RID: 46517 RVA: 0x00425270 File Offset: 0x00423470
		public override bool HandleEvent(UseChargeEvent E)
		{
			if (base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				if (this.ActivePartHasMultipleSubjects())
				{
					List<GameObject> activePartSubjects = this.GetActivePartSubjects();
					int i = 0;
					int count = activePartSubjects.Count;
					while (i < count)
					{
						if (!UseChargeEvent.Process(activePartSubjects[i], E))
						{
							return false;
						}
						i++;
					}
				}
				else
				{
					GameObject activePartFirstSubject = this.GetActivePartFirstSubject();
					if (!UseChargeEvent.Process(activePartFirstSubject, E))
					{
						return false;
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B5B6 RID: 46518 RVA: 0x004252F8 File Offset: 0x004234F8
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Context != "Tinkering" && E.Understood())
			{
				string text = this.ChargeStatus();
				if (text != null)
				{
					StringBuilder stringBuilder = Event.NewStringBuilder(null);
					stringBuilder.Append("{{y|(").Append(this.Description).Append(": ")
						.Append(text)
						.Append(")}}");
					E.AddTag(stringBuilder.ToString(), 0);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B5B7 RID: 46519 RVA: 0x00425378 File Offset: 0x00423578
		public override bool HandleEvent(QueryDrawEvent E)
		{
			GameObject @object = E.Object;
			if (@object == this.ParentObject.Equipped && this.IsObjectActive(@object))
			{
				try
				{
					E.Object = this.ParentObject;
					this.ParentObject.HandleEvent(E);
				}
				finally
				{
					E.Object = @object;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B5B8 RID: 46520 RVA: 0x004253E0 File Offset: 0x004235E0
		public override bool HandleEvent(QueryChargeStorageEvent E)
		{
			if (base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) && this.AnyActivePartSubjectWantsEvent(E.GetID(), E.GetCascadeLevel()))
			{
				this.ActivePartSubjectsHandleEvent(E);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B5B9 RID: 46521 RVA: 0x00425438 File Offset: 0x00423638
		private bool IsObjectActive(GameObject obj)
		{
			BootSequence bootSequence;
			PowerSwitch powerSwitch;
			ArtificialIntelligence artificialIntelligence;
			return obj != null && (!obj.TryGetPart<BootSequence>(out bootSequence) || bootSequence.BootTimeLeft <= 0) && (!obj.TryGetPart<PowerSwitch>(out powerSwitch) || powerSwitch.Active) && (!obj.TryGetPart<ArtificialIntelligence>(out artificialIntelligence) || artificialIntelligence.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null));
		}

		// Token: 0x04004949 RID: 18761
		public string RequiresPart;

		// Token: 0x0400494A RID: 18762
		public string RequiresTagOrProperty;

		// Token: 0x0400494B RID: 18763
		public string RequiresEvent;

		// Token: 0x0400494C RID: 18764
		public string ChargeDisplayStyle;

		// Token: 0x0400494D RID: 18765
		public string AltChargeDisplayStyle;

		// Token: 0x0400494E RID: 18766
		public string AltChargeDisplayProperty = Scanning.GetScanPropertyName(Scanning.Scan.Tech);

		// Token: 0x0400494F RID: 18767
		public string Description = "integrated power systems";
	}
}
