using IronXL;

namespace HHParserWinForm.InnerProg.SubmittedExcel{
    class CreatingExcelFile{
        private static WorkBook workBook = WorkBook.Create();
        private WorkSheet ws = workBook.CreateWorkSheet("SubmittedVacancies");
        public void AddingDataToTable(Parser.PageModel _vacancieModel)
        {
            _ = 1;
            if (Row == 1)
                CreatingFirstRow();
            else
                AddingVacancy(_vacancieModel);
            SavingExcel("ExtraSaveVacancies");
        }
        public void AddingDataToTable()
        {
            CreatingFirstRow();
        }
        private int Row = 1;
        private void CreatingFirstRow(){
            ws["A" + Row].Value = "Number of vacancy";
            ws["B" + Row].Value = "Vacancy name";
            ws["C" + Row].Value = "Vacancy link";
            ws["D" + Row].Value = "Employer name";
            ws["E" + Row].Value = "Employer link";
            ws["F" + Row].Value = "Experience";
            ws["G" + Row].Value = "Salary min";
            ws["H" + Row].Value = "Salary max";
            ws["I" + Row].Value = "Salary tax";
            Row++;
        }
        private void AddingVacancy(Parser.PageModel _vacance){
            ws["A" + Row].Value = Row;
            ws["B" + Row].Value = _vacance.VacancyName;
            ws["C" + Row].Value = _vacance.Link;
            ws["D" + Row].Value = _vacance.EmployerName;
            ws["E" + Row].Value = "https://hh.ru" + _vacance.EmployerHref;
            ws["F" + Row].Value = _vacance.Experience;
            if (_vacance.SalaryNone == "з/п не указана")
                ws["G" + Row].Value = _vacance.SalaryNone;
            else{
                ws["G" + Row].Value = _vacance.Cash;
            }
            Row++;
        }
        public void SavingExcel(string _fileName){
            workBook.SaveAs(_fileName + ".xlsx");
        }
    }
}
