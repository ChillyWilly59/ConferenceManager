using System.ComponentModel;

namespace ConferenceManager.Data.Models
{
    public enum ActivityType
    {
        [Description("Доклад, 35-45 минут")]
        Report,

        [Description("Мастеркласс, 1-2 часа")]
        Masterclass,

        [Description("Дискуссия / круглый стол, 40-50 минут")]
        Discussion
    }
}
