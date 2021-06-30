namespace EtlTool
{
    public interface IFile
    {
        void read(string path);
        string decode(string EncodedText);
    }
}
