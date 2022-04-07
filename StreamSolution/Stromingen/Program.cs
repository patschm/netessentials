
using System.IO.Compression;
using System.Text;

//WritingSpartan();
//ReadSpartan();
//WriteClean();
//ReadClean();
//WriteZipped();
//ReadZipped();

GluurBuur();

void GluurBuur()
{
    FileSystemWatcher watcher = new FileSystemWatcher();
    watcher.Path = @"D:\Test";
    watcher.Created += (s, a) => Console.WriteLine("Created: " + a.Name);
    watcher.Deleted += (s, a) => Console.WriteLine("Deleted: " + a.Name);
    watcher.Changed += (s, a) => Console.WriteLine($"Changed {a.Name}");

    watcher.EnableRaisingEvents = true;
    Console.WriteLine("Enter to stop");
    Console.ReadLine();

}

void ReadZipped()
{
    FileStream fs = File.OpenRead(@"D:\Test\hello2.zip");
    GZipStream gzip =  new GZipStream(fs, CompressionMode.Decompress);
    StreamReader reader = new StreamReader(gzip);
    string line;
    while ((line = reader.ReadLine()) != null)
    {
        Console.WriteLine(line);
    }
    reader.Close();
}

void WriteZipped()
{
    FileStream fs = File.Create(@"D:\Test\hello2.zip");
    GZipStream gzip = new GZipStream(fs, CompressionMode.Compress);
    StreamWriter writer = new StreamWriter(gzip);
    string txt = "Hello World ";
    for (int i = 0; i < 1000; i++)
    {
        writer.WriteLine(txt + i);
    }
    writer.Flush();
    writer.Close();
    // fs.Flush();
    fs.Close();
}

void ReadClean()
{
    FileStream fs = File.OpenRead(@"D:\Test\hello2.txt");
    StreamReader reader = new StreamReader(fs);
    string line;
    while ((line = reader.ReadLine()) != null)
    {
        Console.WriteLine(line);
    }
    reader.Close();
}

void WriteClean()
{
    FileStream fs = File.Create(@"D:\Test\hello2.txt");
    StreamWriter writer = new StreamWriter(fs);
    string txt = "Hello World ";
    for (int i = 0; i < 1000; i++)
    {
        writer.WriteLine(txt + i);
    }
    writer.Flush();
    writer.Close();
   // fs.Flush();
    fs.Close();
}

void ReadSpartan()
{
    FileStream fs = File.OpenRead(@"D:\Test\hellos.txt");

    byte[] buffer = new byte[10];
    int nrRead = 0;
    do
    {
        Array.Clear(buffer, 0, buffer.Length);
        nrRead = fs.Read(buffer, 0, buffer.Length);
        string txt = Encoding.UTF8.GetString(buffer);
        Console.Write(txt);
    }
    while (nrRead > 0);
}

void WritingSpartan()
{
    FileStream fs = File.Create(@"D:\Test\hellos.txt");
    string txt = "Hello World ";
    for(int i = 0; i < 1000;  i++)
    {
        byte[] buffer = Encoding.UTF8.GetBytes(txt + i + "\r\n");
        fs.Write(buffer);
    }
    fs.Flush();
    fs.Close();
}


