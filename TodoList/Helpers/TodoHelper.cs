using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo_Service.Models;
using Todo_Service.ApiHelpers;
using Todo_Service.ApiHelpers.Interfaces;

namespace TodoList.Helpers
{
    public class TodoHelper
    {
        ITodoApiHelper _apiHelper;
        public TodoHelper(ITodoApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }
        public static string strikeThroughCSS(TodoItem t) => t.IsComplete ? "strikeThrough" : "";
        public async Task<IList<TodoItem>> GetTodoList()
        {
            return await _apiHelper.GetTodoItems();
        }
        public async Task<string> AddToDo(string name)
        {
            try
            {
                if (!string.IsNullOrEmpty(name))
                {
                    var status = await _apiHelper.PostTodoItem(new TodoItem { Name = name });
                    return status;
                }
                return "InvalidName";
            }
            catch
            {
                return "Error";
            }
        }
        public async Task<string>RemoveTodo(TodoItem itemToRemove)
        {
            return await _apiHelper.DeleteToDoItem(itemToRemove);
        }
    }
}
