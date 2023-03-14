
Dictionary<char, char> homoglyphs = new Dictionary<char, char>()
{
    ['Ä'] = 'A',
    ['Å'] = 'A',
    ['Á'] = 'A',
    ['Â'] = 'A',
    ['À'] = 'A',
    ['Ã'] = 'A',
    ['â'] = 'a',
    ['ä'] = 'a',
    ['à'] = 'a',
    ['å'] = 'a',
    ['á'] = 'a',
    ['ã'] = 'a',
    ['ß'] = 'B',
    ['ẞ'] = 'B',
    ['þ'] = 'b',
    ['Þ'] = 'b',
    ['þ'] = 'b',
    ['Ç'] = 'C',
    ['ç'] = 'c',
    ['Ð'] = 'D',
    ['É'] = 'E',
    ['Ê'] = 'E',
    ['Ë'] = 'E',
    ['È'] = 'E',
    ['é'] = 'e',
    ['ê'] = 'e',
    ['ë'] = 'e',
    ['è'] = 'e',
    ['ƒ'] = 'f',
    ['Í'] = 'I',
    ['Î'] = 'I',
    ['Ï'] = 'I',
    ['ï'] = 'i',
    ['î'] = 'i',
    ['ì'] = 'i',
    ['í'] = 'i',
    ['ı'] = 'i',
    ['Ñ'] = 'N',
    ['ñ'] = 'n',
    ['Ö'] = 'O',
    ['Ó'] = 'O',
    ['Ô'] = 'O',
    ['Ò'] = 'O',
    ['Õ'] = 'O',
    ['ô'] = 'o',
    ['ö'] = 'o',
    ['ò'] = 'o',
    ['ó'] = 'o',
    ['ø'] = 'o',
    ['ð'] = 'o',
    ['õ'] = 'o',
    ['Ü'] = 'U',
    ['Ú'] = 'U',
    ['Û'] = 'U',
    ['Ù'] = 'U',
    ['ü'] = 'u',
    ['û'] = 'u',
    ['ù'] = 'u',
    ['ú'] = 'u',
    ['µ'] = 'u',
    ['Ý'] = 'Y',
    ['ÿ'] = 'y',
    ['ý'] = 'y',
    ['Ø'] = '0'
};

List<string> programArgs = CheckArgs(args);
string filePathOutput = CreateFilePathOutput(programArgs);
var lines = RemoveHomoglyphs(programArgs[0], homoglyphs);
WriteFormattedFile(filePathOutput, lines);

static List<string> CheckArgs(string[] args)
{
    List<string> values = new List<string>();
    char[] nonValid = { '<', '>', '\\', '%', '#', '&', '{', '}', '*', '?', '/', ' ', '$', '!', '\'', '"', ':', '@', '+', '|', '`', '=' };
    if(args.Length == 0)
    {
        Console.WriteLine("No filepath has been provided");
        System.Environment.Exit(1);
        return null;
    }
    else if(args.Length == 1)
    {
        ValidateFilePath(args[0]);
        values.Add(args[0]);
        return values;
    }
    else if(args.Length == 2)
    {
        ValidateFilePath(args[0]);
        values.Add(args[0]);
        foreach(char c in args[1])
        {
            if (nonValid.Contains(c))
            {
                Console.WriteLine($"File name contains invalid character \"{c}\"");
                System.Environment.Exit(1);
            }
        }
        values.Add(args[1]);
        return values;
    }
    else
    {
        Console.WriteLine("Too many arguments given");
        System.Environment.Exit(1);
        return null;
    }
}
static string ValidateFilePath(string path)
{
    if (File.Exists(path))
    {
        return path;
    }
    else
    {
        Console.WriteLine("Filepath is not valid");
        System.Environment.Exit(1);
        return null;
    }
}
static string CreateFilePathOutput(List<string> _args)
{
    string filePath;
    if (_args.Count() == 1)
    {
        filePath = @$"{Path.GetDirectoryName(_args[0])}\{Path.GetFileNameWithoutExtension(_args[0])}_homoglpyhs_removed.txt";
    }
    else
    {
        filePath = @$"{Path.GetDirectoryName(_args[0])}\{_args[1]}.txt";
    }
    return filePath;
}
static List<string> RemoveHomoglyphs(string filePath, Dictionary<char, char> _homoglyphs)
{
    StreamReader reader = new StreamReader(filePath);
    List<string> results = new List<string>();
    while (true)
    {
        string line = reader.ReadLine();
        if (string.IsNullOrEmpty(line))
        {
            break;
        }
        else
        {
            foreach (char c in line)
            {
                if (_homoglyphs.Keys.Contains(c))
                {
                    line = line.Replace(c, _homoglyphs[c]);
                }
            }
        }
        results.Add(line);
    }
    reader.Close();
    return results;
}
static void WriteFormattedFile(string filePath, List<string> lines)
{
    StreamWriter writer = new StreamWriter(filePath);
    foreach(string line in lines)
    {
        writer.WriteLine(line);
    }
    writer.Close();
    Console.WriteLine("Homoglyphs have been removed!");
}