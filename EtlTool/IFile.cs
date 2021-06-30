namespace EtlTool
{
    public interface IFile
    {
        void read(string path);
        string decrypt(string EncrptedText);
    }
}
