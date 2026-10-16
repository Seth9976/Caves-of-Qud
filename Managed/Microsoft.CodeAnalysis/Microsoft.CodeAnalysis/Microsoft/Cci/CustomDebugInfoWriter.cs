using System;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Debugging;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.Cci
{
	// Token: 0x02000080 RID: 128
	internal sealed class CustomDebugInfoWriter
	{
		// Token: 0x06000345 RID: 837 RVA: 0x0000C96B File Offset: 0x0000AB6B
		public CustomDebugInfoWriter(PdbWriter pdbWriter)
		{
			this._pdbWriter = pdbWriter;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000C97C File Offset: 0x0000AB7C
		public bool ShouldForwardNamespaceScopes(EmitContext context, IMethodBody methodBody, MethodDefinitionHandle methodHandle, out IMethodDefinition forwardToMethod)
		{
			if (this.ShouldForwardToPreviousMethodWithUsingInfo(context, methodBody))
			{
				if (context.Module.GenerateVisualBasicStylePdb)
				{
					forwardToMethod = this._previousMethodBodyWithUsingInfo.MethodDefinition;
				}
				else
				{
					forwardToMethod = null;
				}
				return true;
			}
			this._previousMethodBodyWithUsingInfo = methodBody;
			this._previousMethodWithUsingInfo = methodHandle;
			forwardToMethod = null;
			return false;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000C9CC File Offset: 0x0000ABCC
		public byte[] SerializeMethodDebugInfo(EmitContext context, IMethodBody methodBody, MethodDefinitionHandle methodHandle, bool emitEncInfo, bool suppressNewCustomDebugInfo, out bool emitExternNamespaces)
		{
			emitExternNamespaces = false;
			if (this._methodBodyWithModuleInfo == null && context.Module.GetAssemblyReferenceAliases(context).Any<AssemblyReferenceAlias>())
			{
				this._methodWithModuleInfo = methodHandle;
				this._methodBodyWithModuleInfo = methodBody;
				emitExternNamespaces = true;
			}
			PooledBlobBuilder instance = PooledBlobBuilder.GetInstance(1024);
			CustomDebugInfoEncoder customDebugInfoEncoder = new CustomDebugInfoEncoder(instance);
			if (methodBody.StateMachineTypeName != null)
			{
				customDebugInfoEncoder.AddStateMachineTypeName(methodBody.StateMachineTypeName);
			}
			else
			{
				this.SerializeNamespaceScopeMetadata(ref customDebugInfoEncoder, context, methodBody);
				customDebugInfoEncoder.AddStateMachineHoistedLocalScopes(methodBody.StateMachineHoistedLocalScopes);
			}
			if (!suppressNewCustomDebugInfo)
			{
				CustomDebugInfoWriter.SerializeDynamicLocalInfo(ref customDebugInfoEncoder, methodBody);
				CustomDebugInfoWriter.SerializeTupleElementNames(ref customDebugInfoEncoder, methodBody);
				if (emitEncInfo)
				{
					EditAndContinueMethodDebugInformation encMethodDebugInfo = MetadataWriter.GetEncMethodDebugInfo(methodBody);
					CustomDebugInfoWriter.SerializeCustomDebugInformation(ref customDebugInfoEncoder, encMethodDebugInfo);
				}
			}
			byte[] array = customDebugInfoEncoder.ToArray();
			instance.Free();
			return array;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000CA80 File Offset: 0x0000AC80
		internal static void SerializeCustomDebugInformation(ref CustomDebugInfoEncoder encoder, EditAndContinueMethodDebugInformation debugInfo)
		{
			if (!debugInfo.LocalSlots.IsDefaultOrEmpty)
			{
				encoder.AddRecord<EditAndContinueMethodDebugInformation>(CustomDebugInfoKind.EditAndContinueLocalSlotMap, debugInfo, delegate(EditAndContinueMethodDebugInformation info, BlobBuilder builder)
				{
					info.SerializeLocalSlots(builder);
				});
			}
			if (!debugInfo.Lambdas.IsDefaultOrEmpty)
			{
				encoder.AddRecord<EditAndContinueMethodDebugInformation>(CustomDebugInfoKind.EditAndContinueLambdaMap, debugInfo, delegate(EditAndContinueMethodDebugInformation info, BlobBuilder builder)
				{
					info.SerializeLambdaMap(builder);
				});
			}
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000CAFC File Offset: 0x0000ACFC
		private static ArrayBuilder<T> GetLocalInfoToSerialize<T>(IMethodBody methodBody, Func<ILocalDefinition, bool> filter, Func<LocalScope, ILocalDefinition, T> getInfo)
		{
			ArrayBuilder<T> arrayBuilder = null;
			foreach (LocalScope localScope in methodBody.LocalScopes)
			{
				foreach (ILocalDefinition localDefinition in localScope.Variables)
				{
					if (filter.Invoke(localDefinition))
					{
						if (arrayBuilder == null)
						{
							arrayBuilder = ArrayBuilder<T>.GetInstance();
						}
						arrayBuilder.Add(getInfo.Invoke(default(LocalScope), localDefinition));
					}
				}
				foreach (ILocalDefinition localDefinition2 in localScope.Constants)
				{
					if (filter.Invoke(localDefinition2))
					{
						if (arrayBuilder == null)
						{
							arrayBuilder = ArrayBuilder<T>.GetInstance();
						}
						arrayBuilder.Add(getInfo.Invoke(localScope, localDefinition2));
					}
				}
			}
			return arrayBuilder;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000CBD0 File Offset: 0x0000ADD0
		private static void SerializeDynamicLocalInfo(ref CustomDebugInfoEncoder encoder, IMethodBody methodBody)
		{
			if (!methodBody.HasDynamicLocalVariables)
			{
				return;
			}
			ArrayBuilder<ValueTuple<string, byte[], int, int>> localInfoToSerialize = CustomDebugInfoWriter.GetLocalInfoToSerialize<ValueTuple<string, byte[], int, int>>(methodBody, delegate(ILocalDefinition local)
			{
				ImmutableArray<bool> dynamicTransformFlags = local.DynamicTransformFlags;
				return !dynamicTransformFlags.IsEmpty && dynamicTransformFlags.Length <= 64 && local.Name.Length < 64;
			}, (LocalScope scope, ILocalDefinition local) => new ValueTuple<string, byte[], int, int>(local.Name, CustomDebugInfoWriter.<SerializeDynamicLocalInfo>g__GetDynamicFlags|10_0(local), local.DynamicTransformFlags.Length, (local.SlotIndex < 0) ? 0 : local.SlotIndex));
			if (localInfoToSerialize == null)
			{
				return;
			}
			encoder.AddDynamicLocals(localInfoToSerialize);
			localInfoToSerialize.Free();
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000CC3C File Offset: 0x0000AE3C
		private static void SerializeTupleElementNames(ref CustomDebugInfoEncoder encoder, IMethodBody methodBody)
		{
			ArrayBuilder<ValueTuple<string, int, int, int, ImmutableArray<string>>> localInfoToSerialize = CustomDebugInfoWriter.GetLocalInfoToSerialize<ValueTuple<string, int, int, int, ImmutableArray<string>>>(methodBody, (ILocalDefinition local) => !local.TupleElementNames.IsEmpty, (LocalScope scope, ILocalDefinition local) => new ValueTuple<string, int, int, int, ImmutableArray<string>>(local.Name, local.SlotIndex, scope.StartOffset, scope.EndOffset, local.TupleElementNames));
			if (localInfoToSerialize == null)
			{
				return;
			}
			encoder.AddTupleElementNames(localInfoToSerialize);
			localInfoToSerialize.Free();
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000CCA0 File Offset: 0x0000AEA0
		private void SerializeNamespaceScopeMetadata(ref CustomDebugInfoEncoder encoder, EmitContext context, IMethodBody methodBody)
		{
			if (context.Module.GenerateVisualBasicStylePdb)
			{
				return;
			}
			if (this.ShouldForwardToPreviousMethodWithUsingInfo(context, methodBody))
			{
				encoder.AddForwardMethodInfo(this._previousMethodWithUsingInfo);
				return;
			}
			ArrayBuilder<int> instance = ArrayBuilder<int>.GetInstance();
			for (IImportScope importScope = methodBody.ImportScope; importScope != null; importScope = importScope.Parent)
			{
				instance.Add(importScope.GetUsedNamespaces().Length);
			}
			encoder.AddUsingGroups(instance);
			instance.Free();
			if (this._methodBodyWithModuleInfo != null && this._methodBodyWithModuleInfo != methodBody)
			{
				encoder.AddForwardModuleInfo(this._methodWithModuleInfo);
			}
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000CD2C File Offset: 0x0000AF2C
		private bool ShouldForwardToPreviousMethodWithUsingInfo(EmitContext context, IMethodBody methodBody)
		{
			if (this._previousMethodBodyWithUsingInfo == null || this._previousMethodBodyWithUsingInfo == methodBody)
			{
				return false;
			}
			if (context.Module.GenerateVisualBasicStylePdb && this._pdbWriter.GetOrCreateSerializedNamespaceName(this._previousMethodBodyWithUsingInfo.MethodDefinition.ContainingNamespace) != this._pdbWriter.GetOrCreateSerializedNamespaceName(methodBody.MethodDefinition.ContainingNamespace))
			{
				return false;
			}
			IImportScope importScope = this._previousMethodBodyWithUsingInfo.ImportScope;
			if (methodBody.ImportScope == importScope)
			{
				return true;
			}
			IImportScope importScope2 = methodBody.ImportScope;
			IImportScope importScope3 = importScope;
			while (importScope2 != null && importScope3 != null)
			{
				if (!importScope2.GetUsedNamespaces().SequenceEqual<UsedNamespaceOrType, UsedNamespaceOrType>(importScope3.GetUsedNamespaces(), null))
				{
					return false;
				}
				importScope2 = importScope2.Parent;
				importScope3 = importScope3.Parent;
			}
			return importScope2 == importScope3;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000CDE4 File Offset: 0x0000AFE4
		[CompilerGenerated]
		internal static byte[] <SerializeDynamicLocalInfo>g__GetDynamicFlags|10_0(ILocalDefinition local)
		{
			ImmutableArray<bool> dynamicTransformFlags = local.DynamicTransformFlags;
			byte[] array = new byte[64];
			for (int i = 0; i < dynamicTransformFlags.Length; i++)
			{
				if (dynamicTransformFlags[i])
				{
					array[i] = 1;
				}
			}
			return array;
		}

		// Token: 0x04000162 RID: 354
		private MethodDefinitionHandle _methodWithModuleInfo;

		// Token: 0x04000163 RID: 355
		private IMethodBody _methodBodyWithModuleInfo;

		// Token: 0x04000164 RID: 356
		private MethodDefinitionHandle _previousMethodWithUsingInfo;

		// Token: 0x04000165 RID: 357
		private IMethodBody _previousMethodBodyWithUsingInfo;

		// Token: 0x04000166 RID: 358
		private readonly PdbWriter _pdbWriter;
	}
}
