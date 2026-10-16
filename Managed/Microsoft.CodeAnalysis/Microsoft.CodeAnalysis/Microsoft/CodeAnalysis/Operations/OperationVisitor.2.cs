using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.FlowAnalysis;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200045B RID: 1115
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class OperationVisitor<[Nullable(2)] TArgument, [Nullable(2)] TResult>
	{
		// Token: 0x06002B9E RID: 11166 RVA: 0x0007C77C File Offset: 0x0007A97C
		[NullableContext(2)]
		public virtual TResult Visit(IOperation operation, [Nullable(1)] TArgument argument)
		{
			if (operation != null)
			{
				return operation.Accept<TArgument, TResult>(this, argument);
			}
			return default(TResult);
		}

		// Token: 0x06002B9F RID: 11167 RVA: 0x0007C7A0 File Offset: 0x0007A9A0
		[return: Nullable(2)]
		public virtual TResult DefaultVisit(IOperation operation, TArgument argument)
		{
			return default(TResult);
		}

		// Token: 0x06002BA0 RID: 11168 RVA: 0x0007C7B8 File Offset: 0x0007A9B8
		[return: Nullable(2)]
		internal virtual TResult VisitNoneOperation(IOperation operation, TArgument argument)
		{
			return default(TResult);
		}

		// Token: 0x06002BA1 RID: 11169 RVA: 0x0007C7CE File Offset: 0x0007A9CE
		[return: Nullable(2)]
		public virtual TResult VisitInvalid(IInvalidOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BA2 RID: 11170 RVA: 0x0007C7D8 File Offset: 0x0007A9D8
		[return: Nullable(2)]
		public virtual TResult VisitBlock(IBlockOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BA3 RID: 11171 RVA: 0x0007C7E2 File Offset: 0x0007A9E2
		[return: Nullable(2)]
		public virtual TResult VisitVariableDeclarationGroup(IVariableDeclarationGroupOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BA4 RID: 11172 RVA: 0x0007C7EC File Offset: 0x0007A9EC
		[return: Nullable(2)]
		public virtual TResult VisitSwitch(ISwitchOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BA5 RID: 11173 RVA: 0x0007C7F6 File Offset: 0x0007A9F6
		[return: Nullable(2)]
		public virtual TResult VisitForEachLoop(IForEachLoopOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BA6 RID: 11174 RVA: 0x0007C800 File Offset: 0x0007AA00
		[return: Nullable(2)]
		public virtual TResult VisitForLoop(IForLoopOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BA7 RID: 11175 RVA: 0x0007C80A File Offset: 0x0007AA0A
		[return: Nullable(2)]
		public virtual TResult VisitForToLoop(IForToLoopOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BA8 RID: 11176 RVA: 0x0007C814 File Offset: 0x0007AA14
		[return: Nullable(2)]
		public virtual TResult VisitWhileLoop(IWhileLoopOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BA9 RID: 11177 RVA: 0x0007C81E File Offset: 0x0007AA1E
		[return: Nullable(2)]
		public virtual TResult VisitLabeled(ILabeledOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BAA RID: 11178 RVA: 0x0007C828 File Offset: 0x0007AA28
		[return: Nullable(2)]
		public virtual TResult VisitBranch(IBranchOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BAB RID: 11179 RVA: 0x0007C832 File Offset: 0x0007AA32
		[return: Nullable(2)]
		public virtual TResult VisitEmpty(IEmptyOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BAC RID: 11180 RVA: 0x0007C83C File Offset: 0x0007AA3C
		[return: Nullable(2)]
		public virtual TResult VisitReturn(IReturnOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BAD RID: 11181 RVA: 0x0007C846 File Offset: 0x0007AA46
		[return: Nullable(2)]
		public virtual TResult VisitLock(ILockOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BAE RID: 11182 RVA: 0x0007C850 File Offset: 0x0007AA50
		[return: Nullable(2)]
		public virtual TResult VisitTry(ITryOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BAF RID: 11183 RVA: 0x0007C85A File Offset: 0x0007AA5A
		[return: Nullable(2)]
		public virtual TResult VisitUsing(IUsingOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BB0 RID: 11184 RVA: 0x0007C864 File Offset: 0x0007AA64
		[return: Nullable(2)]
		public virtual TResult VisitExpressionStatement(IExpressionStatementOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BB1 RID: 11185 RVA: 0x0007C86E File Offset: 0x0007AA6E
		[return: Nullable(2)]
		public virtual TResult VisitLocalFunction(ILocalFunctionOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BB2 RID: 11186 RVA: 0x0007C878 File Offset: 0x0007AA78
		[return: Nullable(2)]
		public virtual TResult VisitStop(IStopOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BB3 RID: 11187 RVA: 0x0007C882 File Offset: 0x0007AA82
		[return: Nullable(2)]
		public virtual TResult VisitEnd(IEndOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BB4 RID: 11188 RVA: 0x0007C88C File Offset: 0x0007AA8C
		[return: Nullable(2)]
		public virtual TResult VisitRaiseEvent(IRaiseEventOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BB5 RID: 11189 RVA: 0x0007C896 File Offset: 0x0007AA96
		[return: Nullable(2)]
		public virtual TResult VisitLiteral(ILiteralOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BB6 RID: 11190 RVA: 0x0007C8A0 File Offset: 0x0007AAA0
		[return: Nullable(2)]
		public virtual TResult VisitConversion(IConversionOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BB7 RID: 11191 RVA: 0x0007C8AA File Offset: 0x0007AAAA
		[return: Nullable(2)]
		public virtual TResult VisitInvocation(IInvocationOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BB8 RID: 11192 RVA: 0x0007C8B4 File Offset: 0x0007AAB4
		[return: Nullable(2)]
		public virtual TResult VisitArrayElementReference(IArrayElementReferenceOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BB9 RID: 11193 RVA: 0x0007C8BE File Offset: 0x0007AABE
		[return: Nullable(2)]
		public virtual TResult VisitLocalReference(ILocalReferenceOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BBA RID: 11194 RVA: 0x0007C8C8 File Offset: 0x0007AAC8
		[return: Nullable(2)]
		public virtual TResult VisitParameterReference(IParameterReferenceOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BBB RID: 11195 RVA: 0x0007C8D2 File Offset: 0x0007AAD2
		[return: Nullable(2)]
		public virtual TResult VisitFieldReference(IFieldReferenceOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BBC RID: 11196 RVA: 0x0007C8DC File Offset: 0x0007AADC
		[return: Nullable(2)]
		public virtual TResult VisitMethodReference(IMethodReferenceOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BBD RID: 11197 RVA: 0x0007C8E6 File Offset: 0x0007AAE6
		[return: Nullable(2)]
		public virtual TResult VisitPropertyReference(IPropertyReferenceOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BBE RID: 11198 RVA: 0x0007C8F0 File Offset: 0x0007AAF0
		[return: Nullable(2)]
		public virtual TResult VisitEventReference(IEventReferenceOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BBF RID: 11199 RVA: 0x0007C8FA File Offset: 0x0007AAFA
		[return: Nullable(2)]
		public virtual TResult VisitUnaryOperator(IUnaryOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BC0 RID: 11200 RVA: 0x0007C904 File Offset: 0x0007AB04
		[return: Nullable(2)]
		public virtual TResult VisitBinaryOperator(IBinaryOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BC1 RID: 11201 RVA: 0x0007C90E File Offset: 0x0007AB0E
		[return: Nullable(2)]
		public virtual TResult VisitConditional(IConditionalOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BC2 RID: 11202 RVA: 0x0007C918 File Offset: 0x0007AB18
		[return: Nullable(2)]
		public virtual TResult VisitCoalesce(ICoalesceOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BC3 RID: 11203 RVA: 0x0007C922 File Offset: 0x0007AB22
		[return: Nullable(2)]
		public virtual TResult VisitAnonymousFunction(IAnonymousFunctionOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BC4 RID: 11204 RVA: 0x0007C92C File Offset: 0x0007AB2C
		[return: Nullable(2)]
		public virtual TResult VisitObjectCreation(IObjectCreationOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BC5 RID: 11205 RVA: 0x0007C936 File Offset: 0x0007AB36
		[return: Nullable(2)]
		public virtual TResult VisitTypeParameterObjectCreation(ITypeParameterObjectCreationOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BC6 RID: 11206 RVA: 0x0007C940 File Offset: 0x0007AB40
		[return: Nullable(2)]
		public virtual TResult VisitArrayCreation(IArrayCreationOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BC7 RID: 11207 RVA: 0x0007C94A File Offset: 0x0007AB4A
		[return: Nullable(2)]
		public virtual TResult VisitInstanceReference(IInstanceReferenceOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BC8 RID: 11208 RVA: 0x0007C954 File Offset: 0x0007AB54
		[return: Nullable(2)]
		public virtual TResult VisitIsType(IIsTypeOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BC9 RID: 11209 RVA: 0x0007C95E File Offset: 0x0007AB5E
		[return: Nullable(2)]
		public virtual TResult VisitAwait(IAwaitOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BCA RID: 11210 RVA: 0x0007C968 File Offset: 0x0007AB68
		[return: Nullable(2)]
		public virtual TResult VisitSimpleAssignment(ISimpleAssignmentOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BCB RID: 11211 RVA: 0x0007C972 File Offset: 0x0007AB72
		[return: Nullable(2)]
		public virtual TResult VisitCompoundAssignment(ICompoundAssignmentOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BCC RID: 11212 RVA: 0x0007C97C File Offset: 0x0007AB7C
		[return: Nullable(2)]
		public virtual TResult VisitParenthesized(IParenthesizedOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BCD RID: 11213 RVA: 0x0007C986 File Offset: 0x0007AB86
		[return: Nullable(2)]
		public virtual TResult VisitEventAssignment(IEventAssignmentOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BCE RID: 11214 RVA: 0x0007C990 File Offset: 0x0007AB90
		[return: Nullable(2)]
		public virtual TResult VisitConditionalAccess(IConditionalAccessOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BCF RID: 11215 RVA: 0x0007C99A File Offset: 0x0007AB9A
		[return: Nullable(2)]
		public virtual TResult VisitConditionalAccessInstance(IConditionalAccessInstanceOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BD0 RID: 11216 RVA: 0x0007C9A4 File Offset: 0x0007ABA4
		[return: Nullable(2)]
		public virtual TResult VisitInterpolatedString(IInterpolatedStringOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BD1 RID: 11217 RVA: 0x0007C9AE File Offset: 0x0007ABAE
		[return: Nullable(2)]
		public virtual TResult VisitAnonymousObjectCreation(IAnonymousObjectCreationOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BD2 RID: 11218 RVA: 0x0007C9B8 File Offset: 0x0007ABB8
		[return: Nullable(2)]
		public virtual TResult VisitObjectOrCollectionInitializer(IObjectOrCollectionInitializerOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BD3 RID: 11219 RVA: 0x0007C9C2 File Offset: 0x0007ABC2
		[return: Nullable(2)]
		public virtual TResult VisitMemberInitializer(IMemberInitializerOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BD4 RID: 11220 RVA: 0x0007C9CC File Offset: 0x0007ABCC
		[Obsolete("ICollectionElementInitializerOperation has been replaced with IInvocationOperation and IDynamicInvocationOperation", true)]
		[return: Nullable(2)]
		public virtual TResult VisitCollectionElementInitializer(ICollectionElementInitializerOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BD5 RID: 11221 RVA: 0x0007C9D6 File Offset: 0x0007ABD6
		[return: Nullable(2)]
		public virtual TResult VisitNameOf(INameOfOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BD6 RID: 11222 RVA: 0x0007C9E0 File Offset: 0x0007ABE0
		[return: Nullable(2)]
		public virtual TResult VisitTuple(ITupleOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BD7 RID: 11223 RVA: 0x0007C9EA File Offset: 0x0007ABEA
		[return: Nullable(2)]
		public virtual TResult VisitDynamicObjectCreation(IDynamicObjectCreationOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BD8 RID: 11224 RVA: 0x0007C9F4 File Offset: 0x0007ABF4
		[return: Nullable(2)]
		public virtual TResult VisitDynamicMemberReference(IDynamicMemberReferenceOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BD9 RID: 11225 RVA: 0x0007C9FE File Offset: 0x0007ABFE
		[return: Nullable(2)]
		public virtual TResult VisitDynamicInvocation(IDynamicInvocationOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BDA RID: 11226 RVA: 0x0007CA08 File Offset: 0x0007AC08
		[return: Nullable(2)]
		public virtual TResult VisitDynamicIndexerAccess(IDynamicIndexerAccessOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BDB RID: 11227 RVA: 0x0007CA12 File Offset: 0x0007AC12
		[return: Nullable(2)]
		public virtual TResult VisitTranslatedQuery(ITranslatedQueryOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BDC RID: 11228 RVA: 0x0007CA1C File Offset: 0x0007AC1C
		[return: Nullable(2)]
		public virtual TResult VisitDelegateCreation(IDelegateCreationOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BDD RID: 11229 RVA: 0x0007CA26 File Offset: 0x0007AC26
		[return: Nullable(2)]
		public virtual TResult VisitDefaultValue(IDefaultValueOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BDE RID: 11230 RVA: 0x0007CA30 File Offset: 0x0007AC30
		[return: Nullable(2)]
		public virtual TResult VisitTypeOf(ITypeOfOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BDF RID: 11231 RVA: 0x0007CA3A File Offset: 0x0007AC3A
		[return: Nullable(2)]
		public virtual TResult VisitSizeOf(ISizeOfOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BE0 RID: 11232 RVA: 0x0007CA44 File Offset: 0x0007AC44
		[return: Nullable(2)]
		public virtual TResult VisitAddressOf(IAddressOfOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BE1 RID: 11233 RVA: 0x0007CA4E File Offset: 0x0007AC4E
		[return: Nullable(2)]
		public virtual TResult VisitIsPattern(IIsPatternOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BE2 RID: 11234 RVA: 0x0007CA58 File Offset: 0x0007AC58
		[return: Nullable(2)]
		public virtual TResult VisitIncrementOrDecrement(IIncrementOrDecrementOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BE3 RID: 11235 RVA: 0x0007CA62 File Offset: 0x0007AC62
		[return: Nullable(2)]
		public virtual TResult VisitThrow(IThrowOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BE4 RID: 11236 RVA: 0x0007CA6C File Offset: 0x0007AC6C
		[return: Nullable(2)]
		public virtual TResult VisitDeconstructionAssignment(IDeconstructionAssignmentOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BE5 RID: 11237 RVA: 0x0007CA76 File Offset: 0x0007AC76
		[return: Nullable(2)]
		public virtual TResult VisitDeclarationExpression(IDeclarationExpressionOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BE6 RID: 11238 RVA: 0x0007CA80 File Offset: 0x0007AC80
		[return: Nullable(2)]
		public virtual TResult VisitOmittedArgument(IOmittedArgumentOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BE7 RID: 11239 RVA: 0x0007CA8A File Offset: 0x0007AC8A
		[return: Nullable(2)]
		public virtual TResult VisitFieldInitializer(IFieldInitializerOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BE8 RID: 11240 RVA: 0x0007CA94 File Offset: 0x0007AC94
		[return: Nullable(2)]
		public virtual TResult VisitVariableInitializer(IVariableInitializerOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BE9 RID: 11241 RVA: 0x0007CA9E File Offset: 0x0007AC9E
		[return: Nullable(2)]
		public virtual TResult VisitPropertyInitializer(IPropertyInitializerOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BEA RID: 11242 RVA: 0x0007CAA8 File Offset: 0x0007ACA8
		[return: Nullable(2)]
		public virtual TResult VisitParameterInitializer(IParameterInitializerOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BEB RID: 11243 RVA: 0x0007CAB2 File Offset: 0x0007ACB2
		[return: Nullable(2)]
		public virtual TResult VisitArrayInitializer(IArrayInitializerOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BEC RID: 11244 RVA: 0x0007CABC File Offset: 0x0007ACBC
		[return: Nullable(2)]
		public virtual TResult VisitVariableDeclarator(IVariableDeclaratorOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BED RID: 11245 RVA: 0x0007CAC6 File Offset: 0x0007ACC6
		[return: Nullable(2)]
		public virtual TResult VisitVariableDeclaration(IVariableDeclarationOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BEE RID: 11246 RVA: 0x0007CAD0 File Offset: 0x0007ACD0
		[return: Nullable(2)]
		public virtual TResult VisitArgument(IArgumentOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BEF RID: 11247 RVA: 0x0007CADA File Offset: 0x0007ACDA
		[return: Nullable(2)]
		public virtual TResult VisitCatchClause(ICatchClauseOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BF0 RID: 11248 RVA: 0x0007CAE4 File Offset: 0x0007ACE4
		[return: Nullable(2)]
		public virtual TResult VisitSwitchCase(ISwitchCaseOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BF1 RID: 11249 RVA: 0x0007CAEE File Offset: 0x0007ACEE
		[return: Nullable(2)]
		public virtual TResult VisitDefaultCaseClause(IDefaultCaseClauseOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BF2 RID: 11250 RVA: 0x0007CAF8 File Offset: 0x0007ACF8
		[return: Nullable(2)]
		public virtual TResult VisitPatternCaseClause(IPatternCaseClauseOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BF3 RID: 11251 RVA: 0x0007CB02 File Offset: 0x0007AD02
		[return: Nullable(2)]
		public virtual TResult VisitRangeCaseClause(IRangeCaseClauseOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BF4 RID: 11252 RVA: 0x0007CB0C File Offset: 0x0007AD0C
		[return: Nullable(2)]
		public virtual TResult VisitRelationalCaseClause(IRelationalCaseClauseOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BF5 RID: 11253 RVA: 0x0007CB16 File Offset: 0x0007AD16
		[return: Nullable(2)]
		public virtual TResult VisitSingleValueCaseClause(ISingleValueCaseClauseOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BF6 RID: 11254 RVA: 0x0007CB20 File Offset: 0x0007AD20
		[return: Nullable(2)]
		public virtual TResult VisitInterpolatedStringText(IInterpolatedStringTextOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BF7 RID: 11255 RVA: 0x0007CB2A File Offset: 0x0007AD2A
		[return: Nullable(2)]
		public virtual TResult VisitInterpolation(IInterpolationOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BF8 RID: 11256 RVA: 0x0007CB34 File Offset: 0x0007AD34
		[return: Nullable(2)]
		public virtual TResult VisitConstantPattern(IConstantPatternOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BF9 RID: 11257 RVA: 0x0007CB3E File Offset: 0x0007AD3E
		[return: Nullable(2)]
		public virtual TResult VisitDeclarationPattern(IDeclarationPatternOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BFA RID: 11258 RVA: 0x0007CB48 File Offset: 0x0007AD48
		[return: Nullable(2)]
		public virtual TResult VisitTupleBinaryOperator(ITupleBinaryOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BFB RID: 11259 RVA: 0x0007CB52 File Offset: 0x0007AD52
		[return: Nullable(2)]
		public virtual TResult VisitMethodBodyOperation(IMethodBodyOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BFC RID: 11260 RVA: 0x0007CB5C File Offset: 0x0007AD5C
		[return: Nullable(2)]
		public virtual TResult VisitConstructorBodyOperation(IConstructorBodyOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BFD RID: 11261 RVA: 0x0007CB66 File Offset: 0x0007AD66
		[return: Nullable(2)]
		public virtual TResult VisitDiscardOperation(IDiscardOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BFE RID: 11262 RVA: 0x0007CB70 File Offset: 0x0007AD70
		[return: Nullable(2)]
		public virtual TResult VisitFlowCapture(IFlowCaptureOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002BFF RID: 11263 RVA: 0x0007CB7A File Offset: 0x0007AD7A
		[return: Nullable(2)]
		public virtual TResult VisitFlowCaptureReference(IFlowCaptureReferenceOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002C00 RID: 11264 RVA: 0x0007CB84 File Offset: 0x0007AD84
		[return: Nullable(2)]
		public virtual TResult VisitIsNull(IIsNullOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002C01 RID: 11265 RVA: 0x0007CB8E File Offset: 0x0007AD8E
		[return: Nullable(2)]
		public virtual TResult VisitCaughtException(ICaughtExceptionOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002C02 RID: 11266 RVA: 0x0007CB98 File Offset: 0x0007AD98
		[return: Nullable(2)]
		public virtual TResult VisitStaticLocalInitializationSemaphore(IStaticLocalInitializationSemaphoreOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002C03 RID: 11267 RVA: 0x0007CBA2 File Offset: 0x0007ADA2
		[return: Nullable(2)]
		public virtual TResult VisitFlowAnonymousFunction(IFlowAnonymousFunctionOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002C04 RID: 11268 RVA: 0x0007CBAC File Offset: 0x0007ADAC
		[return: Nullable(2)]
		public virtual TResult VisitCoalesceAssignment(ICoalesceAssignmentOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002C05 RID: 11269 RVA: 0x0007CBB6 File Offset: 0x0007ADB6
		[return: Nullable(2)]
		public virtual TResult VisitRangeOperation(IRangeOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002C06 RID: 11270 RVA: 0x0007CBC0 File Offset: 0x0007ADC0
		[return: Nullable(2)]
		public virtual TResult VisitReDim(IReDimOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002C07 RID: 11271 RVA: 0x0007CBCA File Offset: 0x0007ADCA
		[return: Nullable(2)]
		public virtual TResult VisitReDimClause(IReDimClauseOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002C08 RID: 11272 RVA: 0x0007CBD4 File Offset: 0x0007ADD4
		[return: Nullable(2)]
		public virtual TResult VisitRecursivePattern(IRecursivePatternOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002C09 RID: 11273 RVA: 0x0007CBDE File Offset: 0x0007ADDE
		[return: Nullable(2)]
		public virtual TResult VisitDiscardPattern(IDiscardPatternOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002C0A RID: 11274 RVA: 0x0007CBE8 File Offset: 0x0007ADE8
		[return: Nullable(2)]
		public virtual TResult VisitSwitchExpression(ISwitchExpressionOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002C0B RID: 11275 RVA: 0x0007CBF2 File Offset: 0x0007ADF2
		[return: Nullable(2)]
		public virtual TResult VisitSwitchExpressionArm(ISwitchExpressionArmOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002C0C RID: 11276 RVA: 0x0007CBFC File Offset: 0x0007ADFC
		[return: Nullable(2)]
		public virtual TResult VisitPropertySubpattern(IPropertySubpatternOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002C0D RID: 11277 RVA: 0x0007CC06 File Offset: 0x0007AE06
		[return: Nullable(2)]
		internal virtual TResult VisitAggregateQuery(IAggregateQueryOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002C0E RID: 11278 RVA: 0x0007CC10 File Offset: 0x0007AE10
		[return: Nullable(2)]
		internal virtual TResult VisitNoPiaObjectCreation(INoPiaObjectCreationOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002C0F RID: 11279 RVA: 0x0007CC1A File Offset: 0x0007AE1A
		[return: Nullable(2)]
		internal virtual TResult VisitPlaceholder(IPlaceholderOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002C10 RID: 11280 RVA: 0x0007CC24 File Offset: 0x0007AE24
		[return: Nullable(2)]
		internal virtual TResult VisitPointerIndirectionReference(IPointerIndirectionReferenceOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002C11 RID: 11281 RVA: 0x0007CC2E File Offset: 0x0007AE2E
		[return: Nullable(2)]
		internal virtual TResult VisitWithStatement(IWithStatementOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002C12 RID: 11282 RVA: 0x0007CC38 File Offset: 0x0007AE38
		[return: Nullable(2)]
		public virtual TResult VisitUsingDeclaration(IUsingDeclarationOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002C13 RID: 11283 RVA: 0x0007CC42 File Offset: 0x0007AE42
		[return: Nullable(2)]
		public virtual TResult VisitNegatedPattern(INegatedPatternOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002C14 RID: 11284 RVA: 0x0007CC4C File Offset: 0x0007AE4C
		[return: Nullable(2)]
		public virtual TResult VisitBinaryPattern(IBinaryPatternOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002C15 RID: 11285 RVA: 0x0007CC56 File Offset: 0x0007AE56
		[return: Nullable(2)]
		public virtual TResult VisitTypePattern(ITypePatternOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002C16 RID: 11286 RVA: 0x0007CC60 File Offset: 0x0007AE60
		[return: Nullable(2)]
		public virtual TResult VisitRelationalPattern(IRelationalPatternOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002C17 RID: 11287 RVA: 0x0007CC6A File Offset: 0x0007AE6A
		[return: Nullable(2)]
		public virtual TResult VisitWith(IWithOperation operation, TArgument argument)
		{
			return this.DefaultVisit(operation, argument);
		}

		// Token: 0x06002C18 RID: 11288 RVA: 0x0007CC74 File Offset: 0x0007AE74
		[return: Nullable(2)]
		internal virtual TResult VisitFixed(IFixedOperation operation, TArgument argument)
		{
			return this.VisitNoneOperation(operation, argument);
		}
	}
}
