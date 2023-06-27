namespace SampleGame.Desktop
{
    public abstract class Program
    {
        public static void Main(string[] args)
        {
            using (var game = new SampleGame())
            {
                game.Run();
            }
        }
    }
}
