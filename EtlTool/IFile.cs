namespace EtlTool
{
    public interface IFile
    {
        // - read file content;
        void read(string path);
        // - decode text;
        string decode(string EncodedText);
    }
}
