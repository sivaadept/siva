using System;
using System.Linq;
using System.Threading;
using AutomationFramework.Application.Selenium.Extensions;
using AutomationFramework.Application.Selenium.Screens;
using OpenQA.Selenium;
using WJIV.App.Interfaces;

namespace WJIV.Application.Selenium.Screens
{
    public class AddBuildingLevelSelenuim : AbstractSeleniumScreen, AddBuildingLevel
    {
        public AddBuildingLevelSelenuim(IWebDriver driver)
            : base(driver)
        {
        }

        public bool IsAddBuildingLevelHeadingDisplayed()
        {
            return Driver.FindElement(By.Id("form-info-wj4")).FindElement(By.TagName("h2")).Text.Equals("Add Building Level");
        }

        public bool IsAllBuildingFieldsDispalyed()
        {
            return Driver.FindElement(By.Id("OrgBuildings_0__BuildingName")).Displayed && Driver.FindElement(By.Id("OrgBuildings_1__BuildingName")).Displayed && Driver.FindElement(By.Id("OrgBuildings_2__BuildingName")).Displayed
                   && Driver.FindElement(By.Id("OrgBuildings_3__BuildingName")).Displayed && Driver.FindElement(By.Id("OrgBuildings_4__BuildingName")).Displayed && Driver.FindElement(By.Id("OrgBuildings_5__BuildingName")).Displayed;
                
        }

        public bool IsSaveButtonDisplayed()
        {

            return Driver.FindElement(By.Id("btnSave")).Displayed;
        }

        public bool IsSaveAndAddAnotherButtonDisplayed()
        {

            return Driver.FindElement(By.Id("btnAddSave")).Displayed;
        }

        public bool IscancelButtonDisplayed()
        {
            return IsElementEnabled(By.Name("Cancel"));
        }

        public bool VerifyMaxCharAllFields()
        {
            return Driver.FindElement(By.Id("OrgBuildings_0__BuildingName")).GetAttribute("maxlength") == "100" && Driver.FindElement(By.Id("OrgBuildings_1__BuildingName")).GetAttribute("maxlength") == "100" && Driver.FindElement(By.Id("OrgBuildings_2__BuildingName")).GetAttribute("maxlength") == "100"
                   && Driver.FindElement(By.Id("OrgBuildings_3__BuildingName")).GetAttribute("maxlength") == "100" && Driver.FindElement(By.Id("OrgBuildings_4__BuildingName")).GetAttribute("maxlength") == "100" && Driver.FindElement(By.Id("OrgBuildings_5__BuildingName")).GetAttribute("maxlength") == "100";
               
        }

        public bool VerifyInvalidcharInNameField()
        {
            Driver.FindElement(By.Id("OrgBuildings_0__BuildingName")).SendKeys("a@");
            Driver.FindElement(By.Id("OrgBuildings_1__BuildingName")).Click();
            Thread.Sleep(100);
            Driver.FindElement(By.Id("btnSave")).Click();
            Thread.Sleep(1000);
            return Driver.FindElement(By.Id("divblerrormsgpopup")).Text.Trim().Equals("Invalid Building Name.");

        }

        public bool VerifyDuplicateBuildingLevelName()
        {
            Driver.FindElement(By.Id("OrgBuildings_0__BuildingName")).SendKeys("Build1");
            Driver.FindElement(By.Id("OrgBuildings_1__BuildingName")).Click();
            Thread.Sleep(100);
            Driver.FindElement(By.Id("btnSave")).Click();
            Thread.Sleep(1000);
            return Driver.FindElement(By.Id("divblerrormsgpopup")).Text.Trim().Equals("One or more Building Names already exists. Please create a new Building Name to continue.");
        }
        public void SaveButton_Click()
        {
            Thread.Sleep(1000);
            Driver.FindElement(By.Id("btnSave")).Click();
        }
        
        public void AddAndSaveAnotherBtnClick()
        {
            Driver.FindElement(By.Id("btnAddSave")).Click();
            Thread.Sleep(10000);
        }

        public bool VerifyBuildingSucessfullySavedMsg()
        {
            return Driver.FindElement(By.Id("divsavebuildingsuccess")).FindElement(By.TagName("h6")).Text.Trim().Equals("Building Level successfully saved.");
            
            
        }

        public bool VerifyCancelYesButton(string url)
        {
            Driver.FindElement(By.Name("Cancel")).Click();
            Driver.FindElements(By.Id("subpopup_delete1")).Where(x => x.GetAttribute("onclick").Equals("Onyesclick()")).First().Click();
            return Driver.Url.Equals(url);
        }

        public bool VerifyCancelNoButton()
        {
            string AddSubjectPageUrl = Driver.Url;
            Driver.FindElement(By.Name("Cancel")).Click();
            Driver.FindElement(By.Id("subpopup_cancel1")).Click();
            string PostClickUrl = Driver.Url;
            return AddSubjectPageUrl.Equals(PostClickUrl);
        }

        public bool VerifyAddAndSaveAnotherButton()
        {
            Driver.WaitForElementToBeEnabled(By.Id("btnAddSave"), 30000);
            Driver.FindElement(By.Id("btnAddSave")).Click();
            Thread.Sleep(1000);
            WebElementExtensions.WaitForPageToLoad(Driver);
            return Driver.FindElement(By.Id("OrgBuildings_0__BuildingName")).GetAttribute("value").Equals("") && Driver.FindElement(By.Id("OrgBuildings_1__BuildingName")).GetAttribute("value").Equals("") && Driver.FindElement(By.Id("OrgBuildings_2__BuildingName")).GetAttribute("value").Equals("")
                     && Driver.FindElement(By.Id("OrgBuildings_3__BuildingName")).GetAttribute("value").Equals("") && Driver.FindElement(By.Id("OrgBuildings_4__BuildingName")).GetAttribute("value").Equals("") && Driver.FindElement(By.Id("OrgBuildings_5__BuildingName")).GetAttribute("value").Equals("");
        }

        public void SetBuildingNameField0(string Bname0)
        {
            Thread.Sleep(10000);
            Driver.FindElement(By.Id("OrgBuildings_0__BuildingName")).SendKeys(Bname0);
        }

        public void SetBuildingNameField1(string Bname1)
        {
            Driver.FindElement(By.Id("OrgBuildings_1__BuildingName")).SendKeys(Bname1);
        }

        public void SetBuildingNameField2(string Bname2)
        {
            Driver.FindElement(By.Id("OrgBuildings_2__BuildingName")).SendKeys(Bname2);
        }

        public void SetBuildingNameField3(string Bname3)
        {
            Driver.FindElement(By.Id("OrgBuildings_3__BuildingName")).SendKeys(Bname3);
        }

        public void SetBuildingNameField4(string Bname4)
        {
            Driver.FindElement(By.Id("OrgBuildings_4__BuildingName")).SendKeys(Bname4);
        }

        public void SetBuildingNameField5(string Bname5)
        {
            Driver.FindElement(By.Id("OrgBuildings_5__BuildingName")).SendKeys(Bname5);
        }

        // Building level Setup

        public bool VerifyBuildingLevelSetupPopup()
        {
            return Driver.FindElement(By.Id("BuildinglevelDiv-wmls")).Displayed && Driver.FindElement(By.Id("BuildinglevelDiv-wmls")).FindElement(By.ClassName("h6")).Text.Trim().Contains("Building Level Reporting Setup") &&
                 Driver.FindElement(By.Id("BuildinglevelDiv-wmls")).FindElement(By.ClassName("pop-up-container")).Text.Trim().Contains("Before you can begin administration of WMLS III, a building level structure must be set up to ensure easy roll-up of reports. Please click Continue to begin adding Building Level Structure.");
        }

        public void ContinueButton_Click()
        {
            Driver.FindElement(By.XPath("html/body/div[20]/div[2]/div[2]/div/div[3]/input")).Click();
        }

        public bool IsAddBuildingPopupDisplayed()
        {
            return Driver.FindElement(By.Id("popupAddBuildings")).Displayed;
        }

        public bool VerifyAddBuildingLevelPopupHeading()
        {
            return Driver.FindElement(By.Id("divblheader")).Text.Trim().Equals("Add Building Level");
                           
        }

        public bool IsInstructionsMsgDisplayed()
        {
            return Driver.FindElements(By.TagName("b")).Where(x => x.Text.Trim().Equals("Instructions")).First().Displayed;
            //Driver.FindElement(By.Id("infolevel")).FindElements(By.TagName("div ")).Where(x => x.Text.Trim().Contains("Enter the name of the building(s) you would like to add to your reporting structure.")).First().Displayed;
        }

        public bool IsBuildingNamesHeadingDispalyed()
        {
            return Driver.FindElements(By.TagName("b")).Where(x => x.Text.Trim().Contains("Building Name(s):")).First().Displayed;
        }

        public bool IsPopupSaveButtonDisplayed()
        {

            return Driver.FindElement(By.Id("btnSavepartial")).Displayed;
        }

        public bool IsPopupSaveAndAddAnotherButtonDisplayed()
        {

            return Driver.FindElement(By.Id("btnAddSavepartial")).Displayed;
        }

        public bool IsPopupcancelButtonDisplayed()
        {
            return IsElementEnabled(By.Id("btnSaveUserCancel"));
        }

        public bool VerifyPopupInvalidcharInNameField()
        {
            Driver.FindElement(By.Id("OrgBuildings_0__BuildingName")).SendKeys("a@");
            Driver.FindElement(By.Id("OrgBuildings_0__BuildingName")).SendKeys(Keys.Enter);
            //Driver.FindElement(By.Id("OrgBuildings_1__BuildingName")).Click();
            Thread.Sleep(100);
            Driver.FindElement(By.Id("btnSavepartial")).Click();
            Thread.Sleep(1000);
            return Driver.FindElement(By.Id("divblerrormsgpopup")).Text.Trim().Equals("Invalid Building Name.");

        }

        public bool VerifyPopupCancelNoButton()
        {
            string AddSubjectPageUrl = Driver.Url;
            Driver.FindElement(By.Id("btnSaveUserCancel")).Click();
            Driver.FindElement(By.Id("popup_no")).Click();
            string PostClickUrl = Driver.Url;
            return AddSubjectPageUrl.Equals(PostClickUrl);
        }

        public bool VerifyPopupCancelYesButton(string url)
        {
            Driver.FindElement(By.Id("btnSaveUserCancel")).Click();
            Driver.FindElement(By.Id("popup_yes")).Click();
            return Driver.Url.Equals(url);
        }

        public void ClickBuildingNameField1()
        {
            Driver.FindElement(By.Id("OrgBuildings_1__BuildingName")).Click();
        }

        public void PopupSaveButton_Click()
        {
            Driver.FindElement(By.Id("btnSavepartial")).Click();
        }


       
        


                                                                                                          
    }
}
