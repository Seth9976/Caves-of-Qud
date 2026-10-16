using System;
using System.Collections.Generic;
using Genkit;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02000F93 RID: 3987
	[Serializable]
	public class ForcePylon : IPart
	{
		// Token: 0x06009E1F RID: 40479 RVA: 0x00393B60 File Offset: 0x00391D60
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.Blueprint);
			Writer.WriteOptimized(this.Walls);
			Writer.WriteOptimized(this.Range);
			Writer.Write(this.IsRealityDistortionBased);
			Writer.WriteOptimized(this.Count);
			if (this.Count > 0)
			{
				int i = 0;
				int num = this.Links.Length;
				while (i < num)
				{
					if (this.Links[i].Pylon != null)
					{
						Writer.WriteGameObject(this.Links[i].Pylon);
						if (!this.Links[i].Host)
						{
							Writer.WriteOptimized(-1);
						}
						else
						{
							GameObject[] line = this.Links[i].Line;
							int num2 = line.Length;
							Writer.WriteOptimized(num2);
							for (int j = 0; j < num2; j++)
							{
								Writer.WriteGameObject(line[j]);
							}
						}
					}
					i++;
				}
			}
		}

		// Token: 0x06009E20 RID: 40480 RVA: 0x00393C50 File Offset: 0x00391E50
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			this.Blueprint = Reader.ReadOptimizedString();
			this.Walls = Reader.ReadOptimizedInt32();
			this.Range = Reader.ReadOptimizedInt32();
			this.IsRealityDistortionBased = Reader.ReadBoolean();
			this.Count = Reader.ReadOptimizedInt32();
			if (this.Count > 0)
			{
				this.Links = new ForcePylon.Link[this.Walls];
				for (int i = 0; i < this.Count; i++)
				{
					this.Links[i] = default(ForcePylon.Link);
					this.Links[i].Pylon = Reader.ReadGameObject(null);
					int num = Reader.ReadOptimizedInt32();
					if (num > 0)
					{
						this.Links[i].Host = true;
						GameObject[] array = (this.Links[i].Line = new GameObject[num]);
						for (int j = 0; j < num; j++)
						{
							array[j] = Reader.ReadGameObject(null);
						}
					}
				}
			}
		}

		// Token: 0x06009E21 RID: 40481 RVA: 0x00393D4C File Offset: 0x00391F4C
		public bool IsValidTarget(GameObject Object)
		{
			return GameObject.Validate(Object) && Object.HasPart(typeof(ForcePylon)) && Object != this.ParentObject && this.ParentObject.InSameZone(Object) && this.ParentObject.DistanceTo(Object) <= this.Range;
		}

		// Token: 0x06009E22 RID: 40482 RVA: 0x00393DA3 File Offset: 0x00391FA3
		public bool IsValidField(ref GameObject Object)
		{
			GameObject gameObject = Object;
			if (((gameObject != null) ? gameObject.Physics : null) == null || Object.IsInGraveyard())
			{
				Object = null;
				return false;
			}
			return true;
		}

		// Token: 0x06009E23 RID: 40483 RVA: 0x00393DC4 File Offset: 0x00391FC4
		public bool IsConnected(GameObject Object)
		{
			if (this.Links == null)
			{
				return false;
			}
			int i = 0;
			int num = this.Links.Length;
			while (i < num)
			{
				if (Object == this.Links[i].Pylon)
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x06009E24 RID: 40484 RVA: 0x00393E08 File Offset: 0x00392008
		public bool TryConnectTo(GameObject Object)
		{
			if (this.Count >= this.Walls)
			{
				return false;
			}
			if (!this.IsValidTarget(Object) || this.IsConnected(Object) || !this.ParentObject.HasLOSTo(Object, true, false, false, true, null))
			{
				return false;
			}
			ForcePylon part = Object.GetPart<ForcePylon>();
			if (part == null || part.Count >= part.Walls)
			{
				return false;
			}
			if (this.IsRealityDistortionBased && !base.CheckMyRealityDistortionAdvisability(Object, null, this.ParentObject, null, this, null, 0))
			{
				return false;
			}
			if (this.IsRealityDistortionBased && this.ParentObject.HasRegisteredEvent("InitiateRealityDistortionLocal") && !this.ParentObject.FireEvent(Event.New("InitiateRealityDistortionLocal", "Object", this.ParentObject, "Mutation", this)))
			{
				return false;
			}
			this.AddLink(Object, true);
			part.AddLink(this.ParentObject, false);
			this.DesuspendLink(Object, null);
			return true;
		}

		// Token: 0x06009E25 RID: 40485 RVA: 0x00393EF0 File Offset: 0x003920F0
		public bool TryDisconnectFrom(GameObject Object)
		{
			int i = 0;
			int num = this.Links.Length;
			while (i < num)
			{
				if (this.Links[i].Pylon == Object)
				{
					GameObject[] line = this.Links[i].Line;
					if (line != null && this.Links[i].Host)
					{
						int j = 0;
						int num2 = line.Length;
						while (j < num2)
						{
							if (this.IsValidField(ref line[j]))
							{
								line[j].Obliterate(null, false, null);
								line[j] = null;
							}
							j++;
						}
					}
					this.Links[i].Pylon = null;
					this.Links[i].Host = false;
					this.Count--;
					ForcePylon part = Object.GetPart<ForcePylon>();
					if (part != null)
					{
						part.TryDisconnectFrom(this.ParentObject);
					}
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x06009E26 RID: 40486 RVA: 0x00393FD8 File Offset: 0x003921D8
		public void AddLink(GameObject Object, bool Host = false)
		{
			if (this.Links == null)
			{
				this.Links = new ForcePylon.Link[this.Walls];
			}
			int num = Array.FindIndex<ForcePylon.Link>(this.Links, 0, this.Walls, (ForcePylon.Link x) => x.Pylon == null);
			this.Links[num].Pylon = Object;
			this.Links[num].Host = Host;
			this.Count++;
			if (Host)
			{
				int num2 = this.Range * 2 - 1;
				ref GameObject[] ptr = ref this.Links[num].Line;
				GameObject[] array;
				if ((array = ptr) == null)
				{
					GameObject[] array2;
					ptr = (array2 = new GameObject[num2]);
					array = array2;
				}
				GameObject[] array3 = array;
				string primaryFaction = this.ParentObject.GetPrimaryFaction(false);
				bool flag = false;
				for (int i = 0; i < num2; i++)
				{
					array3[i] = GameObject.Create(this.Blueprint);
					Forcefield firstPartDescendedFrom = array3[i].GetFirstPartDescendedFrom<Forcefield>();
					if (firstPartDescendedFrom != null)
					{
						firstPartDescendedFrom.Creator = this.ParentObject;
						firstPartDescendedFrom.MovesWithOwner = true;
						firstPartDescendedFrom.RejectOwner = false;
						firstPartDescendedFrom.AddAllowPassage(primaryFaction);
					}
					SoundOnCreate part = array3[i].GetPart<SoundOnCreate>();
					if (part != null)
					{
						if (!flag)
						{
							this.ParentObject.PlayWorldSound(part.Sounds.GetRandomSubstring(',', false, null), 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
							flag = true;
						}
						array3[i].RemovePart(part);
					}
				}
			}
		}

		// Token: 0x06009E27 RID: 40487 RVA: 0x00394160 File Offset: 0x00392360
		public void SuspendLink(GameObject Object)
		{
			int i = 0;
			int num = this.Links.Length;
			while (i < num)
			{
				if (this.Links[i].Pylon == Object)
				{
					this.SuspendLink(Object, this.Links[i].Line);
					return;
				}
				i++;
			}
		}

		// Token: 0x06009E28 RID: 40488 RVA: 0x003941B0 File Offset: 0x003923B0
		public void SuspendLink(GameObject Object, GameObject[] Line)
		{
			int i = 0;
			int num = Line.Length;
			while (i < num)
			{
				if (this.IsValidField(ref Line[i]))
				{
					Line[i].RemoveFromContext(null);
				}
				i++;
			}
		}

		// Token: 0x06009E29 RID: 40489 RVA: 0x003941E8 File Offset: 0x003923E8
		public void DesuspendLink(GameObject Object, string Direction = null)
		{
			int i = 0;
			int num = this.Links.Length;
			while (i < num)
			{
				if (this.Links[i].Pylon == Object)
				{
					this.DesuspendLink(Object, this.Links[i].Line, Direction);
					return;
				}
				i++;
			}
		}

		// Token: 0x06009E2A RID: 40490 RVA: 0x00394238 File Offset: 0x00392438
		public void DesuspendLink(GameObject Object, GameObject[] Line, string Direction = null)
		{
			ForcePylon.DesuspendDepth++;
			Cell currentCell = this.ParentObject.CurrentCell;
			Cell currentCell2 = Object.CurrentCell;
			List<Location2D> cardinalLine = Location2D.GetCardinalLine(currentCell.X, currentCell.Y, currentCell2.X, currentCell2.Y, true);
			if (cardinalLine.Count <= 2)
			{
				return;
			}
			if (Direction == null)
			{
				Direction = Directions.GetRandomDirection();
			}
			Forcefield firstPartDescendedFrom = Line[0].GetFirstPartDescendedFrom<Forcefield>();
			int phase = Line[0].GetPhase();
			int num = Math.Min(Line.Length, cardinalLine.Count - 2);
			for (int i = 0; i < num; i++)
			{
				if (GameObject.Validate(ref Line[i]))
				{
					currentCell.ParentZone.GetCell(cardinalLine[i + 1]).AddObject(Line[i], false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				}
			}
			for (int j = 0; j < num; j++)
			{
				GameObject gameObject = Line[j];
				Cell cell = ((gameObject != null) ? gameObject.CurrentCell : null);
				if (cell != null)
				{
					for (int k = cell.Objects.Count - 1; k >= 0; k--)
					{
						GameObject gameObject2 = cell.Objects[k];
						if (gameObject2.Physics != null && gameObject2 != Line[j] && (gameObject2.Physics.Solid || gameObject2.IsCombatObject()) && (firstPartDescendedFrom == null || !firstPartDescendedFrom.CanPass(gameObject2)) && !gameObject2.HasPart(typeof(ForcePylon)) && !gameObject2.HasPart(typeof(Forcefield)) && !gameObject2.HasPart(typeof(HologramMaterial)) && gameObject2.PhaseMatches(phase))
						{
							gameObject2.Physics.Push(Direction, 5000, 4, false, true, null);
						}
					}
				}
			}
		}

		// Token: 0x06009E2B RID: 40491 RVA: 0x00394400 File Offset: 0x00392600
		public void FindPylons(Cell Origin)
		{
			foreach (Cell cell in Origin.IterateAdjacent(this.Range, false, true, true))
			{
				foreach (GameObject gameObject in cell.Objects)
				{
					if (this.TryConnectTo(gameObject))
					{
						return;
					}
				}
			}
		}

		// Token: 0x06009E2C RID: 40492 RVA: 0x00394480 File Offset: 0x00392680
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID || ID == OnDestroyObjectEvent.ID;
		}

		// Token: 0x06009E2D RID: 40493 RVA: 0x003944A0 File Offset: 0x003926A0
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (this.Count > 0)
			{
				int i = 0;
				int num = this.Links.Length;
				while (i < num)
				{
					if (this.Links[i].Pylon != null)
					{
						if (!this.IsValidTarget(this.Links[i].Pylon))
						{
							this.TryDisconnectFrom(this.Links[i].Pylon);
						}
						else if (this.Links[i].Host)
						{
							this.DesuspendLink(this.Links[i].Pylon, this.Links[i].Line, E.Direction);
						}
						else
						{
							this.Links[i].Pylon.GetPart<ForcePylon>().DesuspendLink(this.ParentObject, E.Direction);
						}
					}
					i++;
				}
			}
			if (this.Count < this.Walls)
			{
				this.FindPylons(E.Cell);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009E2E RID: 40494 RVA: 0x003945A8 File Offset: 0x003927A8
		public override bool HandleEvent(OnDestroyObjectEvent E)
		{
			int i = 0;
			int num = this.Links.Length;
			while (i < num)
			{
				if (this.Links[i].Pylon != null)
				{
					this.TryDisconnectFrom(this.Links[i].Pylon);
				}
				i++;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009E2F RID: 40495 RVA: 0x003945FC File Offset: 0x003927FC
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009E30 RID: 40496 RVA: 0x003945FF File Offset: 0x003927FF
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginMove");
		}

		// Token: 0x06009E31 RID: 40497 RVA: 0x0039460C File Offset: 0x0039280C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeginMove" && this.Count > 0)
			{
				int i = 0;
				int num = this.Links.Length;
				while (i < num)
				{
					if (this.Links[i].Pylon != null)
					{
						if (!this.IsValidTarget(this.Links[i].Pylon))
						{
							this.TryDisconnectFrom(this.Links[i].Pylon);
						}
						else if (this.Links[i].Host)
						{
							this.SuspendLink(this.Links[i].Pylon, this.Links[i].Line);
						}
						else
						{
							this.Links[i].Pylon.GetPart<ForcePylon>().SuspendLink(this.ParentObject);
						}
					}
					i++;
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004165 RID: 16741
		public string Blueprint = "Forcefield";

		// Token: 0x04004166 RID: 16742
		public int Walls;

		// Token: 0x04004167 RID: 16743
		public int Range;

		// Token: 0x04004168 RID: 16744
		public bool IsRealityDistortionBased = true;

		// Token: 0x04004169 RID: 16745
		[NonSerialized]
		public ForcePylon.Link[] Links;

		// Token: 0x0400416A RID: 16746
		[NonSerialized]
		public int Count;

		// Token: 0x0400416B RID: 16747
		[NonSerialized]
		private static int DesuspendDepth;

		// Token: 0x02001C9A RID: 7322
		public struct Link
		{
			// Token: 0x04007099 RID: 28825
			public GameObject Pylon;

			// Token: 0x0400709A RID: 28826
			public GameObject[] Line;

			// Token: 0x0400709B RID: 28827
			public bool Host;
		}
	}
}
