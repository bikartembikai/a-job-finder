namespace a_job_finder.Interfaces;

public interface IDbService
{
    Task<IEnumerable<T>> Query<T>(string spname, object parameter);
    Task<T> QueryFirstOrDefault<T>(string spname, object parameter);
    Task<List<T>> QueryMultiple<T>(string spname, object parameter);
    Task<bool> VerifyConnection();
}
