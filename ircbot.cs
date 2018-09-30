using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

class IrcBot
{
    public static string SERVER = "irc.wobscale.website";
    private static int PORT = 6667;
    private static string USER = "USER IrcBot 0 * :IrcBot";
    private static string NICK = "GoobaBot";
    private static string CHANNEL = "#study";

    public static StreamWriter writer;

    public static void Main()
    {
        NetworkStream stream;
        TcpClient irc;
        string inputLine;
        StreamReader reader;
        try
        {
            irc = new TcpClient(SERVER,PORT);
            stream = irc.GetStream();
            reader = new StreamReader(stream);
            writer  = new StreamWriter(stream);
            writer.WriteLine("NICK " + NICK);
            writer.Flush();
            writer.WriteLine(USER);
            writer.Flush();
            while (true)
            {
                while((inputLine = reader.ReadLine()) != null)
                {
                    Console.WriteLine("<-" + inputLine);
                    string[] splitInput = inputLine.Split(new Char[]{' '});
                    if (splitInput[0] == "PING")
                    {
                        string PongReply = splitInput[1];
                        writer.WriteLine("PONG " + PongReply);
                        Console.WriteLine("PONG " + PongReply);
                        writer.Flush();
                        continue;
                    }
                    switch (splitInput[1])
                    {
                        case "001":
                        string JoinString = "JOIN " + CHANNEL;
                        writer.WriteLine(JoinString);
                        writer.Flush();
                        break;
                        default:
                        break;
                    }
                }
                writer.Close();
                reader.Close();
                irc.Close();
            }

        }
        catch (System.Exception)
        {
            throw;
        }
    }
}