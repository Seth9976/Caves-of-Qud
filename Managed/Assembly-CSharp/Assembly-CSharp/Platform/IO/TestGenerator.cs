using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

namespace Platform.IO
{
	// Token: 0x02000333 RID: 819
	public static class TestGenerator
	{
		// Token: 0x06001E8F RID: 7823 RVA: 0x00094EC0 File Offset: 0x000930C0
		public static TestGenerator.Snapshot GenerateSnapshotBaseline()
		{
			TestGenerator.<>c__DisplayClass0_0 CS$<>8__locals1 = new TestGenerator.<>c__DisplayClass0_0();
			CS$<>8__locals1.snapshot = new TestGenerator.Snapshot();
			CS$<>8__locals1.snapshot.tests = new Dictionary<string, string>();
			CS$<>8__locals1.snapshot.lastSaved = DateTime.Now;
			CS$<>8__locals1.byteContent = new byte[]
			{
				0, 0, 0, 0, 1, 2, 3, 4, 5, 6,
				7, 8, 9, 10, 0, 1, 2, 3, 4, 5
			};
			CS$<>8__locals1.txtContent = "abcdeefghei&*nc.a7d7^&*^)@$%^d/wqi321//312asrj\\n23";
			TestGenerator.Snapshot snapshot;
			using (new Utils.OnScopeExit(delegate
			{
				TestGenerator.useRomPaths = false;
				TestGenerator.<GenerateSnapshotBaseline>g__deleteTestDirectory|0_60("cleanup");
			}))
			{
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestsRwAndRom|55("ReadAllText", delegate(TestGenerator.TestBuilder blder)
				{
					blder.Append(File.ReadAllText(TestGenerator.PathPresent()));
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestsRwAndRom|55("ReadAllText -- Missing Path", delegate(TestGenerator.TestBuilder blder)
				{
					TestGenerator.SerializeAnyThrows(blder, true, delegate
					{
						blder.Append(File.ReadAllText(TestGenerator.PathMissing()));
					});
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestsRwAndRom|55("ReadAllText -- Invalid Path", delegate(TestGenerator.TestBuilder blder)
				{
					TestGenerator.SerializeAnyThrows(blder, true, delegate
					{
						blder.Append(File.ReadAllText(TestGenerator.PathInvalid()));
					});
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestsRwAndRom|55("ReadAllBytes", delegate(TestGenerator.TestBuilder blder)
				{
					blder.Append(File.ReadAllBytes(TestGenerator.PathPresent()));
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestsRwAndRom|55("ReadAllBytes -- Missing Path", delegate(TestGenerator.TestBuilder blder)
				{
					TestGenerator.SerializeAnyThrows(blder, true, delegate
					{
						blder.Append(File.ReadAllBytes(TestGenerator.PathMissing()));
					});
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestsRwAndRom|55("ReadAllBytes -- Invalid Path", delegate(TestGenerator.TestBuilder blder)
				{
					TestGenerator.SerializeAnyThrows(blder, true, delegate
					{
						blder.Append(File.ReadAllBytes(TestGenerator.PathInvalid()));
					});
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestRW|56("WriteAllText", delegate(TestGenerator.TestBuilder hashRef)
				{
					File.WriteAllText(TestGenerator.PathAvailableText(), CS$<>8__locals1.txtContent);
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestRW|56("WriteAllText -- Invalid Path", delegate(TestGenerator.TestBuilder blder)
				{
					bool flag = true;
					Action action;
					if ((action = CS$<>8__locals1.<>9__68) == null)
					{
						action = (CS$<>8__locals1.<>9__68 = delegate
						{
							File.WriteAllText(TestGenerator.PathInvalid(), CS$<>8__locals1.txtContent);
						});
					}
					TestGenerator.SerializeAnyThrows(blder, flag, action);
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestRW|56("WriteAllText -- Occupied Path", delegate(TestGenerator.TestBuilder blder)
				{
					TestGenerator.SerializeAnyThrows(blder, true, delegate
					{
						File.WriteAllText(TestGenerator.PathAvailableText(), "first text");
					});
					bool flag2 = true;
					Action action2;
					if ((action2 = CS$<>8__locals1.<>9__70) == null)
					{
						action2 = (CS$<>8__locals1.<>9__70 = delegate
						{
							File.WriteAllText(TestGenerator.PathAvailableText(), CS$<>8__locals1.txtContent);
						});
					}
					TestGenerator.SerializeAnyThrows(blder, flag2, action2);
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestRW|56("WriteAllBytes", delegate(TestGenerator.TestBuilder hashRef)
				{
					File.WriteAllBytes(TestGenerator.PathAvailableText(), CS$<>8__locals1.byteContent);
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestRW|56("WriteAllBytes -- Invalid Path", delegate(TestGenerator.TestBuilder blder)
				{
					bool flag3 = true;
					Action action3;
					if ((action3 = CS$<>8__locals1.<>9__71) == null)
					{
						action3 = (CS$<>8__locals1.<>9__71 = delegate
						{
							File.WriteAllBytes(TestGenerator.PathInvalid(), CS$<>8__locals1.byteContent);
						});
					}
					TestGenerator.SerializeAnyThrows(blder, flag3, action3);
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestRW|56("WriteAllBytes -- Occupied Path", delegate(TestGenerator.TestBuilder blder)
				{
					TestGenerator.SerializeAnyThrows(blder, true, delegate
					{
						File.WriteAllBytes(TestGenerator.PathAvailableText(), new byte[] { 0, 1, 2 });
					});
					bool flag4 = true;
					Action action4;
					if ((action4 = CS$<>8__locals1.<>9__73) == null)
					{
						action4 = (CS$<>8__locals1.<>9__73 = delegate
						{
							File.WriteAllBytes(TestGenerator.PathAvailableText(), CS$<>8__locals1.byteContent);
						});
					}
					TestGenerator.SerializeAnyThrows(blder, flag4, action4);
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestsRwAndRom|55("FileExists", delegate(TestGenerator.TestBuilder blder)
				{
					blder.Append(File.Exists(TestGenerator.PathPresent()));
					blder.Append(File.Exists(TestGenerator.PathMissing()));
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestRW|56("FileDelete -- Invalid Path", delegate(TestGenerator.TestBuilder blder)
				{
					TestGenerator.SerializeAnyThrows(blder, false, delegate
					{
						File.Delete(TestGenerator.PathInvalid());
					});
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestRW|56("FileDelete -- Missing Path", delegate(TestGenerator.TestBuilder blder)
				{
					TestGenerator.SerializeAnyThrows(blder, false, delegate
					{
						File.Delete(TestGenerator.PathMissing());
					});
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestRW|56("Copy -- overwrite: true", delegate(TestGenerator.TestBuilder hashRef)
				{
					File.WriteAllText(TestGenerator.GetPath("CopySource01.txt"), "copy source content 01");
					File.WriteAllText(TestGenerator.GetPath("CopySource02.txt"), "copy source content 02");
					File.Copy(TestGenerator.GetPath("CopySource01.txt"), TestGenerator.GetPath("CopySource02.txt"), true);
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestRW|56("Copy -- overwrite: false", delegate(TestGenerator.TestBuilder blder)
				{
					File.WriteAllText(TestGenerator.GetPath("CopySource01.txt"), "copy source content 01");
					File.WriteAllText(TestGenerator.GetPath("CopySource02.txt"), "copy source content 02");
					TestGenerator.SerializeAnyThrows(blder, true, delegate
					{
						File.Copy(TestGenerator.GetPath("CopySource01.txt"), TestGenerator.GetPath("CopySource02.txt"), false);
					});
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestRW|56("Copy -- missing source", delegate(TestGenerator.TestBuilder blder)
				{
					TestGenerator.SerializeAnyThrows(blder, true, delegate
					{
						File.Copy(TestGenerator.PathMissing(), TestGenerator.PathAvailableText(), false);
					});
					TestGenerator.SerializeAnyThrows(blder, true, delegate
					{
						File.Copy(TestGenerator.PathMissing(), TestGenerator.PathAvailableText(), true);
					});
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestRW|56("Copy -- folder source", delegate(TestGenerator.TestBuilder blder)
				{
					TestGenerator.SerializeAnyThrows(blder, true, delegate
					{
						File.Copy(TestGenerator.PathRootDirectory(), TestGenerator.PathAvailableText(), false);
					});
					TestGenerator.SerializeAnyThrows(blder, true, delegate
					{
						File.Copy(TestGenerator.PathRootDirectory(), TestGenerator.PathAvailableText(), true);
					});
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestsRwAndRom|55("File Info", delegate(TestGenerator.TestBuilder blder)
				{
					FileInfo fileInfo = new FileInfo(TestGenerator.PathPresent());
					TestGenerator.<GenerateSnapshotBaseline>g__serializeFileSystemInfo|0_24(fileInfo, blder);
					blder.Append(fileInfo.Length.ToString());
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestsRwAndRom|55("Directory Info", delegate(TestGenerator.TestBuilder blder)
				{
					TestGenerator.<GenerateSnapshotBaseline>g__serializeFileSystemInfo|0_24(new DirectoryInfo(TestGenerator.PathRootDirectory()), blder);
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestsRwAndRom|55("File Info -- Missing", delegate(TestGenerator.TestBuilder blder)
				{
					FileInfo fileInfo2 = new FileInfo(TestGenerator.PathMissing());
					blder.Append(fileInfo2.Exists);
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestsRwAndRom|55("Directory Info -- Missing", delegate(TestGenerator.TestBuilder blder)
				{
					DirectoryInfo directoryInfo = new DirectoryInfo(TestGenerator.PathMissing());
					blder.Append(directoryInfo.Exists);
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestsRwAndRom|55("File Info -- Missing Path", delegate(TestGenerator.TestBuilder hashRef)
				{
					TestGenerator.SerializeAnyThrows(hashRef, true, delegate
					{
						new FileInfo(TestGenerator.PathMissing());
					});
					TestGenerator.SerializeAnyThrows(hashRef, true, delegate
					{
						new FileInfo(TestGenerator.PathParentMissing());
					});
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestRW|56("DirectoryCreate -- Directory Parent Present", delegate(TestGenerator.TestBuilder blder)
				{
					Directory.CreateDirectory(TestGenerator.PathAvailableFolder());
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestRW|56("DirectoryCreate -- Directory Parent Missing", delegate(TestGenerator.TestBuilder blder)
				{
					Directory.CreateDirectory(TestGenerator.PathAvailableFolderNoParent());
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestRW|56("DirectoryCreate -- File Path", delegate(TestGenerator.TestBuilder blder)
				{
					TestGenerator.SerializeAnyThrows(blder, true, delegate
					{
						Directory.CreateDirectory(TestGenerator.PathAvailableByte());
					});
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestRW|56("DirectoryCreate -- Already Exists", delegate(TestGenerator.TestBuilder blder)
				{
					TestGenerator.SerializeAnyThrows(blder, true, delegate
					{
						Directory.CreateDirectory(TestGenerator.PathRootDirectory());
					});
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestRW|56("DirectoryExists", delegate(TestGenerator.TestBuilder blder)
				{
					blder.Append(Directory.Exists(TestGenerator.PathRootDirectory()));
					blder.Append(Directory.Exists(TestGenerator.PathMissing()));
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestRW|56("DirectoryDelete -- Recursive: true", delegate(TestGenerator.TestBuilder blder)
				{
					Directory.Delete(TestGenerator.PathRootDirectory(), true);
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestRW|56("DirectoryDelete -- Recursive: false", delegate(TestGenerator.TestBuilder blder)
				{
					Directory.Delete(TestGenerator.PathRootDirectory(), false);
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestsRwAndRom|55("EnumerateDirectories -- Search Option Top Level", delegate(TestGenerator.TestBuilder blder)
				{
					foreach (string text in Directory.EnumerateDirectories(TestGenerator.PathRootDirectory()))
					{
						blder.AppendFilePath(text, true);
					}
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestsRwAndRom|55("Enumerate Files -- (no other arguments)", delegate(TestGenerator.TestBuilder blder)
				{
					foreach (string text2 in Directory.EnumerateFiles(TestGenerator.PathRootDirectory()))
					{
						blder.AppendFilePath(text2, true);
					}
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestsRwAndRom|55("Enumerate Files -- Search: Shallow", delegate(TestGenerator.TestBuilder blder)
				{
					foreach (string text3 in TestGenerator.<GenerateSnapshotBaseline>g__sortPaths|0_62(Directory.EnumerateFiles(TestGenerator.PathRootDirectory(), "*", SearchOption.TopDirectoryOnly)))
					{
						blder.AppendFilePath(text3, true);
					}
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestsRwAndRom|55("Enumerate Files -- Search: All", delegate(TestGenerator.TestBuilder blder)
				{
					foreach (string text4 in TestGenerator.<GenerateSnapshotBaseline>g__sortPaths|0_62(Directory.EnumerateFiles(TestGenerator.PathRootDirectory(), "*", SearchOption.AllDirectories)))
					{
						blder.AppendFilePath(text4, true);
					}
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestsRwAndRom|55("Enumerate Files -- Pattern: *.txt -- Search: All", delegate(TestGenerator.TestBuilder blder)
				{
					foreach (string text5 in TestGenerator.<GenerateSnapshotBaseline>g__sortPaths|0_62(Directory.EnumerateFiles(TestGenerator.PathRootDirectory(), "*.txt", SearchOption.AllDirectories)))
					{
						blder.AppendFilePath(text5, true);
					}
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestsRwAndRom|55("Enumerate Files -- Pattern: *.asset -- Search: All", delegate(TestGenerator.TestBuilder blder)
				{
					foreach (string text6 in TestGenerator.<GenerateSnapshotBaseline>g__sortPaths|0_62(Directory.EnumerateFiles(TestGenerator.PathRootDirectory(), "*.asset", SearchOption.AllDirectories)))
					{
						blder.AppendFilePath(text6, true);
					}
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestsRwAndRom|55("Enumerate Files -- Pattern: *subFile* -- Search: All", delegate(TestGenerator.TestBuilder blder)
				{
					foreach (string text7 in TestGenerator.<GenerateSnapshotBaseline>g__sortPaths|0_62(Directory.EnumerateFiles(TestGenerator.PathRootDirectory(), "*subFile*", SearchOption.AllDirectories)))
					{
						blder.AppendFilePath(text7, true);
					}
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestsRwAndRom|55("Enumerate File System Entries -- Pattern: * -- Search: All", delegate(TestGenerator.TestBuilder blder)
				{
					List<FileSystemInfo> list = new DirectoryInfo(TestGenerator.PathRootDirectory()).EnumerateFileSystemInfos().ToList<FileSystemInfo>();
					list.Sort((FileSystemInfo a, FileSystemInfo b) => StringComparer.OrdinalIgnoreCase.Compare(a.FullName, b.FullName));
					foreach (FileSystemInfo fileSystemInfo in list)
					{
						blder.AppendFilePath(fileSystemInfo.FullName, true);
					}
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestsRwAndRom|55("Stream -- Read -- Length", delegate(TestGenerator.TestBuilder blder)
				{
					using (FileStream fileStream = File.OpenRead(TestGenerator.PathPresent()))
					{
						blder.Append(fileStream.Length.ToString());
					}
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestsRwAndRom|55("Stream -- Read -- All", delegate(TestGenerator.TestBuilder blder)
				{
					using (FileStream fileStream2 = File.OpenRead(TestGenerator.PathPresent()))
					{
						byte[] array = new byte[fileStream2.Length];
						blder.Append(fileStream2.Read(array, 0, array.Length).ToString());
						blder.Append(array);
					}
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestsRwAndRom|55("Stream -- Read -- with Offset ", delegate(TestGenerator.TestBuilder blder)
				{
					using (FileStream fileStream3 = File.OpenRead(TestGenerator.PathPresent()))
					{
						byte[] array2 = new byte[4];
						blder.Append(fileStream3.Read(array2, 1, 2).ToString());
						blder.Append(array2);
					}
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestsRwAndRom|55("Stream -- Read -- with Seek & Offset", delegate(TestGenerator.TestBuilder blder)
				{
					using (FileStream fileStream4 = File.OpenRead(TestGenerator.PathPresent()))
					{
						byte[] array3 = new byte[10];
						fileStream4.Seek(1L, SeekOrigin.Begin);
						fileStream4.Read(array3, 0, 2);
						fileStream4.Read(array3, 2, 2);
						fileStream4.Seek(-4L, SeekOrigin.End);
						fileStream4.Read(array3, 6, 2);
						fileStream4.Seek(-8L, SeekOrigin.Current);
						fileStream4.Read(array3, 8, 2);
						blder.Append(array3);
					}
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestsRwAndRom|55("Stream -- Read -- from stream end", delegate(TestGenerator.TestBuilder blder)
				{
					using (FileStream fileStream5 = File.OpenRead(TestGenerator.PathPresent()))
					{
						fileStream5.Seek(0L, SeekOrigin.End);
						byte[] array4 = new byte[1];
						blder.Append(fileStream5.Read(array4, 0, 1).ToString());
						blder.Append(array4);
					}
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestsRwAndRom|55("Stream -- Read -- Missing", delegate(TestGenerator.TestBuilder blder)
				{
					TestGenerator.SerializeAnyThrows(blder, true, delegate
					{
						using (File.OpenRead(TestGenerator.PathMissing()))
						{
						}
					});
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestRW|56("Stream -- Write -- Open Missing", delegate(TestGenerator.TestBuilder blder)
				{
					using (File.OpenWrite(TestGenerator.PathAvailableByte()))
					{
					}
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestRW|56("Stream -- Write -- Open & Flush Empty", delegate(TestGenerator.TestBuilder blder)
				{
					using (FileStream fileStream8 = File.OpenWrite(TestGenerator.PathAvailableByte()))
					{
						fileStream8.Flush();
					}
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestRW|56("Stream -- Write -- Byte", delegate(TestGenerator.TestBuilder blder)
				{
					using (FileStream fileStream9 = File.OpenWrite(TestGenerator.PathAvailableByte()))
					{
						fileStream9.WriteByte(30);
						fileStream9.Flush();
					}
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestRW|56("Stream --  Write -- Buffer Offset & Length", delegate(TestGenerator.TestBuilder blder)
				{
					using (FileStream fileStream10 = File.OpenWrite(TestGenerator.PathAvailableByte()))
					{
						fileStream10.Write(CS$<>8__locals1.byteContent, 4, CS$<>8__locals1.byteContent.Length - 8);
						fileStream10.Flush();
					}
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestRW|56("Stream --  Write -- Buffer Offset & Length Seek", delegate(TestGenerator.TestBuilder blder)
				{
					using (FileStream fileStream11 = File.OpenWrite(TestGenerator.PathAvailableByte()))
					{
						fileStream11.WriteByte(5);
						fileStream11.WriteByte(3);
						fileStream11.WriteByte(8);
						fileStream11.Seek(1L, SeekOrigin.Begin);
						fileStream11.WriteByte(9);
						fileStream11.Flush();
					}
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestRW|56("Stream -- Write -- Open Existing Prepend & Append", delegate(TestGenerator.TestBuilder blder)
				{
					using (FileStream fileStream12 = File.OpenWrite(TestGenerator.PathPresent()))
					{
						fileStream12.WriteByte(9);
						fileStream12.WriteByte(9);
						fileStream12.Seek(0L, SeekOrigin.End);
						fileStream12.WriteByte(9);
						fileStream12.WriteByte(9);
						fileStream12.Flush();
					}
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestRW|56("File -- AppendAllText", delegate(TestGenerator.TestBuilder blder)
				{
					File.AppendAllText(TestGenerator.PathPresent(), "appendText");
				});
				CS$<>8__locals1.<GenerateSnapshotBaseline>g__runTestRW|56("File -- AppendAllTextMissing", delegate(TestGenerator.TestBuilder blder)
				{
					TestGenerator.SerializeAnyThrows(blder, true, delegate
					{
						File.AppendAllText(TestGenerator.PathMissing(), "appendText");
					});
				});
				snapshot = CS$<>8__locals1.snapshot;
			}
			return snapshot;
		}

		// Token: 0x06001E90 RID: 7824 RVA: 0x000957A8 File Offset: 0x000939A8
		public static TestGenerator.Snapshot GenerateSnapshotCandidate__GENERATED()
		{
			TestGenerator.<>c__DisplayClass1_0 CS$<>8__locals1 = new TestGenerator.<>c__DisplayClass1_0();
			CS$<>8__locals1.snapshot = new TestGenerator.Snapshot();
			CS$<>8__locals1.snapshot.tests = new Dictionary<string, string>();
			CS$<>8__locals1.snapshot.lastSaved = DateTime.Now;
			CS$<>8__locals1.byteContent = new byte[]
			{
				0, 0, 0, 0, 1, 2, 3, 4, 5, 6,
				7, 8, 9, 10, 0, 1, 2, 3, 4, 5
			};
			CS$<>8__locals1.txtContent = "abcdeefghei&*nc.a7d7^&*^)@$%^d/wqi321//312asrj\\n23";
			TestGenerator.Snapshot snapshot;
			using (new Utils.OnScopeExit(delegate
			{
				TestGenerator.useRomPaths = false;
				TestGenerator.<GenerateSnapshotCandidate__GENERATED>g__deleteTestDirectory|1_60("cleanup");
			}))
			{
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestsRwAndRom|55("ReadAllText", delegate(TestGenerator.TestBuilder blder)
				{
					blder.Append(File.ReadAllText(TestGenerator.PathPresent()));
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestsRwAndRom|55("ReadAllText -- Missing Path", delegate(TestGenerator.TestBuilder blder)
				{
					TestGenerator.SerializeAnyThrows(blder, true, delegate
					{
						blder.Append(File.ReadAllText(TestGenerator.PathMissing()));
					});
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestsRwAndRom|55("ReadAllText -- Invalid Path", delegate(TestGenerator.TestBuilder blder)
				{
					TestGenerator.SerializeAnyThrows(blder, true, delegate
					{
						blder.Append(File.ReadAllText(TestGenerator.PathInvalid()));
					});
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestsRwAndRom|55("ReadAllBytes", delegate(TestGenerator.TestBuilder blder)
				{
					blder.Append(File.ReadAllBytes(TestGenerator.PathPresent()));
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestsRwAndRom|55("ReadAllBytes -- Missing Path", delegate(TestGenerator.TestBuilder blder)
				{
					TestGenerator.SerializeAnyThrows(blder, true, delegate
					{
						blder.Append(File.ReadAllBytes(TestGenerator.PathMissing()));
					});
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestsRwAndRom|55("ReadAllBytes -- Invalid Path", delegate(TestGenerator.TestBuilder blder)
				{
					TestGenerator.SerializeAnyThrows(blder, true, delegate
					{
						blder.Append(File.ReadAllBytes(TestGenerator.PathInvalid()));
					});
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestRW|56("WriteAllText", delegate(TestGenerator.TestBuilder hashRef)
				{
					File.WriteAllText(TestGenerator.PathAvailableText(), CS$<>8__locals1.txtContent);
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestRW|56("WriteAllText -- Invalid Path", delegate(TestGenerator.TestBuilder blder)
				{
					bool flag = true;
					Action action;
					if ((action = CS$<>8__locals1.<>9__68) == null)
					{
						action = (CS$<>8__locals1.<>9__68 = delegate
						{
							File.WriteAllText(TestGenerator.PathInvalid(), CS$<>8__locals1.txtContent);
						});
					}
					TestGenerator.SerializeAnyThrows(blder, flag, action);
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestRW|56("WriteAllText -- Occupied Path", delegate(TestGenerator.TestBuilder blder)
				{
					TestGenerator.SerializeAnyThrows(blder, true, delegate
					{
						File.WriteAllText(TestGenerator.PathAvailableText(), "first text");
					});
					bool flag2 = true;
					Action action2;
					if ((action2 = CS$<>8__locals1.<>9__70) == null)
					{
						action2 = (CS$<>8__locals1.<>9__70 = delegate
						{
							File.WriteAllText(TestGenerator.PathAvailableText(), CS$<>8__locals1.txtContent);
						});
					}
					TestGenerator.SerializeAnyThrows(blder, flag2, action2);
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestRW|56("WriteAllBytes", delegate(TestGenerator.TestBuilder hashRef)
				{
					File.WriteAllBytes(TestGenerator.PathAvailableText(), CS$<>8__locals1.byteContent);
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestRW|56("WriteAllBytes -- Invalid Path", delegate(TestGenerator.TestBuilder blder)
				{
					bool flag3 = true;
					Action action3;
					if ((action3 = CS$<>8__locals1.<>9__71) == null)
					{
						action3 = (CS$<>8__locals1.<>9__71 = delegate
						{
							File.WriteAllBytes(TestGenerator.PathInvalid(), CS$<>8__locals1.byteContent);
						});
					}
					TestGenerator.SerializeAnyThrows(blder, flag3, action3);
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestRW|56("WriteAllBytes -- Occupied Path", delegate(TestGenerator.TestBuilder blder)
				{
					TestGenerator.SerializeAnyThrows(blder, true, delegate
					{
						File.WriteAllBytes(TestGenerator.PathAvailableText(), new byte[] { 0, 1, 2 });
					});
					bool flag4 = true;
					Action action4;
					if ((action4 = CS$<>8__locals1.<>9__73) == null)
					{
						action4 = (CS$<>8__locals1.<>9__73 = delegate
						{
							File.WriteAllBytes(TestGenerator.PathAvailableText(), CS$<>8__locals1.byteContent);
						});
					}
					TestGenerator.SerializeAnyThrows(blder, flag4, action4);
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestsRwAndRom|55("FileExists", delegate(TestGenerator.TestBuilder blder)
				{
					blder.Append(File.Exists(TestGenerator.PathPresent()));
					blder.Append(File.Exists(TestGenerator.PathMissing()));
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestRW|56("FileDelete -- Invalid Path", delegate(TestGenerator.TestBuilder blder)
				{
					TestGenerator.SerializeAnyThrows(blder, false, delegate
					{
						File.Delete(TestGenerator.PathInvalid());
					});
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestRW|56("FileDelete -- Missing Path", delegate(TestGenerator.TestBuilder blder)
				{
					TestGenerator.SerializeAnyThrows(blder, false, delegate
					{
						File.Delete(TestGenerator.PathMissing());
					});
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestRW|56("Copy -- overwrite: true", delegate(TestGenerator.TestBuilder hashRef)
				{
					File.WriteAllText(TestGenerator.GetPath("CopySource01.txt"), "copy source content 01");
					File.WriteAllText(TestGenerator.GetPath("CopySource02.txt"), "copy source content 02");
					File.Copy(TestGenerator.GetPath("CopySource01.txt"), TestGenerator.GetPath("CopySource02.txt"), true);
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestRW|56("Copy -- overwrite: false", delegate(TestGenerator.TestBuilder blder)
				{
					File.WriteAllText(TestGenerator.GetPath("CopySource01.txt"), "copy source content 01");
					File.WriteAllText(TestGenerator.GetPath("CopySource02.txt"), "copy source content 02");
					TestGenerator.SerializeAnyThrows(blder, true, delegate
					{
						File.Copy(TestGenerator.GetPath("CopySource01.txt"), TestGenerator.GetPath("CopySource02.txt"), false);
					});
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestRW|56("Copy -- missing source", delegate(TestGenerator.TestBuilder blder)
				{
					TestGenerator.SerializeAnyThrows(blder, true, delegate
					{
						File.Copy(TestGenerator.PathMissing(), TestGenerator.PathAvailableText(), false);
					});
					TestGenerator.SerializeAnyThrows(blder, true, delegate
					{
						File.Copy(TestGenerator.PathMissing(), TestGenerator.PathAvailableText(), true);
					});
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestRW|56("Copy -- folder source", delegate(TestGenerator.TestBuilder blder)
				{
					TestGenerator.SerializeAnyThrows(blder, true, delegate
					{
						File.Copy(TestGenerator.PathRootDirectory(), TestGenerator.PathAvailableText(), false);
					});
					TestGenerator.SerializeAnyThrows(blder, true, delegate
					{
						File.Copy(TestGenerator.PathRootDirectory(), TestGenerator.PathAvailableText(), true);
					});
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestsRwAndRom|55("File Info", delegate(TestGenerator.TestBuilder blder)
				{
					FileInfo fileInfo = new FileInfo(TestGenerator.PathPresent());
					TestGenerator.<GenerateSnapshotCandidate__GENERATED>g__serializeFileSystemInfo|1_24(fileInfo, blder);
					blder.Append(fileInfo.Length.ToString());
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestsRwAndRom|55("Directory Info", delegate(TestGenerator.TestBuilder blder)
				{
					TestGenerator.<GenerateSnapshotCandidate__GENERATED>g__serializeFileSystemInfo|1_24(new DirectoryInfo(TestGenerator.PathRootDirectory()), blder);
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestsRwAndRom|55("File Info -- Missing", delegate(TestGenerator.TestBuilder blder)
				{
					FileInfo fileInfo2 = new FileInfo(TestGenerator.PathMissing());
					blder.Append(fileInfo2.Exists);
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestsRwAndRom|55("Directory Info -- Missing", delegate(TestGenerator.TestBuilder blder)
				{
					DirectoryInfo directoryInfo = new DirectoryInfo(TestGenerator.PathMissing());
					blder.Append(directoryInfo.Exists);
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestsRwAndRom|55("File Info -- Missing Path", delegate(TestGenerator.TestBuilder hashRef)
				{
					TestGenerator.SerializeAnyThrows(hashRef, true, delegate
					{
						new FileInfo(TestGenerator.PathMissing());
					});
					TestGenerator.SerializeAnyThrows(hashRef, true, delegate
					{
						new FileInfo(TestGenerator.PathParentMissing());
					});
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestRW|56("DirectoryCreate -- Directory Parent Present", delegate(TestGenerator.TestBuilder blder)
				{
					Directory.CreateDirectory(TestGenerator.PathAvailableFolder());
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestRW|56("DirectoryCreate -- Directory Parent Missing", delegate(TestGenerator.TestBuilder blder)
				{
					Directory.CreateDirectory(TestGenerator.PathAvailableFolderNoParent());
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestRW|56("DirectoryCreate -- File Path", delegate(TestGenerator.TestBuilder blder)
				{
					TestGenerator.SerializeAnyThrows(blder, true, delegate
					{
						Directory.CreateDirectory(TestGenerator.PathAvailableByte());
					});
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestRW|56("DirectoryCreate -- Already Exists", delegate(TestGenerator.TestBuilder blder)
				{
					TestGenerator.SerializeAnyThrows(blder, true, delegate
					{
						Directory.CreateDirectory(TestGenerator.PathRootDirectory());
					});
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestRW|56("DirectoryExists", delegate(TestGenerator.TestBuilder blder)
				{
					blder.Append(Directory.Exists(TestGenerator.PathRootDirectory()));
					blder.Append(Directory.Exists(TestGenerator.PathMissing()));
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestRW|56("DirectoryDelete -- Recursive: true", delegate(TestGenerator.TestBuilder blder)
				{
					Directory.Delete(TestGenerator.PathRootDirectory(), true);
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestRW|56("DirectoryDelete -- Recursive: false", delegate(TestGenerator.TestBuilder blder)
				{
					Directory.Delete(TestGenerator.PathRootDirectory(), false);
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestsRwAndRom|55("EnumerateDirectories -- Search Option Top Level", delegate(TestGenerator.TestBuilder blder)
				{
					foreach (string text in Directory.EnumerateDirectories(TestGenerator.PathRootDirectory()))
					{
						blder.AppendFilePath(text, true);
					}
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestsRwAndRom|55("Enumerate Files -- (no other arguments)", delegate(TestGenerator.TestBuilder blder)
				{
					foreach (string text2 in Directory.EnumerateFiles(TestGenerator.PathRootDirectory()))
					{
						blder.AppendFilePath(text2, true);
					}
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestsRwAndRom|55("Enumerate Files -- Search: Shallow", delegate(TestGenerator.TestBuilder blder)
				{
					foreach (string text3 in TestGenerator.<GenerateSnapshotCandidate__GENERATED>g__sortPaths|1_62(Directory.EnumerateFiles(TestGenerator.PathRootDirectory(), "*", SearchOption.TopDirectoryOnly)))
					{
						blder.AppendFilePath(text3, true);
					}
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestsRwAndRom|55("Enumerate Files -- Search: All", delegate(TestGenerator.TestBuilder blder)
				{
					foreach (string text4 in TestGenerator.<GenerateSnapshotCandidate__GENERATED>g__sortPaths|1_62(Directory.EnumerateFiles(TestGenerator.PathRootDirectory(), "*", SearchOption.AllDirectories)))
					{
						blder.AppendFilePath(text4, true);
					}
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestsRwAndRom|55("Enumerate Files -- Pattern: *.txt -- Search: All", delegate(TestGenerator.TestBuilder blder)
				{
					foreach (string text5 in TestGenerator.<GenerateSnapshotCandidate__GENERATED>g__sortPaths|1_62(Directory.EnumerateFiles(TestGenerator.PathRootDirectory(), "*.txt", SearchOption.AllDirectories)))
					{
						blder.AppendFilePath(text5, true);
					}
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestsRwAndRom|55("Enumerate Files -- Pattern: *.asset -- Search: All", delegate(TestGenerator.TestBuilder blder)
				{
					foreach (string text6 in TestGenerator.<GenerateSnapshotCandidate__GENERATED>g__sortPaths|1_62(Directory.EnumerateFiles(TestGenerator.PathRootDirectory(), "*.asset", SearchOption.AllDirectories)))
					{
						blder.AppendFilePath(text6, true);
					}
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestsRwAndRom|55("Enumerate Files -- Pattern: *subFile* -- Search: All", delegate(TestGenerator.TestBuilder blder)
				{
					foreach (string text7 in TestGenerator.<GenerateSnapshotCandidate__GENERATED>g__sortPaths|1_62(Directory.EnumerateFiles(TestGenerator.PathRootDirectory(), "*subFile*", SearchOption.AllDirectories)))
					{
						blder.AppendFilePath(text7, true);
					}
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestsRwAndRom|55("Enumerate File System Entries -- Pattern: * -- Search: All", delegate(TestGenerator.TestBuilder blder)
				{
					List<FileSystemInfo> list = new DirectoryInfo(TestGenerator.PathRootDirectory()).EnumerateFileSystemInfos().ToList<FileSystemInfo>();
					list.Sort((FileSystemInfo a, FileSystemInfo b) => StringComparer.OrdinalIgnoreCase.Compare(a.FullName, b.FullName));
					foreach (FileSystemInfo fileSystemInfo in list)
					{
						blder.AppendFilePath(fileSystemInfo.FullName, true);
					}
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestsRwAndRom|55("Stream -- Read -- Length", delegate(TestGenerator.TestBuilder blder)
				{
					using (Stream stream = File.OpenRead(TestGenerator.PathPresent()))
					{
						blder.Append(stream.Length.ToString());
					}
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestsRwAndRom|55("Stream -- Read -- All", delegate(TestGenerator.TestBuilder blder)
				{
					using (Stream stream2 = File.OpenRead(TestGenerator.PathPresent()))
					{
						byte[] array = new byte[stream2.Length];
						blder.Append(stream2.Read(array, 0, array.Length).ToString());
						blder.Append(array);
					}
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestsRwAndRom|55("Stream -- Read -- with Offset ", delegate(TestGenerator.TestBuilder blder)
				{
					using (Stream stream3 = File.OpenRead(TestGenerator.PathPresent()))
					{
						byte[] array2 = new byte[4];
						blder.Append(stream3.Read(array2, 1, 2).ToString());
						blder.Append(array2);
					}
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestsRwAndRom|55("Stream -- Read -- with Seek & Offset", delegate(TestGenerator.TestBuilder blder)
				{
					using (Stream stream4 = File.OpenRead(TestGenerator.PathPresent()))
					{
						byte[] array3 = new byte[10];
						stream4.Seek(1L, SeekOrigin.Begin);
						stream4.Read(array3, 0, 2);
						stream4.Read(array3, 2, 2);
						stream4.Seek(-4L, SeekOrigin.End);
						stream4.Read(array3, 6, 2);
						stream4.Seek(-8L, SeekOrigin.Current);
						stream4.Read(array3, 8, 2);
						blder.Append(array3);
					}
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestsRwAndRom|55("Stream -- Read -- from stream end", delegate(TestGenerator.TestBuilder blder)
				{
					using (Stream stream5 = File.OpenRead(TestGenerator.PathPresent()))
					{
						stream5.Seek(0L, SeekOrigin.End);
						byte[] array4 = new byte[1];
						blder.Append(stream5.Read(array4, 0, 1).ToString());
						blder.Append(array4);
					}
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestsRwAndRom|55("Stream -- Read -- Missing", delegate(TestGenerator.TestBuilder blder)
				{
					TestGenerator.SerializeAnyThrows(blder, true, delegate
					{
						using (File.OpenRead(TestGenerator.PathMissing()))
						{
						}
					});
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestRW|56("Stream -- Write -- Open Missing", delegate(TestGenerator.TestBuilder blder)
				{
					using (File.OpenWrite(TestGenerator.PathAvailableByte()))
					{
					}
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestRW|56("Stream -- Write -- Open & Flush Empty", delegate(TestGenerator.TestBuilder blder)
				{
					using (Stream stream8 = File.OpenWrite(TestGenerator.PathAvailableByte()))
					{
						stream8.Flush();
					}
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestRW|56("Stream -- Write -- Byte", delegate(TestGenerator.TestBuilder blder)
				{
					using (Stream stream9 = File.OpenWrite(TestGenerator.PathAvailableByte()))
					{
						stream9.WriteByte(30);
						stream9.Flush();
					}
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestRW|56("Stream --  Write -- Buffer Offset & Length", delegate(TestGenerator.TestBuilder blder)
				{
					using (Stream stream10 = File.OpenWrite(TestGenerator.PathAvailableByte()))
					{
						stream10.Write(CS$<>8__locals1.byteContent, 4, CS$<>8__locals1.byteContent.Length - 8);
						stream10.Flush();
					}
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestRW|56("Stream --  Write -- Buffer Offset & Length Seek", delegate(TestGenerator.TestBuilder blder)
				{
					using (Stream stream11 = File.OpenWrite(TestGenerator.PathAvailableByte()))
					{
						stream11.WriteByte(5);
						stream11.WriteByte(3);
						stream11.WriteByte(8);
						stream11.Seek(1L, SeekOrigin.Begin);
						stream11.WriteByte(9);
						stream11.Flush();
					}
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestRW|56("Stream -- Write -- Open Existing Prepend & Append", delegate(TestGenerator.TestBuilder blder)
				{
					using (Stream stream12 = File.OpenWrite(TestGenerator.PathPresent()))
					{
						stream12.WriteByte(9);
						stream12.WriteByte(9);
						stream12.Seek(0L, SeekOrigin.End);
						stream12.WriteByte(9);
						stream12.WriteByte(9);
						stream12.Flush();
					}
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestRW|56("File -- AppendAllText", delegate(TestGenerator.TestBuilder blder)
				{
					File.AppendAllText(TestGenerator.PathPresent(), "appendText");
				});
				CS$<>8__locals1.<GenerateSnapshotCandidate__GENERATED>g__runTestRW|56("File -- AppendAllTextMissing", delegate(TestGenerator.TestBuilder blder)
				{
					TestGenerator.SerializeAnyThrows(blder, true, delegate
					{
						File.AppendAllText(TestGenerator.PathMissing(), "appendText");
					});
				});
				snapshot = CS$<>8__locals1.snapshot;
			}
			return snapshot;
		}

		// Token: 0x06001E91 RID: 7825 RVA: 0x00096090 File Offset: 0x00094290
		private static string SanitizePath(string path)
		{
			return path.Replace("\\", "/");
		}

		// Token: 0x06001E92 RID: 7826 RVA: 0x000960A4 File Offset: 0x000942A4
		public static bool GenerateTestSnapshot(TestGenerator.TestDelegate test, out Exception exOut, out TestGenerator.Snapshot snapshot)
		{
			exOut = null;
			try
			{
				snapshot = test();
			}
			catch (Exception ex)
			{
				exOut = ex;
				snapshot = new TestGenerator.Snapshot();
				global::UnityEngine.Debug.LogError("Unexpected exception occured while testing, invalidating test results. Exception: " + ex.ToString());
			}
			return exOut == null;
		}

		// Token: 0x06001E93 RID: 7827 RVA: 0x000960F8 File Offset: 0x000942F8
		private static string PathRootDirectory()
		{
			return TestGenerator.GetPath(null);
		}

		// Token: 0x06001E94 RID: 7828 RVA: 0x00096100 File Offset: 0x00094300
		private static string GetPath(string path)
		{
			string text = "IOParityTesting";
			if (path != null)
			{
				text = Path.Combine(new string[] { text, path });
			}
			if (TestGenerator.useRomPaths)
			{
				text = Path.Combine(new string[]
				{
					Application.streamingAssetsPath,
					text
				});
			}
			return text;
		}

		// Token: 0x06001E95 RID: 7829 RVA: 0x0009614A File Offset: 0x0009434A
		private static string PathPresent()
		{
			return TestGenerator.GetPath("AlwaysPresentFile.asset");
		}

		// Token: 0x06001E96 RID: 7830 RVA: 0x00096156 File Offset: 0x00094356
		private static string PathMissing()
		{
			return TestGenerator.GetPath("NeverPresentFilePath.asset");
		}

		// Token: 0x06001E97 RID: 7831 RVA: 0x00096162 File Offset: 0x00094362
		private static string PathInvalid()
		{
			return TestGenerator.GetPath("fqw\\e/&/(\\0.a.((sset");
		}

		// Token: 0x06001E98 RID: 7832 RVA: 0x0009616E File Offset: 0x0009436E
		private static string PathParentMissing()
		{
			return TestGenerator.GetPath(Path.Combine(new string[] { "NeverPresentFolder", "NeverPresentChildFilePath.asset" }));
		}

		// Token: 0x06001E99 RID: 7833 RVA: 0x00096190 File Offset: 0x00094390
		private static string PathAvailableText()
		{
			return TestGenerator.GetPath("FileIOTest.txt");
		}

		// Token: 0x06001E9A RID: 7834 RVA: 0x0009619C File Offset: 0x0009439C
		private static string PathAvailableByte()
		{
			return TestGenerator.GetPath("FileIOTest.bytes");
		}

		// Token: 0x06001E9B RID: 7835 RVA: 0x000961A8 File Offset: 0x000943A8
		private static string PathAvailableFolder()
		{
			return TestGenerator.GetPath("AvailableFolder");
		}

		// Token: 0x06001E9C RID: 7836 RVA: 0x000961B4 File Offset: 0x000943B4
		private static string PathAvailableFolderNoParent()
		{
			return TestGenerator.GetPath(Path.Combine(new string[] { "AvailableFolderParent", "AvailableFolderChild" }));
		}

		// Token: 0x06001E9D RID: 7837 RVA: 0x000961D8 File Offset: 0x000943D8
		private static void SerializeAnyThrows(TestGenerator.TestBuilder blder, bool careAboutExceptionType, Action action)
		{
			try
			{
				action();
			}
			catch (Exception ex)
			{
				if (careAboutExceptionType)
				{
					blder.Append(ex.GetType().FullName);
				}
				else
				{
					blder.Append(typeof(Exception).FullName);
				}
			}
		}

		// Token: 0x06001E9E RID: 7838 RVA: 0x0009622C File Offset: 0x0009442C
		[CompilerGenerated]
		internal static void <GenerateSnapshotBaseline>g__serializeFileSystemInfo|0_24(FileSystemInfo info, TestGenerator.TestBuilder blder)
		{
			blder.AppendFilePath(info.FullName ?? "", true);
			blder.Append(info.Name ?? "");
			blder.Append(string.Format("{0}", info.Exists));
			blder.Append(info.Extension ?? "");
			blder.Append(string.Format("{0}", info.Attributes.HasFlag(FileAttributes.Directory)));
			blder.Append(string.Format("{0}", info.Attributes.HasFlag(FileAttributes.Hidden)));
		}

		// Token: 0x06001E9F RID: 7839 RVA: 0x000962EB File Offset: 0x000944EB
		[CompilerGenerated]
		[Conditional("DEBUGGING_TESTS")]
		internal static void <GenerateSnapshotBaseline>g__log|0_58(string msg)
		{
			global::UnityEngine.Debug.Log(msg);
		}

		// Token: 0x06001EA0 RID: 7840 RVA: 0x000962F4 File Offset: 0x000944F4
		[CompilerGenerated]
		internal static void <GenerateSnapshotBaseline>g__resetTestDirectory|0_59(string testName)
		{
			try
			{
				TestGenerator.<GenerateSnapshotBaseline>g__deleteTestDirectory|0_60(testName);
				Directory.CreateDirectory(TestGenerator.PathRootDirectory());
				File.WriteAllText(TestGenerator.PathPresent(), "present-path-string-content1234567*7d7^&*^\\)@$%^");
				string path = TestGenerator.GetPath(Path.Combine(new string[] { "SubDirectory1", "SubDirectory2", "SubDirectory3", "SubDirectory4" }));
				Directory.CreateDirectory(path);
				File.WriteAllText(Path.Combine(new string[] { path, "subFile1.txt" }), "present-path-string-content1234567*7d7^&*^\\)@$%^");
				File.WriteAllText(Path.Combine(new string[] { path, "subFile2.text" }), "present-path-string-content1234567*7d7^&*^\\)@$%^");
				File.WriteAllText(Path.Combine(new string[] { path, "subFile3.note" }), "present-path-string-content1234567*7d7^&*^\\)@$%^");
			}
			catch (Exception ex)
			{
				global::UnityEngine.Debug.LogError(string.Format("Could not {0}. So tests will fail. {1}", "resetTestDirectory", ex));
			}
		}

		// Token: 0x06001EA1 RID: 7841 RVA: 0x000963E8 File Offset: 0x000945E8
		[CompilerGenerated]
		internal static void <GenerateSnapshotBaseline>g__deleteTestDirectory|0_60(string name)
		{
			try
			{
				if (Directory.Exists(TestGenerator.PathRootDirectory()))
				{
					Directory.Delete(TestGenerator.PathRootDirectory(), true);
				}
			}
			catch (Exception ex)
			{
				global::UnityEngine.Debug.LogError(string.Format("Could not delete test directory for test \"{0}\"\n. {1}", name, ex));
			}
		}

		// Token: 0x06001EA2 RID: 7842 RVA: 0x00096434 File Offset: 0x00094634
		[CompilerGenerated]
		internal static void <GenerateSnapshotBaseline>g__serializeDirectoryState|0_61(TestGenerator.TestBuilder blder)
		{
			if (!Directory.Exists(TestGenerator.PathRootDirectory()))
			{
				blder.Append("Test folder is missing");
				return;
			}
			TestGenerator.<GenerateSnapshotBaseline>g__getDirectoryStateRecursive|0_63(TestGenerator.PathRootDirectory(), blder, 1);
		}

		// Token: 0x06001EA3 RID: 7843 RVA: 0x0009645A File Offset: 0x0009465A
		[CompilerGenerated]
		internal static List<string> <GenerateSnapshotBaseline>g__sortPaths|0_62(IEnumerable<string> paths)
		{
			List<string> list = paths.ToList<string>();
			list.Sort(StringComparer.OrdinalIgnoreCase);
			return list;
		}

		// Token: 0x06001EA4 RID: 7844 RVA: 0x00096470 File Offset: 0x00094670
		[CompilerGenerated]
		internal static void <GenerateSnapshotBaseline>g__getDirectoryStateRecursive|0_63(string currentDirPath, TestGenerator.TestBuilder blder, int depth)
		{
			TestGenerator.<>c__DisplayClass0_5 CS$<>8__locals1;
			CS$<>8__locals1.blder = blder;
			foreach (string text in TestGenerator.<GenerateSnapshotBaseline>g__sortPaths|0_62(Directory.EnumerateFiles(currentDirPath)))
			{
				if (!(Path.GetExtension(text) == ".meta"))
				{
					TestGenerator.<GenerateSnapshotBaseline>g__appendFileHierarchyMarkers|0_88(">", depth, ref CS$<>8__locals1);
					CS$<>8__locals1.blder.AppendFilePath(text, false);
					TestGenerator.<GenerateSnapshotBaseline>g__appendFileHierarchyMarkers|0_88("c", depth + 1, ref CS$<>8__locals1);
					CS$<>8__locals1.blder.Append(File.ReadAllText(text));
				}
			}
			List<string> list = Directory.EnumerateDirectories(currentDirPath).ToList<string>();
			list.Sort(StringComparer.OrdinalIgnoreCase);
			foreach (string text2 in list)
			{
				TestGenerator.<GenerateSnapshotBaseline>g__appendFileHierarchyMarkers|0_88("[", depth, ref CS$<>8__locals1);
				CS$<>8__locals1.blder.AppendFilePath(text2, false);
				TestGenerator.<GenerateSnapshotBaseline>g__getDirectoryStateRecursive|0_63(text2, CS$<>8__locals1.blder, depth + 1);
			}
		}

		// Token: 0x06001EA5 RID: 7845 RVA: 0x0009658C File Offset: 0x0009478C
		[CompilerGenerated]
		internal static void <GenerateSnapshotBaseline>g__appendFileHierarchyMarkers|0_88(string arrowHead, int depth, ref TestGenerator.<>c__DisplayClass0_5 A_2)
		{
			A_2.blder.Append("\n");
			for (int i = 0; i < depth; i++)
			{
				A_2.blder.Append("-");
			}
			A_2.blder.Append(arrowHead);
			A_2.blder.Append(" ");
		}

		// Token: 0x06001EA6 RID: 7846 RVA: 0x000965E4 File Offset: 0x000947E4
		[CompilerGenerated]
		internal static void <GenerateSnapshotCandidate__GENERATED>g__serializeFileSystemInfo|1_24(FileSystemInfo info, TestGenerator.TestBuilder blder)
		{
			blder.AppendFilePath(info.FullName ?? "", true);
			blder.Append(info.Name ?? "");
			blder.Append(string.Format("{0}", info.Exists));
			blder.Append(info.Extension ?? "");
			blder.Append(string.Format("{0}", info.Attributes.HasFlag(FileAttributes.Directory)));
			blder.Append(string.Format("{0}", info.Attributes.HasFlag(FileAttributes.Hidden)));
		}

		// Token: 0x06001EA7 RID: 7847 RVA: 0x000966A3 File Offset: 0x000948A3
		[CompilerGenerated]
		[Conditional("DEBUGGING_TESTS")]
		internal static void <GenerateSnapshotCandidate__GENERATED>g__log|1_58(string msg)
		{
			global::UnityEngine.Debug.Log(msg);
		}

		// Token: 0x06001EA8 RID: 7848 RVA: 0x000966AC File Offset: 0x000948AC
		[CompilerGenerated]
		internal static void <GenerateSnapshotCandidate__GENERATED>g__resetTestDirectory|1_59(string testName)
		{
			try
			{
				TestGenerator.<GenerateSnapshotCandidate__GENERATED>g__deleteTestDirectory|1_60(testName);
				Directory.CreateDirectory(TestGenerator.PathRootDirectory());
				File.WriteAllText(TestGenerator.PathPresent(), "present-path-string-content1234567*7d7^&*^\\)@$%^");
				string path = TestGenerator.GetPath(Path.Combine(new string[] { "SubDirectory1", "SubDirectory2", "SubDirectory3", "SubDirectory4" }));
				Directory.CreateDirectory(path);
				File.WriteAllText(Path.Combine(new string[] { path, "subFile1.txt" }), "present-path-string-content1234567*7d7^&*^\\)@$%^");
				File.WriteAllText(Path.Combine(new string[] { path, "subFile2.text" }), "present-path-string-content1234567*7d7^&*^\\)@$%^");
				File.WriteAllText(Path.Combine(new string[] { path, "subFile3.note" }), "present-path-string-content1234567*7d7^&*^\\)@$%^");
			}
			catch (Exception ex)
			{
				global::UnityEngine.Debug.LogError(string.Format("Could not {0}. So tests will fail. {1}", "resetTestDirectory", ex));
			}
		}

		// Token: 0x06001EA9 RID: 7849 RVA: 0x000967A0 File Offset: 0x000949A0
		[CompilerGenerated]
		internal static void <GenerateSnapshotCandidate__GENERATED>g__deleteTestDirectory|1_60(string name)
		{
			try
			{
				if (Directory.Exists(TestGenerator.PathRootDirectory()))
				{
					Directory.Delete(TestGenerator.PathRootDirectory(), true);
				}
			}
			catch (Exception ex)
			{
				global::UnityEngine.Debug.LogError(string.Format("Could not delete test directory for test \"{0}\"\n. {1}", name, ex));
			}
		}

		// Token: 0x06001EAA RID: 7850 RVA: 0x000967EC File Offset: 0x000949EC
		[CompilerGenerated]
		internal static void <GenerateSnapshotCandidate__GENERATED>g__serializeDirectoryState|1_61(TestGenerator.TestBuilder blder)
		{
			if (!Directory.Exists(TestGenerator.PathRootDirectory()))
			{
				blder.Append("Test folder is missing");
				return;
			}
			TestGenerator.<GenerateSnapshotCandidate__GENERATED>g__getDirectoryStateRecursive|1_63(TestGenerator.PathRootDirectory(), blder, 1);
		}

		// Token: 0x06001EAB RID: 7851 RVA: 0x00096812 File Offset: 0x00094A12
		[CompilerGenerated]
		internal static List<string> <GenerateSnapshotCandidate__GENERATED>g__sortPaths|1_62(IEnumerable<string> paths)
		{
			List<string> list = paths.ToList<string>();
			list.Sort(StringComparer.OrdinalIgnoreCase);
			return list;
		}

		// Token: 0x06001EAC RID: 7852 RVA: 0x00096828 File Offset: 0x00094A28
		[CompilerGenerated]
		internal static void <GenerateSnapshotCandidate__GENERATED>g__getDirectoryStateRecursive|1_63(string currentDirPath, TestGenerator.TestBuilder blder, int depth)
		{
			TestGenerator.<>c__DisplayClass1_5 CS$<>8__locals1;
			CS$<>8__locals1.blder = blder;
			foreach (string text in TestGenerator.<GenerateSnapshotCandidate__GENERATED>g__sortPaths|1_62(Directory.EnumerateFiles(currentDirPath)))
			{
				if (!(Path.GetExtension(text) == ".meta"))
				{
					TestGenerator.<GenerateSnapshotCandidate__GENERATED>g__appendFileHierarchyMarkers|1_88(">", depth, ref CS$<>8__locals1);
					CS$<>8__locals1.blder.AppendFilePath(text, false);
					TestGenerator.<GenerateSnapshotCandidate__GENERATED>g__appendFileHierarchyMarkers|1_88("c", depth + 1, ref CS$<>8__locals1);
					CS$<>8__locals1.blder.Append(File.ReadAllText(text));
				}
			}
			List<string> list = Directory.EnumerateDirectories(currentDirPath).ToList<string>();
			list.Sort(StringComparer.OrdinalIgnoreCase);
			foreach (string text2 in list)
			{
				TestGenerator.<GenerateSnapshotCandidate__GENERATED>g__appendFileHierarchyMarkers|1_88("[", depth, ref CS$<>8__locals1);
				CS$<>8__locals1.blder.AppendFilePath(text2, false);
				TestGenerator.<GenerateSnapshotCandidate__GENERATED>g__getDirectoryStateRecursive|1_63(text2, CS$<>8__locals1.blder, depth + 1);
			}
		}

		// Token: 0x06001EAD RID: 7853 RVA: 0x00096944 File Offset: 0x00094B44
		[CompilerGenerated]
		internal static void <GenerateSnapshotCandidate__GENERATED>g__appendFileHierarchyMarkers|1_88(string arrowHead, int depth, ref TestGenerator.<>c__DisplayClass1_5 A_2)
		{
			A_2.blder.Append("\n");
			for (int i = 0; i < depth; i++)
			{
				A_2.blder.Append("-");
			}
			A_2.blder.Append(arrowHead);
			A_2.blder.Append(" ");
		}

		// Token: 0x04000FFD RID: 4093
		private static bool useRomPaths;

		// Token: 0x04000FFE RID: 4094
		private const string TestDirectoryName = "IOParityTesting";

		// Token: 0x0200171F RID: 5919
		// (Invoke) Token: 0x0600DBF5 RID: 56309
		public delegate TestGenerator.Snapshot TestDelegate();

		// Token: 0x02001720 RID: 5920
		public class Snapshot
		{
			// Token: 0x04005CFA RID: 23802
			public Dictionary<string, string> tests = new Dictionary<string, string>();

			// Token: 0x04005CFB RID: 23803
			public DateTime lastSaved;
		}

		// Token: 0x02001721 RID: 5921
		public class TestBuilder
		{
			// Token: 0x0600DBF9 RID: 56313 RVA: 0x005072B1 File Offset: 0x005054B1
			public void Append(string txt)
			{
				this.builder.Append(txt);
			}

			// Token: 0x0600DBFA RID: 56314 RVA: 0x005072C0 File Offset: 0x005054C0
			public void AppendFilePath(string path, bool addLineBreakAtEnd = true)
			{
				if (Path.GetExtension(path) == ".meta")
				{
					return;
				}
				string text = "IOParityTesting";
				if (!path.Contains(path))
				{
					global::UnityEngine.Debug.LogError("Path " + path + " does not contain folder IOParityTesting and therefore cannot be serialized without breaking the tests. Will not be part of the serialized snapshot state.");
					this.Append(path + " (error appending)");
				}
				ReadOnlySpan<char> readOnlySpan = path.AsSpan(0);
				for (int i = 1; i < path.Length; i++)
				{
					int num = i;
					int num2 = path.Length - i;
					ReadOnlySpan<char> readOnlySpan2 = path.AsSpan(num2, num);
					if (readOnlySpan2.Contains(text.AsSpan(0), StringComparison.Ordinal))
					{
						readOnlySpan = readOnlySpan2;
						break;
					}
				}
				string text2 = TestGenerator.SanitizePath(readOnlySpan.ToString());
				this.Append(text2);
				if (addLineBreakAtEnd)
				{
					this.Append("\n");
				}
			}

			// Token: 0x0600DBFB RID: 56315 RVA: 0x00507388 File Offset: 0x00505588
			public void Append(byte[] bytes)
			{
				foreach (byte b in bytes)
				{
					this.builder.Append(b.ToString("X2"));
				}
			}

			// Token: 0x0600DBFC RID: 56316 RVA: 0x005073C1 File Offset: 0x005055C1
			public void Append(bool boolean)
			{
				if (boolean)
				{
					this.builder.Append("true");
					return;
				}
				this.builder.Append("false");
			}

			// Token: 0x0600DBFD RID: 56317 RVA: 0x005073E9 File Offset: 0x005055E9
			public string Serialize()
			{
				return this.builder.ToString();
			}

			// Token: 0x04005CFC RID: 23804
			private StringBuilder builder = new StringBuilder();
		}
	}
}
