using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.World.Anatomy;
using XRL.World.Capabilities;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x020010A1 RID: 4257
	[Serializable]
	public class Stopsvaalinn : IPoweredPart
	{
		// Token: 0x0600A955 RID: 43349 RVA: 0x003DE348 File Offset: 0x003DC548
		public Stopsvaalinn()
		{
			this.ChargeUse = 500;
			this.IsRealityDistortionBased = true;
			this.WorksOnWearer = true;
			this.WorksOnEquipper = true;
			this.NameForStatus = "ForceEmitter";
		}

		// Token: 0x0600A956 RID: 43350 RVA: 0x003DE3B7 File Offset: 0x003DC5B7
		private void ApplyRobotStop(GameObject Subject = null)
		{
			if (this.RobotStopApplied)
			{
				return;
			}
			if (Subject == null)
			{
				Subject = this.GetActivePartFirstSubject();
			}
			if (Subject != null)
			{
				Subject.SetStringProperty("RobotStop", "true", false);
				this.RobotStopApplied = true;
			}
		}

		// Token: 0x0600A957 RID: 43351 RVA: 0x003DE3E8 File Offset: 0x003DC5E8
		private void UnapplyRobotStop(GameObject Subject = null)
		{
			if (!this.RobotStopApplied)
			{
				return;
			}
			if (Subject == null)
			{
				Subject = this.GetActivePartFirstSubject();
			}
			if (Subject != null)
			{
				Subject.RemoveStringProperty("RobotStop");
				this.RobotStopApplied = false;
			}
		}

		// Token: 0x0600A958 RID: 43352 RVA: 0x003DE414 File Offset: 0x003DC614
		public void Validate()
		{
			Stopsvaalinn.toRemove.Clear();
			foreach (KeyValuePair<string, GameObject> keyValuePair in this.CurrentField)
			{
				if (keyValuePair.Value == null || keyValuePair.Value.IsInvalid())
				{
					Stopsvaalinn.toRemove.Add(keyValuePair.Key);
				}
			}
			if (Stopsvaalinn.toRemove.Count > 0)
			{
				foreach (string text in Stopsvaalinn.toRemove)
				{
					this.CurrentField.Remove(text);
				}
				if (this.CurrentField.Count == 0)
				{
					this.UnapplyRobotStop(null);
				}
			}
		}

		// Token: 0x0600A959 RID: 43353 RVA: 0x003DE4FC File Offset: 0x003DC6FC
		public bool IsActive()
		{
			if (this.CurrentField.Count > 0)
			{
				this.Validate();
				if (this.CurrentField.Count > 0)
				{
					return true;
				}
			}
			this.UnapplyRobotStop(null);
			return false;
		}

		// Token: 0x0600A95A RID: 43354 RVA: 0x003DE52C File Offset: 0x003DC72C
		public bool IsSuspended()
		{
			if (this.CurrentField.Count <= 0)
			{
				return false;
			}
			this.Validate();
			if (this.CurrentField.Count <= 0)
			{
				return false;
			}
			foreach (KeyValuePair<string, GameObject> keyValuePair in this.CurrentField)
			{
				if (keyValuePair.Value.CurrentCell != null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600A95B RID: 43355 RVA: 0x003DE5B4 File Offset: 0x003DC7B4
		public bool IsAnySuspended()
		{
			if (this.CurrentField.Count <= 0)
			{
				return false;
			}
			this.Validate();
			if (this.CurrentField.Count <= 0)
			{
				return false;
			}
			using (Dictionary<string, GameObject>.ValueCollection.Enumerator enumerator = this.CurrentField.Values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.CurrentCell == null)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600A95C RID: 43356 RVA: 0x003DE638 File Offset: 0x003DC838
		public void DestroyBubble()
		{
			this.Validate();
			foreach (GameObject gameObject in this.CurrentField.Values)
			{
				gameObject.Obliterate(null, false, null);
			}
			this.CurrentField.Clear();
			this.UnapplyRobotStop(null);
			this.FieldDirection = null;
		}

		// Token: 0x0600A95D RID: 43357 RVA: 0x003DE6B0 File Offset: 0x003DC8B0
		public int CreateBubble(bool Renew = false)
		{
			this.Validate();
			int num = 0;
			GameObject activePartFirstSubject = this.GetActivePartFirstSubject();
			if (activePartFirstSubject == null)
			{
				return num;
			}
			Cell currentCell = activePartFirstSubject.CurrentCell;
			if (currentCell == null)
			{
				return num;
			}
			if (currentCell.ParentZone.IsWorldMap())
			{
				return num;
			}
			if (Renew && this.RenewChance <= 0)
			{
				return num;
			}
			if (this.FieldDirection.IsNullOrEmpty() || (this.CurrentField.Count == 0 && !Renew))
			{
				this.FieldDirection = base.PickDirectionS("Activate Stopsvaalinn", null);
				if (this.FieldDirection.IsNullOrEmpty())
				{
					return num;
				}
				if (this.FieldDirection == ".")
				{
					this.FieldDirection = Directions.GetRandomDirection();
				}
			}
			foreach (KeyValuePair<string, Cell> keyValuePair in currentCell.GetAdjacentDirectionCellMap(this.FieldDirection, false))
			{
				string key = keyValuePair.Key;
				Cell value = keyValuePair.Value;
				if (value != null && value != currentCell)
				{
					if (this.CurrentField.ContainsKey(key))
					{
						GameObject gameObject = this.CurrentField[key];
						if (gameObject.CurrentCell == value)
						{
							continue;
						}
						gameObject.Obliterate(null, false, null);
						this.CurrentField.Remove(key);
					}
					if ((!this.IsRealityDistortionBased || IComponent<GameObject>.CheckRealityDistortionAccessibility(value)) && (!Renew || this.RenewChance.in100()))
					{
						GameObject gameObject2 = GameObject.Create(this.Blueprint);
						Forcefield part = gameObject2.GetPart<Forcefield>();
						part.Creator = activePartFirstSubject;
						part.MovesWithOwner = true;
						part.RejectOwner = false;
						gameObject2.RequirePart<ExistenceSupport>(false).SupportedBy = this.ParentObject;
						Phase.carryOver(activePartFirstSubject, gameObject2);
						AnimatedMaterialForcefield part2 = gameObject2.GetPart<AnimatedMaterialForcefield>();
						part2.Color = "Red";
						gameObject2.SetStringProperty("RobotStop", "true", false);
						value.AddObject(gameObject2, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						if (gameObject2.CurrentCell == value)
						{
							this.CurrentField.Add(key, gameObject2);
							num++;
							foreach (GameObject gameObject3 in value.GetObjectsWithPart("Physics"))
							{
								if (gameObject3 != gameObject2 && gameObject3 != activePartFirstSubject && gameObject3.ConsiderSolidFor(gameObject2, false) && (part == null || !part.CanPass(gameObject3)) && !gameObject3.HasPart<Forcefield>() && !gameObject3.HasPart<HologramMaterial>() && gameObject3.PhaseMatches(gameObject2))
								{
									gameObject3.Physics.Push(key, 5000 + 500 * this.PushLevel, 4, false, true, null);
								}
							}
							using (List<GameObject>.Enumerator enumerator2 = value.GetObjectsWithPart("Combat").GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									GameObject gameObject4 = enumerator2.Current;
									if (gameObject4 != gameObject2 && gameObject4 != activePartFirstSubject && gameObject4.Physics != null && (part == null || !part.CanPass(gameObject4)) && !gameObject4.HasPart<HologramMaterial>() && gameObject4.PhaseMatches(gameObject2))
									{
										gameObject4.Physics.Push(key, 5000 + 500 * this.PushLevel, 4, false, true, null);
									}
								}
								continue;
							}
						}
						gameObject2.Obliterate(null, false, null);
					}
				}
			}
			if (num > 0)
			{
				this.ApplyRobotStop(activePartFirstSubject);
			}
			return num;
		}

		// Token: 0x0600A95E RID: 43358 RVA: 0x003DEA60 File Offset: 0x003DCC60
		public void SuspendBubble()
		{
			this.Validate();
			foreach (KeyValuePair<string, GameObject> keyValuePair in this.CurrentField)
			{
				keyValuePair.Value.RemoveFromContext(null);
			}
		}

		// Token: 0x0600A95F RID: 43359 RVA: 0x003DEAC0 File Offset: 0x003DCCC0
		public void DesuspendBubble(bool Validated = false)
		{
			if (!Validated)
			{
				this.Validate();
			}
			GameObject activePartFirstSubject = this.GetActivePartFirstSubject();
			Cell cell = ((activePartFirstSubject != null) ? activePartFirstSubject.CurrentCell : null);
			if (cell == null || cell.ParentZone == null || cell.ParentZone.IsWorldMap())
			{
				this.DestroyBubble();
				return;
			}
			Stopsvaalinn.toRemove.Clear();
			foreach (KeyValuePair<string, GameObject> keyValuePair in this.CurrentField)
			{
				string key = keyValuePair.Key;
				GameObject value = keyValuePair.Value;
				if (value.CurrentCell == null)
				{
					Cell cellFromDirection = cell.GetCellFromDirection(key, false);
					if (cellFromDirection == null)
					{
						value.Obliterate(null, false, null);
						Stopsvaalinn.toRemove.Add(key);
					}
					else
					{
						cellFromDirection.AddObject(value, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						Forcefield part = value.GetPart<Forcefield>();
						if (value.CurrentCell == cellFromDirection)
						{
							foreach (GameObject gameObject in cellFromDirection.GetObjectsWithPart("Physics"))
							{
								if (gameObject != value && gameObject.Physics.Solid && (part == null || !part.CanPass(gameObject)) && !gameObject.HasPart<Forcefield>() && !gameObject.HasPart<HologramMaterial>() && gameObject.PhaseMatches(value))
								{
									gameObject.Physics.Push(key, 5000 + 500 * this.PushLevel, 4, false, true, null);
								}
							}
							using (List<GameObject>.Enumerator enumerator2 = cellFromDirection.GetObjectsWithPart("Combat").GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									GameObject gameObject2 = enumerator2.Current;
									if (gameObject2 != value && gameObject2.Physics != null && (part == null || !part.CanPass(gameObject2)) && !gameObject2.HasPart<HologramMaterial>() && gameObject2.PhaseMatches(value))
									{
										gameObject2.Physics.Push(key, 5000 + 500 * this.PushLevel, 4, false, true, null);
									}
								}
								continue;
							}
						}
						value.Obliterate(null, false, null);
						Stopsvaalinn.toRemove.Add(key);
					}
				}
			}
			foreach (string text in Stopsvaalinn.toRemove)
			{
				this.CurrentField.Remove(text);
			}
		}

		// Token: 0x0600A960 RID: 43360 RVA: 0x003DEDA4 File Offset: 0x003DCFA4
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			Writer.Write(this.CurrentField.Count);
			foreach (KeyValuePair<string, GameObject> keyValuePair in this.CurrentField)
			{
				Writer.Write(keyValuePair.Key);
				Writer.WriteGameObject(keyValuePair.Value);
			}
			base.Write(Basis, Writer);
		}

		// Token: 0x0600A961 RID: 43361 RVA: 0x003DEE24 File Offset: 0x003DD024
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			this.CurrentField.Clear();
			int num = Reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				string text = Reader.ReadString();
				GameObject gameObject = Reader.ReadGameObject("stopsvalinn");
				this.CurrentField.Add(text, gameObject);
			}
			base.Read(Basis, Reader);
		}

		// Token: 0x0600A962 RID: 43362 RVA: 0x003DEE77 File Offset: 0x003DD077
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600A963 RID: 43363 RVA: 0x003DEE7A File Offset: 0x003DD07A
		public void CollectStats(Templates.StatCollector stats)
		{
			stats.Set("Duration", "3", false, 0);
		}

		// Token: 0x0600A964 RID: 43364 RVA: 0x003DEE90 File Offset: 0x003DD090
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetDefensiveItemListEvent.ID || ID == PooledEvent<CheckExistenceSupportEvent>.ID || ID == PooledEvent<CommandEvent>.ID || ID == EffectAppliedEvent.ID || ID == SingletonEvent<EndTurnEvent>.ID || ID == EquippedEvent.ID || ID == PooledEvent<ExamineSuccessEvent>.ID || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID || ID == OnDestroyObjectEvent.ID || ID == UnequippedEvent.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x0600A965 RID: 43365 RVA: 0x003DEF09 File Offset: 0x003DD109
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), this.ParentObject.Equipped);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A966 RID: 43366 RVA: 0x003DEF36 File Offset: 0x003DD136
		public override bool HandleEvent(ExamineSuccessEvent E)
		{
			if (E.Object == this.ParentObject)
			{
				this.CheckApplyPowers(this.ParentObject.EquippedOn());
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A967 RID: 43367 RVA: 0x003DEF60 File Offset: 0x003DD160
		public override bool HandleEvent(AIGetDefensiveItemListEvent E)
		{
			if (!this.IsActive() && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) && this.ForceFieldAdvisable() && E.Actor == this.ParentObject.Equipped && E.Actor.IsActivatedAbilityAIUsable(this.ActivatedAbilityID))
			{
				E.Add("ActivateStopsvalinn", 1, this.ParentObject, true, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A968 RID: 43368 RVA: 0x003DEFE8 File Offset: 0x003DD1E8
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == "ActivateStopsvalinn" && E.Actor == this.ParentObject.Equipped && this.ActivateStopsvalinn(E))
			{
				E.RequestInterfaceExit();
				GameObject actor = E.Actor;
				if (actor != null)
				{
					actor.UseEnergy(1000, "Item Stopsvalinn", null, null, false);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A969 RID: 43369 RVA: 0x003DF058 File Offset: 0x003DD258
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			if (base.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				this.DestroyBubble();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A96A RID: 43370 RVA: 0x003DF09A File Offset: 0x003DD29A
		public override bool HandleEvent(CheckExistenceSupportEvent E)
		{
			return !this.CurrentField.ContainsValue(E.Object) && base.HandleEvent(E);
		}

		// Token: 0x0600A96B RID: 43371 RVA: 0x003DF0B8 File Offset: 0x003DD2B8
		public override bool HandleEvent(EndTurnEvent E)
		{
			if (this.IsActive())
			{
				if (!base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
				{
					this.DestroyBubble();
				}
				else if (this.RenewChance > 0 && this.CurrentField.Count < 8)
				{
					this.CreateBubble(true);
				}
				else
				{
					this.MaintainBubble();
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A96C RID: 43372 RVA: 0x003DF12B File Offset: 0x003DD32B
		public override bool HandleEvent(EquippedEvent E)
		{
			this.CheckApplyPowers(E.Part);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A96D RID: 43373 RVA: 0x003DF140 File Offset: 0x003DD340
		public override bool HandleEvent(UnequippedEvent E)
		{
			this.UnapplyPowers(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A96E RID: 43374 RVA: 0x003DF158 File Offset: 0x003DD358
		private void CheckApplyPowers(BodyPart Limb)
		{
			if (this.ActivatedAbilityID == Guid.Empty && Limb != null && this.ParentObject.IsEquippedProperly(Limb))
			{
				GameObject parentObject = Limb.ParentBody.ParentObject;
				if (parentObject != null && (!parentObject.IsPlayer() || this.ParentObject.Understood()))
				{
					this.ApplyPowers(parentObject);
				}
			}
		}

		// Token: 0x0600A96F RID: 43375 RVA: 0x003DF1B4 File Offset: 0x003DD3B4
		private void ApplyPowers(GameObject who)
		{
			base.StatShifter.SetStatShift(who, "Ego", 1, false);
			who.RegisterPartEvent(this, "BeginMove");
			who.RegisterPartEvent(this, "EnteredCell");
			who.RegisterPartEvent(this, "MoveFailed");
			this.ActivatedAbilityID = who.AddActivatedAbility("Activate Stopsvalinn", "ActivateStopsvalinn", "Items", null, "é", null, false, false, false, false, this.IsRealityDistortionBased, false, false, false, true, true, false, false, -1, null, null, null, null, null);
		}

		// Token: 0x0600A970 RID: 43376 RVA: 0x003DF232 File Offset: 0x003DD432
		private void CheckUnapplyPowers(GameObject who = null)
		{
			if (this.ActivatedAbilityID != Guid.Empty)
			{
				this.UnapplyPowers(who);
			}
		}

		// Token: 0x0600A971 RID: 43377 RVA: 0x003DF250 File Offset: 0x003DD450
		private void UnapplyPowers(GameObject who)
		{
			base.StatShifter.RemoveStatShifts(who);
			who.UnregisterPartEvent(this, "BeginMove");
			who.UnregisterPartEvent(this, "EnteredCell");
			who.UnregisterPartEvent(this, "MoveFailed");
			who.RemoveActivatedAbility(ref this.ActivatedAbilityID);
			this.DestroyBubble();
		}

		// Token: 0x0600A972 RID: 43378 RVA: 0x003DF2A0 File Offset: 0x003DD4A0
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (this.ParentObject.Understood() && this.IsObjectActivePartSubject(E.Actor))
			{
				if (this.IsActive())
				{
					E.AddAction("Deactivate", "deactivate", "ActivateStopsvalinn", null, 'a', false, 0, 0, false, false, false, false, true, null, false);
				}
				else
				{
					E.AddAction("Activate", "activate", "ActivateStopsvalinn", null, 'a', false, 0, 0, false, false, false, false, true, null, false);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A973 RID: 43379 RVA: 0x003DF320 File Offset: 0x003DD520
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "ActivateStopsvalinn" && this.ActivateStopsvalinn(E))
			{
				E.RequestInterfaceExit();
				E.Actor.UseEnergy(1000, "Item Stopsvalinn", null, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A974 RID: 43380 RVA: 0x003DF376 File Offset: 0x003DD576
		public override bool HandleEvent(OnDestroyObjectEvent E)
		{
			this.DestroyBubble();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A975 RID: 43381 RVA: 0x003DF385 File Offset: 0x003DD585
		public override bool AllowStaticRegistration()
		{
			return false;
		}

		// Token: 0x0600A976 RID: 43382 RVA: 0x003DF388 File Offset: 0x003DD588
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeginMove")
			{
				this.SuspendBubble();
			}
			else if (E.ID == "EnteredCell" || E.ID == "MoveFailed")
			{
				this.DesuspendBubble(false);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600A977 RID: 43383 RVA: 0x003DF3E4 File Offset: 0x003DD5E4
		private bool ForceFieldAdvisable()
		{
			GameObject activePartFirstSubject = this.GetActivePartFirstSubject();
			return activePartFirstSubject != null && IComponent<GameObject>.CheckRealityDistortionAdvisability(activePartFirstSubject, null, activePartFirstSubject, this.ParentObject, null, null, 0);
		}

		// Token: 0x0600A978 RID: 43384 RVA: 0x003DF418 File Offset: 0x003DD618
		public bool ActivateStopsvalinn(IEvent E = null)
		{
			GameObject activePartFirstSubject = this.GetActivePartFirstSubject();
			if (activePartFirstSubject == null)
			{
				return false;
			}
			Cell currentCell = activePartFirstSubject.CurrentCell;
			if (currentCell == null)
			{
				return false;
			}
			if (!currentCell.BroadcastEvent(Event.New("InitiateForceBubble", "Object", activePartFirstSubject, "Device", this.ParentObject), E))
			{
				return false;
			}
			if (!currentCell.FireEvent(Event.New("InitiateRealityDistortionLocal", "Object", activePartFirstSubject, "Device", this.ParentObject), E))
			{
				return false;
			}
			if (this.IsActive())
			{
				if (this.IsSuspended())
				{
					activePartFirstSubject.Fail(this.ParentObject.Does("vibrate", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " slightly.");
				}
				else if (activePartFirstSubject.IsPlayer())
				{
					IComponent<GameObject>.AddPlayerMessage("The {{R|force bubble}} snaps off.", null, true);
				}
				else if (IComponent<GameObject>.Visible(activePartFirstSubject))
				{
					IComponent<GameObject>.AddPlayerMessage("The {{R|force bubble}} in front of " + activePartFirstSubject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " snaps off.", null, true);
				}
				this.DestroyBubble();
				return true;
			}
			ActivePartStatus activePartStatus = base.GetActivePartStatus(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null);
			if (activePartStatus == ActivePartStatus.Unpowered)
			{
				activePartFirstSubject.Fail(this.ParentObject.Does("don't", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " have enough charge to sustain the field!");
			}
			else
			{
				if (activePartStatus == ActivePartStatus.Operational && this.CreateBubble(false) > 0)
				{
					activePartFirstSubject.PlayWorldSound("Sounds/Interact/sfx_interact_legendaryForcefieldShield_activate", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
					if (activePartFirstSubject.IsPlayer())
					{
						IComponent<GameObject>.AddPlayerMessage("A {{R|force bubble}} pops into being in front of you!", null, true);
					}
					else if (IComponent<GameObject>.Visible(activePartFirstSubject))
					{
						IComponent<GameObject>.AddPlayerMessage("A {{R|force bubble}} pops into being in front of " + activePartFirstSubject.the + activePartFirstSubject.ShortDisplayName + ".", null, true);
					}
					return true;
				}
				activePartFirstSubject.Fail("Nothing happens.");
			}
			return false;
		}

		// Token: 0x0600A979 RID: 43385 RVA: 0x003DF640 File Offset: 0x003DD840
		public void MaintainBubble()
		{
			GameObject activePartFirstSubject = this.GetActivePartFirstSubject();
			if (activePartFirstSubject == null)
			{
				return;
			}
			Phased phased;
			Omniphase omniphase;
			Phase.syncPrep(activePartFirstSubject, out phased, out omniphase);
			foreach (GameObject gameObject in this.CurrentField.Values)
			{
				Phase.sync(activePartFirstSubject, gameObject, phased, omniphase);
			}
		}

		// Token: 0x040045DC RID: 17884
		public string Blueprint = "Forcefield";

		// Token: 0x040045DD RID: 17885
		public int PushLevel = 5;

		// Token: 0x040045DE RID: 17886
		public int RenewChance = 10;

		// Token: 0x040045DF RID: 17887
		public Guid ActivatedAbilityID = Guid.Empty;

		// Token: 0x040045E0 RID: 17888
		public string FieldDirection;

		// Token: 0x040045E1 RID: 17889
		public bool RobotStopApplied;

		// Token: 0x040045E2 RID: 17890
		[NonSerialized]
		public Dictionary<string, GameObject> CurrentField = new Dictionary<string, GameObject>(3);

		// Token: 0x040045E3 RID: 17891
		[NonSerialized]
		public static List<string> toRemove = new List<string>();
	}
}
