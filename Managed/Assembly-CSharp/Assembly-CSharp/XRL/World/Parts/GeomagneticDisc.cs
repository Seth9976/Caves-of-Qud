using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleLib.Console;
using XRL.Rules;
using XRL.UI;
using XRL.World.AI.Pathfinding;
using XRL.World.Anatomy;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	/// <remarks>
	/// overload behavior: the range, number of bounces, and damage roll are
	/// increased by the standard power load bonus, i.e. 2 for the standard
	/// overload power load of 400, and charge usage is adjusted using power
	/// load as a percentage.
	/// </remarks>
	// Token: 0x0200103E RID: 4158
	[Serializable]
	public class GeomagneticDisc : IPoweredPart
	{
		// Token: 0x0600A590 RID: 42384 RVA: 0x003C8DC6 File Offset: 0x003C6FC6
		public GeomagneticDisc()
		{
			this.WorksOnSelf = true;
			this.IsPowerLoadSensitive = true;
		}

		// Token: 0x0600A591 RID: 42385 RVA: 0x003C8E00 File Offset: 0x003C7000
		public override bool SameAs(IPart p)
		{
			GeomagneticDisc geomagneticDisc = p as GeomagneticDisc;
			return !(geomagneticDisc.Damage != this.Damage) && !(geomagneticDisc.Bounces != this.Bounces) && !(geomagneticDisc.Range != this.Range) && base.SameAs(geomagneticDisc);
		}

		// Token: 0x0600A592 RID: 42386 RVA: 0x003C8E5A File Offset: 0x003C705A
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ExamineCriticalFailureEvent.ID || ID == ExamineFailureEvent.ID || ID == PooledEvent<GetThrownWeaponPerformanceEvent>.ID || ID == PooledEvent<GetThrownWeaponRangeEvent>.ID;
		}

		// Token: 0x0600A593 RID: 42387 RVA: 0x003C8E88 File Offset: 0x003C7088
		public override bool HandleEvent(GetThrownWeaponPerformanceEvent E)
		{
			if (base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				E.BaseDamage = this.Damage;
				int num = this.MyPowerLoadBonus(int.MinValue, 100, 150);
				if (num != 0)
				{
					E.DamageModifier += num;
				}
				E.Vorpal = true;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A594 RID: 42388 RVA: 0x003C8EFC File Offset: 0x003C70FC
		public override bool HandleEvent(GetThrownWeaponRangeEvent E)
		{
			GameObject actor = E.Actor;
			E.MaxRange = ((actor != null) ? actor.GetBaseThrowRange(E.Object, null, null, 0) : 0) + this.ParentObject.GetIntProperty("ThrowRangeBonus", 0) + this.MyPowerLoadBonus(int.MinValue, 100, 150) + this.Range.RollCached();
			return false;
		}

		// Token: 0x0600A595 RID: 42389 RVA: 0x003C8F5C File Offset: 0x003C715C
		public override bool HandleEvent(ExamineFailureEvent E)
		{
			return !this.ExamineFailure(E, 25) && base.HandleEvent(E);
		}

		// Token: 0x0600A596 RID: 42390 RVA: 0x003C8F72 File Offset: 0x003C7172
		public override bool HandleEvent(ExamineCriticalFailureEvent E)
		{
			return !this.ExamineFailure(E, 50) && base.HandleEvent(E);
		}

		// Token: 0x0600A597 RID: 42391 RVA: 0x003C8F88 File Offset: 0x003C7188
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600A598 RID: 42392 RVA: 0x003C8F8B File Offset: 0x003C718B
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeforeThrown");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600A599 RID: 42393 RVA: 0x003C8FA0 File Offset: 0x003C71A0
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeforeThrown")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Thrower");
				if (gameObjectParameter == null)
				{
					return false;
				}
				if (this.ParentObject.CurrentCell != null)
				{
					return false;
				}
				GameObject gameObject = E.GetGameObjectParameter("ApparentTarget");
				Cell cell = E.GetParameter("TargetCell") as Cell;
				int num = this.MyPowerLoadLevel();
				int num2 = IComponent<GameObject>.PowerLoadBonus(num, 100, 150);
				int num3 = gameObjectParameter.GetBaseThrowRange(this.ParentObject, null, cell, gameObjectParameter.DistanceTo(cell)) + this.ParentObject.GetIntProperty("ThrowRangeBonus", 0) + num2 + this.Range.RollCached();
				bool flag = false;
				bool flag2 = true;
				bool flag3 = false;
				bool flag4 = false;
				bool flag5 = false;
				bool flag6 = false;
				bool flag7 = false;
				bool flag8 = false;
				bool flag9 = false;
				bool flag10 = false;
				int num4 = 1;
				int? num5 = new int?(num);
				bool flag11 = base.IsReady(flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, num4, null, false, 0L, num5);
				IThrownWeaponFlexPhaseProvider @for = GetThrownWeaponFlexPhaseProviderEvent.GetFor(this.ParentObject, gameObjectParameter);
				if (@for != null)
				{
					@for.ThrownWeaponFlexPhaseStart(this.ParentObject);
				}
				try
				{
					FindPath findPath = null;
					if (gameObject == null || gameObject == gameObjectParameter)
					{
						List<GameObject> list = gameObjectParameter.CurrentCell.FastFloodSearch("Brain", num3);
						if (list.Count > 0)
						{
							list.ShuffleInPlace<GameObject>(null);
							foreach (GameObject gameObject2 in list)
							{
								if (gameObject2 != gameObjectParameter && (gameObjectParameter.IsHostileTowards(gameObject2) || gameObject2.IsHostileTowards(gameObjectParameter)))
								{
									FindPath findPath2 = new FindPath(gameObjectParameter.CurrentCell, gameObject2.CurrentCell, false, true, this.ParentObject, 99, false, false, true, true, @for != null && @for.ThrownWeaponFlexPhaseIsActive(this.ParentObject), null);
									if (findPath2.Usable && findPath2.Directions.Count <= num3)
									{
										gameObject = gameObject2;
										findPath = findPath2;
										break;
									}
								}
							}
						}
						if (gameObject == null)
						{
							if (flag11)
							{
								this.SignalFailure(gameObjectParameter);
								return false;
							}
							return gameObjectParameter.IsPlayer();
						}
					}
					if (!flag11)
					{
						return true;
					}
					if (gameObjectParameter.DistanceTo(gameObject) > num3)
					{
						this.SignalFailure(gameObjectParameter);
						return false;
					}
					if (findPath == null)
					{
						findPath = new FindPath(gameObjectParameter.CurrentCell, gameObject.CurrentCell, false, true, this.ParentObject, 99, false, false, true, true, @for != null && @for.ThrownWeaponFlexPhaseIsActive(this.ParentObject), null);
						if (!findPath.Usable || findPath.Directions.Count > num3)
						{
							this.SignalFailure(gameObjectParameter);
							return false;
						}
					}
					bool flag12 = true;
					bool flag13 = false;
					bool flag14 = false;
					bool flag15 = false;
					bool flag16 = false;
					bool flag17 = false;
					bool flag18 = false;
					bool flag19 = false;
					bool flag20 = false;
					bool flag21 = false;
					int num6 = 1;
					num5 = new int?(num);
					if (!base.IsReady(flag12, flag13, flag14, flag15, flag16, flag17, flag18, flag19, flag20, flag21, num6, null, false, 0L, num5))
					{
						this.SignalLowPower(gameObjectParameter);
						return false;
					}
					if (gameObjectParameter.IsPlayer())
					{
						gameObjectParameter.Target = gameObject;
					}
					GameObject firstThrownWeapon = gameObjectParameter.GetFirstThrownWeapon(delegate(GameObject o)
					{
						GeomagneticDisc geomagneticDisc;
						return o != this.ParentObject && o.TryGetPart<GeomagneticDisc>(out geomagneticDisc) && geomagneticDisc.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null);
					}, null);
					GameObject gameObject3 = gameObjectParameter;
					int num7 = 1000;
					string text = "Physical Skill Throwing Item";
					string text2 = null;
					num5 = null;
					gameObject3.UseEnergy(num7, text, text2, num5, false);
					if (firstThrownWeapon != null)
					{
						IComponent<GameObject>.EmitMessage(gameObjectParameter, this.ParentObject.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " and " + firstThrownWeapon.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " collide and fall to the ground.", ' ', false, false, false, null, null);
						gameObjectParameter.CurrentCell.AddObject(this.ParentObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						gameObjectParameter.CurrentCell.AddObject(firstThrownWeapon, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						return false;
					}
					List<GameObject> list2 = Event.NewGameObjectList();
					List<FindPath> list3 = new List<FindPath>();
					list2.Add(gameObject);
					list3.Add(findPath);
					int num8 = this.Bounces.RollCached() + num2;
					int num9 = 0;
					while (num9 < num8 && num9 < list2.Count)
					{
						List<GameObject> list4 = list2[num9].CurrentCell.FastFloodSearch("Brain", num3);
						if (list4.Count > 0)
						{
							list4.ShuffleInPlace<GameObject>(null);
							foreach (GameObject gameObject4 in list4)
							{
								if (gameObject4 != gameObjectParameter && !list2.Contains(gameObject4) && (gameObjectParameter.IsHostileTowards(gameObject4) || gameObject4.IsHostileTowards(gameObjectParameter)))
								{
									FindPath findPath3 = new FindPath(list2[num9].CurrentCell, gameObject4.CurrentCell, false, true, this.ParentObject, 99, false, false, true, true, @for != null && @for.ThrownWeaponFlexPhaseIsActive(this.ParentObject), null);
									if (findPath3.Usable && findPath3.Directions.Count <= num3)
									{
										bool flag22 = true;
										bool flag23 = false;
										bool flag24 = false;
										bool flag25 = false;
										bool flag26 = false;
										bool flag27 = false;
										bool flag28 = false;
										bool flag29 = false;
										bool flag30 = false;
										bool flag31 = false;
										int num10 = 1;
										num5 = new int?(num);
										if (base.IsReady(flag22, flag23, flag24, flag25, flag26, flag27, flag28, flag29, flag30, flag31, num10, null, false, 0L, num5))
										{
											list2.Add(gameObject4);
											list3.Add(findPath3);
											break;
										}
										break;
									}
								}
							}
						}
						num9++;
					}
					this.DoThrow(gameObjectParameter, list3, false, false, list2, null, E.GetIntParameter("Phase", 0), new int?(num2), @for, E);
					return false;
				}
				finally
				{
					if (@for != null)
					{
						@for.ThrownWeaponFlexPhaseEnd(this.ParentObject);
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600A59A RID: 42394 RVA: 0x003C9560 File Offset: 0x003C7760
		private void DoThrow(GameObject Actor, List<FindPath> Paths, bool UnidentifiedMessage = false, bool UsePopups = false, List<GameObject> Targets = null, GameObject LastTarget = null, int Phase = 0, int? Bonus = null, IThrownWeaponFlexPhaseProvider FlexPhaseProvider = null, IEvent ParentEvent = null)
		{
			if (Paths == null || Paths.Count <= 0)
			{
				return;
			}
			GameObject gameObject;
			Cell cell;
			BodyPart bodyPart;
			int num;
			IContextRelationManager contextRelationManager;
			this.ParentObject.GetContext(out gameObject, out cell, out bodyPart, out num, out contextRelationManager);
			try
			{
				base.PlayWorldSound(this.ParentObject.GetTagOrStringProperty("ThrownSound", "Sounds/Throw/sfx_throwing_geoMagneticDisc_throw"), 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
				Cell cell2 = cell ?? this.ParentObject.GetCurrentCell();
				bool flag = Actor.CurrentZone.IsActive();
				bool flag2 = flag && Options.UseParticleVFX;
				string text = this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false);
				string text2 = "from " + text + ".";
				int num2 = Bonus ?? this.MyPowerLoadBonus(int.MinValue, 100, 150);
				if (Phase == 0)
				{
					Phase = Phase.getWeaponPhase(Actor, GetActivationPhaseEvent.GetFor(this.ParentObject, 0));
				}
				int num3 = Actor.StatMod("Agility", 0) + Actor.GetIntProperty("ThrowToHitBonus", 0) + Actor.GetIntProperty("ThrowToHitSkillBonus", 0);
				if (contextRelationManager != null)
				{
					this.ParentObject.RemoveFromContext(null);
				}
				FindPath findPath = new FindPath(Paths.Last<FindPath>().Steps.Last<Cell>(), Paths[0].Steps[0], false, true, this.ParentObject, 99, false, false, true, true, FlexPhaseProvider != null && FlexPhaseProvider.ThrownWeaponFlexPhaseIsActive(this.ParentObject), null);
				if (findPath.Usable)
				{
					Paths.Add(findPath);
				}
				List<GameObject> objectsThatWantEvent = Actor.CurrentZone.GetObjectsThatWantEvent(PooledEvent<ProjectileMovingEvent>.ID, ProjectileMovingEvent.CascadeLevel);
				ProjectileMovingEvent projectileMovingEvent = null;
				if (objectsThatWantEvent.Count > 0)
				{
					projectileMovingEvent = PooledEvent<ProjectileMovingEvent>.FromPool();
					projectileMovingEvent.Attacker = Actor;
					projectileMovingEvent.Projectile = this.ParentObject;
					projectileMovingEvent.Throw = true;
				}
				TextConsole textConsole = Look._TextConsole;
				ScreenBuffer scrapBuffer = TextConsole.ScrapBuffer;
				ScreenBuffer scrapBuffer2 = TextConsole.ScrapBuffer2;
				if (flag && !flag2)
				{
					scrapBuffer.Draw();
					The.Core.RenderMapToBuffer(scrapBuffer);
					scrapBuffer2.Copy(scrapBuffer);
				}
				RenderEvent renderEvent = ((flag && !flag2) ? this.ParentObject.RenderForUI(null, true) : null);
				int i = 0;
				int count = Paths.Count;
				while (i < count)
				{
					FindPath findPath2 = Paths[i];
					GameObject gameObject2 = null;
					if (findPath2 == findPath)
					{
						gameObject2 = LastTarget;
					}
					else if (Targets != null && Targets.Count > i)
					{
						gameObject2 = Targets[i];
					}
					if (projectileMovingEvent != null)
					{
						projectileMovingEvent.ApparentTarget = gameObject2;
						projectileMovingEvent.TargetCell = ((gameObject2 != null) ? gameObject2.CurrentCell : null) ?? Actor.CurrentCell;
						projectileMovingEvent.Path = findPath2.Steps.Select<Cell, Point>((Cell c) => c.Point).ToList<Point>();
						projectileMovingEvent.ScreenBuffer = scrapBuffer;
					}
					bool flag3 = false;
					int j = 0;
					int num4 = findPath2.Steps.Count - 1;
					while (j <= num4)
					{
						findPath2.Steps[j].WakeCreaturesInArea();
						if (FlexPhaseProvider != null)
						{
							bool flag4;
							bool flag5;
							if (!FlexPhaseProvider.ThrownWeaponFlexPhaseTraversal(Actor, (gameObject2 != null && findPath2.Steps[j].Objects.Contains(gameObject2)) ? gameObject2 : null, gameObject2, this.ParentObject, Phase, cell2, findPath2.Steps[j], out flag4, out flag5, true))
							{
								return;
							}
							if (flag5)
							{
								Phase = this.ParentObject.GetPhase();
							}
						}
						if (contextRelationManager != null)
						{
							findPath2.Steps[j].AddObject(this.ParentObject, false, false, true, true, false, true, true, null, "Thrown", null, Actor, null, null, null, ParentEvent);
							if (!GameObject.Validate(this.ParentObject) || this.ParentObject.CurrentCell != findPath2.Steps[j])
							{
								return;
							}
						}
						if (projectileMovingEvent != null)
						{
							bool flag6 = false;
							projectileMovingEvent.Cell = findPath2.Steps[j];
							projectileMovingEvent.PathIndex = j;
							foreach (GameObject gameObject3 in objectsThatWantEvent)
							{
								bool flag7 = gameObject3.HandleEvent(projectileMovingEvent);
								if (!GameObject.Validate(this.ParentObject) || this.ParentObject.CurrentCell != findPath2.Steps[j])
								{
									return;
								}
								if (projectileMovingEvent.ActivateShowUninvolved)
								{
									flag3 = true;
									projectileMovingEvent.ActivateShowUninvolved = false;
								}
								if (projectileMovingEvent.RecheckPhase)
								{
									Phase = this.ParentObject.GetPhase();
								}
								if (projectileMovingEvent.HitOverride != null)
								{
									gameObject2 = projectileMovingEvent.HitOverride;
									projectileMovingEvent.HitOverride = null;
									flag6 = true;
									break;
								}
								if (!flag7)
								{
									flag6 = true;
									break;
								}
							}
							if (flag6)
							{
								i = count;
								if (findPath2 == findPath)
								{
									break;
								}
								findPath = new FindPath(findPath2.Steps[j], Paths[0].Steps[0], false, true, this.ParentObject, 99, false, false, true, true, FlexPhaseProvider != null && FlexPhaseProvider.ThrownWeaponFlexPhaseIsActive(this.ParentObject), null);
								if (findPath.Usable)
								{
									Paths[Paths.Count - 1] = findPath;
									i--;
									break;
								}
								break;
							}
						}
						if (flag && !flag2)
						{
							if (j == num4 && findPath2 != findPath)
							{
								scrapBuffer.Copy(scrapBuffer2);
								scrapBuffer.Goto(findPath2.Steps[j].X, findPath2.Steps[j].Y);
								scrapBuffer.Write("&RX", true, false, false, null, -1);
							}
							else
							{
								scrapBuffer.Copy(scrapBuffer2);
								scrapBuffer.Goto(findPath2.Steps[j].X, findPath2.Steps[j].Y);
								scrapBuffer.Write(renderEvent);
							}
							scrapBuffer.Draw();
							textConsole.WaitFrame();
						}
						cell2 = findPath2.Steps[j];
						j++;
					}
					if (flag2 && cell2 != null && findPath2.Steps[0] != cell2)
					{
						The.Core.RenderBase(true, false);
						CombatJuiceEntryMissileWeaponVFX combatJuiceEntryMissileWeaponVFX = CombatJuice.Throw(Actor, this.ParentObject, findPath2.Steps[0].Location, cell2.Location, false);
						if (i == count - 1)
						{
							CombatJuice.BlockUntilFinished(combatJuiceEntryMissileWeaponVFX, null, 1500, true);
						}
					}
					if (gameObject2 != null)
					{
						int num5 = Stat.Random(1, 20);
						int num6 = num3 + num5;
						int combatDV = Stats.GetCombatDV(gameObject2);
						if (num6 > combatDV || num5 == 20)
						{
							if (gameObject2.PhaseMatches(Phase))
							{
								gameObject2.PlayWorldSound(this.ParentObject.GetTagOrStringProperty("ImpactSound", "Sounds/Throw/sfx_throwing_geoMagneticDisc_impact"), 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
								bool isCreature = gameObject2.IsCreature;
								string blueprint = gameObject2.Blueprint;
								WeaponUsageTracking.TrackThrownWeaponHit(Actor, this.ParentObject, isCreature, blueprint, false);
								if (IComponent<GameObject>.Visible(gameObject2))
								{
									gameObject2.ParticleBlip("&C\u0003", 10, 0L, false);
								}
								int combatAV = Stats.GetCombatAV(gameObject2);
								ThrownWeapon part = this.ParentObject.GetPart<ThrownWeapon>();
								string text3 = ((part != null) ? part.Damage : null) ?? "2d6";
								int num7 = combatAV;
								int num8 = 0;
								int num9 = combatAV;
								bool flag8 = true;
								GetThrownWeaponPerformanceEvent.GetFor(this.ParentObject, ref text3, ref num7, ref num8, ref num9, ref flag8, false, Actor, gameObject2);
								int num10 = text3.GetCachedDieRoll().Resolve() + num2;
								Event @event = Event.New("WeaponPseudoThrowHit", 0, 0, 0);
								@event.SetParameter("Damage", num10);
								@event.SetParameter("Owner", Actor);
								@event.SetParameter("Attacker", Actor);
								@event.SetParameter("Defender", gameObject2);
								@event.SetParameter("Weapon", this.ParentObject);
								@event.SetParameter("Projectile", this.ParentObject);
								@event.SetParameter("ApparentTarget", gameObject2);
								this.ParentObject.FireEvent(@event);
								num10 = @event.GetIntParameter("Damage", 0);
								bool flag9 = IComponent<GameObject>.Visible(gameObject2);
								GameObject gameObject4 = gameObject2;
								string text4 = "Thrown PseudoThrown Vorpal Cudgel";
								string text5 = null;
								string text6 = null;
								GameObject gameObject5 = null;
								string text7 = (UnidentifiedMessage ? string.Concat(new string[] { "from ", text, " flying into ", gameObject2.them, "!" }) : text2);
								GameObject parentObject = this.ParentObject;
								GameObject gameObject6 = null;
								GameObject gameObject7 = null;
								string text8 = text7;
								bool flag10 = false;
								bool flag11 = false;
								bool flag12 = false;
								int num11 = Phase;
								gameObject4.TakeDamage(ref num10, text4, text5, text6, Actor, gameObject5, parentObject, gameObject6, gameObject7, text8, flag10, flag11, flag12, flag3, false, true, false, false, UsePopups, num11, null);
								WeaponUsageTracking.TrackThrownWeaponDamage(Actor, this.ParentObject, isCreature, blueprint, false, num10);
								if (flag9)
								{
									this.ParentObject.MakeUnderstood(true);
								}
							}
							else if (IComponent<GameObject>.Visible(gameObject2))
							{
								gameObject2.ParticleBlip("&b\t", 10, 0L, false);
								base.DidXToY("pass", "through", gameObject2, null, "!", null, null, gameObject2, null, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
							}
						}
						else
						{
							gameObject2.ParticleBlip("&K\t", 10, 0L, false);
							IComponent<GameObject>.XDidY(gameObject2, "flinch", "out of the way of " + text, "!", null, null, gameObject2, null, false, false, null, null, false, false, false, false, false, null);
						}
					}
					i++;
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("GeomagneticDisc DoThrow", ex, "game_exception");
			}
			finally
			{
				if (contextRelationManager != null && GameObject.Validate(this.ParentObject))
				{
					this.ParentObject.RemoveFromContext(null);
					contextRelationManager.RestoreContextRelation(this.ParentObject, gameObject, cell, bodyPart, num, true);
				}
			}
		}

		// Token: 0x0600A59B RID: 42395 RVA: 0x003C9F5C File Offset: 0x003C815C
		private void SignalFailure(GameObject actor)
		{
			if (actor != null && actor.IsPlayer())
			{
				Popup.ShowFail("A loud buzz is emitted. The failure glyph flashes on the side of " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", true, true, true);
			}
		}

		// Token: 0x0600A59C RID: 42396 RVA: 0x003C9FB8 File Offset: 0x003C81B8
		private void SignalLowPower(GameObject actor)
		{
			if (actor != null && actor.IsPlayer())
			{
				Popup.ShowFail("A loud buzz is emitted. The low power glyph flashes on the side of " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", true, true, true);
			}
		}

		// Token: 0x0600A59D RID: 42397 RVA: 0x003CA014 File Offset: 0x003C8214
		private bool ExamineFailure(IExamineEvent E, int Chance)
		{
			if (E.Pass == 1 && GlobalConfig.GetBoolSetting("ContextualExamineFailures", false) && Chance.in100())
			{
				int num = this.MyPowerLoadLevel();
				bool flag = true;
				bool flag2 = false;
				bool flag3 = false;
				bool flag4 = false;
				bool flag5 = false;
				bool flag6 = false;
				bool flag7 = false;
				bool flag8 = false;
				bool flag9 = true;
				bool flag10 = false;
				int num2 = 1;
				int? num3 = new int?(num);
				if (base.IsReady(flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, num2, null, false, 0L, num3))
				{
					Cell cell = this.ParentObject.CurrentCell ?? E.Actor.CurrentCell;
					if (cell != null)
					{
						int num4 = IComponent<GameObject>.PowerLoadBonus(num, 100, 150);
						int num5 = 0;
						List<Cell> list = Event.NewCellList();
						list.Add(cell);
						List<FindPath> list2 = new List<FindPath>();
						int num6 = this.Bounces.RollCached() + num4;
						int num7 = this.Range.RollCached() + num4;
						List<Cell> passableConnectedAdjacentCells = cell.GetPassableConnectedAdjacentCells(num7, true, null, true);
						while (list2.Count <= num6 && ++num5 < 100)
						{
							Cell cell2;
							if (list2.Count == 0 && cell != E.Actor.CurrentCell)
							{
								cell2 = E.Actor.CurrentCell;
							}
							else
							{
								cell2 = passableConnectedAdjacentCells.GetRandomElement<Cell>(null);
							}
							if (cell2 == null)
							{
								break;
							}
							passableConnectedAdjacentCells.Remove(cell2);
							if (!list.Contains(cell2) && (cell2 == E.Actor.CurrentCell || cell2.IsEmptyOfSolid(true)))
							{
								FindPath findPath = new FindPath(list.Last<Cell>(), cell2, false, true, this.ParentObject, 99, false, false, true, true, false, null);
								if (findPath.Usable)
								{
									list.Add(cell2);
									list2.Add(findPath);
								}
							}
						}
						if (list2.Count > 0)
						{
							if (E.Actor.IsPlayer())
							{
								Popup.Show(this.ParentObject.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " suddenly" + this.ParentObject.GetVerb("start", true, false, false, true, false) + " flying around!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
							}
							GameObject actor = E.Actor;
							List<FindPath> list3 = list2;
							bool flag11 = true;
							GameObject actor2 = E.Actor;
							this.DoThrow(actor, list3, flag11, E.Actor.IsPlayer(), null, actor2, 0, new int?(num4), null, E);
							E.Identify = true;
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x0400446A RID: 17514
		public string Damage = "2d6";

		// Token: 0x0400446B RID: 17515
		public string Bounces = "5";

		// Token: 0x0400446C RID: 17516
		public string Range = "12";
	}
}
