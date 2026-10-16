using System;
using System.Collections.Immutable;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000102 RID: 258
	internal class BuiltInOperators
	{
		// Token: 0x06001374 RID: 4980 RVA: 0x00069474 File Offset: 0x00067674
		internal BuiltInOperators(CSharpCompilation compilation)
		{
			this._compilation = compilation;
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x00069484 File Offset: 0x00067684
		private ImmutableArray<UnaryOperatorSignature> GetSignaturesFromUnaryOperatorKinds(int[] operatorKinds)
		{
			ArrayBuilder<UnaryOperatorSignature> instance = ArrayBuilder<UnaryOperatorSignature>.GetInstance();
			foreach (int num in operatorKinds)
			{
				instance.Add(this.GetSignature((UnaryOperatorKind)num));
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x000694C0 File Offset: 0x000676C0
		internal void GetSimpleBuiltInOperators(UnaryOperatorKind kind, ArrayBuilder<UnaryOperatorSignature> operators, bool skipNativeIntegerOperators)
		{
			if (this._builtInUnaryOperators == null)
			{
				ImmutableArray<UnaryOperatorSignature>[] array = new ImmutableArray<UnaryOperatorSignature>[]
				{
					this.GetSignaturesFromUnaryOperatorKinds(new int[]
					{
						4097, 4098, 4099, 4100, 4101, 4102, 4103, 4104, 4105, 4106,
						4107, 4108, 4109, 4110, 69633, 69634, 69635, 69636, 69637, 69638,
						69639, 69640, 69641, 69642, 69643, 69644, 69645, 69646
					}),
					this.GetSignaturesFromUnaryOperatorKinds(new int[]
					{
						4353, 4354, 4355, 4356, 4357, 4358, 4359, 4360, 4361, 4362,
						4363, 4364, 4365, 4366, 69889, 69890, 69891, 69892, 69893, 69894,
						69895, 69896, 69897, 69898, 69899, 69900, 69901, 69902
					}),
					this.GetSignaturesFromUnaryOperatorKinds(new int[]
					{
						4609, 4610, 4611, 4612, 4613, 4614, 4615, 4616, 4617, 4618,
						4619, 4620, 4621, 4622, 70145, 70146, 70147, 70148, 70149, 70150,
						70151, 70152, 70153, 70154, 70155, 70156, 70157, 70158
					}),
					this.GetSignaturesFromUnaryOperatorKinds(new int[]
					{
						4865, 4866, 4867, 4868, 4869, 4870, 4873, 4874, 4871, 4872,
						4875, 4876, 4877, 4878, 70401, 70402, 70403, 70404, 70405, 70406,
						70407, 70408, 70409, 70410, 70411, 70412, 70413, 70414
					}),
					this.GetSignaturesFromUnaryOperatorKinds(new int[]
					{
						5125, 5126, 5127, 5128, 5129, 5130, 5132, 5133, 5134, 70661,
						70662, 70663, 70664, 70665, 70666, 70668, 70669, 70670
					}),
					this.GetSignaturesFromUnaryOperatorKinds(new int[]
					{
						5381, 5383, 5385, 5388, 5389, 5390, 70917, 70919, 70921, 70924,
						70925, 70926
					}),
					this.GetSignaturesFromUnaryOperatorKinds(new int[] { 5647, 71183 }),
					this.GetSignaturesFromUnaryOperatorKinds(new int[]
					{
						5893, 5894, 5895, 5896, 5897, 5898, 71429, 71430, 71431, 71432,
						71433, 71434
					}),
					ImmutableArray<UnaryOperatorSignature>.Empty,
					ImmutableArray<UnaryOperatorSignature>.Empty
				};
				Interlocked.CompareExchange<ImmutableArray<UnaryOperatorSignature>[]>(ref this._builtInUnaryOperators, array, null);
			}
			foreach (UnaryOperatorSignature unaryOperatorSignature in this._builtInUnaryOperators[kind.OperatorIndex()])
			{
				if (skipNativeIntegerOperators)
				{
					UnaryOperatorKind unaryOperatorKind = unaryOperatorSignature.Kind.OperandTypes();
					if (unaryOperatorKind - UnaryOperatorKind.NInt <= 1)
					{
						continue;
					}
				}
				operators.Add(unaryOperatorSignature);
			}
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x0006964C File Offset: 0x0006784C
		internal UnaryOperatorSignature GetSignature(UnaryOperatorKind kind)
		{
			TypeSymbol typeSymbol;
			switch (kind.OperandTypes())
			{
			case UnaryOperatorKind.SByte:
				typeSymbol = this._compilation.GetSpecialType(SpecialType.System_SByte);
				break;
			case UnaryOperatorKind.Byte:
				typeSymbol = this._compilation.GetSpecialType(SpecialType.System_Byte);
				break;
			case UnaryOperatorKind.Short:
				typeSymbol = this._compilation.GetSpecialType(SpecialType.System_Int16);
				break;
			case UnaryOperatorKind.UShort:
				typeSymbol = this._compilation.GetSpecialType(SpecialType.System_UInt16);
				break;
			case UnaryOperatorKind.Int:
				typeSymbol = this._compilation.GetSpecialType(SpecialType.System_Int32);
				break;
			case UnaryOperatorKind.UInt:
				typeSymbol = this._compilation.GetSpecialType(SpecialType.System_UInt32);
				break;
			case UnaryOperatorKind.Long:
				typeSymbol = this._compilation.GetSpecialType(SpecialType.System_Int64);
				break;
			case UnaryOperatorKind.ULong:
				typeSymbol = this._compilation.GetSpecialType(SpecialType.System_UInt64);
				break;
			case UnaryOperatorKind.NInt:
				typeSymbol = this._compilation.CreateNativeIntegerTypeSymbol(true);
				break;
			case UnaryOperatorKind.NUInt:
				typeSymbol = this._compilation.CreateNativeIntegerTypeSymbol(false);
				break;
			case UnaryOperatorKind.Char:
				typeSymbol = this._compilation.GetSpecialType(SpecialType.System_Char);
				break;
			case UnaryOperatorKind.Float:
				typeSymbol = this._compilation.GetSpecialType(SpecialType.System_Single);
				break;
			case UnaryOperatorKind.Double:
				typeSymbol = this._compilation.GetSpecialType(SpecialType.System_Double);
				break;
			case UnaryOperatorKind.Decimal:
				typeSymbol = this._compilation.GetSpecialType(SpecialType.System_Decimal);
				break;
			case UnaryOperatorKind.Bool:
				typeSymbol = this._compilation.GetSpecialType(SpecialType.System_Boolean);
				break;
			default:
				throw ExceptionUtilities.UnexpectedValue(kind.OperandTypes());
			}
			if (kind.IsLifted())
			{
				typeSymbol = this._compilation.GetSpecialType(SpecialType.System_Nullable_T).Construct(new TypeSymbol[] { typeSymbol });
			}
			return new UnaryOperatorSignature(kind, typeSymbol, typeSymbol, null);
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x000697EC File Offset: 0x000679EC
		private ImmutableArray<BinaryOperatorSignature> GetSignaturesFromBinaryOperatorKinds(int[] operatorKinds)
		{
			ArrayBuilder<BinaryOperatorSignature> instance = ArrayBuilder<BinaryOperatorSignature>.GetInstance();
			foreach (int num in operatorKinds)
			{
				instance.Add(this.GetSignature((BinaryOperatorKind)num));
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x00069828 File Offset: 0x00067A28
		internal void GetSimpleBuiltInOperators(BinaryOperatorKind kind, ArrayBuilder<BinaryOperatorSignature> operators, bool skipNativeIntegerOperators)
		{
			if (this._builtInOperators == null)
			{
				ImmutableArray<BinaryOperatorSignature>[] array = new ImmutableArray<BinaryOperatorSignature>[]
				{
					ImmutableArray<BinaryOperatorSignature>.Empty,
					ImmutableArray<BinaryOperatorSignature>.Empty,
					ImmutableArray<BinaryOperatorSignature>.Empty,
					ImmutableArray<BinaryOperatorSignature>.Empty,
					ImmutableArray<BinaryOperatorSignature>.Empty,
					ImmutableArray<BinaryOperatorSignature>.Empty,
					ImmutableArray<BinaryOperatorSignature>.Empty,
					ImmutableArray<BinaryOperatorSignature>.Empty,
					ImmutableArray<BinaryOperatorSignature>.Empty,
					ImmutableArray<BinaryOperatorSignature>.Empty,
					ImmutableArray<BinaryOperatorSignature>.Empty,
					ImmutableArray<BinaryOperatorSignature>.Empty,
					ImmutableArray<BinaryOperatorSignature>.Empty,
					ImmutableArray.Create<BinaryOperatorSignature>(this.GetSignature(BinaryOperatorKind.LogicalBoolAnd)),
					ImmutableArray<BinaryOperatorSignature>.Empty,
					ImmutableArray.Create<BinaryOperatorSignature>(this.GetSignature(BinaryOperatorKind.LogicalBoolOr))
				};
				ImmutableArray<BinaryOperatorSignature>[] array2 = new ImmutableArray<BinaryOperatorSignature>[]
				{
					this.GetSignaturesFromBinaryOperatorKinds(new int[]
					{
						4101, 4102, 4103, 4104, 4105, 4106, 4108, 4109, 4110, 69637,
						69638, 69639, 69640, 69641, 69642, 69644, 69645, 69646
					}),
					this.GetSignaturesFromBinaryOperatorKinds(new int[]
					{
						4357, 4358, 4359, 4360, 4361, 4362, 4364, 4365, 4366, 69893,
						69894, 69895, 69896, 69897, 69898, 69900, 69901, 69902, 4369, 4370,
						4371
					}),
					this.GetSignaturesFromBinaryOperatorKinds(new int[]
					{
						4613, 4614, 4615, 4616, 4617, 4618, 4620, 4621, 4622, 70149,
						70150, 70151, 70152, 70153, 70154, 70156, 70157, 70158
					}),
					this.GetSignaturesFromBinaryOperatorKinds(new int[]
					{
						4869, 4870, 4871, 4872, 4873, 4874, 4876, 4877, 4878, 70405,
						70406, 70407, 70408, 70409, 70410, 70412, 70413, 70414
					}),
					this.GetSignaturesFromBinaryOperatorKinds(new int[]
					{
						5125, 5126, 5127, 5128, 5129, 5130, 5132, 5133, 5134, 70661,
						70662, 70663, 70664, 70665, 70666, 70668, 70669, 70670
					}),
					this.GetSignaturesFromBinaryOperatorKinds(new int[]
					{
						5381, 5382, 5383, 5384, 5385, 5386, 70917, 70918, 70919, 70920,
						70921, 70922
					}),
					this.GetSignaturesFromBinaryOperatorKinds(new int[]
					{
						5637, 5638, 5639, 5640, 5641, 5642, 71173, 71174, 71175, 71176,
						71177, 71178
					}),
					this.GetSignaturesFromBinaryOperatorKinds(new int[]
					{
						5893, 5894, 5895, 5896, 5897, 5898, 5900, 5901, 5902, 5903,
						71429, 71430, 71431, 71432, 71433, 71434, 71436, 71437, 71438, 71439,
						5904, 5905
					}),
					this.GetSignaturesFromBinaryOperatorKinds(new int[]
					{
						6149, 6150, 6151, 6152, 6153, 6154, 6156, 6157, 6158, 6159,
						71685, 71686, 71687, 71688, 71689, 71690, 71692, 71693, 71694, 71695,
						6160, 6161
					}),
					this.GetSignaturesFromBinaryOperatorKinds(new int[]
					{
						6405, 6406, 6407, 6408, 6409, 6410, 6412, 6413, 6414, 71941,
						71942, 71943, 71944, 71945, 71946, 71948, 71949, 71950
					}),
					this.GetSignaturesFromBinaryOperatorKinds(new int[]
					{
						6661, 6662, 6663, 6664, 6665, 6666, 6668, 6669, 6670, 72197,
						72198, 72199, 72200, 72201, 72202, 72204, 72205, 72206
					}),
					this.GetSignaturesFromBinaryOperatorKinds(new int[]
					{
						6917, 6918, 6919, 6920, 6921, 6922, 6924, 6925, 6926, 72453,
						72454, 72455, 72456, 72457, 72458, 72460, 72461, 72462
					}),
					this.GetSignaturesFromBinaryOperatorKinds(new int[]
					{
						7173, 7174, 7175, 7176, 7177, 7178, 7180, 7181, 7182, 72709,
						72710, 72711, 72712, 72713, 72714, 72716, 72717, 72718
					}),
					this.GetSignaturesFromBinaryOperatorKinds(new int[]
					{
						7429, 7430, 7431, 7432, 7433, 7434, 7439, 72965, 72966, 72967,
						72968, 72969, 72970, 72975
					}),
					this.GetSignaturesFromBinaryOperatorKinds(new int[]
					{
						7685, 7686, 7687, 7688, 7689, 7690, 7695, 73221, 73222, 73223,
						73224, 73225, 73226, 73231
					}),
					this.GetSignaturesFromBinaryOperatorKinds(new int[]
					{
						7941, 7942, 7943, 7944, 7945, 7946, 7951, 73477, 73478, 73479,
						73480, 73481, 73482, 73487
					})
				};
				ImmutableArray<BinaryOperatorSignature>[][] array3 = new ImmutableArray<BinaryOperatorSignature>[][] { array2, array };
				Interlocked.CompareExchange<ImmutableArray<BinaryOperatorSignature>[][]>(ref this._builtInOperators, array3, null);
			}
			foreach (BinaryOperatorSignature binaryOperatorSignature in this._builtInOperators[kind.IsLogical() ? 1 : 0][kind.OperatorIndex()])
			{
				if (skipNativeIntegerOperators)
				{
					BinaryOperatorKind binaryOperatorKind = binaryOperatorSignature.Kind.OperandTypes();
					if (binaryOperatorKind - BinaryOperatorKind.NInt <= 1)
					{
						continue;
					}
				}
				operators.Add(binaryOperatorSignature);
			}
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x00069B9C File Offset: 0x00067D9C
		internal BinaryOperatorSignature GetSignature(BinaryOperatorKind kind)
		{
			TypeSymbol typeSymbol = this.LeftType(kind);
			BinaryOperatorKind binaryOperatorKind = kind.Operator();
			if (binaryOperatorKind <= BinaryOperatorKind.Equal)
			{
				if (binaryOperatorKind > BinaryOperatorKind.Division)
				{
					if (binaryOperatorKind <= BinaryOperatorKind.LeftShift)
					{
						if (binaryOperatorKind == BinaryOperatorKind.Remainder)
						{
							goto IL_0111;
						}
						if (binaryOperatorKind != BinaryOperatorKind.LeftShift)
						{
							goto IL_0187;
						}
					}
					else if (binaryOperatorKind != BinaryOperatorKind.RightShift)
					{
						if (binaryOperatorKind != BinaryOperatorKind.Equal)
						{
							goto IL_0187;
						}
						goto IL_0171;
					}
					TypeSymbol typeSymbol2 = this._compilation.GetSpecialType(SpecialType.System_Int32);
					if (kind.IsLifted())
					{
						typeSymbol2 = this._compilation.GetSpecialType(SpecialType.System_Nullable_T).Construct(new TypeSymbol[] { typeSymbol2 });
					}
					return new BinaryOperatorSignature(kind, typeSymbol, typeSymbol2, typeSymbol, null);
				}
				if (binaryOperatorKind <= BinaryOperatorKind.Addition)
				{
					if (binaryOperatorKind != BinaryOperatorKind.Multiplication)
					{
						if (binaryOperatorKind != BinaryOperatorKind.Addition)
						{
							goto IL_0187;
						}
						return new BinaryOperatorSignature(kind, typeSymbol, this.RightType(kind), this.ReturnType(kind), null);
					}
				}
				else if (binaryOperatorKind != BinaryOperatorKind.Subtraction && binaryOperatorKind != BinaryOperatorKind.Division)
				{
					goto IL_0187;
				}
			}
			else if (binaryOperatorKind <= BinaryOperatorKind.GreaterThanOrEqual)
			{
				if (binaryOperatorKind <= BinaryOperatorKind.GreaterThan)
				{
					if (binaryOperatorKind != BinaryOperatorKind.NotEqual && binaryOperatorKind != BinaryOperatorKind.GreaterThan)
					{
						goto IL_0187;
					}
					goto IL_0171;
				}
				else
				{
					if (binaryOperatorKind != BinaryOperatorKind.LessThan && binaryOperatorKind != BinaryOperatorKind.GreaterThanOrEqual)
					{
						goto IL_0187;
					}
					goto IL_0171;
				}
			}
			else if (binaryOperatorKind <= BinaryOperatorKind.And)
			{
				if (binaryOperatorKind == BinaryOperatorKind.LessThanOrEqual)
				{
					goto IL_0171;
				}
				if (binaryOperatorKind != BinaryOperatorKind.And)
				{
					goto IL_0187;
				}
			}
			else if (binaryOperatorKind != BinaryOperatorKind.Xor && binaryOperatorKind != BinaryOperatorKind.Or)
			{
				goto IL_0187;
			}
			IL_0111:
			return new BinaryOperatorSignature(kind, typeSymbol, typeSymbol, typeSymbol, null);
			IL_0171:
			return new BinaryOperatorSignature(kind, typeSymbol, typeSymbol, this._compilation.GetSpecialType(SpecialType.System_Boolean), null);
			IL_0187:
			return new BinaryOperatorSignature(kind, typeSymbol, this.RightType(kind), this.ReturnType(kind), null);
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x00069D48 File Offset: 0x00067F48
		private TypeSymbol LeftType(BinaryOperatorKind kind)
		{
			if (kind.IsLifted())
			{
				return this.LiftedType(kind);
			}
			switch (kind.OperandTypes())
			{
			case BinaryOperatorKind.Int:
				return this._compilation.GetSpecialType(SpecialType.System_Int32);
			case BinaryOperatorKind.UInt:
				return this._compilation.GetSpecialType(SpecialType.System_UInt32);
			case BinaryOperatorKind.Long:
				return this._compilation.GetSpecialType(SpecialType.System_Int64);
			case BinaryOperatorKind.ULong:
				return this._compilation.GetSpecialType(SpecialType.System_UInt64);
			case BinaryOperatorKind.NInt:
				return this._compilation.CreateNativeIntegerTypeSymbol(true);
			case BinaryOperatorKind.NUInt:
				return this._compilation.CreateNativeIntegerTypeSymbol(false);
			case BinaryOperatorKind.Float:
				return this._compilation.GetSpecialType(SpecialType.System_Single);
			case BinaryOperatorKind.Double:
				return this._compilation.GetSpecialType(SpecialType.System_Double);
			case BinaryOperatorKind.Decimal:
				return this._compilation.GetSpecialType(SpecialType.System_Decimal);
			case BinaryOperatorKind.Bool:
				return this._compilation.GetSpecialType(SpecialType.System_Boolean);
			case BinaryOperatorKind.Object:
			case BinaryOperatorKind.ObjectAndString:
				return this._compilation.GetSpecialType(SpecialType.System_Object);
			case BinaryOperatorKind.String:
			case BinaryOperatorKind.StringAndObject:
				return this._compilation.GetSpecialType(SpecialType.System_String);
			}
			return null;
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x00069E5C File Offset: 0x0006805C
		private TypeSymbol RightType(BinaryOperatorKind kind)
		{
			if (kind.IsLifted())
			{
				return this.LiftedType(kind);
			}
			switch (kind.OperandTypes())
			{
			case BinaryOperatorKind.Int:
				return this._compilation.GetSpecialType(SpecialType.System_Int32);
			case BinaryOperatorKind.UInt:
				return this._compilation.GetSpecialType(SpecialType.System_UInt32);
			case BinaryOperatorKind.Long:
				return this._compilation.GetSpecialType(SpecialType.System_Int64);
			case BinaryOperatorKind.ULong:
				return this._compilation.GetSpecialType(SpecialType.System_UInt64);
			case BinaryOperatorKind.NInt:
				return this._compilation.CreateNativeIntegerTypeSymbol(true);
			case BinaryOperatorKind.NUInt:
				return this._compilation.CreateNativeIntegerTypeSymbol(false);
			case BinaryOperatorKind.Float:
				return this._compilation.GetSpecialType(SpecialType.System_Single);
			case BinaryOperatorKind.Double:
				return this._compilation.GetSpecialType(SpecialType.System_Double);
			case BinaryOperatorKind.Decimal:
				return this._compilation.GetSpecialType(SpecialType.System_Decimal);
			case BinaryOperatorKind.Bool:
				return this._compilation.GetSpecialType(SpecialType.System_Boolean);
			case BinaryOperatorKind.Object:
			case BinaryOperatorKind.StringAndObject:
				return this._compilation.GetSpecialType(SpecialType.System_Object);
			case BinaryOperatorKind.String:
			case BinaryOperatorKind.ObjectAndString:
				return this._compilation.GetSpecialType(SpecialType.System_String);
			}
			return null;
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x00069F70 File Offset: 0x00068170
		private TypeSymbol ReturnType(BinaryOperatorKind kind)
		{
			if (kind.IsLifted())
			{
				return this.LiftedType(kind);
			}
			switch (kind.OperandTypes())
			{
			case BinaryOperatorKind.Int:
				return this._compilation.GetSpecialType(SpecialType.System_Int32);
			case BinaryOperatorKind.UInt:
				return this._compilation.GetSpecialType(SpecialType.System_UInt32);
			case BinaryOperatorKind.Long:
				return this._compilation.GetSpecialType(SpecialType.System_Int64);
			case BinaryOperatorKind.ULong:
				return this._compilation.GetSpecialType(SpecialType.System_UInt64);
			case BinaryOperatorKind.NInt:
				return this._compilation.CreateNativeIntegerTypeSymbol(true);
			case BinaryOperatorKind.NUInt:
				return this._compilation.CreateNativeIntegerTypeSymbol(false);
			case BinaryOperatorKind.Float:
				return this._compilation.GetSpecialType(SpecialType.System_Single);
			case BinaryOperatorKind.Double:
				return this._compilation.GetSpecialType(SpecialType.System_Double);
			case BinaryOperatorKind.Decimal:
				return this._compilation.GetSpecialType(SpecialType.System_Decimal);
			case BinaryOperatorKind.Bool:
				return this._compilation.GetSpecialType(SpecialType.System_Boolean);
			case BinaryOperatorKind.Object:
				return this._compilation.GetSpecialType(SpecialType.System_Object);
			case BinaryOperatorKind.String:
			case BinaryOperatorKind.StringAndObject:
			case BinaryOperatorKind.ObjectAndString:
				return this._compilation.GetSpecialType(SpecialType.System_String);
			}
			return null;
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x0006A084 File Offset: 0x00068284
		private TypeSymbol LiftedType(BinaryOperatorKind kind)
		{
			NamedTypeSymbol specialType = this._compilation.GetSpecialType(SpecialType.System_Nullable_T);
			switch (kind.OperandTypes())
			{
			case BinaryOperatorKind.Int:
				return specialType.Construct(new TypeSymbol[] { this._compilation.GetSpecialType(SpecialType.System_Int32) });
			case BinaryOperatorKind.UInt:
				return specialType.Construct(new TypeSymbol[] { this._compilation.GetSpecialType(SpecialType.System_UInt32) });
			case BinaryOperatorKind.Long:
				return specialType.Construct(new TypeSymbol[] { this._compilation.GetSpecialType(SpecialType.System_Int64) });
			case BinaryOperatorKind.ULong:
				return specialType.Construct(new TypeSymbol[] { this._compilation.GetSpecialType(SpecialType.System_UInt64) });
			case BinaryOperatorKind.NInt:
				return specialType.Construct(new TypeSymbol[] { this._compilation.CreateNativeIntegerTypeSymbol(true) });
			case BinaryOperatorKind.NUInt:
				return specialType.Construct(new TypeSymbol[] { this._compilation.CreateNativeIntegerTypeSymbol(false) });
			case BinaryOperatorKind.Float:
				return specialType.Construct(new TypeSymbol[] { this._compilation.GetSpecialType(SpecialType.System_Single) });
			case BinaryOperatorKind.Double:
				return specialType.Construct(new TypeSymbol[] { this._compilation.GetSpecialType(SpecialType.System_Double) });
			case BinaryOperatorKind.Decimal:
				return specialType.Construct(new TypeSymbol[] { this._compilation.GetSpecialType(SpecialType.System_Decimal) });
			case BinaryOperatorKind.Bool:
				return specialType.Construct(new TypeSymbol[] { this._compilation.GetSpecialType(SpecialType.System_Boolean) });
			}
			return null;
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x0006A200 File Offset: 0x00068400
		internal static bool IsValidObjectEquality(Conversions Conversions, TypeSymbol leftType, bool leftIsNull, bool leftIsDefault, TypeSymbol rightType, bool rightIsNull, bool rightIsDefault, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (leftType != null && leftType.IsTypeParameter())
			{
				if (leftType.IsValueType || (!leftType.IsReferenceType && !rightIsNull))
				{
					return false;
				}
				leftType = ((TypeParameterSymbol)leftType).EffectiveBaseClass(ref useSiteInfo);
			}
			if (rightType != null && rightType.IsTypeParameter())
			{
				if (rightType.IsValueType || (!rightType.IsReferenceType && !leftIsNull))
				{
					return false;
				}
				rightType = ((TypeParameterSymbol)rightType).EffectiveBaseClass(ref useSiteInfo);
			}
			if ((leftType == null || !leftType.IsReferenceType) && !leftIsNull && !leftIsDefault)
			{
				return false;
			}
			if ((rightType == null || !rightType.IsReferenceType) && !rightIsNull && !rightIsDefault)
			{
				return false;
			}
			if (leftIsDefault && rightIsDefault)
			{
				return false;
			}
			if (leftIsDefault && rightIsNull)
			{
				return false;
			}
			if (leftIsNull && rightIsDefault)
			{
				return false;
			}
			if (leftIsNull || rightIsNull || leftIsDefault || rightIsDefault)
			{
				return true;
			}
			Conversion conversion = Conversions.ClassifyConversionFromType(leftType, rightType, ref useSiteInfo, false);
			if (conversion.IsIdentity || conversion.IsReference)
			{
				return true;
			}
			Conversion conversion2 = Conversions.ClassifyConversionFromType(rightType, leftType, ref useSiteInfo, false);
			return conversion2.IsIdentity || conversion2.IsReference;
		}

		// Token: 0x040005F9 RID: 1529
		private readonly CSharpCompilation _compilation;

		// Token: 0x040005FA RID: 1530
		private ImmutableArray<UnaryOperatorSignature>[] _builtInUnaryOperators;

		// Token: 0x040005FB RID: 1531
		private ImmutableArray<BinaryOperatorSignature>[][] _builtInOperators;
	}
}
