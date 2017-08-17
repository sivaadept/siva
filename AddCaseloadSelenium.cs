using System.Linq;
using System.Threading;
using AutomationFramework.Application.Selenium.Extensions;
using AutomationFramework.Application.Selenium.Screens;
using OpenQA.Selenium;
using WJIV.App.Interfaces;

namespace WJIV.Application.Selenium.Screens
{
    public class AddCaseloadSelenium : AbstractSeleniumScreen, AddCaseload
    {
        public AddCaseloadSelenium(IWebDriver driver)
            : base(driver)
        {
        }

        public bool IsCaseloadNameFieldDisplayed()
        {
            return IsElementEnabled(By.Id("inpTitle"));
        }

        public bool VerifyMaxCharCaseloadNameField()
        
            return (Driver.FindElement(By.Id("inpTitle")).GetAttribute("maxlength") == "100");
        
        public bool VerifySaveButtonDisplayed()
        
            return Driver.FindElement(By.Id("btnSave")).Displayed;
        

        public bool VerifyCaseloadErrorMessage()
        {
            Driver.FindElement(By.Id("inpTitle")).Clear();
            Driver.FindElement(By.Id("btnSave")).Click();
            return Driver.FindElement(By.Id("spErrName")).Text.Trim().Equals("Caseload folder name required");
        }

        public bool VerifyPageTitleDisplayed(string title)
        {
            return title.Equals(Driver.Title);
        }

        public void SetCaseloadName(string caseloadName)
        {
            Thread.Sleep(6000);
            WebElementExtensions.WaitForElement(Driver, By.Id("inpTitle"), 30000);
            Driver.FindElement(By.Id("inpTitle")).SendKeys(caseloadName);
            Thread.Sleep(2000);
        }

        public void SaveButton_Click()
        {
            WebElementExtensions.WaitForElement(Driver, By.Id("btnSave"));
            Driver.FindElement(By.Id("btnSave")).Click();
        }

        public void ClearCaseloadNamefield()
        {
            Driver.FindElement(By.Id("inpTitle")).Clear();
        }

        public bool VerifyPopulateCaseloadName(string caseloadName)
        {
            var CaseloadName = Driver.FindElement(By.ClassName("search-results")).FindElements(By.TagName("td"))[1].Text;
            return caseloadName.Equals(CaseloadName);
        }

        public bool IsCaseloadActionIconsDisplayed()
        {
            return (Driver.FindElement(By.ClassName("subject-edit")).Displayed
                && Driver.FindElement(By.ClassName("subject-Delete")).Displayed);
        }

        public bool IsCaseloadActionIconsEnabled()
        {
            var caseloadName = Driver.FindElement(By.ClassName("search-results")).FindElements(By.TagName("td"))[3];
            return (caseloadName.FindElement(By.ClassName("subject-edit")).Enabled
                && caseloadName.FindElement(By.ClassName("subject-Delete")).Enabled);
        }

        public bool IsFooterDisplayed()
        {
            var checkboxes = Driver.FindElements(By.CssSelector("input[type='checkbox']"));

            if (checkboxes.Count > 2)
            {
                for (int i = 1; i < 3; i++)
                    Driver.FindElements(By.CssSelector("input[type='checkbox']"))[i].Click();
            }

            return (Driver.FindElement(By.Id("thSelectedItems")).Text.Trim().Equals("With Selected Items")
                && Driver.FindElement(By.ClassName("subject-Delete")).Displayed);
        }

        public bool IsActionIconsGrayedOut()
        {

            bool bool_value;
            bool_value = false
            
            var checkboxes = Driver.FindElements(By.CssSelector("input[type='checkbox']"));

            if (checkboxes.Count > 2)
            {
                for (int i = 1; i < checkboxes.Count; i++)
                {
                    bool_value = (Driver.FindElements(By.TagName("tr"))[i].FindElements(By.TagName("td"))[3].FindElements(By.TagName("a"))[0].GetAttribute("class").Trim().Equals("subject-edit-gray"))
                                    && (Driver.FindElements(By.TagName("tr"))[i].FindElements(By.TagName("td"))[3].FindElements(By.TagName("a"))[1].GetAttribute("class").Trim().Equals("caseload-Delete-gray"));
                    if (bool_value == true) 
                    {
                        j++;
                        if (j == 2)
                            break;

                    }
                        
                
            }
            return bool_value;
        }

        public bool Only25RecordsPerPageAreDisplayed()
        {
            return Driver.FindElement(By.Id("tbdyCaseFolders")).FindElements(By.TagName("tr")).Count().Equals(25);
        }

        public bool VerifyPagination()
        {
            return Driver.FindElement(By.ClassName("pagination")).FindElements(By.TagName("li")).Where(x => x.Text.Contains("First")).First().Displayed;
        }

        public bool VerifyDuplicateCaseloadErrorMessage(string caseloadName)
        {
            Driver.FindElement(By.Id("inpTitle")).SendKeys(caseloadName);
            Driver.FindElement(By.Id("btnSave")).Click();
            return Driver.FindElement(By.Id("spErrName")).Text.Trim().Equals("Caseload folder name already exists");
        
        public bool VerifyDuplicateErrorMessage()
        {
            Driver.WaitForPageToLoad();
            return Driver.FindElement(By.ClassName("field-validation-error-caseload")).Text.Contains("Caseload folder name already exists");
        }
        public bool VerifySelectAllFunctionality()
        {
            Driver.FindElement(By.Id("chbCaseFolders")).Click();
            Driver.FindElements(By.ClassName("selectedId"))[1].Click();
            Driver.FindElements(By.ClassName("selectedId"))[1].Click();
            bool result= Driver.FindElement(By.Id("chbCaseFolders")).Selected;
            Driver.FindElement(By.Id("chbCaseFolders")).Click();
            return result;
        
    }
}

