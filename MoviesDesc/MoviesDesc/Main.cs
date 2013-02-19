using System;

namespace MoviesDesc
{
	class MainClass
	{
		public static string Version="0.0.1";
		
		public static int Main (string[] args)
		{
			//1. Parse program arguments
			ProgramParams PrgPar=new ProgramParams();
			int p;
			if ((p=PrgPar.ParseArgs(args))!=0) {
				Console.Error.WriteLine("Cannot parse given parameter number {0}. Try run only with: -h",p);
				return -1;
			}
			
			if (PrgPar.ShowHelp) {
				Console.WriteLine("MoviesDesc");
				Console.WriteLine(PrgPar.GetHelp());
				return 0;
			}
			
			if (PrgPar.ShowVersion) {
				Console.WriteLine("MoviesDesc v.{0}",Version);
				return 0;
			}
			
			return 0;
		}
	}
}
