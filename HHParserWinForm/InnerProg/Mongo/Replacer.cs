using HHParserWinForm.InnerProg.Parser;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHParserWinForm.InnerProg.Mongo{
    class Replacer{
        public void Replace(string _field, string _searchingValue, string _insertedValue){
            var filter = Builders<PageModel>
                .Filter.Eq(_field, _searchingValue);
            var update = Builders<PageModel>
                .Update.Set(_field, _insertedValue);
            
            IMongoCollection<PageModel> collectionVacancies =
                DefaultStrings.MongoDatabaseHHParse.GetCollection<PageModel>("Vacancies");

            collectionVacancies.UpdateMany(filter, update);
        }
        public void ReplaceVacanceAndEmployerName(string[] _fields, string _searchingValue, string _insertedValue){
            foreach (var field in _fields){
                if (!string.IsNullOrEmpty(field)){
                    IMongoCollection<PageModel> collectionVacancies =
                    DefaultStrings.MongoDatabaseHHParse.GetCollection<PageModel>("Vacancies");

                    if (field == "EmployerName"){
                        var filterEmployerName = Builders<PageModel>
                            .Filter.Where(e => e.EmployerName.Contains(_searchingValue));

                        var collection = collectionVacancies.Find(filterEmployerName).ToList();
                        foreach (var vac in collection){
                            string empName = vac.EmployerName.Replace(_insertedValue, " ");
                            var update = Builders<PageModel>
                                .Update.Set(field, empName);
                            collectionVacancies.UpdateMany(filterEmployerName, update);
                        }
                    }
                    if (field == "VacancyName"){
                        var filterVacancyName = Builders<PageModel>
                            .Filter.Where(e => e.VacancyName.Contains(_searchingValue));

                        var collection = collectionVacancies.Find(filterVacancyName).ToList();
                        foreach (var vac in collection){
                            string vacName = vac.VacancyName.Replace(_insertedValue, " ");
                            var update = Builders<PageModel>
                                .Update.Set(field, vacName);
                            collectionVacancies.UpdateMany(filterVacancyName, update);
                        }
                    }
                }
            }
        }

    }
}
