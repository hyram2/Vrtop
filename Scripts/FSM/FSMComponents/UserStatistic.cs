namespace StatisticSystem.FSM.FSMComponents
{
    internal class UserStatistic : Singleton<UserStatistic>
    {
        public string Name { get; set; }
        public UserStatistics Statistics = new UserStatistics();
    }
}