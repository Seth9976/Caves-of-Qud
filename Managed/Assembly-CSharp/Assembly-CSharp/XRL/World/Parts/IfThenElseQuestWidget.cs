using System;
using System.Collections.Generic;
using XRL.UI;
using XRL.Wish;
using XRL.World.AI;
using XRL.World.AI.Pathfinding;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x020012FF RID: 4863
	[HasWishCommand]
	[Serializable]
	public class IfThenElseQuestWidget : IPart
	{
		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x0600BE94 RID: 48788 RVA: 0x0045C564 File Offset: 0x0045A764
		// (set) Token: 0x0600BE95 RID: 48789 RVA: 0x0045C5B3 File Offset: 0x0045A7B3
		public GameObject Tau
		{
			get
			{
				if (!GameObject.Validate(ref this._Tau) || this._Tau.CurrentCell == null)
				{
					this._Tau = this.ParentObject.CurrentZone.FindFirstObject(new Predicate<GameObject>(IfThenElseQuestWidget.IsTau));
				}
				return this._Tau;
			}
			set
			{
				this._Tau = value;
			}
		}

		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x0600BE96 RID: 48790 RVA: 0x0045C5BC File Offset: 0x0045A7BC
		// (set) Token: 0x0600BE97 RID: 48791 RVA: 0x0045C60B File Offset: 0x0045A80B
		public GameObject Companion
		{
			get
			{
				if (!GameObject.Validate(ref this._Companion) || this._Companion.CurrentCell == null)
				{
					this._Companion = this.ParentObject.CurrentZone.FindFirstObject(new Predicate<GameObject>(IfThenElseQuestWidget.IsCompanion));
				}
				return this._Companion;
			}
			set
			{
				this._Companion = value;
			}
		}

		// Token: 0x0600BE98 RID: 48792 RVA: 0x0045C614 File Offset: 0x0045A814
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BE99 RID: 48793 RVA: 0x0045C617 File Offset: 0x0045A817
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600BE9A RID: 48794 RVA: 0x0045C61A File Offset: 0x0045A81A
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ZoneActivatedEvent.ID;
		}

		// Token: 0x0600BE9B RID: 48795 RVA: 0x0045C630 File Offset: 0x0045A830
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			if (The.Game.HasUnfinishedQuest("If, Then, Else"))
			{
				if (this.Stage <= 0)
				{
					this.Stage = 1;
					The.Game.FinishQuestStep("If, Then, Else", "Go to the Bottom of Taproot", -1, true, null);
					FindPath findPath = new FindPath(E.Zone.GetCell(40, 10), E.Zone.GetCell(35, 19), false, true, null, 100, false, false, false, false, false, null);
					if (findPath.Usable)
					{
						foreach (Cell cell in findPath.Steps)
						{
							cell.ClearImpassableObjects(null, false);
						}
					}
				}
				if (this.Stage >= 1 && this.Stage < 5)
				{
					this.RequireSubjects();
				}
			}
			else
			{
				this.ParentObject.Destroy(null, true, false, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BE9C RID: 48796 RVA: 0x0045C72C File Offset: 0x0045A92C
		public void RequireSubjects()
		{
			if (this.Tau == null)
			{
				this.Tau = this.ParentObject.CurrentZone.GetCell(40, 10).AddObject(this.TauBlueprint, null, null, null, null);
			}
			this.Tau.RegisterPartEvent(this, "AfterConversation");
			this.Tau.RegisterPartEvent(this, "TookDamage");
			this.Tau.RegisterPartEvent(this, "BeforeDie");
			if (this.Companion == null)
			{
				this.Companion = this.ParentObject.CurrentZone.GetCell(33, 19).AddObject("AoygNoLonger", null, null, null, null);
			}
		}

		// Token: 0x0600BE9D RID: 48797 RVA: 0x0045C7D4 File Offset: 0x0045A9D4
		public override bool FireEvent(Event E)
		{
			if (this.Stage == 1)
			{
				if (E.ID == "AfterConversation" && The.Game.HasFinishedQuestStep("If, Then, Else", "Reunite Tau with Her Chime"))
				{
					this.Timer = 0;
					this.Tau = this.Tau.ReplaceWith(this.TauBlueprint);
					this.Tau.TeleportSwirl(null, "&C", false, null, 'ù', false);
					this.RequireSubjects();
					this.Stage = ((this.Tau.Blueprint == "TauNoLonger") ? 2 : 5);
				}
				else if (E.ID == "TookDamage")
				{
					GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
					GameObject gameObjectParameter2 = E.GetGameObjectParameter("Defender");
					GameObject companion = this.Companion;
					if (companion != null && gameObjectParameter2 == this.Tau && (!gameObjectParameter2.IsHostileTowards(gameObjectParameter) || !companion.IsHostileTowards(gameObjectParameter)))
					{
						companion.AddOpinion<OpinionAttackAlly>(gameObjectParameter, gameObjectParameter2, 1f);
					}
				}
			}
			if (E.ID == "BeforeDie")
			{
				GameObject gameObject = E.GetParameter("Killer") as GameObject;
				if (gameObject != null && gameObject.IsPlayer())
				{
					List<GameObject> list = Event.NewGameObjectList();
					foreach (GameObject gameObject2 in The.ActiveZone.IterateObjects())
					{
						if (gameObject2.Blueprint == "TauHeadpiece")
						{
							list.Add(gameObject2);
						}
						else
						{
							if (gameObject2.Inventory != null)
							{
								foreach (GameObject gameObject3 in gameObject2.Inventory.Objects)
								{
									if (gameObject3.Blueprint == "TauHeadpiece")
									{
										list.Add(gameObject3);
									}
								}
							}
							Body body = gameObject2.Body;
							List<GameObject> list2 = ((body != null) ? body.GetEquippedObjectsReadonly() : null);
							if (list2 != null)
							{
								foreach (GameObject gameObject4 in list2)
								{
									if (gameObject4.Blueprint == "TauHeadpiece")
									{
										list.Add(gameObject4);
									}
								}
							}
						}
					}
					foreach (GameObject gameObject5 in list)
					{
						gameObject5.ReplaceWith("TauDagger");
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600BE9E RID: 48798 RVA: 0x0045CA84 File Offset: 0x0045AC84
		public static bool IsTau(GameObject Object)
		{
			GameUnique part = Object.GetPart<GameUnique>();
			return ((part != null) ? part.State : null) == "TauElse";
		}

		// Token: 0x0600BE9F RID: 48799 RVA: 0x0045CAA2 File Offset: 0x0045ACA2
		public static bool IsCompanion(GameObject Object)
		{
			GameUnique part = Object.GetPart<GameUnique>();
			return ((part != null) ? part.State : null) == "TauCompanion";
		}

		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x0600BEA0 RID: 48800 RVA: 0x0045CAC0 File Offset: 0x0045ACC0
		public string TauBlueprint
		{
			get
			{
				if (!The.Game.HasFinishedQuestStep("If, Then, Else", "Reunite Tau with Her Chime"))
				{
					return "TauSoft";
				}
				if (!The.Game.GetBooleanGameState("TauCompanionDead", false))
				{
					return "TauNoLonger";
				}
				return "TauWanderer";
			}
		}

		// Token: 0x0600BEA1 RID: 48801 RVA: 0x0045CAFB File Offset: 0x0045ACFB
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600BEA2 RID: 48802 RVA: 0x0045CB00 File Offset: 0x0045AD00
		public override void TurnTick(long TimeTick, int Amount)
		{
			int num = this.Stage;
			if (num <= 0 || num >= 5)
			{
				return;
			}
			GameObject tau = this.Tau;
			bool flag;
			if (tau == null)
			{
				flag = null != null;
			}
			else
			{
				Brain brain = tau.Brain;
				flag = ((brain != null) ? brain.PartyLeader : null) != null;
			}
			if (flag)
			{
				tau.Brain.PartyLeader = null;
				tau.Brain.Goals.Clear();
			}
			GameObject companion = this.Companion;
			bool flag2;
			if (companion == null)
			{
				flag2 = null != null;
			}
			else
			{
				Brain brain2 = companion.Brain;
				flag2 = ((brain2 != null) ? brain2.PartyLeader : null) != null;
			}
			if (flag2)
			{
				companion.Brain.PartyLeader = null;
				companion.Brain.Goals.Clear();
			}
			if (this.Stage == 2)
			{
				num = this.Timer;
				this.Timer = num + 1;
				if (num >= 5)
				{
					this.Timer = 0;
					if (((tau != null) ? tau.Brain : null) == null)
					{
						return;
					}
					this.Stage = 3;
					tau.Brain.Wanders = (tau.Brain.WandersRandomly = false);
					GlobalLocation startingCell = tau.Brain.StartingCell;
					if (startingCell == null || startingCell.CellX != 35 || startingCell.CellY != 19)
					{
						tau.Brain.StartingCell = this.ParentObject.CurrentZone.GetCell(35, 19).GetGlobalLocation();
						tau.Brain.Goals.Clear();
						return;
					}
					return;
				}
			}
			if (this.Stage == 3)
			{
				if (The.Game.HasIntGameState("ElseingComplete"))
				{
					this.Stage = 4;
					this.Timer = 0;
					return;
				}
			}
			else if (this.Stage == 4)
			{
				num = this.Timer;
				this.Timer = num + 1;
				if (num >= 5)
				{
					this.Stage = 5;
					if (tau != null)
					{
						tau.RemoveEffect(typeof(Dominated));
						if (!tau.IsPlayer())
						{
							tau.TeleportSwirl(null, "&C", false, null, 'ù', false);
							tau.Physics.DidX("disappear", null, null, null, null, null, null, false, false, null, null, true, false, false, false, false, null);
							tau.Obliterate(null, true, null);
						}
					}
					if (companion != null)
					{
						companion.RemoveEffect(typeof(Dominated));
						if (!companion.IsPlayer())
						{
							foreach (GameObject gameObject in companion.CurrentZone.IterateObjects())
							{
								Brain brain3 = gameObject.Brain;
								if (((brain3 != null) ? brain3.PartyLeader : null) == companion)
								{
									gameObject.Obliterate(null, true, null);
								}
							}
							companion.TeleportSwirl(null, "&C", false, null, 'ù', false);
							companion.Physics.DidX("disappear", null, null, null, null, null, null, false, false, null, null, true, false, false, false, false, null);
							companion.Obliterate(null, true, null);
						}
					}
					this.ParentObject.Obliterate(null, true, null);
				}
			}
		}

		// Token: 0x0600BEA3 RID: 48803 RVA: 0x0045CDBC File Offset: 0x0045AFBC
		[WishCommand("elseing:start", null)]
		public static void WishElseingStart()
		{
			Popup.Suppress = true;
			try
			{
				The.Game.StartQuest("If, Then, Else", null, null, null);
				if (The.Player.HasItemWithBlueprint("TauChime") == null)
				{
					The.Player.ReceiveObject("TauChime", false, 0, 0, null, null, null, null);
				}
			}
			finally
			{
				Popup.Suppress = false;
			}
		}

		// Token: 0x0600BEA4 RID: 48804 RVA: 0x0045CE24 File Offset: 0x0045B024
		[WishCommand("elseing:taproot", null)]
		public static void WishElseingTaproot()
		{
			Popup.Suppress = true;
			try
			{
				The.Game.StartQuest("If, Then, Else", null, null, null);
				if (The.Player.HasItemWithBlueprint("TauChime") == null)
				{
					The.Player.ReceiveObject("TauChime", false, 0, 0, null, null, null, null);
				}
				The.Player.ZoneTeleport("JoppaWorld.76.5.1.1.50", 41, 11, null, null, null, null, "You are transported!", true, null);
			}
			finally
			{
				Popup.Suppress = false;
			}
		}

		// Token: 0x0600BEA5 RID: 48805 RVA: 0x0045CEAC File Offset: 0x0045B0AC
		[WishCommand("elseing:success", null)]
		public static void WishElseingSuccess()
		{
			Popup.Suppress = true;
			try
			{
				The.Game.StartQuest("If, Then, Else", null, null, null);
				The.Game.CompleteQuest("If, Then, Else");
				The.Game.SetIntGameState("ElseingComplete", 1);
			}
			finally
			{
				Popup.Suppress = false;
			}
		}

		// Token: 0x0600BEA6 RID: 48806 RVA: 0x0045CF0C File Offset: 0x0045B10C
		[WishCommand("elseing:kill", null)]
		public static void WishElseingKill(string Name)
		{
			Popup.Suppress = true;
			try
			{
				The.Game.StartQuest("If, Then, Else", null, null, null);
				if (Name.EqualsNoCase("tau"))
				{
					GameObject gameObject = The.ActiveZone.FindFirstObject(new Predicate<GameObject>(IfThenElseQuestWidget.IsTau));
					if (gameObject == null)
					{
						gameObject = GameObjectFactory.Factory.CreateSampleObject("TauNoLonger", null);
					}
					gameObject.Die(The.Player, null, null, null, false, null, null, false, false, null, null, null);
				}
				else if (Name.EqualsNoCase("companion"))
				{
					GameObject gameObject2 = The.ActiveZone.FindFirstObject(new Predicate<GameObject>(IfThenElseQuestWidget.IsCompanion));
					if (gameObject2 == null)
					{
						gameObject2 = GameObjectFactory.Factory.CreateSampleObject("AoygNoLonger", null);
					}
					gameObject2.Die(The.Player, null, null, null, false, null, null, false, false, null, null, null);
				}
				else if (Name.EqualsNoCase("chime"))
				{
					GameObject gameObject3 = The.ActiveZone.FindFirstObject("TauChime");
					if (gameObject3 == null)
					{
						gameObject3 = GameObjectFactory.Factory.CreateSampleObject("TauChime", null);
					}
					gameObject3.Die(The.Player, null, null, null, false, null, null, false, false, null, null, null);
				}
			}
			finally
			{
				Popup.Suppress = false;
			}
		}

		// Token: 0x04004CB9 RID: 19641
		public const string QUEST_ID = "If, Then, Else";

		// Token: 0x04004CBA RID: 19642
		public const string STEP_TAP_ID = "Go to the Bottom of Taproot";

		// Token: 0x04004CBB RID: 19643
		public const string STEP_CHIME_ID = "Reunite Tau with Her Chime";

		// Token: 0x04004CBC RID: 19644
		public int Stage;

		// Token: 0x04004CBD RID: 19645
		public int Timer;

		// Token: 0x04004CBE RID: 19646
		[NonSerialized]
		private GameObject _Tau;

		// Token: 0x04004CBF RID: 19647
		[NonSerialized]
		private GameObject _Companion;
	}
}
