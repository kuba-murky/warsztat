using System;
using System.IO.Ports;

namespace SerialTerminal
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("List of serial ports:");
			foreach (string s in SerialPort.GetPortNames()) {
				Console.Write ("[ {0} ]",s);
			}
			Console.ReadKey();
			SerialPort SP=new SerialPort("/dev/ttyS1",115200);
			Console.WriteLine(SP.ReadLine());
		}
	}
}
