namespace HHParserWinForm.InnerProg.Sender{
    /// <summary>
    /// I'm a greedy devil, so I won't fill in these fields
    /// </summary>
    public class Paths{
        public static class PageParser{
            public static string ScrollingChargeInBottom = "//a[@data-qa='hidden-block-for-the-full-version-please-contact-me']";
        }
        public static class PageLogin{
            public static string PhoneNumber { get; set; }
            public static string TelephoneBox = "//input[@data-qa='hidden-block-for-the-full-version-please-contact-me']";
            public static string FirstButton = "//button[@data-qa='hidden-block-for-the-full-version-please-contact-me']";
            public static string SmsWritingBox = "//input[@data-qa='hidden-block-for-the-full-version-please-contact-me']";
            public static string SecondButton = "//button[@data-qa='hidden-block-for-the-full-version-please-contact-me']";
        }
        public static class NewPageVacancy{
            
            public static string IsResponsed = "//div[@class='hidden-block-for-the-full-version-please-contact-me']";
            public static string ResponseButton = "//a[@class='hidden-block-for-the-full-version-please-contact-me']";
            public static string SecondResponseSendButton = "//button[@data-qa='hidden-block-for-the-full-version-please-contact-me']";
            public static string NeedLetter = "//div[@class='hidden-block-for-the-full-version-please-contact-me']";
            public static string IsLetter = "//div[@data-qa='hidden-block-for-the-full-version-please-contact-me']";
            public static string AnyCountry = "//button[@data-qa='hidden-block-for-the-full-version-please-contact-me']";

            public static string ChangeResume = "//span[@data-qa='hidden-block-for-the-full-version-please-contact-me']";
            public static string Message = "Здравствуйте. К отклику требовалось сопроводительное письмо, в связи с этим, вы читаете это сообщение.";
            public static string ErrorFillDailyRequests = ".//div[@data-qa='hidden-block-for-the-full-version-please-contact-me']//div[@class='hidden-block-for-the-full-version-please-contact-me']";

        }
    }
}

