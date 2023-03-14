
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

//Console.Write("Please enter a filepath, and optinally a new file name spearated by a space: ");

string filePathInput = GetFilePath(args[0]);
string inputFileName = Path.GetFileName(filePathInput).Replace(".txt", "");
string baseFilePath = Path.GetDirectoryName(filePathInput);
string filePathOutput;

if (args.ElementAtOrDefault(1) == null)
{
    filePathOutput = @$"{baseFilePath}\{inputFileName}_Formatted.txt";
}
else
{
    filePathOutput = @$"{baseFilePath}\{args[1]}.txt";
}

var lines = RemoveHomoglyphs(filePathInput, homoglyphs);
WriteFormattedFile(filePathOutput, lines);

static string GetFilePath(string path)
{
    string filePath = path;
    while (true)
    {
        filePath = filePath.Replace("\"", "");
        if (string.IsNullOrEmpty(filePath))
        {
            Console.Write("Filepath cannot be empty, please enter a valid filepath: ");
        }
        else
        {
            try
            {
                StreamReader reader = new StreamReader(filePath);
                break;
            }
            catch
            {
                Console.Write("File not found, please enter a valid filepath: ");
            }
        }
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
