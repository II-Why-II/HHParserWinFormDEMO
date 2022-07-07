using System;
using System.IO;

namespace HHParserWinForm.InnerProg.Parser{
    /// <summary>
    /// I'm a greedy devil, so I won't fill in these fields
    /// </summary>
    class VacancyPageParser
    {
        
        private HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
        private static int id = 0;
        private PageModel VacancyModel = new PageModel();
        public PageModel ParsingPage(string _page, string _url){
            using (TextReader pageReader = new StringReader(_page))
                doc.Load(pageReader);

            id++;
            VacancyModel.Id = Convert.ToString(id);

            VacancyModel.Link = _url;
            
            try { VacancyModel.VacancyName = doc.DocumentNode.SelectSingleNode(".//div[@class='hidden-block-for-the-full-version-please-contact-me']").InnerText.Replace("&nbsp;", " "); }
            catch { VacancyModel.VacancyName = null; }

            try { VacancyModel.Text = doc.DocumentNode.SelectSingleNode(".//div[@data-qa='hidden-block-for-the-full-version-please-contact-me']").InnerText; }
            catch { VacancyModel.Text = null; }

            try { VacancyModel.EmployerName = doc.DocumentNode.SelectSingleNode(".//a[@data-qa='hidden-block-for-the-full-version-please-contact-me']").InnerText.Replace("&nbsp;", " "); }
            catch { VacancyModel.EmployerName = null; }

            try { VacancyModel.EmployerHref = doc.DocumentNode.SelectSingleNode(".//a[@data-qa='hidden-block-for-the-full-version-please-contact-me']").Attributes["href"].Value; }
            catch { VacancyModel.EmployerHref = null; }

            try { VacancyModel.Busyness = doc.DocumentNode.SelectSingleNode("//p[@data-qa='hidden-block-for-the-full-version-please-contact-me']").InnerText; }
            catch { VacancyModel.Busyness = null; }

            try { VacancyModel.Cash = doc.DocumentNode.SelectSingleNode(".//div[@data-qa='hidden-block-for-the-full-version-please-contact-me']/span").InnerHtml.Replace("&nbsp;", " ").Replace("<!-- -->", "#").Replace("<span class=\"vacancy-salary-compensation-type\">", "").Replace("<span class=\"\">", "").Replace("</span>", "");
            }
            catch { VacancyModel.Cash = null; }
            GetCash(VacancyModel.Cash);

            try{ VacancyModel.Town = //doc.DocumentNode.SelectSingleNode(".//span[@data-qa='hidden-block-for-the-full-version-please-contact-me']").InnerText.Replace("&nbsp;", " "); ?? 
                                     doc.DocumentNode.SelectSingleNode(".//p[@hidden-block-for-the-full-version-please-contact-me']").InnerText;
                                     //doc.DocumentNode.SelectSingleNode(".//p[@class='hidden-block-for-the-full-version-please-contact-me']").InnerText.Replace("&nbsp;", " ") 
                                     //doc.DocumentNode.SelectSingleNode(".//span[@data-qa='hidden-block-for-the-full-version-please-contact-me']").InnerText ??
            }
            catch { VacancyModel.Town = null; }

            try{ VacancyModel.Experience = doc.DocumentNode.SelectSingleNode("//div[@class='hidden-block-for-the-full-version-please-contact-me']/p").InnerText;
                                           //doc.DocumentNode.SelectSingleNode(".//p[@class='hidden-block-for-the-full-version-please-contact-me']").InnerText ??
            }
            catch { VacancyModel.Experience = null; }

            try { VacancyModel.ExperienceYear = doc.DocumentNode.SelectSingleNode(".//span[@data-qa='hidden-block-for-the-full-version-please-contact-me']").InnerText; }
            catch { VacancyModel.ExperienceYear = null; }

            try{ VacancyModel.PublicationDate = //doc.DocumentNode.SelectSingleNode(".//p[@class='hidden-block-for-the-full-version-please-contact-me']").InnerText.Replace("&nbsp;", " ") ??
                                                doc.DocumentNode.SelectSingleNode(".//p[@class='hidden-block-for-the-full-version-please-contact-me']").InnerText.Replace("&nbsp;", " ");
                                                //doc.DocumentNode.SelectSingleNode(".//div[@class='hidden-block-for-the-full-version-please-contact-me']").InnerText
            }
            catch { VacancyModel.PublicationDate = null; }

            try { VacancyModel.ResponseLinkHaha = doc.DocumentNode.SelectSingleNode(".//a[@data-qa='hidden-block-for-the-full-version-please-contact-me']").Attributes["href"].Value; }
            catch { VacancyModel.ResponseLinkHaha = null; }

            _ = 1;
            return VacancyModel;
            
        }
        public void GetCash(string _inputCash){
            if (_inputCash != null || _inputCash != "з/п не указана"){
                string[] pathInput = _inputCash.Split('#');
                if (pathInput[0] == "от " && pathInput[2] == " "){
                    VacancyModel.SalaryMin = pathInput[1];
                    VacancyModel.SalaryMax = "infinity";
                    VacancyModel.SalaryTax = pathInput[3];
                    VacancyModel.SalaryNalogi = pathInput[4];
                }
                if (pathInput[0] == "от " && pathInput[2] == " до "){
                    VacancyModel.SalaryMin = pathInput[1];
                    VacancyModel.SalaryMax = pathInput[3];
                    VacancyModel.SalaryTax = pathInput[5];
                    VacancyModel.SalaryNalogi = pathInput[6];
                }
                if (pathInput[0] == "до "){
                    VacancyModel.SalaryMax = pathInput[1];
                    VacancyModel.SalaryTax = pathInput[3];
                    VacancyModel.SalaryNalogi = pathInput[4];
                }
                VacancyModel.SalaryNone = null;
            }
            if (_inputCash == null || _inputCash == "з/п не указана"){
                VacancyModel.SalaryNone = "з/п не указана";
                VacancyModel.SalaryMin = null;
                VacancyModel.SalaryMax = null;
                VacancyModel.SalaryNalogi = null;
                VacancyModel.SalaryTax = null;
            }
        }
    }
}
