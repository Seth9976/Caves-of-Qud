using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Cci.Pdb;
using Mono.Cecil.Cil;
using Mono.Collections.Generic;

namespace Mono.Cecil.Pdb
{
	// Token: 0x02000327 RID: 807
	public class NativePdbReader : ISymbolReader, IDisposable
	{
		// Token: 0x06001526 RID: 5414 RVA: 0x00041BDC File Offset: 0x0003FDDC
		internal NativePdbReader(Disposable<Stream> file)
		{
			this.pdb_file = file;
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x00041C0C File Offset: 0x0003FE0C
		public ISymbolWriterProvider GetWriterProvider()
		{
			return new NativePdbWriterProvider();
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x00041C14 File Offset: 0x0003FE14
		public bool ProcessDebugHeader(ImageDebugHeader header)
		{
			if (!header.HasEntries)
			{
				return false;
			}
			using (this.pdb_file)
			{
				PdbInfo pdbInfo = PdbFile.LoadFunctions(this.pdb_file.value);
				foreach (ImageDebugHeaderEntry imageDebugHeaderEntry in header.Entries)
				{
					if (NativePdbReader.IsMatchingEntry(pdbInfo, imageDebugHeaderEntry))
					{
						foreach (PdbFunction pdbFunction in pdbInfo.Functions)
						{
							this.functions.Add(pdbFunction.token, pdbFunction);
						}
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x00041CC8 File Offset: 0x0003FEC8
		private static bool IsMatchingEntry(PdbInfo info, ImageDebugHeaderEntry entry)
		{
			if (entry.Directory.Type != ImageDebugType.CodeView)
			{
				return false;
			}
			byte[] data = entry.Data;
			if (data.Length < 24)
			{
				return false;
			}
			if (NativePdbReader.ReadInt32(data, 0) != 1396986706)
			{
				return false;
			}
			byte[] array = new byte[16];
			Buffer.BlockCopy(data, 4, array, 0, 16);
			return info.Guid == new Guid(array);
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x0003BBB0 File Offset: 0x00039DB0
		private static int ReadInt32(byte[] bytes, int start)
		{
			return (int)bytes[start] | ((int)bytes[start + 1] << 8) | ((int)bytes[start + 2] << 16) | ((int)bytes[start + 3] << 24);
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x00041D2C File Offset: 0x0003FF2C
		public MethodDebugInformation Read(MethodDefinition method)
		{
			MetadataToken metadataToken = method.MetadataToken;
			PdbFunction pdbFunction;
			if (!this.functions.TryGetValue(metadataToken.ToUInt32(), out pdbFunction))
			{
				return null;
			}
			MethodDebugInformation methodDebugInformation = new MethodDebugInformation(method);
			this.ReadSequencePoints(pdbFunction, methodDebugInformation);
			MethodDebugInformation methodDebugInformation2 = methodDebugInformation;
			ScopeDebugInformation scopeDebugInformation2;
			if (pdbFunction.scopes.IsNullOrEmpty<PdbScope>())
			{
				ScopeDebugInformation scopeDebugInformation = new ScopeDebugInformation();
				scopeDebugInformation.Start = new InstructionOffset(0);
				scopeDebugInformation2 = scopeDebugInformation;
				scopeDebugInformation.End = new InstructionOffset((int)pdbFunction.length);
			}
			else
			{
				scopeDebugInformation2 = this.ReadScopeAndLocals(pdbFunction.scopes[0], methodDebugInformation);
			}
			methodDebugInformation2.scope = scopeDebugInformation2;
			if (pdbFunction.tokenOfMethodWhoseUsingInfoAppliesToThisMethod != method.MetadataToken.ToUInt32() && pdbFunction.tokenOfMethodWhoseUsingInfoAppliesToThisMethod != 0U)
			{
				methodDebugInformation.scope.import = this.GetImport(pdbFunction.tokenOfMethodWhoseUsingInfoAppliesToThisMethod, method.Module);
			}
			if (pdbFunction.scopes.Length > 1)
			{
				for (int i = 1; i < pdbFunction.scopes.Length; i++)
				{
					ScopeDebugInformation scopeDebugInformation3 = this.ReadScopeAndLocals(pdbFunction.scopes[i], methodDebugInformation);
					if (!NativePdbReader.AddScope(methodDebugInformation.scope.Scopes, scopeDebugInformation3))
					{
						methodDebugInformation.scope.Scopes.Add(scopeDebugInformation3);
					}
				}
			}
			if (pdbFunction.iteratorScopes != null)
			{
				StateMachineScopeDebugInformation stateMachineScopeDebugInformation = new StateMachineScopeDebugInformation();
				foreach (ILocalScope localScope in pdbFunction.iteratorScopes)
				{
					stateMachineScopeDebugInformation.Scopes.Add(new StateMachineScope((int)localScope.Offset, (int)(localScope.Offset + localScope.Length + 1U)));
				}
				methodDebugInformation.CustomDebugInformations.Add(stateMachineScopeDebugInformation);
			}
			if (pdbFunction.synchronizationInformation != null)
			{
				AsyncMethodBodyDebugInformation asyncMethodBodyDebugInformation = new AsyncMethodBodyDebugInformation((int)pdbFunction.synchronizationInformation.GeneratedCatchHandlerOffset);
				foreach (PdbSynchronizationPoint pdbSynchronizationPoint in pdbFunction.synchronizationInformation.synchronizationPoints)
				{
					asyncMethodBodyDebugInformation.Yields.Add(new InstructionOffset((int)pdbSynchronizationPoint.SynchronizeOffset));
					asyncMethodBodyDebugInformation.Resumes.Add(new InstructionOffset((int)pdbSynchronizationPoint.ContinuationOffset));
					asyncMethodBodyDebugInformation.ResumeMethods.Add(method);
				}
				methodDebugInformation.CustomDebugInformations.Add(asyncMethodBodyDebugInformation);
				methodDebugInformation.StateMachineKickOffMethod = (MethodDefinition)method.Module.LookupToken((int)pdbFunction.synchronizationInformation.kickoffMethodToken);
			}
			return methodDebugInformation;
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x00041F78 File Offset: 0x00040178
		private Collection<ScopeDebugInformation> ReadScopeAndLocals(PdbScope[] scopes, MethodDebugInformation info)
		{
			Collection<ScopeDebugInformation> collection = new Collection<ScopeDebugInformation>(scopes.Length);
			foreach (PdbScope pdbScope in scopes)
			{
				if (pdbScope != null)
				{
					collection.Add(this.ReadScopeAndLocals(pdbScope, info));
				}
			}
			return collection;
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x00041FB4 File Offset: 0x000401B4
		private ScopeDebugInformation ReadScopeAndLocals(PdbScope scope, MethodDebugInformation info)
		{
			ScopeDebugInformation scopeDebugInformation = new ScopeDebugInformation();
			scopeDebugInformation.Start = new InstructionOffset((int)scope.offset);
			scopeDebugInformation.End = new InstructionOffset((int)(scope.offset + scope.length));
			if (!scope.slots.IsNullOrEmpty<PdbSlot>())
			{
				scopeDebugInformation.variables = new Collection<VariableDebugInformation>(scope.slots.Length);
				foreach (PdbSlot pdbSlot in scope.slots)
				{
					if ((pdbSlot.flags & 1) == 0)
					{
						VariableDebugInformation variableDebugInformation = new VariableDebugInformation((int)pdbSlot.slot, pdbSlot.name);
						if ((pdbSlot.flags & 4) != 0)
						{
							variableDebugInformation.IsDebuggerHidden = true;
						}
						scopeDebugInformation.variables.Add(variableDebugInformation);
					}
				}
			}
			if (!scope.constants.IsNullOrEmpty<PdbConstant>())
			{
				scopeDebugInformation.constants = new Collection<ConstantDebugInformation>(scope.constants.Length);
				foreach (PdbConstant pdbConstant in scope.constants)
				{
					TypeReference typeReference = info.Method.Module.Read<PdbConstant, TypeReference>(pdbConstant, (PdbConstant c, MetadataReader r) => r.ReadConstantSignature(new MetadataToken(c.token)));
					object obj = pdbConstant.value;
					if (typeReference != null && !typeReference.IsValueType && obj is int && (int)obj == 0)
					{
						obj = null;
					}
					scopeDebugInformation.constants.Add(new ConstantDebugInformation(pdbConstant.name, typeReference, obj));
				}
			}
			if (!scope.usedNamespaces.IsNullOrEmpty<string>())
			{
				ImportDebugInformation import;
				if (this.imports.TryGetValue(scope, out import))
				{
					scopeDebugInformation.import = import;
				}
				else
				{
					import = NativePdbReader.GetImport(scope, info.Method.Module);
					this.imports.Add(scope, import);
					scopeDebugInformation.import = import;
				}
			}
			scopeDebugInformation.scopes = this.ReadScopeAndLocals(scope.scopes, info);
			return scopeDebugInformation;
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x00042188 File Offset: 0x00040388
		private static bool AddScope(Collection<ScopeDebugInformation> scopes, ScopeDebugInformation scope)
		{
			foreach (ScopeDebugInformation scopeDebugInformation in scopes)
			{
				if (scopeDebugInformation.HasScopes && NativePdbReader.AddScope(scopeDebugInformation.Scopes, scope))
				{
					return true;
				}
				if (scope.Start.Offset >= scopeDebugInformation.Start.Offset && scope.End.Offset <= scopeDebugInformation.End.Offset)
				{
					scopeDebugInformation.Scopes.Add(scope);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x00042238 File Offset: 0x00040438
		private ImportDebugInformation GetImport(uint token, ModuleDefinition module)
		{
			PdbFunction pdbFunction;
			if (!this.functions.TryGetValue(token, out pdbFunction))
			{
				return null;
			}
			if (pdbFunction.scopes.Length != 1)
			{
				return null;
			}
			PdbScope pdbScope = pdbFunction.scopes[0];
			ImportDebugInformation import;
			if (this.imports.TryGetValue(pdbScope, out import))
			{
				return import;
			}
			import = NativePdbReader.GetImport(pdbScope, module);
			this.imports.Add(pdbScope, import);
			return import;
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x00042298 File Offset: 0x00040498
		private static ImportDebugInformation GetImport(PdbScope scope, ModuleDefinition module)
		{
			if (scope.usedNamespaces.IsNullOrEmpty<string>())
			{
				return null;
			}
			ImportDebugInformation importDebugInformation = new ImportDebugInformation();
			foreach (string text in scope.usedNamespaces)
			{
				if (!string.IsNullOrEmpty(text))
				{
					ImportTarget importTarget = null;
					string text2 = text.Substring(1);
					char c = text[0];
					if (c <= '@')
					{
						if (c != '*')
						{
							if (c == '@')
							{
								if (!text2.StartsWith("P:"))
								{
									goto IL_0194;
								}
								importTarget = new ImportTarget(ImportTargetKind.ImportNamespace)
								{
									@namespace = text2.Substring(2)
								};
							}
						}
						else
						{
							importTarget = new ImportTarget(ImportTargetKind.ImportNamespace)
							{
								@namespace = text2
							};
						}
					}
					else if (c != 'A')
					{
						if (c != 'T')
						{
							if (c == 'U')
							{
								importTarget = new ImportTarget(ImportTargetKind.ImportNamespace)
								{
									@namespace = text2
								};
							}
						}
						else
						{
							TypeReference typeReference = TypeParser.ParseType(module, text2, false);
							if (typeReference != null)
							{
								importTarget = new ImportTarget(ImportTargetKind.ImportType)
								{
									type = typeReference
								};
							}
						}
					}
					else
					{
						int num = text.IndexOf(' ');
						if (num < 0)
						{
							importTarget = new ImportTarget(ImportTargetKind.ImportNamespace)
							{
								@namespace = text
							};
						}
						else
						{
							string text3 = text.Substring(1, num - 1);
							string text4 = text.Substring(num + 2);
							char c2 = text[num + 1];
							if (c2 != 'T')
							{
								if (c2 == 'U')
								{
									importTarget = new ImportTarget(ImportTargetKind.DefineNamespaceAlias)
									{
										alias = text3,
										@namespace = text4
									};
								}
							}
							else
							{
								TypeReference typeReference2 = TypeParser.ParseType(module, text4, false);
								if (typeReference2 != null)
								{
									importTarget = new ImportTarget(ImportTargetKind.DefineTypeAlias)
									{
										alias = text3,
										type = typeReference2
									};
								}
							}
						}
					}
					if (importTarget != null)
					{
						importDebugInformation.Targets.Add(importTarget);
					}
				}
				IL_0194:;
			}
			return importDebugInformation;
		}

		// Token: 0x06001531 RID: 5425 RVA: 0x00042448 File Offset: 0x00040648
		private void ReadSequencePoints(PdbFunction function, MethodDebugInformation info)
		{
			if (function.lines == null)
			{
				return;
			}
			info.sequence_points = new Collection<SequencePoint>();
			foreach (PdbLines pdbLines in function.lines)
			{
				this.ReadLines(pdbLines, info);
			}
		}

		// Token: 0x06001532 RID: 5426 RVA: 0x0004248C File Offset: 0x0004068C
		private void ReadLines(PdbLines lines, MethodDebugInformation info)
		{
			Document document = this.GetDocument(lines.file);
			PdbLine[] lines2 = lines.lines;
			for (int i = 0; i < lines2.Length; i++)
			{
				NativePdbReader.ReadLine(lines2[i], document, info);
			}
		}

		// Token: 0x06001533 RID: 5427 RVA: 0x000424CC File Offset: 0x000406CC
		private static void ReadLine(PdbLine line, Document document, MethodDebugInformation info)
		{
			SequencePoint sequencePoint = new SequencePoint((int)line.offset, document);
			sequencePoint.StartLine = (int)line.lineBegin;
			sequencePoint.StartColumn = (int)line.colBegin;
			sequencePoint.EndLine = (int)line.lineEnd;
			sequencePoint.EndColumn = (int)line.colEnd;
			info.sequence_points.Add(sequencePoint);
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x00042524 File Offset: 0x00040724
		private Document GetDocument(PdbSource source)
		{
			string name = source.name;
			Document document;
			if (this.documents.TryGetValue(name, out document))
			{
				return document;
			}
			document = new Document(name)
			{
				LanguageGuid = source.language,
				LanguageVendorGuid = source.vendor,
				TypeGuid = source.doctype,
				HashAlgorithmGuid = source.checksumAlgorithm,
				Hash = source.checksum
			};
			this.documents.Add(name, document);
			return document;
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x0004259C File Offset: 0x0004079C
		public void Dispose()
		{
			this.pdb_file.Dispose();
		}

		// Token: 0x04000A77 RID: 2679
		private readonly Disposable<Stream> pdb_file;

		// Token: 0x04000A78 RID: 2680
		private readonly Dictionary<string, Document> documents = new Dictionary<string, Document>();

		// Token: 0x04000A79 RID: 2681
		private readonly Dictionary<uint, PdbFunction> functions = new Dictionary<uint, PdbFunction>();

		// Token: 0x04000A7A RID: 2682
		private readonly Dictionary<PdbScope, ImportDebugInformation> imports = new Dictionary<PdbScope, ImportDebugInformation>();
	}
}
