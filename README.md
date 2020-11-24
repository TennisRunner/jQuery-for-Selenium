# jQuery-for-Selenium
A jQuery wrapper for automating selenium in C#


```C#
IWebDriver chromeDriver = new ChromeDriver();

// Create the jQuery Selenium Chrome
jQuerySE j = new jQuerySE(chromeDriver as IJavaScriptExecutor);


// Go to the page
chromeDriver.Navigate().GoToUrl("https://www.youtube.com/results?search_query=game");


// Sleep until the element exists on the page
if(j.waitForElement("#items ytd-video-renderer") == false)
	throw new Exception("Unable to find any results");

string thumbnailUrl = null;

// Iterate the elements
j.find("#items ytd-video-renderer").forEach((index, el) =>
{
	// Use jQuery selectors such as :contains()
	if(el.find(".ytd-channel-name:contains('Markiplier')").length > 0)
	{
		// Get and set attributes 
		thumbnailUrl = el.find("ytd-thumbnail img:first").attr("src");

		// return false to stop iterating
		return false;
	}

	return true;
});

if(thumbnailUrl == null)
	throw new Exception("Failed to find the thumbnail url");

MessageBox.Show("The thumbnail url is: " + thumbnailUrl);
```
