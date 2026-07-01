using Azor.Web.Models.Entities;

namespace Azor.Web.Services;

public interface ISeasonStorage
{
    List<Season> GetAll();
    Season GetById(int id);
    void Add(Season season);
    void Update(Season season);
    void Delete(int id);
}