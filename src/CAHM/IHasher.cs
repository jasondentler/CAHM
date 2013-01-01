namespace CAHM
{
    public interface IHasher
    {

        string Hash(string salt, string secret);

    }
}
