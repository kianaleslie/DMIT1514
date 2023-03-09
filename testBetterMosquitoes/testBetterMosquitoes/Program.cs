using System;

namespace MosquitoAttack
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new MosquitoAttackGame())
            {
                game.Run();
            }
        }
    }
}
