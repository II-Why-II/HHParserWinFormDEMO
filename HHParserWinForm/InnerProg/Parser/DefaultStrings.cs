using MongoDB.Driver;

namespace HHParserWinForm.InnerProg
{
    class DefaultStrings
    {
        private static IMongoClient MongoClientLocal = new MongoClient("mongodb://localhost:27017");
        public static IMongoDatabase MongoDatabaseHHParse = MongoClientLocal.GetDatabase("HHParse02");
        public static IMongoCollection<Parser.PageModel> MongoCollectionVacancies = MongoDatabaseHHParse.GetCollection<Parser.PageModel>("Vacancies");

        public static string[] IgnoreVacanciesByName = {
        "unity",
        "python",
        "c++",
        "1c",
        "1с",
        "java ",
        "javascript",
        "golang",
        "go",
        "node.js",
        "oracle",
        "delphi",
        "senior",
        "руководитель",
        "архитектор",
        "заместитель",
        "начальник",
        "менеджер",
        "специалист",
        "ведущий",
        "главный",
        "проектировщик",
        "разработчик бд",
        "бд",
        "релокация",
        "montenegro" };
        public static string[] MustHaveAnyTagInVacanceName = {
            "разработчик",
            "developer",
            "c#",
            "с#",
            "net",
            "программист",

        };
    }
}
