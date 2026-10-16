using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000150 RID: 336
	internal class DefiniteAssignmentPass : LocalDataFlowPass<DefiniteAssignmentPass.LocalState, DefiniteAssignmentPass.LocalFunctionState>
	{
		// Token: 0x06001B0E RID: 6926 RVA: 0x0008D410 File Offset: 0x0008B610
		internal DefiniteAssignmentPass(CSharpCompilation compilation, Symbol member, BoundNode node, bool strictAnalysis, bool trackUnassignments = false, HashSet<PrefixUnaryExpressionSyntax> unassignedVariableAddressOfSyntaxes = null, bool requireOutParamsAssigned = true, bool trackClassFields = false, bool trackStaticMembers = false)
			: base(compilation, member, node, strictAnalysis ? EmptyStructTypeCache.CreatePrecise() : EmptyStructTypeCache.CreateForDev12Compatibility(compilation), trackUnassignments)
		{
			this.initiallyAssignedVariables = null;
			this._sourceAssembly = ((member == null) ? null : ((SourceAssemblySymbol)member.ContainingAssembly));
			this._unassignedVariableAddressOfSyntaxes = unassignedVariableAddressOfSyntaxes;
			this._requireOutParamsAssigned = requireOutParamsAssigned;
			this._trackClassFields = trackClassFields;
			this._trackStaticMembers = trackStaticMembers;
			this.topLevelMethod = member as MethodSymbol;
			this._shouldCheckConverted = base.GetType() == typeof(DefiniteAssignmentPass);
		}

		// Token: 0x06001B0F RID: 6927 RVA: 0x0008D50C File Offset: 0x0008B70C
		internal DefiniteAssignmentPass(CSharpCompilation compilation, Symbol member, BoundNode node, EmptyStructTypeCache emptyStructs, bool trackUnassignments = false, HashSet<Symbol> initiallyAssignedVariables = null)
			: base(compilation, member, node, emptyStructs, trackUnassignments)
		{
			this.initiallyAssignedVariables = initiallyAssignedVariables;
			this._sourceAssembly = ((member == null) ? null : ((SourceAssemblySymbol)member.ContainingAssembly));
			this.CurrentSymbol = member;
			this._unassignedVariableAddressOfSyntaxes = null;
			this._requireOutParamsAssigned = true;
			this.topLevelMethod = member as MethodSymbol;
			this._shouldCheckConverted = base.GetType() == typeof(DefiniteAssignmentPass);
		}

		// Token: 0x06001B10 RID: 6928 RVA: 0x0008D5F0 File Offset: 0x0008B7F0
		internal DefiniteAssignmentPass(CSharpCompilation compilation, Symbol member, BoundNode node, BoundNode firstInRegion, BoundNode lastInRegion, HashSet<Symbol> initiallyAssignedVariables, HashSet<PrefixUnaryExpressionSyntax> unassignedVariableAddressOfSyntaxes, bool trackUnassignments)
			: base(compilation, member, node, EmptyStructTypeCache.CreateNeverEmpty(), firstInRegion, lastInRegion, true, trackUnassignments)
		{
			this.initiallyAssignedVariables = initiallyAssignedVariables;
			this._sourceAssembly = null;
			this.CurrentSymbol = member;
			this._unassignedVariableAddressOfSyntaxes = unassignedVariableAddressOfSyntaxes;
			this._shouldCheckConverted = base.GetType() == typeof(DefiniteAssignmentPass);
		}

		// Token: 0x06001B11 RID: 6929 RVA: 0x0008D6B8 File Offset: 0x0008B8B8
		protected override void Free()
		{
			this.variableBySlot.Free();
			this._variableSlot.Free();
			this._usedVariables.Free();
			PooledHashSet<ParameterSymbol> readParameters = this._readParameters;
			if (readParameters != null)
			{
				readParameters.Free();
			}
			this._usedLocalFunctions.Free();
			this._writtenVariables.Free();
			this._capturedVariables.Free();
			this._capturedInside.Free();
			this._capturedOutside.Free();
			this._unsafeAddressTakenVariables.Free();
			base.Free();
		}

		// Token: 0x06001B12 RID: 6930 RVA: 0x0008D73F File Offset: 0x0008B93F
		protected override bool TryGetVariable(LocalDataFlowPass<DefiniteAssignmentPass.LocalState, DefiniteAssignmentPass.LocalFunctionState>.VariableIdentifier identifier, out int slot)
		{
			return this._variableSlot.TryGetValue(identifier, ref slot);
		}

		// Token: 0x06001B13 RID: 6931 RVA: 0x0008D750 File Offset: 0x0008B950
		protected override int AddVariable(LocalDataFlowPass<DefiniteAssignmentPass.LocalState, DefiniteAssignmentPass.LocalFunctionState>.VariableIdentifier identifier)
		{
			int count = this.variableBySlot.Count;
			this._variableSlot.Add(identifier, count);
			this.variableBySlot.Add(identifier);
			return count;
		}

		// Token: 0x06001B14 RID: 6932 RVA: 0x0008D784 File Offset: 0x0008B984
		protected Symbol GetNonMemberSymbol(int slot)
		{
			LocalDataFlowPass<DefiniteAssignmentPass.LocalState, DefiniteAssignmentPass.LocalFunctionState>.VariableIdentifier variableIdentifier = this.variableBySlot[slot];
			while (variableIdentifier.ContainingSlot > 0)
			{
				variableIdentifier = this.variableBySlot[variableIdentifier.ContainingSlot];
			}
			return variableIdentifier.Symbol;
		}

		// Token: 0x06001B15 RID: 6933 RVA: 0x0008D7C4 File Offset: 0x0008B9C4
		private int RootSlot(int slot)
		{
			for (;;)
			{
				int containingSlot = this.variableBySlot[slot].ContainingSlot;
				if (containingSlot == 0)
				{
					break;
				}
				slot = containingSlot;
			}
			return slot;
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x0008D7EC File Offset: 0x0008B9EC
		protected override bool ConvertInsufficientExecutionStackExceptionToCancelledByStackGuardException()
		{
			return this._convertInsufficientExecutionStackExceptionToCancelledByStackGuardException;
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x0008D7F4 File Offset: 0x0008B9F4
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1 })]
		protected override ImmutableArray<AbstractFlowPass<DefiniteAssignmentPass.LocalState, DefiniteAssignmentPass.LocalFunctionState>.PendingBranch> Scan(ref bool badRegion)
		{
			base.Diagnostics.Clear();
			ImmutableArray<ParameterSymbol> methodParameters = base.MethodParameters;
			ParameterSymbol methodThisParameter = base.MethodThisParameter;
			this._alreadyReported = BitVector.Empty;
			this.regionPlace = RegionPlace.Before;
			this.EnterParameters(methodParameters);
			if (methodThisParameter != null)
			{
				this.EnterParameter(methodThisParameter);
				if (methodThisParameter.Type.SpecialType != SpecialType.None)
				{
					int orCreateSlot = this.GetOrCreateSlot(methodThisParameter, 0, false, true);
					this.SetSlotState(orCreateSlot, true);
				}
			}
			ImmutableArray<AbstractFlowPass<DefiniteAssignmentPass.LocalState, DefiniteAssignmentPass.LocalFunctionState>.PendingBranch> immutableArray = base.Scan(ref badRegion);
			Location location;
			if (base.ShouldAnalyzeOutParameters(out location))
			{
				this.LeaveParameters(methodParameters, null, location);
				if (methodThisParameter != null)
				{
					this.LeaveParameter(methodThisParameter, null, location);
				}
				DefiniteAssignmentPass.LocalState state = this.State;
				foreach (AbstractFlowPass<DefiniteAssignmentPass.LocalState, DefiniteAssignmentPass.LocalFunctionState>.PendingBranch pendingBranch in immutableArray)
				{
					this.State = pendingBranch.State;
					this.LeaveParameters(methodParameters, pendingBranch.Branch.Syntax, null);
					if (methodThisParameter != null)
					{
						this.LeaveParameter(methodThisParameter, pendingBranch.Branch.Syntax, null);
					}
					this.Join(ref state, ref this.State);
				}
				this.State = state;
			}
			return immutableArray;
		}

		// Token: 0x06001B18 RID: 6936 RVA: 0x0008D900 File Offset: 0x0008BB00
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1 })]
		protected override ImmutableArray<AbstractFlowPass<DefiniteAssignmentPass.LocalState, DefiniteAssignmentPass.LocalFunctionState>.PendingBranch> RemoveReturns()
		{
			ImmutableArray<AbstractFlowPass<DefiniteAssignmentPass.LocalState, DefiniteAssignmentPass.LocalFunctionState>.PendingBranch> immutableArray = base.RemoveReturns();
			MethodSymbol methodSymbol = this.CurrentSymbol as MethodSymbol;
			if (methodSymbol != null && methodSymbol.IsAsync && !methodSymbol.IsImplicitlyDeclared)
			{
				if (!immutableArray.Any<AbstractFlowPass<DefiniteAssignmentPass.LocalState, DefiniteAssignmentPass.LocalFunctionState>.PendingBranch>((AbstractFlowPass<DefiniteAssignmentPass.LocalState, DefiniteAssignmentPass.LocalFunctionState>.PendingBranch pending) => DefiniteAssignmentPass.HasAwait(pending)))
				{
					LambdaSymbol lambdaSymbol = this.CurrentSymbol as LambdaSymbol;
					Location location = ((lambdaSymbol != null) ? lambdaSymbol.DiagnosticLocation : this.CurrentSymbol.Locations.FirstOrNone());
					base.Diagnostics.Add(ErrorCode.WRN_AsyncLacksAwaits, location);
				}
			}
			return immutableArray;
		}

		// Token: 0x06001B19 RID: 6937 RVA: 0x0008D998 File Offset: 0x0008BB98
		private static bool HasAwait([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] AbstractFlowPass<DefiniteAssignmentPass.LocalState, DefiniteAssignmentPass.LocalFunctionState>.PendingBranch pending)
		{
			BoundNode branch = pending.Branch;
			if (branch == null)
			{
				return false;
			}
			BoundKind kind = branch.Kind;
			if (kind <= BoundKind.UsingLocalDeclarations)
			{
				if (kind == BoundKind.AwaitExpression)
				{
					return true;
				}
				if (kind == BoundKind.UsingLocalDeclarations)
				{
					return ((BoundUsingLocalDeclarations)branch).AwaitOpt != null;
				}
			}
			else
			{
				if (kind == BoundKind.ForEachStatement)
				{
					return ((BoundForEachStatement)branch).AwaitOpt != null;
				}
				if (kind == BoundKind.UsingStatement)
				{
					return ((BoundUsingStatement)branch).AwaitOpt != null;
				}
			}
			return false;
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06001B1A RID: 6938 RVA: 0x0008DA05 File Offset: 0x0008BC05
		public sealed override bool AwaitUsingAndForeachAddsPendingBranch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x0008DA08 File Offset: 0x0008BC08
		protected virtual void ReportUnassignedOutParameter(ParameterSymbol parameter, SyntaxNode node, Location location)
		{
			if (!this._requireOutParamsAssigned && this.topLevelMethod == this.CurrentSymbol)
			{
				return;
			}
			if (base.Diagnostics != null && this.State.Reachable)
			{
				if (location == null)
				{
					location = new SourceLocation(node);
				}
				bool flag = false;
				if (parameter.IsThis)
				{
					int num = base.VariableSlot(parameter, 0);
					if (!this.State.IsAssigned(num))
					{
						foreach (FieldSymbol fieldSymbol in this._emptyStructTypeCache.GetStructInstanceFields(parameter.Type))
						{
							if (!this._emptyStructTypeCache.IsEmptyStructType(fieldSymbol.Type) && !LocalDataFlowPass<DefiniteAssignmentPass.LocalState, DefiniteAssignmentPass.LocalFunctionState>.HasInitializer(fieldSymbol))
							{
								int num2 = base.VariableSlot(fieldSymbol, num);
								if (num2 == -1 || !this.State.IsAssigned(num2))
								{
									Symbol associatedSymbol = fieldSymbol.AssociatedSymbol;
									if (associatedSymbol != null && associatedSymbol.Kind == SymbolKind.Property)
									{
										base.Diagnostics.Add(ErrorCode.ERR_UnassignedThisAutoProperty, location, new object[] { associatedSymbol });
									}
									else
									{
										base.Diagnostics.Add(ErrorCode.ERR_UnassignedThis, location, new object[] { fieldSymbol });
									}
								}
							}
						}
						flag = true;
					}
				}
				if (!flag)
				{
					base.Diagnostics.Add(ErrorCode.ERR_ParamUnassigned, location, new object[] { parameter.Name });
				}
			}
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x0008DB7C File Offset: 0x0008BD7C
		public static void Analyze(CSharpCompilation compilation, MethodSymbol member, BoundNode node, DiagnosticBag diagnostics, bool requireOutParamsAssigned = true)
		{
			DefiniteAssignmentPass.<>c__DisplayClass35_0 CS$<>8__locals1;
			CS$<>8__locals1.compilation = compilation;
			CS$<>8__locals1.member = member;
			CS$<>8__locals1.node = node;
			CS$<>8__locals1.requireOutParamsAssigned = requireOutParamsAssigned;
			CS$<>8__locals1.diagnostics = diagnostics;
			DiagnosticBag diagnosticBag = DefiniteAssignmentPass.<Analyze>g__analyze|35_1(true, ref CS$<>8__locals1);
			if (diagnosticBag.IsEmptyWithoutResolution)
			{
				diagnosticBag.Free();
				return;
			}
			DiagnosticBag diagnosticBag2 = DefiniteAssignmentPass.<Analyze>g__analyze|35_1(false, ref CS$<>8__locals1);
			if (Enumerable.Any<Diagnostic>(diagnosticBag2.AsEnumerable(), (Diagnostic d) => d.Code == 8078))
			{
				CS$<>8__locals1.diagnostics.AddRangeAndFree(diagnosticBag2);
				diagnosticBag.Free();
				return;
			}
			if (diagnosticBag.Count == diagnosticBag2.Count)
			{
				CS$<>8__locals1.diagnostics.AddRangeAndFree(diagnosticBag);
				diagnosticBag2.Free();
				return;
			}
			HashSet<Diagnostic> hashSet = new HashSet<Diagnostic>(diagnosticBag2.AsEnumerable(), DefiniteAssignmentPass.SameDiagnosticComparer.Instance);
			diagnosticBag2.Free();
			foreach (Diagnostic diagnostic in diagnosticBag.AsEnumerable())
			{
				if (diagnostic.Severity == DiagnosticSeverity.Error && !hashSet.Contains(diagnostic))
				{
					ErrorCode code = (ErrorCode)diagnostic.Code;
					ErrorCode errorCode;
					if (code <= ErrorCode.ERR_ParamUnassigned)
					{
						if (code <= ErrorCode.ERR_UseDefViolationField)
						{
							if (code != ErrorCode.ERR_UseDefViolation)
							{
								if (code != ErrorCode.ERR_UseDefViolationField)
								{
									goto IL_01C4;
								}
								errorCode = ErrorCode.WRN_UseDefViolationField;
							}
							else
							{
								errorCode = ErrorCode.WRN_UseDefViolation;
							}
						}
						else if (code != ErrorCode.ERR_UnassignedThis)
						{
							if (code != ErrorCode.ERR_ParamUnassigned)
							{
								goto IL_01C4;
							}
							errorCode = ErrorCode.WRN_ParamUnassigned;
						}
						else
						{
							errorCode = ErrorCode.WRN_UnassignedThis;
						}
					}
					else if (code <= ErrorCode.ERR_UseDefViolationOut)
					{
						if (code != ErrorCode.ERR_UseDefViolationThis)
						{
							if (code != ErrorCode.ERR_UseDefViolationOut)
							{
								goto IL_01C4;
							}
							errorCode = ErrorCode.WRN_UseDefViolationOut;
						}
						else
						{
							errorCode = ErrorCode.WRN_UseDefViolationThis;
						}
					}
					else if (code != ErrorCode.ERR_UnassignedThisAutoProperty)
					{
						if (code != ErrorCode.ERR_UseDefViolationProperty)
						{
							goto IL_01C4;
						}
						errorCode = ErrorCode.WRN_UseDefViolationProperty;
					}
					else
					{
						errorCode = ErrorCode.WRN_UnassignedThisAutoProperty;
					}
					IL_01C8:
					ErrorCode errorCode2 = errorCode;
					DiagnosticWithInfo diagnosticWithInfo = diagnostic as DiagnosticWithInfo;
					if (diagnosticWithInfo == null)
					{
						goto IL_01F1;
					}
					DiagnosticInfo info = diagnosticWithInfo.Info;
					if (info == null)
					{
						goto IL_01F1;
					}
					object[] arguments = info.Arguments;
					object[] array = arguments;
					IL_0201:
					object[] array2 = array;
					CS$<>8__locals1.diagnostics.Add(errorCode2, diagnostic.Location, array2);
					continue;
					IL_01F1:
					array = Enumerable.ToArray<object>(diagnostic.Arguments);
					goto IL_0201;
					IL_01C4:
					errorCode = code;
					goto IL_01C8;
				}
				CS$<>8__locals1.diagnostics.Add(diagnostic);
			}
			diagnosticBag.Free();
		}

		// Token: 0x06001B1D RID: 6941 RVA: 0x0008DDE0 File Offset: 0x0008BFE0
		protected void Analyze(ref bool badRegion, DiagnosticBag diagnostics)
		{
			base.Analyze(ref badRegion, default(Optional<DefiniteAssignmentPass.LocalState>));
			if (diagnostics != null)
			{
				foreach (Symbol symbol in this._capturedVariables)
				{
					Location location;
					if (this._unsafeAddressTakenVariables.TryGetValue(symbol, ref location))
					{
						diagnostics.Add(ErrorCode.ERR_LocalCantBeFixedAndHoisted, location, new object[] { symbol.Name });
					}
				}
				diagnostics.AddRange(base.Diagnostics);
				if (this.CurrentSymbol is SynthesizedRecordConstructor)
				{
					foreach (ParameterSymbol parameterSymbol in base.MethodParameters)
					{
						PooledHashSet<ParameterSymbol> readParameters = this._readParameters;
						if (readParameters == null || !readParameters.Contains(parameterSymbol))
						{
							diagnostics.Add(ErrorCode.WRN_UnreadRecordParameter, parameterSymbol.Locations.FirstOrNone(), new object[] { parameterSymbol.Name });
						}
					}
				}
			}
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x0008DEF0 File Offset: 0x0008C0F0
		private void CheckCaptured(Symbol variable, ParameterSymbol rangeVariableUnderlyingParameter = null)
		{
			SourceMethodSymbol sourceMethodSymbol = this.CurrentSymbol as SourceMethodSymbol;
			if (sourceMethodSymbol != null && Symbol.IsCaptured(rangeVariableUnderlyingParameter ?? variable, sourceMethodSymbol))
			{
				this.NoteCaptured(variable);
			}
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x0008DF24 File Offset: 0x0008C124
		private void NoteCaptured(Symbol variable)
		{
			if (this.regionPlace == RegionPlace.Inside)
			{
				this._capturedInside.Add(variable);
				this._capturedVariables.Add(variable);
				return;
			}
			if (variable.Kind != SymbolKind.RangeVariable)
			{
				this._capturedOutside.Add(variable);
				this._capturedVariables.Add(variable);
			}
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x0008DF79 File Offset: 0x0008C179
		protected IEnumerable<Symbol> GetCapturedInside()
		{
			return Enumerable.ToArray<Symbol>(this._capturedInside);
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x0008DF86 File Offset: 0x0008C186
		protected IEnumerable<Symbol> GetCapturedOutside()
		{
			return Enumerable.ToArray<Symbol>(this._capturedOutside);
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x0008DF93 File Offset: 0x0008C193
		protected IEnumerable<Symbol> GetCaptured()
		{
			return Enumerable.ToArray<Symbol>(this._capturedVariables);
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x0008DFA0 File Offset: 0x0008C1A0
		protected IEnumerable<Symbol> GetUnsafeAddressTaken()
		{
			return Enumerable.ToArray<Symbol>(this._unsafeAddressTakenVariables.Keys);
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x0008DFB2 File Offset: 0x0008C1B2
		protected IEnumerable<MethodSymbol> GetUsedLocalFunctions()
		{
			return Enumerable.ToArray<LocalFunctionSymbol>(this._usedLocalFunctions);
		}

		// Token: 0x06001B25 RID: 6949 RVA: 0x0008DFC0 File Offset: 0x0008C1C0
		private void NoteRecordParameterReadIfNeeded(Symbol symbol)
		{
			ParameterSymbol parameterSymbol = symbol as ParameterSymbol;
			if (parameterSymbol != null && symbol.ContainingSymbol is SynthesizedRecordConstructor)
			{
				if (this._readParameters == null)
				{
					this._readParameters = PooledHashSet<ParameterSymbol>.GetInstance();
				}
				this._readParameters.Add(parameterSymbol);
			}
		}

		// Token: 0x06001B26 RID: 6950 RVA: 0x0008E004 File Offset: 0x0008C204
		protected virtual void NoteRead(Symbol variable, ParameterSymbol rangeVariableUnderlyingParameter = null)
		{
			LocalSymbol localSymbol = variable as LocalSymbol;
			if (localSymbol != null)
			{
				this._usedVariables.Add(localSymbol);
			}
			this.NoteRecordParameterReadIfNeeded(variable);
			LocalFunctionSymbol localFunctionSymbol = variable as LocalFunctionSymbol;
			if (localFunctionSymbol != null)
			{
				this._usedLocalFunctions.Add(localFunctionSymbol);
			}
			if (variable != null)
			{
				if (this._sourceAssembly != null && variable.Kind == SymbolKind.Field)
				{
					this._sourceAssembly.NoteFieldAccess((FieldSymbol)variable.OriginalDefinition, true, false);
				}
				this.CheckCaptured(variable, rangeVariableUnderlyingParameter);
			}
		}

		// Token: 0x06001B27 RID: 6951 RVA: 0x0008E07C File Offset: 0x0008C27C
		private void NoteRead(BoundNode fieldOrEventAccess)
		{
			BoundNode boundNode = fieldOrEventAccess;
			while (boundNode != null)
			{
				BoundKind kind = boundNode.Kind;
				if (kind <= BoundKind.Local)
				{
					if (kind == BoundKind.ThisReference)
					{
						this.NoteRead(base.MethodThisParameter, null);
						return;
					}
					if (kind != BoundKind.Local)
					{
						return;
					}
					this.NoteRead(((BoundLocal)boundNode).LocalSymbol, null);
					return;
				}
				else
				{
					if (kind == BoundKind.Parameter)
					{
						this.NoteRead(((BoundParameter)boundNode).ParameterSymbol, null);
						return;
					}
					if (kind != BoundKind.FieldAccess)
					{
						if (kind != BoundKind.EventAccess)
						{
							return;
						}
						BoundEventAccess boundEventAccess = (BoundEventAccess)boundNode;
						FieldSymbol associatedField = boundEventAccess.EventSymbol.AssociatedField;
						if (associatedField != null)
						{
							this.NoteRead(associatedField, null);
							if (this.MayRequireTracking(boundEventAccess.ReceiverOpt, associatedField))
							{
								boundNode = boundEventAccess.ReceiverOpt;
								continue;
							}
						}
						return;
					}
					else
					{
						BoundFieldAccess boundFieldAccess = (BoundFieldAccess)boundNode;
						this.NoteRead(boundFieldAccess.FieldSymbol, null);
						if (!this.MayRequireTracking(boundFieldAccess.ReceiverOpt, boundFieldAccess.FieldSymbol))
						{
							return;
						}
						boundNode = boundFieldAccess.ReceiverOpt;
					}
				}
			}
		}

		// Token: 0x06001B28 RID: 6952 RVA: 0x0008E170 File Offset: 0x0008C370
		protected virtual void NoteWrite(Symbol variable, BoundExpression value, bool read)
		{
			if (variable != null)
			{
				this._writtenVariables.Add(variable);
				if (this._sourceAssembly != null && variable.Kind == SymbolKind.Field)
				{
					FieldSymbol fieldSymbol = (FieldSymbol)variable.OriginalDefinition;
					this._sourceAssembly.NoteFieldAccess(fieldSymbol, read && DefiniteAssignmentPass.WriteConsideredUse(fieldSymbol.Type, value), true);
				}
				LocalSymbol localSymbol = variable as LocalSymbol;
				if (localSymbol != null && read && DefiniteAssignmentPass.WriteConsideredUse(localSymbol.Type, value))
				{
					this._usedVariables.Add(localSymbol);
				}
				this.CheckCaptured(variable, null);
			}
		}

		// Token: 0x06001B29 RID: 6953 RVA: 0x0008E1FC File Offset: 0x0008C3FC
		internal static bool WriteConsideredUse(TypeSymbol type, BoundExpression value)
		{
			if (value == null || value.HasAnyErrors)
			{
				return true;
			}
			if (type != null && type.IsReferenceType && type.SpecialType != SpecialType.System_String)
			{
				return value.ConstantValue != ConstantValue.Null;
			}
			if (type != null && type.IsPointerOrFunctionPointer())
			{
				return true;
			}
			if (value != null && value.ConstantValue != null && value.Kind != BoundKind.InterpolatedString)
			{
				return false;
			}
			BoundKind kind = value.Kind;
			if (kind <= BoundKind.Conversion)
			{
				if (kind - BoundKind.DefaultLiteral <= 1)
				{
					return false;
				}
				if (kind == BoundKind.Conversion)
				{
					BoundConversion boundConversion = (BoundConversion)value;
					return boundConversion.ConversionKind.IsUserDefinedConversion() || boundConversion.ConversionKind == ConversionKind.IntPtr || DefiniteAssignmentPass.WriteConsideredUse(null, boundConversion.Operand);
				}
			}
			else
			{
				if (kind == BoundKind.ObjectCreationExpression)
				{
					BoundObjectCreationExpression boundObjectCreationExpression = (BoundObjectCreationExpression)value;
					return !boundObjectCreationExpression.Constructor.IsImplicitlyDeclared || boundObjectCreationExpression.InitializerExpressionOpt != null;
				}
				if (kind - BoundKind.TupleLiteral <= 1)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001B2A RID: 6954 RVA: 0x0008E2E4 File Offset: 0x0008C4E4
		private void NoteWrite(BoundExpression n, BoundExpression value, bool read)
		{
			while (n != null)
			{
				BoundKind kind = n.Kind;
				if (kind <= BoundKind.Parameter)
				{
					if (kind == BoundKind.ThisReference)
					{
						this.NoteWrite(base.MethodThisParameter, value, read);
						return;
					}
					switch (kind)
					{
					case BoundKind.Local:
						this.NoteWrite(((BoundLocal)n).LocalSymbol, value, read);
						return;
					case BoundKind.PseudoVariable:
						return;
					case BoundKind.RangeVariable:
						this.NoteWrite(((BoundRangeVariable)n).Value, value, read);
						return;
					case BoundKind.Parameter:
						this.NoteWrite(((BoundParameter)n).ParameterSymbol, value, read);
						return;
					default:
						return;
					}
				}
				else if (kind != BoundKind.FieldAccess)
				{
					if (kind != BoundKind.EventAccess)
					{
						return;
					}
					BoundEventAccess boundEventAccess = (BoundEventAccess)n;
					FieldSymbol associatedField = boundEventAccess.EventSymbol.AssociatedField;
					if (associatedField != null)
					{
						if (this._sourceAssembly != null)
						{
							FieldSymbol originalDefinition = associatedField.OriginalDefinition;
							this._sourceAssembly.NoteFieldAccess(originalDefinition, value == null || DefiniteAssignmentPass.WriteConsideredUse(associatedField.Type, value), true);
						}
						if (this.MayRequireTracking(boundEventAccess.ReceiverOpt, associatedField))
						{
							n = boundEventAccess.ReceiverOpt;
							continue;
						}
					}
					return;
				}
				else
				{
					BoundFieldAccess boundFieldAccess = (BoundFieldAccess)n;
					if (this._sourceAssembly != null)
					{
						FieldSymbol originalDefinition2 = boundFieldAccess.FieldSymbol.OriginalDefinition;
						this._sourceAssembly.NoteFieldAccess(originalDefinition2, value == null || DefiniteAssignmentPass.WriteConsideredUse(boundFieldAccess.FieldSymbol.Type, value), true);
					}
					if (!this.MayRequireTracking(boundFieldAccess.ReceiverOpt, boundFieldAccess.FieldSymbol))
					{
						return;
					}
					n = boundFieldAccess.ReceiverOpt;
					if (n.Kind == BoundKind.Local)
					{
						this._usedVariables.Add(((BoundLocal)n).LocalSymbol);
					}
				}
			}
		}

		// Token: 0x06001B2B RID: 6955 RVA: 0x0008E474 File Offset: 0x0008C674
		protected override void Normalize(ref DefiniteAssignmentPass.LocalState state)
		{
			int capacity = state.Assigned.Capacity;
			int count = this.variableBySlot.Count;
			state.Assigned.EnsureCapacity(count);
			for (int i = capacity; i < count; i++)
			{
				int containingSlot = this.variableBySlot[i].ContainingSlot;
				bool flag = containingSlot > 0 && state.Assigned[containingSlot] && this.variableBySlot[containingSlot].Symbol.GetTypeOrReturnType().TypeKind == TypeKind.Struct;
				if (state.NormalizeToBottom && containingSlot == 0)
				{
					flag = true;
				}
				state.Assigned[i] = flag;
			}
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x0008E514 File Offset: 0x0008C714
		protected override bool TryGetReceiverAndMember(BoundExpression expr, out BoundExpression receiver, out Symbol member)
		{
			receiver = null;
			member = null;
			switch (expr.Kind)
			{
			case BoundKind.FieldAccess:
			{
				BoundFieldAccess boundFieldAccess = (BoundFieldAccess)expr;
				FieldSymbol fieldSymbol = boundFieldAccess.FieldSymbol;
				member = fieldSymbol;
				if (fieldSymbol.IsFixedSizeBuffer)
				{
					return false;
				}
				if (fieldSymbol.IsStatic)
				{
					return this._trackStaticMembers;
				}
				receiver = boundFieldAccess.ReceiverOpt;
				break;
			}
			case BoundKind.PropertyAccess:
			{
				BoundPropertyAccess boundPropertyAccess = (BoundPropertyAccess)expr;
				if (Binder.AccessingAutoPropertyFromConstructor(boundPropertyAccess, this.CurrentSymbol))
				{
					PropertySymbol propertySymbol = boundPropertyAccess.PropertySymbol;
					SourcePropertySymbolBase sourcePropertySymbolBase = propertySymbol as SourcePropertySymbolBase;
					member = ((sourcePropertySymbolBase != null) ? sourcePropertySymbolBase.BackingField : null);
					if (member == null)
					{
						return false;
					}
					if (propertySymbol.IsStatic)
					{
						return this._trackStaticMembers;
					}
					receiver = boundPropertyAccess.ReceiverOpt;
				}
				break;
			}
			case BoundKind.EventAccess:
			{
				BoundEventAccess boundEventAccess = (BoundEventAccess)expr;
				EventSymbol eventSymbol = boundEventAccess.EventSymbol;
				member = eventSymbol.AssociatedField;
				if (eventSymbol.IsStatic)
				{
					return this._trackStaticMembers;
				}
				receiver = boundEventAccess.ReceiverOpt;
				break;
			}
			}
			return member != null && receiver != null && receiver.Kind != BoundKind.TypeExpression && this.MayRequireTrackingReceiverType(receiver.Type);
		}

		// Token: 0x06001B2D RID: 6957 RVA: 0x0008E62E File Offset: 0x0008C82E
		private bool MayRequireTrackingReceiverType(TypeSymbol type)
		{
			return type != null && (this._trackClassFields || type.TypeKind == TypeKind.Struct);
		}

		// Token: 0x06001B2E RID: 6958 RVA: 0x0008E649 File Offset: 0x0008C849
		protected bool MayRequireTracking(BoundExpression receiverOpt, FieldSymbol fieldSymbol)
		{
			return fieldSymbol != null && receiverOpt != null && !fieldSymbol.IsStatic && !fieldSymbol.IsFixedSizeBuffer && receiverOpt.Kind != BoundKind.TypeExpression && this.MayRequireTrackingReceiverType(receiverOpt.Type) && !receiverOpt.Type.IsPrimitiveRecursiveStruct();
		}

		// Token: 0x06001B2F RID: 6959 RVA: 0x0008E68C File Offset: 0x0008C88C
		protected void CheckAssigned(Symbol symbol, SyntaxNode node)
		{
			if (symbol != null)
			{
				this.NoteRead(symbol, null);
				if (this.State.Reachable)
				{
					int num = base.VariableSlot(symbol, 0);
					if (num >= this.State.Assigned.Capacity)
					{
						this.Normalize(ref this.State);
					}
					if (num > 0 && !this.State.IsAssigned(num))
					{
						this.ReportUnassignedIfNotCapturedInLocalFunction(symbol, node, num, true);
					}
				}
			}
		}

		// Token: 0x06001B30 RID: 6960 RVA: 0x0008E6F5 File Offset: 0x0008C8F5
		private void ReportUnassignedIfNotCapturedInLocalFunction(Symbol symbol, SyntaxNode node, int slot, bool skipIfUseBeforeDeclaration = true)
		{
			if (this.IsCapturedInLocalFunction(slot))
			{
				this.RecordReadInLocalFunction(slot);
				return;
			}
			this.ReportUnassigned(symbol, node, slot, skipIfUseBeforeDeclaration);
		}

		// Token: 0x06001B31 RID: 6961 RVA: 0x0008E714 File Offset: 0x0008C914
		protected virtual void ReportUnassigned(Symbol symbol, SyntaxNode node, int slot, bool skipIfUseBeforeDeclaration)
		{
			if (slot <= 0)
			{
				return;
			}
			LocalSymbol localSymbol = symbol as LocalSymbol;
			if (localSymbol != null && localSymbol.IsConst)
			{
				return;
			}
			if (slot >= this._alreadyReported.Capacity)
			{
				this._alreadyReported.EnsureCapacity(this.variableBySlot.Count);
			}
			if ((!skipIfUseBeforeDeclaration || symbol.Kind != SymbolKind.Local || (symbol.Locations.Length != 0 && node.Span.End >= symbol.Locations.FirstOrNone().SourceSpan.Start)) && !this._alreadyReported[slot] && !symbol.GetTypeOrReturnType().Type.IsErrorType())
			{
				string text = symbol.Name;
				ErrorCode errorCode;
				if (symbol.Kind == SymbolKind.Field)
				{
					Symbol associatedSymbol = ((FieldSymbol)symbol).AssociatedSymbol;
					if (associatedSymbol != null && associatedSymbol.Kind == SymbolKind.Property)
					{
						errorCode = ErrorCode.ERR_UseDefViolationProperty;
						text = associatedSymbol.Name;
					}
					else
					{
						errorCode = ErrorCode.ERR_UseDefViolationField;
					}
				}
				else if (symbol.Kind == SymbolKind.Parameter && ((ParameterSymbol)symbol).RefKind == RefKind.Out)
				{
					if (((ParameterSymbol)symbol).IsThis)
					{
						errorCode = ErrorCode.ERR_UseDefViolationThis;
					}
					else
					{
						errorCode = ErrorCode.ERR_UseDefViolationOut;
					}
				}
				else
				{
					errorCode = ErrorCode.ERR_UseDefViolation;
				}
				base.Diagnostics.Add(errorCode, new SourceLocation(node), new object[] { text });
			}
			this._alreadyReported[slot] = true;
		}

		// Token: 0x06001B32 RID: 6962 RVA: 0x0008E884 File Offset: 0x0008CA84
		protected virtual void CheckAssigned(BoundExpression expr, FieldSymbol fieldSymbol, SyntaxNode node)
		{
			int num;
			if (this.State.Reachable && !this.IsAssigned(expr, out num))
			{
				this.ReportUnassignedIfNotCapturedInLocalFunction(fieldSymbol, node, num, true);
			}
			this.NoteRead(expr);
		}

		// Token: 0x06001B33 RID: 6963 RVA: 0x0008E8BC File Offset: 0x0008CABC
		private bool IsAssigned(BoundExpression node, out int unassignedSlot)
		{
			unassignedSlot = -1;
			if (this._emptyStructTypeCache.IsEmptyStructType(node.Type))
			{
				return true;
			}
			BoundKind kind = node.Kind;
			if (kind != BoundKind.ThisReference)
			{
				switch (kind)
				{
				case BoundKind.Local:
					unassignedSlot = this.GetOrCreateSlot(((BoundLocal)node).LocalSymbol, 0, false, true);
					goto IL_01B6;
				case BoundKind.PseudoVariable:
				case BoundKind.RangeVariable:
					break;
				case BoundKind.Parameter:
				{
					BoundParameter boundParameter = (BoundParameter)node;
					unassignedSlot = this.GetOrCreateSlot(boundParameter.ParameterSymbol, 0, false, true);
					goto IL_01B6;
				}
				default:
					switch (kind)
					{
					case BoundKind.FieldAccess:
					{
						BoundFieldAccess boundFieldAccess = (BoundFieldAccess)node;
						if (!this.MayRequireTracking(boundFieldAccess.ReceiverOpt, boundFieldAccess.FieldSymbol) || this.IsAssigned(boundFieldAccess.ReceiverOpt, out unassignedSlot))
						{
							return true;
						}
						unassignedSlot = this.GetOrCreateSlot(boundFieldAccess.FieldSymbol, unassignedSlot, false, true);
						goto IL_01B6;
					}
					case BoundKind.PropertyAccess:
					{
						BoundPropertyAccess boundPropertyAccess = (BoundPropertyAccess)node;
						if (Binder.AccessingAutoPropertyFromConstructor(boundPropertyAccess, this.CurrentSymbol))
						{
							SourcePropertySymbolBase sourcePropertySymbolBase = boundPropertyAccess.PropertySymbol as SourcePropertySymbolBase;
							SynthesizedBackingFieldSymbol synthesizedBackingFieldSymbol = ((sourcePropertySymbolBase != null) ? sourcePropertySymbolBase.BackingField : null);
							if (synthesizedBackingFieldSymbol != null)
							{
								if (!this.MayRequireTracking(boundPropertyAccess.ReceiverOpt, synthesizedBackingFieldSymbol) || this.IsAssigned(boundPropertyAccess.ReceiverOpt, out unassignedSlot))
								{
									return true;
								}
								unassignedSlot = this.GetOrCreateSlot(synthesizedBackingFieldSymbol, unassignedSlot, false, true);
								goto IL_01B6;
							}
						}
						break;
					}
					case BoundKind.EventAccess:
					{
						BoundEventAccess boundEventAccess = (BoundEventAccess)node;
						if (!this.MayRequireTracking(boundEventAccess.ReceiverOpt, boundEventAccess.EventSymbol.AssociatedField) || this.IsAssigned(boundEventAccess.ReceiverOpt, out unassignedSlot))
						{
							return true;
						}
						unassignedSlot = this.GetOrCreateSlot(boundEventAccess.EventSymbol.AssociatedField, unassignedSlot, false, true);
						goto IL_01B6;
					}
					}
					break;
				}
				unassignedSlot = -1;
				return true;
			}
			if (base.MethodThisParameter == null)
			{
				unassignedSlot = -1;
				return true;
			}
			unassignedSlot = this.GetOrCreateSlot(base.MethodThisParameter, 0, false, true);
			IL_01B6:
			return unassignedSlot <= 0 || this.State.IsAssigned(unassignedSlot);
		}

		// Token: 0x06001B34 RID: 6964 RVA: 0x0008EA94 File Offset: 0x0008CC94
		private Symbol UseNonFieldSymbolUnsafely(BoundExpression expression)
		{
			while (expression != null)
			{
				BoundKind kind = expression.Kind;
				switch (kind)
				{
				case BoundKind.ThisReference:
					return base.MethodThisParameter;
				case BoundKind.PreviousSubmissionReference:
				case BoundKind.HostObjectMemberReference:
				case BoundKind.PseudoVariable:
					break;
				case BoundKind.BaseReference:
					return base.MethodThisParameter;
				case BoundKind.Local:
				{
					LocalSymbol localSymbol = ((BoundLocal)expression).LocalSymbol;
					this._usedVariables.Add(localSymbol);
					return localSymbol;
				}
				case BoundKind.RangeVariable:
					return ((BoundRangeVariable)expression).RangeVariableSymbol;
				case BoundKind.Parameter:
					return ((BoundParameter)expression).ParameterSymbol;
				default:
					if (kind == BoundKind.FieldAccess)
					{
						BoundFieldAccess boundFieldAccess = (BoundFieldAccess)expression;
						FieldSymbol fieldSymbol = boundFieldAccess.FieldSymbol;
						if (this._sourceAssembly != null)
						{
							this._sourceAssembly.NoteFieldAccess(fieldSymbol, true, true);
						}
						if (fieldSymbol.ContainingType.IsReferenceType || fieldSymbol.IsStatic)
						{
							return null;
						}
						expression = boundFieldAccess.ReceiverOpt;
						continue;
					}
					break;
				}
				return null;
			}
			return null;
		}

		// Token: 0x06001B35 RID: 6965 RVA: 0x0008EB70 File Offset: 0x0008CD70
		protected void Assign(BoundNode node, BoundExpression value, bool isRef = false, bool read = true)
		{
			this.AssignImpl(node, value, isRef, true, read);
		}

		// Token: 0x06001B36 RID: 6966 RVA: 0x0008EB80 File Offset: 0x0008CD80
		protected virtual void AssignImpl(BoundNode node, BoundExpression value, bool isRef, bool written, bool read)
		{
			BoundKind kind = node.Kind;
			if (kind <= BoundKind.Parameter)
			{
				if (kind <= BoundKind.LocalDeclaration)
				{
					if (kind != BoundKind.BadExpression)
					{
						if (kind != BoundKind.LocalDeclaration)
						{
							return;
						}
						LocalSymbol localSymbol = ((BoundLocalDeclaration)node).LocalSymbol;
						int orCreateSlot = this.GetOrCreateSlot(localSymbol, 0, false, true);
						this.SetSlotState(orCreateSlot, written || !this.State.Reachable);
						if (written)
						{
							this.NoteWrite(localSymbol, value, read);
							return;
						}
						return;
					}
					else
					{
						BoundBadExpression boundBadExpression = (BoundBadExpression)node;
						if (!boundBadExpression.ChildBoundNodes.IsDefault && boundBadExpression.ChildBoundNodes.Length == 1)
						{
							this.AssignImpl(boundBadExpression.ChildBoundNodes[0], value, isRef, written, read);
							return;
						}
						return;
					}
				}
				else if (kind != BoundKind.ThisReference)
				{
					switch (kind)
					{
					case BoundKind.Local:
					{
						BoundLocal boundLocal = (BoundLocal)node;
						if (boundLocal.LocalSymbol.RefKind != RefKind.None && !isRef)
						{
							if (written)
							{
								this.VisitRvalue(boundLocal, true);
								return;
							}
							return;
						}
						else
						{
							int num = this.MakeSlot(boundLocal);
							this.SetSlotState(num, written);
							if (written)
							{
								this.NoteWrite(boundLocal, value, read);
								return;
							}
							return;
						}
						break;
					}
					case BoundKind.PseudoVariable:
						return;
					case BoundKind.RangeVariable:
						this.AssignImpl(((BoundRangeVariable)node).Value, value, isRef, written, read);
						return;
					case BoundKind.Parameter:
					{
						BoundParameter boundParameter = (BoundParameter)node;
						ParameterSymbol parameterSymbol = boundParameter.ParameterSymbol;
						if (isRef && parameterSymbol.RefKind == RefKind.Out)
						{
							this.LeaveParameter(parameterSymbol, node.Syntax, boundParameter.Syntax.Location);
						}
						int num2 = this.MakeSlot(boundParameter);
						this.SetSlotState(num2, written);
						if (written)
						{
							this.NoteWrite(boundParameter, value, read);
							return;
						}
						return;
					}
					default:
						return;
					}
				}
			}
			else if (kind <= BoundKind.FieldAccess)
			{
				if (kind - BoundKind.TupleLiteral <= 1)
				{
					((BoundTupleExpression)node).VisitAllElements<DefiniteAssignmentPass>(delegate([global::System.Runtime.CompilerServices.Nullable(1)] BoundExpression x, DefiniteAssignmentPass self)
					{
						self.Assign(x, null, isRef, true);
					}, this);
					return;
				}
				if (kind != BoundKind.FieldAccess)
				{
					return;
				}
			}
			else if (kind - BoundKind.PropertyAccess > 1)
			{
				if (kind != BoundKind.DeclarationPattern)
				{
					if (kind != BoundKind.RecursivePattern)
					{
						return;
					}
					BoundRecursivePattern boundRecursivePattern = (BoundRecursivePattern)node;
					LocalSymbol localSymbol2 = boundRecursivePattern.Variable as LocalSymbol;
					if (localSymbol2 != null)
					{
						int orCreateSlot2 = this.GetOrCreateSlot(localSymbol2, 0, false, true);
						this.SetSlotState(orCreateSlot2, written || !this.State.Reachable);
					}
					if (written)
					{
						this.NoteWrite(boundRecursivePattern.VariableAccess, value, read);
						return;
					}
					return;
				}
				else
				{
					BoundDeclarationPattern boundDeclarationPattern = (BoundDeclarationPattern)node;
					LocalSymbol localSymbol3 = boundDeclarationPattern.Variable as LocalSymbol;
					if (localSymbol3 != null)
					{
						int orCreateSlot3 = this.GetOrCreateSlot(localSymbol3, 0, false, true);
						this.SetSlotState(orCreateSlot3, written || !this.State.Reachable);
					}
					if (written)
					{
						this.NoteWrite(boundDeclarationPattern.VariableAccess, value, read);
						return;
					}
					return;
				}
			}
			BoundExpression boundExpression = (BoundExpression)node;
			int num3 = this.MakeSlot(boundExpression);
			this.SetSlotState(num3, written);
			if (written)
			{
				this.NoteWrite(boundExpression, value, read);
				return;
			}
		}

		// Token: 0x06001B37 RID: 6967 RVA: 0x0008EE88 File Offset: 0x0008D088
		private bool FieldsAllSet(int containingSlot, DefiniteAssignmentPass.LocalState state)
		{
			TypeSymbol type = this.variableBySlot[containingSlot].Symbol.GetTypeOrReturnType().Type;
			foreach (FieldSymbol fieldSymbol in this._emptyStructTypeCache.GetStructInstanceFields(type))
			{
				if (!this._emptyStructTypeCache.IsEmptyStructType(fieldSymbol.Type) && !(fieldSymbol is TupleErrorFieldSymbol))
				{
					int num = base.VariableSlot(fieldSymbol, containingSlot);
					if (num == -1 || !state.IsAssigned(num))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06001B38 RID: 6968 RVA: 0x0008EF34 File Offset: 0x0008D134
		protected void SetSlotState(int slot, bool assigned)
		{
			if (slot <= 0)
			{
				return;
			}
			if (assigned)
			{
				this.SetSlotAssigned(slot);
				return;
			}
			this.SetSlotUnassigned(slot);
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x0008EF50 File Offset: 0x0008D150
		protected void SetSlotAssigned(int slot, ref DefiniteAssignmentPass.LocalState state)
		{
			if (slot < 0)
			{
				return;
			}
			LocalDataFlowPass<DefiniteAssignmentPass.LocalState, DefiniteAssignmentPass.LocalFunctionState>.VariableIdentifier variableIdentifier = this.variableBySlot[slot];
			TypeSymbol type = variableIdentifier.Symbol.GetTypeOrReturnType().Type;
			if (slot >= state.Assigned.Capacity)
			{
				this.Normalize(ref state);
			}
			if (state.IsAssigned(slot))
			{
				return;
			}
			state.Assign(slot);
			if (!EmptyStructTypeCache.IsTrackableStructType(type))
			{
				goto IL_00D1;
			}
			using (IEnumerator<FieldSymbol> enumerator = this._emptyStructTypeCache.GetStructInstanceFields(type).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					FieldSymbol fieldSymbol = enumerator.Current;
					int num = base.VariableSlot(fieldSymbol, slot);
					if (num > 0)
					{
						this.SetSlotAssigned(num, ref state);
					}
				}
				goto IL_00D1;
			}
			IL_009D:
			slot = variableIdentifier.ContainingSlot;
			if (state.IsAssigned(slot) || !this.FieldsAllSet(slot, state))
			{
				return;
			}
			state.Assign(slot);
			variableIdentifier = this.variableBySlot[slot];
			IL_00D1:
			if (variableIdentifier.ContainingSlot > 0)
			{
				goto IL_009D;
			}
		}

		// Token: 0x06001B3A RID: 6970 RVA: 0x0008F048 File Offset: 0x0008D248
		private void SetSlotAssigned(int slot)
		{
			this.SetSlotAssigned(slot, ref this.State);
		}

		// Token: 0x06001B3B RID: 6971 RVA: 0x0008F058 File Offset: 0x0008D258
		private void SetSlotUnassigned(int slot, ref DefiniteAssignmentPass.LocalState state)
		{
			if (slot < 0)
			{
				return;
			}
			LocalDataFlowPass<DefiniteAssignmentPass.LocalState, DefiniteAssignmentPass.LocalFunctionState>.VariableIdentifier variableIdentifier = this.variableBySlot[slot];
			TypeSymbol type = variableIdentifier.Symbol.GetTypeOrReturnType().Type;
			if (!state.IsAssigned(slot))
			{
				return;
			}
			state.Unassign(slot);
			if (!EmptyStructTypeCache.IsTrackableStructType(type))
			{
				goto IL_00A4;
			}
			using (IEnumerator<FieldSymbol> enumerator = this._emptyStructTypeCache.GetStructInstanceFields(type).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					FieldSymbol fieldSymbol = enumerator.Current;
					int num = base.VariableSlot(fieldSymbol, slot);
					if (num > 0)
					{
						this.SetSlotUnassigned(num, ref state);
					}
				}
				goto IL_00A4;
			}
			IL_0088:
			slot = variableIdentifier.ContainingSlot;
			state.Unassign(slot);
			variableIdentifier = this.variableBySlot[slot];
			IL_00A4:
			if (variableIdentifier.ContainingSlot <= 0)
			{
				return;
			}
			goto IL_0088;
		}

		// Token: 0x06001B3C RID: 6972 RVA: 0x0008F124 File Offset: 0x0008D324
		private void SetSlotUnassigned(int slot)
		{
			if (this.NonMonotonicState.HasValue)
			{
				DefiniteAssignmentPass.LocalState value = this.NonMonotonicState.Value;
				this.SetSlotUnassigned(slot, ref value);
				this.NonMonotonicState = value;
			}
			this.SetSlotUnassigned(slot, ref this.State);
		}

		// Token: 0x06001B3D RID: 6973 RVA: 0x0008F16C File Offset: 0x0008D36C
		protected override DefiniteAssignmentPass.LocalState TopState()
		{
			return new DefiniteAssignmentPass.LocalState(BitVector.Empty, false);
		}

		// Token: 0x06001B3E RID: 6974 RVA: 0x0008F17C File Offset: 0x0008D37C
		protected override DefiniteAssignmentPass.LocalState ReachableBottomState()
		{
			DefiniteAssignmentPass.LocalState localState = new DefiniteAssignmentPass.LocalState(BitVector.AllSet(this.variableBySlot.Count), false);
			localState.Assigned[0] = false;
			return localState;
		}

		// Token: 0x06001B3F RID: 6975 RVA: 0x0008F1B0 File Offset: 0x0008D3B0
		protected override void EnterParameter(ParameterSymbol parameter)
		{
			int orCreateSlot = this.GetOrCreateSlot(parameter, 0, false, true);
			if (parameter.RefKind == RefKind.Out)
			{
				MethodSymbol methodSymbol = this.CurrentSymbol as MethodSymbol;
				if (methodSymbol == null || !methodSymbol.IsAsync)
				{
					if (orCreateSlot > 0)
					{
						int num = orCreateSlot;
						HashSet<Symbol> hashSet = this.initiallyAssignedVariables;
						this.SetSlotState(num, hashSet != null && hashSet.Contains(parameter));
						return;
					}
					return;
				}
			}
			if (orCreateSlot > 0)
			{
				this.SetSlotState(orCreateSlot, true);
			}
			this.NoteWrite(parameter, null, true);
		}

		// Token: 0x06001B40 RID: 6976 RVA: 0x0008F21C File Offset: 0x0008D41C
		protected override void LeaveParameters(ImmutableArray<ParameterSymbol> parameters, SyntaxNode syntax, Location location)
		{
			if (!this.State.Reachable)
			{
				return;
			}
			base.LeaveParameters(parameters, syntax, location);
		}

		// Token: 0x06001B41 RID: 6977 RVA: 0x0008F238 File Offset: 0x0008D438
		protected override void LeaveParameter(ParameterSymbol parameter, SyntaxNode syntax, Location location)
		{
			if (parameter.RefKind != RefKind.None)
			{
				int num = base.VariableSlot(parameter, 0);
				if (num > 0 && !this.State.IsAssigned(num))
				{
					this.ReportUnassignedOutParameter(parameter, syntax, location);
				}
				this.NoteRead(parameter, null);
			}
		}

		// Token: 0x06001B42 RID: 6978 RVA: 0x0008F27C File Offset: 0x0008D47C
		protected override DefiniteAssignmentPass.LocalState UnreachableState()
		{
			DefiniteAssignmentPass.LocalState localState = this.State.Clone();
			localState.Assigned.EnsureCapacity(1);
			localState.Assign(0);
			return localState;
		}

		// Token: 0x06001B43 RID: 6979 RVA: 0x0008F2AC File Offset: 0x0008D4AC
		public override void VisitPattern(BoundPattern pattern)
		{
			base.VisitPattern(pattern);
			DefiniteAssignmentPass.LocalState stateWhenFalse = this.StateWhenFalse;
			base.SetState(this.StateWhenTrue);
			this.AssignPatternVariables(pattern, true);
			base.SetConditionalState(this.State, stateWhenFalse);
		}

		// Token: 0x06001B44 RID: 6980 RVA: 0x0008F2E8 File Offset: 0x0008D4E8
		private void AssignPatternVariables(BoundPattern pattern, bool definitely = true)
		{
			switch (pattern.Kind)
			{
			case BoundKind.ConstantPattern:
			{
				BoundConstantPattern boundConstantPattern = (BoundConstantPattern)pattern;
				this.VisitRvalue(boundConstantPattern.Value, false);
				return;
			}
			case BoundKind.DiscardPattern:
			case BoundKind.TypePattern:
				return;
			case BoundKind.DeclarationPattern:
			{
				BoundDeclarationPattern boundDeclarationPattern = (BoundDeclarationPattern)pattern;
				if (definitely)
				{
					this.Assign(boundDeclarationPattern, null, false, false);
					return;
				}
				return;
			}
			case BoundKind.RecursivePattern:
			{
				BoundRecursivePattern boundRecursivePattern = (BoundRecursivePattern)pattern;
				if (!boundRecursivePattern.Deconstruction.IsDefaultOrEmpty)
				{
					foreach (BoundSubpattern boundSubpattern in boundRecursivePattern.Deconstruction)
					{
						this.AssignPatternVariables(boundSubpattern.Pattern, definitely);
					}
				}
				if (!boundRecursivePattern.Properties.IsDefaultOrEmpty)
				{
					foreach (BoundSubpattern boundSubpattern2 in boundRecursivePattern.Properties)
					{
						this.AssignPatternVariables(boundSubpattern2.Pattern, definitely);
					}
				}
				if (definitely)
				{
					this.Assign(boundRecursivePattern, null, false, false);
					return;
				}
				return;
			}
			case BoundKind.ITuplePattern:
				foreach (BoundSubpattern boundSubpattern3 in ((BoundITuplePattern)pattern).Subpatterns)
				{
					this.AssignPatternVariables(boundSubpattern3.Pattern, definitely);
				}
				return;
			case BoundKind.BinaryPattern:
			{
				BoundBinaryPattern boundBinaryPattern = (BoundBinaryPattern)pattern;
				bool flag = definitely && !boundBinaryPattern.Disjunction;
				this.AssignPatternVariables(boundBinaryPattern.Left, flag);
				this.AssignPatternVariables(boundBinaryPattern.Right, flag);
				return;
			}
			case BoundKind.NegatedPattern:
			{
				BoundNegatedPattern boundNegatedPattern = (BoundNegatedPattern)pattern;
				this.AssignPatternVariables(boundNegatedPattern.Negated, false);
				return;
			}
			case BoundKind.RelationalPattern:
			{
				BoundRelationalPattern boundRelationalPattern = (BoundRelationalPattern)pattern;
				this.VisitRvalue(boundRelationalPattern.Value, false);
				return;
			}
			}
			throw ExceptionUtilities.UnexpectedValue(pattern.Kind);
		}

		// Token: 0x06001B45 RID: 6981 RVA: 0x0008F4B4 File Offset: 0x0008D6B4
		public override BoundNode VisitBlock(BoundBlock node)
		{
			this.DeclareVariables(node.Locals);
			this.VisitStatementsWithLocalFunctions(node);
			foreach (LocalSymbol localSymbol in node.Locals)
			{
				if (localSymbol.IsUsing)
				{
					this.NoteRead(localSymbol, null);
				}
			}
			this.ReportUnusedVariables(node.Locals);
			this.ReportUnusedVariables(node.LocalFunctions);
			return null;
		}

		// Token: 0x06001B46 RID: 6982 RVA: 0x0008F520 File Offset: 0x0008D720
		private void VisitStatementsWithLocalFunctions(BoundBlock block)
		{
			if (!this.TrackingRegions && !block.LocalFunctions.IsDefaultOrEmpty)
			{
				foreach (BoundStatement boundStatement in block.Statements)
				{
					if (boundStatement.Kind == BoundKind.LocalFunctionStatement)
					{
						base.VisitAlways(boundStatement);
					}
				}
				foreach (BoundStatement boundStatement2 in block.Statements)
				{
					if (boundStatement2.Kind != BoundKind.LocalFunctionStatement)
					{
						this.VisitStatement(boundStatement2);
					}
				}
				return;
			}
			foreach (BoundStatement boundStatement3 in block.Statements)
			{
				this.VisitStatement(boundStatement3);
			}
		}

		// Token: 0x06001B47 RID: 6983 RVA: 0x0008F5DB File Offset: 0x0008D7DB
		public override BoundNode VisitSwitchStatement(BoundSwitchStatement node)
		{
			this.DeclareVariables(node.InnerLocals);
			BoundNode boundNode = base.VisitSwitchStatement(node);
			this.ReportUnusedVariables(node.InnerLocals);
			this.ReportUnusedVariables(node.InnerLocalFunctions);
			return boundNode;
		}

		// Token: 0x06001B48 RID: 6984 RVA: 0x0008F608 File Offset: 0x0008D808
		protected override void VisitSwitchSection(BoundSwitchSection node, bool isLastSection)
		{
			this.DeclareVariables(node.Locals);
			base.VisitSwitchSection(node, isLastSection);
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x0008F61E File Offset: 0x0008D81E
		public override BoundNode VisitForStatement(BoundForStatement node)
		{
			this.DeclareVariables(node.OuterLocals);
			this.DeclareVariables(node.InnerLocals);
			BoundNode boundNode = base.VisitForStatement(node);
			this.ReportUnusedVariables(node.InnerLocals);
			this.ReportUnusedVariables(node.OuterLocals);
			return boundNode;
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x0008F657 File Offset: 0x0008D857
		public override BoundNode VisitDoStatement(BoundDoStatement node)
		{
			this.DeclareVariables(node.Locals);
			BoundNode boundNode = base.VisitDoStatement(node);
			this.ReportUnusedVariables(node.Locals);
			return boundNode;
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x0008F678 File Offset: 0x0008D878
		public override BoundNode VisitWhileStatement(BoundWhileStatement node)
		{
			this.DeclareVariables(node.Locals);
			BoundNode boundNode = base.VisitWhileStatement(node);
			this.ReportUnusedVariables(node.Locals);
			return boundNode;
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x0008F69C File Offset: 0x0008D89C
		public override BoundNode VisitUsingStatement(BoundUsingStatement node)
		{
			ImmutableArray<LocalSymbol> locals = node.Locals;
			this.DeclareVariables(locals);
			BoundNode boundNode = base.VisitUsingStatement(node);
			if (!locals.IsDefaultOrEmpty)
			{
				foreach (LocalSymbol localSymbol in locals)
				{
					if (localSymbol.DeclarationKind == LocalDeclarationKind.UsingVariable)
					{
						this.NoteRead(localSymbol, null);
					}
				}
			}
			return boundNode;
		}

		// Token: 0x06001B4D RID: 6989 RVA: 0x0008F6F5 File Offset: 0x0008D8F5
		public override BoundNode VisitFixedStatement(BoundFixedStatement node)
		{
			this.DeclareVariables(node.Locals);
			return base.VisitFixedStatement(node);
		}

		// Token: 0x06001B4E RID: 6990 RVA: 0x0008F70A File Offset: 0x0008D90A
		public override BoundNode VisitSequence(BoundSequence node)
		{
			this.DeclareVariables(node.Locals);
			BoundNode boundNode = base.VisitSequence(node);
			this.ReportUnusedVariables(node.Locals);
			return boundNode;
		}

		// Token: 0x06001B4F RID: 6991 RVA: 0x0008F72C File Offset: 0x0008D92C
		private void DeclareVariables(ImmutableArray<LocalSymbol> locals)
		{
			foreach (LocalSymbol localSymbol in locals)
			{
				this.DeclareVariable(localSymbol);
			}
		}

		// Token: 0x06001B50 RID: 6992 RVA: 0x0008F75C File Offset: 0x0008D95C
		private void DeclareVariable(LocalSymbol symbol)
		{
			bool flag;
			if (!symbol.IsConst)
			{
				HashSet<Symbol> hashSet = this.initiallyAssignedVariables;
				flag = hashSet != null && hashSet.Contains(symbol);
			}
			else
			{
				flag = true;
			}
			bool flag2 = flag;
			this.SetSlotState(this.GetOrCreateSlot(symbol, 0, false, true), flag2);
		}

		// Token: 0x06001B51 RID: 6993 RVA: 0x0008F79C File Offset: 0x0008D99C
		private void ReportUnusedVariables(ImmutableArray<LocalSymbol> locals)
		{
			foreach (LocalSymbol localSymbol in locals)
			{
				this.ReportIfUnused(localSymbol, true);
			}
		}

		// Token: 0x06001B52 RID: 6994 RVA: 0x0008F7CC File Offset: 0x0008D9CC
		private void ReportIfUnused(LocalSymbol symbol, bool assigned)
		{
			if (!this._usedVariables.Contains(symbol) && symbol.DeclarationKind != LocalDeclarationKind.PatternVariable && !string.IsNullOrEmpty(symbol.Name))
			{
				base.Diagnostics.Add((assigned && this._writtenVariables.Contains(symbol)) ? ErrorCode.WRN_UnreferencedVarAssg : ErrorCode.WRN_UnreferencedVar, symbol.Locations.FirstOrNone(), new object[] { symbol.Name });
			}
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x0008F840 File Offset: 0x0008DA40
		private void ReportUnusedVariables(ImmutableArray<LocalFunctionSymbol> locals)
		{
			foreach (LocalFunctionSymbol localFunctionSymbol in locals)
			{
				this.ReportIfUnused(localFunctionSymbol);
			}
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x0008F870 File Offset: 0x0008DA70
		private void ReportIfUnused(LocalFunctionSymbol symbol)
		{
			if (!this._usedLocalFunctions.Contains(symbol) && !string.IsNullOrEmpty(symbol.Name))
			{
				base.Diagnostics.Add(ErrorCode.WRN_UnreferencedLocalFunction, symbol.Locations.FirstOrNone(), new object[] { symbol.Name });
			}
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x0008F8C4 File Offset: 0x0008DAC4
		public override BoundNode VisitLocal(BoundLocal node)
		{
			LocalSymbol localSymbol = node.LocalSymbol;
			SourceLocalSymbol sourceLocalSymbol = localSymbol as SourceLocalSymbol;
			if (sourceLocalSymbol != null && sourceLocalSymbol.IsVar)
			{
				SyntaxNode forbiddenZone = localSymbol.ForbiddenZone;
				if (forbiddenZone != null && forbiddenZone.Contains(node.Syntax))
				{
					int orCreateSlot = this.GetOrCreateSlot(node.LocalSymbol, 0, false, true);
					if (orCreateSlot > 0)
					{
						this._alreadyReported[orCreateSlot] = true;
					}
				}
			}
			this.CheckAssigned(localSymbol, node.Syntax);
			if (localSymbol.IsFixed)
			{
				MethodSymbol methodSymbol = this.CurrentSymbol as MethodSymbol;
				if (methodSymbol != null && (methodSymbol.MethodKind == MethodKind.AnonymousFunction || methodSymbol.MethodKind == MethodKind.LocalFunction) && this._capturedVariables.Contains(localSymbol))
				{
					base.Diagnostics.Add(ErrorCode.ERR_FixedLocalInLambda, new SourceLocation(node.Syntax), new object[] { localSymbol });
				}
			}
			return null;
		}

		// Token: 0x06001B56 RID: 6998 RVA: 0x0008F994 File Offset: 0x0008DB94
		public override BoundNode VisitLocalDeclaration(BoundLocalDeclaration node)
		{
			this.GetOrCreateSlot(node.LocalSymbol, 0, false, true);
			HashSet<Symbol> hashSet = this.initiallyAssignedVariables;
			if (hashSet != null && hashSet.Contains(node.LocalSymbol))
			{
				this.Assign(node, null, false, true);
			}
			BoundNode boundNode = base.VisitLocalDeclaration(node);
			if (node.InitializerOpt != null)
			{
				this.Assign(node, node.InitializerOpt, false, true);
			}
			return boundNode;
		}

		// Token: 0x06001B57 RID: 6999 RVA: 0x0008F9F4 File Offset: 0x0008DBF4
		public override BoundNode VisitMethodGroup(BoundMethodGroup node)
		{
			foreach (MethodSymbol methodSymbol in node.Methods)
			{
				if (methodSymbol.MethodKind == MethodKind.LocalFunction)
				{
					this._usedLocalFunctions.Add((LocalFunctionSymbol)methodSymbol);
				}
			}
			return base.VisitMethodGroup(node);
		}

		// Token: 0x06001B58 RID: 7000 RVA: 0x0008FA48 File Offset: 0x0008DC48
		public override BoundNode VisitLambda(BoundLambda node)
		{
			Symbol currentSymbol = this.CurrentSymbol;
			this.CurrentSymbol = node.Symbol;
			AbstractFlowPass<DefiniteAssignmentPass.LocalState, DefiniteAssignmentPass.LocalFunctionState>.SavedPending savedPending = base.SavePending();
			DefiniteAssignmentPass.LocalState state = this.State;
			this.State = (this.State.Reachable ? this.State.Clone() : this.ReachableBottomState());
			if (!node.WasCompilerGenerated)
			{
				this.EnterParameters(node.Symbol.Parameters);
			}
			AbstractFlowPass<DefiniteAssignmentPass.LocalState, DefiniteAssignmentPass.LocalFunctionState>.SavedPending savedPending2 = base.SavePending();
			base.VisitAlways(node.Body);
			base.RestorePending(savedPending2);
			ImmutableArray<AbstractFlowPass<DefiniteAssignmentPass.LocalState, DefiniteAssignmentPass.LocalFunctionState>.PendingBranch> immutableArray = this.RemoveReturns();
			base.RestorePending(savedPending);
			this.LeaveParameters(node.Symbol.Parameters, node.Syntax, null);
			this.Join(ref state, ref this.State);
			foreach (AbstractFlowPass<DefiniteAssignmentPass.LocalState, DefiniteAssignmentPass.LocalFunctionState>.PendingBranch pendingBranch in immutableArray)
			{
				this.State = pendingBranch.State;
				if (pendingBranch.Branch.Kind == BoundKind.ReturnStatement)
				{
					this.LeaveParameters(node.Symbol.Parameters, pendingBranch.Branch.Syntax, null);
				}
				this.Join(ref state, ref this.State);
			}
			this.State = state;
			this.CurrentSymbol = currentSymbol;
			return null;
		}

		// Token: 0x06001B59 RID: 7001 RVA: 0x0008FB7C File Offset: 0x0008DD7C
		public override BoundNode VisitThisReference(BoundThisReference node)
		{
			this.CheckAssigned(base.MethodThisParameter, node.Syntax);
			return null;
		}

		// Token: 0x06001B5A RID: 7002 RVA: 0x0008FB91 File Offset: 0x0008DD91
		public override BoundNode VisitParameter(BoundParameter node)
		{
			if (!node.WasCompilerGenerated)
			{
				this.CheckAssigned(node.ParameterSymbol, node.Syntax);
			}
			else
			{
				this.NoteRecordParameterReadIfNeeded(node.ParameterSymbol);
			}
			return null;
		}

		// Token: 0x06001B5B RID: 7003 RVA: 0x0008FBBC File Offset: 0x0008DDBC
		public override BoundNode VisitAssignmentOperator(BoundAssignmentOperator node)
		{
			base.VisitAssignmentOperator(node);
			this.Assign(node.Left, node.Right, node.IsRef, true);
			return null;
		}

		// Token: 0x06001B5C RID: 7004 RVA: 0x0008FBE0 File Offset: 0x0008DDE0
		public override BoundNode VisitDeconstructionAssignmentOperator(BoundDeconstructionAssignmentOperator node)
		{
			base.VisitDeconstructionAssignmentOperator(node);
			this.Assign(node.Left, node.Right, false, true);
			return null;
		}

		// Token: 0x06001B5D RID: 7005 RVA: 0x0008FBFF File Offset: 0x0008DDFF
		public override BoundNode VisitIncrementOperator(BoundIncrementOperator node)
		{
			base.VisitIncrementOperator(node);
			this.Assign(node.Operand, node, false, true);
			return null;
		}

		// Token: 0x06001B5E RID: 7006 RVA: 0x0008FC19 File Offset: 0x0008DE19
		public override BoundNode VisitCompoundAssignmentOperator(BoundCompoundAssignmentOperator node)
		{
			base.VisitCompoundAssignmentTarget(node);
			this.VisitRvalue(node.Right, false);
			base.AfterRightHasBeenVisited(node);
			this.Assign(node.Left, node, false, true);
			return null;
		}

		// Token: 0x06001B5F RID: 7007 RVA: 0x0008FC48 File Offset: 0x0008DE48
		public override BoundNode VisitFixedLocalCollectionInitializer(BoundFixedLocalCollectionInitializer node)
		{
			BoundExpression boundExpression = node.Expression;
			if (boundExpression.Kind == BoundKind.AddressOfOperator)
			{
				boundExpression = ((BoundAddressOfOperator)boundExpression).Operand;
			}
			base.VisitAddressOfOperand(boundExpression, false);
			return null;
		}

		// Token: 0x06001B60 RID: 7008 RVA: 0x0008FC7C File Offset: 0x0008DE7C
		public override BoundNode VisitAddressOfOperator(BoundAddressOfOperator node)
		{
			BoundExpression operand = node.Operand;
			bool flag = false;
			Symbol symbol = this.UseNonFieldSymbolUnsafely(operand);
			if (symbol != null)
			{
				HashSet<PrefixUnaryExpressionSyntax> unassignedVariableAddressOfSyntaxes = this._unassignedVariableAddressOfSyntaxes;
				if (unassignedVariableAddressOfSyntaxes != null && !unassignedVariableAddressOfSyntaxes.Contains(node.Syntax as PrefixUnaryExpressionSyntax))
				{
					flag = true;
				}
				if (!this._unsafeAddressTakenVariables.ContainsKey(symbol))
				{
					this._unsafeAddressTakenVariables.Add(symbol, node.Syntax.Location);
				}
			}
			base.VisitAddressOfOperand(node.Operand, flag);
			return null;
		}

		// Token: 0x06001B61 RID: 7009 RVA: 0x0008FCF4 File Offset: 0x0008DEF4
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		protected override void WriteArgument(BoundExpression arg, RefKind refKind, MethodSymbol method)
		{
			if (refKind == RefKind.Ref)
			{
				this.CheckAssigned(arg, arg.Syntax);
			}
			this.Assign(arg, null, false, true);
			if (refKind != RefKind.None && (method == null || method.IsExtern))
			{
				TypeSymbol type = arg.Type;
				if (type != null)
				{
					this.MarkFieldsUsed(type);
				}
			}
		}

		// Token: 0x06001B62 RID: 7010 RVA: 0x0008FD3C File Offset: 0x0008DF3C
		protected void CheckAssigned(BoundExpression expr, SyntaxNode node)
		{
			if (!this.State.Reachable)
			{
				return;
			}
			this.MakeSlot(expr);
			BoundKind kind = expr.Kind;
			if (kind <= BoundKind.Parameter)
			{
				switch (kind)
				{
				case BoundKind.ThisReference:
				case BoundKind.BaseReference:
					this.CheckAssigned(base.MethodThisParameter, node);
					break;
				case BoundKind.PreviousSubmissionReference:
				case BoundKind.HostObjectMemberReference:
					break;
				case BoundKind.Local:
					this.CheckAssigned(((BoundLocal)expr).LocalSymbol, node);
					return;
				default:
					if (kind != BoundKind.Parameter)
					{
						return;
					}
					this.CheckAssigned(((BoundParameter)expr).ParameterSymbol, node);
					return;
				}
			}
			else if (kind != BoundKind.FieldAccess)
			{
				if (kind != BoundKind.EventAccess)
				{
					return;
				}
				BoundEventAccess boundEventAccess = (BoundEventAccess)expr;
				FieldSymbol associatedField = boundEventAccess.EventSymbol.AssociatedField;
				if (associatedField != null && this.MayRequireTracking(boundEventAccess.ReceiverOpt, associatedField))
				{
					this.CheckAssigned(boundEventAccess, associatedField, node);
					return;
				}
			}
			else
			{
				BoundFieldAccess boundFieldAccess = (BoundFieldAccess)expr;
				FieldSymbol fieldSymbol = boundFieldAccess.FieldSymbol;
				if (!fieldSymbol.IsFixedSizeBuffer && this.MayRequireTracking(boundFieldAccess.ReceiverOpt, fieldSymbol))
				{
					this.CheckAssigned(expr, fieldSymbol, node);
					return;
				}
			}
		}

		// Token: 0x06001B63 RID: 7011 RVA: 0x0008FE38 File Offset: 0x0008E038
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		private void MarkFieldsUsed(TypeSymbol type)
		{
			TypeKind typeKind = type.TypeKind;
			if (typeKind == TypeKind.Array)
			{
				this.MarkFieldsUsed(((ArrayTypeSymbol)type).ElementType);
				return;
			}
			if (typeKind != TypeKind.Class && typeKind != TypeKind.Struct)
			{
				return;
			}
			if (!type.IsFromCompilation(this.compilation))
			{
				return;
			}
			SourceAssemblySymbol sourceAssemblySymbol = type.ContainingAssembly as SourceAssemblySymbol;
			if (sourceAssemblySymbol == null)
			{
				return;
			}
			if (sourceAssemblySymbol.TypesReferencedInExternalMethods.Add(type))
			{
				foreach (Symbol symbol in ((NamedTypeSymbol)type).GetMembersUnordered())
				{
					if (symbol.Kind == SymbolKind.Field)
					{
						FieldSymbol fieldSymbol = (FieldSymbol)symbol;
						sourceAssemblySymbol.NoteFieldAccess(fieldSymbol, true, true);
						this.MarkFieldsUsed(fieldSymbol.Type);
					}
				}
			}
		}

		// Token: 0x06001B64 RID: 7012 RVA: 0x0008FEEB File Offset: 0x0008E0EB
		public override BoundNode VisitBaseReference(BoundBaseReference node)
		{
			this.CheckAssigned(base.MethodThisParameter, node.Syntax);
			return null;
		}

		// Token: 0x06001B65 RID: 7013 RVA: 0x0008FF00 File Offset: 0x0008E100
		protected override void VisitCatchBlock(BoundCatchBlock catchBlock, ref DefiniteAssignmentPass.LocalState finallyState)
		{
			this.DeclareVariables(catchBlock.Locals);
			BoundExpression exceptionSourceOpt = catchBlock.ExceptionSourceOpt;
			if (exceptionSourceOpt != null)
			{
				this.Assign(exceptionSourceOpt, null, false, false);
			}
			base.VisitCatchBlock(catchBlock, ref finallyState);
			foreach (LocalSymbol localSymbol in catchBlock.Locals)
			{
				this.ReportIfUnused(localSymbol, localSymbol.DeclarationKind != LocalDeclarationKind.CatchVariable);
			}
		}

		// Token: 0x06001B66 RID: 7014 RVA: 0x0008FF6C File Offset: 0x0008E16C
		public override BoundNode VisitFieldAccess(BoundFieldAccess node)
		{
			BoundNode boundNode = base.VisitFieldAccess(node);
			this.NoteRead(node.FieldSymbol, null);
			if (node.FieldSymbol.IsFixedSizeBuffer && node.Syntax != null && !SyntaxFacts.IsFixedStatementExpression(node.Syntax))
			{
				Symbol symbol = this.UseNonFieldSymbolUnsafely(node.ReceiverOpt);
				if (symbol != null)
				{
					this.CheckCaptured(symbol, null);
					if (!this._unsafeAddressTakenVariables.ContainsKey(symbol))
					{
						this._unsafeAddressTakenVariables.Add(symbol, node.Syntax.Location);
						return boundNode;
					}
				}
			}
			else if (this.MayRequireTracking(node.ReceiverOpt, node.FieldSymbol))
			{
				this.CheckAssigned(node, node.FieldSymbol, node.Syntax);
			}
			return boundNode;
		}

		// Token: 0x06001B67 RID: 7015 RVA: 0x00090014 File Offset: 0x0008E214
		public override BoundNode VisitPropertyAccess(BoundPropertyAccess node)
		{
			BoundNode boundNode = base.VisitPropertyAccess(node);
			if (Binder.AccessingAutoPropertyFromConstructor(node, this.CurrentSymbol))
			{
				SourcePropertySymbolBase sourcePropertySymbolBase = node.PropertySymbol as SourcePropertySymbolBase;
				SynthesizedBackingFieldSymbol synthesizedBackingFieldSymbol = ((sourcePropertySymbolBase != null) ? sourcePropertySymbolBase.BackingField : null);
				int num;
				if (synthesizedBackingFieldSymbol != null && this.MayRequireTracking(node.ReceiverOpt, synthesizedBackingFieldSymbol) && this.State.Reachable && !this.IsAssigned(node, out num))
				{
					this.ReportUnassignedIfNotCapturedInLocalFunction(synthesizedBackingFieldSymbol, node.Syntax, num, true);
				}
			}
			return boundNode;
		}

		// Token: 0x06001B68 RID: 7016 RVA: 0x00090090 File Offset: 0x0008E290
		public override BoundNode VisitEventAccess(BoundEventAccess node)
		{
			BoundNode boundNode = base.VisitEventAccess(node);
			FieldSymbol associatedField = node.EventSymbol.AssociatedField;
			if (associatedField != null)
			{
				this.NoteRead(associatedField, null);
				if (this.MayRequireTracking(node.ReceiverOpt, associatedField))
				{
					this.CheckAssigned(node, associatedField, node.Syntax);
				}
			}
			return boundNode;
		}

		// Token: 0x06001B69 RID: 7017 RVA: 0x000900D8 File Offset: 0x0008E2D8
		public override void VisitForEachIterationVariables(BoundForEachStatement node)
		{
			foreach (LocalSymbol localSymbol in node.IterationVariables)
			{
				int orCreateSlot = this.GetOrCreateSlot(localSymbol, 0, false, true);
				if (orCreateSlot > 0)
				{
					this.SetSlotAssigned(orCreateSlot);
				}
				this.NoteWrite(localSymbol, null, true);
			}
		}

		// Token: 0x06001B6A RID: 7018 RVA: 0x00090128 File Offset: 0x0008E328
		public override BoundNode VisitObjectInitializerMember(BoundObjectInitializerMember node)
		{
			BoundNode boundNode = base.VisitObjectInitializerMember(node);
			if (this._sourceAssembly != null && node.MemberSymbol != null && node.MemberSymbol.Kind == SymbolKind.Field)
			{
				this._sourceAssembly.NoteFieldAccess((FieldSymbol)node.MemberSymbol.OriginalDefinition, false, true);
			}
			return boundNode;
		}

		// Token: 0x06001B6B RID: 7019 RVA: 0x0009017D File Offset: 0x0008E37D
		public override BoundNode VisitDynamicObjectInitializerMember(BoundDynamicObjectInitializerMember node)
		{
			return null;
		}

		// Token: 0x06001B6C RID: 7020 RVA: 0x00090180 File Offset: 0x0008E380
		protected override void VisitAssignmentOfNullCoalescingAssignment(BoundNullCoalescingAssignmentOperator node, BoundPropertyAccess propertyAccessOpt)
		{
			base.VisitAssignmentOfNullCoalescingAssignment(node, propertyAccessOpt);
			this.Assign(node.LeftOperand, node.RightOperand, false, true);
		}

		// Token: 0x06001B6D RID: 7021 RVA: 0x0009019E File Offset: 0x0008E39E
		protected override void AdjustStateForNullCoalescingAssignmentNonNullCase(BoundNullCoalescingAssignmentOperator node)
		{
			this.Assign(node.LeftOperand, node.LeftOperand, false, true);
		}

		// Token: 0x06001B6E RID: 7022 RVA: 0x000901B4 File Offset: 0x0008E3B4
		protected override string Dump(DefiniteAssignmentPass.LocalState state)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[assigned ");
			this.AppendBitNames(state.Assigned, stringBuilder);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x06001B6F RID: 7023 RVA: 0x000901F4 File Offset: 0x0008E3F4
		protected void AppendBitNames(BitVector a, StringBuilder builder)
		{
			bool flag = false;
			foreach (int num in a.TrueBits())
			{
				if (flag)
				{
					builder.Append(", ");
				}
				flag = true;
				this.AppendBitName(num, builder);
			}
		}

		// Token: 0x06001B70 RID: 7024 RVA: 0x00090258 File Offset: 0x0008E458
		protected void AppendBitName(int bit, StringBuilder builder)
		{
			LocalDataFlowPass<DefiniteAssignmentPass.LocalState, DefiniteAssignmentPass.LocalFunctionState>.VariableIdentifier variableIdentifier = this.variableBySlot[bit];
			if (variableIdentifier.ContainingSlot > 0)
			{
				this.AppendBitName(variableIdentifier.ContainingSlot, builder);
				builder.Append(".");
			}
			builder.Append((bit == 0) ? "<unreachable>" : (string.IsNullOrEmpty(variableIdentifier.Symbol.Name) ? ("<anon>" + variableIdentifier.Symbol.GetHashCode().ToString()) : variableIdentifier.Symbol.Name));
		}

		// Token: 0x06001B71 RID: 7025 RVA: 0x000902E4 File Offset: 0x0008E4E4
		protected override bool Meet(ref DefiniteAssignmentPass.LocalState self, ref DefiniteAssignmentPass.LocalState other)
		{
			if (self.Assigned.Capacity != other.Assigned.Capacity)
			{
				this.Normalize(ref self);
				this.Normalize(ref other);
			}
			if (!other.Reachable)
			{
				self.Assigned[0] = true;
				return true;
			}
			bool flag = false;
			for (int i = 1; i < self.Assigned.Capacity; i++)
			{
				if (other.Assigned[i] && !self.Assigned[i])
				{
					this.SetSlotAssigned(i, ref self);
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x06001B72 RID: 7026 RVA: 0x00090370 File Offset: 0x0008E570
		protected override bool Join(ref DefiniteAssignmentPass.LocalState self, ref DefiniteAssignmentPass.LocalState other)
		{
			if (self.Reachable == other.Reachable)
			{
				if (self.Assigned.Capacity != other.Assigned.Capacity)
				{
					this.Normalize(ref self);
					this.Normalize(ref other);
				}
				return self.Assigned.IntersectWith(in other.Assigned);
			}
			if (!self.Reachable)
			{
				self.Assigned = other.Assigned.Clone();
				return true;
			}
			return false;
		}

		// Token: 0x06001B73 RID: 7027 RVA: 0x000903DF File Offset: 0x0008E5DF
		protected override DefiniteAssignmentPass.LocalFunctionState CreateLocalFunctionState(LocalFunctionSymbol symbol)
		{
			return this.CreateLocalFunctionState();
		}

		// Token: 0x06001B74 RID: 7028 RVA: 0x000903E7 File Offset: 0x0008E5E7
		private DefiniteAssignmentPass.LocalFunctionState CreateLocalFunctionState()
		{
			return new DefiniteAssignmentPass.LocalFunctionState(new DefiniteAssignmentPass.LocalState(BitVector.AllSet(this.variableBySlot.Count), true), this.UnreachableState());
		}

		// Token: 0x06001B75 RID: 7029 RVA: 0x0009040C File Offset: 0x0008E60C
		protected override void VisitLocalFunctionUse(LocalFunctionSymbol localFunc, DefiniteAssignmentPass.LocalFunctionState localFunctionState, SyntaxNode syntax, bool isCall)
		{
			this._usedLocalFunctions.Add(localFunc);
			BitVector readVars = localFunctionState.ReadVars;
			for (int i = 1; i < readVars.Capacity; i++)
			{
				if (readVars[i])
				{
					Symbol symbol = this.variableBySlot[i].Symbol;
					this.CheckIfAssignedDuringLocalFunctionReplay(symbol, syntax, i);
				}
			}
			base.VisitLocalFunctionUse(localFunc, localFunctionState, syntax, isCall);
		}

		// Token: 0x06001B76 RID: 7030 RVA: 0x00090470 File Offset: 0x0008E670
		private void CheckIfAssignedDuringLocalFunctionReplay(Symbol symbol, SyntaxNode node, int slot)
		{
			if (symbol != null)
			{
				this.NoteRead(symbol, null);
				if (this.State.Reachable)
				{
					if (slot >= this.State.Assigned.Capacity)
					{
						this.Normalize(ref this.State);
					}
					if (slot > 0 && !this.State.IsAssigned(slot))
					{
						this.ReportUnassignedIfNotCapturedInLocalFunction(symbol, node, slot, false);
					}
				}
			}
		}

		// Token: 0x06001B77 RID: 7031 RVA: 0x000904D0 File Offset: 0x0008E6D0
		private void RecordReadInLocalFunction(int slot)
		{
			LocalFunctionSymbol nearestLocalFunctionOpt = DefiniteAssignmentPass.GetNearestLocalFunctionOpt(this.CurrentSymbol);
			DefiniteAssignmentPass.LocalFunctionState orCreateLocalFuncUsages = base.GetOrCreateLocalFuncUsages(nearestLocalFunctionOpt);
			TypeSymbol type = this.variableBySlot[slot].Symbol.GetTypeOrReturnType().Type;
			if (EmptyStructTypeCache.IsTrackableStructType(type))
			{
				using (IEnumerator<FieldSymbol> enumerator = this._emptyStructTypeCache.GetStructInstanceFields(type).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						FieldSymbol fieldSymbol = enumerator.Current;
						int orCreateSlot = this.GetOrCreateSlot(fieldSymbol, slot, false, true);
						if (orCreateSlot > 0 && !this.State.IsAssigned(orCreateSlot))
						{
							this.RecordReadInLocalFunction(orCreateSlot);
						}
					}
					return;
				}
			}
			orCreateLocalFuncUsages.ReadVars[slot] = true;
		}

		// Token: 0x06001B78 RID: 7032 RVA: 0x00090594 File Offset: 0x0008E794
		private BitVector GetCapturedBitmask()
		{
			int count = this.variableBySlot.Count;
			BitVector bitVector = BitVector.AllSet(count);
			for (int i = 1; i < count; i++)
			{
				bitVector[i] = this.IsCapturedInLocalFunction(i);
			}
			return bitVector;
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x000905D0 File Offset: 0x0008E7D0
		private bool IsCapturedInLocalFunction(int slot)
		{
			if (slot <= 0)
			{
				return false;
			}
			Symbol symbol = this.variableBySlot[this.RootSlot(slot)].Symbol;
			LocalFunctionSymbol nearestLocalFunctionOpt = DefiniteAssignmentPass.GetNearestLocalFunctionOpt(this.CurrentSymbol);
			return nearestLocalFunctionOpt != null && Symbol.IsCaptured(symbol, nearestLocalFunctionOpt);
		}

		// Token: 0x06001B7A RID: 7034 RVA: 0x00090613 File Offset: 0x0008E813
		private static LocalFunctionSymbol GetNearestLocalFunctionOpt(Symbol symbol)
		{
			while (symbol != null)
			{
				if (symbol.Kind == SymbolKind.Method && ((MethodSymbol)symbol).MethodKind == MethodKind.LocalFunction)
				{
					return (LocalFunctionSymbol)symbol;
				}
				symbol = symbol.ContainingSymbol;
			}
			return null;
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x00090649 File Offset: 0x0008E849
		protected override DefiniteAssignmentPass.LocalFunctionState LocalFunctionStart(DefiniteAssignmentPass.LocalFunctionState startState)
		{
			DefiniteAssignmentPass.LocalFunctionState localFunctionState = this.CreateLocalFunctionState();
			localFunctionState.ReadVars = startState.ReadVars.Clone();
			startState.ReadVars.Clear();
			return localFunctionState;
		}

		// Token: 0x06001B7C RID: 7036 RVA: 0x00090670 File Offset: 0x0008E870
		protected override bool LocalFunctionEnd(DefiniteAssignmentPass.LocalFunctionState savedState, DefiniteAssignmentPass.LocalFunctionState currentState, ref DefiniteAssignmentPass.LocalState stateAtReturn)
		{
			if (currentState.CapturedMask.IsNull)
			{
				currentState.CapturedMask = this.GetCapturedBitmask();
				currentState.InvertedCapturedMask = currentState.CapturedMask.Clone();
				currentState.InvertedCapturedMask.Invert();
			}
			stateAtReturn.Assigned.IntersectWith(in currentState.CapturedMask);
			if (this.NonMonotonicState.HasValue)
			{
				DefiniteAssignmentPass.LocalState value = this.NonMonotonicState.Value;
				value.Assigned.UnionWith(in currentState.InvertedCapturedMask);
				this.NonMonotonicState = value;
			}
			BitVector readVars = currentState.ReadVars;
			readVars.IntersectWith(in currentState.CapturedMask);
			return savedState.ReadVars.UnionWith(in readVars);
		}

		// Token: 0x06001B7D RID: 7037 RVA: 0x00090720 File Offset: 0x0008E920
		[CompilerGenerated]
		internal static DiagnosticBag <Analyze>g__analyze|35_1(bool strictAnalysis, ref DefiniteAssignmentPass.<>c__DisplayClass35_0 A_1)
		{
			DiagnosticBag instance = DiagnosticBag.GetInstance();
			DefiniteAssignmentPass definiteAssignmentPass = new DefiniteAssignmentPass(A_1.compilation, A_1.member, A_1.node, strictAnalysis, false, null, A_1.requireOutParamsAssigned, false, false);
			definiteAssignmentPass._convertInsufficientExecutionStackExceptionToCancelledByStackGuardException = true;
			try
			{
				bool flag = false;
				definiteAssignmentPass.Analyze(ref flag, instance);
			}
			catch (BoundTreeVisitor.CancelledByStackGuardException ex) when (A_1.diagnostics != null)
			{
				ex.AddAnError(instance);
			}
			finally
			{
				definiteAssignmentPass.Free();
			}
			return instance;
		}

		// Token: 0x04000E26 RID: 3622
		private readonly PooledDictionary<LocalDataFlowPass<DefiniteAssignmentPass.LocalState, DefiniteAssignmentPass.LocalFunctionState>.VariableIdentifier, int> _variableSlot = PooledDictionary<LocalDataFlowPass<DefiniteAssignmentPass.LocalState, DefiniteAssignmentPass.LocalFunctionState>.VariableIdentifier, int>.GetInstance();

		// Token: 0x04000E27 RID: 3623
		protected readonly ArrayBuilder<LocalDataFlowPass<DefiniteAssignmentPass.LocalState, DefiniteAssignmentPass.LocalFunctionState>.VariableIdentifier> variableBySlot = ArrayBuilder<LocalDataFlowPass<DefiniteAssignmentPass.LocalState, DefiniteAssignmentPass.LocalFunctionState>.VariableIdentifier>.GetInstance(1, default(LocalDataFlowPass<DefiniteAssignmentPass.LocalState, DefiniteAssignmentPass.LocalFunctionState>.VariableIdentifier));

		// Token: 0x04000E28 RID: 3624
		private readonly HashSet<Symbol> initiallyAssignedVariables;

		// Token: 0x04000E29 RID: 3625
		private readonly PooledHashSet<LocalSymbol> _usedVariables = PooledHashSet<LocalSymbol>.GetInstance();

		// Token: 0x04000E2A RID: 3626
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })]
		private PooledHashSet<ParameterSymbol> _readParameters;

		// Token: 0x04000E2B RID: 3627
		private readonly PooledHashSet<LocalFunctionSymbol> _usedLocalFunctions = PooledHashSet<LocalFunctionSymbol>.GetInstance();

		// Token: 0x04000E2C RID: 3628
		private readonly PooledHashSet<Symbol> _writtenVariables = PooledHashSet<Symbol>.GetInstance();

		// Token: 0x04000E2D RID: 3629
		private readonly PooledDictionary<Symbol, Location> _unsafeAddressTakenVariables = PooledDictionary<Symbol, Location>.GetInstance();

		// Token: 0x04000E2E RID: 3630
		private readonly PooledHashSet<Symbol> _capturedVariables = PooledHashSet<Symbol>.GetInstance();

		// Token: 0x04000E2F RID: 3631
		private readonly PooledHashSet<Symbol> _capturedInside = PooledHashSet<Symbol>.GetInstance();

		// Token: 0x04000E30 RID: 3632
		private readonly PooledHashSet<Symbol> _capturedOutside = PooledHashSet<Symbol>.GetInstance();

		// Token: 0x04000E31 RID: 3633
		private readonly SourceAssemblySymbol _sourceAssembly;

		// Token: 0x04000E32 RID: 3634
		private readonly HashSet<PrefixUnaryExpressionSyntax> _unassignedVariableAddressOfSyntaxes;

		// Token: 0x04000E33 RID: 3635
		private BitVector _alreadyReported;

		// Token: 0x04000E34 RID: 3636
		private readonly bool _requireOutParamsAssigned;

		// Token: 0x04000E35 RID: 3637
		private readonly bool _trackClassFields;

		// Token: 0x04000E36 RID: 3638
		private readonly bool _trackStaticMembers;

		// Token: 0x04000E37 RID: 3639
		protected MethodSymbol topLevelMethod;

		// Token: 0x04000E38 RID: 3640
		protected bool _convertInsufficientExecutionStackExceptionToCancelledByStackGuardException;

		// Token: 0x04000E39 RID: 3641
		private readonly bool _shouldCheckConverted;

		// Token: 0x020006E5 RID: 1765
		private sealed class SameDiagnosticComparer : EqualityComparer<Diagnostic>
		{
			// Token: 0x060084BF RID: 33983 RVA: 0x001E0E75 File Offset: 0x001DF075
			public override bool Equals(Diagnostic x, Diagnostic y)
			{
				return x.Equals(y);
			}

			// Token: 0x060084C0 RID: 33984 RVA: 0x001E0E7E File Offset: 0x001DF07E
			public override int GetHashCode(Diagnostic obj)
			{
				return Hash.Combine(Hash.CombineValues<object>(obj.Arguments, int.MaxValue), Hash.Combine(obj.Location.GetHashCode(), obj.Code));
			}

			// Token: 0x040021DD RID: 8669
			public static readonly DefiniteAssignmentPass.SameDiagnosticComparer Instance = new DefiniteAssignmentPass.SameDiagnosticComparer();
		}

		// Token: 0x020006E6 RID: 1766
		internal struct LocalState : LocalDataFlowPass<DefiniteAssignmentPass.LocalState, DefiniteAssignmentPass.LocalFunctionState>.ILocalDataFlowState, AbstractFlowPass<DefiniteAssignmentPass.LocalState, DefiniteAssignmentPass.LocalFunctionState>.ILocalState
		{
			// Token: 0x170026A6 RID: 9894
			// (get) Token: 0x060084C3 RID: 33987 RVA: 0x001E0EBF File Offset: 0x001DF0BF
			public readonly bool NormalizeToBottom { get; }

			// Token: 0x060084C4 RID: 33988 RVA: 0x001E0EC7 File Offset: 0x001DF0C7
			internal LocalState(BitVector assigned, bool normalizeToBottom = false)
			{
				this.Assigned = assigned;
				this.NormalizeToBottom = normalizeToBottom;
			}

			// Token: 0x060084C5 RID: 33989 RVA: 0x001E0ED7 File Offset: 0x001DF0D7
			public DefiniteAssignmentPass.LocalState Clone()
			{
				return new DefiniteAssignmentPass.LocalState(this.Assigned.Clone(), false);
			}

			// Token: 0x060084C6 RID: 33990 RVA: 0x001E0EEA File Offset: 0x001DF0EA
			public bool IsAssigned(int slot)
			{
				return this.Assigned[slot];
			}

			// Token: 0x060084C7 RID: 33991 RVA: 0x001E0EF8 File Offset: 0x001DF0F8
			public void Assign(int slot)
			{
				if (slot == -1)
				{
					return;
				}
				this.Assigned[slot] = true;
			}

			// Token: 0x060084C8 RID: 33992 RVA: 0x001E0F0C File Offset: 0x001DF10C
			public void Unassign(int slot)
			{
				if (slot == -1)
				{
					return;
				}
				this.Assigned[slot] = false;
			}

			// Token: 0x170026A7 RID: 9895
			// (get) Token: 0x060084C9 RID: 33993 RVA: 0x001E0F20 File Offset: 0x001DF120
			public bool Reachable
			{
				get
				{
					return this.Assigned.Capacity <= 0 || !this.IsAssigned(0);
				}
			}

			// Token: 0x040021DE RID: 8670
			internal BitVector Assigned;
		}

		// Token: 0x020006E7 RID: 1767
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal sealed class LocalFunctionState : AbstractFlowPass<DefiniteAssignmentPass.LocalState, DefiniteAssignmentPass.LocalFunctionState>.AbstractLocalFunctionState
		{
			// Token: 0x060084CA RID: 33994 RVA: 0x001E0F3C File Offset: 0x001DF13C
			public LocalFunctionState(DefiniteAssignmentPass.LocalState stateFromBottom, DefiniteAssignmentPass.LocalState stateFromTop)
				: base(stateFromBottom, stateFromTop)
			{
			}

			// Token: 0x040021E0 RID: 8672
			public BitVector ReadVars = BitVector.Empty;

			// Token: 0x040021E1 RID: 8673
			public BitVector CapturedMask = BitVector.Null;

			// Token: 0x040021E2 RID: 8674
			public BitVector InvertedCapturedMask = BitVector.Null;
		}
	}
}
