using System;
using System.IO;

static class RandomUtil
{
    public static string GetRandomString()
    {
        string path = Path.GetRandomFileName();
        path = path.Replace(".", ""); // Remove period.
        return path.Substring(0,8);
    }
}
class Program
{
    static void Main()
    {
        // Test the random string method.
        Console.WriteLine(RandomUtil.GetRandomString());
        Console.ReadLine();
    }
}