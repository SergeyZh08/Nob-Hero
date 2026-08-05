public interface IStorageService
{
    public void Save(SaveData data);
    public SaveData Load();
    public void Delete();
}