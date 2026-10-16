using System;
using Genkit;
using XRL.Messages;
using XRL.Rules;
using XRL.World;
using XRL.World.Parts;

namespace XRL
{
	// Token: 0x020004F8 RID: 1272
	[Serializable]
	public class SvardymSystem : IGameSystem
	{
		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x060036D2 RID: 14034 RVA: 0x00109B04 File Offset: 0x00107D04
		// (set) Token: 0x060036D3 RID: 14035 RVA: 0x00109B53 File Offset: 0x00107D53
		public Zone StormZone
		{
			get
			{
				if (this.lastZone != null)
				{
					if (this._StormZone == null)
					{
						this._StormZone = The.ZoneManager.GetZone(this.lastZone);
					}
					else if (this._StormZone.Suspended)
					{
						this.StormZone = null;
					}
				}
				return this._StormZone;
			}
			set
			{
				this._StormZone = value;
				this.lastZone = ((value != null) ? value.ZoneID : null);
			}
		}

		// Token: 0x060036D4 RID: 14036 RVA: 0x00109B6E File Offset: 0x00107D6E
		public override void Register(XRLGame Game, IEventRegistrar Registrar)
		{
			Registrar.Register(ZoneActivatedEvent.ID, 0, false);
			if (this.storming || Registrar.IsUnregister)
			{
				Registrar.Register(SingletonEvent<EndTurnEvent>.ID, 0, false);
				Registrar.Register(SingletonEvent<GetAmbientLightEvent>.ID, 0, false);
			}
		}

		// Token: 0x060036D5 RID: 14037 RVA: 0x00109BA8 File Offset: 0x00107DA8
		public override bool HandleEvent(GetAmbientLightEvent E)
		{
			if (this.storming && E.Source is Daylight && this.PlayerInStorm())
			{
				if (this.stormEndingTurn > 0)
				{
					E.Radius = Math.Min(this.radius, this.lastRadius);
				}
				else
				{
					this.lastRadius = Math.Min(this.radius, 40) - this.stormTurn;
					E.Radius = Math.Max(0, this.lastRadius);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060036D6 RID: 14038 RVA: 0x00109C28 File Offset: 0x00107E28
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			if (this.IsValidZone(E.Zone))
			{
				if (this.storming)
				{
					this.StormZone = E.Zone;
				}
				else if (SvardymSystem.CHANCE_PER_ROUND_SVARDYM_STORM_DENOM_10000.in10000())
				{
					this.StormZone = E.Zone;
					this.BeginStorm();
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060036D7 RID: 14039 RVA: 0x00109C7E File Offset: 0x00107E7E
		public override bool HandleEvent(EndTurnEvent E)
		{
			if (this.storming)
			{
				this.Tick();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060036D8 RID: 14040 RVA: 0x00109C98 File Offset: 0x00107E98
		public void BeginStorm()
		{
			this.storming = true;
			this.stormTurn = 0;
			this.stormEndingTurn = 0;
			this.eggs = Stat.Random(10, 15);
			this.nextEgg = Stat.Random(5, 7);
			if (this.PlayerInStorm())
			{
				MessageQueue.AddPlayerMessage("Your hear a swelling thpthp sound.", null, true);
				SoundManager.PlaySound("EggSackPlague", 0f, 1f, 1f, SoundRequest.SoundEffectType.None, 0f);
				if (Calendar.IsDay())
				{
					MessageQueue.AddPlayerMessage("The sky begins to darken.", null, true);
				}
			}
			base.RegisterEvent(SingletonEvent<EndTurnEvent>.ID, 0, false);
			base.RegisterEvent(SingletonEvent<GetAmbientLightEvent>.ID, 0, false);
		}

		// Token: 0x060036D9 RID: 14041 RVA: 0x00109D36 File Offset: 0x00107F36
		public void EndStorm()
		{
			this.storming = false;
			this.StormZone = null;
			base.UnregisterEvent(SingletonEvent<EndTurnEvent>.ID);
			base.UnregisterEvent(SingletonEvent<GetAmbientLightEvent>.ID);
		}

		// Token: 0x060036DA RID: 14042 RVA: 0x00109D5C File Offset: 0x00107F5C
		public void SpawnEgg()
		{
			Zone stormZone = this.StormZone;
			Cell cell = ((stormZone != null) ? stormZone.GetEmptyCells(null, null).GetRandomElement<Cell>(null) : null);
			if (cell != null)
			{
				GameObject gameObject = cell.AddObject("Svardym Egg Sac", null, null, null, null);
				if (this.PlayerInStorm())
				{
					if (gameObject.IsVisible())
					{
						gameObject.Slimesplatter(false, "&g.");
						gameObject.DustPuff("&y", 15);
					}
					gameObject.PlayWorldSound("svardym_plop", 1f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				}
			}
		}

		// Token: 0x060036DB RID: 14043 RVA: 0x00109DEA File Offset: 0x00107FEA
		public bool PlayerInStorm()
		{
			return The.Player.CurrentZone == this.StormZone;
		}

		// Token: 0x060036DC RID: 14044 RVA: 0x00109E00 File Offset: 0x00108000
		public void Tick()
		{
			this.stormTurn++;
			if (this.eggs > 0)
			{
				int num = this.nextEgg - 1;
				this.nextEgg = num;
				if (num <= 0)
				{
					this.SpawnEgg();
					this.nextEgg = Stat.Random(5, 7);
					num = this.eggs - 1;
					this.eggs = num;
					if (num <= 3)
					{
						this.stormEndingTurn = this.stormTurn;
						if (this.PlayerInStorm())
						{
							MessageQueue.AddPlayerMessage("The thpthp sound wanes.", null, true);
							if (Calendar.IsDay())
							{
								MessageQueue.AddPlayerMessage("The sky begins to brighten.", null, true);
							}
						}
					}
				}
			}
			if (this.stormEndingTurn > 0)
			{
				if (this.lastRadius < 80)
				{
					this.lastRadius++;
					return;
				}
				this.EndStorm();
			}
		}

		// Token: 0x060036DD RID: 14045 RVA: 0x00109EBA File Offset: 0x001080BA
		public bool IsValidZone(Zone Z)
		{
			return Z.Z == 10 && !Z.HasZoneProperty("NoSvardymStorm") && Z.GetTerrainObject().HasTag("SvardymStorm");
		}

		// Token: 0x04001B55 RID: 6997
		public string lastZone;

		// Token: 0x04001B56 RID: 6998
		public bool storming;

		// Token: 0x04001B57 RID: 6999
		public Location2D epicenter;

		// Token: 0x04001B58 RID: 7000
		public int radius;

		// Token: 0x04001B59 RID: 7001
		public int eggs;

		// Token: 0x04001B5A RID: 7002
		public int nextEgg;

		// Token: 0x04001B5B RID: 7003
		public int stormTurn;

		// Token: 0x04001B5C RID: 7004
		public int stormEndingTurn;

		// Token: 0x04001B5D RID: 7005
		public int lastRadius;

		// Token: 0x04001B5E RID: 7006
		private static readonly int CHANCE_PER_ROUND_SVARDYM_STORM_DENOM_10000 = 5;

		// Token: 0x04001B5F RID: 7007
		[NonSerialized]
		private Zone _StormZone;
	}
}
