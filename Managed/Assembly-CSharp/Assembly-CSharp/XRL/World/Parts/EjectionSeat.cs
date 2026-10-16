using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleLib.Console;
using UnityEngine;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x020012AC RID: 4780
	[Serializable]
	public class EjectionSeat : IActivePart
	{
		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x0600BCC6 RID: 48326 RVA: 0x0045199A File Offset: 0x0044FB9A
		public bool CanEject
		{
			get
			{
				return this.GetSlot() != null && this.GetSitter() != null;
			}
		}

		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x0600BCC7 RID: 48327 RVA: 0x004519AF File Offset: 0x0044FBAF
		public bool WithinInterior
		{
			get
			{
				return this.ParentObject.CurrentZone is InteriorZone;
			}
		}

		// Token: 0x0600BCC8 RID: 48328 RVA: 0x004519C4 File Offset: 0x0044FBC4
		public EjectionSeat()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600BCC9 RID: 48329 RVA: 0x004519E1 File Offset: 0x0044FBE1
		public override bool AllowStaticRegistration()
		{
			return false;
		}

		// Token: 0x0600BCCA RID: 48330 RVA: 0x004519E4 File Offset: 0x0044FBE4
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CommandSeatEject")
			{
				return !this.Eject(true);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600BCCB RID: 48331 RVA: 0x00451A0A File Offset: 0x0044FC0A
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<BeforeInteriorCollapseEvent>.ID || ID == GetInventoryActionsEvent.ID || ID == PooledEvent<GetKineticResistanceEvent>.ID || ID == InventoryActionEvent.ID || ID == PooledEvent<AfterPlayerBodyChangeEvent>.ID;
		}

		// Token: 0x0600BCCC RID: 48332 RVA: 0x00451A40 File Offset: 0x0044FC40
		public GameObject GetSlot()
		{
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell == null)
			{
				return null;
			}
			return currentCell.GetFirstObject(new Predicate<GameObject>(this.IsOurSlot));
		}

		// Token: 0x0600BCCD RID: 48333 RVA: 0x00451A64 File Offset: 0x0044FC64
		public GameObject GetSitter()
		{
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell == null)
			{
				return null;
			}
			return currentCell.GetFirstObject(new Predicate<GameObject>(this.IsOurSitter));
		}

		// Token: 0x0600BCCE RID: 48334 RVA: 0x00451A88 File Offset: 0x0044FC88
		public bool IsOurSlot(GameObject Object)
		{
			return Object.HasPart(typeof(EjectionSlot));
		}

		// Token: 0x0600BCCF RID: 48335 RVA: 0x00451A9A File Offset: 0x0044FC9A
		public bool IsOurSitter(GameObject Object)
		{
			return Object.HasEffect(new Predicate<Effect>(this.IsOurEffect));
		}

		// Token: 0x0600BCD0 RID: 48336 RVA: 0x00451AB0 File Offset: 0x0044FCB0
		public bool IsOurEffect(Effect FX)
		{
			Sitting sitting = FX as Sitting;
			return sitting != null && sitting.SittingOn == this.ParentObject;
		}

		// Token: 0x0600BCD1 RID: 48337 RVA: 0x00451AD7 File Offset: 0x0044FCD7
		public override bool HandleEvent(GetKineticResistanceEvent E)
		{
			if (!this.ParentObject.Physics.Takeable && this.GetSlot() != null)
			{
				E.LinearIncrease += 800;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BCD2 RID: 48338 RVA: 0x00451B0C File Offset: 0x0044FD0C
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (this.CanEject)
			{
				E.AddAction("Eject", "eject", "CommandSingleSeatEject", null, 'j', false, 0, 0, false, false, false, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BCD3 RID: 48339 RVA: 0x00451B4B File Offset: 0x0044FD4B
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "CommandSingleSeatEject")
			{
				E.RequestInterfaceExit();
				GameManager.Instance.gameQueue.queueTask(delegate
				{
					Zone currentZone = this.ParentObject.CurrentZone;
					this.Eject(false);
				}, 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BCD4 RID: 48340 RVA: 0x00451B89 File Offset: 0x0044FD89
		public override bool HandleEvent(BeforeInteriorCollapseEvent E)
		{
			this.Eject(true);
			return base.HandleEvent(E);
		}

		// Token: 0x0600BCD5 RID: 48341 RVA: 0x00451B9C File Offset: 0x0044FD9C
		public override bool HandleEvent(AfterPlayerBodyChangeEvent E)
		{
			InteriorZone interiorZone = this.ParentObject.CurrentZone as InteriorZone;
			if (interiorZone != null && interiorZone.ParentObject == E.NewBody && E.OldBody.InSameCellAs(this.ParentObject) && this.IsOurSitter(E.OldBody))
			{
				ActivatedAbilities activatedAbilities = E.NewBody.RequirePart<ActivatedAbilities>(false);
				ActivatedAbilityEntry activatedAbilityEntry = activatedAbilities.GetAbilityByCommand("CommandSeatEject");
				if (activatedAbilityEntry == null)
				{
					Guid guid = activatedAbilities.AddAbility("Eject", "CommandSeatEject", E.NewBody.HasPart(typeof(Vehicle)) ? "Vehicle" : "Maneuvers", null, "ø", null, false, false, false, false, false, false, true, false, true, true, false, false, -1, null, null, null, null, null);
					activatedAbilityEntry = activatedAbilities.GetAbility(guid);
				}
				if (this.CanEject)
				{
					activatedAbilityEntry.Enabled = true;
					activatedAbilityEntry.DisabledMessage = null;
				}
				else
				{
					activatedAbilityEntry.Enabled = false;
					activatedAbilityEntry.DisabledMessage = "Your seat is unable to eject.";
				}
				E.NewBody.RegisterPartEvent(this, "CommandSeatEject");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BCD6 RID: 48342 RVA: 0x00451CAC File Offset: 0x0044FEAC
		public bool TryGetEjectionSeats(bool AllInZone, out List<EjectionSeat> Seats, out List<GameObject> Sitters)
		{
			Seats = new List<EjectionSeat>();
			Sitters = new List<GameObject>();
			if (AllInZone)
			{
				using (IEnumerator<GameObject> enumerator = this.ParentObject.CurrentZone.YieldObjects().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						EjectionSeat ejectionSeat;
						if (enumerator.Current.TryGetPart<EjectionSeat>(out ejectionSeat) && ejectionSeat.CanEject)
						{
							Seats.Add(ejectionSeat);
							Sitters.Add(ejectionSeat.GetSitter());
						}
					}
					goto IL_0086;
				}
			}
			if (this.CanEject)
			{
				Seats.Add(this);
				Sitters.Add(this.GetSitter());
			}
			IL_0086:
			return !Seats.IsNullOrEmpty<EjectionSeat>();
		}

		// Token: 0x0600BCD7 RID: 48343 RVA: 0x00451D5C File Offset: 0x0044FF5C
		public List<Cell> GetTargetsFor(List<EjectionSeat> Seats, List<GameObject> Sitters)
		{
			List<Cell> list = new List<Cell>();
			BallBag<Cell> ballBag = new BallBag<Cell>(0);
			for (int i = 0; i < Seats.Count; i++)
			{
				ballBag.Clear();
				EjectionSeat ejectionSeat = Seats[i];
				GameObject gameObject = Sitters[i];
				Cell ejectionOrigin = ejectionSeat.GetEjectionOrigin();
				foreach (Cell cell in ejectionOrigin.YieldAdjacentCells(this.MaxRange, true, true))
				{
					if (ejectionOrigin.PathDistanceTo(cell) >= this.MinRange && cell.IsPassable(ejectionSeat.ParentObject, true) && cell.IsPassable(gameObject, true) && !list.Contains(cell))
					{
						int navigationWeightFor = cell.GetNavigationWeightFor(gameObject, false, false, false, false, false);
						int num = 300;
						if (navigationWeightFor > 30)
						{
							num = 1;
						}
						else if (navigationWeightFor > 10)
						{
							num = 10;
						}
						else if (navigationWeightFor > 1)
						{
							num = 100;
						}
						else if (navigationWeightFor == 1)
						{
							num = 200;
						}
						ballBag.Add(cell, num);
					}
				}
				list.Add(ballBag.PeekOne());
			}
			return list;
		}

		// Token: 0x0600BCD8 RID: 48344 RVA: 0x00451E90 File Offset: 0x00450090
		public Cell GetEjectionOrigin()
		{
			InteriorZone interiorZone = this.ParentObject.CurrentZone as InteriorZone;
			return ((interiorZone != null) ? interiorZone.ParentCell : null) ?? this.ParentObject.CurrentCell;
		}

		// Token: 0x0600BCD9 RID: 48345 RVA: 0x00451EBD File Offset: 0x004500BD
		public GameObject GetEjectionSource()
		{
			InteriorZone interiorZone = this.ParentObject.CurrentZone as InteriorZone;
			return ((interiorZone != null) ? interiorZone.ParentObject : null) ?? this.ParentObject;
		}

		// Token: 0x0600BCDA RID: 48346 RVA: 0x00451EE8 File Offset: 0x004500E8
		public void PlayAnimation(List<EjectionSeat> Seats, List<GameObject> Sitters, List<Cell> Targets)
		{
			CombatJuiceSynchronizedEntry combatJuiceSynchronizedEntry = new CombatJuiceSynchronizedEntry();
			for (int i = 0; i < Sitters.Count; i++)
			{
				Cell currentCell = Sitters[i].CurrentCell;
				Cell ejectionOrigin = Seats[i].GetEjectionOrigin();
				if (currentCell.IsVisible() || ejectionOrigin.IsVisible())
				{
					combatJuiceSynchronizedEntry.Entries.Add(CombatJuice.Jump(Sitters[i], ejectionOrigin.Location, Targets[i].Location, Stat.Random(0.95f, 1.15f), Stat.Random(1.8f, 2.2f), 0.35f, false, false));
				}
			}
			if (combatJuiceSynchronizedEntry.Entries.IsNullOrEmpty<CombatJuiceEntry>())
			{
				return;
			}
			Zone zone = this.GetEjectionOrigin().ParentZone;
			bool hide = this.ParentObject.IsInActiveZone();
			CombatJuiceManager.enqueueEntry(combatJuiceSynchronizedEntry, false);
			CombatJuice.BlockUntilFinished(combatJuiceSynchronizedEntry, delegate
			{
				ScreenBuffer scrapBuffer = TextConsole.GetScrapBuffer1(false);
				zone.Render(scrapBuffer);
				XRLCore.ParticleManager.Frame();
				XRLCore.ParticleManager.Render(scrapBuffer);
				if (hide)
				{
					foreach (EjectionSeat ejectionSeat in Seats)
					{
						ejectionSeat.HidePlayer(scrapBuffer);
					}
				}
				scrapBuffer.Draw();
			}, 10000, true);
		}

		// Token: 0x0600BCDB RID: 48347 RVA: 0x00451FE8 File Offset: 0x004501E8
		public bool Eject(bool AllInZone = false)
		{
			InteriorZone interiorZone = this.ParentObject.CurrentZone as InteriorZone;
			if (interiorZone != null && interiorZone.ParentZone.IsWorldMap())
			{
				return false;
			}
			List<EjectionSeat> list;
			List<GameObject> list2;
			if (!this.TryGetEjectionSeats(AllInZone, out list, out list2))
			{
				return false;
			}
			List<Cell> targetsFor = this.GetTargetsFor(list, list2);
			for (int i = list.Count - 1; i >= 0; i--)
			{
				if (targetsFor[i] == null || list[i].IsDisabled(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
				{
					list.RemoveAt(i);
					list2.RemoveAt(i);
					targetsFor.RemoveAt(i);
				}
				else if (!list[i].WithinInterior)
				{
					EjectionSeat.Blast(list[i].currentCell, 7, 25);
					list[i].PlayWorldSound(this.Sound, 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
				}
			}
			if (list.IsNullOrEmpty<EjectionSeat>())
			{
				return false;
			}
			GameObject ejectionSource = this.GetEjectionSource();
			if (this.WithinInterior)
			{
				Cell ejectionOrigin = this.GetEjectionOrigin();
				EjectionSeat.Blast(ejectionOrigin, 7, 25);
				ejectionOrigin.PlayWorldSound(this.Sound, 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			if (Options.UseOverlayCombatEffects)
			{
				this.PlayAnimation(list, list2, targetsFor);
			}
			for (int j = 0; j < list.Count; j++)
			{
				GameObject parentObject = list[j].ParentObject;
				parentObject.SystemMoveTo(targetsFor[j], null, false, true, false, false);
				parentObject.Physics.Takeable = true;
			}
			this.Message(ejectionSource, list2);
			EjectionSeat.Puff(targetsFor, 10, 12);
			The.Core.RenderBase(true, false);
			return true;
		}

		// Token: 0x0600BCDC RID: 48348 RVA: 0x004521C8 File Offset: 0x004503C8
		public void Message(GameObject Source, List<GameObject> Sitters)
		{
			bool flag = Sitters.Any<GameObject>((GameObject x) => x.IsPlayer());
			if (Source.IsVisible() || flag)
			{
				foreach (GameObject gameObject in Sitters)
				{
					gameObject.Physics.DidXToY("eject", (Source == this.ParentObject) ? "with" : "from", Source, null, "!", null, null, null, null, false, false, false, false, false, null, null, Source, !flag, false, true, false, false, null);
				}
			}
		}

		// Token: 0x0600BCDD RID: 48349 RVA: 0x00452280 File Offset: 0x00450480
		public static void Blast(Cell Cell, int Count = 7, int Life = 25)
		{
			EjectionSeat.Blast(Cell.X, Cell.Y, Count, Life);
		}

		// Token: 0x0600BCDE RID: 48350 RVA: 0x00452298 File Offset: 0x00450498
		public static void Blast(int X, int Y, int Count = 7, int Life = 25)
		{
			string[] array = new string[] { "&r", "&R", "&W" };
			for (int i = 0; i < Count; i++)
			{
				float num = (float)Stat.RandomCosmetic(0, 359) / 58f;
				float num2 = (float)Math.Sin((double)num) / ((float)Life / 3f);
				float num3 = (float)Math.Cos((double)num) / ((float)Life / 3f);
				string text = array.GetRandomElementCosmetic<string>() + ((char)(219 + Stat.RandomCosmetic(0, 4))).ToString();
				XRLCore.ParticleManager.Add(text, (float)X, (float)Y, num2, num3, Life, 0f, 0f, 0L);
			}
		}

		// Token: 0x0600BCDF RID: 48351 RVA: 0x00452348 File Offset: 0x00450548
		public static void Puff(IEnumerable<Cell> Cells, int Count = 10, int Life = 12)
		{
			foreach (Cell cell in Cells)
			{
				EjectionSeat.Puff(cell, Count, Life);
			}
		}

		// Token: 0x0600BCE0 RID: 48352 RVA: 0x00452390 File Offset: 0x00450590
		public static void Puff(Cell Cell, int Count = 10, int Life = 12)
		{
			if (!Cell.IsVisible())
			{
				return;
			}
			EjectionSeat.Puff(Cell.X, Cell.Y, Count, Life);
		}

		// Token: 0x0600BCE1 RID: 48353 RVA: 0x004523B0 File Offset: 0x004505B0
		public static void Puff(int X, int Y, int Count = 10, int Life = 12)
		{
			for (int i = 0; i < Count; i++)
			{
				float num = (float)Stat.Rnd2.Next(360) * 0.017453292f;
				float num2 = Mathf.Sin(num) / ((float)Life / 3f);
				float num3 = Mathf.Cos(num) / ((float)Life / 3f);
				string text = ((Stat.RandomCosmetic(1, 4) <= 3) ? "&y." : "&y±");
				XRLCore.ParticleManager.Add(text, (float)X, (float)Y, num2, num3, Life, 0f, 0f, 0L);
			}
		}

		// Token: 0x0600BCE2 RID: 48354 RVA: 0x00452433 File Offset: 0x00450633
		public bool IsTertiary(GameObject Object)
		{
			return !Object.IsPlayer() && Object != this.ParentObject;
		}

		// Token: 0x0600BCE3 RID: 48355 RVA: 0x0045244C File Offset: 0x0045064C
		public void HidePlayer(ScreenBuffer SB)
		{
			Cell currentCell = this.ParentObject.CurrentCell;
			GameObject highestRenderLayerObject = currentCell.GetHighestRenderLayerObject(new Predicate<GameObject>(this.IsTertiary));
			IRenderable renderable = ((highestRenderLayerObject != null) ? highestRenderLayerObject.Render : null);
			if (renderable == null)
			{
				renderable = currentCell;
			}
			SB.Goto(currentCell.X, currentCell.Y);
			SB.Write(renderable);
		}

		// Token: 0x04004BB0 RID: 19376
		public const string ABL_CMD = "CommandSeatEject";

		// Token: 0x04004BB1 RID: 19377
		public const string INV_CMD = "CommandSingleSeatEject";

		// Token: 0x04004BB2 RID: 19378
		public string Sound;

		// Token: 0x04004BB3 RID: 19379
		public int MinRange = 3;

		// Token: 0x04004BB4 RID: 19380
		public int MaxRange = 8;
	}
}
