using System.IO.Compression;
using System.Text;
using System;
using System.IO;

namespace Streams
{
    class Program
    {
        static void Main(string[] args)
        {
            // Abrir o arquivo animals.txyt
            // Ler 10 bytes do arquivo
            // imprimir no console
            //            Lendo10bytes();

            var message = "Hello World!";

            Compactar(message);
            Descompactar();
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
