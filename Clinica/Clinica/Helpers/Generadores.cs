namespace Clinica.Helpers
{
    public class Generadores
    {
        private static Random random = new Random();
        private static string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public static string GetNewMatricula(int largo)
        {
            return GetRandom(caracteres, largo);
        }

        private static string GetRandom(string caracteres, int largo)
        {
            return new string(Enumerable.Repeat(caracteres, largo).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
