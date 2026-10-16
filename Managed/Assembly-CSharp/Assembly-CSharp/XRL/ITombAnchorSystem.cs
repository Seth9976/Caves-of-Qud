using System;
using XRL.Collections;
using XRL.Messages;
using XRL.Rules;
using XRL.World;
using XRL.World.Effects;
using XRL.World.Parts;

namespace XRL
{
	// Token: 0x020004F2 RID: 1266
	public abstract class ITombAnchorSystem : IGameSystem
	{
		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x060036A3 RID: 13987
		public abstract int Depth { get; }

		// Token: 0x060036A4 RID: 13988 RVA: 0x00107A88 File Offset: 0x00105C88
		public bool PlayerInRecallArea()
		{
			GameObject player = The.Player;
			int? num;
			if (player == null)
			{
				num = null;
			}
			else
			{
				Zone currentZone = player.CurrentZone;
				num = ((currentZone != null) ? new int?(currentZone.Z) : null);
			}
			if ((num ?? (-1)) == this.Depth)
			{
				GameObject player2 = The.Player;
				return this.IsZoneInTomb((player2 != null) ? player2.CurrentZone : null);
			}
			return false;
		}

		// Token: 0x060036A5 RID: 13989 RVA: 0x00107AFC File Offset: 0x00105CFC
		public bool IsZoneInTomb(Zone Z)
		{
			if (Z == null)
			{
				return false;
			}
			InteriorZone interiorZone = Z as InteriorZone;
			if (interiorZone != null)
			{
				Z = interiorZone.ResolveBasisZone() ?? Z;
			}
			return Z.wX == 53 && Z.wY == 3 && Z.Z >= 7 && Z.Z <= 11;
		}

		// Token: 0x060036A6 RID: 13990 RVA: 0x00107B50 File Offset: 0x00105D50
		public bool IsZoneInRange(Zone Z)
		{
			return Z != null && Z.wX == 53 && Z.wY == 3 && Z.Z == this.Depth && (Z.X != 1 || Z.Y != 1);
		}

		// Token: 0x060036A7 RID: 13991 RVA: 0x00107B90 File Offset: 0x00105D90
		public virtual string GetAnchorZoneFor(Zone Z)
		{
			int num;
			int num2;
			do
			{
				num = Stat.Random(0, 2);
				num2 = Stat.Random(0, 2);
			}
			while ((num == 1 && num2 == 1) || (num == Z.X && num2 == Z.Y));
			return ZoneID.Assemble(Z.ZoneWorld, Z.wX, Z.wY, num, num2, Z.Z);
		}

		// Token: 0x060036A8 RID: 13992 RVA: 0x00107BE8 File Offset: 0x00105DE8
		public void OnEndTurn()
		{
			if (The.Game.HasIntGameState("BellOfRestDestroyed"))
			{
				this.Active = false;
				return;
			}
			this.Timer--;
			if (this.Timer <= 0)
			{
				if (this.Active)
				{
					this.AnchorCall();
				}
				this.Timer = Stat.Roll(this.Duration, null);
				return;
			}
			if (this.Timer % this.Announcement == 0 && this.Active && this.PlayerInRecallArea())
			{
				The.PlayerCell.PlayWorldSound("sfx_bellOfRest_toll", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				MessageQueue.AddPlayerMessage("&MThe Bell of Rest tolls! The dead will be recalled in " + this.Timer.ToString() + " rounds.", null, true);
			}
		}

		// Token: 0x060036A9 RID: 13993 RVA: 0x00107CB4 File Offset: 0x00105EB4
		public void Recall(Zone TargetZone)
		{
			if (!this.PlayerInRecallArea())
			{
				return;
			}
			GameObject player = The.Player;
			Cell cell = TargetZone.GetCellsWithObject("AnchorRoomTile").GetRandomElement<Cell>(null);
			if (cell == null)
			{
				cell = TargetZone.GetCellsWithObject("AnchorRoomTile").GetRandomElement<Cell>(null).GetCellOrFirstConnectedSpawnLocation(true);
			}
			if (cell != null && cell.HasObjectWithPart("StairsDown"))
			{
				cell = cell.GetConnectedSpawnLocation(true);
			}
			ScopeDisposedList<GameObject> scopeDisposedList = null;
			if (player.HasEffect(typeof(LatchedOnto)))
			{
				for (int i = player._Effects.Count - 1; i >= 0; i--)
				{
					LatchedOnto latchedOnto = player._Effects[i] as LatchedOnto;
					if (latchedOnto != null)
					{
						GameObject equipped = latchedOnto.LatchedOnWeapon.Equipped;
						if (GameObject.Validate(equipped))
						{
							if (scopeDisposedList == null)
							{
								scopeDisposedList = ScopeDisposedList<GameObject>.GetFromPool();
							}
							scopeDisposedList.Add(equipped);
							equipped.TeleportTo(cell, new int?(0), true, false, true, false, "disappear", "appear");
						}
						player.RemoveEffect(latchedOnto, true);
					}
				}
			}
			SoundManager.PlaySound("sfx_bellOfRest_toll", 0f, 1f, 1f, SoundRequest.SoundEffectType.None, 0f);
			player.TeleportTo(cell, new int?(0), true, false, true, false, "disappear", "appear");
			GameObject firstObjectWithPart = cell.GetFirstObjectWithPart("Enclosing");
			if (firstObjectWithPart != null)
			{
				Enclosing part = firstObjectWithPart.GetPart<Enclosing>();
				part.EnterEnclosure(player, null);
				if (!scopeDisposedList.IsNullOrEmpty<GameObject>())
				{
					foreach (GameObject gameObject in scopeDisposedList)
					{
						part.EnterEnclosure(gameObject, null);
						CryptFerretBehavior part2 = gameObject.GetPart<CryptFerretBehavior>();
						if (part2 != null)
						{
							part2.behaviorState = "looting";
							gameObject.Brain.Goals.Clear();
						}
					}
				}
			}
			if (scopeDisposedList != null)
			{
				scopeDisposedList.Dispose();
			}
			MessageQueue.AddPlayerMessage("You've been recalled to a resting place.", 'M', true);
			player.TeleportSwirl(null, "&C", false, null, 'ù', false);
		}

		// Token: 0x060036AA RID: 13994 RVA: 0x00107EB0 File Offset: 0x001060B0
		public void AnchorCall()
		{
			if (!base.Player.HasMarkOfDeath(null))
			{
				return;
			}
			if (base.Player.HasEffect(typeof(CorpseTethered)))
			{
				MessageQueue.AddPlayerMessage("You were not recalled as you're already in a resting place.", 'M', true);
				return;
			}
			Zone currentZone = base.Player.CurrentZone;
			string anchorZoneFor = this.GetAnchorZoneFor(currentZone);
			this.Recall(The.ZoneManager.GetZone(anchorZoneFor));
		}

		// Token: 0x060036AB RID: 13995 RVA: 0x00107F16 File Offset: 0x00106116
		public override void Register(XRLGame Game, IEventRegistrar Registrar)
		{
			Registrar.Register(SingletonEvent<EndTurnEvent>.ID, 0, false);
			Registrar.Register(ZoneActivatedEvent.ID, 0, false);
		}

		// Token: 0x060036AC RID: 13996 RVA: 0x00107F32 File Offset: 0x00106132
		public override bool HandleEvent(EndTurnEvent E)
		{
			this.OnEndTurn();
			return base.HandleEvent(E);
		}

		// Token: 0x060036AD RID: 13997 RVA: 0x00107F44 File Offset: 0x00106144
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			this.Active = this.IsZoneInRange(E.Zone) && !The.Game.HasIntGameState("BellOfRestDestroyed");
			if (!this.Active && !this.IsZoneInTomb(E.Zone))
			{
				The.Game.FlagSystemForRemoval(this);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04001B4D RID: 6989
		public string Duration = "300";

		// Token: 0x04001B4E RID: 6990
		public int Timer = 300;

		// Token: 0x04001B4F RID: 6991
		public int Announcement = 20;

		// Token: 0x04001B50 RID: 6992
		public bool Active;
	}
}
