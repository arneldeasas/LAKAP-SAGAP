namespace LAKAPSAGAP.Services.Repositories;

public interface IUoMRepository
{
    Task<string?> CreateUoM(UoMViewModel uoM);
    Task<UoM?> GetUoM(string uomId);
    Task<List<UoM>> GetAllUoM();
    Task UpdateUoM(UoMViewModel uoM);
    //Task DeleteUoM(UoM uoM); // No Deletion of Data Please
}
