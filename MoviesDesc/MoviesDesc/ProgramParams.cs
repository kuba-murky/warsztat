using System;
using System.Collections.Generic;

namespace MoviesDesc
{
	public enum InputType
	{
		Directory,
		DirectoryList,
		File,
		FileList,
		ListInFile
	}
	
	public enum OrderType
	{
		None,
		Alpha,
		Rating,
		Duration
	}
	
	public class ProgramParams
	{
		public InputType Input;			//-k/--input-kind
		public string InputPath;		//-i/--input
		public string OutputFolder;		//-o/--output
		public int ThreadsCnt;			//-t/--threads
		public OrderType OutputSort;	//-s/--sort-by
		public bool Recursive;			//-r/--recursive
		public List<string> MoviesExt;	//-e/--exts
		public bool ShowGui;			//-g/--gui
		public bool ShowHelp;			//-h/--help
		public bool ShowVersion;		//-v/--version
		
		public ProgramParams ()
		{
			Input=InputType.Directory;
			InputPath="./";
			OutputFolder="./description";
			ThreadsCnt=10;
			OutputSort=OrderType.None;
			Recursive=false;
			MoviesExt=SplitExts("avi,mkv,mp4,rm,ram,rmvb,rmp,asf,wm,wmv,wmp,ifv,dat,mov,qt,mpg,mpeg,mpe,mpv,m1v,vob,m2ts,m4v,mjp,mjpg,qt,bdmv,bmk,bsf,flv");
			ShowGui=false;
			ShowHelp=false;
			ShowVersion=false;
		}
		
		public int ParseArgs(string[] ProgramArgs)
		{
			int p=0;
			try {
				while (p<ProgramArgs.Length) {
					if ((ProgramArgs[p].Equals("-k")) || (ProgramArgs.Equals("--input-kind"))) Enum.TryParse<InputType>(ProgramArgs[++p],out Input);
					if ((ProgramArgs[p].Equals("-i")) || (ProgramArgs.Equals("--input"))) InputPath=ProgramArgs[++p];
					if ((ProgramArgs[p].Equals("-o")) || (ProgramArgs.Equals("--output"))) OutputFolder=ProgramArgs[++p];
					if ((ProgramArgs[p].Equals("-t")) || (ProgramArgs.Equals("--threads"))) int.TryParse(ProgramArgs[++p],out ThreadsCnt);
					if ((ProgramArgs[p].Equals("-s")) || (ProgramArgs.Equals("--sort-by"))) Enum.TryParse<OrderType>(ProgramArgs[++p],out OutputSort);
					if ((ProgramArgs[p].Equals("-r")) || (ProgramArgs.Equals("--recursive"))) Recursive=true;
					if ((ProgramArgs[p].Equals("-e")) || (ProgramArgs.Equals("--exts"))) MoviesExt=SplitExts(ProgramArgs[++p]);
					if ((ProgramArgs[p].Equals("-g")) || (ProgramArgs.Equals("--gui"))) ShowGui=true;
					if ((ProgramArgs[p].Equals("-h")) || (ProgramArgs.Equals("--help"))) ShowHelp=true;
					if ((ProgramArgs[p].Equals("-v")) || (ProgramArgs.Equals("--version"))) ShowVersion=true;
					p++;
				}
			} catch {
				return p+1;
			}
			return 0;
		}
		
		public string GetHelp()
		{
			return "Program Parameters: \n" +
				"-k/--input-kind KIND	- Kind of input data. Available: Directory, DirectoryList, File, FileList and ListInFile\n" +
				"-i/--input PATH     	- Inputh path. Role of this path depends on kind of input\n" +
				"                    	  * Kind: Directory,     Meaning: Single directory name\n" +
				"                    	  * Kind: DirectoryList, Meaning: Scanned for directories root directory name\n" +
				"                    	  * Kind: File,          Meaning: Single file name\n" +
				"                    	  * Kind: FileList,      Meaning: Scanned for files root directory name\n" +
				"                    	  * Kind: ListInFile,    Meaning: Name of file with movies list\n" +
				"-o/--output PATH    	- Output directory name (created if no exists)\n" +
				"-t/--threads NUMBER 	- Number of searching threads\n" +
				"-s/--sort-by TYPE   	- Output order type. Available: None,Alpha,Rating,Duration\n" +
				"-r/--recursive      	- Search recursive (available for this kinds: DirectoryList, FileList)\n" +
				"-e/--exts	LIST	 	- Coma-separated list of movies file extinsions\n" +
				"-g/--gui            	- Display graphical user interface instead of console\n" +
				"-h/--help           	- Show this help\n" +
				"-v/--version        	- Show program version\n";
		}
		
		private List<string> SplitExts(string Exts)
		{
			List<string> ret=new List<string>();
			foreach (String s in Exts.Split(','))
				ret.Add(s);
			return ret;
		}
	}
}

