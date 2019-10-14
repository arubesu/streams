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

            using (var fs = new FileStream("animals.txt", FileMode.Open, FileAccess.Read))
            {
                var offset = 0;
                var array = new byte[10];
                var count = 10;

                var buffer = new Buffer(offset, array, count);

                //Ler primeiros 10 bytes
                Print(fs, buffer);

                
                //Ler proximos 10 bytes
                Print(fs, buffer);
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
