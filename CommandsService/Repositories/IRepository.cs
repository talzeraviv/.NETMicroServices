namespace CommandsService.Repositories
{
    public interface IRepository
    {
        bool SaveChanges();
        bool Exists(int Id);
    }
}