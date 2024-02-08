namespace Business.GalaxiaWordle.Interfaces
{
    public interface IWordleContext
    {
        int WordLength { get; set; }
        string RandomWordApiUrl { get; }
    }
}