using System;

namespace RandomWalk
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Walk())
                game.Run();
        }
    }
}
