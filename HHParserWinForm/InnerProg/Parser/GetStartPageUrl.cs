namespace HHParserWinForm.InnerProg.Parser
{
    class GetStartPageUrl{
        private static string host = "https://hh.ru";
        private string href = "/search/vacancy?excluded_text=Senior%2C+Unity&search_field=name&search_field=company_name&search_field=description&text=c%23&from=suggest_post&page=";
        private string exclutedText { get; set; }
        private string searchingText { get; set; }
        private int numberOfPage { get; set; }
        private string specialTags { get; set; }
        public string StartPage{
            get{
                return host + href + specialTags;
            }
        }
        public string LoadingSearchPage{
            get{
                return host + "/search/vacancy?excluded_text=" + exclutedText + "&search_field=name&search_field=company_name&search_field=description&text=" +
                    searchingText + "&from=suggest_post&page=" + numberOfPage + "&hhtmFrom=vacancy_search_list";
            }
        }
        public GetStartPageUrl(int _numberOfPage){
            specialTags = _numberOfPage + "&hhtmFrom=vacancy_search_list";
        }
        public GetStartPageUrl(string _href){
            this.href = _href;
        }
        public GetStartPageUrl(string _searchingText, int _numberOfPage){
            this.searchingText = _searchingText;
            this.numberOfPage = _numberOfPage;
        }
        public GetStartPageUrl(string _searchingText, string _excludedText, int _numberOfPage){
            this.exclutedText = _excludedText;
            this.searchingText = _searchingText;
            this.numberOfPage = _numberOfPage;
        }
    }
}
