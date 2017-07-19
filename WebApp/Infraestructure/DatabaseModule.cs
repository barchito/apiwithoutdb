using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Infraestructure
{
    public static class DatabaseModule
    {
        public static IServiceCollection AddDirectDatabaseConnection(this IServiceCollection services, string connectionstring)
        {
            services.AddTransient<IDbConnection>((s) =>
            {
                //var connectionString = ConfigurationManager
                return new SqlConnection(connectionstring);
            });
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services.AddTransient<ITodoItemRepository, TodoItemRepository>();
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services.AddTransient<ITodoService, TodoService>();
        }

        public static IServiceCollection AddQueries(this IServiceCollection services)
        {
            return services.AddTransient<IQueryHandler<GetAllTodoQuery,IEnumerable<TodoItem>>, GetAllTodoQueryHandler>();
        }

    }

    public class GetAllTodoQuery
    {
        public bool Completed { get; set; }
        public string Description { get; set; }
    }

    public interface IQueryHandler<T,TOut>
    {
        Task<TOut> Handle(T Query);
    }

    public class GetAllTodoQueryHandler : IQueryHandler<GetAllTodoQuery,IEnumerable<TodoItem>>
    {
        public Task<IEnumerable<TodoItem>> Handle(GetAllTodoQuery Query)
        {
            IEnumerable<TodoItem> todos = new List<TodoItem>();
            return Task.FromResult(todos);
        }
    }

    public interface ITodoService
    {
        void Create(TodoItem todoItem);

        IEnumerable<TodoItem> GetAll();
    }

    public class TodoService : ITodoService
    {
        public void Create(TodoItem todoItem)
        {

        }

        public IEnumerable<TodoItem> GetAll()
        {
            return null;
        }
    }

    public interface ITodoItemRepository
    {
        IEnumerable<TodoItem> GetAll();

        void AddTodoItem();
        void DeleteTodoItem();
        void Save();
    }

    public class TodoItemRepository : ITodoItemRepository
    {
        public TodoItemRepository()
        {

        }

        public void AddTodoItem()
        {
        }

        public void DeleteTodoItem()
        {
        }

        public IEnumerable<TodoItem> GetAll()
        {
            return null;
        }

        public void Save()
        {
        }
    }



}
