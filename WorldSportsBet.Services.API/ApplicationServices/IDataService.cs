namespace WorldSportsBet.Services.API.ApplicationServices
{
    public interface IDataService<TEntity> where TEntity : class
    {
        TEntity Get(string route, IDictionary<string, string> urlSegment);
    }
}
