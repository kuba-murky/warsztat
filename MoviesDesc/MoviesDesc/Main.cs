using System;

namespace MoviesDesc
{
	class MainClass
	{
		public static int Main (string[] args)
		{
			//1. Parse program arguments
			ProgramParams PrgPar=new ProgramParams();
			if (!PrgPar.ParseArgs(args)) {
				Console.WriteLine("Cannot parse program parameters. Try run only with: -h");
				return -1;
			}
			
			return 0;
		}
	}
}
