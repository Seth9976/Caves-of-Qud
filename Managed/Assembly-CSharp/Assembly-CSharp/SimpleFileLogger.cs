using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Platform.IO;
using XRL;

// Token: 0x02000067 RID: 103
public class SimpleFileLogger
{
	// Token: 0x06000382 RID: 898 RVA: 0x0001778C File Offset: 0x0001598C
	public static async Task<SimpleFileLogger> Create(string filePath)
	{
		SimpleFileLogger logger = new SimpleFileLogger();
		logger.filePath = DataManager.SavePath(filePath);
		TaskAwaiter<bool> taskAwaiter = File.ExistsAsync(filePath).GetAwaiter();
		if (!taskAwaiter.IsCompleted)
		{
			await taskAwaiter;
			TaskAwaiter<bool> taskAwaiter2;
			taskAwaiter = taskAwaiter2;
			taskAwaiter2 = default(TaskAwaiter<bool>);
		}
		if (taskAwaiter.GetResult())
		{
			(await Blob.CopyAsync(filePath, filePath + ".prev", true)).LogIfErrored();
		}
		(await Blob.WriteAllTextAsync(filePath, "--log start--\n")).LogIfErrored();
		return logger;
	}

	// Token: 0x06000383 RID: 899 RVA: 0x000177D0 File Offset: 0x000159D0
	public void Info(string logRaw)
	{
		string text = this.DecorateLog(logRaw);
		File.AppendAllText(this.filePath, text);
	}

	// Token: 0x06000384 RID: 900 RVA: 0x000177F4 File Offset: 0x000159F4
	public void Error(string logRaw)
	{
		string text = this.DecorateLog(logRaw);
		File.AppendAllText(this.filePath, text);
	}

	// Token: 0x06000385 RID: 901 RVA: 0x00017818 File Offset: 0x00015A18
	private string DecorateLog(string log)
	{
		return "[" + DateTime.Now.ToString("s") + "] " + log + "\n";
	}

	// Token: 0x040002C0 RID: 704
	private string filePath;
}
