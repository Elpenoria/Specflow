using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using Newtonsoft.Json;
using SeleniumExtras.WaitHelpers;

namespace TestProject.StepDefinitions
{

    [Binding]
	public sealed class Test
	{
		public IWebDriver driver;

		private readonly ScenarioContext _scenarioContext;

		public Test(ScenarioContext scenarioContext)
		{
			_scenarioContext = scenarioContext;
		}

		[Given(@"I Login to the site with credentials gainchanger / justdoit")]
		public void loginWithCorrectCredentials()
		{

			driver = new ChromeDriver("C:/Users/Perdorues/Documents");

			driver.Manage().Window.Maximize();

			driver.Navigate().GoToUrl("https://cozy-fairy-5394bc.netlify.app");

			driver.FindElement(By.Id("username")).SendKeys("gainchanger");
			Thread.Sleep(1000);

			driver.FindElement(By.Id("password")).SendKeys("justdoit");
			Thread.Sleep(1000);

			driver.FindElement(By.Id("submit")).Click();
			WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

			wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div/header[1]/div/div/div[2]/div/div/div/div/nav[1]/ul/li[4]")));



		}

		[When(@"I navigate to the following url")]
		public void WhenINavigateToTheFollowingUrl()
		{
			driver.FindElement(By.XPath("/html/body/div[1]/div/header[1]/div/div/div[2]/div/div/div/div/nav[1]/ul/li[4]")).Click();
			Thread.Sleep(5000);
		}



		[When(@"Access the first blog in the list of blog posts present in the resources page")]
		public void accesTheFirstBlog()
		{

			var element = driver.FindElement(By.XPath("/html/body/div[2]/div/section[2]/div[2]/div/div[1]/div/div/div/div/div[1]/article[1]/div/a"));
			IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
			js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");

			element.Click();
			Thread.Sleep(3000);


		}


		public IList<IWebElement> h1 = new List<IWebElement>();
		public IList<IWebElement> meta = new List<IWebElement>();
		public IList<IWebElement> h2 = new List<IWebElement>();
		public IList<IWebElement> p = new List<IWebElement>();

		[Then(@"I can extract the following fields from the blog page\. \(H(.*), Meta title, Meta description, H(.*) elements, Paragraph elements\)")]
		public void ThenICanExtractTheFollowingFieldsFromTheBlogPage_HMetaTitleMetaDescriptionHElementsParagraphElements(int p0, int p1)
		{
			
			h1 = driver.FindElements(By.TagName("h1"));

			meta = driver.FindElements(By.TagName("meta"));

			h2 = driver.FindElements(By.TagName("h2"));

			p = driver.FindElements(By.TagName("p"));



		}




		[Then(@"Export the fields in a file in json format")]
		public void exportFieldsInJsonFormat()
		{
			Random rnd = new Random();
			int randomNum = rnd.Next(1000);
			string convert = randomNum.ToString();
			string path = "C:/Users/Perdorues/source/repos/SpecFlowSelenium/SpecFlowSelenium/Data/output" + convert + ".json";

			//Adding h1 elements

			int i = 1;
			String titleh1 = "H1 Elements";
			Dictionary<int, string> datah1 = new Dictionary<int, string>();
			foreach (var el in h1)
			{
				datah1.Add(i, el.Text);
				i++;
			}

			string jsonh1 = JsonConvert.SerializeObject(datah1, Formatting.Indented);
			string formath1 = JsonConvert.SerializeObject(titleh1, Formatting.Indented);

			File.AppendAllText(path, formath1);
			File.AppendAllText(path, jsonh1);

			//Adding the meta title element

			int k = 1;
			String title = "Title Element";
			Dictionary<int, string> dataTitle = new Dictionary<int, string>();

			dataTitle.Add(k, driver.Title);


			string jsonTitle = JsonConvert.SerializeObject(dataTitle, Formatting.Indented);
			string formatTitle = JsonConvert.SerializeObject(title, Formatting.Indented);

			File.AppendAllText(path, formatTitle);
			File.AppendAllText(path, jsonTitle);

			//Adding meta elements

			int m = 1;
			String titleMeta = "Meta Elements";
			Dictionary<int, string> datameta = new Dictionary<int, string>();
			foreach (var el in meta)
			{
				datameta.Add(m, el.GetAttribute("content"));
				m++;
			}

			string jsonmeta = JsonConvert.SerializeObject(datameta, Formatting.Indented);
			string formatmeta = JsonConvert.SerializeObject(titleMeta, Formatting.Indented);

			File.AppendAllText(path, formatmeta);
			File.AppendAllText(path, jsonmeta);


			//Adding h2 elements

			int l = 1;
			String titleh2 = "H2 Elements";
			Dictionary<int, string> datah2 = new Dictionary<int, string>();
			foreach (var el in h2)
			{
				datah2.Add(l, el.Text);
				l++;
			}

			string jsonh2 = JsonConvert.SerializeObject(datah2, Formatting.Indented);
			string formath2 = JsonConvert.SerializeObject(titleh2, Formatting.Indented);

			File.AppendAllText(path, formath2);
			File.AppendAllText(path, jsonh2);

			//Adding paragraph elements

			int j = 1;
			String titlep = "Paragraph Elements";
			Dictionary<int, string> dataP = new Dictionary<int, string>();
			foreach (var el in p)
			{
				dataP.Add(j, el.Text);
				j++;
			}
			string jsonP = JsonConvert.SerializeObject(dataP, Formatting.Indented);
			string formatP = JsonConvert.SerializeObject(titlep, Formatting.Indented);

			File.AppendAllText(path, formatP);
			File.AppendAllText(path, jsonP);



			driver.Quit();

			

		}
	}
}