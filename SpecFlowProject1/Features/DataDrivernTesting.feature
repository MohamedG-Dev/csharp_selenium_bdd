Feature: DataDriven Testing feature

Search for the Testers Talk channel using data driven method

@TestersTalk
Scenario: Search for the TestersTalk using data driven testing
	Given Open the browser
	When navigate to the youtube
	Then search for 'SpecFlow Tutorials'

@TestersTalk
Scenario: Search for a number value using data driven testing
	Given Open the browser
	When navigate to the youtube
	Then search for 1000

@TestersTalk
Scenario: Examples Keyword search data driven testing
	Given Open the browser
	When navigate to the youtube
	Then search with <searchValue>
Examples:
	| searchValue              |
	| Specflow by TestersTalk  |
	| Selenium by Rahul Shetty |


@TestersTalk
Scenario: DataTable example in data driven testing
	Given Open the browser
	When navigate to the youtube
	Then Enter search keyword in Youtube
		| searchValue                    |
		| Specflow by TestersTalk        |
		| Selenium by Rahul Shetty       |
		| Slenium python by rahul shetty |