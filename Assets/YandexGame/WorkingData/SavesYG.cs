
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        public int currentLevel = 1;
        public bool isSuggestedReview = false;
        public SavesYG()
        {
        }
    }
}
