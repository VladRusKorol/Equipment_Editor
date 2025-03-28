namespace Equipment_Editor.Repository;

public interface IRepositoryBase<T> where T: class
{
   Task<List<T>?> GetAllAsync();
   Task<T> GetByIdAysnc(int id);
   Task<int> CreateAsync(T item); // создание объекта
   Task<int> UpdateAsync(T item); // обновление объекта
   Task<int> DeleteAsync(int id); // удаление объекта по id
}
