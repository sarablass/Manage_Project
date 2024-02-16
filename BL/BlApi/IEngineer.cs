namespace BlApi;
/// <summary>
/// A engineer interface with actions: adding a engineer, reading a engineer, reading engineers, updating a engineer and deleting a engineer.
/// </summary>
public interface IEngineer
{
    public int Create(BO.Engineer boEngineer);
    public BO.Engineer? Read(int id);
    public IEnumerable<BO.Engineer> ReadAll(Func<BO.Engineer, bool>? filter = null);
    public void Update(BO.Engineer boEngineer);
    public void Delete(int id);
}
