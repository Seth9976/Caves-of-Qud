using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;

namespace System.CodeDom.Compiler
{
	/// <summary>Provides command execution functions for invoking compilers. This class cannot be inherited.</summary>
	// Token: 0x0200035F RID: 863
	[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
	public static class Executor
	{
		/// <summary>Executes the command using the specified temporary files and waits for the call to return.</summary>
		/// <param name="cmd">The command to execute. </param>
		/// <param name="tempFiles">A <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> with which to manage and store references to intermediate files generated during compilation. </param>
		// Token: 0x06001C94 RID: 7316 RVA: 0x000674FC File Offset: 0x000656FC
		public static void ExecWait(string cmd, TempFileCollection tempFiles)
		{
			string text = null;
			string text2 = null;
			Executor.ExecWaitWithCapture(cmd, Environment.CurrentDirectory, tempFiles, ref text, ref text2);
		}

		/// <summary>Executes the specified command using the specified user token, current directory, and temporary files; then waits for the call to return, storing output and error information from the compiler in the specified strings.</summary>
		/// <returns>The return value from the compiler.</returns>
		/// <param name="userToken">The token to start the compiler process with. </param>
		/// <param name="cmd">The command to execute. </param>
		/// <param name="currentDir">The directory to start the process in. </param>
		/// <param name="tempFiles">A <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> with which to manage and store references to intermediate files generated during compilation. </param>
		/// <param name="outputName">A reference to a string that will store the compiler's message output. </param>
		/// <param name="errorName">A reference to a string that will store the name of the error or errors encountered. </param>
		// Token: 0x06001C95 RID: 7317 RVA: 0x00067520 File Offset: 0x00065720
		[SecurityPermission(SecurityAction.Assert, ControlPrincipal = true)]
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public static int ExecWaitWithCapture(IntPtr userToken, string cmd, string currentDir, TempFileCollection tempFiles, ref string outputName, ref string errorName)
		{
			int num;
			using (WindowsIdentity.Impersonate(userToken))
			{
				num = Executor.InternalExecWaitWithCapture(cmd, currentDir, tempFiles, ref outputName, ref errorName);
			}
			return num;
		}

		/// <summary>Executes the specified command using the specified user token and temporary files, and waits for the call to return, storing output and error information from the compiler in the specified strings.</summary>
		/// <returns>The return value from the compiler.</returns>
		/// <param name="userToken">The token to start the compiler process with. </param>
		/// <param name="cmd">The command to execute. </param>
		/// <param name="tempFiles">A <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> with which to manage and store references to intermediate files generated during compilation. </param>
		/// <param name="outputName">A reference to a string that will store the compiler's message output. </param>
		/// <param name="errorName">A reference to a string that will store the name of the error or errors encountered. </param>
		// Token: 0x06001C96 RID: 7318 RVA: 0x00067560 File Offset: 0x00065760
		public static int ExecWaitWithCapture(IntPtr userToken, string cmd, TempFileCollection tempFiles, ref string outputName, ref string errorName)
		{
			return Executor.ExecWaitWithCapture(userToken, cmd, Environment.CurrentDirectory, tempFiles, ref outputName, ref errorName);
		}

		/// <summary>Executes the specified command using the specified current directory and temporary files, and waits for the call to return, storing output and error information from the compiler in the specified strings.</summary>
		/// <returns>The return value from the compiler.</returns>
		/// <param name="cmd">The command to execute. </param>
		/// <param name="currentDir">The current directory. </param>
		/// <param name="tempFiles">A <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> with which to manage and store references to intermediate files generated during compilation. </param>
		/// <param name="outputName">A reference to a string that will store the compiler's message output. </param>
		/// <param name="errorName">A reference to a string that will store the name of the error or errors encountered. </param>
		// Token: 0x06001C97 RID: 7319 RVA: 0x00067572 File Offset: 0x00065772
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public static int ExecWaitWithCapture(string cmd, string currentDir, TempFileCollection tempFiles, ref string outputName, ref string errorName)
		{
			return Executor.InternalExecWaitWithCapture(cmd, currentDir, tempFiles, ref outputName, ref errorName);
		}

		/// <summary>Executes the specified command using the specified temporary files and waits for the call to return, storing output and error information from the compiler in the specified strings.</summary>
		/// <returns>The return value from the compiler.</returns>
		/// <param name="cmd">The command to execute. </param>
		/// <param name="tempFiles">A <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> with which to manage and store references to intermediate files generated during compilation. </param>
		/// <param name="outputName">A reference to a string that will store the compiler's message output. </param>
		/// <param name="errorName">A reference to a string that will store the name of the error or errors encountered. </param>
		// Token: 0x06001C98 RID: 7320 RVA: 0x0006757F File Offset: 0x0006577F
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public static int ExecWaitWithCapture(string cmd, TempFileCollection tempFiles, ref string outputName, ref string errorName)
		{
			return Executor.InternalExecWaitWithCapture(cmd, Environment.CurrentDirectory, tempFiles, ref outputName, ref errorName);
		}

		// Token: 0x06001C99 RID: 7321 RVA: 0x00067590 File Offset: 0x00065790
		private static int InternalExecWaitWithCapture(string cmd, string currentDir, TempFileCollection tempFiles, ref string outputName, ref string errorName)
		{
			if (cmd == null || cmd.Length == 0)
			{
				throw new ExternalException(global::Locale.GetText("No command provided for execution."));
			}
			if (outputName == null)
			{
				outputName = tempFiles.AddExtension("out");
			}
			if (errorName == null)
			{
				errorName = tempFiles.AddExtension("err");
			}
			int num = -1;
			Process process = new Process();
			process.StartInfo.FileName = cmd;
			process.StartInfo.CreateNoWindow = true;
			process.StartInfo.UseShellExecute = false;
			process.StartInfo.RedirectStandardOutput = true;
			process.StartInfo.RedirectStandardError = true;
			process.StartInfo.WorkingDirectory = currentDir;
			try
			{
				process.Start();
				Executor.ProcessResultReader processResultReader = new Executor.ProcessResultReader(process.StandardOutput, outputName);
				Thread thread = new Thread(new ThreadStart(new Executor.ProcessResultReader(process.StandardError, errorName).Read));
				thread.Start();
				processResultReader.Read();
				thread.Join();
				process.WaitForExit();
			}
			finally
			{
				num = process.ExitCode;
				process.Close();
			}
			return num;
		}

		// Token: 0x02000360 RID: 864
		private class ProcessResultReader
		{
			// Token: 0x06001C9A RID: 7322 RVA: 0x00067698 File Offset: 0x00065898
			public ProcessResultReader(StreamReader reader, string file)
			{
				this.reader = reader;
				this.file = file;
			}

			// Token: 0x06001C9B RID: 7323 RVA: 0x000676B0 File Offset: 0x000658B0
			public void Read()
			{
				StreamWriter streamWriter = new StreamWriter(this.file);
				try
				{
					string text;
					while ((text = this.reader.ReadLine()) != null)
					{
						streamWriter.WriteLine(text);
					}
				}
				finally
				{
					streamWriter.Close();
				}
			}

			// Token: 0x04000EAA RID: 3754
			private StreamReader reader;

			// Token: 0x04000EAB RID: 3755
			private string file;
		}
	}
}
