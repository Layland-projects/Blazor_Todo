using System.Collections.Generic;
using System.Threading.Tasks;
using Todo_Service.Models;

namespace Todo_Service.ApiHelpers.Interfaces
{
    public interface ITodoApiHelper
    {
        Task<IList<TodoItem>> GetTodoItems(int id = 0, int timeout = -1);
        Task<string> PostTodoItem(TodoItem item, int timeout = -1);
        Task<string> DeleteToDoItem(TodoItem item, int timeout = -1);
    }
}