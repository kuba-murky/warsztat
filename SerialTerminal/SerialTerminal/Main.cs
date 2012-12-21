using System;
using System.IO.Ports;//Dołączenie przestrzeni nazw obsługujących port szeregowy
using System.Threading;//Obsługa wątków

namespace SerialTerminal
{
	class MainClass
	{
		private static bool run=true;//zmienna informująca wątek o tym czy ma odbierać znaki
		
		public static void OnChars(object param)//funkcja uruchamiana w wątku do odbierania znaków z urządzenia i wypisywania ich na terminal
		{
			SerialPort port = (SerialPort)param;//konwersja parametru na obiekt klasy SerialPort
			while (run) {
				port.ReadTimeout=100;	//czekaj maksymalnie 100ms na znak
				try {
					char rcv = (char)port.ReadChar();//odczytaj znak
					Console.Write(rcv);	//wypisz odczytany znak na konsoli
				} catch {}	//jeśli będzie błąd (nie będzie znaku przez 100ms) to nic nie rób
			}
		}
			
		public static void Main (string[] args)
		{	
			string[] AvailablePorts=SerialPort.GetPortNames();//pobranie dostępnych portów szeregowych
			
			Console.WriteLine("Select serial port");
			for (int i=0;i<AvailablePorts.Length;i++) {	//wypisanie dostępnych portów
				Console.WriteLine("{0} - {1}",i,AvailablePorts[i]);
			}
			
			Console.Write("Please enter serial port number: ");
			int ind;
			if (!int.TryParse(Console.ReadLine(),out ind)) {//konwersja wprowadzonego znaku na wartość liczbową
				Console.WriteLine("E: Cannot select this serial port");//jeśli konwersja się nie udała to zakończ program
				return;
			}
			
			Console.Write("Please enter serial port baudrate: ");
			int baud;
			if (!int.TryParse(Console.ReadLine(),out baud)) {//konwersja wprowadzonego tekstu na wartość liczbową baudrate
				Console.WriteLine("E: Cannot set given baudrate!");//jeśli konwersja się nie udała to zakończ program
				return;
			}
			
			Console.Write("Connecting to: {0} at {1} baud...",AvailablePorts[ind],baud);
			
			SerialPort SP;
			try {//spróbuj...
				SP=new SerialPort(AvailablePorts[ind],baud);//...stworzyć obiekt klasy...
				SP.Open();	//...otworzyć port
				Console.WriteLine("OK");
			} catch (Exception e) {//jeśli się nie udało to zakończ program
				Console.WriteLine("FAIL\nE: {0}",e.Message);
				return;
			}
			
			Thread thr=new Thread(new ParameterizedThreadStart(OnChars));//stworzenie wątku odbierającego
			thr.Start(SP);//uruchomienie funkcji OnChars w wątku. Jako parametr jest przekazywany obiekt klasy portu szeregowego
			
			Console.TreatControlCAsInput=true;//traktuj ctrl+c jako znak i nie kończ wtedy applikacji tylko prześlij do urządzenia
			ConsoleKeyInfo cmd;
			do {
				cmd=Console.ReadKey(true);//pobranie pojedynczego znaku z konsoli (bez wypisania go na terminal)
				SP.Write(cmd.KeyChar.ToString());//wysłanie znaku do urządzenia szeregowego
			} while (cmd.KeyChar!=17);//Powtarzaj dopóki nie wciśnięto Ctrl+Q
			
			run=false;//kończ wątek odbierający znaki
			
			SP.Close();//zamknij dostęp do urządzenia szeregowego
		}
	}
}
