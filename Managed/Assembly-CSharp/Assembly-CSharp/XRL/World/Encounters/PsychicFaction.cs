using System;
using System.CodeDom.Compiler;
using System.Text;
using Occult.Engine.CodeGeneration;

namespace XRL.World.Encounters
{
	// Token: 0x02000BA2 RID: 2978
	[GenerateSerializationPartial]
	[Serializable]
	public sealed class PsychicFaction : IComposite
	{
		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x060076AB RID: 30379 RVA: 0x002C9D69 File Offset: 0x002C7F69
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060076AC RID: 30380 RVA: 0x002C9D6C File Offset: 0x002C7F6C
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public void Write(SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.factionName);
			Writer.WriteOptimized(this.preferredMutation);
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
			Writer.WriteOptimized(this.dimensionSymbol);
			Writer.WriteOptimized(this.cultSymbol);
			Writer.WriteOptimized(this.cultForm);
			Writer.WriteOptimized(this.dimensionName);
			Writer.WriteOptimized(this.dimensionSecretID);
			Writer.WriteOptimized(this.dimensionalWeaponIndex);
			Writer.WriteOptimized(this.dimensionalMissileWeaponIndex);
			Writer.WriteOptimized(this.dimensionalArmorIndex);
			Writer.WriteOptimized(this.dimensionalShieldIndex);
			Writer.WriteOptimized(this.dimensionalMiscIndex);
			Writer.WriteOptimized(this.dimensionalTraining);
		}

		// Token: 0x060076AD RID: 30381 RVA: 0x002C9F2C File Offset: 0x002C812C
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public void Read(SerializationReader Reader)
		{
			this.factionName = Reader.ReadOptimizedString();
			this.preferredMutation = Reader.ReadOptimizedString();
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
			this.dimensionSymbol = Reader.ReadOptimizedInt32();
			this.cultSymbol = Reader.ReadOptimizedInt32();
			this.cultForm = Reader.ReadOptimizedString();
			this.dimensionName = Reader.ReadOptimizedString();
			this.dimensionSecretID = Reader.ReadOptimizedString();
			this.dimensionalWeaponIndex = Reader.ReadOptimizedInt32();
			this.dimensionalMissileWeaponIndex = Reader.ReadOptimizedInt32();
			this.dimensionalArmorIndex = Reader.ReadOptimizedInt32();
			this.dimensionalShieldIndex = Reader.ReadOptimizedInt32();
			this.dimensionalMiscIndex = Reader.ReadOptimizedInt32();
			this.dimensionalTraining = Reader.ReadOptimizedString();
		}

		// Token: 0x060076AF RID: 30383 RVA: 0x002CA0F4 File Offset: 0x002C82F4
		public string Weirdify(string Text)
		{
			PsychicFaction.SB.Clear();
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
				PsychicFaction.SB.Append(c);
				i++;
			}
			return PsychicFaction.SB.ToString();
		}

		// Token: 0x0400367A RID: 13946
		public string factionName;

		// Token: 0x0400367B RID: 13947
		public string preferredMutation;

		// Token: 0x0400367C RID: 13948
		public string mainColor;

		// Token: 0x0400367D RID: 13949
		public string detailColor;

		// Token: 0x0400367E RID: 13950
		public string a;

		// Token: 0x0400367F RID: 13951
		public string e;

		// Token: 0x04003680 RID: 13952
		public string i;

		// Token: 0x04003681 RID: 13953
		public string o;

		// Token: 0x04003682 RID: 13954
		public string u;

		// Token: 0x04003683 RID: 13955
		public string A;

		// Token: 0x04003684 RID: 13956
		public string E;

		// Token: 0x04003685 RID: 13957
		public string I;

		// Token: 0x04003686 RID: 13958
		public string O;

		// Token: 0x04003687 RID: 13959
		public string U;

		// Token: 0x04003688 RID: 13960
		public string c;

		// Token: 0x04003689 RID: 13961
		public string f;

		// Token: 0x0400368A RID: 13962
		public string n;

		// Token: 0x0400368B RID: 13963
		public string t;

		// Token: 0x0400368C RID: 13964
		public string y;

		// Token: 0x0400368D RID: 13965
		public string B;

		// Token: 0x0400368E RID: 13966
		public string C;

		// Token: 0x0400368F RID: 13967
		public string Y;

		// Token: 0x04003690 RID: 13968
		public string L;

		// Token: 0x04003691 RID: 13969
		public string R;

		// Token: 0x04003692 RID: 13970
		public string N;

		// Token: 0x04003693 RID: 13971
		public int dimensionSymbol;

		// Token: 0x04003694 RID: 13972
		public int cultSymbol;

		// Token: 0x04003695 RID: 13973
		public string cultForm;

		// Token: 0x04003696 RID: 13974
		public string dimensionName;

		// Token: 0x04003697 RID: 13975
		public string dimensionSecretID;

		// Token: 0x04003698 RID: 13976
		public int dimensionalWeaponIndex;

		// Token: 0x04003699 RID: 13977
		public int dimensionalMissileWeaponIndex;

		// Token: 0x0400369A RID: 13978
		public int dimensionalArmorIndex;

		// Token: 0x0400369B RID: 13979
		public int dimensionalShieldIndex;

		// Token: 0x0400369C RID: 13980
		public int dimensionalMiscIndex;

		// Token: 0x0400369D RID: 13981
		public string dimensionalTraining;

		// Token: 0x0400369E RID: 13982
		private static StringBuilder SB = new StringBuilder();
	}
}
