using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using Genkit;
using XRL.UI;
using XRL.World.AI.GoalHandlers;
using XRL.World.Capabilities;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x0200097E RID: 2430
	public class PointOfInterest
	{
		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x060066DD RID: 26333 RVA: 0x001FFB90 File Offset: 0x001FDD90
		// (set) Token: 0x060066DE RID: 26334 RVA: 0x001FFC10 File Offset: 0x001FDE10
		public string DisplayName
		{
			get
			{
				if (this._DisplayName == null)
				{
					if (this.Object != null)
					{
						this._DisplayName = this.Object.GetReferenceDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, false, false, null, false);
					}
					if (this._DisplayName == null && !this.Explanation.IsNullOrEmpty())
					{
						this._DisplayName = this.Explanation;
						this.Explanation = null;
					}
					if (this._DisplayName == null)
					{
						this._DisplayName = "?";
					}
				}
				return this._DisplayName;
			}
			set
			{
				this._DisplayName = value;
			}
		}

		// Token: 0x060066DF RID: 26335 RVA: 0x001FFC1C File Offset: 0x001FDE1C
		public string GetDisplayName(GameObject Observer)
		{
			PointOfInterest.DisplayItems.Clear();
			string text = this.DisplayName;
			if (!this.Explanation.IsNullOrEmpty())
			{
				PointOfInterest.DisplayItems.Add(this.Explanation);
			}
			string text2 = null;
			if (Observer != null)
			{
				if (this.IsAt(Observer))
				{
					text2 = "here";
				}
				else if (this.Location != null)
				{
					text2 = Observer.DescribeDirectionToward(this.Location, true, true);
				}
				else if (this.Object != null)
				{
					text2 = Observer.DescribeDirectionToward(this.Object, true, true);
				}
			}
			if (!text2.IsNullOrEmpty())
			{
				PointOfInterest.DisplayItems.Add(text2);
			}
			if (PointOfInterest.DisplayItems.Count > 0)
			{
				text = text + " [" + string.Join(", ", PointOfInterest.DisplayItems.ToArray()) + "]";
			}
			return text;
		}

		// Token: 0x060066E0 RID: 26336 RVA: 0x001FFCEC File Offset: 0x001FDEEC
		public string GetSentenceName(GameObject Observer)
		{
			string text = this.DisplayName;
			if (this.Object != null)
			{
				text = this.Object.the + text;
			}
			else if (!text.StartsWith("the ", StringComparison.InvariantCultureIgnoreCase) && !text.StartsWith("a ", StringComparison.InvariantCultureIgnoreCase) && !text.StartsWith("an ", StringComparison.InvariantCultureIgnoreCase))
			{
				text = "the " + text;
			}
			return text;
		}

		// Token: 0x060066E1 RID: 26337 RVA: 0x001FFD53 File Offset: 0x001FDF53
		public bool IsAt(GameObject Observer, int Radius)
		{
			return this.GetDistanceTo(Observer) <= Radius;
		}

		// Token: 0x060066E2 RID: 26338 RVA: 0x001FFD62 File Offset: 0x001FDF62
		public bool IsAt(GameObject Observer)
		{
			return this.IsAt(Observer, this.GetAppropriateRadius(Observer));
		}

		// Token: 0x060066E3 RID: 26339 RVA: 0x001FFD72 File Offset: 0x001FDF72
		public int GetAppropriateRadius(GameObject Observer)
		{
			if (this.Radius >= 0)
			{
				return this.Radius;
			}
			if (this.Object != null && (this.Object.IsCreature || this.Object.ConsiderSolidFor(Observer, false)))
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x060066E4 RID: 26340 RVA: 0x001FFDAB File Offset: 0x001FDFAB
		public int GetDistanceTo(GameObject Observer)
		{
			if (!(this.Location != null))
			{
				return Observer.DistanceTo(this.Object);
			}
			return Observer.DistanceTo(this.Location);
		}

		// Token: 0x060066E5 RID: 26341 RVA: 0x001FFDD4 File Offset: 0x001FDFD4
		public IRenderable GetIcon()
		{
			IRenderable renderable;
			if ((renderable = this.Icon) == null)
			{
				GameObject @object = this.Object;
				if (@object == null)
				{
					return null;
				}
				renderable = @object.RenderForUI(null, false);
			}
			return renderable;
		}

		// Token: 0x060066E6 RID: 26342 RVA: 0x001FFDF4 File Offset: 0x001FDFF4
		public bool NavigateTo(GameObject Observer)
		{
			if (!GameObject.Validate(ref Observer))
			{
				return false;
			}
			int appropriateRadius = this.GetAppropriateRadius(Observer);
			if (this.IsAt(Observer, appropriateRadius))
			{
				if (Observer.IsPlayer())
				{
					Popup.ShowFail(string.Concat(new string[]
					{
						"You are already ",
						this.Preposition,
						" ",
						this.GetSentenceName(Observer),
						"."
					}), true, true, true);
				}
				return false;
			}
			if (Observer.IsPlayer())
			{
				string text = ((appropriateRadius > 0) ? ("P" + appropriateRadius.ToString() + ":") : "M");
				if (this.Object != null)
				{
					AutoAct.Setting = text + this.Object.ID;
					Observer.ForfeitTurn(false);
				}
				else if (this.Location != null)
				{
					AutoAct.Setting = text + this.Location.X.ToString() + "," + this.Location.Y.ToString();
					Observer.ForfeitTurn(false);
				}
				else
				{
					Popup.ShowFail("Somehow there seems to be no location for " + this.GetSentenceName(Observer) + ".", true, true, true);
				}
			}
			else if (this.Location != null)
			{
				Brain brain = Observer.Brain;
				if (brain != null)
				{
					brain.PushGoal(new MoveTo(this.Location, true, false, appropriateRadius, false, false, false, -1, -1, 95));
				}
			}
			else if (this.Object != null)
			{
				Brain brain2 = Observer.Brain;
				if (brain2 != null)
				{
					brain2.PushGoal(new MoveTo(this.Object, true, false, appropriateRadius, false, false, false, -1, -1, 95));
				}
			}
			return true;
		}

		// Token: 0x060066E7 RID: 26343 RVA: 0x001FFF94 File Offset: 0x001FE194
		public static int Compare(PointOfInterest a, PointOfInterest b)
		{
			int num = a.Order.CompareTo(b.Order);
			if (num != 0)
			{
				return num;
			}
			int num2 = ColorUtility.CompareExceptFormattingAndCase(a.DisplayName, b.DisplayName);
			if (num2 != 0)
			{
				return num2;
			}
			return a.GetDistanceTo(The.Player).CompareTo(b.GetDistanceTo(The.Player));
		}

		// Token: 0x04002F64 RID: 12132
		public GameObject Object;

		// Token: 0x04002F65 RID: 12133
		public string _DisplayName;

		// Token: 0x04002F66 RID: 12134
		public string Explanation;

		// Token: 0x04002F67 RID: 12135
		public string Key;

		// Token: 0x04002F68 RID: 12136
		public string Preposition = "at";

		// Token: 0x04002F69 RID: 12137
		public int Radius;

		// Token: 0x04002F6A RID: 12138
		public int Order;

		// Token: 0x04002F6B RID: 12139
		public Location2D Location;

		// Token: 0x04002F6C RID: 12140
		public IRenderable Icon;

		// Token: 0x04002F6D RID: 12141
		private static List<string> DisplayItems = new List<string>();
	}
}
