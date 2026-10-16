using System;
using System.CodeDom.Compiler;
using System.Text;
using Occult.Engine.CodeGeneration;

namespace XRL.World.Encounters
{
	// Token: 0x02000BA1 RID: 2977
	[GenerateSerializationPartial]
	[Serializable]
	public sealed class ExtraDimension : IComposite
	{
		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x060076A5 RID: 30373 RVA: 0x002C974A File Offset: 0x002C794A
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060076A6 RID: 30374 RVA: 0x002C9750 File Offset: 0x002C7950
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public void Write(SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.Name);
			Writer.WriteOptimized(this.Symbol);
			Writer.WriteOptimized(this.WeaponIndex);
			Writer.WriteOptimized(this.MissileWeaponIndex);
			Writer.WriteOptimized(this.ArmorIndex);
			Writer.WriteOptimized(this.ShieldIndex);
			Writer.WriteOptimized(this.MiscIndex);
			Writer.WriteOptimized(this.Training);
			Writer.WriteOptimized(this.SecretID);
			Writer.WriteOptimized(this.mainColor);
			Writer.WriteOptimized(this.detailColor);
			Writer.WriteOptimized(this.a);
			Writer.WriteOptimized(this.e);
			Writer.WriteOptimized(this.i);
			Writer.WriteOptimized(this.o);
			Writer.WriteOptimized(this.u);
			Writer.WriteOptimized(this.A);
			Writer.WriteOptimized(this.E);
			Writer.WriteOptimized(this.I);
			Writer.WriteOptimized(this.O);
			Writer.WriteOptimized(this.U);
			Writer.WriteOptimized(this.c);
			Writer.WriteOptimized(this.f);
			Writer.WriteOptimized(this.n);
			Writer.WriteOptimized(this.t);
			Writer.WriteOptimized(this.y);
			Writer.WriteOptimized(this.B);
			Writer.WriteOptimized(this.C);
			Writer.WriteOptimized(this.Y);
			Writer.WriteOptimized(this.L);
			Writer.WriteOptimized(this.R);
			Writer.WriteOptimized(this.N);
		}

		// Token: 0x060076A7 RID: 30375 RVA: 0x002C98E0 File Offset: 0x002C7AE0
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public void Read(SerializationReader Reader)
		{
			this.Name = Reader.ReadOptimizedString();
			this.Symbol = Reader.ReadOptimizedInt32();
			this.WeaponIndex = Reader.ReadOptimizedInt32();
			this.MissileWeaponIndex = Reader.ReadOptimizedInt32();
			this.ArmorIndex = Reader.ReadOptimizedInt32();
			this.ShieldIndex = Reader.ReadOptimizedInt32();
			this.MiscIndex = Reader.ReadOptimizedInt32();
			this.Training = Reader.ReadOptimizedString();
			this.SecretID = Reader.ReadOptimizedString();
			this.mainColor = Reader.ReadOptimizedString();
			this.detailColor = Reader.ReadOptimizedString();
			this.a = Reader.ReadOptimizedString();
			this.e = Reader.ReadOptimizedString();
			this.i = Reader.ReadOptimizedString();
			this.o = Reader.ReadOptimizedString();
			this.u = Reader.ReadOptimizedString();
			this.A = Reader.ReadOptimizedString();
			this.E = Reader.ReadOptimizedString();
			this.I = Reader.ReadOptimizedString();
			this.O = Reader.ReadOptimizedString();
			this.U = Reader.ReadOptimizedString();
			this.c = Reader.ReadOptimizedString();
			this.f = Reader.ReadOptimizedString();
			this.n = Reader.ReadOptimizedString();
			this.t = Reader.ReadOptimizedString();
			this.y = Reader.ReadOptimizedString();
			this.B = Reader.ReadOptimizedString();
			this.C = Reader.ReadOptimizedString();
			this.Y = Reader.ReadOptimizedString();
			this.L = Reader.ReadOptimizedString();
			this.R = Reader.ReadOptimizedString();
			this.N = Reader.ReadOptimizedString();
		}

		// Token: 0x060076A9 RID: 30377 RVA: 0x002C9A78 File Offset: 0x002C7C78
		public string Weirdify(string Text)
		{
			ExtraDimension.SB.Clear();
			bool flag = false;
			bool flag2 = false;
			int i = 0;
			int length = Text.Length;
			while (i < length)
			{
				char c = Text[i];
				if (c == '=')
				{
					flag = !flag;
				}
				else if (c == '*')
				{
					flag2 = !flag2;
				}
				else if (!flag && !flag2)
				{
					char c2;
					if (c <= 'i')
					{
						if (c <= 'R')
						{
							switch (c)
							{
							case 'A':
								c2 = this.A[0];
								break;
							case 'B':
								c2 = this.B[0];
								break;
							case 'C':
								c2 = this.C[0];
								break;
							case 'D':
							case 'F':
							case 'G':
							case 'H':
							case 'J':
							case 'K':
							case 'M':
								goto IL_02AE;
							case 'E':
								c2 = this.E[0];
								break;
							case 'I':
								c2 = this.I[0];
								break;
							case 'L':
								c2 = this.L[0];
								break;
							case 'N':
								c2 = this.N[0];
								break;
							case 'O':
								c2 = this.O[0];
								break;
							default:
								if (c != 'R')
								{
									goto IL_02AE;
								}
								c2 = this.R[0];
								break;
							}
						}
						else if (c != 'U')
						{
							if (c != 'Y')
							{
								switch (c)
								{
								case 'a':
									c2 = this.a[0];
									break;
								case 'b':
								case 'd':
								case 'g':
								case 'h':
									goto IL_02AE;
								case 'c':
									c2 = this.c[0];
									break;
								case 'e':
									c2 = this.e[0];
									break;
								case 'f':
									c2 = this.f[0];
									break;
								case 'i':
									c2 = this.i[0];
									break;
								default:
									goto IL_02AE;
								}
							}
							else
							{
								c2 = this.Y[0];
							}
						}
						else
						{
							c2 = this.U[0];
						}
					}
					else if (c <= 'o')
					{
						if (c != 'n')
						{
							if (c != 'o')
							{
								goto IL_02AE;
							}
							c2 = this.o[0];
						}
						else
						{
							c2 = this.n[0];
						}
					}
					else if (c != 't')
					{
						if (c != 'u')
						{
							if (c != 'y')
							{
								goto IL_02AE;
							}
							c2 = this.y[0];
						}
						else
						{
							c2 = this.u[0];
						}
					}
					else
					{
						c2 = this.t[0];
					}
					IL_02B2:
					c = c2;
					goto IL_02B6;
					IL_02AE:
					c2 = c;
					goto IL_02B2;
				}
				IL_02B6:
				ExtraDimension.SB.Append(c);
				i++;
			}
			return ExtraDimension.SB.ToString();
		}

		// Token: 0x04003659 RID: 13913
		public string Name;

		// Token: 0x0400365A RID: 13914
		public int Symbol;

		// Token: 0x0400365B RID: 13915
		public int WeaponIndex;

		// Token: 0x0400365C RID: 13916
		public int MissileWeaponIndex;

		// Token: 0x0400365D RID: 13917
		public int ArmorIndex;

		// Token: 0x0400365E RID: 13918
		public int ShieldIndex;

		// Token: 0x0400365F RID: 13919
		public int MiscIndex;

		// Token: 0x04003660 RID: 13920
		public string Training;

		// Token: 0x04003661 RID: 13921
		public string SecretID;

		// Token: 0x04003662 RID: 13922
		public string mainColor;

		// Token: 0x04003663 RID: 13923
		public string detailColor;

		// Token: 0x04003664 RID: 13924
		public string a;

		// Token: 0x04003665 RID: 13925
		public string e;

		// Token: 0x04003666 RID: 13926
		public string i;

		// Token: 0x04003667 RID: 13927
		public string o;

		// Token: 0x04003668 RID: 13928
		public string u;

		// Token: 0x04003669 RID: 13929
		public string A;

		// Token: 0x0400366A RID: 13930
		public string E;

		// Token: 0x0400366B RID: 13931
		public string I;

		// Token: 0x0400366C RID: 13932
		public string O;

		// Token: 0x0400366D RID: 13933
		public string U;

		// Token: 0x0400366E RID: 13934
		public string c;

		// Token: 0x0400366F RID: 13935
		public string f;

		// Token: 0x04003670 RID: 13936
		public string n;

		// Token: 0x04003671 RID: 13937
		public string t;

		// Token: 0x04003672 RID: 13938
		public string y;

		// Token: 0x04003673 RID: 13939
		public string B;

		// Token: 0x04003674 RID: 13940
		public string C;

		// Token: 0x04003675 RID: 13941
		public string Y;

		// Token: 0x04003676 RID: 13942
		public string L;

		// Token: 0x04003677 RID: 13943
		public string R;

		// Token: 0x04003678 RID: 13944
		public string N;

		// Token: 0x04003679 RID: 13945
		private static StringBuilder SB = new StringBuilder();
	}
}
