namespace SampleGame.Desktop
{
    public abstract class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            using (var game = new SampleGame())
            {
                game.Run();
            }
        }
    }
}
