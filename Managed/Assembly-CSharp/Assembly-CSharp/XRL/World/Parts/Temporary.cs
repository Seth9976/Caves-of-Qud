using System;
using XRL.Collections;

namespace XRL.World.Parts
{
	// Token: 0x02001203 RID: 4611
	[Serializable]
	public class Temporary : IPart
	{
		// Token: 0x0600B75E RID: 46942 RVA: 0x0042BF01 File Offset: 0x0042A101
		public Temporary()
		{
		}

		// Token: 0x0600B75F RID: 46943 RVA: 0x0042BF20 File Offset: 0x0042A120
		public Temporary(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x0600B760 RID: 46944 RVA: 0x0042BF2F File Offset: 0x0042A12F
		public Temporary(int Duration, string TurnInto)
			: this(Duration)
		{
			this.TurnInto = TurnInto;
		}

		// Token: 0x0600B761 RID: 46945 RVA: 0x0042BF3F File Offset: 0x0042A13F
		public Temporary(Temporary src)
			: this(src.Duration, src.TurnInto)
		{
		}

		// Token: 0x0600B762 RID: 46946 RVA: 0x0042BF54 File Offset: 0x0042A154
		public override bool SameAs(IPart p)
		{
			Temporary temporary = p as Temporary;
			return temporary.Duration == this.Duration && !(temporary.TurnInto != this.TurnInto) && base.SameAs(p);
		}

		// Token: 0x0600B763 RID: 46947 RVA: 0x0042BF94 File Offset: 0x0042A194
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AdjustValueEvent.ID || ID == AfterAfterThrownEvent.ID || ID == PooledEvent<CanBeTradedEvent>.ID || ID == PooledEvent<CheckExistenceSupportEvent>.ID || ID == DerivationCreatedEvent.ID || ID == SingletonEvent<GetDebugInternalsEvent>.ID || ID == PooledEvent<ReplaceInContextEvent>.ID || ID == WasDerivedFromEvent.ID || ID == ZoneActivatedEvent.ID || ID == ZoneThawedEvent.ID || (ID == PooledEvent<RealityStabilizeEvent>.ID && this.TurnInto == "*fugue");
		}

		// Token: 0x0600B764 RID: 46948 RVA: 0x0042C017 File Offset: 0x0042A217
		public override bool HandleEvent(WasDerivedFromEvent E)
		{
			E.ApplyToEach(delegate(GameObject obj)
			{
				obj.RemovePart<Temporary>();
			});
			E.ApplyToEach(delegate(GameObject obj)
			{
				obj.AddPart<Temporary>(new Temporary(this), true, false);
			});
			return base.HandleEvent(E);
		}

		// Token: 0x0600B765 RID: 46949 RVA: 0x0042C057 File Offset: 0x0042A257
		public override bool HandleEvent(DerivationCreatedEvent E)
		{
			if (!E.Original.HasPart<Temporary>())
			{
				E.Object.RemovePart(this);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B766 RID: 46950 RVA: 0x0042C079 File Offset: 0x0042A279
		public override bool HandleEvent(AfterAfterThrownEvent E)
		{
			if (this.TurnInto == "*fugue" && this.ParentObject.IsValid())
			{
				this.Expire(false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B767 RID: 46951 RVA: 0x0042C0A8 File Offset: 0x0042A2A8
		public override bool HandleEvent(CanBeTradedEvent E)
		{
			return false;
		}

		// Token: 0x0600B768 RID: 46952 RVA: 0x0042C0AB File Offset: 0x0042A2AB
		public override bool HandleEvent(AdjustValueEvent E)
		{
			E.AdjustValue(0.0);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B769 RID: 46953 RVA: 0x0042C0C4 File Offset: 0x0042A2C4
		public override bool HandleEvent(CheckExistenceSupportEvent E)
		{
			Temporary part = E.Object.GetPart<Temporary>();
			return (part == null || !(part.TurnInto == this.TurnInto)) && base.HandleEvent(E);
		}

		// Token: 0x0600B76A RID: 46954 RVA: 0x0042C0FC File Offset: 0x0042A2FC
		public override bool HandleEvent(RealityStabilizeEvent E)
		{
			if (this.TurnInto == "*fugue" && E.Check(true))
			{
				this.ParentObject.ParticleBlip("&K~", 10, 0L, false);
				if (base.Visible())
				{
					if (this.ParentObject.it == "it")
					{
						IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
						{
							this.ParentObject.Poss("worldline through spacetime", true),
							" snaps back to its canonical path, and ",
							this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
							this.ParentObject.GetVerb("vanish", true, false, false, true, false),
							"."
						}), null, true);
					}
					else
					{
						IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
						{
							this.ParentObject.Poss("worldline through spacetime", true),
							" snaps back to its canonical path, and ",
							this.ParentObject.it,
							this.ParentObject.GetVerb("vanish", true, true, false, true, false),
							"."
						}), null, true);
					}
				}
				this.Expire(true);
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B76B RID: 46955 RVA: 0x0042C24C File Offset: 0x0042A44C
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			int num = (int)Math.Max(The.Game.Turns - this.LastTurn, 0L);
			if (this.Duration > 0 && (this.Duration -= num) <= 0)
			{
				this.Expire(false);
			}
			this.LastTurn = The.Game.Turns;
			return base.HandleEvent(E);
		}

		// Token: 0x0600B76C RID: 46956 RVA: 0x0042C2B0 File Offset: 0x0042A4B0
		public override bool HandleEvent(ZoneThawedEvent E)
		{
			int num = (int)Math.Max(The.Game.Turns - this.LastTurn, 0L);
			if (this.Duration > 0 && (this.Duration -= num) <= 0)
			{
				this.Expire(false);
			}
			this.LastTurn = The.Game.Turns;
			return base.HandleEvent(E);
		}

		// Token: 0x0600B76D RID: 46957 RVA: 0x0042C312 File Offset: 0x0042A512
		public override bool HandleEvent(ReplaceInContextEvent E)
		{
			E.Replacement.RemovePart<Temporary>();
			E.Replacement.AddPart<Temporary>(new Temporary(this), true, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B76E RID: 46958 RVA: 0x0042C33B File Offset: 0x0042A53B
		public override bool HandleEvent(GetDebugInternalsEvent E)
		{
			E.AddEntry(this, "Duration", this.Duration);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B76F RID: 46959 RVA: 0x0042C356 File Offset: 0x0042A556
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600B770 RID: 46960 RVA: 0x0042C35C File Offset: 0x0042A55C
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (this.Duration > 0 && (this.Duration -= Amount) <= 0)
			{
				this.Expire(false);
			}
			this.LastTurn = TimeTick;
		}

		// Token: 0x0600B771 RID: 46961 RVA: 0x0042C394 File Offset: 0x0042A594
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B772 RID: 46962 RVA: 0x0042C397 File Offset: 0x0042A597
		public override void Initialize()
		{
			base.Initialize();
			this.ParentObject.ModIntProperty("WontSell", 1, true);
		}

		// Token: 0x0600B773 RID: 46963 RVA: 0x0042C3B2 File Offset: 0x0042A5B2
		public override void Attach()
		{
			this.ParentObject.Flags |= 8;
		}

		// Token: 0x0600B774 RID: 46964 RVA: 0x0042C3C7 File Offset: 0x0042A5C7
		public override void Remove()
		{
			this.ParentObject.ModIntProperty("WontSell", -1, true);
			if (!Temporary.CheckTemporary(this.ParentObject))
			{
				this.ParentObject.Flags &= -9;
			}
		}

		// Token: 0x0600B775 RID: 46965 RVA: 0x0042C3FD File Offset: 0x0042A5FD
		public bool TurnsIntoSomething()
		{
			return !this.TurnInto.IsNullOrEmpty() && this.TurnInto != "*fugue";
		}

		// Token: 0x0600B776 RID: 46966 RVA: 0x0042C420 File Offset: 0x0042A620
		public void Expire(bool Silent = false)
		{
			Cell cell = null;
			GameObject gameObject = null;
			if (this.TurnsIntoSomething())
			{
				cell = this.ParentObject.GetCurrentCell();
				GameObject gameObject2;
				if ((gameObject2 = this.ParentObject.InInventory) == null)
				{
					gameObject2 = this.ParentObject.Equipped ?? this.ParentObject.Implantee;
				}
				gameObject = gameObject2;
			}
			using (ScopeDisposedList<GameObject> fromPool = ScopeDisposedList<GameObject>.GetFromPool())
			{
				this.ParentObject.GetContents(fromPool);
				using (PooledContainer<GameObject>.Enumerator enumerator = fromPool.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Temporary temporary;
						if (enumerator.Current.TryGetPart<Temporary>(out temporary))
						{
							temporary.Expire(true);
						}
					}
				}
				this.ParentObject.RemoveContents(Silent, true);
				if (!Silent && !this.TurnsIntoSomething())
				{
					base.DidX("disappear", null, null, null, null, null, null, false, false, null, null, true, false, false, false, false, null);
				}
				this.ParentObject.Obliterate("Faded from existence.", true, null);
				this.ParentObject.RemoveFromContext(null);
				if (this.TurnsIntoSomething())
				{
					if (cell != null)
					{
						cell.AddObject(this.TurnInto, null, null, null, null);
					}
					else if (GameObject.Validate(ref gameObject))
					{
						gameObject.Inventory.AddObject(this.TurnInto, false, false);
					}
				}
			}
		}

		// Token: 0x0600B777 RID: 46967 RVA: 0x0042C574 File Offset: 0x0042A774
		public static void AddHierarchically(GameObject Object, int Duration = -1, string TurnInto = null, GameObject DependsOn = null, bool RootObjectValidateEveryTurn = false)
		{
			if (GameObject.Validate(ref Object))
			{
				MakeTemporaryEvent.Send(Object, Duration, TurnInto, DependsOn, RootObjectValidateEveryTurn);
			}
		}

		// Token: 0x0600B778 RID: 46968 RVA: 0x0042C58C File Offset: 0x0042A78C
		public static void CarryOver(GameObject src, GameObject dest, bool CanRemove = false)
		{
			Temporary temporary;
			if (src.TryGetPart<Temporary>(out temporary))
			{
				dest.RemovePart<Temporary>();
				dest.AddPart<Temporary>(new Temporary(temporary), true, false);
			}
			else if (CanRemove)
			{
				dest.RemovePart<Temporary>();
			}
			ExistenceSupport existenceSupport;
			if (src.TryGetPart<ExistenceSupport>(out existenceSupport))
			{
				dest.RemovePart<ExistenceSupport>();
				dest.AddPart<ExistenceSupport>(new ExistenceSupport(existenceSupport), true, false);
				return;
			}
			if (CanRemove)
			{
				dest.RemovePart<ExistenceSupport>();
			}
		}

		// Token: 0x0600B779 RID: 46969 RVA: 0x0042C5F0 File Offset: 0x0042A7F0
		public static bool IsTemporary(GameObject Object)
		{
			return (Object.Flags & 8) != 0;
		}

		// Token: 0x0600B77A RID: 46970 RVA: 0x0042C600 File Offset: 0x0042A800
		public static bool CheckTemporary(GameObject Object)
		{
			PartRack partsList = Object.PartsList;
			int i = 0;
			int count = partsList.Count;
			while (i < count)
			{
				Type type = partsList[i].GetType();
				if (type == typeof(Temporary) || type == typeof(ExistenceSupport))
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x0600B77B RID: 46971 RVA: 0x0042C651 File Offset: 0x0042A851
		public static bool IsNotTemporary(GameObject obj)
		{
			return !Temporary.IsTemporary(obj);
		}

		// Token: 0x040049C6 RID: 18886
		public int Duration = 12;

		// Token: 0x040049C7 RID: 18887
		public string TurnInto;

		// Token: 0x040049C8 RID: 18888
		public long LastTurn = long.MaxValue;
	}
}
