using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000539 RID: 1337
	internal class SourceFixedFieldSymbol : SourceMemberFieldSymbolFromDeclarator
	{
		// Token: 0x06007022 RID: 28706 RVA: 0x0019BC7C File Offset: 0x00199E7C
		internal SourceFixedFieldSymbol(SourceMemberContainerTypeSymbol containingType, VariableDeclaratorSyntax declarator, DeclarationModifiers modifiers, bool modifierErrors, BindingDiagnosticBag diagnostics)
			: base(containingType, declarator, modifiers, modifierErrors, diagnostics)
		{
		}

		// Token: 0x06007023 RID: 28707 RVA: 0x0019BC94 File Offset: 0x00199E94
		internal override void AddSynthesizedAttributes(PEModuleBuilder moduleBuilder, ref ArrayBuilder<SynthesizedAttributeData> attributes)
		{
			base.AddSynthesizedAttributes(moduleBuilder, ref attributes);
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			NamedTypeSymbol wellKnownType = declaringCompilation.GetWellKnownType(WellKnownType.System_Type);
			NamedTypeSymbol specialType = declaringCompilation.GetSpecialType(SpecialType.System_Int32);
			TypedConstant typedConstant = new TypedConstant(wellKnownType, TypedConstantKind.Type, ((PointerTypeSymbol)base.Type).PointedAtType);
			TypedConstant typedConstant2 = new TypedConstant(specialType, TypedConstantKind.Primitive, this.FixedSize);
			Symbol.AddSynthesizedAttribute(ref attributes, declaringCompilation.TrySynthesizeAttribute(WellKnownMember.System_Runtime_CompilerServices_FixedBufferAttribute__ctor, ImmutableArray.Create<TypedConstant>(typedConstant, typedConstant2), default(ImmutableArray<KeyValuePair<WellKnownMember, TypedConstant>>), false));
		}

		// Token: 0x17001E21 RID: 7713
		// (get) Token: 0x06007024 RID: 28708 RVA: 0x0019BD10 File Offset: 0x00199F10
		public sealed override int FixedSize
		{
			get
			{
				if (this._fixedSize == -1)
				{
					BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance();
					int num = 0;
					VariableDeclaratorSyntax variableDeclaratorNode = base.VariableDeclaratorNode;
					if (variableDeclaratorNode.ArgumentList != null)
					{
						SeparatedSyntaxList<ArgumentSyntax> arguments = variableDeclaratorNode.ArgumentList.Arguments;
						if (arguments.Count != 0 && arguments[0].Expression.Kind() != SyntaxKind.OmittedArraySizeExpression)
						{
							if (arguments.Count > 1)
							{
								instance.Add(ErrorCode.ERR_FixedBufferTooManyDimensions, variableDeclaratorNode.ArgumentList.Location);
							}
							ExpressionSyntax expression = arguments[0].Expression;
							Binder binder = this.DeclaringCompilation.GetBinderFactory(base.SyntaxTree, false).GetBinder(expression, null, null);
							binder = new ExecutableCodeBinder(expression, binder.ContainingMemberOrLambda, binder, null).GetBinder(expression);
							TypeSymbol specialType = binder.GetSpecialType(SpecialType.System_Int32, instance, expression);
							ConstantValue andValidateConstantValue = ConstantValueUtils.GetAndValidateConstantValue(binder.GenerateConversionForAssignment(specialType, binder.BindValue(expression, instance, Binder.BindValueKind.RValue), instance, false, false), this, specialType, expression.Location, instance);
							if (andValidateConstantValue.IsIntegral)
							{
								int int32Value = andValidateConstantValue.Int32Value;
								if (int32Value > 0)
								{
									num = int32Value;
									TypeSymbol pointedAtType = ((PointerTypeSymbol)base.Type).PointedAtType;
									if ((long)pointedAtType.FixedBufferElementSizeInBytes() * (long)int32Value > 2147483647L)
									{
										instance.Add(ErrorCode.ERR_FixedOverflow, expression.Location, new object[] { int32Value, pointedAtType });
									}
								}
								else
								{
									instance.Add(ErrorCode.ERR_InvalidFixedArraySize, expression.Location);
								}
							}
						}
					}
					if (Interlocked.CompareExchange(ref this._fixedSize, num, -1) == -1)
					{
						this.AddDeclarationDiagnostics(instance);
						this.state.NotePartComplete(CompletionPart.Members);
					}
					instance.Free();
				}
				return this._fixedSize;
			}
		}

		// Token: 0x06007025 RID: 28709 RVA: 0x0019BEC6 File Offset: 0x0019A0C6
		internal override NamedTypeSymbol FixedImplementationType(PEModuleBuilder emitModule)
		{
			return emitModule.SetFixedImplementationType(this);
		}

		// Token: 0x04001B79 RID: 7033
		private const int FixedSizeNotInitialized = -1;

		// Token: 0x04001B7A RID: 7034
		private int _fixedSize = -1;
	}
}
