using System;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02001050 RID: 4176
	[Serializable]
	public abstract class ITeleporter : IPoweredPart
	{
		// Token: 0x0600A648 RID: 42568 RVA: 0x003CD698 File Offset: 0x003CB898
		public ITeleporter()
		{
			this.ChargeUse = 0;
			this.WorksOnCarrier = true;
			this.WorksOnHolder = true;
		}

		// Token: 0x0600A649 RID: 42569 RVA: 0x003CD6D0 File Offset: 0x003CB8D0
		public override bool SameAs(IPart p)
		{
			ITeleporter teleporter = p as ITeleporter;
			return !(teleporter.DestinationZone != this.DestinationZone) && teleporter.DestinationX == this.DestinationX && teleporter.DestinationY == this.DestinationY && teleporter.UsableInCombat == this.UsableInCombat && teleporter.Intraplanar == this.Intraplanar && teleporter.Interplanar == this.Interplanar && teleporter.Interprotocol == this.Interprotocol && base.SameAs(p);
		}

		// Token: 0x0600A64A RID: 42570 RVA: 0x003CD760 File Offset: 0x003CB960
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (ID == AddedToInventoryEvent.ID && this.WorksOnCarrier) || (ID == EquippedEvent.ID && (this.WorksOnEquipper || this.WorksOnHolder || this.WorksOnWearer)) || ID == PooledEvent<ExamineSuccessEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == PooledEvent<GetRecoilersEvent>.ID || (ID == ImplantedEvent.ID && this.WorksOnImplantee);
		}

		// Token: 0x0600A64B RID: 42571 RVA: 0x003CD7D1 File Offset: 0x003CB9D1
		public override bool HandleEvent(GetRecoilersEvent E)
		{
			if (this.VisibleInRecoilerList())
			{
				E.Objects.Add(this.ParentObject);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A64C RID: 42572 RVA: 0x003CD7F3 File Offset: 0x003CB9F3
		public override bool HandleEvent(ExamineSuccessEvent E)
		{
			if (E.Object == this.ParentObject)
			{
				this.RequireRecoilAbility(E.Actor);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A64D RID: 42573 RVA: 0x003CD817 File Offset: 0x003CBA17
		public override bool HandleEvent(AddedToInventoryEvent E)
		{
			if (E.Item == this.ParentObject)
			{
				this.RequireRecoilAbility(E.Actor);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A64E RID: 42574 RVA: 0x003CD83B File Offset: 0x003CBA3B
		public override bool HandleEvent(EquippedEvent E)
		{
			if (E.Item == this.ParentObject)
			{
				this.RequireRecoilAbility(E.Actor);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A64F RID: 42575 RVA: 0x003CD85F File Offset: 0x003CBA5F
		public override bool HandleEvent(ImplantedEvent E)
		{
			if (E.Item == this.ParentObject)
			{
				this.RequireRecoilAbility(E.Implantee);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A650 RID: 42576 RVA: 0x003CD883 File Offset: 0x003CBA83
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				E.Add("travel", 5);
				if (this.Interprotocol)
				{
					E.Add("circuitry", 5);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A651 RID: 42577 RVA: 0x003CD8BC File Offset: 0x003CBABC
		public int GetTeleportFailure()
		{
			if (The.ZoneManager == null || !The.ZoneManager.IsZoneLive(this.DestinationZone))
			{
				return 0;
			}
			Zone anyBasisZone = this.GetAnyBasisZone();
			WorldBlueprint worldBlueprint = ((anyBasisZone != null) ? anyBasisZone.ResolveWorldBlueprint() : null);
			Zone zone = The.ZoneManager.GetZone(this.DestinationZone);
			WorldBlueprint worldBlueprint2 = ((zone != null) ? zone.ResolveWorldBlueprint() : null);
			if (!this.Intraplanar && worldBlueprint != worldBlueprint2)
			{
				return 1;
			}
			if (!this.Interplanar && ((worldBlueprint != null) ? worldBlueprint.Plane : null) != ((worldBlueprint2 != null) ? worldBlueprint2.Plane : null))
			{
				return 2;
			}
			if (!this.Interprotocol && ((worldBlueprint != null) ? worldBlueprint.Protocol : null) != ((worldBlueprint2 != null) ? worldBlueprint2.Protocol : null))
			{
				return 4;
			}
			return 0;
		}

		// Token: 0x0600A652 RID: 42578 RVA: 0x003CD975 File Offset: 0x003CBB75
		public override bool GetActivePartLocallyDefinedFailure()
		{
			return this.GetTeleportFailure() != 0 || base.GetActivePartLocallyDefinedFailure();
		}

		// Token: 0x0600A653 RID: 42579 RVA: 0x003CD988 File Offset: 0x003CBB88
		public override string GetActivePartLocallyDefinedFailureDescription()
		{
			switch (this.GetTeleportFailure())
			{
			case 1:
				return "RelativityCompensationFailure";
			case 2:
				return "QuantumPhaseMismatch";
			case 4:
				return "ProtocolMismatch";
			}
			return base.GetActivePartLocallyDefinedFailureDescription();
		}

		// Token: 0x0600A654 RID: 42580 RVA: 0x003CD9CE File Offset: 0x003CBBCE
		public virtual string GetCustomTeleportFailure(GameObject Actor)
		{
			return null;
		}

		// Token: 0x0600A655 RID: 42581 RVA: 0x003CD9D1 File Offset: 0x003CBBD1
		public virtual bool VisibleInRecoilerList()
		{
			return this.ParentObject.Understood() && !this.ParentObject.HasPropertyOrTag("ExcludeFromRecoilerList");
		}

		// Token: 0x0600A656 RID: 42582 RVA: 0x003CD9F8 File Offset: 0x003CBBF8
		public bool AttemptTeleport(GameObject Actor, IEvent FromEvent = null)
		{
			if (this.DestinationZone.IsNullOrEmpty())
			{
				return Actor.Fail("Nothing happens.");
			}
			if (!this.UsableInCombat && Actor.AreHostilesNearby())
			{
				if (Actor == this.ParentObject)
				{
					return Actor.Fail("You can't recoil with hostiles nearby!");
				}
				return Actor.Fail("You can't use " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " with hostiles nearby!");
			}
			else
			{
				int num = this.ParentObject.QueryCharge(false, 0L, true, true);
				The.ZoneManager.GetZone(this.DestinationZone);
				ActivePartStatus activePartStatus = base.GetActivePartStatus(true, false, false, false, false, false, false, true, false, false, 1, null, false, 0L, null);
				string customTeleportFailure = this.GetCustomTeleportFailure(Actor);
				if (!customTeleportFailure.IsNullOrEmpty() || activePartStatus != ActivePartStatus.Operational || !this.IsObjectActivePartSubject(Actor))
				{
					if (Actor.IsPlayer())
					{
						if (activePartStatus == ActivePartStatus.LocallyDefinedFailure)
						{
							int teleportFailure = this.GetTeleportFailure();
							if (teleportFailure == 4)
							{
								Zone anyBasisZone = this.GetAnyBasisZone();
								string text;
								if (anyBasisZone == null)
								{
									text = null;
								}
								else
								{
									WorldBlueprint worldBlueprint = anyBasisZone.ResolveWorldBlueprint();
									text = ((worldBlueprint != null) ? worldBlueprint.Protocol : null);
								}
								string text2 = text;
								Zone zone = The.ZoneManager.GetZone(this.DestinationZone);
								string text3;
								if (zone == null)
								{
									text3 = null;
								}
								else
								{
									WorldBlueprint worldBlueprint2 = zone.ResolveWorldBlueprint();
									text3 = ((worldBlueprint2 != null) ? worldBlueprint2.Protocol : null);
								}
								string text4 = text3;
								if (text2 == "THIN")
								{
									Popup.ShowFail("You have no bodily tether to recoil.", true, true, true);
								}
								else if (text4 == "THIN")
								{
									if (text2.IsNullOrEmpty())
									{
										Popup.ShowFail(this.ParentObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " encoded with an imprint of the Thin World that has no meaning in the Thick World.", true, true, true);
									}
									else
									{
										Popup.ShowFail(this.ParentObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " encoded with an imprint of the Thin World that has no meaning in your present context.", true, true, true);
									}
								}
								else
								{
									Popup.ShowFail(this.ParentObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " encoded with an imprint that has no meaning in your present context.", true, true, true);
								}
							}
							else if (teleportFailure == 2)
							{
								Zone anyBasisZone2 = this.GetAnyBasisZone();
								string text5;
								if (anyBasisZone2 == null)
								{
									text5 = null;
								}
								else
								{
									WorldBlueprint worldBlueprint3 = anyBasisZone2.ResolveWorldBlueprint();
									text5 = ((worldBlueprint3 != null) ? worldBlueprint3.Plane : null);
								}
								Zone zone2 = The.ZoneManager.GetZone(this.DestinationZone);
								string text6;
								if (zone2 == null)
								{
									text6 = null;
								}
								else
								{
									WorldBlueprint worldBlueprint4 = zone2.ResolveWorldBlueprint();
									text6 = ((worldBlueprint4 != null) ? worldBlueprint4.Plane : null);
								}
								string text7 = text6;
								if (!text5.IsNullOrEmpty())
								{
									Popup.ShowFail("You are stuck in a remote pocket dimension and cannot recoil out.", true, true, true);
								}
								else
								{
									Popup.ShowFail(this.ParentObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " encoded with the imprint of a remote pocket dimension, " + text7 + ", that is inaccessible from your present vibrational plane.", true, true, true);
								}
							}
							else
							{
								Popup.ShowFail("You cannot do that here.", true, true, true);
							}
						}
						else if (activePartStatus == ActivePartStatus.Rusted)
						{
							Popup.ShowFail(this.ParentObject.Poss("activation button", true) + " button is rusted in place.", true, true, true);
						}
						else if (activePartStatus == ActivePartStatus.Broken)
						{
							Popup.ShowFail(this.ParentObject.Itis + " broken...", true, true, true);
						}
						else if (activePartStatus == ActivePartStatus.Booting)
						{
							Popup.ShowFail(this.ParentObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " still starting up.", true, true, true);
						}
						else if (activePartStatus == ActivePartStatus.Unpowered)
						{
							if (num > 0 && this.ParentObject.QueryCharge(false, 0L, true, true) < num)
							{
								Popup.ShowFail(this.ParentObject.Does("hum", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " for a moment, then powers down. " + this.ParentObject.Does("don't", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, true, true, null, true, null, false) + " have enough charge to function.", true, true, true);
							}
							else
							{
								Popup.ShowFail(this.ParentObject.Does("don't", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " have enough charge to function.", true, true, true);
							}
						}
						else if (!customTeleportFailure.IsNullOrEmpty())
						{
							Popup.ShowFail(customTeleportFailure, true, true, true);
						}
						else
						{
							Popup.ShowFail("Nothing happens.", true, true, true);
						}
					}
					return false;
				}
				Actor.PlayWorldOrUISound(this.Sound, null);
				if (Actor.IsPlayer())
				{
					IComponent<GameObject>.AddPlayerMessage("You activate the recoiler.", null, true);
				}
				bool flag = Actor.ZoneTeleport(this.DestinationZone, this.DestinationX, this.DestinationY, FromEvent, this.ParentObject, Actor, null, "You are transported!", true, null);
				if (flag)
				{
					this.LastTurnUsed = The.CurrentTurn;
				}
				return flag;
			}
		}

		// Token: 0x0600A657 RID: 42583 RVA: 0x003CDF38 File Offset: 0x003CC138
		public bool RequireRecoilAbility(GameObject Subject)
		{
			if (!GameObject.Validate(ref Subject))
			{
				return false;
			}
			if (Subject.IsPlayer() && !this.ParentObject.Understood())
			{
				return false;
			}
			if (!this.IsObjectActivePartSubject(Subject))
			{
				return false;
			}
			if (!this.VisibleInRecoilerList())
			{
				return false;
			}
			Subject.RequirePart<RecoilAbility>(false);
			return true;
		}

		// Token: 0x040044A6 RID: 17574
		public const int FAIL_NONE = 0;

		// Token: 0x040044A7 RID: 17575
		public const int FAIL_WORLD = 1;

		// Token: 0x040044A8 RID: 17576
		public const int FAIL_PLANE = 2;

		// Token: 0x040044A9 RID: 17577
		public const int FAIL_PROTOCOL = 4;

		// Token: 0x040044AA RID: 17578
		public string DestinationZone = "";

		// Token: 0x040044AB RID: 17579
		public string Sound;

		// Token: 0x040044AC RID: 17580
		public int DestinationX = 40;

		// Token: 0x040044AD RID: 17581
		public int DestinationY = 13;

		// Token: 0x040044AE RID: 17582
		public bool UsableInCombat;

		// Token: 0x040044AF RID: 17583
		public bool Intraplanar;

		// Token: 0x040044B0 RID: 17584
		public bool Interplanar;

		// Token: 0x040044B1 RID: 17585
		public bool Interprotocol;

		// Token: 0x040044B2 RID: 17586
		public long LastTurnUsed;
	}
}
