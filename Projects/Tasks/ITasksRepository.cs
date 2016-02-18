namespace CrazyAppsStudio.Delegacje.Tasks
{
    public interface ITasksRepository
    {
        UsersTasks UsersTasks { get; }
        DictionariesTasks DictionariesTasks { get; }
		BusinessTripsTasks BusinessTripsTasks { get; }
		CurrenciesTasks CurrenciesTasks { get; }
    }
}
