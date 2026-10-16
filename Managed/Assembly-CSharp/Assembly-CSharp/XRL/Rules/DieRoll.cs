using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XRL.World;

namespace XRL.Rules
{
	// Token: 0x020005DB RID: 1499
	[Serializable]
	public class DieRoll
	{
		// Token: 0x06004035 RID: 16437 RVA: 0x0013B31D File Offset: 0x0013951D
		public DieRoll()
		{
		}

		// Token: 0x06004036 RID: 16438 RVA: 0x0013B328 File Offset: 0x00139528
		public DieRoll(DieRoll source)
		{
			this.Type = source.Type;
			this.LeftValue = source.LeftValue;
			this.RightValue = source.RightValue;
			if (source.Left != null)
			{
				this.Left = new DieRoll(source.Left);
			}
			if (source.Right != null)
			{
				this.Right = new DieRoll(source.Right);
			}
			if (source.Set != null)
			{
				this.Set = new List<DieRoll>(source.Set);
			}
		}

		// Token: 0x06004037 RID: 16439 RVA: 0x0013B3AA File Offset: 0x001395AA
		public DieRoll(string Dice)
			: this()
		{
			this.Parse(Dice);
		}

		// Token: 0x06004038 RID: 16440 RVA: 0x0013B3B9 File Offset: 0x001395B9
		public DieRoll(string Dice, out bool AnyUnknown)
			: this()
		{
			this.Parse(Dice, out AnyUnknown);
		}

		// Token: 0x06004039 RID: 16441 RVA: 0x0013B3C9 File Offset: 0x001395C9
		public DieRoll(int Type, int Left, int Right)
			: this()
		{
			this.Type = Type;
			this.LeftValue = Left;
			this.RightValue = Right;
		}

		// Token: 0x0600403A RID: 16442 RVA: 0x0013B3E6 File Offset: 0x001395E6
		public DieRoll(int Type, DieRoll Left, DieRoll Right)
			: this()
		{
			this.Type = Type;
			this.Left = Left;
			this.Right = Right;
		}

		// Token: 0x0600403B RID: 16443 RVA: 0x0013B403 File Offset: 0x00139603
		public DieRoll(int Type, int Left, DieRoll Right)
			: this()
		{
			this.Type = Type;
			this.LeftValue = Left;
			this.Right = Right;
		}

		// Token: 0x0600403C RID: 16444 RVA: 0x0013B420 File Offset: 0x00139620
		public DieRoll(int Type, DieRoll Left, int Right)
			: this()
		{
			this.Type = Type;
			this.Left = Left;
			this.RightValue = Right;
		}

		// Token: 0x0600403D RID: 16445 RVA: 0x0013B43D File Offset: 0x0013963D
		public DieRoll(int Type, int Left, string Right)
			: this()
		{
			this.Type = Type;
			this.LeftValue = Left;
			this.Right = new DieRoll(Right);
		}

		// Token: 0x0600403E RID: 16446 RVA: 0x0013B45F File Offset: 0x0013965F
		public DieRoll(int Type, string Left, int Right)
			: this()
		{
			this.Type = Type;
			this.Left = new DieRoll(Left);
			this.RightValue = Right;
		}

		// Token: 0x0600403F RID: 16447 RVA: 0x0013B481 File Offset: 0x00139681
		public int Generate(int Low, int High)
		{
			return Stat.GlobalChannelRandom(this.Channel, Low, High);
		}

		// Token: 0x06004040 RID: 16448 RVA: 0x0013B490 File Offset: 0x00139690
		public int Resolve()
		{
			switch (this.Type)
			{
			case 0:
				return 0;
			case 1:
			{
				int num = ((this.Left == null) ? this.LeftValue : this.Left.Resolve());
				int num2 = ((this.Right == null) ? this.RightValue : this.Right.Resolve());
				int num3 = 0;
				for (int i = 0; i < num; i++)
				{
					num3 += this.Generate(1, num2);
				}
				return num3;
			}
			case 2:
				return this.Generate((this.Left == null) ? this.LeftValue : this.Left.Resolve(), (this.Right == null) ? this.RightValue : this.Right.Resolve());
			case 3:
				return this.LeftValue;
			case 4:
				return ((this.Left == null) ? this.LeftValue : this.Left.Resolve()) + ((this.Right == null) ? this.RightValue : this.Right.Resolve());
			case 5:
				return ((this.Left == null) ? this.LeftValue : this.Left.Resolve()) - ((this.Right == null) ? this.RightValue : this.Right.Resolve());
			case 6:
				return ((this.Left == null) ? this.LeftValue : this.Left.Resolve()) * ((this.Right == null) ? this.RightValue : this.Right.Resolve());
			case 7:
			{
				int num4 = ((this.Right == null) ? this.RightValue : this.Right.Resolve());
				if (num4 == 0)
				{
					return 0;
				}
				return ((this.Left == null) ? this.LeftValue : this.Left.Resolve()) / num4;
			}
			case 8:
				return -((this.Left == null) ? this.LeftValue : this.Left.Resolve());
			case 9:
				if (this.Set != null && this.Set.Count > 0)
				{
					if (this.Set.Count == 1)
					{
						return this.Set[0].Resolve();
					}
					int num5 = Stat.GlobalChannelRandom(this.Channel, 0, this.Set.Count - 1);
					return this.Set[num5].Resolve();
				}
				else if (this.Generate(0, 1) != 0)
				{
					if (this.Right != null)
					{
						return this.Right.Resolve();
					}
					return this.RightValue;
				}
				else
				{
					if (this.Left != null)
					{
						return this.Left.Resolve();
					}
					return this.LeftValue;
				}
				break;
			default:
				return this.LeftValue;
			}
		}

		// Token: 0x06004041 RID: 16449 RVA: 0x0013B725 File Offset: 0x00139925
		public int AverageRounded()
		{
			return (int)Math.Round(this.Average());
		}

		// Token: 0x06004042 RID: 16450 RVA: 0x0013B734 File Offset: 0x00139934
		public double Average()
		{
			switch (this.Type)
			{
			case 0:
				return 0.0;
			case 1:
			{
				double num = ((this.Left == null) ? ((double)this.LeftValue) : this.Left.Average());
				double num2 = ((this.Right == null) ? ((double)this.RightValue) : this.Right.Average());
				return (1.0 + num2) * num / 2.0;
			}
			case 2:
			{
				double num3 = ((this.Left == null) ? ((double)this.LeftValue) : this.Left.Average());
				double num4 = ((this.Right == null) ? ((double)this.RightValue) : this.Right.Average());
				return (num3 + num4) / 2.0;
			}
			case 3:
				return (double)this.LeftValue;
			case 4:
				return ((this.Left == null) ? ((double)this.LeftValue) : this.Left.Average()) + ((this.Right == null) ? ((double)this.RightValue) : this.Right.Average());
			case 5:
				return ((this.Left == null) ? ((double)this.LeftValue) : this.Left.Average()) - ((this.Right == null) ? ((double)this.RightValue) : this.Right.Average());
			case 6:
				return ((this.Left == null) ? ((double)this.LeftValue) : this.Left.Average()) * ((this.Right == null) ? ((double)this.RightValue) : this.Right.Average());
			case 7:
			{
				double num5 = ((this.Right == null) ? ((double)this.RightValue) : this.Right.Average());
				if (num5 == 0.0)
				{
					return 0.0;
				}
				return ((this.Left == null) ? ((double)this.LeftValue) : this.Left.Average()) / num5;
			}
			case 8:
				return -((this.Left == null) ? ((double)this.LeftValue) : this.Left.Average());
			case 9:
			{
				if (this.Set != null && this.Set.Count > 0)
				{
					return this.Set.Select<DieRoll, double>((DieRoll s) => s.Average()).Average();
				}
				double num6 = ((this.Left == null) ? ((double)this.LeftValue) : this.Left.Average());
				double num7 = ((this.Right == null) ? ((double)this.RightValue) : this.Right.Average());
				return (num6 + num7) / 2.0;
			}
			default:
				return (double)this.LeftValue;
			}
		}

		// Token: 0x06004043 RID: 16451 RVA: 0x0013B9DC File Offset: 0x00139BDC
		public int Min()
		{
			switch (this.Type)
			{
			case 0:
				return 0;
			case 1:
			case 2:
				if (this.Left != null)
				{
					return this.Left.Min();
				}
				return this.LeftValue;
			case 3:
				return this.LeftValue;
			case 4:
				return ((this.Left == null) ? this.LeftValue : this.Left.Min()) + ((this.Right == null) ? this.RightValue : this.Right.Min());
			case 5:
				return ((this.Left == null) ? this.LeftValue : this.Left.Min()) - ((this.Right == null) ? this.RightValue : this.Right.Min());
			case 6:
				return ((this.Left == null) ? this.LeftValue : this.Left.Min()) * ((this.Right == null) ? this.RightValue : this.Right.Min());
			case 7:
			{
				int num = ((this.Right == null) ? this.RightValue : this.Right.Min());
				if (num == 0)
				{
					return 0;
				}
				return ((this.Left == null) ? this.LeftValue : this.Left.Min()) / num;
			}
			case 8:
				return -((this.Left == null) ? this.LeftValue : this.Left.Min());
			case 9:
			{
				if (this.Set != null && this.Set.Count > 0)
				{
					int num2 = int.MaxValue;
					foreach (DieRoll dieRoll in this.Set)
					{
						int num3 = dieRoll.Min();
						if (num3 < num2)
						{
							num2 = num3;
						}
					}
					return num2;
				}
				int num4 = int.MaxValue;
				int num5 = ((this.Left == null) ? this.LeftValue : this.Left.Min());
				if (num5 < num4)
				{
					num4 = num5;
				}
				int num6 = ((this.Right == null) ? this.RightValue : this.Right.Min());
				if (num6 < num4)
				{
					num4 = num6;
				}
				return num4;
			}
			default:
				return this.LeftValue;
			}
		}

		// Token: 0x06004044 RID: 16452 RVA: 0x0013BC14 File Offset: 0x00139E14
		public int Max()
		{
			switch (this.Type)
			{
			case 0:
				return 0;
			case 1:
			case 6:
				return ((this.Left == null) ? this.LeftValue : this.Left.Max()) * ((this.Right == null) ? this.RightValue : this.Right.Max());
			case 2:
				if (this.Right != null)
				{
					return this.Right.Max();
				}
				return this.RightValue;
			case 3:
				return this.LeftValue;
			case 4:
				return ((this.Left == null) ? this.LeftValue : this.Left.Max()) + ((this.Right == null) ? this.RightValue : this.Right.Max());
			case 5:
				return ((this.Left == null) ? this.LeftValue : this.Left.Max()) - ((this.Right == null) ? this.RightValue : this.Right.Max());
			case 7:
			{
				int num = ((this.Right == null) ? this.RightValue : this.Right.Max());
				if (num == 0)
				{
					return 0;
				}
				return ((this.Left == null) ? this.LeftValue : this.Left.Max()) / num;
			}
			case 8:
				return -((this.Left == null) ? this.LeftValue : this.Left.Max());
			case 9:
			{
				if (this.Set != null && this.Set.Count > 0)
				{
					int num2 = int.MinValue;
					foreach (DieRoll dieRoll in this.Set)
					{
						int num3 = dieRoll.Max();
						if (num3 > num2)
						{
							num2 = num3;
						}
					}
					return num2;
				}
				int num4 = int.MinValue;
				int num5 = ((this.Left == null) ? this.LeftValue : this.Left.Max());
				if (num5 > num4)
				{
					num4 = num5;
				}
				int num6 = ((this.Right == null) ? this.RightValue : this.Right.Max());
				if (num6 > num4)
				{
					num4 = num6;
				}
				return num4;
			}
			default:
				return this.LeftValue;
			}
		}

		// Token: 0x06004045 RID: 16453 RVA: 0x0013BE4C File Offset: 0x0013A04C
		public override string ToString()
		{
			string text = this.ToStringInner();
			if (!string.IsNullOrEmpty(this.Channel))
			{
				text = text + "[" + this.Channel + "]";
			}
			return text;
		}

		// Token: 0x06004046 RID: 16454 RVA: 0x0013BE88 File Offset: 0x0013A088
		private string ToStringInner()
		{
			switch (this.Type)
			{
			case 0:
				return "";
			case 1:
				return this.LeftString() + "d" + this.RightString();
			case 2:
				return this.LeftString() + "-" + this.RightString();
			case 3:
				return this.LeftString();
			case 4:
			{
				string text = this.RightString();
				if (text == "0")
				{
					return this.LeftString();
				}
				return this.LeftString() + "+" + text;
			}
			case 5:
			{
				string text2 = this.RightString();
				if (text2 == "0")
				{
					return this.LeftString();
				}
				return this.LeftString() + "-" + text2;
			}
			case 6:
			{
				string text3 = this.RightString();
				if (text3 == "1")
				{
					return this.LeftString();
				}
				return this.LeftString() + "x" + text3;
			}
			case 7:
			{
				string text4 = this.RightString();
				if (text4 == "1")
				{
					return this.LeftString();
				}
				return this.LeftString() + "/" + text4;
			}
			case 8:
				return "-" + this.LeftString();
			case 9:
				if (this.Set != null && this.Set.Count > 0)
				{
					StringBuilder stringBuilder = Event.NewStringBuilder(null);
					for (int i = 0; i < this.Set.Count; i++)
					{
						stringBuilder.Compound(this.Set[i].ToString(), '|');
					}
					return stringBuilder.ToString();
				}
				return this.LeftString() + "|" + this.RightString();
			default:
				return "";
			}
		}

		// Token: 0x06004047 RID: 16455 RVA: 0x0013C04A File Offset: 0x0013A24A
		private string LeftString()
		{
			if (this.Left == null)
			{
				return this.LeftValue.ToString();
			}
			return this.Left.ToString();
		}

		// Token: 0x06004048 RID: 16456 RVA: 0x0013C06B File Offset: 0x0013A26B
		private string RightString()
		{
			if (this.Right == null)
			{
				return this.RightValue.ToString();
			}
			return this.Right.ToString();
		}

		// Token: 0x06004049 RID: 16457 RVA: 0x0013C08C File Offset: 0x0013A28C
		private void IncorporateLeft(string Dice, ref bool AnyUnknown)
		{
			if (!int.TryParse(Dice, out this.LeftValue))
			{
				bool flag;
				this.Left = new DieRoll(Dice, out flag);
				if (flag)
				{
					AnyUnknown = true;
				}
				if (this.Left.Type == 3)
				{
					this.LeftValue = this.Left.LeftValue;
					this.Left = null;
				}
			}
		}

		// Token: 0x0600404A RID: 16458 RVA: 0x0013C0E4 File Offset: 0x0013A2E4
		private void IncorporateRight(string Dice, ref bool AnyUnknown)
		{
			if (!int.TryParse(Dice, out this.RightValue))
			{
				bool flag;
				this.Right = new DieRoll(Dice, out flag);
				if (flag)
				{
					AnyUnknown = true;
				}
				if (this.Right.Type == 3)
				{
					this.RightValue = this.Right.LeftValue;
					this.Right = null;
				}
			}
		}

		// Token: 0x0600404B RID: 16459 RVA: 0x0013C13C File Offset: 0x0013A33C
		public void Parse(string Dice, out bool AnyUnknown)
		{
			AnyUnknown = false;
			if (string.IsNullOrEmpty(Dice))
			{
				this.Type = 0;
				AnyUnknown = true;
				return;
			}
			Stat.TryExtractChannel(ref Dice, ref this.Channel);
			if (Dice[0] == '-')
			{
				if (int.TryParse(Dice, out this.LeftValue))
				{
					this.Type = 3;
					return;
				}
				this.Type = 8;
				this.IncorporateLeft(Dice.Substring(1), ref AnyUnknown);
				return;
			}
			else if (Dice.Contains("|"))
			{
				this.Type = 9;
				string[] array = Dice.Split('|', StringSplitOptions.None);
				switch (array.Length)
				{
				case 0:
					this.Type = 0;
					AnyUnknown = true;
					return;
				case 1:
				{
					bool flag;
					this.Parse(array[0], out flag);
					if (flag)
					{
						AnyUnknown = true;
					}
					return;
				}
				case 2:
					this.Type = 9;
					this.IncorporateLeft(array[0], ref AnyUnknown);
					this.IncorporateRight(array[1], ref AnyUnknown);
					return;
				default:
					this.Type = 9;
					this.Set = new List<DieRoll>(array.Length);
					foreach (string text in array)
					{
						this.Set.Add(new DieRoll(text));
					}
					return;
				}
			}
			else
			{
				if (Dice.Contains("+"))
				{
					string[] array3 = Dice.Split(Stat.splitterPlus, 2);
					this.Type = 4;
					this.IncorporateLeft(array3[0], ref AnyUnknown);
					this.IncorporateRight(array3[1], ref AnyUnknown);
					return;
				}
				if (Dice.Contains("d") && Dice.Contains("-"))
				{
					string[] array4 = Dice.Split(Stat.splitterMinus, 2);
					this.Type = 5;
					this.IncorporateLeft(array4[0], ref AnyUnknown);
					this.IncorporateRight(array4[1], ref AnyUnknown);
					return;
				}
				if (Dice.Contains("x"))
				{
					string[] array5 = Dice.Split(Stat.splitterX, 2);
					this.Type = 6;
					this.IncorporateLeft(array5[0], ref AnyUnknown);
					this.IncorporateRight(array5[1], ref AnyUnknown);
					return;
				}
				if (Dice.Contains("/"))
				{
					string[] array6 = Dice.Split(Stat.splitterSlash, 2);
					this.Type = 7;
					this.IncorporateLeft(array6[0], ref AnyUnknown);
					this.IncorporateRight(array6[1], ref AnyUnknown);
					return;
				}
				if (Dice.Contains("d"))
				{
					string[] array7 = Dice.Split(Stat.splitterD, 2);
					this.Type = 1;
					this.IncorporateLeft(array7[0], ref AnyUnknown);
					this.IncorporateRight(array7[1], ref AnyUnknown);
					return;
				}
				if (Dice.Contains("-"))
				{
					string[] array8 = Dice.Split(Stat.splitterMinus, 2);
					this.Type = 2;
					this.IncorporateLeft(array8[0], ref AnyUnknown);
					this.IncorporateRight(array8[1], ref AnyUnknown);
					return;
				}
				if (int.TryParse(Dice, out this.LeftValue))
				{
					this.Type = 3;
					return;
				}
				this.Type = 0;
				AnyUnknown = true;
				return;
			}
		}

		// Token: 0x0600404C RID: 16460 RVA: 0x0013C3E0 File Offset: 0x0013A5E0
		public void Parse(string Dice)
		{
			bool flag;
			this.Parse(Dice, out flag);
		}

		// Token: 0x0600404D RID: 16461 RVA: 0x0013C3F8 File Offset: 0x0013A5F8
		public DieRoll FindType(int TargetType)
		{
			if (this.Type == TargetType)
			{
				return this;
			}
			if (this.Left != null)
			{
				DieRoll dieRoll = this.Left.FindType(TargetType);
				if (dieRoll != null)
				{
					return dieRoll;
				}
			}
			if (this.Right != null)
			{
				DieRoll dieRoll2 = this.Right.FindType(TargetType);
				if (dieRoll2 != null)
				{
					return dieRoll2;
				}
			}
			if (this.Set != null)
			{
				foreach (DieRoll dieRoll3 in this.Set)
				{
					DieRoll dieRoll4 = dieRoll3.FindType(TargetType);
					if (dieRoll4 != null)
					{
						return dieRoll4;
					}
				}
			}
			return null;
		}

		// Token: 0x0600404E RID: 16462 RVA: 0x0013C4A0 File Offset: 0x0013A6A0
		public DieRoll FindTypeWithConstantLeft(int TargetType)
		{
			if (this.Type == TargetType && this.Left == null)
			{
				return this;
			}
			if (this.Left != null)
			{
				DieRoll dieRoll = this.Left.FindTypeWithConstantLeft(TargetType);
				if (dieRoll != null)
				{
					return dieRoll;
				}
			}
			if (this.Right != null)
			{
				DieRoll dieRoll2 = this.Right.FindTypeWithConstantLeft(TargetType);
				if (dieRoll2 != null)
				{
					return dieRoll2;
				}
			}
			if (this.Set != null)
			{
				foreach (DieRoll dieRoll3 in this.Set)
				{
					DieRoll dieRoll4 = dieRoll3.FindTypeWithConstantLeft(TargetType);
					if (dieRoll4 != null)
					{
						return dieRoll4;
					}
				}
			}
			return null;
		}

		// Token: 0x0600404F RID: 16463 RVA: 0x0013C550 File Offset: 0x0013A750
		public DieRoll FindTypeWithConstantRight(int TargetType)
		{
			if (this.Type == TargetType && this.Right == null)
			{
				return this;
			}
			if (this.Left != null)
			{
				DieRoll dieRoll = this.Left.FindTypeWithConstantRight(TargetType);
				if (dieRoll != null)
				{
					return dieRoll;
				}
			}
			if (this.Right != null)
			{
				DieRoll dieRoll2 = this.Right.FindTypeWithConstantRight(TargetType);
				if (dieRoll2 != null)
				{
					return dieRoll2;
				}
			}
			if (this.Set != null)
			{
				foreach (DieRoll dieRoll3 in this.Set)
				{
					DieRoll dieRoll4 = dieRoll3.FindTypeWithConstantRight(TargetType);
					if (dieRoll4 != null)
					{
						return dieRoll4;
					}
				}
			}
			return null;
		}

		// Token: 0x06004050 RID: 16464 RVA: 0x0013C600 File Offset: 0x0013A800
		public DieRoll FindTypeWithConstantBoth(int TargetType)
		{
			if (this.Type == TargetType && this.Right == null && this.Left == null)
			{
				return this;
			}
			if (this.Left != null)
			{
				DieRoll dieRoll = this.Left.FindTypeWithConstantBoth(TargetType);
				if (dieRoll != null)
				{
					return dieRoll;
				}
			}
			if (this.Right != null)
			{
				DieRoll dieRoll2 = this.Right.FindTypeWithConstantBoth(TargetType);
				if (dieRoll2 != null)
				{
					return dieRoll2;
				}
			}
			if (this.Set != null)
			{
				foreach (DieRoll dieRoll3 in this.Set)
				{
					DieRoll dieRoll4 = dieRoll3.FindTypeWithConstantBoth(TargetType);
					if (dieRoll4 != null)
					{
						return dieRoll4;
					}
				}
			}
			return null;
		}

		// Token: 0x06004051 RID: 16465 RVA: 0x0013C6B8 File Offset: 0x0013A8B8
		public void AdjustDieSize(int Amount)
		{
			if (Amount == 0)
			{
				return;
			}
			if (this.Type == 3)
			{
				this.Type = ((Amount < 0) ? 5 : 4);
				this.Right = new DieRoll("1d" + Math.Abs(Amount).ToString());
				return;
			}
			DieRoll dieRoll = this.FindTypeWithConstantRight(1);
			if (dieRoll != null)
			{
				dieRoll.RightValue += Amount;
				return;
			}
			DieRoll dieRoll2 = this.FindTypeWithConstantRight(2);
			if (dieRoll2 != null)
			{
				dieRoll2.RightValue += Amount;
				return;
			}
			this.Left = new DieRoll(this);
			this.LeftValue = 0;
			this.Type = ((Amount < 0) ? 5 : 4);
			this.Right = new DieRoll("1d" + Math.Abs(Amount).ToString());
			this.RightValue = 0;
		}

		// Token: 0x06004052 RID: 16466 RVA: 0x0013C783 File Offset: 0x0013A983
		public static string AdjustDieSize(string Roll, int Amount)
		{
			if (Amount == 0)
			{
				return Roll;
			}
			DieRoll dieRoll = new DieRoll(Roll);
			dieRoll.AdjustDieSize(Amount);
			return dieRoll.ToString();
		}

		// Token: 0x06004053 RID: 16467 RVA: 0x0013C79C File Offset: 0x0013A99C
		public void AdjustResult(int Amount)
		{
			if (Amount == 0)
			{
				return;
			}
			if (this.Type == 3)
			{
				this.LeftValue += Amount;
				return;
			}
			DieRoll dieRoll = this.FindTypeWithConstantRight(4);
			if (dieRoll != null)
			{
				dieRoll.RightValue += Amount;
				if (dieRoll.RightValue < 0)
				{
					dieRoll.RightValue = -dieRoll.RightValue;
					dieRoll.Type = 5;
				}
				return;
			}
			DieRoll dieRoll2 = this.FindTypeWithConstantRight(5);
			if (dieRoll2 != null)
			{
				dieRoll2.RightValue -= Amount;
				if (dieRoll2.RightValue < 0)
				{
					dieRoll2.RightValue = -dieRoll2.RightValue;
					dieRoll2.Type = 4;
				}
				return;
			}
			DieRoll dieRoll3 = this.FindTypeWithConstantLeft(4);
			if (dieRoll3 != null)
			{
				dieRoll3.LeftValue += Amount;
				if (dieRoll3.LeftValue < 0)
				{
					dieRoll3.LeftValue = -dieRoll3.LeftValue;
					dieRoll3.Type = 5;
				}
				return;
			}
			DieRoll dieRoll4 = this.FindTypeWithConstantLeft(5);
			if (dieRoll4 != null)
			{
				dieRoll4.LeftValue -= Amount;
				if (dieRoll4.LeftValue < 0)
				{
					dieRoll4.LeftValue = -dieRoll4.LeftValue;
					dieRoll4.Type = 4;
				}
				return;
			}
			DieRoll dieRoll5 = this.FindTypeWithConstantBoth(2);
			if (dieRoll5 != null && dieRoll5.LeftValue + Amount >= 0 && dieRoll5.RightValue + Amount >= 0)
			{
				dieRoll5.LeftValue += Amount;
				dieRoll5.RightValue += Amount;
				return;
			}
			this.Left = new DieRoll(this);
			this.LeftValue = 0;
			this.Type = ((Amount < 0) ? 5 : 4);
			this.Right = null;
			this.RightValue = Math.Abs(Amount);
		}

		// Token: 0x06004054 RID: 16468 RVA: 0x0013C919 File Offset: 0x0013AB19
		public static string AdjustResult(string Roll, int Amount)
		{
			if (Amount == 0)
			{
				return Roll;
			}
			DieRoll dieRoll = new DieRoll(Roll);
			dieRoll.AdjustResult(Amount);
			return dieRoll.ToString();
		}

		// Token: 0x04001F82 RID: 8066
		public const int TYPE_UNKNOWN = 0;

		// Token: 0x04001F83 RID: 8067
		public const int TYPE_DIE = 1;

		// Token: 0x04001F84 RID: 8068
		public const int TYPE_RANGE = 2;

		// Token: 0x04001F85 RID: 8069
		public const int TYPE_CONSTANT = 3;

		// Token: 0x04001F86 RID: 8070
		public const int TYPE_ADD = 4;

		// Token: 0x04001F87 RID: 8071
		public const int TYPE_SUBTRACT = 5;

		// Token: 0x04001F88 RID: 8072
		public const int TYPE_MULTIPLY = 6;

		// Token: 0x04001F89 RID: 8073
		public const int TYPE_DIVIDE = 7;

		// Token: 0x04001F8A RID: 8074
		public const int TYPE_NEGATE = 8;

		// Token: 0x04001F8B RID: 8075
		public const int TYPE_ALTERNATE = 9;

		// Token: 0x04001F8C RID: 8076
		public int Type;

		// Token: 0x04001F8D RID: 8077
		public int LeftValue;

		// Token: 0x04001F8E RID: 8078
		public int RightValue;

		// Token: 0x04001F8F RID: 8079
		public DieRoll Left;

		// Token: 0x04001F90 RID: 8080
		public DieRoll Right;

		// Token: 0x04001F91 RID: 8081
		public List<DieRoll> Set;

		// Token: 0x04001F92 RID: 8082
		public string Channel;
	}
}
