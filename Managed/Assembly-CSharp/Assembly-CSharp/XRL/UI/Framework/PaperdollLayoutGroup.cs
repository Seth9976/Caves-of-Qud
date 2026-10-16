using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Genkit;
using UnityEngine;
using UnityEngine.UI;
using XRL.World.Anatomy;

namespace XRL.UI.Framework
{
	// Token: 0x020014EF RID: 5359
	public class PaperdollLayoutGroup : LayoutGroup, ILayoutElement
	{
		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x0600D14D RID: 53581 RVA: 0x004DAE2E File Offset: 0x004D902E
		// (set) Token: 0x0600D14E RID: 53582 RVA: 0x004DAE36 File Offset: 0x004D9036
		public PaperdollLayoutGroup.Corner startCorner
		{
			get
			{
				return this.m_StartCorner;
			}
			set
			{
				base.SetProperty<PaperdollLayoutGroup.Corner>(ref this.m_StartCorner, value);
			}
		}

		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x0600D14F RID: 53583 RVA: 0x004DAE45 File Offset: 0x004D9045
		// (set) Token: 0x0600D150 RID: 53584 RVA: 0x004DAE4D File Offset: 0x004D904D
		public PaperdollLayoutGroup.Axis startAxis
		{
			get
			{
				return this.m_StartAxis;
			}
			set
			{
				base.SetProperty<PaperdollLayoutGroup.Axis>(ref this.m_StartAxis, value);
			}
		}

		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x0600D151 RID: 53585 RVA: 0x004DAE5C File Offset: 0x004D905C
		// (set) Token: 0x0600D152 RID: 53586 RVA: 0x004DAE64 File Offset: 0x004D9064
		public Vector2 cellSize
		{
			get
			{
				return this.m_CellSize;
			}
			set
			{
				base.SetProperty<Vector2>(ref this.m_CellSize, value);
			}
		}

		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x0600D153 RID: 53587 RVA: 0x004DAE73 File Offset: 0x004D9073
		// (set) Token: 0x0600D154 RID: 53588 RVA: 0x004DAE7B File Offset: 0x004D907B
		public Vector2 spacing
		{
			get
			{
				return this.m_Spacing;
			}
			set
			{
				base.SetProperty<Vector2>(ref this.m_Spacing, value);
			}
		}

		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x0600D155 RID: 53589 RVA: 0x004DAE8A File Offset: 0x004D908A
		// (set) Token: 0x0600D156 RID: 53590 RVA: 0x004DAE92 File Offset: 0x004D9092
		public int constraintCount
		{
			get
			{
				return this.m_ConstraintCount;
			}
			set
			{
				base.SetProperty<int>(ref this.m_ConstraintCount, value);
			}
		}

		// Token: 0x0600D157 RID: 53591 RVA: 0x004DAEA4 File Offset: 0x004D90A4
		protected PaperdollLayoutGroup()
		{
		}

		// Token: 0x0600D158 RID: 53592 RVA: 0x004DAF24 File Offset: 0x004D9124
		public int Leftmost()
		{
			int x;
			int num;
			for (x = -this.usedCellCountX; x <= this.usedCellCountX; x = num + 1)
			{
				int sy = this.usedCellCountY / 2;
				int y;
				Func<PaperdollLayoutElement, bool> <>9__0;
				Func<PaperdollLayoutElement, bool> <>9__1;
				for (y = 0; y < this.usedCellCountY; y = num + 1)
				{
					IEnumerable<PaperdollLayoutElement> enumerable = this.paperdollElements;
					Func<PaperdollLayoutElement, bool> func;
					if ((func = <>9__0) == null)
					{
						func = (<>9__0 = (PaperdollLayoutElement e) => e.gridLocation.Contains(new Point2D(x, sy - y)));
					}
					PaperdollLayoutElement paperdollLayoutElement = enumerable.FirstOrDefault<PaperdollLayoutElement>(func);
					if (paperdollLayoutElement != null)
					{
						return this.paperdollElements.IndexOf(paperdollLayoutElement);
					}
					IEnumerable<PaperdollLayoutElement> enumerable2 = this.paperdollElements;
					Func<PaperdollLayoutElement, bool> func2;
					if ((func2 = <>9__1) == null)
					{
						func2 = (<>9__1 = (PaperdollLayoutElement e) => e.gridLocation.Contains(new Point2D(x, sy + y)));
					}
					paperdollLayoutElement = enumerable2.FirstOrDefault<PaperdollLayoutElement>(func2);
					if (paperdollLayoutElement != null)
					{
						return this.paperdollElements.IndexOf(paperdollLayoutElement);
					}
					num = y;
				}
				num = x;
			}
			return 0;
		}

		// Token: 0x0600D159 RID: 53593 RVA: 0x004DB03C File Offset: 0x004D923C
		public int Rightmost()
		{
			int x;
			int num;
			for (x = this.usedCellCountX; x >= -this.usedCellCountX; x = num - 1)
			{
				int sy = this.usedCellCountY / 2;
				int y;
				Func<PaperdollLayoutElement, bool> <>9__0;
				Func<PaperdollLayoutElement, bool> <>9__1;
				for (y = 0; y < this.usedCellCountY; y = num + 1)
				{
					IEnumerable<PaperdollLayoutElement> enumerable = this.paperdollElements;
					Func<PaperdollLayoutElement, bool> func;
					if ((func = <>9__0) == null)
					{
						func = (<>9__0 = (PaperdollLayoutElement e) => e.gridLocation.Contains(new Point2D(x, sy - y)));
					}
					PaperdollLayoutElement paperdollLayoutElement = enumerable.FirstOrDefault<PaperdollLayoutElement>(func);
					if (paperdollLayoutElement != null)
					{
						return this.paperdollElements.IndexOf(paperdollLayoutElement);
					}
					IEnumerable<PaperdollLayoutElement> enumerable2 = this.paperdollElements;
					Func<PaperdollLayoutElement, bool> func2;
					if ((func2 = <>9__1) == null)
					{
						func2 = (<>9__1 = (PaperdollLayoutElement e) => e.gridLocation.Contains(new Point2D(x, sy + y)));
					}
					paperdollLayoutElement = enumerable2.FirstOrDefault<PaperdollLayoutElement>(func2);
					if (paperdollLayoutElement != null)
					{
						return this.paperdollElements.IndexOf(paperdollLayoutElement);
					}
					num = y;
				}
				num = x;
			}
			return 0;
		}

		// Token: 0x0600D15A RID: 53594 RVA: 0x004DB154 File Offset: 0x004D9354
		public int LeftOf(PaperdollLayoutElement element)
		{
			int w;
			int num;
			for (w = 0; w < this.usedCellCountY; w = num + 1)
			{
				int x;
				for (x = element.gridLocation.x1 - 1; x >= -this.usedCellCountX; x = num - 1)
				{
					int y;
					for (y = element.gridLocation.y1; y <= element.gridLocation.y2; y = num + 1)
					{
						PaperdollLayoutElement paperdollLayoutElement = this.paperdollElements.FirstOrDefault<PaperdollLayoutElement>((PaperdollLayoutElement e) => e.gridLocation.Contains(new Point2D(x, y - w)));
						if (paperdollLayoutElement == null)
						{
							paperdollLayoutElement = this.paperdollElements.FirstOrDefault<PaperdollLayoutElement>((PaperdollLayoutElement e) => e.gridLocation.Contains(new Point2D(x, y + w)));
						}
						if (paperdollLayoutElement != null)
						{
							return this.paperdollElements.IndexOf(paperdollLayoutElement);
						}
						num = y;
					}
					num = x;
				}
				num = w;
			}
			return -1;
		}

		// Token: 0x0600D15B RID: 53595 RVA: 0x004DB274 File Offset: 0x004D9474
		public int RightOf(PaperdollLayoutElement element)
		{
			int w;
			int num;
			for (w = 0; w < this.usedCellCountY; w = num + 1)
			{
				int x;
				for (x = element.gridLocation.x2 + 1; x <= this.usedCellCountX; x = num + 1)
				{
					int y;
					for (y = element.gridLocation.y1; y <= element.gridLocation.y2; y = num + 1)
					{
						PaperdollLayoutElement paperdollLayoutElement = this.paperdollElements.FirstOrDefault<PaperdollLayoutElement>((PaperdollLayoutElement e) => e.gridLocation.Contains(new Point2D(x, y - w)));
						if (paperdollLayoutElement == null)
						{
							paperdollLayoutElement = this.paperdollElements.FirstOrDefault<PaperdollLayoutElement>((PaperdollLayoutElement e) => e.gridLocation.Contains(new Point2D(x, y + w)));
						}
						if (paperdollLayoutElement != null)
						{
							return this.paperdollElements.IndexOf(paperdollLayoutElement);
						}
						num = y;
					}
					num = x;
				}
				num = w;
			}
			return -1;
		}

		// Token: 0x0600D15C RID: 53596 RVA: 0x004DB394 File Offset: 0x004D9594
		public int BelowOf(PaperdollLayoutElement element)
		{
			int w;
			int num;
			for (w = 0; w < this.usedCellCountX; w = num + 1)
			{
				int y;
				for (y = element.gridLocation.y2 + 1; y <= this.usedCellCountY; y = num + 1)
				{
					int x;
					for (x = element.gridLocation.x1; x <= element.gridLocation.x2; x = num + 1)
					{
						PaperdollLayoutElement paperdollLayoutElement = this.paperdollElements.FirstOrDefault<PaperdollLayoutElement>((PaperdollLayoutElement e) => e.gridLocation.Contains(new Point2D(x - w, y)));
						if (paperdollLayoutElement == null)
						{
							paperdollLayoutElement = this.paperdollElements.FirstOrDefault<PaperdollLayoutElement>((PaperdollLayoutElement e) => e.gridLocation.Contains(new Point2D(x + w, y)));
						}
						if (paperdollLayoutElement != null)
						{
							return this.paperdollElements.IndexOf(paperdollLayoutElement);
						}
						num = x;
					}
					num = y;
				}
				num = w;
			}
			return -1;
		}

		// Token: 0x0600D15D RID: 53597 RVA: 0x004DB4B4 File Offset: 0x004D96B4
		public int AboveOf(PaperdollLayoutElement element)
		{
			int w;
			int num;
			for (w = 0; w < this.usedCellCountX; w = num + 1)
			{
				int y;
				for (y = element.gridLocation.y1 - 1; y >= 0; y = num - 1)
				{
					int x;
					for (x = element.gridLocation.x1; x <= element.gridLocation.x2; x = num + 1)
					{
						PaperdollLayoutElement paperdollLayoutElement = this.paperdollElements.FirstOrDefault<PaperdollLayoutElement>((PaperdollLayoutElement e) => e.gridLocation.Contains(new Point2D(x - w, y)));
						if (paperdollLayoutElement == null)
						{
							paperdollLayoutElement = this.paperdollElements.FirstOrDefault<PaperdollLayoutElement>((PaperdollLayoutElement e) => e.gridLocation.Contains(new Point2D(x + w, y)));
						}
						if (paperdollLayoutElement != null)
						{
							return this.paperdollElements.IndexOf(paperdollLayoutElement);
						}
						num = x;
					}
					num = y;
				}
				num = w;
			}
			return -1;
		}

		// Token: 0x0600D15E RID: 53598 RVA: 0x004DB5CE File Offset: 0x004D97CE
		public void Setup(List<PaperdollLayoutElement> inboundPaperdollElements)
		{
			this.paperdollElements.Clear();
			this.paperdollElements.AddRange(inboundPaperdollElements);
			this.lastPaperdollLayoutColumnsUsed = -1;
			this.LayoutBodyPlan();
		}

		// Token: 0x0600D15F RID: 53599 RVA: 0x004DB5F4 File Offset: 0x004D97F4
		public void LayoutBodyPlan()
		{
			this.ConnectorsDirty = true;
			this.lastHandLayedOut.Clear();
			this.lastPaperdollLayoutColumnsUsed = this.cellCountX;
			List<PaperdollLayoutElement> list = this.paperdollElements.Where<PaperdollLayoutElement>((PaperdollLayoutElement e) => !this.paperdollElements.Any<PaperdollLayoutElement>((PaperdollLayoutElement el) => el.data.bodyPart.Parts != null && el.data.bodyPart.Parts.Contains(e.data.bodyPart))).ToList<PaperdollLayoutElement>();
			list.Where<PaperdollLayoutElement>((PaperdollLayoutElement b) => b.data.bodyPart.Type == "Body").ToList<PaperdollLayoutElement>();
			list.Where<PaperdollLayoutElement>((PaperdollLayoutElement b) => b.data.bodyPart.Type != "Body").ToList<PaperdollLayoutElement>();
			this.grid.Clear();
			this.usedGrids.Clear();
			this.abstractSlots.Clear();
			foreach (PaperdollLayoutElement paperdollLayoutElement in list)
			{
				int num = Math.Max(0, Math.Max(paperdollLayoutElement.data.bodyPart.GetPartCount((BodyPart p) => p.Type == "Arm" && (p.Laterality & 1) != 0) - 1, paperdollLayoutElement.data.bodyPart.GetPartCount((BodyPart p) => p.Type == "Arm" && (p.Laterality & 2) != 0) - 1));
				if (paperdollLayoutElement.data.bodyPart.GetPartCount("Arm") == 0)
				{
					num = Math.Max(0, paperdollLayoutElement.data.bodyPart.GetPartCount((BodyPart p) => p.Type == "Hand") / 2 - 1);
				}
				if (num == 0 && paperdollLayoutElement.data.bodyPart.GetPartCount("Hand") == 0)
				{
					num = Math.Max(0, paperdollLayoutElement.data.bodyPart.GetPartCount((BodyPart p) => p.Type == "Foot") / 2 - 1);
				}
				int num2 = Math.Max(0, Math.Max(paperdollLayoutElement.data.bodyPart.GetPartCount((BodyPart p) => p.Type == "Head") - 1, paperdollLayoutElement.data.bodyPart.GetPartCount((BodyPart p) => p.Type == "Feet") - 1));
				for (int i = 0; i <= num2; i++)
				{
					for (int j = 0; j <= num; j++)
					{
						this.grid[new ValueTuple<int, int>(i, j)] = paperdollLayoutElement;
					}
				}
				paperdollLayoutElement.gridLocation = new Rect2D(0, 0, num2, num);
				this.usedGrids.Add(paperdollLayoutElement.gridLocation);
				if (paperdollLayoutElement.data.bodyPart.Parts != null)
				{
					using (List<BodyPart>.Enumerator enumerator2 = paperdollLayoutElement.data.bodyPart.Parts.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							BodyPart part = enumerator2.Current;
							PaperdollLayoutElement paperdollLayoutElement2 = this.paperdollElements.Where<PaperdollLayoutElement>((PaperdollLayoutElement el) => el.data.bodyPart == part).FirstOrDefault<PaperdollLayoutElement>();
							if (paperdollLayoutElement2 == null)
							{
								MetricsManager.LogError(string.Format("No layout element for {0}", part));
							}
							else
							{
								this.LayoutBodyPart(paperdollLayoutElement2, paperdollLayoutElement);
							}
						}
					}
				}
			}
			int num3 = int.MaxValue;
			int num4 = int.MaxValue;
			int num5 = int.MinValue;
			int num6 = int.MinValue;
			foreach (Rect2D rect2D in this.usedGrids)
			{
				if (rect2D.x1 < num3)
				{
					num3 = rect2D.x1;
				}
				if (rect2D.y1 < num4)
				{
					num4 = rect2D.y1;
				}
				if (rect2D.x2 > num5)
				{
					num5 = rect2D.x2;
				}
				if (rect2D.y2 > num6)
				{
					num6 = rect2D.y2;
				}
			}
			using (IEnumerator<PaperdollLayoutElement> enumerator4 = this.abstractSlots.Where<PaperdollLayoutElement>((PaperdollLayoutElement s) => s.data.bodyPart.Type.Contains("Thrown")).GetEnumerator())
			{
				IL_04CF:
				while (enumerator4.MoveNext())
				{
					PaperdollLayoutElement paperdollLayoutElement3 = enumerator4.Current;
					for (int k = num6; k >= 0; k--)
					{
						for (int l = num3; l <= num5; l++)
						{
							if (!this.grid.ContainsKey(new ValueTuple<int, int>(l, k)))
							{
								paperdollLayoutElement3.connectorDirection = ".";
								paperdollLayoutElement3.clearConnector();
								this.grid[new ValueTuple<int, int>(l, k)] = paperdollLayoutElement3;
								paperdollLayoutElement3.gridLocation = new Rect2D(l, k, l, k);
								this.usedGrids.Add(paperdollLayoutElement3.gridLocation);
								goto IL_04CF;
							}
						}
					}
				}
			}
			using (IEnumerator<PaperdollLayoutElement> enumerator4 = this.abstractSlots.Where<PaperdollLayoutElement>((PaperdollLayoutElement s) => s.data.bodyPart.Contact && !s.data.bodyPart.Type.Contains("Thrown")).GetEnumerator())
			{
				IL_05B4:
				while (enumerator4.MoveNext())
				{
					PaperdollLayoutElement paperdollLayoutElement4 = enumerator4.Current;
					for (int m = num6; m >= 0; m--)
					{
						for (int n = num5; n >= num3 / 2; n--)
						{
							if (!this.grid.ContainsKey(new ValueTuple<int, int>(n, m)))
							{
								paperdollLayoutElement4.connectorDirection = ".";
								paperdollLayoutElement4.clearConnector();
								this.grid[new ValueTuple<int, int>(n, m)] = paperdollLayoutElement4;
								paperdollLayoutElement4.gridLocation = new Rect2D(n, m, n, m);
								this.usedGrids.Add(paperdollLayoutElement4.gridLocation);
								goto IL_05B4;
							}
						}
					}
				}
			}
			using (IEnumerator<PaperdollLayoutElement> enumerator4 = this.abstractSlots.Where<PaperdollLayoutElement>((PaperdollLayoutElement s) => !s.data.bodyPart.Contact && !s.data.bodyPart.Type.Contains("Thrown")).GetEnumerator())
			{
				IL_0697:
				while (enumerator4.MoveNext())
				{
					PaperdollLayoutElement paperdollLayoutElement5 = enumerator4.Current;
					for (int num7 = num4; num7 <= num6; num7++)
					{
						for (int num8 = num5; num8 >= num3; num8--)
						{
							if (!this.grid.ContainsKey(new ValueTuple<int, int>(num8, num7)))
							{
								paperdollLayoutElement5.connectorDirection = ".";
								paperdollLayoutElement5.clearConnector();
								this.grid[new ValueTuple<int, int>(num8, num7)] = paperdollLayoutElement5;
								paperdollLayoutElement5.gridLocation = new Rect2D(num8, num7, num8, num7);
								this.usedGrids.Add(paperdollLayoutElement5.gridLocation);
								goto IL_0697;
							}
						}
					}
				}
			}
			num3 = int.MaxValue;
			num4 = int.MaxValue;
			foreach (Rect2D rect2D2 in this.usedGrids)
			{
				if (rect2D2.x1 < num3)
				{
					num3 = rect2D2.x1;
				}
				if (rect2D2.y1 < num4)
				{
					num4 = rect2D2.y1;
				}
			}
			foreach (PaperdollLayoutElement paperdollLayoutElement6 in this.paperdollElements)
			{
				PaperdollLayoutElement paperdollLayoutElement7 = paperdollLayoutElement6;
				paperdollLayoutElement7.gridLocation.y1 = paperdollLayoutElement7.gridLocation.y1 + -num4;
				PaperdollLayoutElement paperdollLayoutElement8 = paperdollLayoutElement6;
				paperdollLayoutElement8.gridLocation.y2 = paperdollLayoutElement8.gridLocation.y2 + -num4;
				if (paperdollLayoutElement6.gridLocation.Width - 1 > num5 - paperdollLayoutElement6.gridLocation.x1)
				{
					paperdollLayoutElement6.gridLocation.x2 = paperdollLayoutElement6.gridLocation.x1 + (num5 - paperdollLayoutElement6.gridLocation.x1);
				}
				paperdollLayoutElement6.setSize(paperdollLayoutElement6.gridLocation.Width - 1, paperdollLayoutElement6.gridLocation.Height - 1);
			}
		}

		// Token: 0x0600D160 RID: 53600 RVA: 0x004DBE9C File Offset: 0x004DA09C
		public bool IsAbstract(PaperdollLayoutElement part)
		{
			return part.data.bodyPart.Abstract;
		}

		// Token: 0x0600D161 RID: 53601 RVA: 0x004DBEB0 File Offset: 0x004DA0B0
		public Rect2D GetUnusedSlotInDirection(PaperdollLayoutElement part, List<string> preferredDirection)
		{
			int num = 0;
			for (;;)
			{
				for (int i = 1; i < this.gridWidth + num; i++)
				{
					foreach (string text in preferredDirection)
					{
						for (int j = part.gridLocation.x1; j <= part.gridLocation.x2; j++)
						{
							for (int k = part.gridLocation.y1; k <= part.gridLocation.y2; k++)
							{
								Vector2i vector2i = new Vector2i(j, k);
								for (int l = 0; l < i; l++)
								{
									if (text == "N")
									{
										vector2i.y--;
									}
									if (text == "S")
									{
										vector2i.y++;
									}
									if (text == "E")
									{
										vector2i.x++;
									}
									if (text == "W")
									{
										vector2i.x--;
									}
								}
								if (!this.grid.ContainsKey(new ValueTuple<int, int>(vector2i.x, vector2i.y)) && Math.Abs(vector2i.x) <= this.cellCountX / 2)
								{
									return new Rect2D(vector2i.x, vector2i.y, vector2i.x, vector2i.y);
								}
							}
						}
					}
				}
				if (preferredDirection.Count < 4)
				{
					break;
				}
				num++;
			}
			return this.GetUnusedSlotInDirection(part, new List<string> { "N", "S", "E", "W" });
		}

		// Token: 0x0600D162 RID: 53602 RVA: 0x004DC0B0 File Offset: 0x004DA2B0
		public void LayoutBodyPart(PaperdollLayoutElement part, PaperdollLayoutElement parent)
		{
			if (this.IsAbstract(part))
			{
				this.abstractSlots.Add(part);
				return;
			}
			if (part.data.bodyPart.Type == "Hand")
			{
				this.lastHandLayedOut.Push(part);
			}
			BodyPart bodyPart = part.data.bodyPart;
			List<string> list = null;
			if ((bodyPart.Laterality & 1) != 0)
			{
				list = new List<string> { "W", "N", "S", "E" };
			}
			if ((bodyPart.Laterality & 2) != 0)
			{
				list = new List<string> { "E", "N", "S", "W" };
			}
			if (list == null)
			{
				if (bodyPart.Type == "Head")
				{
					list = new List<string> { "N", "W", "E" };
				}
				if (bodyPart.Type == "Face")
				{
					list = new List<string> { "N", "W", "E", "S" };
				}
				if (bodyPart.Type == "Back")
				{
					list = new List<string> { "S", "W", "E" };
				}
				if (bodyPart.Type == "Arm")
				{
					list = new List<string> { "W", "E", "S", "N" };
				}
				if (bodyPart.Type == "Hand")
				{
					list = new List<string> { "W", "E", "S", "N" };
				}
				if (bodyPart.Type == "Foot")
				{
					list = new List<string> { "S" };
				}
				if (bodyPart.Type == "Hands")
				{
					list = new List<string> { "W", "N" };
				}
				if (bodyPart.Type == "Feet")
				{
					list = new List<string> { "S" };
				}
				if (bodyPart.Type == "Fungal Outcrop")
				{
					list = new List<string> { "S", "E", "W", "N" };
				}
				if (bodyPart.Type == "Icy Outcrop")
				{
					list = new List<string> { "S", "E", "W", "N" };
				}
				if (bodyPart.Type == "Fin")
				{
					list = new List<string> { "S", "E", "W", "N" };
				}
				if (bodyPart.Type == "Roots")
				{
					list = new List<string> { "S", "E", "W" };
				}
				if (bodyPart.Type == "Tail")
				{
					list = new List<string> { "S", "E", "W" };
				}
				if (bodyPart.Type == "Tread")
				{
					list = new List<string> { "S", "E", "W" };
				}
			}
			if (list == null)
			{
				list = new List<string> { "N", "W", "E", "S" };
			}
			PaperdollLayoutElement paperdollLayoutElement = parent;
			if (part.data.bodyPart.Type == "Hands" && this.lastHandLayedOut.Count > 0)
			{
				paperdollLayoutElement = this.lastHandLayedOut.Pop();
			}
			part.clearConnector();
			Rect2D unusedSlotInDirection = this.GetUnusedSlotInDirection(paperdollLayoutElement, list);
			part.connectorDirection = ".";
			if (unusedSlotInDirection.x1 > paperdollLayoutElement.gridLocation.x1)
			{
				part.connectorDirection = "L";
			}
			else if (unusedSlotInDirection.x1 < paperdollLayoutElement.gridLocation.x1)
			{
				part.connectorDirection = "R";
			}
			else if (unusedSlotInDirection.y1 < paperdollLayoutElement.gridLocation.y1)
			{
				part.connectorDirection = "U";
			}
			else if (unusedSlotInDirection.y1 > paperdollLayoutElement.gridLocation.y1)
			{
				part.connectorDirection = "D";
			}
			part.gridLocation = unusedSlotInDirection;
			this.usedGrids.Add(unusedSlotInDirection);
			this.grid[new ValueTuple<int, int>(unusedSlotInDirection.x1, unusedSlotInDirection.y1)] = part;
			if (bodyPart.Parts != null)
			{
				using (List<BodyPart>.Enumerator enumerator = bodyPart.Parts.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						BodyPart childPart = enumerator.Current;
						PaperdollLayoutElement paperdollLayoutElement2 = this.paperdollElements.Where<PaperdollLayoutElement>((PaperdollLayoutElement el) => el.data.bodyPart == childPart).FirstOrDefault<PaperdollLayoutElement>();
						if (paperdollLayoutElement2 == null)
						{
							MetricsManager.LogError(string.Format("No layout element for child {0}", childPart));
						}
						else
						{
							this.LayoutBodyPart(paperdollLayoutElement2, part);
						}
					}
				}
			}
		}

		// Token: 0x0600D163 RID: 53603 RVA: 0x004DC688 File Offset: 0x004DA888
		public override void CalculateLayoutInputHorizontal()
		{
			base.CalculateLayoutInputHorizontal();
			base.SetLayoutInputForAxis(0f, 0f, -1f, 0);
		}

		// Token: 0x0600D164 RID: 53604 RVA: 0x004DC6A8 File Offset: 0x004DA8A8
		public override void CalculateLayoutInputVertical()
		{
			float num = (float)base.padding.vertical + (this.cellSize.y + this.spacing.y) * (float)this.usedRows - this.spacing.y;
			base.SetLayoutInputForAxis(num, num, -1f, 1);
		}

		// Token: 0x0600D165 RID: 53605 RVA: 0x004DC6FC File Offset: 0x004DA8FC
		public override void SetLayoutHorizontal()
		{
			this.LayoutChildrenAlongAxis(0);
		}

		// Token: 0x0600D166 RID: 53606 RVA: 0x004DC705 File Offset: 0x004DA905
		public override void SetLayoutVertical()
		{
			this.LayoutChildrenAlongAxis(1);
		}

		// Token: 0x0600D167 RID: 53607 RVA: 0x004DC710 File Offset: 0x004DA910
		private void LayoutChildrenAlongAxis(int axis)
		{
			if (axis == 0)
			{
				for (int i = 0; i < base.rectChildren.Count; i++)
				{
					RectTransform rectTransform = base.rectChildren[i];
					this.m_Tracker.Add(this, rectTransform, DrivenTransformProperties.AnchoredPositionX | DrivenTransformProperties.AnchoredPositionY | DrivenTransformProperties.AnchorMinX | DrivenTransformProperties.AnchorMinY | DrivenTransformProperties.AnchorMaxX | DrivenTransformProperties.AnchorMaxY | DrivenTransformProperties.SizeDeltaX | DrivenTransformProperties.SizeDeltaY);
					rectTransform.anchorMin = Vector2.up;
					rectTransform.anchorMax = Vector2.up;
					rectTransform.sizeDelta = this.cellSize;
				}
				return;
			}
			float x = base.rectTransform.rect.size.x;
			float y = base.rectTransform.rect.size.y;
			this.cellCountX = 1;
			this.cellCountY = 1;
			this.cellCountX = Mathf.Max(1, Mathf.FloorToInt((x - (float)base.padding.horizontal + this.spacing.x + 0.001f) / (this.cellSize.x + this.spacing.x)));
			this.cellCountY = Mathf.Max(1, Mathf.FloorToInt((y - (float)base.padding.vertical + this.spacing.y + 0.001f) / (this.cellSize.y + this.spacing.y)));
			PaperdollLayoutGroup.Corner corner = this.startCorner % PaperdollLayoutGroup.Corner.LowerLeft;
			PaperdollLayoutGroup.Corner corner2 = this.startCorner / PaperdollLayoutGroup.Corner.LowerLeft;
			if (this.startAxis == PaperdollLayoutGroup.Axis.Horizontal)
			{
				int num = this.cellCountX;
				this.usedCellCountX = Mathf.Clamp(this.cellCountX, 1, base.rectChildren.Count);
			}
			else
			{
				int num = this.cellCountY;
				this.usedCellCountX = Mathf.Clamp(this.cellCountX, 1, Mathf.CeilToInt((float)base.rectChildren.Count / (float)num));
			}
			this.usedCellCountX = this.cellCountX;
			if (this.cellCountX != this.lastPaperdollLayoutColumnsUsed)
			{
				this.LayoutBodyPlan();
			}
			Vector2 vector = new Vector2((float)this.usedCellCountX * this.cellSize.x + (float)(this.usedCellCountX - 1) * this.spacing.x, (float)this.usedRows * this.cellSize.y + (float)(this.usedRows - 1) * this.spacing.y);
			Vector2 vector2 = new Vector2(base.GetStartOffset(0, vector.x), base.GetStartOffset(1, vector.y));
			vector2 = new Vector2(base.rectTransform.rect.width / 2f, 0f);
			this.usedRows = 0;
			this.usedRows = 0;
			float num2 = 0f;
			for (int j = 0; j < base.rectChildren.Count; j++)
			{
				PaperdollLayoutElement component = base.rectChildren[j].GetComponent<PaperdollLayoutElement>();
				float num3 = ((float)component.gridLocation.x1 + (float)component.gridLocation.x2) / 2f - 0.5f;
				float num4 = ((float)component.gridLocation.y1 + (float)component.gridLocation.y2) / 2f;
				if (num4 > num2)
				{
					num2 = num4;
				}
				base.SetChildAlongAxis(base.rectChildren[j], 0, vector2.x + (this.cellSize[0] + this.spacing[0]) * num3, this.cellSize[0]);
				base.SetChildAlongAxis(base.rectChildren[j], 1, vector2.y + (this.cellSize[1] + this.spacing[1]) * num4, this.cellSize[1]);
				if (component.gridLocation.y2 + 1 > this.usedRows)
				{
					this.usedRows = component.gridLocation.y2 + 1;
				}
				PaperdollLayoutElement component2 = base.rectChildren[j].GetComponent<PaperdollLayoutElement>();
				if (component2 != null && this.ConnectorsDirty)
				{
					component2.clearConnector();
					component2.connectorImage.transform.SetParent(base.gameObject.transform, true);
					component2.connectorImage.SetAsFirstSibling();
				}
			}
			if (this.usedCellCountY == this.usedRows)
			{
				this.ConnectorsDirty = false;
			}
			this.usedCellCountY = this.usedRows;
			Vector2 vector3 = new Vector2(base.rectTransform.sizeDelta.x, (float)this.usedRows * this.cellSize.y + (float)base.padding.top + (float)base.padding.bottom);
			if (base.rectTransform.sizeDelta != vector3)
			{
				base.rectTransform.sizeDelta = vector3;
			}
		}

		// Token: 0x040052A0 RID: 21152
		[SerializeField]
		protected PaperdollLayoutGroup.Corner m_StartCorner;

		// Token: 0x040052A1 RID: 21153
		[SerializeField]
		protected PaperdollLayoutGroup.Axis m_StartAxis;

		// Token: 0x040052A2 RID: 21154
		[SerializeField]
		protected Vector2 m_CellSize = new Vector2(100f, 100f);

		// Token: 0x040052A3 RID: 21155
		[SerializeField]
		protected Vector2 m_Spacing = Vector2.zero;

		// Token: 0x040052A4 RID: 21156
		[SerializeField]
		protected int m_ConstraintCount = 2;

		// Token: 0x040052A5 RID: 21157
		private bool ConnectorsDirty;

		// Token: 0x040052A6 RID: 21158
		private List<PaperdollLayoutElement> paperdollElements = new List<PaperdollLayoutElement>();

		// Token: 0x040052A7 RID: 21159
		[TupleElementNames(new string[] { "x", "y" })]
		private Dictionary<ValueTuple<int, int>, PaperdollLayoutElement> grid = new Dictionary<ValueTuple<int, int>, PaperdollLayoutElement>();

		// Token: 0x040052A8 RID: 21160
		private List<Rect2D> usedGrids = new List<Rect2D>();

		// Token: 0x040052A9 RID: 21161
		private List<PaperdollLayoutElement> abstractSlots = new List<PaperdollLayoutElement>();

		// Token: 0x040052AA RID: 21162
		private Stack<PaperdollLayoutElement> lastHandLayedOut = new Stack<PaperdollLayoutElement>();

		// Token: 0x040052AB RID: 21163
		private int lastPaperdollLayoutColumnsUsed = -1;

		// Token: 0x040052AC RID: 21164
		private int gridWidth = 8;

		// Token: 0x040052AD RID: 21165
		public int usedRows;

		// Token: 0x040052AE RID: 21166
		private int cellCountX;

		// Token: 0x040052AF RID: 21167
		private int cellCountY;

		// Token: 0x040052B0 RID: 21168
		private int usedCellCountX;

		// Token: 0x040052B1 RID: 21169
		private int usedCellCountY;

		// Token: 0x02001DFD RID: 7677
		public enum Corner
		{
			// Token: 0x04007535 RID: 30005
			UpperLeft,
			// Token: 0x04007536 RID: 30006
			UpperRight,
			// Token: 0x04007537 RID: 30007
			LowerLeft,
			// Token: 0x04007538 RID: 30008
			LowerRight
		}

		// Token: 0x02001DFE RID: 7678
		public enum Axis
		{
			// Token: 0x0400753A RID: 30010
			Horizontal,
			// Token: 0x0400753B RID: 30011
			Vertical
		}
	}
}
