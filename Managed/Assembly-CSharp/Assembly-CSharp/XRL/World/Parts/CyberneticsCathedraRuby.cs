using System;
using System.Collections.Generic;
using System.Linq;
using Genkit;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	// Token: 0x02000F20 RID: 3872
	[Serializable]
	public class CyberneticsCathedraRuby : CyberneticsCathedra
	{
		// Token: 0x06009A6B RID: 39531 RVA: 0x003807E4 File Offset: 0x0037E9E4
		public override bool AllowStaticRegistration()
		{
			return false;
		}

		// Token: 0x06009A6C RID: 39532 RVA: 0x003807E8 File Offset: 0x0037E9E8
		public override void OnImplanted(GameObject Object)
		{
			base.OnImplanted(Object);
			Object.RegisterPartEvent(this, "BeginMove");
			Object.RegisterPartEvent(this, "MoveFailed");
			this.ActivatedAbilityID = Object.AddActivatedAbility("Pyrokinesis Field", "CommandActivateCathedra", "Cybernetics", null, "*", null, false, false, false, false, false, false, false, false, true, true, false, false, -1, "CommandCathedraRuby", null, null, null, null);
		}

		// Token: 0x06009A6D RID: 39533 RVA: 0x0038084C File Offset: 0x0037EA4C
		public override void OnUnimplanted(GameObject Object)
		{
			base.OnUnimplanted(Object);
			Object.UnregisterPartEvent(this, "BeginMove");
			Object.UnregisterPartEvent(this, "MoveFailed");
		}

		// Token: 0x06009A6E RID: 39534 RVA: 0x0038086D File Offset: 0x0037EA6D
		public override void CollectStats(Templates.StatCollector stats)
		{
			base.CollectStats(stats);
			Pyrokinesis.CollectProxyStats(stats, base.GetLevel(null));
		}

		// Token: 0x06009A6F RID: 39535 RVA: 0x00380883 File Offset: 0x0037EA83
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeginMove")
			{
				this.SuspendPyroField();
			}
			else if (E.ID == "MoveFailed")
			{
				this.DesuspendPyroField(null, false);
			}
			return base.FireEvent(E);
		}

		// Token: 0x06009A70 RID: 39536 RVA: 0x003808C0 File Offset: 0x0037EAC0
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			base.Write(Basis, Writer);
			Writer.Write(this.Field.Count);
			foreach (KeyValuePair<Point2D, GameObject> keyValuePair in this.Field)
			{
				Writer.Write(keyValuePair.Key.x);
				Writer.Write(keyValuePair.Key.y);
				Writer.WriteGameObject(keyValuePair.Value);
			}
		}

		// Token: 0x06009A71 RID: 39537 RVA: 0x00380958 File Offset: 0x0037EB58
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			base.Read(Basis, Reader);
			this.Field.Clear();
			int i = 0;
			int num = Reader.ReadInt32();
			while (i < num)
			{
				Point2D point2D = new Point2D(Reader.ReadInt32(), Reader.ReadInt32());
				this.Field[point2D] = Reader.ReadGameObject(null);
				i++;
			}
		}

		// Token: 0x06009A72 RID: 39538 RVA: 0x003809B4 File Offset: 0x0037EBB4
		public void Validate()
		{
			if (this.Field.Count == 0)
			{
				return;
			}
			this.RemovePoints.Clear();
			foreach (KeyValuePair<Point2D, GameObject> keyValuePair in this.Field)
			{
				if (keyValuePair.Value == null || keyValuePair.Value.IsInvalid())
				{
					this.RemovePoints.Add(keyValuePair.Key);
				}
			}
			foreach (Point2D point2D in this.RemovePoints)
			{
				this.Field.Remove(point2D);
			}
		}

		// Token: 0x06009A73 RID: 39539 RVA: 0x00380A8C File Offset: 0x0037EC8C
		public override void Activate(GameObject Actor)
		{
			Cell currentCell = Actor.CurrentCell;
			if (((currentCell != null) ? currentCell.ParentZone : null) == null || currentCell.ParentZone.IsWorldMap())
			{
				return;
			}
			List<Cell> list = currentCell.YieldAdjacentCells(2, false, false).ToList<Cell>();
			this.Duration = CyberneticsCathedraRuby.BASE_PYRO_FIELD_DURATION;
			foreach (GameObject gameObject in Pyrokinesis.Pyro(Actor, base.GetLevel(Actor), list, new int?(9999), 0, true))
			{
				Point2D point2D = gameObject.CurrentCell.PathDifferenceTo(currentCell);
				this.Field[point2D] = gameObject;
			}
			base.Activate(Actor);
		}

		// Token: 0x06009A74 RID: 39540 RVA: 0x00380B4C File Offset: 0x0037ED4C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<EndTurnEvent>.ID || ID == PooledEvent<BeforeTemperatureChangeEvent>.ID;
		}

		// Token: 0x06009A75 RID: 39541 RVA: 0x00380B6C File Offset: 0x0037ED6C
		public override bool HandleEvent(EndTurnEvent E)
		{
			if (this.Field.Count > 0)
			{
				int num = this.Duration - 1;
				this.Duration = num;
				if (num < 0)
				{
					this.DestroyPyroField();
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009A76 RID: 39542 RVA: 0x00380BA8 File Offset: 0x0037EDA8
		public override bool HandleEvent(BeforeTemperatureChangeEvent E)
		{
			return this.Duration < 0 && base.HandleEvent(E);
		}

		// Token: 0x06009A77 RID: 39543 RVA: 0x00380BBC File Offset: 0x0037EDBC
		public override bool HandleEvent(EnteredCellEvent E)
		{
			this.DesuspendPyroField(null, false);
			return base.HandleEvent(E);
		}

		// Token: 0x06009A78 RID: 39544 RVA: 0x00380BD0 File Offset: 0x0037EDD0
		public void DestroyPyroField()
		{
			this.Validate();
			foreach (KeyValuePair<Point2D, GameObject> keyValuePair in this.Field)
			{
				keyValuePair.Value.Obliterate(null, false, null);
			}
			this.Field.Clear();
		}

		// Token: 0x06009A79 RID: 39545 RVA: 0x00380C40 File Offset: 0x0037EE40
		public void SuspendPyroField()
		{
			this.Validate();
			foreach (KeyValuePair<Point2D, GameObject> keyValuePair in this.Field)
			{
				keyValuePair.Value.RemoveFromContext(null);
			}
		}

		// Token: 0x06009A7A RID: 39546 RVA: 0x00380CA0 File Offset: 0x0037EEA0
		public void DesuspendPyroField(GameObject Actor = null, bool Validated = false)
		{
			if (!Validated)
			{
				this.Validate();
			}
			GameObject gameObject = Actor ?? base.User;
			Cell cell = ((gameObject != null) ? gameObject.CurrentCell : null);
			Zone zone = ((cell != null) ? cell.ParentZone : null);
			if (zone == null || zone.IsWorldMap())
			{
				this.DestroyPyroField();
				return;
			}
			this.RemovePoints.Clear();
			foreach (KeyValuePair<Point2D, GameObject> keyValuePair in this.Field)
			{
				Point2D key = keyValuePair.Key;
				GameObject value = keyValuePair.Value;
				if (value.CurrentCell == null)
				{
					Cell cellGlobal = zone.GetCellGlobal(cell.X + key.x, cell.Y + key.y, false, true);
					if (cellGlobal == null)
					{
						value.Obliterate(null, false, null);
						this.RemovePoints.Add(key);
					}
					else
					{
						cellGlobal.AddObject(value, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						if (value.CurrentCell != cellGlobal)
						{
							value.Obliterate(null, false, null);
							this.RemovePoints.Add(key);
						}
					}
				}
			}
			foreach (Point2D point2D in this.RemovePoints)
			{
				this.Field.Remove(point2D);
			}
		}

		// Token: 0x0400403F RID: 16447
		public static readonly int BASE_PYRO_FIELD_DURATION = 3;

		// Token: 0x04004040 RID: 16448
		public int Duration = -1;

		// Token: 0x04004041 RID: 16449
		[NonSerialized]
		public Dictionary<Point2D, GameObject> Field = new Dictionary<Point2D, GameObject>();

		// Token: 0x04004042 RID: 16450
		[NonSerialized]
		public List<Point2D> RemovePoints = new List<Point2D>();
	}
}
