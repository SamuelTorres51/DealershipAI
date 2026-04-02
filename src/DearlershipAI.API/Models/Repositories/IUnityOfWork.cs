namespace DearlershipAI.API.Models.Repositories;

public interface IUnityOfWork {
    Task Commit();
}
