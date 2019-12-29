using System.IO.Compression;
using System.Text;
using System;
using System.IO;
using System.Reflection;
using System.Linq;

namespace Streams
{
	class Program
	{
		static void Main(string[] args)
		{
			ReadFileAndWriteToConsole("contas.txt");

			Console.ReadKey();
		}

		private static void ReadFileAndWriteToConsole(string fileName)
		{
			var filePath = TryToGetDirectoryThatHasFile(fileName);

			var buffer = new byte[1024];
			var readBytes = -1;

			using (var fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
			{
				while ((readBytes = fileStream.Read(buffer, 0, buffer.Length)) != 0)
				{
					var texto = UTF8Encoding.UTF8.GetString(buffer, 0, readBytes);
					Console.Write(texto);
				}
			}
		}

		private static string TryToGetDirectoryThatHasFile(string fileName)
		{
			var directory = new DirectoryInfo(Directory.GetCurrentDirectory());

			while (!directory.GetFiles(fileName).Any())
			{
				if (directory.Root.FullName.Equals(directory.FullName))
					throw new DirectoryNotFoundException();

				directory = directory.Parent;

			}

			return Path.Combine(directory.FullName, fileName);
		}

		private static void DriverInfo()
		{
			// Drivers Info 
			var drivers = DriveInfo.GetDrives();

			foreach (var driver in drivers)
			{
				//Driver name
				System.Console.WriteLine($"DriverName {driver.Name}");

				//Status
				System.Console.WriteLine($"Ready: {driver.IsReady}");

				//Type
				System.Console.WriteLine($"Type: {driver.DriveType}");

				///Format
				System.Console.WriteLine($"Format: {driver.DriveFormat}");

				//size
				System.Console.WriteLine($"Format: {driver.TotalSize}");
			}
		}

		private static void Descompactar()
		{
			using (var fs = new FileStream("Texto.zip", FileMode.OpenOrCreate, FileAccess.ReadWrite))
			using (var gzipStream = new GZipStream(fs, CompressionMode.Decompress))
			using (var sw = new StreamReader(gzipStream))
			{
				System.Console.WriteLine(sw.ReadToEnd());
			}
		}

		private static void Compactar(string message)
		{
			using (var fs = new FileStream("Texto.zip", FileMode.OpenOrCreate, FileAccess.ReadWrite))
			using (var gzipStream = new GZipStream(fs, CompressionMode.Compress))
			using (var sw = new StreamWriter(gzipStream))
			{
				sw.Write(message);
			}
		}

		private static void WriteMessage()
		{
			var message = "Hello World!";

			var array = Encoding.UTF8.GetBytes(message);
			var offset = 0;
			var count = message.Length;

			using (var fs = new FileStream("output.txt", FileMode.Create, FileAccess.Write))
			{
				fs.Write(array, offset, count);
			}
		}

		private static void ReadingAction(int offset, int count, byte[] array)
		{
			Action<Stream, int, int, byte[]> action = ReadAction();

			using (var fs = new FileStream("animals.txt", FileMode.Open, FileAccess.Read))
			{
				action(fs, offset, count, array);
			}
		}

		private static Action<Stream, int, int, byte[]> ReadAction()
		{
			return (stream, offset, count, arr) =>
			{
				stream.Read(arr, offset, count);

				foreach (var @char in arr)
					System.Console.Write((char)@char);
			};
		}

		private static void Lendo10bytes()
		{
			using (var fs = new FileStream("animals.txt", FileMode.Open, FileAccess.Read))
			{
				var offset = 0;
				var array = new byte[10];
				var count = 10;

				var buffer = new Buffer(offset, array, count);

				//Ler primeiros 10 bytes
				Print(fs, buffer);


				//Ler proximos 10 bytes
				//  Print(fs, buffer);
			}
		}

		private static void Print(FileStream fs, Buffer buffer)
		{
			fs.Read(buffer.Array, buffer.Offset, buffer.Count);

			foreach (var @char in buffer.Array)
				System.Console.Write((char)@char);
		}
	}

	public class Buffer
	{
		public Buffer(int offset, byte[] array, int count)
		{
			Offset = offset;
			Array = array;
			Count = count;
		}
		public int Offset { get; }
		public byte[] Array { get; }
		public int Count { get; }
	}
}
